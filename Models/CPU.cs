// The MIT License(MIT)
//
// Copyright(c) 2022 Marcos Bagarotti Marin
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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
    /// <summary>
    /// This class will collect some data of the first CPU in the System.
    /// </summary>
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

        /// <summary>
        /// CPU class Constructor.
        /// Init the Performance Counter and collect the data of the CPU.
        /// </summary>
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

        }

        /// <summary>
        /// GET/SET L1CacheSize
        /// Value is manually calculated:
        /// L1 Data cache = 32 KB per core
        ///  L1 Instruction cache = 32 KB per core
        ///  L1 cache size per core = 32 KB + 32 KB, which = 64 KB
        ///  L1CacheSize = numberOfLogicalProcessors * 64
        /// </summary>
        public double L1CacheSize 
        {
            get { return l1CacheSize; }
            set { l1CacheSize = value; }
        }

        /// <summary>
        /// GET/SET L2CahceSize
        /// </summary>
        public double L2CacheSize 
        {
            get { return l2CacheSize; }
            set { l2CacheSize = value; }
        }

        /// <summary>
        /// GET/SET L3CahceSize
        /// </summary>
        public double L3CacheSize 
        {
            get { return l3CacheSize; }
            set { l3CacheSize = value; }
        }

        /// <summary>
        /// GET/SET ThreadCount
        /// </summary>
        public int ThreadCount 
        {
            get { return threadCount; }
            set { threadCount = value; }
        }

        /// <summary>
        /// GET/SET NumberOfCores
        /// </summary>
        public int NumberOfCores 
        {
            get { return numberOfCores; }
            set { numberOfCores = value; }
        }

        /// <summary>
        /// GET/SET NumberOfLogicalProcessors
        /// </summary>
        public int NumberOfLogicalProcessors 
        {
            get { return numberOfLogicalProcessors; }
            set { numberOfLogicalProcessors = value; }
        }

        /// <summary>
        /// GET/SET Name
        /// Name is the Processor Description
        /// </summary>
        public String Name 
        {
            get 
            { return name; }
            set { name = value; }
        }

        /// <summary>
        /// GET/SET CPU_BaseFrequency
        /// </summary>
        public double CPU_BaseFrequency 
        { 
            get 
            { return maxSpeed;}
            set { maxSpeed = value; }
        }

        /// <summary>
        /// An async Task that calculate the percentage of the CPU usage 
        /// </summary>
        /// <returns>The second Performance reader which conatin the correct value</returns>
        public async Task<double> getCurrentCpuUsage(int miliseconds=500)
        {
            cpuusage.NextValue();
            await Task.Delay(miliseconds);
            return cpuusage.NextValue();
        }

        /// <summary>
        /// An async Task which calculate the current Frequency of the CPU
        /// </summary>
        ///<returns>The Frequency base on the formula basefrequency * secondcounter/100</returns>
        public async Task<double> getCurrentCpuFrequency(int miliseconds=500)
        {
            cpufrequency.NextValue();
            await Task.Delay(miliseconds);
            return this.maxSpeed * cpufrequency.NextValue() / 100;
        }
    }
}
