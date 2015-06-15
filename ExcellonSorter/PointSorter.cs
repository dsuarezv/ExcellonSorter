using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcellonSorter
{
    class PointSorter
    {
        public static List<Point2D> Sort(List<Point2D> pointList)
        {
            // Go through the list and sort by shortest distance to last point. Start in 0, 0

            List<Point2D> result = new List<Point2D>();
            Point2D currentPoint = new Point2D(0, 0);

            while (pointList.Count > 0)
            {
                var p = PopClosestPoint(currentPoint, pointList);
                result.Add(p);
                currentPoint = p;
            }

            return result;
        }

        private static Point2D PopClosestPoint(Point2D target, List<Point2D> pointList)
        { 
            double currentDistance = double.MaxValue;
            Point2D result = null;

            foreach (var p in pointList)
            { 
                var distance = p.GetDistanceTo(target);

                if (distance < currentDistance)
                {
                    currentDistance = distance;
                    result = p;
                }
            }

            if (result != null)
            {
                pointList.Remove(result);
            }

            return result;
        }
    }
}
