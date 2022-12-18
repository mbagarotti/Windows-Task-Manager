using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SkiaSharp;
using Task_Manager.Models;
using LiveChartsCore.Defaults;
using System.Collections.ObjectModel;
using System.Threading;

namespace Task_Manager.ViewModels
{
    public class CPUViewModel: ObservableObject
    {
        CPU mycpu;
        Thread workerThread;
        Thread workerThread2;
        bool pin;
        int count;
        private static ObservableCollection<ObservableValue> _observableValues;
        private String utilization;
        private String frequency;
        public CPUViewModel()
        {
            count = 0;
            mycpu = new CPU();
            pin = true;
            _observableValues= new ObservableCollection<ObservableValue>();
            CPUActivity = new ObservableCollection<ISeries>
            {
                new LineSeries<ObservableValue>
                {
                    Values = _observableValues,
                    // Set he Fill property to build an area series
                    // by default the series has a fill color based on your app theme
                    Fill = new SolidColorPaint(new SKColor(43,141,163)),
                    LineSmoothness = 0,
                    Stroke = null,
                    GeometryFill = null,
                    GeometryStroke = null
                }
            };
            workerThread = new Thread(new ThreadStart(Thread_Utilization_value));
            workerThread2 = new Thread(new ThreadStart(Thread_Frequency_Value));
            workerThread.IsBackground = true;
            workerThread2.IsBackground = true;
            workerThread.Start();
            workerThread2.Start();

        }
        public void Thread_Utilization_value() 
        {
            while (pin)
            {
                var q = Utilization(1000);
                _observableValues.Add(new (q/10));
                Utilization_value = Convert.ToString((int)q);
                if (count == 61)
                {
                    _observableValues.Remove(_observableValues[0]);
                    count--;
                }
                count++;
            }
                                
        }
        public void Thread_Frequency_Value()
        {
            while (pin)
            {
                var j = Speed(1000);
                Frequency_Value = j.ToString("f2");
            }

        }
        public String Utilization_value 
        { 
            get => utilization + "%";
            set=> SetProperty(ref utilization, value);
        }
        public String Frequency_Value 
        {
            get => frequency + " GHz";
            set => SetProperty(ref frequency, value);

        }
        public String Name { get { return mycpu.Name; } }
        public double Utilization(int miliseconds=500) { return this.mycpu.getCurrentCpuUsage(miliseconds); }
        public double Speed(int miliseconds=500) { return this.mycpu.getCurrentCpuFrequency(miliseconds); }
        public string Version { get; set; }
        
        public ObservableCollection<ISeries> CPUActivity { get; set; }
        
        public Axis[] CPUXAxes { get; set; } =
        {
            new Axis
            {
                MinLimit = 0,
                MaxLimit = 60,
                ForceStepToMin = true,
                MinStep = 5,
                TextSize = 0,
                SeparatorsPaint = null
                
            }
        };

        public Axis[] CPUYAxes { get; set; } =
        {
            new Axis
            {
                MinLimit = 0,
                MaxLimit = 10,
                ForceStepToMin = true,
                MinStep = 0.5,
                TextSize = 0,
                SeparatorsPaint = null
                
            }
        };
        public Axis[] XAxes { get; set; } =
        {
            new Axis
            {
                MinLimit = 0,
                MaxLimit = 60,
                ForceStepToMin = true,
                MinStep = 5,
                TextSize = 0,
                //SeparatorsPaint = null
                SeparatorsPaint = new SolidColorPaint
                {
                    Color = SKColors.White,
                    StrokeThickness = 0.1f
                }
            }
        };

        public Axis[] YAxes { get; set; } =
        {
            new Axis
            {
                MinLimit = 0,
                MaxLimit = 10,
                ForceStepToMin = true,
                MinStep = 1,
                TextSize = 0,
                //SeparatorsPaint = null
                SeparatorsPaint = new SolidColorPaint
                {
                    Color = SKColors.White,
                    StrokeThickness = 0.1f
                }
            }
        };

    }
}
