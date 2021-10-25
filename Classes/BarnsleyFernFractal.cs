using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_TestProject2.Models;

namespace WPF_TestProject2.Classes
{
    class BarnsleyFernFractal
    {
       
        private BarnsleyFernFractalModel _fractalModel;
        private int[] _probabilitiesRange;

        public BarnsleyFernFractal(BarnsleyFernFractalModel fractalModel)
        {
           
            _fractalModel = fractalModel;
            _probabilitiesRange = CreateProbabilitiesRange();
        }

        public Tuple<double, double> GetNextPoint(Tuple<double, double> currPoint, int randomNum)
        {
            
            int transformationNum = 0;
            foreach(int probabilityRange in _probabilitiesRange)
            {
                if (randomNum < probabilityRange)
                {
                    break;
                }
                transformationNum++;
            }
            double nextX = GetNextX(transformationNum, currPoint);
            double nextY = GetNextY(transformationNum, currPoint);
            return new Tuple<double, double>(nextX, nextY);
        }

        private double GetNextX(int transformationNum, Tuple<double, double> currPoint)
        {
            return (_fractalModel.A[transformationNum] * currPoint.Item1) + (_fractalModel.B[transformationNum] * currPoint.Item2) +
                _fractalModel.E[transformationNum];
        }

        private double GetNextY(int transformationNum, Tuple<double, double> currPoint)
        {
            return (_fractalModel.C[transformationNum] * currPoint.Item1) + (_fractalModel.D[transformationNum] * currPoint.Item2) +
                _fractalModel.F[transformationNum];
        }

        private int[] CreateProbabilitiesRange()
        {
            List<double> probabilities = _fractalModel.Probabilites.ToList();
            int size = probabilities.Count;
            int[] probabilitiesRange = new int[size];
            for(int i = 0; i < size; i++)
            {
                if(i!=0)
                {
                    probabilitiesRange[i] = probabilitiesRange[i - 1];
                }
                probabilitiesRange[i] += (int)(probabilities[i] * 100);
            }
            return probabilitiesRange;
        }
    }
}
