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
		private Dictionary<Polygon, TranslateTransform> listOfPolygon;
		public static double GridHeight = 10;
		public static double GridWidth = 10;

		public MainPage()
        {
            this.InitializeComponent();
			listOfPolygon = new Dictionary<Polygon, TranslateTransform>();
			PopulateOneRoom();
        }

		private void PopulateOneRoom()
		{
			AddStdBox();
		}

		private void Line_RightTapped(object sender, RightTappedRoutedEventArgs e)
		{
			e.Handled = true;
			Debug.WriteLine(e.GetPosition((Polyline)sender).ToString());
		}

		void Line_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
		{
			// Move the rectangle.
			foreach(KeyValuePair<Polygon, TranslateTransform> item in listOfPolygon)
			{
				if (item.Key == sender)
				{
					TranslateTransform a = item.Value as TranslateTransform;
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
			Point position = child.TransformToVisual(Window.Current.Content).TransformPoint(new Point(0, 0));
			Point newPoint = RoomCompute.SnapGridPoint(position);
			foreach (KeyValuePair<Polygon, TranslateTransform> item in listOfPolygon)
			{
				if (item.Key == sender)
				{
					TranslateTransform a = item.Value as TranslateTransform;
					a.X += newPoint.X;
					a.Y += newPoint.Y;
					break;
				}
			}
			Debug.WriteLine("Was at X: {0}, Y : {1}\nNow at X: {2}, Y : {3}",position.X,position.Y, newPoint.X, newPoint.Y);
		}

		private void Add_Click(object sender, RoutedEventArgs e)
		{
			AddStdBox();
		}

		private void AddStdBox()
		{
			TranslateTransform tmpTransform = new TranslateTransform();

			Polygon line = new Polygon()
			{
				Points = RoomCompute.GetPoints(new Room.Room(0)),
				IsRightTapEnabled = true,
				Stroke = new SolidColorBrush(Colors.Black),
				StrokeThickness = 6,
				ManipulationMode = ManipulationModes.All,
				RenderTransform = tmpTransform,
				Fill = new SolidColorBrush(Colors.Transparent)
			};
			line.RightTapped += Line_RightTapped;
			line.ManipulationDelta += Line_ManipulationDelta;
			line.ManipulationCompleted += Line_ManipulationCompleted;
			Canvas.SetLeft(line, 50);
			Canvas.SetTop(line, 50);
			listOfPolygon.Add(line, tmpTransform);
			Plan.Children.Add(line);
		}
	}
}
