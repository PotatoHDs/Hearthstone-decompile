using System.Runtime.InteropServices;

namespace bgs.types
{
	public struct FriendsUpdate
	{
		public enum Action
		{
			UNKNOWN,
			FRIEND_ADDED,
			FRIEND_REMOVED,
			FRIEND_INVITE,
			FRIEND_INVITE_REMOVED,
			FRIEND_SENT_INVITE,
			FRIEND_SENT_INVITE_REMOVED,
			FRIEND_ROLE_CHANGE,
			FRIEND_ATTR_CHANGE,
			FRIEND_GAME_ADDED,
			FRIEND_GAME_REMOVED
		}

		public int action;

		public BnetEntityId entity1;

		public BnetEntityId entity2;

		public int int1;

		public string string1;

		public string string2;

		public string string3;

		public ulong long1;

		public ulong long2;

		public ulong long3;

		[MarshalAs(UnmanagedType.I1)]
		public bool bool1;
	}
}
