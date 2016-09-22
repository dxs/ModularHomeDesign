using System;
using System.Collections.Generic;
using System.Diagnostics;
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
		public static PointCollection GetPoints(double left, double top, double height, double width)
		{
			PointCollection points = new PointCollection();

			points.Add(new Point(left, top));
			points.Add(new Point(left + width, top));
			points.Add(new Point(left + width, top + height));
			points.Add(new Point(left, top + height));
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
			double gridWidth = MainPage.GridWidth;
			double gridHeight = MainPage.GridHeight;
			double snapX = 0;
			double snapY = 0;

			if (original.X % gridWidth < gridWidth / 2)
				snapX = original.X - original.X % gridWidth;
			else
				snapX = original.X + (gridWidth - original.X % gridWidth);

			if (original.Y % gridHeight < gridHeight / 2)
				snapY = original.Y - original.Y % gridHeight;
			else
				snapY = original.Y + (gridHeight - original.Y % gridHeight);

			Debug.WriteLine("original X: {0}, Y: {1}\tEnd X: {2}, Y_ {3}", original.X, original.Y, snapX, snapY);
			return new Point(snapX, snapY);
		}

		public static bool HasTwoCommonPoint(Room a, Room b)
		{
			if (a.top == b.top + b.height)
				return true;
			if (a.top + a.height == b.top)
				return true;
			if (a.left == b.left + b.width)
				return true;
			if (a.left + a.width == b.left)
				return true;
			return false;
		}
	}
}
