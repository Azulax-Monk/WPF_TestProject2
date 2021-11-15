using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WPF_TestProject2.Classes;
using WPF_TestProject2.Commands;
using WPF_TestProject2.Stores;


namespace WPF_TestProject2.ViewModels
{
    class AffineTransformationsViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        private System.Windows.Media.Imaging.WriteableBitmap _transformationsBmp;

        public Point P1 { get; set; }

        public Point P2 { get; set; }

        public Point P3 { get; set; }

        public Point P4 { get; set; }

        public double A { get; set; }

        public double B { get; set; }

        public AffineTransformationsViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        #region Commands

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
        /// copy Fractal image to clipboard command
        /// <summary>
        private DelegateCommand _copyImageToClipboardCommand;

        public ICommand CopyImageToClipboardCommand
        {
            get
            {
                if (_copyImageToClipboardCommand == null)
                {
                    _copyImageToClipboardCommand = new DelegateCommand(() => CopyImageToClipboard(_transformationsBmp));
                }
                return _copyImageToClipboardCommand;
            }
        }

        public void CopyImageToClipboard(WriteableBitmap bmp)
        {
            System.Windows.Clipboard.SetImage(bmp);
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
            _navigationStore.CurrentViewModel = new AffineTransformationsViewModel(_navigationStore);
        }

        /// <summary>
        /// Apply paralelogram settings and draw it
        /// </summary>
        private DelegateCommand _applyCommand;

        public ICommand ApplyCommand
        {
            get
            {
                if (_applyCommand == null)
                {
                    _applyCommand = new DelegateCommand(Apply);
                }
                return _applyCommand;
            }
        }

        public void Apply()
        {
            if (ParallelogramExists(P1, P2, P3))
            {
                P4 = FindLastEdge(P1, P2, P3);
            }
        }

        private bool ParallelogramExists(Point p1, Point p2, Point p3)
        {
            if (GraphicsUtils.IsPointOnLine(p1, p2, p3) ||
                GraphicsUtils.IsPointOnLine(p1, p3, p2) ||
                GraphicsUtils.IsPointOnLine(p1, p3, p1))
                return false;
            else
                return true;
        }

        private Point FindLastEdge(Point p1, Point p2, Point p3)
        {
            Point p4 = new Point(0, 0);

            if(p2.X > p1.X && p3.X > p1.X)
            {
                p4.X = p2.X + p3.X;
            }
            else if (p2.X < p1.X && p3.X > p1.X)
            {
                p4.X = p3.X - (p1.X - p2.X);
            }
            else if (p2.X > p1.X && p3.X < p1.X)
            {
                p4.X = p2.X - (p1.X - p3.X);
            }
            else if (p2.X < p1.X && p3.X < p1.X)
            {

            }

            return p4;
        }
        #endregion
    }
}
