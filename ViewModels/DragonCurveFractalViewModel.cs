using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WPF_TestProject2.Classes;
using WPF_TestProject2.Commands;
using WPF_TestProject2.Models;
using WPF_TestProject2.Stores;

namespace WPF_TestProject2.ViewModels
{
    class DragonCurveFractalViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        private System.Windows.Media.Imaging.WriteableBitmap _fractalBmp;

        public DragonCurveFractalModel DragonCurveFractalModel;

        public DragonCurveFractalViewModel(NavigationStore navigationStore)
        {
            DragonCurveFractalModel = new DragonCurveFractalModel();

            _navigationStore = navigationStore;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            _fractalBmp = GetFractal();
            RenderTime = sw.ElapsedMilliseconds / 1000.0f;
        }

        private WriteableBitmap GetFractal()
        {
            int sWidth = 700, sHeight = 650;
            Bitmap bmp = new Bitmap(sWidth, sHeight);
            Point center = new Point(100, 200);

            FractalGenerator fGen = new FractalGenerator();
            FractalInitiator fInit = new FractalInitiator();

            fInit.AddVertex(new Point(50, 200));
            fInit.AddVertex(new Point(400, 200));

            fGen.AddNode(45);
            fGen.AddNode(-90);

            DragonCurveFracral dragonCurve = new DragonCurveFracral(fGen, fInit, RecursionsCount);
            dragonCurve.Run();
            for (int i = 0; i < dragonCurve.EdgeCount; ++i)
            {
                Tuple<Point, Point> edge = dragonCurve.GetEdge(i);
                Point start = new Point(edge.Item1.X + center.X, edge.Item1.Y + center.Y);
                Point end = new Point(edge.Item2.X + center.X, edge.Item2.Y + center.Y);
                if (GraphicsUtils.IsFit(start, sWidth, sHeight) && GraphicsUtils.IsFit(end, sWidth, sHeight))
                    GraphicsUtils.DrawLine(bmp, GraphicsUtils.GetPointsOnLine(start, end), System.Drawing.Color.Black);
            }

            return GraphicsUtils.ConvertToWriteableBitmap(bmp);
        }

        public System.Windows.Media.Imaging.WriteableBitmap FractalBmp
        {
            get { return _fractalBmp; }
            set 
            {
                _fractalBmp = value;
                OnPropertyChanged(nameof(FractalBmp));
            }
        }

        public FractalType SelectedFractalType
        {
            get { return DragonCurveFractalModel.SelectedFractalType; }
            set
            {
                DragonCurveFractalModel.SelectedFractalType = value;
                if (value == FractalType.KOCH_SNOWFLAKE)
                    this.NavigateKochSnowflakeFractalCommand.Execute(null);
                else if (value == FractalType.BARNSLEY_FERN)
                    this.NavigateBarnsleyFernFractalCommand.Execute(null);

                OnPropertyChanged(nameof(SelectedFractalType));
            }
        }

        public OrientationType SelectedOrientationType
        {
            get { return DragonCurveFractalModel.SelectedOrientationType; }
            set
            {
                DragonCurveFractalModel.SelectedOrientationType = value;
                FractalBmp = GraphicsUtils.RotateImage(FractalBmp, (float)DragonCurveFractalModel.SelectedOrientationType);
                OnPropertyChanged(nameof(SelectedOrientationType));
            }
        }

        public int RecursionsCount
        {
            get { return DragonCurveFractalModel.RecursionsCount; }
            set
            {
                DragonCurveFractalModel.RecursionsCount = value;
                Stopwatch sw = new Stopwatch();
                sw.Start();
                FractalBmp = GetFractal();
                RenderTime = sw.ElapsedMilliseconds / 1000.0f;
                OnPropertyChanged(nameof(RecursionsCount));
            }
        }

        public int Scale
        {
            get { return DragonCurveFractalModel.Scale; }
            set
            {
                DragonCurveFractalModel.Scale = value;
                OnPropertyChanged(nameof(Scale));
            }
        }

