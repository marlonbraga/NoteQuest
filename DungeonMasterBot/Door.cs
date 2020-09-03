using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonMasterBot {
	class Door
	{
		public DungeonRoom FrontRoom { get; set; }
		public DungeonRoom BackRoom { get; set; }
		public string icon = "🚫";
		public bool isLocked;
		public Direction Direction { get; set; }

		public Door(Direction direction, DungeonRoom BackRoom)
		{
			this.BackRoom = BackRoom;
			this.Direction = direction;
			if(direction == Direction.up) {
				icon = "⏫";
			} else if(direction == Direction.left) {
				icon = "⏪";
			} else if(direction == Direction.down) {
				icon = "⏬";
			} else if(direction == Direction.right) {
				icon = "⏩";
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
	}
}
