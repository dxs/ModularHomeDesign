using ModularHomeDesign.Room;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ModularHomeDesign
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
		private List<Room.Room> listOfRoom;
		public static double GridHeight = 50;
		public static double GridWidth = 50;
		int CurrentId = 0;

		public MainPage()
        {
            this.InitializeComponent();
			listOfRoom = new List<Room.Room>();
			PopulateOneRoom();
			//PopulateGrid();
        }

		private void PopulateGrid()
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
				Canvas.SetLeft(a, i*50);
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
				Canvas.SetTop(a, i*50);
				Canvas.SetLeft(a, 0);
				Plan.Children.Add(a);
			}

		}

		private void PopulateOneRoom()
		{
			AddStdBox();
		}

		void Line_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
		{
			// Move the rectangle.
			foreach(Room.Room item in listOfRoom)
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

		private void Line_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
		{
			//Stick to grid
			Polygon child = sender as Polygon;
			SnapToGrid(child);
			foreach(Room.Room item in listOfRoom)
				if (item.draw == child)
					AddRelatives(item);
			foreach (Room.Room item in listOfRoom)
				Debug.WriteLine(item.ToString());
		}

		private void AddRelatives(Room.Room child)
		{
			foreach(Room.Room item in listOfRoom)
			{
				int result = RoomCompute.HasTwoCommonPoint(child, item);
				switch(result)
				{
					case 1:
						child.LeftRoomId = item.Id;
						item.RightRoomId = child.Id;
						break;
					case 2:
						child.TopRoomId = item.Id;
						item.DownRoomId = child.Id;
						break;
					case 3:
						child.RightRoomId = item.Id;
						item.LeftRoomId = child.Id;
						break;
					case 4:
						child.DownRoomId = item.Id;
						item.TopRoomId = child.Id;
						break;
					default:
						break;
				}
			}
		}

		private void SnapToGrid(Polygon child)
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

		private void Add_Click(object sender, RoutedEventArgs e)
		{
			AddStdBox();
		}

		private void AddStdBox()
		{
			Room.Room room = new Room.Room(CurrentId++);
			Polygon line = room.draw;
			line.RightTapped += Line_RightTapped;
			line.ManipulationDelta += Line_ManipulationDelta;
			line.ManipulationCompleted += Line_ManipulationCompleted;
			line.IsTapEnabled = true;
			Canvas.SetLeft(line, 50);
			Canvas.SetTop(line, 50);
			listOfRoom.Add(room);
			Plan.Children.Add(line);
		}

		private void Line_RightTapped(object sender, RightTappedRoutedEventArgs e)
		{
			e.Handled = true;

		}

	}
}
