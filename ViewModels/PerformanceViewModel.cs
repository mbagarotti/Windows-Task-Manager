using LiveChartsCore;
using LiveChartsCore.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager.ViewModels
{
    public class PerformanceViewModel
    {
               
        public PerformanceViewModel() 
        { 
           CPUviewModel = new CPUViewModel();
        }
        public CPUViewModel CPUviewModel { get; set; }
        public ObservableValue ObservableValue { get; set; }
    }
}
