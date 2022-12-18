using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore.SkiaSharpView.Painting.Effects;

namespace Task_Manager.ViewModels
{
    public class PerformanceViewModel
    {
               
        public PerformanceViewModel() 
        { 
           CPUviewModel = new CPUViewModel();
        }
        public CPUViewModel CPUviewModel { get; }
        public ObservableValue ObservableValue { get; set; }

        public ISeries[] Series { get; set; } =
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
