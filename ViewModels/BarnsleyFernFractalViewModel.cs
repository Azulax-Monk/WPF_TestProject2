using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
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
        private BarnsleyFernFractalModel _barnsleyFernFractalModel;
        public BarnsleyFernFractalModel BarnsleyFernFractalModel
        {
            get { return _barnsleyFernFractalModel; }
            set
            {
                _barnsleyFernFractalModel = value;
                OnPropertyChanged(nameof(BarnsleyFernFractalModel));
            }
        }
        public BarnsleyFernFractalViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _barnsleyFernFractalModel = new BarnsleyFernFractalModel();
            FractalBmp = GetFractal();
        }
        public BarnsleyFernFractalViewModel(NavigationStore navigationStore, int option = 0)
        {
            _navigationStore = navigationStore;
            if (option == 2)
            {
                _barnsleyFernFractalModel = new BarnsleyFernFractalModel(2);
            }
            FractalBmp = GetFractal();
        }


        private WriteableBitmap GetFractal()
        {
            Bitmap bmp = new Bitmap(700, 700);
            Tuple<double, double> point = new Tuple<double, double>(0, 0);
            BarnsleyFernFractal fractal = new BarnsleyFernFractal(BarnsleyFernFractalModel);
            var r = new Random();
            int randomNum;
            for (int count = 0; count < 100000; count++)
            {
                randomNum = r.Next(100);
                point = fractal.GetNextPoint(point, randomNum);
                int x = (int)(300 + 60 * point.Item1);
                int y = (int)(600 - 60 * point.Item2);
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

        public float RenderTime
        {
            get { return BarnsleyFernFractalModel.RenderTime; }
            set
            {
                BarnsleyFernFractalModel.RenderTime = value;
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
        /// Handles navigation to KochSnowflake view
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
        /// Handles navigation to DragonCurve view
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
        /// Handles submitting the coefficients of equations
        /// </summary>
        private Command _submitCommand;
        public Command SubmitCommand
        {
            get
            {
                return _submitCommand ??
                    (_submitCommand = new Command(obj =>
                    {
                        Stopwatch sw = new Stopwatch();
                        sw.Start();
                        FractalBmp = GetFractal();
                        RenderTime = sw.ElapsedMilliseconds / 1000.0f;
                    }));
            }
        }
        /// <summary>
        /// Handles switching between default set of equations
        /// </summary>
        private Command _changeModelCommand;
        public Command ChangeModelCommand
        {
            get
            {
                return _changeModelCommand ??
                    (_changeModelCommand = new Command(obj =>
                    {
                        string option = (string)obj;
                        switch (option)
                        {
                            case "1":
                                _navigationStore.CurrentViewModel = new BarnsleyFernFractalViewModel(_navigationStore);
                                break;

                            case "2":
                                _navigationStore.CurrentViewModel = new BarnsleyFernFractalViewModel(_navigationStore, 2);
                                break;
                        }
                    }));
            }
        }

        /// <summary>
        /// Handles copying image
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
            _navigationStore.CurrentViewModel = new BarnsleyFernFractalViewModel(_navigationStore);
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
        #endregion
    }
}
