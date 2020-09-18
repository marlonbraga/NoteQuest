using System;
using System.Collections.Generic;
using System.Text;

namespace Game {
	public class Door
	{
		public Direction Direction { get; set; }
		public Room previousRoom { get; set; }
		public Room FrontRoom { get; set; }
		public string mapIcon = "🚪";
		public string optionIcon = "*";
		public bool isLocked;

		public Door(Direction direction, Room previousRoom)
		{
			this.Direction = direction;
			this.previousRoom = previousRoom;

			optionIcon = SetOptionIconBy(direction);
		}
		private string SetOptionIconBy(Direction direction) {
			string icon = "";
			if(direction == Direction.up) {
				icon = "⏫";
			} else if(direction == Direction.left) {
				icon = "⏪";
			} else if(direction == Direction.down) {
				icon = "⏬";
			} else if(direction == Direction.right) {
				icon = "⏩";
			}
			return icon;
		}

		public Room PassDoor()
		{
			if (FrontRoom == null)
			{
				RoomBuilder dungeonRoomBuilder = new RoomBuilder();
				FrontRoom = dungeonRoomBuilder.BuildRoom(this);
			}
			FrontRoom.Enter();

			return FrontRoom;
		}

		public Door InvertDoor() {
			Door door = this;
			Direction direction = door.Direction;
			if(direction == Direction.up) {
				direction = Direction.down;
			} else if(direction == Direction.left) {
				direction = Direction.right;
			} else if(direction == Direction.down) {
				direction = Direction.up;
			} else if(direction == Direction.right) {
				direction = Direction.left;
			}
			this.Direction = direction;
			door.SetOptionIconBy(direction);
			return door;
		}
	}
}
