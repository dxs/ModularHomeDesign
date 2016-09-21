using ModularHomeDesign.Room;
using System;
using System.Collections.Generic;
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

		public MainPage()
        {
            this.InitializeComponent();
			PopulateOneRoom();
        }

		private void PopulateOneRoom()
		{
			Polyline line = new Polyline();
			line.Stroke = new SolidColorBrush(Colors.Black);
			line.StrokeThickness = 2;
			line.Points = RoomCompute.GetPoints(new Room.Room(0));
			Plan.Children.Add(line);
		}


		//TestRectangle.ManipulationDelta += Drag_ManipulationDelta;
		//	dragTranslation = new TranslateTransform();
		//TestRectangle.RenderTransform = this.dragTranslation;

		void Drag_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
		{
			// Move the rectangle.
			dragTranslation.X += e.Delta.Translation.X;
			dragTranslation.Y += e.Delta.Translation.Y;
		}
	}
}
