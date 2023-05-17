using ItsAgentFramework.Components;
using System.Collections;
using UnityEngine;

namespace Assets.MAS_ITS.ItsAgentFramework.Components
{
    public abstract class AgentMonoWrapper<TState> : MonoBehaviour
    {
        public Agent<TState> agent;
        // Use this for initialization
        void Start()
        {
            InitAgent();
        }

        // Update is called once per frame
        void Update()
        {
            agent.Update();
        }
        protected abstract void InitAgent();
    }
}