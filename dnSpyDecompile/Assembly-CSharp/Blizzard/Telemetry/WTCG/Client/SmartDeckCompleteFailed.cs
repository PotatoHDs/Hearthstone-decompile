using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011F7 RID: 4599
	public class SmartDeckCompleteFailed : IProtoBuf
	{
		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x0600CDD1 RID: 52689 RVA: 0x003D5EE3 File Offset: 0x003D40E3
		// (set) Token: 0x0600CDD2 RID: 52690 RVA: 0x003D5EEB File Offset: 0x003D40EB
		public Player Player
		{
			get
			{
				return this._Player;
			}
			set
			{
				this._Player = value;
				this.HasPlayer = (value != null);
			}
		}

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x0600CDD3 RID: 52691 RVA: 0x003D5EFE File Offset: 0x003D40FE
		// (set) Token: 0x0600CDD4 RID: 52692 RVA: 0x003D5F06 File Offset: 0x003D4106
		public int RequestMessageSize
		{
			get
			{
				return this._RequestMessageSize;
			}
			set
			{
				this._RequestMessageSize = value;
				this.HasRequestMessageSize = true;
			}
		}

		// Token: 0x0600CDD5 RID: 52693 RVA: 0x003D5F18 File Offset: 0x003D4118
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPlayer)
			{
				num ^= this.Player.GetHashCode();
			}
			if (this.HasRequestMessageSize)
			{
				num ^= this.RequestMessageSize.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CDD6 RID: 52694 RVA: 0x003D5F64 File Offset: 0x003D4164
		public override bool Equals(object obj)
		{
			SmartDeckCompleteFailed smartDeckCompleteFailed = obj as SmartDeckCompleteFailed;
			return smartDeckCompleteFailed != null && this.HasPlayer == smartDeckCompleteFailed.HasPlayer && (!this.HasPlayer || this.Player.Equals(smartDeckCompleteFailed.Player)) && this.HasRequestMessageSize == smartDeckCompleteFailed.HasRequestMessageSize && (!this.HasRequestMessageSize || this.RequestMessageSize.Equals(smartDeckCompleteFailed.RequestMessageSize));
		}

		// Token: 0x0600CDD7 RID: 52695 RVA: 0x003D5FD7 File Offset: 0x003D41D7
		public void Deserialize(Stream stream)
		{
			SmartDeckCompleteFailed.Deserialize(stream, this);
		}

		// Token: 0x0600CDD8 RID: 52696 RVA: 0x003D5FE1 File Offset: 0x003D41E1
		public static SmartDeckCompleteFailed Deserialize(Stream stream, SmartDeckCompleteFailed instance)
		{
			return SmartDeckCompleteFailed.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CDD9 RID: 52697 RVA: 0x003D5FEC File Offset: 0x003D41EC
		public static SmartDeckCompleteFailed DeserializeLengthDelimited(Stream stream)
		{
			SmartDeckCompleteFailed smartDeckCompleteFailed = new SmartDeckCompleteFailed();
			SmartDeckCompleteFailed.DeserializeLengthDelimited(stream, smartDeckCompleteFailed);
			return smartDeckCompleteFailed;
		}

		// Token: 0x0600CDDA RID: 52698 RVA: 0x003D6008 File Offset: 0x003D4208
		public static SmartDeckCompleteFailed DeserializeLengthDelimited(Stream stream, SmartDeckCompleteFailed instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SmartDeckCompleteFailed.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CDDB RID: 52699 RVA: 0x003D6030 File Offset: 0x003D4230
		public static SmartDeckCompleteFailed Deserialize(Stream stream, SmartDeckCompleteFailed instance, long limit)
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
						instance.RequestMessageSize = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else if (instance.Player == null)
				{
					instance.Player = Player.DeserializeLengthDelimited(stream);
				}
				else
				{
					Player.DeserializeLengthDelimited(stream, instance.Player);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600CDDC RID: 52700 RVA: 0x003D60E3 File Offset: 0x003D42E3
		public void Serialize(Stream stream)
		{
			SmartDeckCompleteFailed.Serialize(stream, this);
		}

		// Token: 0x0600CDDD RID: 52701 RVA: 0x003D60EC File Offset: 0x003D42EC
		public static void Serialize(Stream stream, SmartDeckCompleteFailed instance)
		{
			if (instance.HasPlayer)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.HasRequestMessageSize)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RequestMessageSize));
			}
		}

		// Token: 0x0600CDDE RID: 52702 RVA: 0x003D6144 File Offset: 0x003D4344
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasPlayer)
			{
				num += 1U;
				uint serializedSize = this.Player.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasRequestMessageSize)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.RequestMessageSize));
			}
			return num;
		}

		// Token: 0x0400A12B RID: 41259
		public bool HasPlayer;

		// Token: 0x0400A12C RID: 41260
		private Player _Player;

		// Token: 0x0400A12D RID: 41261
		public bool HasRequestMessageSize;

		// Token: 0x0400A12E RID: 41262
		private int _RequestMessageSize;
	}
}
