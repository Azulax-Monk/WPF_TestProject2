using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_TestProject2.Classes;

namespace WPF_TestProject2.Models
{
    class BarnsleyFernFractalModel
    {
       
        public FractalType SelectedFractalType { get; set; }
        public double[] Probabilites { get; set; }
        public double[] A { get; set; }
        public double[] B { get; set; }
        public double[] C { get; set; }
        public double[] D { get; set; }
        public double[] E { get; set; }
        public double[] F { get; set; }
        public int Scale { get; set; }
        public int RenderTime { get; set; }
        

        public BarnsleyFernFractalModel()
        {
            SelectedFractalType = FractalType.BARNSLEY_FERN;
            Probabilites = new double[] { 0.01, 0.85, 0.07, 0.07 };
            A = new double[] { 0, 0.85, 0.2, -0.15 };
            B = new double[] {0, 0.04, -0.26, 0.28 };
            C = new double[] { 0, -0.04, 0.23, 0.26};
            D = new double[] { 0.16, 0.85, 0.22, 0.24};
            E = new double[] {0,0,0,0 };
            F = new double[] {0,1.6,1.6,0.44 };

            Scale = 100;
            RenderTime = 0;
        }
    }
}
