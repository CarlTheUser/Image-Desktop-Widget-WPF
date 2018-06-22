using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Desktop_Widget
{
    public abstract class StartupBehavior
    {
        
        public event EventHandler<StartupBehaviorEventArgs> RunAtStartupSet;

        public abstract bool IsRunAtStartup { get; }

        public abstract void RunAtStartup(bool enabled);

        protected void OnRunAtStartupSet(bool isSuccessful, bool currentValue)
        {
            RunAtStartupSet?.Invoke(this, new StartupBehaviorEventArgs(isSuccessful, currentValue));
        }

        public class StartupBehaviorEventArgs : EventArgs
        {
            public bool StartupEnabledChanged { get; }

            public bool CurrentStartupValue { get; }

            public StartupBehaviorEventArgs(bool startupEnabledChanged, bool currentvalue)
            {
                StartupEnabledChanged = startupEnabledChanged;
                CurrentStartupValue = currentvalue;
            }
            
        }

    }
}
