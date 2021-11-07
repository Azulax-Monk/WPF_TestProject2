using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;
using WPF_TestProject2.Classes;
using WPF_TestProject2.Commands;
using WPF_TestProject2.Stores;

namespace WPF_TestProject2.ViewModels
{
    class ColorSchemeViewModel : ViewModelBase
    {
        public ColorSchemeViewModel()
        {
      
            C = 0;
            M = 0;
            Y = 0;
            K = 0;
        }
        private readonly NavigationStore _navigationStore;

        private Bitmap _originalBmp;
        public Bitmap OriginalBmp
        {
            get { return _originalBmp; }
            set
            {
                _originalBmp = value;
                OnPropertyChanged(nameof(OriginalBmp));
            }
        }

        private Bitmap _changedBmp;
        public Bitmap ChangedBmp
        {
            get { return _changedBmp; }
            set
            {
                _changedBmp = value;
                OnPropertyChanged(nameof(ChangedBmp));
            }
        }

        private double _panelX;
        private double _panelY;
        private float _c;
        public float C
        {
            get { return _c; }
            set
            {
                _c = value;
                OnPropertyChanged(nameof(C));
            }
        }
        private float _m;
        public float M
        {
            get { return _m; }
            set
            {
                _m = value;
                OnPropertyChanged(nameof(M));
            }
        }
        private float _y;
        public float Y
        {
            get { return _y; }
            set
            {
                _y = value;
                OnPropertyChanged(nameof(Y));
            }
        }
        private float _k;
        public float K
        {
            get { return _k; }
            set
            {
                _k = value;
                OnPropertyChanged(nameof(K));
            }
        }
        private float _h;
        public float H
        {
            get { return _h; }
            set
            {
                _h = value;
                OnPropertyChanged(nameof(H));
            }
        }
        private float _s;
        public float S
        {
            get { return _s; }
            set
            {
                _s = value;
                OnPropertyChanged(nameof(S));
            }
        }
        private float _l;
        public float L
        {
            get { return _l; }
            set
            {
                _l = value;
                OnPropertyChanged(nameof(L));
            }
        }
        public double PanelX
        {
            get { return _panelX; }
            set
            {
                _panelX = value;
                OnPropertyChanged(nameof(PanelX));
                GetCMYK();
                GetHSL();
            }
        }

        public double PanelY
        {
            get { return _panelY; }
            set
            {
                _panelY = value;
                OnPropertyChanged(nameof(PanelY));
                GetCMYK();
                GetHSL();
            }
        }
        private double _lightness;
        public double Lightness
        {
            get { return _lightness; }
            set
            {
                _lightness = value;
                OnPropertyChanged(nameof(Lightness));
                ChangeLightness();
            }
        }

        public ColorSchemeViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

        }

        private void GetCMYK()
        {
            if (OriginalBmp != null)
            {
                Color col = OriginalBmp.GetPixel((int)PanelX, (int)PanelY);
                float[] cmyk = ColorUtils.rgbToCmyk(col.R, col.G, col.B);
                C = cmyk[0];
                M = cmyk[1];
                Y = cmyk[2];
                K = cmyk[3];

            }
        }

        private void GetHSL()
        {
            if (OriginalBmp != null)
            {
                Color col = OriginalBmp.GetPixel((int)PanelX, (int)PanelY);
                float[] hsl = ColorUtils.rgbToHsl(col.R, col.G, col.B);
                H = hsl[0];
                S = hsl[1];
                L = hsl[2];
            }
        }

        private void ChangeLightness()
        {
            if (OriginalBmp != null)
            {
                Bitmap bmp = new Bitmap(OriginalBmp);
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                System.Drawing.Imaging.BitmapData bmpData =
                    bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                    bmp.PixelFormat);
                IntPtr ptr = bmpData.Scan0;

                // Declare an array to hold the bytes of the bitmap.
                int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
                byte[] rgbValues = new byte[bytes];

                // Copy the RGB values into the array.
                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
                float[] currRgb;
                float[] currHsl;
                
               
                for (int counter = 0; counter < rgbValues.Length; counter += 4)
                {
                    
                    currHsl = ColorUtils.rgbToHsl(rgbValues[counter + 2], rgbValues[counter + 1], rgbValues[counter]);
                    if(currHsl[0]>=40&&currHsl[0]<=60)
                        currHsl[2] *= (float)Lightness;
                    currRgb = ColorUtils.hslToRgb(currHsl[0], currHsl[1], currHsl[2]);
                    rgbValues[counter] = (byte)Math.Round(currRgb[2] * 255);
                    rgbValues[counter+1] = (byte)Math.Round(currRgb[1] * 255);
                    rgbValues[counter+2] = (byte)Math.Round(currRgb[0] * 255);
                }
                
                System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
                bmp.UnlockBits(bmpData);
                ChangedBmp = bmp;
               

            }


        }
        #region Commands

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

        /// <summary>
        /// /////
        /// </summary>

        private DelegateCommand _loadImageCommand;

        public ICommand LoadImageCommand
        {
            get
            {
                if (_loadImageCommand == null)
                {
                    _loadImageCommand = new DelegateCommand(LoadImage);
                }
                return _loadImageCommand;
            }
        }

        public void LoadImage()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = dlg.FileName;
                OriginalBmp = new Bitmap(selectedFileName);
                ChangedBmp = OriginalBmp;
            }

        }
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
            _navigationStore.CurrentViewModel = new ColorSchemeViewModel(_navigationStore);
        }
    }

    #endregion
}