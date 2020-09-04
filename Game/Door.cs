using System;
using System.Collections.Generic;
using System.Text;

namespace Game {
	public class Door
	{
		public DungeonRoom FrontRoom { get; set; }
		public DungeonRoom BackRoom { get; set; }
		public string mapIcon = "🚪";
		public string optionIcon = "*";
		public bool isLocked;
		public Direction Direction { get; set; }

		public Door(Direction direction, DungeonRoom BackRoom)
		{
			this.BackRoom = BackRoom;
			this.Direction = direction;
			if(direction == Direction.up) {
				optionIcon = "⏫";
			} else if(direction == Direction.left) {
				optionIcon = "⏪";
			} else if(direction == Direction.down) {
				optionIcon = "⏬";
			} else if(direction == Direction.right) {
				optionIcon = "⏩";
			}
		}
		public DungeonRoom PassDoor()
		{
			if (FrontRoom == null)
			{
				FrontRoom = new DungeonRoom(this, BackRoom);
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
			door = new Door(direction, door.BackRoom);
			return door;
		}
	}
}
