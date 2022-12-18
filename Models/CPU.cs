using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Manager.Views;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualBasic;
using System.Management;
using System.Xml.Linq;

namespace Task_Manager.Models
{
    internal class CPU
    {
        private PerformanceCounter cpuusage;
        private PerformanceCounter cpufrequency;
        private ManagementObjectSearcher myProcessorObject;

        private double maxSpeed;
        private String name;
        private double l1CacheSize;
        private double l2CacheSize;
        private double l3CacheSize;
        private int threadCount;
        private int numberOfCores;
        private int numberOfLogicalProcessors;

        public CPU() 
        {
            cpuusage = new PerformanceCounter("Processor Information", "% Processor Utility", "_Total");
            cpufrequency = new PerformanceCounter("Processor Information", "% Processor Performance", "_Total");
            myProcessorObject = new ManagementObjectSearcher("select * from Win32_Processor");
            
            foreach (ManagementObject obj in myProcessorObject.Get())
            {
                Name = Convert.ToString(obj["Name"]);
                CPU_BaseFrequency = Convert.ToDouble(obj["MaxClockSpeed"]) / 1000;
                ThreadCount = Convert.ToInt16(obj["ThreadCount"]);
                NumberOfCores = Convert.ToInt16(obj["NumberOfCores"]);
                NumberOfLogicalProcessors = Convert.ToInt16(obj["NumberOfLogicalProcessors"]);
                //Numbers of Cores times 32KB plus Data cache(same math)
                L1CacheSize = numberOfLogicalProcessors * 64;
                L2CacheSize = Convert.ToDouble(obj["L2CacheSize"]);
                L3CacheSize = Convert.ToDouble(obj["L3CacheSize"]);
                break;
            }

            /*while (true) {
                //System.Diagnostics.Debug.WriteLine("First: " + getCurrentCpuUsage() + " %");
                
                
                System.Diagnostics.Debug.WriteLine(getCurrentCpuUsage(500) + "%" +" "+ string.Format("{0:F2}", getCurrentCpuFrequency(500)) + " GHz");

                
            }*/
        }
        
        public double L1CacheSize 
        {
            get { return l1CacheSize; }
            set { l1CacheSize = value; }
        }
        public double L2CacheSize 
        {
            get { return l2CacheSize; }
            set { l2CacheSize = value; }
        }
        public double L3CacheSize 
        {
            get { return l3CacheSize; }
            set { l3CacheSize = value; }
        }

        public int ThreadCount 
        {
            get { return threadCount; }
            set { threadCount = value; }
        }
        public int NumberOfCores 
        {
            get { return numberOfCores; }
            set { numberOfCores = value; }
        }
        public int NumberOfLogicalProcessors 
        {
            get { return numberOfLogicalProcessors; }
            set { numberOfLogicalProcessors = value; }
        }
        //Return CPU string detail
        public String Name 
        {
            get 
            { return name; }
            set { name = value; }
        }

        //Return current base Frequency
        public double CPU_BaseFrequency 
        { 
            get 
            { return maxSpeed;}
            set { maxSpeed = value; }
        }

        //Return current CPU usage
        public int getCurrentCpuUsage(int miliseconds)
        {
            cpuusage.NextValue();
            Thread.Sleep(miliseconds);
            return (int)cpuusage.NextValue();
        }

        //Return current GHz
        public double getCurrentCpuFrequency(int miliseconds)
        {
            cpufrequency.NextValue();
            Thread.Sleep(miliseconds);
            return this.maxSpeed * cpufrequency.NextValue() / 100;
        }
    }
}
