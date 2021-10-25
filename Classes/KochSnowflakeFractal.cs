using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_TestProject2.Classes
{
    class KochSnowflakeFractal
    {
        private FractalGenerator _fGen;
        private FractalInitiator _fInit;
        private List<Point> _edgeStarts;
        private List<Point> _edgeEnds;
        public int EdgeCount { get; private set; }
        public int Level { get; private set; }
        public OrientationType Orientation { get; set; }

        public KochSnowflakeFractal()
        {
            _fGen = new FractalGenerator();
            _fInit = new FractalInitiator();
            _edgeStarts = new List<Point>();
            _edgeEnds = new List<Point>();
            Level = 4;
            EdgeCount = 0;
            
            _fInit.AddVertex(new Point(0, 0)); //120, 340
            _fInit.AddVertex(new Point(100, 100)); //200, 40
            _fInit.AddVertex(new Point(200, 0)); //280, 340

            _fGen.AddNode(0);
            _fGen.AddNode(60);
            _fGen.AddNode(-120);
            _fGen.AddNode(60);
        }

        public KochSnowflakeFractal(FractalGenerator fGen, FractalInitiator fInit, int level)
        {
            _edgeStarts = new List<Point>();
            _edgeEnds = new List<Point>();
            _fGen = fGen;
            _fInit = fInit;
            Level = level;
            EdgeCount = 0;
        }

        public void Run()
        {
            Tuple<Point, Point> edgeTmp = new Tuple<Point, Point>(new Point(-1, -1), new Point(-1, -1));            
            for (int i = 0; i < _fInit.EdgeCount; ++i)
            {
                edgeTmp = _fInit.GetEdge(i);
                if (edgeTmp != null)
                    Itterate(edgeTmp.Item1, edgeTmp.Item2, Level);
            }
        }

        private void Itterate(Point p1, Point p2, int level)
        {
            level--;

            List<Point> edgeStartsTmp = new List<Point>();
            List<Point> edgeEndsTmp = new List<Point>();
            Tuple<Point, Point> edgeTmp = new Tuple<Point, Point>(new Point(-1, -1), new Point(-1, -1));

            _fGen.SetStartPoint(p1);
            _fGen.SetEndPoint(p2);
            _fGen.Compute();

            for (int i = 0; i < _fGen.EdgeCount; ++i)
            {
                edgeTmp = _fGen.GetEdge(i);
                if (edgeTmp != null)
                {
                    edgeStartsTmp.Add(edgeTmp.Item1);
                    edgeEndsTmp.Add(edgeTmp.Item2);
                }
            }

            for (int i = 0; i < _fGen.EdgeCount; ++i)
            {
                if (level == 0)
                {
                    _edgeStarts.Add(edgeStartsTmp[i]);
                    _edgeEnds.Add(edgeEndsTmp[i]);
                    EdgeCount++;
                }
                else
                {
                    Itterate(edgeStartsTmp[i], edgeEndsTmp[i], level);
                }
            }
        }

        public Tuple<Point, Point> GetEdge(int index)
        {
            if (index < 0 || index > (_edgeStarts.Count - 1))
                return null;

            return new Tuple<Point, Point>(_edgeStarts[index],
                _edgeEnds[index]);
        }
    }
}
