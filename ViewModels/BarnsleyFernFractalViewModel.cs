using System;
using System.Windows.Input;
using WPF_TestProject2.Classes;
using WPF_TestProject2.Commands;
using WPF_TestProject2.Models;
using WPF_TestProject2.Stores;
namespace WPF_TestProject2.ViewModels
{
    class BarnsleyFernFractalViewModel: ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public BarnsleyFernFractalModel BarnsleyFernFractalModel;

        public BarnsleyFernFractalViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            BarnsleyFernFractalModel = new BarnsleyFernFractalModel();
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
        public double[] Probabilities
        {
            get { return BarnsleyFernFractalModel.Probabilites; }
            set
            {
                BarnsleyFernFractalModel.Probabilites = value;
                OnPropertyChanged(nameof(Probabilities));
            }
        }

        public double[] A
        {
            get { return BarnsleyFernFractalModel.A; }
            set
            {
                BarnsleyFernFractalModel.A = value;
                OnPropertyChanged(nameof(A));
            }
        }
        public double[] B
        {
            get { return BarnsleyFernFractalModel.B; }
            set
            {
                BarnsleyFernFractalModel.B = value;
                OnPropertyChanged(nameof(B));
            }
        }
        public double[] C
        {
            get { return BarnsleyFernFractalModel.C; }
            set
            {
                BarnsleyFernFractalModel.C = value;
                OnPropertyChanged(nameof(C));
            }
        }
        public double[] D
        {
            get { return BarnsleyFernFractalModel.D; }
            set
            {
                BarnsleyFernFractalModel.D = value;
                OnPropertyChanged(nameof(D));
            }
        }
        public double[] E
        {
            get { return BarnsleyFernFractalModel.E; }
            set
            {
                BarnsleyFernFractalModel.E = value;
                OnPropertyChanged(nameof(E));
            }
        }

        public double[] F
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
    }
}
#endregion