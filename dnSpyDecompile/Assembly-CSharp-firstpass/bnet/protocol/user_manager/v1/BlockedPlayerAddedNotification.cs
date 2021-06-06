using System;
using System.IO;

namespace bnet.protocol.user_manager.v1
{
	// Token: 0x020002F3 RID: 755
	public class BlockedPlayerAddedNotification : IProtoBuf
	{
		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06002D0A RID: 11530 RVA: 0x0009A7E6 File Offset: 0x000989E6
		// (set) Token: 0x06002D0B RID: 11531 RVA: 0x0009A7EE File Offset: 0x000989EE
		public BlockedPlayer Player { get; set; }

		// Token: 0x06002D0C RID: 11532 RVA: 0x0009A7F7 File Offset: 0x000989F7
		public void SetPlayer(BlockedPlayer val)
		{
			this.Player = val;
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06002D0D RID: 11533 RVA: 0x0009A800 File Offset: 0x00098A00
		// (set) Token: 0x06002D0E RID: 11534 RVA: 0x0009A808 File Offset: 0x00098A08
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

		// Token: 0x06002D0F RID: 11535 RVA: 0x0009A81B File Offset: 0x00098A1B
		public void SetGameAccountId(EntityId val)
		{
			this.GameAccountId = val;
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06002D10 RID: 11536 RVA: 0x0009A824 File Offset: 0x00098A24
		// (set) Token: 0x06002D11 RID: 11537 RVA: 0x0009A82C File Offset: 0x00098A2C
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

		// Token: 0x06002D12 RID: 11538 RVA: 0x0009A83F File Offset: 0x00098A3F
		public void SetAccountId(EntityId val)
		{
			this.AccountId = val;
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x0009A848 File Offset: 0x00098A48
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

		// Token: 0x06002D14 RID: 11540 RVA: 0x0009A89C File Offset: 0x00098A9C
		public override bool Equals(object obj)
		{
			BlockedPlayerAddedNotification blockedPlayerAddedNotification = obj as BlockedPlayerAddedNotification;
			return blockedPlayerAddedNotification != null && this.Player.Equals(blockedPlayerAddedNotification.Player) && this.HasGameAccountId == blockedPlayerAddedNotification.HasGameAccountId && (!this.HasGameAccountId || this.GameAccountId.Equals(blockedPlayerAddedNotification.GameAccountId)) && this.HasAccountId == blockedPlayerAddedNotification.HasAccountId && (!this.HasAccountId || this.AccountId.Equals(blockedPlayerAddedNotification.AccountId));
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06002D15 RID: 11541 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x0009A921 File Offset: 0x00098B21
		public static BlockedPlayerAddedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<BlockedPlayerAddedNotification>(bs, 0, -1);
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x0009A92B File Offset: 0x00098B2B
		public void Deserialize(Stream stream)
		{
			BlockedPlayerAddedNotification.Deserialize(stream, this);
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x0009A935 File Offset: 0x00098B35
		public static BlockedPlayerAddedNotification Deserialize(Stream stream, BlockedPlayerAddedNotification instance)
		{
			return BlockedPlayerAddedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x0009A940 File Offset: 0x00098B40
		public static BlockedPlayerAddedNotification DeserializeLengthDelimited(Stream stream)
		{
			BlockedPlayerAddedNotification blockedPlayerAddedNotification = new BlockedPlayerAddedNotification();
			BlockedPlayerAddedNotification.DeserializeLengthDelimited(stream, blockedPlayerAddedNotification);
			return blockedPlayerAddedNotification;
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x0009A95C File Offset: 0x00098B5C
		public static BlockedPlayerAddedNotification DeserializeLengthDelimited(Stream stream, BlockedPlayerAddedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BlockedPlayerAddedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x0009A984 File Offset: 0x00098B84
		public static BlockedPlayerAddedNotification Deserialize(Stream stream, BlockedPlayerAddedNotification instance, long limit)
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

		// Token: 0x06002D1C RID: 11548 RVA: 0x0009AA86 File Offset: 0x00098C86
		public void Serialize(Stream stream)
		{
			BlockedPlayerAddedNotification.Serialize(stream, this);
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x0009AA90 File Offset: 0x00098C90
		public static void Serialize(Stream stream, BlockedPlayerAddedNotification instance)
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

		// Token: 0x06002D1E RID: 11550 RVA: 0x0009AB34 File Offset: 0x00098D34
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

		// Token: 0x0400127E RID: 4734
		public bool HasGameAccountId;

		// Token: 0x0400127F RID: 4735
		private EntityId _GameAccountId;

		// Token: 0x04001280 RID: 4736
		public bool HasAccountId;

		// Token: 0x04001281 RID: 4737
		private EntityId _AccountId;
	}
}
