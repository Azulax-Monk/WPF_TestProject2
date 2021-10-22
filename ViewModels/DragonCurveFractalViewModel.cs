using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_TestProject2.Classes;
using WPF_TestProject2.Commands;
using WPF_TestProject2.Models;
using WPF_TestProject2.Stores;

namespace WPF_TestProject2.ViewModels
{
    class DragonCurveFractalViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public DragonCurveFractalModel DragonCurveFractalModel;

        public DragonCurveFractalViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            DragonCurveFractalModel = new DragonCurveFractalModel();
        }

        public FractalType SelectedFractalType
        {
            get { return DragonCurveFractalModel.SelectedFractalType; }
            set
            {
                DragonCurveFractalModel.SelectedFractalType = value;
                if (value == FractalType.KOCH_SNOWFLAKE)
                    this.NavigateKochSnowflakeFractalCommand.Execute(null);
                //else if (value == FractalType.BARNSLEY_FERN)

                OnPropertyChanged(nameof(SelectedFractalType));
            }
        }

        public OrientationType SelectedOrientationType
        {
            get { return DragonCurveFractalModel.SelectedOrientationType; }
            set
            {
                DragonCurveFractalModel.SelectedOrientationType = value;
                OnPropertyChanged(nameof(SelectedOrientationType));
            }
        }

        public int RecursionsCount
        {
            get { return DragonCurveFractalModel.RecursionsCount; }
            set
            {
                DragonCurveFractalModel.RecursionsCount = value;
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

        public int RenderTime
        {
            get { return DragonCurveFractalModel.RenderTime; }
            set
            {
                DragonCurveFractalModel.RenderTime = value;
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
        #endregion
    }
}
