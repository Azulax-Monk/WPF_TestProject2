using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_TestProject2.Commands;
using WPF_TestProject2.Stores;

namespace WPF_TestProject2.ViewModels
{
    class MenuViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public MenuViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        #region Commands
        /// <summary>
        /// Navigate to Fractals view
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
        /// Navigate to Affine Transformation view
        /// </summary>
        private DelegateCommand _navigateAffineTransformationsCommand;

        public ICommand NavigateAffineTransformationsCommand
        {
            get
            {
                if (_navigateAffineTransformationsCommand == null)
                {
                    Console.WriteLine("Reached here");
                    _navigateAffineTransformationsCommand = new DelegateCommand(NavigateAffineTransformations);
                }
                return _navigateAffineTransformationsCommand;
            }
        }

        public void NavigateAffineTransformations()
        {
            _navigationStore.CurrentViewModel = new AffineTransformationsViewModel(_navigationStore);
        }

        /// <summary>
        /// Navigate to Color Scheme view
        /// </summary>
        private DelegateCommand _navigateColorSchemeCommand;
        public ICommand NavigateColorSchemeCommand
        {
            get
            {
                if (_navigateColorSchemeCommand == null)
                {
                    Console.WriteLine("Reached here");
                    _navigateColorSchemeCommand = new DelegateCommand(NavigateColorScheme);
                }
                return _navigateColorSchemeCommand;
            }
        }

        public void NavigateColorScheme()
        {
            _navigationStore.CurrentViewModel = new ColorSchemeViewModel(_navigationStore);
        }
        #endregion
    }
}
