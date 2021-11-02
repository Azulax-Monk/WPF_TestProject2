using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WPF_TestProject2.Commands;
using WPF_TestProject2.Stores;

namespace WPF_TestProject2.ViewModels
{
    class ColorSchemeViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        private WriteableBitmap _originalBmp;
        public WriteableBitmap OriginalBmp
        {
            get { return _originalBmp; }
            set
            {
                _originalBmp = value;
                OnPropertyChanged(nameof(OriginalBmp));
            }
        }

        public ColorSchemeViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

        }

        #region Commands


        private DelegateCommand _getMousePositionCommand;

        public ICommand GetMousePositionCommand
        {
            get
            {
                if (_getMousePositionCommand == null)
                {
                    Console.WriteLine("Reached here");
                    _getMousePositionCommand = new DelegateCommand(GetMousePosition);
                }
                return _getMousePositionCommand;
            }
        }

        public void GetMousePosition()
        {

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

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string selectedFileName = dlg.FileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                OriginalBmp = new WriteableBitmap(bitmap);
            }

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