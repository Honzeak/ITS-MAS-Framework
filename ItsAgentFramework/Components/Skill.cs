using ItsAgentFramework.Communication;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsAgentFramework.Components
{
    public abstract class Skill
    {
        public bool IsActive { get; private set; }

        public event EventHandler<SkillTerminatedEventArgs>? SkillTerminated;
        // Test for event

        public virtual void Activate()
        {
            IsActive = true;
        }
        public virtual void Deactivate()
        {
            IsActive = false;
        }
        public void Update()
        {
            if (!IsActive) return;
            Act();
        }

        protected abstract void Act();

        protected virtual void OnSkillTerminated(SkillTerminatedEventArgs.Reason reason)
        {
            SkillTerminated?.Invoke(this, new SkillTerminatedEventArgs(reason));
        }

        //public override bool Equals(object? obj)
        //{
        //    return obj is not null ? GetType() == obj.GetType() : false;
        //}
    }

    public class SkillTerminatedEventArgs : EventArgs
    {
        public Reason reason { get; private set; }
        public ITSMessage? ItsMessage { get; private set; }
        public ACLMessage? AclMessage { get; private set; }

        public SkillTerminatedEventArgs(Reason reason,
            ITSMessage? itsMessage = null, ACLMessage? aclMessage = null)
        {
            this.reason = reason;
            ItsMessage = itsMessage;
            AclMessage = aclMessage;
        } 
        public enum Reason
        {
            Finished,
            Interrupted,
            Failed
        }
    }
}
