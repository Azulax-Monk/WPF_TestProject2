using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_TestProject2.Classes
{
    class FractalInitiator
    {
        public int EdgeCount 
        {
            get
            {
                if (_vertices.Count < 2)
                    return -1;
                else if (_vertices.Count == 2)
                    return 1;
                else
                    return _vertices.Count;
            }
        }

        private List<Point> _vertices;

        public FractalInitiator()
        {
            _vertices = new List<Point>();
        }

        public void AddVertex(Point p)
        {
            _vertices.Add(p);
        }

        public Tuple<Point, Point> GetEdge(int index)
        {
            if (index < 0 || index > (EdgeCount - 1))
                return null;

            Point start = _vertices[index];
            Point end = new Point(-1, -1);

            if (index < EdgeCount - 1 || EdgeCount == 1)
                end = _vertices[index + 1];
            else
                end = _vertices[0];

            return new Tuple<Point, Point>(start, end);
        }
    }
}
