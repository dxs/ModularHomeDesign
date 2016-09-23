using ModularHomeDesign.Model;
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
using static ModularHomeDesign.Model.Manipulation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ModularHomeDesign
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
		public static bool IsMoveAll = false;
		public MainPage()
        {
            this.InitializeComponent();
			listOfRoom = new List<Room.Room>();

			//PopulateGrid(Plan);
			PopulateOneRoom();
        }

		public void PopulateOneRoom()
		{
			AddStdBox();
		}

		public void Add_Click(object sender, RoutedEventArgs e)
		{
			AddStdBox();
		}

		public void MoveAll(object sender, RoutedEventArgs e)
		{
			IsMoveAll = !IsMoveAll;
			if (IsMoveAll)
				EllipsePin.Fill = new SolidColorBrush(Colors.LightSalmon);
			else
				EllipsePin.Fill = new SolidColorBrush(Colors.LightGray);

			Debug.WriteLine("Pin or UnPin");
		}

		public void AddStdBox()
		{
			Room.Room room = new Room.Room(CurrentId++, "Room");
			Grid panel = new Grid();
			Polygon line = room.draw;
			line.RightTapped += Line_RightTapped;
			line.ManipulationDelta += Line_ManipulationDelta;
			line.ManipulationCompleted += Line_ManipulationCompleted;
			line.IsTapEnabled = true;
			Canvas.SetLeft(line, 50);
			Canvas.SetTop(line, 50);

			TextBox box = new TextBox()
			{
				Text = "Room",
				RenderTransform = line.RenderTransform,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center,
				BorderThickness = new Thickness(1),
				BorderBrush = new SolidColorBrush(Colors.LightSalmon) { Opacity = 0.7 },
				Background = new SolidColorBrush(Colors.LightSalmon) { Opacity = 0.3 },
			};
			Canvas.SetLeft(panel, 50);
			Canvas.SetTop(panel, 50);
			panel.Children.Add(line);
			panel.Children.Add(box);

			foreach (Polyline inLine in room.lines)
			{
				inLine.RightTapped += InLine_RightTapped;
				panel.Children.Add(inLine);
			}

			Plan.Children.Add(panel);
			listOfRoom.Add(room);
		}

		private void InLine_RightTapped(object sender, RightTappedRoutedEventArgs e)
		{
			if (sender == null)
				return;
			foreach(Room.Room room in listOfRoom)
			{
				if (room == null)
					continue;

				foreach(Polyline line in room.lines)
				{
					if (line == null)
						continue;

					if (line == sender as Polyline)
					{
						int lineId = 1;
						if (line.Fill == new SolidColorBrush(Colors.Black))
							lineId = ChangeSelection(room.Id, line);
						((sender as Polyline).Parent as Grid).Children.Remove(sender as Polyline);
						//((sender as Polyline).Parent as Grid).Children.Add(room.lines[1]);
						break;
					}
				}
			}
		}

		private static void Line_RightTapped(object sender, RightTappedRoutedEventArgs e)
		{
			e.Handled = true;
			Point position = e.GetPosition(sender as Polyline);
			Debug.WriteLine("X: {0}\tY: {1}", position.X, position.Y);
		}
	}
}
