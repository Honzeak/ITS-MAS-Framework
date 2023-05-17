using System.Linq;
using ItsAgentFramework.Communication;
using ItsAgentFramework.Components;
using System;
using static ItsAgentFramework.Components.SkillTerminatedEventArgs;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

namespace ItsAgentFramework
{
    public abstract class Sequencer
    {
        private List<Skill> _skillList;
        private AgentTask? _currentTask;
        public Queue<AgentTask>? TaskQueue { get; private set; }
        public event EventHandler<TaskTerminatedEventArgs>? TaskFailed;
        public event EventHandler<TaskTerminatedEventArgs>? PlanFinished;

        public Sequencer(List<Skill> skillList)
        {
            _skillList = skillList;

            foreach (var skill in _skillList)
            {
                skill.SkillTerminated += HandleSkillTerminated!;
            }
        }

        public void Start()
        {
            if (_currentTask is null)
            {
                if (TaskQueue is null)
                    throw new InvalidOperationException("Cannot start sequencer with empty task queue.");
                SwitchCurrentTask(TaskQueue.Dequeue());
            }
        }

        public void Update()
        {
            if (_currentTask is null)
                Start();
            foreach (var skill in _skillList)
            {
                skill.Update();
            }
        }

        public void Stop()
        {
            foreach (var skill in _skillList)
            {
                skill.Deactivate();
            }
        }

        public void AddTask(AgentTask agentTask, AddTaskOptions taskOptions = AddTaskOptions.AppendLast)
        {
            if (agentTask is null) throw new ArgumentNullException(nameof(agentTask));
            var agentTaskToList = new List<AgentTask>() { agentTask };
            AddTasks(agentTaskToList, taskOptions);
        }

        public void AddTasks(IEnumerable<AgentTask> taskEnum, AddTaskOptions addTaskOptions = AddTaskOptions.AppendLast)
        {
            if (taskEnum is null) throw new ArgumentNullException(nameof(taskEnum));
            var taskQueue = new Queue<AgentTask>(taskEnum);

            if (TaskQueue is null)
            {
                TaskQueue = taskQueue;
                return;
            }

            switch (addTaskOptions)
            {
                case AddTaskOptions.Overwrite:
                    TaskQueue = taskQueue;
                    SwitchCurrentTask(TaskQueue.Dequeue());
                    break;
                case AddTaskOptions.AppendLast:
                    TaskQueue.Concat(taskQueue.ToArray());
                    break;
                case AddTaskOptions.AppendFirst:
                    TaskQueue = new ( taskQueue.Concat(TaskQueue));
                    SwitchCurrentTask( TaskQueue.Dequeue());
                    break;
                default:
                    throw new ArgumentException("Unexpected options value", nameof(addTaskOptions));
            }
        }

        protected void SwitchCurrentTask(AgentTask newTask)
        {
            if (newTask is null) throw new ArgumentNullException(nameof(newTask));

            _currentTask = newTask;

            foreach (var configTuple in _currentTask.DependentSkills)
            {
                Skill skill;
                try
                {
                    skill = _skillList.First(x => x.GetType() ==  configTuple.skilltype);
                }
                catch (InvalidOperationException e)
                {
                    throw new KeyNotFoundException("Cannot executed task, as there is skill definition mismatch between sequencer and task specification."); ;
                }
                if (skill is null) throw new ArgumentNullException(nameof(skill));


                if (configTuple.configTransform is not null)
                    configTuple.configTransform(skill);
            }
            
            DeactivateSkills();
            ActivateTaskSkills(_currentTask);
        }


        //override with pattern matching
        protected virtual void HandleSkillTerminated(object objSkill, SkillTerminatedEventArgs e)
        {
            var skill = (Skill)objSkill;
            skill.Deactivate();

            switch( e.reason) 
            {
                case SkillTerminatedEventArgs.Reason.Finished:  OnTaskFinished((Skill)objSkill);
                    break;
                case SkillTerminatedEventArgs.Reason.Failed:  OnTaskFailed((Skill)objSkill);
                    break;
                case SkillTerminatedEventArgs.Reason.Interrupted:  OnSkillInterrupted((Skill)objSkill);
                    break;
                default: Debug.Log($"Skill of type {objSkill.GetType()} terminated without specifying a reason");
                    break;
            };
        }

        protected virtual void OnTaskFinished(Skill skill)
        {
            DeactivateSkills();
            _currentTask = null;
            if (TaskQueue!.Count == 0)
                PlanFinished?.Invoke(this, new TaskTerminatedEventArgs(skill));
            else 
                SwitchCurrentTask(TaskQueue.Dequeue());
        }

        protected virtual void OnTaskFailed(Skill skill)
        {
            DeactivateSkills();
            TaskFailed?.Invoke(this, new TaskTerminatedEventArgs(skill));
        }
        protected abstract void OnSkillInterrupted(Skill skill);

        protected virtual void ActivateTaskSkills(AgentTask task)
        {
            foreach( var skillTuple in task.DependentSkills)
            {
                //_skillList[skillTuple.skilltype].Activate();
                foreach (var skill in _skillList)
                {
                    if (skill.GetType() == skillTuple.skilltype) skill.Activate();
                }
            }
        }
        protected void DeactivateSkills()
        {
            foreach( var skill in _skillList)
            {
                skill.Deactivate();
            }
        }
        public enum AddTaskOptions
        {
            Overwrite,
            AppendLast,
            AppendFirst
        }
    }

    public class TaskTerminatedEventArgs : EventArgs
    {
        public Skill TerminatedSkill { get; private set; }
        public ITSMessage? ItsMessage { get; private set; }
        public ACLMessage? AclMessage { get; private set; }

        public TaskTerminatedEventArgs(Skill terminatedSkill,
            ITSMessage? itsMessage = null, ACLMessage? aclMessage = null)
        {
            TerminatedSkill = terminatedSkill;
            ItsMessage = itsMessage;
            AclMessage = aclMessage;
        }
    }
}
