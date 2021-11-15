using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using WPF_TestProject2.Commands;
using WPF_TestProject2.Stores;
using System;
using WPF_TestProject2.Classes;
using System.Collections.ObjectModel;

namespace WPF_TestProject2.ViewModels
{
    class AffineTransformationsViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private int _planeWidth = 500;
        private int _planeHeight = 350; 
        private double _step = 20;
        public double Step 
        { get 
            { return _step; }
            set 
            { _step = value;
                UpdateCoordinateSystem();
            }
        }
        private Canvas _canvas;
        public Canvas Canvas
        {
            get { return _canvas; }
            set
            {
                _canvas = value;
                OnPropertyChanged(nameof(Canvas));
            }
        }
        public ObservableCollection<Point> Parallelogram { get; set; }
        public ObservableCollection<double> Line { get; set; }

        public AffineTransformationsViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            Canvas = new Canvas();
            Parallelogram = new ObservableCollection<Point>() 
            { 
                new Point(-4, -1), 
                new Point(-2, 3), 
                new Point(4, 5), 
                new Point(2, 1) 
            };
            Line = new ObservableCollection<double>() { 0, 0 };

            UpdateCoordinateSystem();
        }

        //draws parallelogram by 4 points
        private void DrawParalellogram()
        {
            int xMid = _planeWidth / 2, yMid = _planeHeight / 2;
            Point[] convertedParallelogram = new Point[4];

            for (int i = 0; i < Parallelogram.Count; i++)
            {
                convertedParallelogram[i] = ConvertPoint(_step, xMid, yMid, Parallelogram[i]);
            }
            PathFigure parallelogram = new PathFigure
            {
                StartPoint = convertedParallelogram[0]
            };

            parallelogram.Segments.Add(new PolyLineSegment(convertedParallelogram, true));
            parallelogram.IsClosed = true;
            PathGeometry path = new PathGeometry();
            path.Figures.Add(parallelogram);

            Path paral = new Path();
            paral.StrokeThickness = 1;
            paral.Stroke = Brushes.Black;
            paral.Data = path;

            Canvas.Children.Add(paral);
            //OnPropertyChanged(nameof(Canvas));
        }

        private void DrawLine()
        {
            int xMid = _planeWidth / 2, yMid = _planeHeight / 2;
            Point[] convertedLine = new Point[2];

            convertedLine[0] = new Point(-12, LineFunc(Line[0], Line[1], -12));
            convertedLine[1] = new Point(12, LineFunc(Line[0], Line[1], 12));

            for (int i = 0; i < Line.Count; i++)
            {
                convertedLine[i] = ConvertPoint(_step, xMid, yMid, convertedLine[i]);
            }

            

            Canvas.Children.Add(GetLine(convertedLine[0], convertedLine[1]));
            OnPropertyChanged(nameof(Canvas));
        }

        private void DrawCoordinateSystem()
        {
            Canvas canvasTemp = new Canvas();
            int xMin = 0, xMax = _planeWidth, xMid = (xMax - xMin) / 2;
            int yMin = 0, yMax = _planeHeight, yMid = (yMax - yMin) / 2;

            //draw x axis
            GeometryGroup xAxis = new GeometryGroup();
            xAxis.Children.Add(new LineGeometry(new Point(xMin, yMid), new Point(_planeWidth, yMid)));

            //positive x axis ticks
            int counter = 1;
            for (double x = xMid + _step; x < _planeWidth; x += _step)
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
            Canvas.SetLeft(xLabel, _planeWidth - 10);
            canvasTemp.Children.Add(xLabel);

            //negative x axis ticks
            counter = -1;
            for (double x = xMid - _step; x > xMin; x -= _step)
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
            yAxis.Children.Add(new LineGeometry(new Point(xMid, yMin), new Point(xMid, _planeHeight)));

            //positive y axis ticks, from mid to upper edge
            counter = 1;
            for (double y = yMid - _step; y > yMin; y -= _step)
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
            for (double y = yMid + _step; y < _planeHeight; y += _step)
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

            Canvas = canvasTemp;
        }

        private void UpdateCoordinateSystem()
        {
            DrawCoordinateSystem();
            DrawParalellogram();
        }
        //converts point according to current coordinate system
        private Point ConvertPoint(double step, int xMid, int yMid, Point point)
        {
            double x = point.X * step + xMid;
            double y = -1 * point.Y * step + yMid;
            return new Point(x, y);
        }

        

        private bool ParallelogramExists(Point p1, Point p2, Point p3)
        {
            if (GraphicsUtils.IsPointOnLine(p1, p2, p3) ||
                GraphicsUtils.IsPointOnLine(p1, p3, p2) ||
                GraphicsUtils.IsPointOnLine(p2, p3, p1))
                return false;
            else
                return true;
        }

        private Point FindLastEdge(Point p1, Point p2, Point p3)
        {
            Point p4 = new Point(0, 0);
            Point m = new Point(0, 0);

            m.Y = p3.Y - p2.Y;
            m.X = p3.X - p2.X;

            p4.X = p1.X + m.X;
            p4.Y = p1.Y + m.Y;

            return p4;
        }

        private Line GetLine(Point s, Point e)
        {
            Line line = new Line();

            line.Stroke = System.Windows.Media.Brushes.Black;

            line.X1 = s.X;
            line.X2 = e.X;
            line.Y1 = s.Y;
            line.Y2 = e.Y;

            line.StrokeThickness = 1;

            return line;
        }

        

        private double LineFunc(double a, double b, double x)
        {
            return a * x + b;
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
            _navigationStore.CurrentViewModel = new TransformationsInfoViewModel(_navigationStore, this);
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
            if (ParallelogramExists(Parallelogram[0], Parallelogram[1], Parallelogram[2]))
            {
                Parallelogram[3] = 
                    FindLastEdge(Parallelogram[0], Parallelogram[1], Parallelogram[2]);

                DrawCoordinateSystem();
                DrawParalellogram();
                DrawLine();
            }
            else
            {
                MessageBox.Show("Wrong parallelogram edges");
            }
        }
        #endregion
    }
}
