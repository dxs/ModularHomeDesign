using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace ModularHomeDesign.Room
{
	public class Room
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public double left { get; set; }
		public double top { get; set; }
		public double width { get; set; }
		public double height { get; set; }

		/// <summary>
		/// true == door
		/// false == wall
		/// null == hole
		/// </summary>
		public bool? LeftDoorTop { get; set; }
		public bool? LeftDoorBot { get; set; }
		public bool? RightDoorTop { get; set; }
		public bool? RightDoorBot { get; set; }
		public bool? TopDoorLeft { get; set; }
		public bool? TopDoorRight { get; set; }
		public bool? BotDoorLeft { get; set; }
		public bool? BotDoorRight { get; set; }

		public int LeftRoomTopId { get; set; }
		public int LeftRoomBotId { get; set; }
		public int RightRoomTopId { get; set; }
		public int RightRoomBotId { get; set; }
		public int TopRoomLeftId { get; set; }
		public int TopRoomRightId { get; set; }
		public int BotRoomLeftId { get; set; }
		public int BotRoomRightId { get; set; }

		public Polygon draw { get; set; }
		public TranslateTransform transform { get; set; }

		public Room(int _id, string _name)
		{
			Id = _id;
			Name = _name;
			left = 0;
			top = 0;
			width = 200;
			height = 200;

			LeftDoorTop = false;
			LeftDoorBot = false;
			RightDoorTop = false;
			RightDoorBot = false;
			TopDoorLeft = false;
			TopDoorRight = false;
			BotDoorLeft = false;
			BotDoorRight = false;

			LeftRoomTopId = -1;
			LeftRoomBotId = -1;
			RightRoomTopId = -1;
			RightRoomBotId = -1;
			TopRoomLeftId = -1;
			TopRoomRightId = -1;
			BotRoomLeftId = -1;
			BotRoomRightId = -1;

			AddPolygon();
		}

		public override string ToString()
		{
#pragma warning disable CS0168 // La variable est déclarée mais jamais utilisée
			//string a = "Id = " + this.Id + "\tName = " + this.Name + "\nPosition:\nLeft = " + this.left + "\tTop = " + this.top + "\nSize:\nWidth = " + this.width + "\tHeight = " + this.height + "\nDoor:\n\tLeft = " + this.LeftDoor + "\n\tRight = " + this.RightDoor + "\n\tTop = " + this.TopDoor + "\n\tBottom = " + this.DownDoor + "\nRooms:\n\tLeft = " + this.LeftRoomId + "\n\tRight = " + this.RightRoomId + "\n\tTop = " + this.TopRoomId + "\n\tBottom = " + this.DownRoomId;
#pragma warning restore CS0168 // La variable est déclarée mais jamais utilisée
			return "";
		}

		private void AddPolygon()
		{
			transform = new TranslateTransform();

			draw = new Polygon()
			{
				Points = RoomCompute.GetPoints(this.top, this.left, this.height, this.width),
				IsRightTapEnabled = true,
				Stroke = new SolidColorBrush(Colors.Black),
				StrokeThickness = 6,
				ManipulationMode = ManipulationModes.All,
				RenderTransform = transform,
				Fill = new SolidColorBrush(Colors.Transparent)
			};
		}
	}
}
