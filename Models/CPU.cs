using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Manager.Views;
using System.Diagnostics;
using System.Threading;

namespace Task_Manager.Models
{
    internal class CPU
    {
        private static PerformanceCounter ramp;

        public CPU() 
        {
            ramp = new PerformanceCounter("Processor Information", "% Processor Utility", "_Total");
            while(true) {
                //System.Diagnostics.Debug.WriteLine("First: " + getCurrentCpuUsage() + " %");
                
                
                System.Diagnostics.Debug.WriteLine("Second: " + getCurrentCpuUsage(500) + " %");
                
            }
        }
        public int getCurrentCpuUsage(int miliseconds)
        {
            ramp.NextValue();
            Thread.Sleep(miliseconds);
            return (int)ramp.NextValue();
        }
    }
}
