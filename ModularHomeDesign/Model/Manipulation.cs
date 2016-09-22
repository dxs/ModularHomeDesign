using ModularHomeDesign.Room;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace ModularHomeDesign.Model
{
	public static class Manipulation
	{
		public static List<Room.Room> listOfRoom;
		public static double GridHeight = 50;
		public static double GridWidth = 50;
		public static int CurrentId = 0;


		public static void PopulateGrid(Canvas Plan)
		{
			for (int i = 0; i < 80; i++)
			{
				Rectangle a = new Rectangle()
				{
					Stroke = new SolidColorBrush(Colors.Red),
					Fill = new SolidColorBrush(Colors.Red),
					StrokeThickness = 1,
					Width = 1,
					Height = 800
				};
				Canvas.SetTop(a, 0);
				Canvas.SetLeft(a, i * 50);
				Plan.Children.Add(a);
			}
			for (int i = 0; i < 50; i++)
			{
				Rectangle a = new Rectangle()
				{
					Stroke = new SolidColorBrush(Colors.Red),
					Fill = new SolidColorBrush(Colors.Red),
					StrokeThickness = 1,
					Height = 1,
					Width = 1800
				};
				Canvas.SetTop(a, i * 50);
				Canvas.SetLeft(a, 0);
				Plan.Children.Add(a);
			}

		}

		public static void Line_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
		{
			// Move the rectangle.
			foreach (Room.Room item in listOfRoom)
			{
				if (item.draw == sender)
				{
					TranslateTransform a = item.transform as TranslateTransform;
					a.X += e.Delta.Translation.X;
					a.Y += e.Delta.Translation.Y;
					break;
				}
			}
		}

		public static void Line_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
		{
			//Stick to grid
			Polygon child = sender as Polygon;
			SnapToGrid(child);
		}

		public static void AddRelatives(Room.Room child)
		{
			foreach (Room.Room item in listOfRoom)
			{
				int result = RoomCompute.HasTwoCommonPoint(child, item);
				switch (result)
				{
					case 1:
						child.LeftRoomTopId = item.Id;
						item.RightRoomTopId = child.Id;
						break;
					case 2:
						child.TopRoomLeftId = item.Id;
						item.BotRoomLeftId = child.Id;
						break;
					case 3:
						child.RightRoomTopId = item.Id;
						item.LeftRoomTopId = child.Id;
						break;
					case 4:
						child.BotRoomLeftId = item.Id;
						item.TopRoomLeftId = child.Id;
						break;
					default:
						break;
				}
			}
		}

		public static void SnapToGrid(Polygon child)
		{
			Point position = child.TransformToVisual(Window.Current.Content).TransformPoint(new Point(0, 0));
			Point newPoint = RoomCompute.SnapGridPoint(position);
			foreach (Room.Room item in listOfRoom)
			{
				if (item.draw == child)
				{
					TranslateTransform a = item.transform as TranslateTransform;
					a.X = newPoint.X - GridWidth;
					a.Y = newPoint.Y - GridHeight;
					item.top = a.Y;
					item.left = a.X;
					break;
				}
			}
		}
	}
}
