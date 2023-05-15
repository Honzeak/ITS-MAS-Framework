using System;
using System.Collections.Generic;

namespace ItsAgentFramework.Components
{
    public abstract class AgentTask
    {
        private IEnumerable<(Type skilltype, Action<Skill>? configTransform)>? _dependentSkills;
        public IEnumerable<(Type skilltype, Action<Skill>? configTransform)> DependentSkills
        {
            get
            {
                if (_dependentSkills is null)
                    throw new InvalidOperationException("Tried to interact with task before its initialization.");
                return _dependentSkills;
            }
            protected set
            {
                _dependentSkills = value;
            }
        }

        public AgentTask()
        {
            var activatedSkills = GetSkillDependencies();

            DependentSkills = activatedSkills;
        }

        protected abstract List<(Type, Action<Skill>?)> GetSkillDependencies();
    }
}