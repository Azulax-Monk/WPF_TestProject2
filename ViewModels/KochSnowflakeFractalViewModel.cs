using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            _navigationStore = navigationStore;
            _fractalBmp = GetFractal();
            
            KochSnowflakeFractalModel = new KochSnowflakeFractalModel();
        }

        public WriteableBitmap GetFractal()
        {
            Bitmap bmp = new Bitmap(700, 700);
            Point center = new Point(300, 300);

            //FractalInitiator _fInit = new FractalInitiator();
            //_fInit.AddVertex(new Point(120, 340)); //120, 340
            //_fInit.AddVertex(new Point(200, 40)); //320, 40
            //_fInit.AddVertex(new Point(280, 340));
            //for (int i = 0; i < _fInit.EdgeCount; ++i)
            //{
            //    Tuple<Point, Point> edge = _fInit.GetEdge(i);
            //    GraphicsUtils.DrawLine(bmp, GraphicsUtils.GetPointsOnLine(edge.Item1, edge.Item2), System.Drawing.Color.White);
            //}

            KochSnowflakeFractal kochSnowflake = new KochSnowflakeFractal();
            kochSnowflake.Run();
            for (int i = 0; i < kochSnowflake.EdgeCount; ++i)
            {
                Tuple<Point, Point> edge = kochSnowflake.GetEdge(i);
                Point start = new Point(edge.Item1.X + center.X, edge.Item1.Y + center.Y);
                Point end = new Point(edge.Item2.X + center.X, edge.Item2.Y + center.Y);
                GraphicsUtils.DrawLine(bmp, GraphicsUtils.GetPointsOnLine(start, end), System.Drawing.Color.White);
            }


            return GraphicsUtils.ConvertToWriteableBitmap(bmp);
        }

        public System.Windows.Media.Imaging.WriteableBitmap FractalBmp
        {
            get { return _fractalBmp; }
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
                OnPropertyChanged(nameof(SelectedOrientationType));
            }
        }

        public int IterationsCount
        {
            get { return KochSnowflakeFractalModel.IterationsCount; }
            set
            {
                KochSnowflakeFractalModel.IterationsCount = value;
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

        public int RenderTime
        {
            get { return KochSnowflakeFractalModel.RenderTime; }
            set
            {
                KochSnowflakeFractalModel.RenderTime = value;
                OnPropertyChanged(nameof(RenderTime));
            }
        }

        #region Commands
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
        #endregion
    }
}
