using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularHomeDesign.Room
{
	class Room
	{
		public int Id { get; set; }

		public double left { get; set; }
		public double top { get; set; }
		public double width { get; set; }
		public double height { get; set; }

		public bool LeftDoor { get; set; }
		public bool RightDoor { get; set; }
		public bool TopDoor { get; set; }
		public bool DownDoor { get; set; }

		public int LeftRoomId { get; set; }
		public int RightRoomId { get; set; }
		public int TopRoomId { get; set; }
		public int DownRoomId { get; set; }


		public Room(int _id)
		{
			Id = _id;
			left = 0;
			top = 0;
			width = 200;
			height = 200;

			LeftDoor = false;
			RightDoor = false;
			TopDoor = false;
			DownDoor = false;

			LeftRoomId = -1;
			RightRoomId = -1;
			TopRoomId = -1;
			DownRoomId = -1;
		}
	}
}
