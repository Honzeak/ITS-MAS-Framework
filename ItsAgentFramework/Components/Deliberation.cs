using System;
using System.Collections.Generic;

namespace ItsAgentFramework.Components
{
    public abstract class Deliberation<TState>
    {
        protected Sequencer _sequencer;
        protected TState _state;
        protected string currentGoal;

        public Deliberation(TState state, Sequencer sequencer)
        {
            if (state is null) throw new ArgumentNullException(nameof(state));
            if (sequencer is null) throw new ArgumentNullException(nameof(sequencer));

            _state = state;
            _sequencer = sequencer;

            _sequencer.TaskFailed += HandleTaskFailed!;
            _sequencer.PlanFinished += HandlePlanFinished!;
            //_sequencer.TaskFinished+= HandleTaskFinished!;
        }

        public void Update()
        {
            _sequencer.Update();
        }

        public virtual void SetNewGoal(string goal)
        {
            currentGoal = goal;
            _ = ResolveTasksFromGoal(goal); // Here just for reference, this method should be used in the overriden implementation
        }

        protected abstract IList<AgentTask> ResolveTasksFromGoal(string goal);

        protected virtual void HandleTaskFailed(object sequencer, TaskTerminatedEventArgs ttea)
        {
            InitFailSafeTask();
        }

        protected abstract void InitFailSafeTask();

        protected virtual void HandlePlanFinished(object sequencer, TaskTerminatedEventArgs ttea)
        {
            _sequencer.Stop();
        }

    }
}