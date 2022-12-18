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

namespace Task_Manager.Models
{
    internal class CPU
    {
        private static PerformanceCounter cpuusage;
        private static PerformanceCounter cpufrequency;
        private ManagementObjectSearcher myProcessorObject;
        private double maxSpeed;


        public CPU() 
        {
            cpuusage = new PerformanceCounter("Processor Information", "% Processor Utility", "_Total");
            cpufrequency = new PerformanceCounter("Processor Information", "% Processor Performance", "_Total");
            myProcessorObject = new ManagementObjectSearcher("select * from Win32_Processor");
            
            foreach (ManagementObject obj in myProcessorObject.Get())
            {
                System.Diagnostics.Debug.WriteLine("Name  -  " + obj["Name"]);
                System.Diagnostics.Debug.WriteLine("DeviceID  -  " + obj["DeviceID"]);
                System.Diagnostics.Debug.WriteLine("Manufacturer  -  " + obj["Manufacturer"]);
                System.Diagnostics.Debug.WriteLine("CurrentClockSpeed  -  " + obj["CurrentClockSpeed"]);
                System.Diagnostics.Debug.WriteLine("MaxClockSpeed  -  " + obj["MaxClockSpeed"]);
                maxSpeed = Convert.ToDouble(obj["MaxClockSpeed"]) / 1000;
                System.Diagnostics.Debug.WriteLine("Virtualization  -  " + obj["VirtualizationFirmwareEnabled"]);
                System.Diagnostics.Debug.WriteLine("Number of Threads  -  " + obj["ThreadCount"]);
                System.Diagnostics.Debug.WriteLine("Caption  -  " + obj["Caption"]);
                System.Diagnostics.Debug.WriteLine("NumberOfCores  -  " + obj["NumberOfCores"]);
                System.Diagnostics.Debug.WriteLine("NumberOfEnabledCore  -  " + obj["NumberOfEnabledCore"]);
                System.Diagnostics.Debug.WriteLine("NumberOfLogicalProcessors  -  " + obj["NumberOfLogicalProcessors"]);
                System.Diagnostics.Debug.WriteLine("Architecture  -  " + obj["Architecture"]);
                System.Diagnostics.Debug.WriteLine("Family  -  " + obj["Family"]);
                System.Diagnostics.Debug.WriteLine("ProcessorType  -  " + obj["ProcessorType"]);
                System.Diagnostics.Debug.WriteLine("Characteristics  -  " + obj["Characteristics"]);
                System.Diagnostics.Debug.WriteLine("AddressWidth  -  " + obj["AddressWidth"]);
                break;
            }

            while (true) {
                //System.Diagnostics.Debug.WriteLine("First: " + getCurrentCpuUsage() + " %");
                
                
                System.Diagnostics.Debug.WriteLine(getCurrentCpuUsage(500) + "%" +" "+ string.Format("{0:F2}", getCurrentCpuFrequency(500)) + " GHz");

                
            }
        }
        public double CPU_MaxFrequency 
        { 
            get ;
        }
        public int getCurrentCpuUsage(int miliseconds)
        {
            cpuusage.NextValue();
            Thread.Sleep(miliseconds);
            return (int)cpuusage.NextValue();
        }
        public double getCurrentCpuFrequency(int miliseconds)
        {
            cpufrequency.NextValue();
            Thread.Sleep(miliseconds);
            return this.maxSpeed * cpufrequency.NextValue() / 100;
        }
    }
}
