using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_TestProject2.Classes;

namespace WPF_TestProject2.Models
{
    class DragonCurveFractalModel
    {
        public FractalType SelectedFractalType { get; set;}
        public int RecursionsCount { get; set; }
        public OrientationType SelectedOrientationType { get; set; }
        public int Scale { get; set; }
        public float RenderTime { get; set; }

        public DragonCurveFractalModel(FractalType fType, int recCount, OrientationType oType, int scale, int renderTime)
        {
            SelectedFractalType = fType;
            RecursionsCount = recCount;
            SelectedOrientationType = oType;
            Scale = scale;
            RenderTime = renderTime;
        }

        public DragonCurveFractalModel()
        {
            SelectedFractalType = FractalType.DRAGON_CURVE;
            RecursionsCount = 1;
            SelectedOrientationType = OrientationType.UP;
            Scale = 100;
            RenderTime = 0;
        }
    }
}
