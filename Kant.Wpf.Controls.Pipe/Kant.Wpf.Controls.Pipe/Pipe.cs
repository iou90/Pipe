using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kant.Wpf.Controls
{
    [TemplatePart(Name = "PartTailStuffing", Type = typeof(Path))]
    public class Pipe : Control
    {
        #region constructor

        static Pipe()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Pipe), new FrameworkPropertyMetadata(typeof(Pipe)));
        }

        #endregion

        #region Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var path = GetTemplateChild("PartTailStuffing") as Path;

            if(path == null)
            {
                throw new MissingMemberException("can not find template child PartTailStuffing.");
            }

            tailStaffing = path;
        }

        #endregion

        #region Fields & Properties

        public double RadiusX
        {
            get { return (double)GetValue(RadiusXProperty); }
            set { SetValue(RadiusXProperty, value); }
        }

        public static readonly DependencyProperty RadiusXProperty = DependencyProperty.Register("RadiusX", typeof(double), typeof(Pipe), new PropertyMetadata(6));

        public double RadiusY
        {
            get { return (double)GetValue(RadiusYProperty); }
            set { SetValue(RadiusYProperty, value); }
        }

        public static readonly DependencyProperty RadiusYProperty = DependencyProperty.Register("RadiusY", typeof(double), typeof(Pipe), new PropertyMetadata(10));

        public Brush Color
        {
            get { return (Brush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(Brush), typeof(Pipe), new PropertyMetadata(new SolidColorBrush(Colors.DarkGray)));

        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register("Stroke", typeof(Brush), typeof(Pipe), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register("StrokeThickness", typeof(double), typeof(Pipe), new PropertyMetadata(1));

        public Brush TailColor
        {
            get { return (Brush)GetValue(TailColorProperty); }
            set { SetValue(TailColorProperty, value); }
        }

        public static readonly DependencyProperty TailColorProperty = DependencyProperty.Register("TailColor", typeof(Brush), typeof(Pipe), new PropertyMetadata(new SolidColorBrush(Colors.DarkGray) { Opacity = 0.05 }));

        public double TailStrokeThickness
        {
            get { return (double)GetValue(TailStrokeThicknessProperty); }
            set { SetValue(TailStrokeThicknessProperty, value); }
        }

        public static readonly DependencyProperty TailStrokeThicknessProperty = DependencyProperty.Register("TailStrokeThickness", typeof(double), typeof(Pipe), new PropertyMetadata(3));

        protected double pipeTailWidth;
        public double PipeTailWidth
        {
            get
            {
                return pipeTailWidth;
            }
        }

        private Path tailStaffing;

        #endregion
    }
}
