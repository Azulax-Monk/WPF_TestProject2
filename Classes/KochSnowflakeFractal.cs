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
        public int Level { get; set; }
        public OrientationType Orientation { get; set; }

        public KochSnowflakeFractal()
        {
            _fGen = new FractalGenerator();
            _fInit = new FractalInitiator();
            _edgeStarts = new List<Point>();
            _edgeEnds = new List<Point>();
            Level = 2;
            EdgeCount = 0;

            _fInit.AddVertex(new Point(50, 75)); //50, 75
            _fInit.AddVertex(new Point(325, 75)); //325, 75
            //_fInit.AddVertex(new Point(175, -225));

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
                    Iterate(edgeTmp.Item1, edgeTmp.Item2, Level);
            }
        }

        public void RunIFS()
        {
            Tuple<Point, Point> edgeTmp = new Tuple<Point, Point>(new Point(-1, -1), new Point(-1, -1));
            for (int i = 0; i < _fInit.EdgeCount; ++i)
            {
                edgeTmp = _fInit.GetEdge(i);
                if (edgeTmp != null)
                    IterateIFS(edgeTmp.Item1, edgeTmp.Item2, Level);
            }
        }

        private void IterateIFS(Point p1, Point p2, int level)
        {
            double angle = Math.Round(GraphicsUtils.GetAngle(p1, p2));
            double angleRadians = Math.PI / 180 * angle;
            double turnRightRadians = Math.PI / 180 * 60; //(angle - 60)
            double turnLeftRadians = Math.PI / 180 * -60; //(angle - 60)
            float segLength = GraphicsUtils.GetLength(p1, p2);
            
            AffineTransformation.AffineMatrix rule1 =
                new AffineTransformation.AffineMatrix(1.0 / 3, 0, 0, 1.0 / 3, 0, 0);
            AffineTransformation.AffineMatrix rule2 =
                new AffineTransformation.AffineMatrix(Math.Cos(turnLeftRadians) / 3, -Math.Sin(turnLeftRadians) / 3, Math.Sin(turnLeftRadians) / 3, Math.Cos(turnLeftRadians) / 3, segLength / 3, 0);
            AffineTransformation.AffineMatrix rule3 =
                new AffineTransformation.AffineMatrix(Math.Cos(turnRightRadians) / 3, -Math.Sin(turnRightRadians) / 3, Math.Sin(turnRightRadians) / 3, Math.Cos(turnRightRadians) / 3, segLength / 2, (-segLength * Math.Sqrt(3)) / 6);
            AffineTransformation.AffineMatrix rule4 =
                new AffineTransformation.AffineMatrix(1.0 / 3, 0, 0, 1.0 / 3, (segLength * 2.0) / 3, 0);


            Point x1 = AffineTransformation.Transform(p2, p1, rule1);
            Point x2 = AffineTransformation.Transform(p2, p1, rule2);
            Point x3 = AffineTransformation.Transform(p2, p1, rule3);
            Point x4 = AffineTransformation.Transform(p2, p1, rule4);


            if (level < 1)
            {
                _edgeStarts.Add(p1);
                _edgeEnds.Add(p2);
                EdgeCount++;
            }
            else
            {
                IterateIFS(p1, x1, (level - 1));
                IterateIFS(x1, x2, (level - 1));
                IterateIFS(x2, x3, (level - 1));
                IterateIFS(x3, x4, (level - 1));
            }
        }

        private void Iterate(Point p1, Point p2, int level)
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
                    Iterate(edgeStartsTmp[i], edgeEndsTmp[i], level);
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
