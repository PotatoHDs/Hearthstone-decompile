using System;

namespace bgs.types
{
	// Token: 0x02000274 RID: 628
	public struct PresenceUpdate
	{
		// Token: 0x04000FDD RID: 4061
		public EntityId entityId;

		// Token: 0x04000FDE RID: 4062
		public uint programId;

		// Token: 0x04000FDF RID: 4063
		public uint groupId;

		// Token: 0x04000FE0 RID: 4064
		public uint fieldId;

		// Token: 0x04000FE1 RID: 4065
		public ulong index;

		// Token: 0x04000FE2 RID: 4066
		public bool boolVal;

		// Token: 0x04000FE3 RID: 4067
		public long intVal;

		// Token: 0x04000FE4 RID: 4068
		public string stringVal;

		// Token: 0x04000FE5 RID: 4069
		public EntityId entityIdVal;

		// Token: 0x04000FE6 RID: 4070
		public byte[] blobVal;

		// Token: 0x04000FE7 RID: 4071
		public bool valCleared;
	}
}
