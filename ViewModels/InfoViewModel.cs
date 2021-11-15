using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_TestProject2.Commands;
using WPF_TestProject2.Stores;

namespace WPF_TestProject2.ViewModels
{
    class InfoViewModel:ViewModelBase
    {
        protected readonly NavigationStore _navigationStore;
        protected ViewModelBase _previousViewModel;

        public InfoViewModel(NavigationStore navigationStore, ViewModelBase prevViewModel)
        {
            _previousViewModel = prevViewModel;
            _navigationStore = navigationStore;
        }

        protected Command _backCommand;
        public Command BackCommand
        {
            get
            {
                return _backCommand ??
                    (_backCommand = new Command(obj =>
                    {
                        _navigationStore.CurrentViewModel = _previousViewModel;
                    }));
            }
        }
    }


}
