using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_TestProject2.Commands;

namespace WPF_TestProject2.ViewModels
{
    class CMYK_DialogViewModel : ViewModelBase
    {
        private float _c;
        private float _m;
        private float _y;
        private float _k;

        public float C
        {
            get
            {
                return _c;
            }
            set
            {
                if (value < 0)
                    _c = 0;
                else if (value > 1.0)
                    _c = 1.0f;
                else
                    _c = value;
            }
        }
        public float M
        {
            get
            {
                return _m;
            }
            set
            {
                if (value < 0)
                    _m = 0;
                else if (value > 1.0)
                    _m = 1.0f;
                else
                    _m = value;
            }
        }
        public float Y
        {
            get
            {
                return _y;
            }
            set
            {
                if (value < 0)
                    _y = 0;
                else if (value > 1.0)
                    _y = 1.0f;
                else
                    _y = value;
            }
        }
        public float K
        {
            get
            {
                return _k;
            }
            set
            {
                if (value < 0)
                    _k = 0;
                else if (value > 1.0)
                    _k = 1.0f;
                else
                    _k = value;
            }
        }

        public CMYK_DialogViewModel(float c, float m, float y, float k, ICommand navigateHSL)
        {
            C = c;
            M = m;
            Y = y;
            K = k;
            NavigateHSL_Command = navigateHSL;
        }

        #region Commands
        /// <summary>
        /// Navigate to CMYK color scheme dialog
        /// </summary>
        private DelegateCommand _navigateHSL_Command;

        public ICommand NavigateHSL_Command
        {
            get
            {
                return _navigateHSL_Command;
            }
            set
            {
                _navigateHSL_Command = (DelegateCommand)value;
            }
        }
        #endregion
    }
}
