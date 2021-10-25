using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_TestProject2.Views
{
    /// <summary>
    /// Interaction logic for BarnsleyFernFractalView.xaml
    /// </summary>
    public partial class BarnsleyFernFractalView : UserControl
    {
        public BarnsleyFernFractalView()
        {
            InitializeComponent();
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox text = sender as TextBox;
            BindingOperations.GetBindingExpression(text, TextBox.TextProperty).UpdateSource();
        }

    }
}
