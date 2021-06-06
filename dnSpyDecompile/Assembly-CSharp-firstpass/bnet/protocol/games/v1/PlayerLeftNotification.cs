using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000378 RID: 888
	public class PlayerLeftNotification : IProtoBuf
	{
		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06003883 RID: 14467 RVA: 0x000B8FFD File Offset: 0x000B71FD
		// (set) Token: 0x06003884 RID: 14468 RVA: 0x000B9005 File Offset: 0x000B7205
		public GameHandle GameHandle { get; set; }

		// Token: 0x06003885 RID: 14469 RVA: 0x000B900E File Offset: 0x000B720E
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06003886 RID: 14470 RVA: 0x000B9017 File Offset: 0x000B7217
		// (set) Token: 0x06003887 RID: 14471 RVA: 0x000B901F File Offset: 0x000B721F
		public EntityId GameAccountId { get; set; }

		// Token: 0x06003888 RID: 14472 RVA: 0x000B9028 File Offset: 0x000B7228
		public void SetGameAccountId(EntityId val)
		{
			this.GameAccountId = val;
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06003889 RID: 14473 RVA: 0x000B9031 File Offset: 0x000B7231
		// (set) Token: 0x0600388A RID: 14474 RVA: 0x000B9039 File Offset: 0x000B7239
		public uint Reason
		{
			get
			{
				return this._Reason;
			}
			set
			{
				this._Reason = value;
				this.HasReason = true;
			}
		}

		// Token: 0x0600388B RID: 14475 RVA: 0x000B9049 File Offset: 0x000B7249
		public void SetReason(uint val)
		{
			this.Reason = val;
		}

		// Token: 0x0600388C RID: 14476 RVA: 0x000B9054 File Offset: 0x000B7254
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.GameHandle.GetHashCode();
			num ^= this.GameAccountId.GetHashCode();
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x000B90A4 File Offset: 0x000B72A4
		public override bool Equals(object obj)
		{
			PlayerLeftNotification playerLeftNotification = obj as PlayerLeftNotification;
			return playerLeftNotification != null && this.GameHandle.Equals(playerLeftNotification.GameHandle) && this.GameAccountId.Equals(playerLeftNotification.GameAccountId) && this.HasReason == playerLeftNotification.HasReason && (!this.HasReason || this.Reason.Equals(playerLeftNotification.Reason));
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x0600388E RID: 14478 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600388F RID: 14479 RVA: 0x000B9116 File Offset: 0x000B7316
		public static PlayerLeftNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<PlayerLeftNotification>(bs, 0, -1);
		}

		// Token: 0x06003890 RID: 14480 RVA: 0x000B9120 File Offset: 0x000B7320
		public void Deserialize(Stream stream)
		{
			PlayerLeftNotification.Deserialize(stream, this);
		}

		// Token: 0x06003891 RID: 14481 RVA: 0x000B912A File Offset: 0x000B732A
		public static PlayerLeftNotification Deserialize(Stream stream, PlayerLeftNotification instance)
		{
			return PlayerLeftNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003892 RID: 14482 RVA: 0x000B9138 File Offset: 0x000B7338
		public static PlayerLeftNotification DeserializeLengthDelimited(Stream stream)
		{
			PlayerLeftNotification playerLeftNotification = new PlayerLeftNotification();
			PlayerLeftNotification.DeserializeLengthDelimited(stream, playerLeftNotification);
			return playerLeftNotification;
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x000B9154 File Offset: 0x000B7354
		public static PlayerLeftNotification DeserializeLengthDelimited(Stream stream, PlayerLeftNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PlayerLeftNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06003894 RID: 14484 RVA: 0x000B917C File Offset: 0x000B737C
		public static PlayerLeftNotification Deserialize(Stream stream, PlayerLeftNotification instance, long limit)
		{
			instance.Reason = 1U;
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
							instance.Reason = ProtocolParser.ReadUInt32(stream);
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
				else if (instance.GameHandle == null)
				{
					instance.GameHandle = GameHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003895 RID: 14485 RVA: 0x000B926B File Offset: 0x000B746B
		public void Serialize(Stream stream)
		{
			PlayerLeftNotification.Serialize(stream, this);
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x000B9274 File Offset: 0x000B7474
		public static void Serialize(Stream stream, PlayerLeftNotification instance)
		{
			if (instance.GameHandle == null)
			{
				throw new ArgumentNullException("GameHandle", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
			GameHandle.Serialize(stream, instance.GameHandle);
			if (instance.GameAccountId == null)
			{
				throw new ArgumentNullException("GameAccountId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
			EntityId.Serialize(stream, instance.GameAccountId);
			if (instance.HasReason)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
			}
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x000B9318 File Offset: 0x000B7518
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.GameHandle.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			uint serializedSize2 = this.GameAccountId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			if (this.HasReason)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Reason);
			}
			return num + 2U;
		}

		// Token: 0x040014FF RID: 5375
		public bool HasReason;

		// Token: 0x04001500 RID: 5376
		private uint _Reason;
	}
}
