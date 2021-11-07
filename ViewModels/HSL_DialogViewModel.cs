using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_TestProject2.Commands;

namespace WPF_TestProject2.ViewModels
{
    class HSL_DialogViewModel : ViewModelBase
    {
        private float _h;
        private float _s;
        private float _l;

        public float H 
        { 
            get
            {
                return _h;
            }
            set
            {
                if (value < 0)
                    _h = 0;
                else if (value > 360)
                    _h = 360;
                else
                    _h = value;
                OnPropertyChanged(nameof(H));
            }
        }
        public float S
        {
            get
            {
                return _s;
            }
            set
            {
                if (value < 0)
                    _s = 0;
                else if (value > 1.0)
                    _s = 1.0f;
                else
                    _s = value;
                OnPropertyChanged(nameof(S));
            }
        }
        public float L
        {
            get
            {
                return _l;
            }
            set
            {
                if (value < 0)
                    _l = 0;
                else if (value > 1.0)
                    _l = 1.0f;
                else
                    _l = value;
                OnPropertyChanged(nameof(L));
            }
        }

        public HSL_DialogViewModel(float h, float s, float l, ICommand navigateCMYK)
        {
            H = h;
            S = s;
            L = l;
            NavigateCMYK_Command = navigateCMYK;
        }

        #region Commands
        /// <summary>
        /// Navigate to CMYK color scheme dialog
        /// </summary>
        private DelegateCommand _navigateCMYK_Command;

        public ICommand NavigateCMYK_Command
        {
            get
            {
                return _navigateCMYK_Command;
            }
            set
            {
                _navigateCMYK_Command = (DelegateCommand)value;
            }
        }
        #endregion
    }
}
