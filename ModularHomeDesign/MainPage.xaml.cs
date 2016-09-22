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
		private TranslateTransform dragTranslation;
		private List<Polygon> listOfPolygon;

		public MainPage()
        {
            this.InitializeComponent();
			listOfPolygon = new List<Polygon>();
			PopulateOneRoom();
        }

		private void PopulateOneRoom()
		{
			dragTranslation = new TranslateTransform();
			Polygon line = new Polygon()
			{
				Points = RoomCompute.GetPoints(new Room.Room(0)),
				IsRightTapEnabled = true,
				Stroke = new SolidColorBrush(Colors.Black),
				StrokeThickness = 6,
				ManipulationMode = ManipulationModes.All,
				RenderTransform = this.dragTranslation,
				Fill = new SolidColorBrush(Colors.Transparent)
			};
			line.RightTapped += Line_RightTapped;
			line.ManipulationDelta += Line_ManipulationDelta;
			Canvas.SetLeft(line, 50);
			Canvas.SetTop(line, 200);
			listOfPolygon.Add(line);
			Plan.Children.Add(line);
		}

		private void Line_RightTapped(object sender, RightTappedRoutedEventArgs e)
		{
			e.Handled = true;
			Debug.WriteLine(e.GetPosition((Polyline)sender).ToString());
		}


		//TestRectangle.ManipulationDelta += Drag_ManipulationDelta;
		//	dragTranslation = new TranslateTransform();
		//TestRectangle.RenderTransform = this.dragTranslation;

		void Line_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
		{
			// Move the rectangle.
			dragTranslation.X += e.Delta.Translation.X;
			dragTranslation.Y += e.Delta.Translation.Y;
			foreach(Polygon child in listOfPolygon)
				child.TransformToVisual(Window.Current.Content).TransformPoint(new Point(0,0));
		}
	}
}
