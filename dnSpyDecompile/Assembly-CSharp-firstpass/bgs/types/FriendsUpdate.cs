using System;
using System.Runtime.InteropServices;

namespace bgs.types
{
	// Token: 0x02000273 RID: 627
	public struct FriendsUpdate
	{
		// Token: 0x04000FD2 RID: 4050
		public int action;

		// Token: 0x04000FD3 RID: 4051
		public BnetEntityId entity1;

		// Token: 0x04000FD4 RID: 4052
		public BnetEntityId entity2;

		// Token: 0x04000FD5 RID: 4053
		public int int1;

		// Token: 0x04000FD6 RID: 4054
		public string string1;

		// Token: 0x04000FD7 RID: 4055
		public string string2;

		// Token: 0x04000FD8 RID: 4056
		public string string3;

		// Token: 0x04000FD9 RID: 4057
		public ulong long1;

		// Token: 0x04000FDA RID: 4058
		public ulong long2;

		// Token: 0x04000FDB RID: 4059
		public ulong long3;

		// Token: 0x04000FDC RID: 4060
		[MarshalAs(UnmanagedType.I1)]
		public bool bool1;

		// Token: 0x020006F3 RID: 1779
		public enum Action
		{
			// Token: 0x040022B9 RID: 8889
			UNKNOWN,
			// Token: 0x040022BA RID: 8890
			FRIEND_ADDED,
			// Token: 0x040022BB RID: 8891
			FRIEND_REMOVED,
			// Token: 0x040022BC RID: 8892
			FRIEND_INVITE,
			// Token: 0x040022BD RID: 8893
			FRIEND_INVITE_REMOVED,
			// Token: 0x040022BE RID: 8894
			FRIEND_SENT_INVITE,
			// Token: 0x040022BF RID: 8895
			FRIEND_SENT_INVITE_REMOVED,
			// Token: 0x040022C0 RID: 8896
			FRIEND_ROLE_CHANGE,
			// Token: 0x040022C1 RID: 8897
			FRIEND_ATTR_CHANGE,
			// Token: 0x040022C2 RID: 8898
			FRIEND_GAME_ADDED,
			// Token: 0x040022C3 RID: 8899
			FRIEND_GAME_REMOVED
		}
	}
}
