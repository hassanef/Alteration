using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.BackgroundServiceConfigurations
{
    /// <summary>
    /// an object of configuration of BackgroundTasks are read from appsetting.json 
    /// </summary>
    public class BackgroundServiceConfiguration
    {
        public string Name { get; set; }
        public int Interval { get; set; }
        /// <summary>
        /// Some BackgroundTasks is being started by ApiStart and do the action and finish
        /// </summary>
        public bool IsFireAndForget { get; set; }
    }
}
