using System;
using System.Collections.Generic;
using System.Linq;
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
	}
}
