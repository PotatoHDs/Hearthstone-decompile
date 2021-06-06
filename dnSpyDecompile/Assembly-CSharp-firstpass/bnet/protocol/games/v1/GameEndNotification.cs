using System;
using System.IO;
using bnet.protocol.games.v1.Types;
using bnet.protocol.games.v2;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000396 RID: 918
	public class GameEndNotification : IProtoBuf
	{
		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06003AE2 RID: 15074 RVA: 0x000BE9E6 File Offset: 0x000BCBE6
		// (set) Token: 0x06003AE3 RID: 15075 RVA: 0x000BE9EE File Offset: 0x000BCBEE
		public GameHandle GameHandle
		{
			get
			{
				return this._GameHandle;
			}
			set
			{
				this._GameHandle = value;
				this.HasGameHandle = (value != null);
			}
		}

		// Token: 0x06003AE4 RID: 15076 RVA: 0x000BEA01 File Offset: 0x000BCC01
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06003AE5 RID: 15077 RVA: 0x000BEA0A File Offset: 0x000BCC0A
		// (set) Token: 0x06003AE6 RID: 15078 RVA: 0x000BEA12 File Offset: 0x000BCC12
		public GameEndedReason Reason
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

		// Token: 0x06003AE7 RID: 15079 RVA: 0x000BEA22 File Offset: 0x000BCC22
		public void SetReason(GameEndedReason val)
		{
			this.Reason = val;
		}

		// Token: 0x06003AE8 RID: 15080 RVA: 0x000BEA2C File Offset: 0x000BCC2C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameHandle)
			{
				num ^= this.GameHandle.GetHashCode();
			}
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003AE9 RID: 15081 RVA: 0x000BEA7C File Offset: 0x000BCC7C
		public override bool Equals(object obj)
		{
			GameEndNotification gameEndNotification = obj as GameEndNotification;
			return gameEndNotification != null && this.HasGameHandle == gameEndNotification.HasGameHandle && (!this.HasGameHandle || this.GameHandle.Equals(gameEndNotification.GameHandle)) && this.HasReason == gameEndNotification.HasReason && (!this.HasReason || this.Reason.Equals(gameEndNotification.Reason));
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06003AEA RID: 15082 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003AEB RID: 15083 RVA: 0x000BEAFA File Offset: 0x000BCCFA
		public static GameEndNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameEndNotification>(bs, 0, -1);
		}

		// Token: 0x06003AEC RID: 15084 RVA: 0x000BEB04 File Offset: 0x000BCD04
		public void Deserialize(Stream stream)
		{
			GameEndNotification.Deserialize(stream, this);
		}

		// Token: 0x06003AED RID: 15085 RVA: 0x000BEB0E File Offset: 0x000BCD0E
		public static GameEndNotification Deserialize(Stream stream, GameEndNotification instance)
		{
			return GameEndNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003AEE RID: 15086 RVA: 0x000BEB1C File Offset: 0x000BCD1C
		public static GameEndNotification DeserializeLengthDelimited(Stream stream)
		{
			GameEndNotification gameEndNotification = new GameEndNotification();
			GameEndNotification.DeserializeLengthDelimited(stream, gameEndNotification);
			return gameEndNotification;
		}

		// Token: 0x06003AEF RID: 15087 RVA: 0x000BEB38 File Offset: 0x000BCD38
		public static GameEndNotification DeserializeLengthDelimited(Stream stream, GameEndNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameEndNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06003AF0 RID: 15088 RVA: 0x000BEB60 File Offset: 0x000BCD60
		public static GameEndNotification Deserialize(Stream stream, GameEndNotification instance, long limit)
		{
			instance.Reason = GameEndedReason.GAME_ENDED_REASON_DISSOLVED_BY_GAME_SERVER;
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
						instance.Reason = (GameEndedReason)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06003AF1 RID: 15089 RVA: 0x000BEC1A File Offset: 0x000BCE1A
		public void Serialize(Stream stream)
		{
			GameEndNotification.Serialize(stream, this);
		}

		// Token: 0x06003AF2 RID: 15090 RVA: 0x000BEC24 File Offset: 0x000BCE24
		public static void Serialize(Stream stream, GameEndNotification instance)
		{
			if (instance.HasGameHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				GameHandle.Serialize(stream, instance.GameHandle);
			}
			if (instance.HasReason)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Reason));
			}
		}

		// Token: 0x06003AF3 RID: 15091 RVA: 0x000BEC7C File Offset: 0x000BCE7C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameHandle)
			{
				num += 1U;
				uint serializedSize = this.GameHandle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasReason)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Reason));
			}
			return num;
		}

		// Token: 0x04001552 RID: 5458
		public bool HasGameHandle;

		// Token: 0x04001553 RID: 5459
		private GameHandle _GameHandle;

		// Token: 0x04001554 RID: 5460
		public bool HasReason;

		// Token: 0x04001555 RID: 5461
		private GameEndedReason _Reason;
	}
}
