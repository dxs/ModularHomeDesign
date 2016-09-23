using ModularHomeDesign.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace ModularHomeDesign.Room
{
	class Slots
	{

		public static Polyline Slot1(double _left, double _top, bool? _isDoor)
		{
			PointCollection col = new PointCollection();
			if (_isDoor == null)//Open
				;

			if (_isDoor == false)//Wall
				col = new PointCollection()
				{
					new Point(_left, _top),
					new Point(_left + Manipulation.GridWidth*4, _top)
				};

			if (_isDoor == true)//Door
				;

			Polyline line = new Polyline()
			{
				IsRightTapEnabled = true,
				Stroke = new SolidColorBrush(Colors.Black),
				StrokeThickness = 6,
				ManipulationMode = ManipulationModes.All,
				Points = col
			};
			return line;
		}
		
		public static Polyline Slot2(double _left, double _top, bool? _isDoor)
		{
			PointCollection col = new PointCollection();
			if (_isDoor == null)//Open
				;

			if (_isDoor == false)//Wall
				col = new PointCollection()
				{
					new Point(_left+Manipulation.GridWidth*4, _top),
					new Point(_left + Manipulation.GridWidth*4, _top+Manipulation.GridHeight*4)
				};

			if (_isDoor == true)//Door
				;

			Polyline line = new Polyline()
			{
				IsRightTapEnabled = true,
				Stroke = new SolidColorBrush(Colors.Black),
				StrokeThickness = 6,
				ManipulationMode = ManipulationModes.All,
				Points = col
			};
			return line;
		}

		public static Polyline Slot3(double _left, double _top, bool? _isDoor)
		{
			PointCollection col = new PointCollection();
			if (_isDoor == null)//Open
				;

			if (_isDoor == false)//Wall
				col = new PointCollection()
				{
					new Point(_left + Manipulation.GridWidth*4, _top+Manipulation.GridHeight*4),
					new Point(_left, _top+Manipulation.GridHeight*4)
				};

			if (_isDoor == true)//Door
				;

			Polyline line = new Polyline()
			{
				IsRightTapEnabled = true,
				Stroke = new SolidColorBrush(Colors.Black),
				StrokeThickness = 6,
				ManipulationMode = ManipulationModes.All,
				Points = col
			};
			return line;
		}

		public static Polyline Slot4(double _left, double _top, bool? _isDoor)
		{
			PointCollection col = new PointCollection();
			if (_isDoor == null)//Open
				;

			if (_isDoor == false)//Wall
				col = new PointCollection()
				{
					new Point(_left, _top+Manipulation.GridHeight*4),
					new Point(_left , _top)
				};

			if (_isDoor == true)//Door
				;

			Polyline line = new Polyline()
			{
				IsRightTapEnabled = true,
				Stroke = new SolidColorBrush(Colors.Black),
				StrokeThickness = 6,
				ManipulationMode = ManipulationModes.All,
				Points = col
			};
			return line;
		}

		public static List<Polyline> GetAllWall(double _left, double _top)
		{
			List<Polyline> list = new List<Polyline>();
			list.Add(Slot1(_left, _top, false));
			list.Add(Slot2(_left, _top, false));
			list.Add(Slot3(_left, _top, false));
			list.Add(Slot4(_left, _top, false));
			return list;
		}
	}
}
