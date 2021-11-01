using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_TestProject2.Classes
{
    class DragonCurveFracral
    {
        private FractalGenerator _fGen;
        private FractalInitiator _fInit;
        private List<Point> _edgeStarts;
        private List<Point> _edgeEnds;
        public int EdgeCount { get; private set; }
        public int Level { get; private set; }
        public OrientationType Orientation { get; set; }

        public DragonCurveFracral()
        {
            _fGen = new FractalGenerator();
            _fInit = new FractalInitiator();
            _edgeStarts = new List<Point>();
            _edgeEnds = new List<Point>();
            Level = 12;
            EdgeCount = 0;

            _fInit.AddVertex(new Point(50, 200)); 
            _fInit.AddVertex(new Point(400, 200));

            _fGen.AddNode(45);
            _fGen.AddNode(-90);
        }

        public DragonCurveFracral(FractalGenerator fGen, FractalInitiator fInit, int level)
        {
            _edgeStarts = new List<Point>();
            _edgeEnds = new List<Point>();
            _fGen = fGen;
            _fInit = fInit;
            Level = level;
            EdgeCount = 0;
        }

        public void RunIFS()
        {
            Tuple<Point, Point> edge = _fInit.GetEdge(0);

            IterateIFS(edge.Item1, edge.Item2, Level);
        }

        private void IterateIFS(Point p1, Point p2, int level)
        {
            double angle = Math.Round(GraphicsUtils.GetAngle(p1, p2));
            double angleRadians = Math.PI / 180 * angle;
            float segLength = GraphicsUtils.GetLength(p1, p2);

            AffineTransformation.AffineMatrix rule1 =
                new AffineTransformation.AffineMatrix(1.0 / 2, 1.0 / 2, -1.0 / 2, 1.0 / 2, 0, 0); // 1.0 / 2, -1.0 / 2, 1.0 / 2, 1.0 / 2, 0, 0
            AffineTransformation.AffineMatrix rule2 =
                new AffineTransformation.AffineMatrix(-1.0 / 2, 1.0 / 2, -1.0 / 2, -1.0 / 2, segLength, 0); // -1.0 / 2, -1.0 / 2, 1.0 / 2, -1.0 / 2, segLength, 0

            Point x1 = AffineTransformation.Transform(p2, p1, rule1);
            Point x2 = AffineTransformation.Transform(p2, p1, rule2);

            if (level < 1)
            {
                _edgeStarts.Add(p1);
                _edgeEnds.Add(p2);
                EdgeCount++;
            }
            else
            {
                IterateIFS(p1, x1, (level - 1));
                IterateIFS(p2, x2, (level - 1));
            }
        }

        public void Run()
        {
            Tuple<Point, Point> edge = _fInit.GetEdge(0);

            Iterate(edge.Item1, edge.Item2, Level, 1); 
        }

        private void Iterate(Point p1, Point p2, int level, int sign)
        {
            level--;

            int sign2 = -1;
            List<Point> edgeStartsTmp = new List<Point>();
            List<Point> edgeEndsTmp = new List<Point>();
            Tuple<Point, Point> edgeTmp = new Tuple<Point, Point>(new Point(-1, -1), new Point(-1, -1));

            // Setup generator and call it
            _fGen.SetStartPoint(p1);
            _fGen.SetEndPoint(p2);
            _fGen.SetSign(sign);
            _fGen.Compute();

            // Retrive all generated edges
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
                    Iterate(edgeStartsTmp[i], edgeEndsTmp[i], level, sign2);
                    sign2 *= -1;
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
