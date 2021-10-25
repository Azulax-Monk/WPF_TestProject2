using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WPF_TestProject2.Classes
{
    class FractalGenerator
    {
        private List<float> _nodeAngles;
        private List<Point> _startPoints;
        private List<Point> _endPoints;
        private Point _start;
        private Point _end;
        public int EdgeCount { get; private set; }

        public FractalGenerator()
        {
            EdgeCount = 1;
            _start = new Point(-1, -1);
            _end = new Point(-1, -1);
            _nodeAngles = new List<float>();
            _startPoints = new List<Point>();
            _endPoints = new List<Point>();
        }

        public void SetStartPoint(Point p)
        {
            _start = p;
        }

        public void SetEndPoint(Point p)
        {
            _end = p;
        }

        public void AddNode(float angle)
        {
            _nodeAngles.Add(angle);
            EdgeCount = 2 + _nodeAngles.Count;
        }

        public Tuple<Point, Point> GetEdge(int index)
        {
            if (index < 0 || index > (EdgeCount - 1))
                return null;

            return new Tuple<Point, Point>(_startPoints[index], 
                _endPoints[index]);
        }

        public bool Compute()
        {
            _startPoints.Clear();
            _endPoints.Clear();

            _startPoints.Add(_start);

            if (EdgeCount == 1)
            {
                _endPoints.Add(_end);
            }
            else 
            {
                // Compute edge length (distance from start to end = 1)
                // Compute number of segments, we divide into
                float segmentCount = 2.0f;
                float cumulativeAngle = 0.0f;
                foreach(float i in _nodeAngles)
                {
                    cumulativeAngle += i;
                    segmentCount += (float)Math.Abs(Math.Cos(cumulativeAngle * (Math.PI / 180.0f)));
                }

                float initLenght = (float)Math.Sqrt(Math.Pow(_end.X - _start.X, 2.0) +
                    Math.Pow(_end.Y - _start.Y, 2.0));

                float edgeLength = initLenght / segmentCount;

                // Compute the first edge
                float angle = GraphicsUtils.GetAngle(_start, _end);
                Point edgeEnd = GraphicsUtils.GetEndpoint(angle, _start, edgeLength);
                _endPoints.Add(edgeEnd);

                // Loop through each node and find appropriate edge
                Point startTmp = edgeEnd, endTmp = new Point(-1, -1);
                float sightAngle = angle;
                foreach (float currentAngle in _nodeAngles)
                {
                    _startPoints.Add(startTmp);
                    sightAngle += currentAngle;
                    endTmp = GraphicsUtils.GetEndpoint(sightAngle, startTmp, edgeLength);
                    _endPoints.Add(endTmp);
                    startTmp = endTmp;
                }

                // Compute last edge
                _startPoints.Add(endTmp);
                _endPoints.Add(_end);
            }

            return true;
        }

    }
}
