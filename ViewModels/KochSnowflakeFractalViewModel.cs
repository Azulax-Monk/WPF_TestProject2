using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPF_TestProject2.Classes;
using WPF_TestProject2.Commands;
using WPF_TestProject2.Models;
using WPF_TestProject2.Stores;

namespace WPF_TestProject2.ViewModels
{
    class KochSnowflakeFractalViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        private System.Windows.Media.Imaging.WriteableBitmap _fractalBmp;

        public KochSnowflakeFractalModel KochSnowflakeFractalModel;

        public KochSnowflakeFractalViewModel(NavigationStore navigationStore)
        {
            KochSnowflakeFractalModel = new KochSnowflakeFractalModel();

            _navigationStore = navigationStore;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            _fractalBmp = GetFractal();
            RenderTime = sw.ElapsedMilliseconds / 1000.0f;
        }

        public WriteableBitmap GetFractal()
        {
            int sWidth = 1280, sHeight = 900;
            Bitmap bmp = new Bitmap(sWidth, sHeight);
            Point center = new Point(250, 350);

            FractalGenerator fGen = new FractalGenerator();
            FractalInitiator fInit = new FractalInitiator();

            fInit.AddVertex(new Point(50, 75));
            fInit.AddVertex(new Point(175, -225));
            fInit.AddVertex(new Point(325, 75));

            fGen.AddNode(0);
            fGen.AddNode(60); //-60
            fGen.AddNode(-120); //120
            fGen.AddNode(60); //-60

            KochSnowflakeFractal kochSnowflake = new KochSnowflakeFractal(fGen, fInit, IterationsCount);
            kochSnowflake.Run();

            for (int i = 0; i < kochSnowflake.EdgeCount; ++i)
            {
                Tuple<Point, Point> edge = kochSnowflake.GetEdge(i);
                Point start = new Point(edge.Item1.X + center.X, edge.Item1.Y + center.Y);
                Point end = new Point(edge.Item2.X + center.X, edge.Item2.Y + center.Y);
                if (GraphicsUtils.IsFit(start, sWidth, sHeight) && GraphicsUtils.IsFit(end, sWidth, sHeight))
                    GraphicsUtils.DrawLine(bmp, start, end, System.Drawing.Color.Black);
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
            get { return KochSnowflakeFractalModel.SelectedFractalType; }
            set
            {
                KochSnowflakeFractalModel.SelectedFractalType = value;
                if (value == FractalType.DRAGON_CURVE)
                    this.NavigateDragonCurveFractalCommand.Execute(null);
                else if (value == FractalType.BARNSLEY_FERN)
                    this.NavigateBarnsleyFernFractalCommand.Execute(null);
                    
                OnPropertyChanged(nameof(SelectedFractalType));
            }
        }

        public OrientationType SelectedOrientationType
        {
            get { return KochSnowflakeFractalModel.SelectedOrientationType; }
            set
            {
                KochSnowflakeFractalModel.SelectedOrientationType = value;
                FractalBmp = GraphicsUtils.RotateImage(FractalBmp, (float)KochSnowflakeFractalModel.SelectedOrientationType);
                OnPropertyChanged(nameof(SelectedOrientationType));
            }
        }

        public int IterationsCount
        {
            get { return KochSnowflakeFractalModel.IterationsCount; }
            set
            {
                KochSnowflakeFractalModel.IterationsCount = value;
                Stopwatch sw = new Stopwatch();
                sw.Start();
                FractalBmp = GetFractal();
                RenderTime = sw.ElapsedMilliseconds / 1000.0f;
                OnPropertyChanged(nameof(IterationsCount));
            }
        }

        public int Scale
        {
            get { return KochSnowflakeFractalModel.Scale; }
            set
            {
                KochSnowflakeFractalModel.Scale = value;
                OnPropertyChanged(nameof(Scale));
            }
        }

        public float RenderTime
        {
            get { return KochSnowflakeFractalModel.RenderTime; }
            set
            {
                KochSnowflakeFractalModel.RenderTime = value;
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
        /// navigation to dragon curve fracral viewmodel
        /// </summary>
        private DelegateCommand _navigateDragonCurveFractalCommand;

        public ICommand NavigateDragonCurveFractalCommand
        {
            get
            {
                if (_navigateDragonCurveFractalCommand == null)
                {
                    Console.WriteLine("Reached here");
                    _navigateDragonCurveFractalCommand = new DelegateCommand(NavigateDragonCurveFractal);
                }
                return _navigateDragonCurveFractalCommand;
            }
        }

        public void NavigateDragonCurveFractal()
        {
            _navigationStore.CurrentViewModel = new DragonCurveFractalViewModel(_navigationStore);
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
        /// copy Fractal image to clipboard command
        /// <summary>
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
            _navigationStore.CurrentViewModel = new KochSnowflakeFractalViewModel(_navigationStore);
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

            int sWidth = 1280, sHeight = 900;
            Bitmap bmp = new Bitmap(sWidth, sHeight);
            Point center = new Point(250, 350);

            FractalGenerator fGen = new FractalGenerator();
            FractalInitiator fInit = new FractalInitiator();

            fInit.AddVertex(new Point(50, 75));
            fInit.AddVertex(new Point(175, -225));
            fInit.AddVertex(new Point(325, 75));

            fGen.AddNode(0);
            fGen.AddNode(60); //-60
            fGen.AddNode(-120); //120
            fGen.AddNode(60); //-60

            KochSnowflakeFractal kochSnowflake = new KochSnowflakeFractal(fGen, fInit, IterationsCount);
            kochSnowflake.RunIFS();
            for (int i = 0; i < kochSnowflake.EdgeCount; ++i)
            {
                Tuple<Point, Point> edge = kochSnowflake.GetEdge(i);
                Point start = new Point(edge.Item1.X + center.X, edge.Item1.Y + center.Y);
                Point end = new Point(edge.Item2.X + center.X, edge.Item2.Y + center.Y);
                if (GraphicsUtils.IsFit(start, sWidth, sHeight) && GraphicsUtils.IsFit(end, sWidth, sHeight))
                    GraphicsUtils.DrawLine(bmp, start, end, System.Drawing.Color.Black);
            }

            FractalBmp = GraphicsUtils.ConvertToWriteableBitmap(bmp); ;
            RenderTime = sw.ElapsedMilliseconds / 1000.0f;
        }
        #endregion
    }
}
