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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ModularHomeDesign
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
		public MainPage()
        {
            this.InitializeComponent();
			Manipulation.listOfRoom = new List<Room.Room>();         
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

		public void AddStdBox()
		{
			Room.Room room = new Room.Room(Manipulation.CurrentId++, "Room");
			Grid panel = new Grid();
			Polygon line = room.draw;
			line.RightTapped += Line_RightTapped;
			line.ManipulationDelta += Manipulation.Line_ManipulationDelta;
			line.ManipulationCompleted += Manipulation.Line_ManipulationCompleted;
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
			Plan.Children.Add(panel);
			Manipulation.listOfRoom.Add(room);
		}

		private static void Line_RightTapped(object sender, RightTappedRoutedEventArgs e)
		{
			e.Handled = true;
			Point position = e.GetPosition(sender as Polygon);
			Debug.WriteLine("X: {0}\tY: {1}", position.X, position.Y);
		}
	}
}
