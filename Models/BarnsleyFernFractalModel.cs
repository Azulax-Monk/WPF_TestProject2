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
            Probabilites = new double[] { 0.02, 0.84, 0.07, 0.07 };
            A = new double[] { 0, 0.95, 0.035, -0.04 };
            B = new double[] {0, 0.005, -0.2, 0.2 };
            C = new double[] { 0, -0.05, 0.16, 0.16};
            D = new double[] { 0.25, 0.93, 0.04, 0.04};
            E = new double[] {0,-0.002,-0.09,0.083 };
            F = new double[] {-0.4,0.5,0.02,0.12 };

            Scale = 100;
            RenderTime = 0;
        }
    }
}
