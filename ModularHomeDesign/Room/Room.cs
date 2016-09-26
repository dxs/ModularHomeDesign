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
		public bool? LeftDoor { get; set; }
		public bool? RightDoor { get; set; }
		public bool? TopDoor { get; set; }
		public bool? BotDoor { get; set; }

		public int LeftRoomId { get; set; }
		public int RightRoomId { get; set; }
		public int TopRoomId { get; set; }
		public int BotRoomId { get; set; }

		public Polygon draw { get; set; }
		public List<Polyline> lines { get; set; }
		public TranslateTransform transform { get; set; }

		public Room(int _id)
		{
			Id = _id;
			Name = "Room";
			left = 0;
			top = 0;
			width = 200;
			height = 200;
			lines = new List<Polyline>();
			lines = Slots.GetAllWall(left, top);
			LeftDoor = false;
			RightDoor = false;
			TopDoor = false;
			BotDoor = false;

			LeftRoomId = -1;
			RightRoomId = -1;
			TopRoomId = -1;
			BotRoomId = -1;

			AddPolygon();
			SetPolyline();
		}

		public Room(int _id, string _name)
		{
			Id = _id;
			Name = _name;
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
				Points = Compute.GetPoints(this.top, this.left, this.height, this.width),
				IsRightTapEnabled = true,
				Stroke = new SolidColorBrush(Colors.Transparent),
				StrokeThickness = 0,
				ManipulationMode = ManipulationModes.All,
				RenderTransform = transform,
				Fill = new SolidColorBrush(Colors.Transparent)
			};
		}


		private void SetPolyline()
		{
			foreach(Polyline line in lines)
			{
				line.ManipulationMode = ManipulationModes.All;
				line.RenderTransform = transform;
			}
		}

		public static Room CopyTo(Room old)
		{
			Room newRoom = new Room(old.Id, old.Name);
			newRoom.BotDoor = old.BotDoor;
			newRoom.LeftDoor = old.LeftDoor;
			newRoom.RightDoor = old.RightDoor;
			newRoom.TopDoor = old.TopDoor;

			newRoom.BotRoomId = old.BotRoomId;
			newRoom.TopRoomId = old.TopRoomId;
			newRoom.LeftRoomId = old.LeftRoomId;
			newRoom.RightRoomId = old.RightRoomId;

			newRoom.width = old.width;
			newRoom.height = old.height;
			newRoom.top = old.top;
			newRoom.left = old.left;
			newRoom.lines = new List<Polyline>();
			foreach (Polyline line in old.lines)
				newRoom.lines.Add(new Polyline()
				{
					Points = new PointCollection()
					{
						new Point(line.Points[0].X, line.Points[0].Y),
						new Point(line.Points[1].X, line.Points[1].Y)
					}
				});
			newRoom.AddPolygon();
			newRoom.SetPolyline();

			return newRoom;
		}
	}
}