        public float RenderTime
        {
            get { return DragonCurveFractalModel.RenderTime; }
            set
            {
                DragonCurveFractalModel.RenderTime = value;
                OnPropertyChanged(nameof(RenderTime));
            }
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
        /// Handles navigation to KochFractal view
        /// </summary>
        private DelegateCommand _navigateKochSnowflakeFractalCommand;

        public ICommand NavigateKochSnowflakeFractalCommand
        {
            get
            {
                if (_navigateKochSnowflakeFractalCommand == null)
                {
                    Console.WriteLine("Reached here");
                    _navigateKochSnowflakeFractalCommand = new DelegateCommand(NavigateKochSnowflakeFractal);
                }
                return _navigateKochSnowflakeFractalCommand;
            }
        }

        public void NavigateKochSnowflakeFractal()
        {
            _navigationStore.CurrentViewModel = new KochSnowflakeFractalViewModel(_navigationStore);
        }

        /// <summary>
        /// navigation to barnsley fern fracral viewmodel
        /// </summary>
        private DelegateCommand _navigateBarnsleyFernFractalCommand;

        public ICommand NavigateBarnsleyFernFractalCommand
        {
            get
            {
                if (_navigateBarnsleyFernFractalCommand == null)
                {
                    Console.WriteLine("Reached here");
                    _navigateBarnsleyFernFractalCommand = new DelegateCommand(NavigateBarnsleyFernFractal);
                }
                return _navigateBarnsleyFernFractalCommand;
            }
        }

        public void NavigateBarnsleyFernFractal()
        {
            _navigationStore.CurrentViewModel = new BarnsleyFernFractalViewModel(_navigationStore);
        }

        /// <summary>
        /// Copies selected image to clipboard
        /// </summary>
        private DelegateCommand _copyImageToClipboardCommand;

        public ICommand CopyImageToClipboardCommand
        {
            get
            {
                if (_copyImageToClipboardCommand == null)
                {
                    _copyImageToClipboardCommand = new DelegateCommand(() => CopyImageToClipboard(FractalBmp));
                }
                return _copyImageToClipboardCommand;
            }
        }

        public void CopyImageToClipboard(WriteableBitmap bmp)
        {
            System.Windows.Clipboard.SetImage(bmp);
        }

        /// <summary>
        /// Resets viewmodel state
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
            _navigationStore.CurrentViewModel = new DragonCurveFractalViewModel(_navigationStore);
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
            _navigationStore.CurrentViewModel = new FractalsInfoViewModel(_navigationStore, this);
        }

        /// <summary>
        /// Renders IFS version of current fractal
        /// </summary>
        private DelegateCommand _ifsCommand;

        public ICommand IFSCommand
        {
            get
            {
                if (_ifsCommand == null)
                {
                    _ifsCommand = new DelegateCommand(GetFractalIFS);
                }
                return _ifsCommand;
            }
        }

        public void GetFractalIFS()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            int sWidth = 700, sHeight = 650;
            Bitmap bmp = new Bitmap(sWidth, sHeight);
            Point center = new Point(100, 200);

            FractalGenerator fGen = new FractalGenerator();
            FractalInitiator fInit = new FractalInitiator();

            fInit.AddVertex(new Point(50, 200));
            fInit.AddVertex(new Point(400, 200));

            fGen.AddNode(45);
            fGen.AddNode(-90);

            DragonCurveFracral dragonCurve = new DragonCurveFracral(fGen, fInit, RecursionsCount);
            dragonCurve.RunIFS();
            for (int i = 0; i < dragonCurve.EdgeCount; ++i)
            {
                Tuple<Point, Point> edge = dragonCurve.GetEdge(i);
                Point start = new Point(edge.Item1.X + center.X, edge.Item1.Y + center.Y);
                Point end = new Point(edge.Item2.X + center.X, edge.Item2.Y + center.Y);
                if (GraphicsUtils.IsFit(start, sWidth, sHeight) && GraphicsUtils.IsFit(end, sWidth, sHeight))
                    GraphicsUtils.DrawLine(bmp, GraphicsUtils.GetPointsOnLine(start, end), System.Drawing.Color.Black);
            }

            FractalBmp = GraphicsUtils.ConvertToWriteableBitmap(bmp); ;
            RenderTime = sw.ElapsedMilliseconds / 1000.0f; 
        }
        #endregion
    }
}
