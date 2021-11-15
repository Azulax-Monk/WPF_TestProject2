using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_TestProject2.Commands;
using WPF_TestProject2.Stores;
using System;
using WPF_TestProject2.Classes;
using System.Windows.Shapes;
using WPF_TestProject2.Commands;
using WPF_TestProject2.Stores;
using System;


namespace WPF_TestProject2.ViewModels
{
    class AffineTransformationsViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private Point[] _parallelogram;
        private Canvas _canvas;
        public Canvas canvas
        {
            get { return _canvas; }
            set
            {
                _canvas = value;
                OnPropertyChanged(nameof(canvas));
            }
        }

        private System.Windows.Media.Imaging.WriteableBitmap _transformationsBmp;

        public Point P2 { get; set; }

        public Point P3 { get; set; }

        public Point P4 { get; set; }

        public double A { get; set; }

        public double B { get; set; }

        public AffineTransformationsViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            canvas = new Canvas();
            _parallelogram = new Point[] { new Point(-4, -1), new Point(-2, 3), new Point(4, 5), new Point(2, 1) };
            DrawCoordinateSystem();

        }
        //draws parallelogram by 4 points
        private PathGeometry DrawParalellogram(double step, int xMid, int yMid)
        {
            Point[] convertedParallelogram = new Point[4];
            for (int i = 0; i < _parallelogram.Length; i++)
            {
                convertedParallelogram[i] = ConvertPoint(step, xMid, yMid, _parallelogram[i]);
            }
            PathFigure parallelogram = new PathFigure
            {
                StartPoint = convertedParallelogram[0]
            };
            parallelogram.Segments.Add(new PolyLineSegment(convertedParallelogram, true));
            parallelogram.IsClosed = true;
            PathGeometry path = new PathGeometry();
            path.Figures.Add(parallelogram);
            return path;
        }
        //converts point according to current coordinate system
        private Point ConvertPoint(double step, int xMid, int yMid, Point point)
        {
            double x = point.X * step + xMid;
            double y = -1 * point.Y * step + yMid;
            return new Point(x, y);
        }

        private void DrawCoordinateSystem()
        {
            Canvas canvasTemp = new Canvas();
            double step = 20;
            int width = 500, height = 350;
            int xMin = 0, xMax = width, xMid = (xMax - xMin) / 2;
            int yMin = 0, yMax = height, yMid = (yMax - yMin) / 2;

            //draw x axis
            GeometryGroup xAxis = new GeometryGroup();
            xAxis.Children.Add(new LineGeometry(new Point(xMin, yMid), new Point(width, yMid)));

            //positive x axis ticks
            int counter = 1;
            for (double x = xMid + step; x < width; x += step)
            {
                xAxis.Children.Add(new LineGeometry(
                    new Point(x, yMid - 5),
                    new Point(x, yMid + 5)));
                TextBlock num = new TextBlock();
                num.Text = counter++.ToString();
                num.FontSize = 8;
                Canvas.SetTop(num, yMid + 10);
                Canvas.SetLeft(num, x);
                canvasTemp.Children.Add(num);
            }
            TextBlock xLabel = new TextBlock();
            xLabel.Text = "x";
            Canvas.SetTop(xLabel, yMid - 20);
            Canvas.SetLeft(xLabel, width - 10);
            canvasTemp.Children.Add(xLabel);

            //negative x axis ticks
            counter = -1;
            for (double x = xMid - step; x > xMin; x -= step)
            {
                xAxis.Children.Add(new LineGeometry(
                    new Point(x, yMid - 5),
                    new Point(x, yMid + 5)));
                TextBlock num = new TextBlock();
                num.Text = counter--.ToString();
                num.FontSize = 8;
                Canvas.SetTop(num, yMid + 10);
                Canvas.SetLeft(num, x);
                canvasTemp.Children.Add(num);
            }

            Path xAxisPath = new Path();
            xAxisPath.StrokeThickness = 1;
            xAxisPath.Stroke = Brushes.Black;
            xAxisPath.Data = xAxis;
            canvasTemp.Children.Add(xAxisPath);

            //draw y axis
            GeometryGroup yAxis = new GeometryGroup();
            yAxis.Children.Add(new LineGeometry(new Point(xMid, yMin), new Point(xMid, height)));

            //positive y axis ticks, from mid to upper edge
            counter = 1;
            for (double y = yMid - step; y > yMin; y -= step)
            {
                xAxis.Children.Add(new LineGeometry(
                    new Point(xMid - 5, y),
                    new Point(xMid + 5, y)));
                TextBlock num = new TextBlock();
                num.Text = counter++.ToString();
                num.FontSize = 8;
                Canvas.SetTop(num, y);
                Canvas.SetLeft(num, xMid + 10);
                canvasTemp.Children.Add(num);
            }
            TextBlock yLabel = new TextBlock();
            yLabel.Text = "y";
            Canvas.SetTop(yLabel, yMin);
            Canvas.SetLeft(yLabel, xMid - 10);
            canvasTemp.Children.Add(yLabel);
            Path yAxisPath = new Path();
            yAxisPath.StrokeThickness = 1;
            yAxisPath.Stroke = Brushes.Black;
            yAxisPath.Data = yAxis;
            canvasTemp.Children.Add(yAxisPath);

            //negative y axis ticks, from mid to lower edge
            counter = -1;
            for (double y = yMid + step; y < height; y += step)
            {
                xAxis.Children.Add(new LineGeometry(
                    new Point(xMid - 5, y),
                    new Point(xMid + 5, y)));
                TextBlock num = new TextBlock();
                num.Text = counter--.ToString();
                num.FontSize = 8;
                Canvas.SetTop(num, y - 5);
                Canvas.SetLeft(num, xMid + 10);
                canvasTemp.Children.Add(num);
            }
            Path paral = new Path();
            paral.StrokeThickness = 1;
            paral.Stroke = Brushes.Black;
            paral.Data = DrawParalellogram(step, xMid, yMid);
            canvasTemp.Children.Add(paral);
            canvas = canvasTemp;

        }

        #region Commands

        /// <summary>
        /// Handles navigation to Menu view
        /// </summary>
        private DelegateCommand _navigateMenuCommand;

        public ICommand NavigateMenuCommand
        {
            get
            {
                if (_navigateMenuCommand == null)
                {
                    Console.WriteLine("Reached here");
                    _navigateMenuCommand = new DelegateCommand(NavigateMenu);
                }
                return _navigateMenuCommand;
            }
        }

        public void NavigateMenu()
        {
            _navigationStore.CurrentViewModel = new MenuViewModel(_navigationStore);
        }

        /// <summary>
        /// Handles navigation to Info view
        /// </summary>
        private DelegateCommand _navigateInfoPageCommand;
        public ICommand NavigateInfoPageCommand
        {
            get
            {
                if (_navigateInfoPageCommand == null)
                {
                    Console.WriteLine("Reached here");
                    _navigateInfoPageCommand = new DelegateCommand(NavigateInfoPage);
                }
                return _navigateInfoPageCommand;
            }
        }

        public void NavigateInfoPage()
        {
            _navigationStore.CurrentViewModel = new InfoViewModel(_navigationStore, this);
        }


        /// <summary>
        /// reset state command
        /// </summary>
        private DelegateCommand _resetCommand;

        public ICommand ResetCommand
        {
            get
            {
                if (_resetCommand == null)
                {
                    _resetCommand = new DelegateCommand(Reset);
                }
                return _resetCommand;
            }
        }

        public void Reset()
        {
            _navigationStore.CurrentViewModel = new AffineTransformationsViewModel(_navigationStore);
        }

        /// <summary>
        /// Apply paralelogram settings and draw it
        /// </summary>
        private DelegateCommand _applyCommand;

        public ICommand ApplyCommand
        {
            get
            {
                if (_applyCommand == null)
                {
                    _applyCommand = new DelegateCommand(Apply);
                }
                return _applyCommand;
            }
        }

        public void Apply()
        {
            if (ParallelogramExists(P1, P2, P3))
            {
                P4 = FindLastEdge(P1, P2, P3);
            }
        }

        private bool ParallelogramExists(Point p1, Point p2, Point p3)
        {
            if (GraphicsUtils.IsPointOnLine(p1, p2, p3) ||
                GraphicsUtils.IsPointOnLine(p1, p3, p2) ||
                GraphicsUtils.IsPointOnLine(p1, p3, p1))
                return false;
            else
                return true;
        }

        private Point FindLastEdge(Point p1, Point p2, Point p3)
        {
            Point p4 = new Point(0, 0);

            if(p2.X > p1.X && p3.X > p1.X)
            {
                p4.X = p2.X + p3.X;
            }
            else if (p2.X < p1.X && p3.X > p1.X)
            {
                p4.X = p3.X - (p1.X - p2.X);
            }
            else if (p2.X > p1.X && p3.X < p1.X)
            {
                p4.X = p2.X - (p1.X - p3.X);
            }
            else if (p2.X < p1.X && p3.X < p1.X)
            {

            }

            return p4;
        }
        #endregion
    }
}
