using System;
using System.IO;

namespace bnet.protocol.user_manager.v1
{
	// Token: 0x020002F4 RID: 756
	public class BlockedPlayerRemovedNotification : IProtoBuf
	{
		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06002D20 RID: 11552 RVA: 0x0009ABA5 File Offset: 0x00098DA5
		// (set) Token: 0x06002D21 RID: 11553 RVA: 0x0009ABAD File Offset: 0x00098DAD
		public BlockedPlayer Player { get; set; }

		// Token: 0x06002D22 RID: 11554 RVA: 0x0009ABB6 File Offset: 0x00098DB6
		public void SetPlayer(BlockedPlayer val)
		{
			this.Player = val;
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06002D23 RID: 11555 RVA: 0x0009ABBF File Offset: 0x00098DBF
		// (set) Token: 0x06002D24 RID: 11556 RVA: 0x0009ABC7 File Offset: 0x00098DC7
		public EntityId GameAccountId
		{
			get
			{
				return this._GameAccountId;
			}
			set
			{
				this._GameAccountId = value;
				this.HasGameAccountId = (value != null);
			}
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x0009ABDA File Offset: 0x00098DDA
		public void SetGameAccountId(EntityId val)
		{
			this.GameAccountId = val;
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06002D26 RID: 11558 RVA: 0x0009ABE3 File Offset: 0x00098DE3
		// (set) Token: 0x06002D27 RID: 11559 RVA: 0x0009ABEB File Offset: 0x00098DEB
		public EntityId AccountId
		{
			get
			{
				return this._AccountId;
			}
			set
			{
				this._AccountId = value;
				this.HasAccountId = (value != null);
			}
		}

		// Token: 0x06002D28 RID: 11560 RVA: 0x0009ABFE File Offset: 0x00098DFE
		public void SetAccountId(EntityId val)
		{
			this.AccountId = val;
		}

		// Token: 0x06002D29 RID: 11561 RVA: 0x0009AC08 File Offset: 0x00098E08
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Player.GetHashCode();
			if (this.HasGameAccountId)
			{
				num ^= this.GameAccountId.GetHashCode();
			}
			if (this.HasAccountId)
			{
				num ^= this.AccountId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x0009AC5C File Offset: 0x00098E5C
		public override bool Equals(object obj)
		{
			BlockedPlayerRemovedNotification blockedPlayerRemovedNotification = obj as BlockedPlayerRemovedNotification;
			return blockedPlayerRemovedNotification != null && this.Player.Equals(blockedPlayerRemovedNotification.Player) && this.HasGameAccountId == blockedPlayerRemovedNotification.HasGameAccountId && (!this.HasGameAccountId || this.GameAccountId.Equals(blockedPlayerRemovedNotification.GameAccountId)) && this.HasAccountId == blockedPlayerRemovedNotification.HasAccountId && (!this.HasAccountId || this.AccountId.Equals(blockedPlayerRemovedNotification.AccountId));
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06002D2B RID: 11563 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002D2C RID: 11564 RVA: 0x0009ACE1 File Offset: 0x00098EE1
		public static BlockedPlayerRemovedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<BlockedPlayerRemovedNotification>(bs, 0, -1);
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x0009ACEB File Offset: 0x00098EEB
		public void Deserialize(Stream stream)
		{
			BlockedPlayerRemovedNotification.Deserialize(stream, this);
		}

		// Token: 0x06002D2E RID: 11566 RVA: 0x0009ACF5 File Offset: 0x00098EF5
		public static BlockedPlayerRemovedNotification Deserialize(Stream stream, BlockedPlayerRemovedNotification instance)
		{
			return BlockedPlayerRemovedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002D2F RID: 11567 RVA: 0x0009AD00 File Offset: 0x00098F00
		public static BlockedPlayerRemovedNotification DeserializeLengthDelimited(Stream stream)
		{
			BlockedPlayerRemovedNotification blockedPlayerRemovedNotification = new BlockedPlayerRemovedNotification();
			BlockedPlayerRemovedNotification.DeserializeLengthDelimited(stream, blockedPlayerRemovedNotification);
			return blockedPlayerRemovedNotification;
		}

		// Token: 0x06002D30 RID: 11568 RVA: 0x0009AD1C File Offset: 0x00098F1C
		public static BlockedPlayerRemovedNotification DeserializeLengthDelimited(Stream stream, BlockedPlayerRemovedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BlockedPlayerRemovedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x0009AD44 File Offset: 0x00098F44
		public static BlockedPlayerRemovedNotification Deserialize(Stream stream, BlockedPlayerRemovedNotification instance, long limit)
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
				else if (num != 10)
				{
					if (num != 18)
					{
						if (num != 26)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else if (instance.AccountId == null)
						{
							instance.AccountId = EntityId.DeserializeLengthDelimited(stream);
						}
						else
						{
							EntityId.DeserializeLengthDelimited(stream, instance.AccountId);
						}
					}
					else if (instance.GameAccountId == null)
					{
						instance.GameAccountId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.GameAccountId);
					}
				}
				else if (instance.Player == null)
				{
					instance.Player = BlockedPlayer.DeserializeLengthDelimited(stream);
				}
				else
				{
					BlockedPlayer.DeserializeLengthDelimited(stream, instance.Player);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x0009AE46 File Offset: 0x00099046
		public void Serialize(Stream stream)
		{
			BlockedPlayerRemovedNotification.Serialize(stream, this);
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x0009AE50 File Offset: 0x00099050
		public static void Serialize(Stream stream, BlockedPlayerRemovedNotification instance)
		{
			if (instance.Player == null)
			{
				throw new ArgumentNullException("Player", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
			BlockedPlayer.Serialize(stream, instance.Player);
			if (instance.HasGameAccountId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.GameAccountId);
			}
			if (instance.HasAccountId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AccountId);
			}
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x0009AEF4 File Offset: 0x000990F4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Player.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasGameAccountId)
			{
				num += 1U;
				uint serializedSize2 = this.GameAccountId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasAccountId)
			{
				num += 1U;
				uint serializedSize3 = this.AccountId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num + 1U;
		}

		// Token: 0x04001283 RID: 4739
		public bool HasGameAccountId;

		// Token: 0x04001284 RID: 4740
		private EntityId _GameAccountId;

		// Token: 0x04001285 RID: 4741
		public bool HasAccountId;

		// Token: 0x04001286 RID: 4742
		private EntityId _AccountId;
	}
}
