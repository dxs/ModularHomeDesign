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
		public static bool IsAddDoor = false;
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

		public void AddDoor(object sender, RoutedEventArgs e)
		{
			IsAddDoor = !IsAddDoor;
			if(IsAddDoor)
				EllipseDoor.Fill = new SolidColorBrush(Colors.LightSalmon);
			else
				EllipseDoor.Fill = new SolidColorBrush(Colors.LightGray);
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

		public void AddStdBox(Room.Room room = null)
		{
			if(room == null)
				room = new Room.Room(CurrentId++);
			Grid panel = new Grid();
			Polygon line = room.draw;
			line.ManipulationDelta += Line_ManipulationDelta;
			line.ManipulationCompleted += Line_ManipulationCompleted;
			line.IsTapEnabled = true;

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
				inLine.Tapped += InLine_RightTapped;
				panel.Children.Add(inLine);
			}

			Plan.Children.Add(panel);
			listOfRoom.Add(room);
		}

		private void InLine_RightTapped(object sender, TappedRoutedEventArgs e)
		{
			
			if (!IsAddDoor)
			{
				bool breakMe = false;
				foreach (Room.Room room in listOfRoom)
				{
					foreach (Polyline line in room.lines)
						if (line == sender as Polyline)
						{
							if (line.Fill == new SolidColorBrush(Colors.Black))
								DeleteSelection(room.Id, line);
							((sender as Polyline).Parent as Grid).Children.Remove(sender as Polyline);
							breakMe = true;
							break;
						}
					if (breakMe)
						break;
				}
			}
			else//Add a door
			{
				bool breakMe = false;
				Room.Room tmp = null;
				Polyline tmpLine = null;
				foreach (Room.Room r in listOfRoom)
				{
					foreach (Polyline line in r.lines)
						if (line == sender as Polyline)
						{
							tmp = r;
							tmpLine = line;
							Plan.Children.Remove((sender as Polyline).Parent as Grid);
							listOfRoom.Remove(r);
							breakMe = true;
							break;
						}
					if (breakMe)
						break;
				}
				if(tmp != null && tmpLine != null)
				{
					/*Check if line is horizontal or vertical*/
					/*Utilisation d'une soustraction puis normalisation pour 
					 avoir un coef inverseur si necessaire afin de creer la porte dans le bon sens*/
					tmp.lines.Remove(tmpLine);
					double coefInverseur = 0;
					if (tmpLine.Points[0].X == tmpLine.Points[1].X)//Vertical
					{
						coefInverseur = tmpLine.Points[1].Y - tmpLine.Points[0].Y / Math.Abs(tmpLine.Points[1].Y - tmpLine.Points[0].Y);
						tmp.lines.Add(new Polyline()
						{
							Points = new PointCollection()
							{
								new Point(tmpLine.Points[0].X, tmpLine.Points[0].Y),
								new Point(tmpLine.Points[0].X, tmpLine.Points[0].Y + coefInverseur*GridHeight/4)
							}
						});
						tmp.lines.Add(new Polyline()
						{
							Points = new PointCollection()
							{
								new Point(tmpLine.Points[1].X, tmpLine.Points[1].Y),
								new Point(tmpLine.Points[1].X, tmpLine.Points[1].Y - coefInverseur*GridHeight/4)
							}
						});
					}
					else//Horizontal
					{
						coefInverseur = tmpLine.Points[1].X - tmpLine.Points[0].X;
						coefInverseur /= Math.Abs(coefInverseur);
						tmp.lines.Add(new Polyline()
						{
							Points = new PointCollection()
							{
								new Point(tmpLine.Points[0].X, tmpLine.Points[0].Y),
								new Point(tmpLine.Points[0].X + coefInverseur*GridWidth/4, tmpLine.Points[0].Y)
							}
						});
						tmp.lines.Add(new Polyline()
						{
							Points = new PointCollection()
							{
								new Point(tmpLine.Points[1].X, tmpLine.Points[1].Y),
								new Point(tmpLine.Points[1].X - coefInverseur*GridWidth/4, tmpLine.Points[1].Y)
							}
						});
					}
					AddStdBox(Room.Room.CopyTo(tmp));
				}
			}
		}
	}
}
