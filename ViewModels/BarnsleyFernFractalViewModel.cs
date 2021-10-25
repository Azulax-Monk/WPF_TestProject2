using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WPF_TestProject2.Classes;
using WPF_TestProject2.Commands;
using WPF_TestProject2.Models;
using WPF_TestProject2.Stores;
namespace WPF_TestProject2.ViewModels
{
    class BarnsleyFernFractalViewModel : ViewModelBase
    {

        private System.Windows.Media.Imaging.WriteableBitmap _fractalBmp;
        private readonly NavigationStore _navigationStore;
        public BarnsleyFernFractalModel BarnsleyFernFractalModel;

        public BarnsleyFernFractalViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            BarnsleyFernFractalModel = new BarnsleyFernFractalModel();
            _fractalBmp = GetFractal();
        }


        private WriteableBitmap GetFractal()
        {
            Bitmap bmp = new Bitmap(600, 600);
            Tuple<double, double> point = new Tuple<double, double>(0, 0);
            BarnsleyFernFractal fractal = new BarnsleyFernFractal(BarnsleyFernFractalModel);
            var r = new Random();
            int randomNum;
            for (int count = 0; count < 100000; count++)
            {
                randomNum = r.Next(100);
                point = fractal.GetNextPoint(point, randomNum);
                int x = (int)(300 + 60 * point.Item1);
                int y = (int)((600 - 60 * point.Item2) - 100);
                if (!GraphicsUtils.CheckIfPointInRange(bmp, new Point(x, y)))
                    continue;
                try
                {
                    bmp.SetPixel(x, y, Color.Black);
                }
                catch (Exception ex)
                {
                    //display this on form
                    Console.WriteLine($"{ex} occured.");
                }
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
            get { return BarnsleyFernFractalModel.SelectedFractalType; }
            set
            {
                BarnsleyFernFractalModel.SelectedFractalType = value;
                if (value == FractalType.KOCH_SNOWFLAKE)
                    this.NavigateKochSnowflakeFractalCommand.Execute(null);
                else if (value == FractalType.DRAGON_CURVE)
                    this.NavigateDragonCurveFractalCommand.Execute(null);

                OnPropertyChanged(nameof(SelectedFractalType));
            }
        }
        public ObservableCollection<double> Probabilities
        {
            get { return BarnsleyFernFractalModel.Probabilites; }
            set
            {
                BarnsleyFernFractalModel.Probabilites = value;
                OnPropertyChanged(nameof(Probabilities));
            }
        }

        public ObservableCollection<double> A
        {
            get { return BarnsleyFernFractalModel.A; }
            set
            {
                BarnsleyFernFractalModel.A = value;
                OnPropertyChanged(nameof(A));
            }
        }
        public ObservableCollection<double> B
        {
            get { return BarnsleyFernFractalModel.B; }
            set
            {
                BarnsleyFernFractalModel.B = value;
                OnPropertyChanged(nameof(B));
            }
        }
        public ObservableCollection<double> C
        {
            get { return BarnsleyFernFractalModel.C; }
            set
            {
                BarnsleyFernFractalModel.C = value;
                OnPropertyChanged(nameof(C));
            }
        }
        public ObservableCollection<double> D
        {
            get { return BarnsleyFernFractalModel.D; }
            set
            {
                BarnsleyFernFractalModel.D = value;
                OnPropertyChanged(nameof(D));
            }
        }
        public ObservableCollection<double> E
        {
            get { return BarnsleyFernFractalModel.E; }
            set
            {
                BarnsleyFernFractalModel.E = value;
                OnPropertyChanged(nameof(E));
            }
        }

        public ObservableCollection<double> F
        {
            get { return BarnsleyFernFractalModel.F; }
            set
            {
                BarnsleyFernFractalModel.F = value;
                OnPropertyChanged(nameof(F));
            }
        }
        public int Scale
        {
            get { return BarnsleyFernFractalModel.Scale; }
            set
            {
                BarnsleyFernFractalModel.Scale = value;
                OnPropertyChanged(nameof(Scale));
            }
        }

        public int RenderTime
        {
            get { return BarnsleyFernFractalModel.RenderTime; }
            set
            {
                BarnsleyFernFractalModel.RenderTime = value;
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

        private Command _submitCommand;
        public Command SubmitCommand
        {
            get
            {
                return _submitCommand ??
                    (_submitCommand = new Command(obj =>
                    {
                        Console.WriteLine("sff");
                        FractalBmp = GetFractal();
                    }));
            }
        }
    }
}
#endregion