using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExcellonSorter
{
    class ExcellonParser
    {
        private static Regex CoordinateRegEx = new Regex("X([0-9]+)Y([0-9]+)");
        private List<Point2D> mPoints = new List<Point2D>();


        public void ProcessStream(StreamReader reader, StreamWriter writer)
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                ProcessLine(line, writer);
            }
        }

        private void ProcessLine(string line, StreamWriter writer)
        {
            var m = CoordinateRegEx.Match(line);

            if (!m.Success)
            {
                FlushPoints(writer);
                writer.WriteLine(line);
                return;
            }

            var x = GetDouble(m.Groups[1].Value);
            var y = GetDouble(m.Groups[2].Value);

            mPoints.Add(new Point2D(x, y));
        }


        private void FlushPoints(StreamWriter writer)
        {
            mPoints = PointSorter.Sort(mPoints);

            foreach (var p in mPoints) writer.WriteLine(p);

            mPoints.Clear();
        }

        private double GetDouble(string val)
        {
            double result;

            if (double.TryParse(val, out result)) return result / 10000;

            return 0;
        }
    }

    class Point2D
    {
        public double X;
        public double Y;

        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double GetDistanceTo(Point2D target)
        {
            var x = target.X - X;
            var y = target.Y - Y;

            return Math.Sqrt(x * x + y * y);
        }

        public override string ToString()
        {
            return string.Format("X{0:000000}Y{1:000000}", X * 10000, Y * 10000);
        }
    }

}
