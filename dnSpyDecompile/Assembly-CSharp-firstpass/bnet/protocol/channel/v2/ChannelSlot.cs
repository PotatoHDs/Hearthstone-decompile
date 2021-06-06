using System;
using System.IO;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000480 RID: 1152
	public class ChannelSlot : IProtoBuf
	{
		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x06004FEC RID: 20460 RVA: 0x000F823E File Offset: 0x000F643E
		// (set) Token: 0x06004FED RID: 20461 RVA: 0x000F8246 File Offset: 0x000F6446
		public bool Reserved
		{
			get
			{
				return this._Reserved;
			}
			set
			{
				this._Reserved = value;
				this.HasReserved = true;
			}
		}

		// Token: 0x06004FEE RID: 20462 RVA: 0x000F8256 File Offset: 0x000F6456
		public void SetReserved(bool val)
		{
			this.Reserved = val;
		}

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x06004FEF RID: 20463 RVA: 0x000F825F File Offset: 0x000F645F
		// (set) Token: 0x06004FF0 RID: 20464 RVA: 0x000F8267 File Offset: 0x000F6467
		public bool Rejoin
		{
			get
			{
				return this._Rejoin;
			}
			set
			{
				this._Rejoin = value;
				this.HasRejoin = true;
			}
		}

		// Token: 0x06004FF1 RID: 20465 RVA: 0x000F8277 File Offset: 0x000F6477
		public void SetRejoin(bool val)
		{
			this.Rejoin = val;
		}

		// Token: 0x06004FF2 RID: 20466 RVA: 0x000F8280 File Offset: 0x000F6480
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasReserved)
			{
				num ^= this.Reserved.GetHashCode();
			}
			if (this.HasRejoin)
			{
				num ^= this.Rejoin.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004FF3 RID: 20467 RVA: 0x000F82CC File Offset: 0x000F64CC
		public override bool Equals(object obj)
		{
			ChannelSlot channelSlot = obj as ChannelSlot;
			return channelSlot != null && this.HasReserved == channelSlot.HasReserved && (!this.HasReserved || this.Reserved.Equals(channelSlot.Reserved)) && this.HasRejoin == channelSlot.HasRejoin && (!this.HasRejoin || this.Rejoin.Equals(channelSlot.Rejoin));
		}

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x06004FF4 RID: 20468 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004FF5 RID: 20469 RVA: 0x000F8342 File Offset: 0x000F6542
		public static ChannelSlot ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelSlot>(bs, 0, -1);
		}

		// Token: 0x06004FF6 RID: 20470 RVA: 0x000F834C File Offset: 0x000F654C
		public void Deserialize(Stream stream)
		{
			ChannelSlot.Deserialize(stream, this);
		}

		// Token: 0x06004FF7 RID: 20471 RVA: 0x000F8356 File Offset: 0x000F6556
		public static ChannelSlot Deserialize(Stream stream, ChannelSlot instance)
		{
			return ChannelSlot.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004FF8 RID: 20472 RVA: 0x000F8364 File Offset: 0x000F6564
		public static ChannelSlot DeserializeLengthDelimited(Stream stream)
		{
			ChannelSlot channelSlot = new ChannelSlot();
			ChannelSlot.DeserializeLengthDelimited(stream, channelSlot);
			return channelSlot;
		}

		// Token: 0x06004FF9 RID: 20473 RVA: 0x000F8380 File Offset: 0x000F6580
		public static ChannelSlot DeserializeLengthDelimited(Stream stream, ChannelSlot instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChannelSlot.Deserialize(stream, instance, num);
		}

		// Token: 0x06004FFA RID: 20474 RVA: 0x000F83A8 File Offset: 0x000F65A8
		public static ChannelSlot Deserialize(Stream stream, ChannelSlot instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else
					{
						instance.Rejoin = ProtocolParser.ReadBool(stream);
					}
				}
				else
				{
					instance.Reserved = ProtocolParser.ReadBool(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004FFB RID: 20475 RVA: 0x000F843F File Offset: 0x000F663F
		public void Serialize(Stream stream)
		{
			ChannelSlot.Serialize(stream, this);
		}

		// Token: 0x06004FFC RID: 20476 RVA: 0x000F8448 File Offset: 0x000F6648
		public static void Serialize(Stream stream, ChannelSlot instance)
		{
			if (instance.HasReserved)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.Reserved);
			}
			if (instance.HasRejoin)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Rejoin);
			}
		}

		// Token: 0x06004FFD RID: 20477 RVA: 0x000F8484 File Offset: 0x000F6684
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasReserved)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasRejoin)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x040019CC RID: 6604
		public bool HasReserved;

		// Token: 0x040019CD RID: 6605
		private bool _Reserved;

		// Token: 0x040019CE RID: 6606
		public bool HasRejoin;

		// Token: 0x040019CF RID: 6607
		private bool _Rejoin;
	}
}
