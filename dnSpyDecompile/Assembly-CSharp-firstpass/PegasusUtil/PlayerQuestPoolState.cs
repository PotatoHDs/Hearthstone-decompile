using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200003B RID: 59
	public class PlayerQuestPoolState : IProtoBuf
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000D6FF File Offset: 0x0000B8FF
		// (set) Token: 0x06000342 RID: 834 RVA: 0x0000D707 File Offset: 0x0000B907
		public int QuestPoolId
		{
			get
			{
				return this._QuestPoolId;
			}
			set
			{
				this._QuestPoolId = value;
				this.HasQuestPoolId = true;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000D717 File Offset: 0x0000B917
		// (set) Token: 0x06000344 RID: 836 RVA: 0x0000D71F File Offset: 0x0000B91F
		public int SecondsUntilNextGrant
		{
			get
			{
				return this._SecondsUntilNextGrant;
			}
			set
			{
				this._SecondsUntilNextGrant = value;
				this.HasSecondsUntilNextGrant = true;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000D72F File Offset: 0x0000B92F
		// (set) Token: 0x06000346 RID: 838 RVA: 0x0000D737 File Offset: 0x0000B937
		public int RerollAvailableCount
		{
			get
			{
				return this._RerollAvailableCount;
			}
			set
			{
				this._RerollAvailableCount = value;
				this.HasRerollAvailableCount = true;
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000D748 File Offset: 0x0000B948
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasQuestPoolId)
			{
				num ^= this.QuestPoolId.GetHashCode();
			}
			if (this.HasSecondsUntilNextGrant)
			{
				num ^= this.SecondsUntilNextGrant.GetHashCode();
			}
			if (this.HasRerollAvailableCount)
			{
				num ^= this.RerollAvailableCount.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000D7B0 File Offset: 0x0000B9B0
		public override bool Equals(object obj)
		{
			PlayerQuestPoolState playerQuestPoolState = obj as PlayerQuestPoolState;
			return playerQuestPoolState != null && this.HasQuestPoolId == playerQuestPoolState.HasQuestPoolId && (!this.HasQuestPoolId || this.QuestPoolId.Equals(playerQuestPoolState.QuestPoolId)) && this.HasSecondsUntilNextGrant == playerQuestPoolState.HasSecondsUntilNextGrant && (!this.HasSecondsUntilNextGrant || this.SecondsUntilNextGrant.Equals(playerQuestPoolState.SecondsUntilNextGrant)) && this.HasRerollAvailableCount == playerQuestPoolState.HasRerollAvailableCount && (!this.HasRerollAvailableCount || this.RerollAvailableCount.Equals(playerQuestPoolState.RerollAvailableCount));
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000D854 File Offset: 0x0000BA54
		public void Deserialize(Stream stream)
		{
			PlayerQuestPoolState.Deserialize(stream, this);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000D85E File Offset: 0x0000BA5E
		public static PlayerQuestPoolState Deserialize(Stream stream, PlayerQuestPoolState instance)
		{
			return PlayerQuestPoolState.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000D86C File Offset: 0x0000BA6C
		public static PlayerQuestPoolState DeserializeLengthDelimited(Stream stream)
		{
			PlayerQuestPoolState playerQuestPoolState = new PlayerQuestPoolState();
			PlayerQuestPoolState.DeserializeLengthDelimited(stream, playerQuestPoolState);
			return playerQuestPoolState;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000D888 File Offset: 0x0000BA88
		public static PlayerQuestPoolState DeserializeLengthDelimited(Stream stream, PlayerQuestPoolState instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PlayerQuestPoolState.Deserialize(stream, instance, num);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000D8B0 File Offset: 0x0000BAB0
		public static PlayerQuestPoolState Deserialize(Stream stream, PlayerQuestPoolState instance, long limit)
		{
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else if (num != 8)
				{
					if (num != 16)
					{
						if (num != 24)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else
						{
							instance.RerollAvailableCount = (int)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.SecondsUntilNextGrant = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.QuestPoolId = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000D960 File Offset: 0x0000BB60
		public void Serialize(Stream stream)
		{
			PlayerQuestPoolState.Serialize(stream, this);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000D96C File Offset: 0x0000BB6C
		public static void Serialize(Stream stream, PlayerQuestPoolState instance)
		{
			if (instance.HasQuestPoolId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.QuestPoolId));
			}
			if (instance.HasSecondsUntilNextGrant)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SecondsUntilNextGrant));
			}
			if (instance.HasRerollAvailableCount)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RerollAvailableCount));
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000D9D0 File Offset: 0x0000BBD0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasQuestPoolId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.QuestPoolId));
			}
			if (this.HasSecondsUntilNextGrant)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SecondsUntilNextGrant));
			}
			if (this.HasRerollAvailableCount)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.RerollAvailableCount));
			}
			return num;
		}

		// Token: 0x04000113 RID: 275
		public bool HasQuestPoolId;

		// Token: 0x04000114 RID: 276
		private int _QuestPoolId;

		// Token: 0x04000115 RID: 277
		public bool HasSecondsUntilNextGrant;

		// Token: 0x04000116 RID: 278
		private int _SecondsUntilNextGrant;

		// Token: 0x04000117 RID: 279
		public bool HasRerollAvailableCount;

		// Token: 0x04000118 RID: 280
		private int _RerollAvailableCount;
	}
}
