using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsAgentFramework.Components
{
    public class Agent<TState>
    {
        protected Deliberation<TState> _deliberation;

        public Agent(Deliberation<TState> deliberationLayer)
        {
            if (deliberationLayer is null) throw new ArgumentNullException(nameof(deliberationLayer));

            _deliberation = deliberationLayer;
        }

        public void Update()
        {
            _deliberation.Update();
        }
        public void SetNewGoal(string goal) => _deliberation.SetNewGoal(goal);
    }
}
