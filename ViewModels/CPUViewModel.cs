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
        Task taskworkerThread3;
        Task tasktaskworkerThread4;
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
            Task.Run(()=>Thread_Utilization_value());
            
        }
        public async Task Thread_Utilization_value()
        {
            while (pin)
            {
                var a = new Task<double>[] { Utilization(), Speed() };
                var result = await Task.WhenAll(a);
                var q = result[0];
                var j = result[1];
                _observableValues.Add(new(q / 10));
                Utilization_value = Convert.ToString((int)q);
                Frequency_Value = j.ToString("f2");
                if (count == 61)
                {
                    _observableValues.Remove(_observableValues[0]);
                    count--;
                }
                count++;
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
        public Task<double> Utilization(int miliseconds = 1000)
        {
            return this.mycpu.getCurrentCpuUsage(miliseconds);
        }
        public Task<double> Speed(int miliseconds = 1000) { return this.mycpu.getCurrentCpuFrequency(miliseconds); }
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
