using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
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
    public class Pipe : Control, INotifyPropertyChanged
    {
        #region constructor

        static Pipe()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Pipe), new FrameworkPropertyMetadata(typeof(Pipe)));
        }

        public Pipe()
        {
            SizeChanged += (s, e) =>
            {
                UpdateLayout();
            };
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

        public static void OnCurvenessSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var value = (double)e.NewValue;

            if(!(value > 0 && value <= 1))
            {
                ((Pipe)o).UpdateLayout();
            }
            else
            {
                throw new ArgumentOutOfRangeException("curveness has to greater than 0 and less than or equal 1");
            }
        }

        private void UpdateLayout()
        {
            if(tailStaffing == null)
            {
                return;
            }

            RadiusY = ActualHeight / 2;
            RadiusX = RadiusY * Curveness;
            PipeTailWidth = RadiusX * 2;
            var pathLength = (RadiusX / 6) * 7;
            tailStaffing.Margin = new Thickness(-pathLength, 0, 0, 0);
            tailStaffing.Width = pathLength;
            var tempCenterX = (RadiusX / 3) * 4;
            var tempRectangleWidth = RadiusX * 3;
            var ellipseGeometry = new EllipseGeometry(new Point(tempCenterX, RadiusY), RadiusX, RadiusY);
            var rectangleGeometry = new RectangleGeometry(new Rect(0, 0, tempRectangleWidth, ActualHeight));

            if (tailStaffing.Data == null)
            {
                var combinedGeometry = new CombinedGeometry(GeometryCombineMode.Xor, ellipseGeometry, rectangleGeometry);
                tailStaffing.Data = combinedGeometry;
            }
            else
            {
                var combinedGeometry = (CombinedGeometry)tailStaffing.Data;
                combinedGeometry.Geometry1 = ellipseGeometry;
                combinedGeometry.Geometry2 = rectangleGeometry;
            }
        }

        #endregion

        #region Fields & Properties

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

        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register("StrokeThickness", typeof(double), typeof(Pipe), new PropertyMetadata(1.0));

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

        public static readonly DependencyProperty TailStrokeThicknessProperty = DependencyProperty.Register("TailStrokeThickness", typeof(double), typeof(Pipe), new PropertyMetadata(3.0));

        public double Curveness
        {
            get { return (double)GetValue(CurvenessProperty); }
            set { SetValue(CurvenessProperty, value); }
        }

        public static readonly DependencyProperty CurvenessProperty = DependencyProperty.Register("Curveness", typeof(double), typeof(Pipe), new PropertyMetadata(0.6, OnCurvenessSourceChanged));

        protected double pipeTailWidth;
        public double PipeTailWidth
        {
            get
            {
                return pipeTailWidth;
            }
            private set
            {
                if(value > 0 && value != pipeTailWidth)
                {
                    pipeTailWidth = value;
                    RaisePropertyChanged(() => PipeTailWidth);
                }
            }
        }

        protected double radiusX;
        public double RadiusX
        {
            get
            {
                return radiusX;
            }
            private set
            {
                if (value > 0 && value != radiusX)
                {
                    radiusX = value;
                    RaisePropertyChanged(() => RadiusX);
                }
            }
        }

        protected double radiusY;
        public double RadiusY
        {
            get
            {
                return radiusY;
            }
            private set
            {
                if (value > 0 && value != radiusY)
                {
                    radiusY = value;
                    RaisePropertyChanged(() => RadiusY);
                }
            }
        }

        private Path tailStaffing;

        #endregion

        #region INotifyPropertyChanged

        private void RaisePropertyChanged<T>(Expression<Func<T>> action)
        {
            var propertyName = GetPropertyName(action);
            RaisePropertyChanged(propertyName);
        }

        private static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            var propertyName = expression.Member.Name;
            return propertyName;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
