using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000377 RID: 887
	public class GameEndedNotification : IProtoBuf
	{
		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06003870 RID: 14448 RVA: 0x000B8D4C File Offset: 0x000B6F4C
		// (set) Token: 0x06003871 RID: 14449 RVA: 0x000B8D54 File Offset: 0x000B6F54
		public GameHandle GameHandle { get; set; }

		// Token: 0x06003872 RID: 14450 RVA: 0x000B8D5D File Offset: 0x000B6F5D
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06003873 RID: 14451 RVA: 0x000B8D66 File Offset: 0x000B6F66
		// (set) Token: 0x06003874 RID: 14452 RVA: 0x000B8D6E File Offset: 0x000B6F6E
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

		// Token: 0x06003875 RID: 14453 RVA: 0x000B8D7E File Offset: 0x000B6F7E
		public void SetReason(uint val)
		{
			this.Reason = val;
		}

		// Token: 0x06003876 RID: 14454 RVA: 0x000B8D88 File Offset: 0x000B6F88
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.GameHandle.GetHashCode();
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003877 RID: 14455 RVA: 0x000B8DCC File Offset: 0x000B6FCC
		public override bool Equals(object obj)
		{
			GameEndedNotification gameEndedNotification = obj as GameEndedNotification;
			return gameEndedNotification != null && this.GameHandle.Equals(gameEndedNotification.GameHandle) && this.HasReason == gameEndedNotification.HasReason && (!this.HasReason || this.Reason.Equals(gameEndedNotification.Reason));
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06003878 RID: 14456 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003879 RID: 14457 RVA: 0x000B8E29 File Offset: 0x000B7029
		public static GameEndedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameEndedNotification>(bs, 0, -1);
		}

		// Token: 0x0600387A RID: 14458 RVA: 0x000B8E33 File Offset: 0x000B7033
		public void Deserialize(Stream stream)
		{
			GameEndedNotification.Deserialize(stream, this);
		}

		// Token: 0x0600387B RID: 14459 RVA: 0x000B8E3D File Offset: 0x000B703D
		public static GameEndedNotification Deserialize(Stream stream, GameEndedNotification instance)
		{
			return GameEndedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600387C RID: 14460 RVA: 0x000B8E48 File Offset: 0x000B7048
		public static GameEndedNotification DeserializeLengthDelimited(Stream stream)
		{
			GameEndedNotification gameEndedNotification = new GameEndedNotification();
			GameEndedNotification.DeserializeLengthDelimited(stream, gameEndedNotification);
			return gameEndedNotification;
		}

		// Token: 0x0600387D RID: 14461 RVA: 0x000B8E64 File Offset: 0x000B7064
		public static GameEndedNotification DeserializeLengthDelimited(Stream stream, GameEndedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameEndedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x0600387E RID: 14462 RVA: 0x000B8E8C File Offset: 0x000B708C
		public static GameEndedNotification Deserialize(Stream stream, GameEndedNotification instance, long limit)
		{
			instance.Reason = 0U;
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
					if (num != 16)
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

		// Token: 0x0600387F RID: 14463 RVA: 0x000B8F45 File Offset: 0x000B7145
		public void Serialize(Stream stream)
		{
			GameEndedNotification.Serialize(stream, this);
		}

		// Token: 0x06003880 RID: 14464 RVA: 0x000B8F50 File Offset: 0x000B7150
		public static void Serialize(Stream stream, GameEndedNotification instance)
		{
			if (instance.GameHandle == null)
			{
				throw new ArgumentNullException("GameHandle", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
			GameHandle.Serialize(stream, instance.GameHandle);
			if (instance.HasReason)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
			}
		}

		// Token: 0x06003881 RID: 14465 RVA: 0x000B8FB8 File Offset: 0x000B71B8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.GameHandle.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasReason)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Reason);
			}
			return num + 1U;
		}

		// Token: 0x040014FB RID: 5371
		public bool HasReason;

		// Token: 0x040014FC RID: 5372
		private uint _Reason;
	}
}
