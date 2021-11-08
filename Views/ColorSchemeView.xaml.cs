
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WPF_TestProject2.ViewModels;

namespace WPF_TestProject2.Views
{
    /// <summary>
    /// Interaction logic for CMYKView.xaml
    /// </summary>
    public partial class ColorSchemeView : UserControl
    {
        public ColorSchemeView()
        {
            InitializeComponent();

        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mousePosition = e.GetPosition(sender as UIElement);
            Canvas.SetLeft(selectionRectangle, mousePosition.X);
            Canvas.SetTop(selectionRectangle, mousePosition.Y);
            selectionRectangle.Visibility = System.Windows.Visibility.Visible;

        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var mousePosition = e.GetPosition(sender as UIElement);
                selectionRectangle.Width = Math.Abs( mousePosition.X - Canvas.GetLeft(selectionRectangle));
                selectionRectangle.Height = Math.Abs(mousePosition.Y - Canvas.GetTop(selectionRectangle));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ColorSchemeViewModel vm = (ColorSchemeViewModel)this.DataContext;
            vm.CanvasHeight = canvas.ActualHeight;
            vm.CanvasWidth = canvas.ActualWidth;
        }
    }
}
