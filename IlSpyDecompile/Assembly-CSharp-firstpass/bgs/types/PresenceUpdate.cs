namespace bgs.types
{
	public struct PresenceUpdate
	{
		public EntityId entityId;

		public uint programId;

		public uint groupId;

		public uint fieldId;

		public ulong index;

		public bool boolVal;

		public long intVal;

		public string stringVal;

		public EntityId entityIdVal;

		public byte[] blobVal;

		public bool valCleared;
	}
}
