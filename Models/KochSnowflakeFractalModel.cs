using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_TestProject2.Classes;

namespace WPF_TestProject2.Models
{
    class KochSnowflakeFractalModel
    {
        public FractalType SelectedFractalType { get; set; }
        public int IterationsCount { get; set; }
        public OrientationType SelectedOrientationType { get; set; }
        public int Scale { get; set; }
        public int RenderTime { get; set; }

        public KochSnowflakeFractalModel(FractalType fType, int recCount, OrientationType oType, int scale, int renderTime)
        {
            SelectedFractalType = fType;
            IterationsCount = recCount;
            SelectedOrientationType = oType;
            Scale = scale;
            RenderTime = renderTime;
        }

        public KochSnowflakeFractalModel()
        {
            SelectedFractalType = FractalType.KOCH_SNOWFLAKE;
            IterationsCount = 1;
            SelectedOrientationType = OrientationType.UP;
            Scale = 100;
            RenderTime = 1;
        }
    }
}
