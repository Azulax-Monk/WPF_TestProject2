using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    class KochSnowflakeFractalViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public KochSnowflakeFractalModel KochSnowflakeFractalModel;

        public KochSnowflakeFractalViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            KochSnowflakeFractalModel = new KochSnowflakeFractalModel();
        }

        public FractalType SelectedFractalType
        {
            get { return KochSnowflakeFractalModel.SelectedFractalType; }
            set
            {
                KochSnowflakeFractalModel.SelectedFractalType = value;
                if (value == FractalType.DRAGON_CURVE)
                    this.NavigateDragonCurveFractalCommand.Execute(null);
                //else if (value = FractalType.BARNSLEY_FERN)
                    
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
        #endregion
    }
}
