using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Manager.Views;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualBasic;

namespace Task_Manager.Models
{
    internal class CPU
    {
        private static PerformanceCounter cpuusage;
        private static PerformanceCounter cpufrequency;

        public CPU() 
        {
            cpuusage = new PerformanceCounter("Processor Information", "% Processor Utility", "_Total");
            cpufrequency = new PerformanceCounter("Processor Information", "% of Maximum Frequency", "_Total");
            while (true) {
                //System.Diagnostics.Debug.WriteLine("First: " + getCurrentCpuUsage() + " %");
                
                
                System.Diagnostics.Debug.WriteLine("Usage: " + getCurrentCpuUsage(250) + " %" + "Frequency: " + (getCurrentCpuFrequency(250)*4.70/100) + "GHz");

                
            }
        }
        public int getCurrentCpuUsage(int miliseconds)
        {
            cpuusage.NextValue();
            Thread.Sleep(miliseconds);
            return (int)cpuusage.NextValue();
        }
        public int getCurrentCpuFrequency(int miliseconds)
        {
            cpufrequency.NextValue();
            Thread.Sleep(miliseconds);
            return (int)cpufrequency.NextValue();
        }
    }
}
