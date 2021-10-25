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
        private int _sign;
        public int EdgeCount { get; private set; }

        public FractalGenerator()
        {
            EdgeCount = 1;
            _start = new Point(-1, -1);
            _end = new Point(-1, -1);
            _nodeAngles = new List<float>();
            _startPoints = new List<Point>();
            _endPoints = new List<Point>();
            _sign = 1;
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
            EdgeCount = _nodeAngles.Count;
        }

        public bool SetSign(int sign)
        {
            if (sign == 1 || sign == -1)
            {
                _sign = sign;
                return true;
            }
            else
                return false;
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

            if (EdgeCount <= 1)
            {
                _startPoints.Add(_start);
                _endPoints.Add(_end);
            }
            else 
            {
                // Compute edge length (distance from start to end = 1)
                // Compute number of segments, we divide into
                float lengthMultiplier = 0.0f;
                float cumulativeAngle = 0.0f;
                foreach(float i in _nodeAngles)
                {
                    cumulativeAngle += i;
                    lengthMultiplier += (float)Math.Abs(Math.Cos(cumulativeAngle * (Math.PI / 180.0f)));
                }

                float initLenght = (float)Math.Sqrt(Math.Pow(_end.X - _start.X, 2.0) +
                    Math.Pow(_end.Y - _start.Y, 2.0));

                float edgeLength = initLenght / lengthMultiplier;

                // Look at the first edge
                float angle = GraphicsUtils.GetAngle(_start, _end);

                // Loop through each node and find appropriate edge
                Point startTmp = _start, endTmp = new Point(-1, -1);
                float sightAngle = angle;
                foreach (float currentAngle in _nodeAngles)
                {
                    _startPoints.Add(startTmp);
                    sightAngle += _sign * currentAngle;
                    endTmp = GraphicsUtils.GetEndpoint(sightAngle, startTmp, edgeLength);
                    _endPoints.Add(endTmp);
                    startTmp = endTmp;
                }
            }

            return true;
        }

    }
}
