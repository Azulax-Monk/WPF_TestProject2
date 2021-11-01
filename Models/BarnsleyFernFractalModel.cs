using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_TestProject2.Classes;

namespace WPF_TestProject2.Models
{
    class BarnsleyFernFractalModel
    {
       
        public FractalType SelectedFractalType { get; set; }
        public ObservableCollection<double> Probabilites { get; set; }
        public ObservableCollection<double> A { get; set; }
        public ObservableCollection<double> B { get; set; }
        public ObservableCollection<double> C { get; set; }
        public ObservableCollection<double> D { get; set; }
        public ObservableCollection<double> E { get; set; }
        public ObservableCollection<double> F { get; set; }
        public int Scale { get; set; }
        public float RenderTime { get; set; }
        

        public BarnsleyFernFractalModel()
        {
            SelectedFractalType = FractalType.BARNSLEY_FERN;
            Probabilites = new ObservableCollection<double> { 0.02, 0.84, 0.07, 0.07 };
            A = new ObservableCollection<double> { 0, 0.95, 0.035, -0.04 };
            B = new ObservableCollection<double> { 0, 0.005, -0.2, 0.2 };
            C = new ObservableCollection<double> { 0, -0.05, 0.16, 0.16};
            D = new ObservableCollection<double> { 0.25, 0.93, 0.04, 0.04};
            E = new ObservableCollection<double> { 0,-0.002,-0.09,0.083 };
            F = new ObservableCollection<double> { -0.4,0.5,0.02,0.12 };

            Scale = 100;
            RenderTime = 0;
        }

        public BarnsleyFernFractalModel(int i)
        {
            SelectedFractalType = FractalType.BARNSLEY_FERN;
            switch (i) {
                case 1:
                    {
                        Probabilites = new ObservableCollection<double> { 0.02, 0.84, 0.07, 0.07 };
                        A = new ObservableCollection<double> { 0, 0.95, 0.035, -0.04 };
                        B = new ObservableCollection<double> { 0, 0.005, -0.2, 0.2 };
                        C = new ObservableCollection<double> { 0, -0.05, 0.16, 0.16 };
                        D = new ObservableCollection<double> { 0.25, 0.93, 0.04, 0.04 };
                        E = new ObservableCollection<double> { 0, -0.002, -0.09, 0.083 };
                        F = new ObservableCollection<double> { -0.4, 0.5, 0.02, 0.12 };
                        break;
                    }

                case 2:
                    {
                        Probabilites = new ObservableCollection<double> { 0.01, 0.85, 0.07, 0.07 };
                        A = new ObservableCollection<double> { 0, 0.85, 0.2, -0.15 };
                        B = new ObservableCollection<double> { 0, 0.004, -0.26, 0.28 };
                        C = new ObservableCollection<double> { 0, -0.04, 0.23, 0.26 };
                        D = new ObservableCollection<double> { 0.16, 0.85, 0.22, 0.24 };
                        E = new ObservableCollection<double> { 0, 0, 0, 0 };
                        F = new ObservableCollection<double> { 0, 1.6, 1.6, 0.44 };
                        break;
                    }
                default:
                    {
                        break;
                    }
        }
            Scale = 100;
            RenderTime = 0;
        }
    }
}
