using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_TestProject2.Commands;
using WPF_TestProject2.Stores;

namespace WPF_TestProject2.ViewModels
{
    class AffineTransformationsViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
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

        public AffineTransformationsViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            canvas = new Canvas();
            DrawCoordinateSystem();

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
            xAxis.Children.Add(new LineGeometry(new System.Windows.Point(xMin, yMid), new System.Windows.Point(width, yMid)));

            //positive x axis ticks
            int counter = 1;
            for( double x = xMid + step; x < width; x += step)
            {
                xAxis.Children.Add(new LineGeometry(
                    new System.Windows.Point(x, yMid - 5),
                    new System.Windows.Point(x, yMid + 5)));
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
                    new System.Windows.Point(x, yMid - 5),
                    new System.Windows.Point(x, yMid + 5)));
                TextBlock num = new TextBlock();
                num.Text = counter--.ToString();
                num.FontSize = 8;
                Canvas.SetTop(num, yMid + 10);
                Canvas.SetLeft(num, x);
                canvasTemp.Children.Add(num);
            }

            Path xAxisPath = new Path();
            xAxisPath.StrokeThickness = 1;
            xAxisPath.Stroke = System.Windows.Media.Brushes.Black;
            xAxisPath.Data = xAxis;
            canvasTemp.Children.Add(xAxisPath);

            //draw y axis
            counter = 1;
            GeometryGroup yAxis = new GeometryGroup();
            yAxis.Children.Add(new LineGeometry(new System.Windows.Point(xMid, yMin), new System.Windows.Point(xMid, height)));

            //positive y axis ticks, from mid to upper edge
            counter = 1;
            for (double y = yMid - step; y > yMin; y -= step)
            {
                xAxis.Children.Add(new LineGeometry(
                    new System.Windows.Point(xMid - 5, y),
                    new System.Windows.Point(xMid + 5, y)));
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
            yAxisPath.Stroke = System.Windows.Media.Brushes.Black;
            yAxisPath.Data = yAxis;
            canvasTemp.Children.Add(yAxisPath);

            //negative y axis ticks, from mid to lower edge
            counter = -1;
            for (double y = yMid + step; y < height; y += step)
            {
                xAxis.Children.Add(new LineGeometry(
                    new System.Windows.Point(xMid - 5, y),
                    new System.Windows.Point(xMid + 5, y)));
                TextBlock num = new TextBlock();
                num.Text = counter--.ToString();
                num.FontSize = 8;
                Canvas.SetTop(num, y - 5);
                Canvas.SetLeft(num, xMid + 10);
                canvasTemp.Children.Add(num);
            }

           
            //int width = 500, height = 300;
            //const double margin = 10;
            //double xmin = margin;   
            //double xmax = width - margin;
            //double ymin = margin;
            //double ymax = height;
            //double xMid = (xmax - xmin) / 2;
            //double yMid = (ymax - ymin) / 2;
            //const double step = 30;
            //GeometryGroup xaxis_geom = new GeometryGroup();
            //xaxis_geom.Children.Add(new LineGeometry(
            //    new System.Windows.Point(0, yMid), new System.Windows.Point(width, yMid)));
            //int counter = (int)((width - step * 2)/(step) * - 1)/2;
            //for (double x = xmin + step; x <= width - step; x += step)
            //{
            //    xaxis_geom.Children.Add(new LineGeometry(
            //        new System.Windows.Point(x, yMid - margin / 2),
            //        new System.Windows.Point(x, yMid + margin / 2)));
            //    TextBlock num = new TextBlock();
            //    num.Text = counter++.ToString();
            //    num.FontSize = 8;
            //    Canvas.SetTop(num, yMid + margin);
            //    Canvas.SetLeft(num, x);
            //    can.Children.Add(num);
            //}

            //Path xaxis_path = new Path();
            //xaxis_path.StrokeThickness = 1;
            //xaxis_path.Stroke = System.Windows.Media.Brushes.Black;
            //xaxis_path.Data = xaxis_geom;
            //can.Children.Add(xaxis_path);

            //GeometryGroup yaxis_geom = new GeometryGroup();
            //yaxis_geom.Children.Add(new LineGeometry(
            //    new System.Windows.Point(xMid, 0), new System.Windows.Point(xMid, height)));
            //counter = (int)((height - step * 2 - ymin - margin * 2) / (step + ymin) * -1);
            //for (double y = step; y <= height - step; y += step)
            //{
            //    yaxis_geom.Children.Add(new LineGeometry(
            //        new System.Windows.Point(xMid - margin / 2, y),
            //        new System.Windows.Point(xMid + margin / 2, y)));
            //    TextBlock num = new TextBlock();
            //    num.Text = counter++.ToString();
            //    num.FontSize = 8;
            //    Canvas.SetTop(num, y );
            //    Canvas.SetLeft(num, xMid + margin );
            //    can.Children.Add(num);
            //}

            //Path yaxis_path = new Path();
            //yaxis_path.StrokeThickness = 1;
            //yaxis_path.Stroke = System.Windows.Media.Brushes.Black;
            //yaxis_path.Data = yaxis_geom;
            //can.Children.Add(yaxis_path);

            //TextBlock xText = new TextBlock();
            //xText.Text = "X";
            //Canvas.SetLeft(xText, xmax);
            //Canvas.SetTop(xText, yMid);
            //can.Children.Add(xText);

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
        /// copy Fractal image to clipboard command
        /// <summary>
        private DelegateCommand _copyImageToClipboardCommand;

        public ICommand CopyImageToClipboardCommand
        {
            get
            {
                if (_copyImageToClipboardCommand == null)
                {
                    _copyImageToClipboardCommand = new DelegateCommand(() => CopyImageToClipboard(_transformationsBmp));
                }
                return _copyImageToClipboardCommand;
            }
        }

        public void CopyImageToClipboard(WriteableBitmap bmp)
        {
            System.Windows.Clipboard.SetImage(bmp);
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
        #endregion
    }
}
