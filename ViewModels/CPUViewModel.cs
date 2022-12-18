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

namespace Task_Manager.ViewModels
{
    public class CPUViewModel: ObservableObject
    {
        CPU mycpu;
        public CPUViewModel() 
        { 
            mycpu= new CPU();
        }
        public string Utilization { get; set; }
        public string Speed { get; set; }
        public string Version { get; set; }
        public bool IsRunning { get; set; }
        public bool IsPaused { get; set; }
        public bool IsStopped { get; set; }

        public ISeries[] CPUActivity { get; set; } =
        {
            new LineSeries<double>
            {
                Values = new double[] {8,9,5,1, 3, 5, 3, 4, 6 },
                // Set he Fill property to build an area series
                // by default the series has a fill color based on your app theme
                Fill = new SolidColorPaint(SKColors.CornflowerBlue),
                LineSmoothness = 0,
                Stroke = null,
                GeometryFill = null,
                GeometryStroke = null
            }
        };
        public Axis[] CPUXAxes { get; set; } =
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

        public Axis[] CPUYAxes { get; set; } =
        {
            new Axis
            {
                MinLimit = 0,
                MaxLimit = 20,
                ForceStepToMin = true,
                MinStep = 2,
                TextSize = 0,
                SeparatorsPaint = null
                
            }
        };

    }
}
