using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Media;

namespace ModularHomeDesign.Room
{
	static class RoomCompute
	{
		public static PointCollection GetPoints(Room a)
		{
			PointCollection points = new PointCollection();

			points.Add(new Point(a.left, a.top));
			points.Add(new Point(a.left, a.top + a.height));
			points.Add(new Point(a.left + a.width, a.top + a.height));
			points.Add(new Point(a.left + a.width, a.top));
			return points;
		}
		
		/// <summary>
		/// Get the absolute distance between two points
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static double GetDistance(Point a, Point b)
		{
			return Math.Sqrt(Math.Pow(GetDistanceX(a, b), 2) + Math.Pow(GetDistanceY(a, b), 2));
		}

		/// <summary>
		/// Get the absolute distance on x axis between two points
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static double GetDistanceX(Point a, Point b)
		{
			return Math.Abs(a.X - b.X);
		}

		/// <summary>
		/// Get th absolute distance on y axis between two points
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static double GetDistanceY(Point a, Point b)
		{
			return Math.Abs(a.Y - b.Y);
		}

		public static Point SnapGridPoint(Point original)
		{
			double snapX = Math.Round(original.X / MainPage.GridWidth) * MainPage.GridWidth;
			double snapY = Math.Round(original.Y / MainPage.GridHeight) * MainPage.GridHeight;

			return new Point(snapX, snapY);
		}
	}
}
