using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_TestProject2.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get;  }

        public MainViewModel()
        {
            CurrentViewModel = new MenuViewModel();
        }
    } 
}
