using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_TestProject2.Stores;

namespace WPF_TestProject2.ViewModels
{

    class FractalsInfoViewModel : InfoViewModel
    {
        public FractalsInfoViewModel(NavigationStore navigationStore, ViewModelBase prevViewModel)
            : base(navigationStore, prevViewModel)
        { }
    }
}
