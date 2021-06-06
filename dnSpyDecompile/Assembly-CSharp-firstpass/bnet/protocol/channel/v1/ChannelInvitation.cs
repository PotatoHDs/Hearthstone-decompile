using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004B8 RID: 1208
	public class ChannelInvitation : IProtoBuf
	{
		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x0600545A RID: 21594 RVA: 0x0010387E File Offset: 0x00101A7E
		// (set) Token: 0x0600545B RID: 21595 RVA: 0x00103886 File Offset: 0x00101A86
		public ChannelDescription ChannelDescription { get; set; }

		// Token: 0x0600545C RID: 21596 RVA: 0x0010388F File Offset: 0x00101A8F
		public void SetChannelDescription(ChannelDescription val)
		{
			this.ChannelDescription = val;
		}

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x0600545D RID: 21597 RVA: 0x00103898 File Offset: 0x00101A98
		// (set) Token: 0x0600545E RID: 21598 RVA: 0x001038A0 File Offset: 0x00101AA0
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

		// Token: 0x0600545F RID: 21599 RVA: 0x001038B0 File Offset: 0x00101AB0
		public void SetReserved(bool val)
		{
			this.Reserved = val;
		}

		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x06005460 RID: 21600 RVA: 0x001038B9 File Offset: 0x00101AB9
		// (set) Token: 0x06005461 RID: 21601 RVA: 0x001038C1 File Offset: 0x00101AC1
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

		// Token: 0x06005462 RID: 21602 RVA: 0x001038D1 File Offset: 0x00101AD1
		public void SetRejoin(bool val)
		{
			this.Rejoin = val;
		}

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06005463 RID: 21603 RVA: 0x001038DA File Offset: 0x00101ADA
		// (set) Token: 0x06005464 RID: 21604 RVA: 0x001038E2 File Offset: 0x00101AE2
		public uint ServiceType { get; set; }

		// Token: 0x06005465 RID: 21605 RVA: 0x001038EB File Offset: 0x00101AEB
		public void SetServiceType(uint val)
		{
			this.ServiceType = val;
		}

		// Token: 0x06005466 RID: 21606 RVA: 0x001038F4 File Offset: 0x00101AF4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ChannelDescription.GetHashCode();
			if (this.HasReserved)
			{
				num ^= this.Reserved.GetHashCode();
			}
			if (this.HasRejoin)
			{
				num ^= this.Rejoin.GetHashCode();
			}
			return num ^ this.ServiceType.GetHashCode();
		}

		// Token: 0x06005467 RID: 21607 RVA: 0x00103960 File Offset: 0x00101B60
		public override bool Equals(object obj)
		{
			ChannelInvitation channelInvitation = obj as ChannelInvitation;
			return channelInvitation != null && this.ChannelDescription.Equals(channelInvitation.ChannelDescription) && this.HasReserved == channelInvitation.HasReserved && (!this.HasReserved || this.Reserved.Equals(channelInvitation.Reserved)) && this.HasRejoin == channelInvitation.HasRejoin && (!this.HasRejoin || this.Rejoin.Equals(channelInvitation.Rejoin)) && this.ServiceType.Equals(channelInvitation.ServiceType);
		}

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x06005468 RID: 21608 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005469 RID: 21609 RVA: 0x00103A03 File Offset: 0x00101C03
		public static ChannelInvitation ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelInvitation>(bs, 0, -1);
		}

		// Token: 0x0600546A RID: 21610 RVA: 0x00103A0D File Offset: 0x00101C0D
		public void Deserialize(Stream stream)
		{
			ChannelInvitation.Deserialize(stream, this);
		}

		// Token: 0x0600546B RID: 21611 RVA: 0x00103A17 File Offset: 0x00101C17
		public static ChannelInvitation Deserialize(Stream stream, ChannelInvitation instance)
		{
			return ChannelInvitation.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600546C RID: 21612 RVA: 0x00103A24 File Offset: 0x00101C24
		public static ChannelInvitation DeserializeLengthDelimited(Stream stream)
		{
			ChannelInvitation channelInvitation = new ChannelInvitation();
			ChannelInvitation.DeserializeLengthDelimited(stream, channelInvitation);
			return channelInvitation;
		}

		// Token: 0x0600546D RID: 21613 RVA: 0x00103A40 File Offset: 0x00101C40
		public static ChannelInvitation DeserializeLengthDelimited(Stream stream, ChannelInvitation instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChannelInvitation.Deserialize(stream, instance, num);
		}

		// Token: 0x0600546E RID: 21614 RVA: 0x00103A68 File Offset: 0x00101C68
		public static ChannelInvitation Deserialize(Stream stream, ChannelInvitation instance, long limit)
		{
			instance.Reserved = false;
			instance.Rejoin = false;
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
				else
				{
					if (num <= 16)
					{
						if (num != 10)
						{
							if (num == 16)
							{
								instance.Reserved = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
						else
						{
							if (instance.ChannelDescription == null)
							{
								instance.ChannelDescription = ChannelDescription.DeserializeLengthDelimited(stream);
								continue;
							}
							ChannelDescription.DeserializeLengthDelimited(stream, instance.ChannelDescription);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.Rejoin = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 32)
						{
							instance.ServiceType = ProtocolParser.ReadUInt32(stream);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600546F RID: 21615 RVA: 0x00103B61 File Offset: 0x00101D61
		public void Serialize(Stream stream)
		{
			ChannelInvitation.Serialize(stream, this);
		}

		// Token: 0x06005470 RID: 21616 RVA: 0x00103B6C File Offset: 0x00101D6C
		public static void Serialize(Stream stream, ChannelInvitation instance)
		{
			if (instance.ChannelDescription == null)
			{
				throw new ArgumentNullException("ChannelDescription", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.ChannelDescription.GetSerializedSize());
			ChannelDescription.Serialize(stream, instance.ChannelDescription);
			if (instance.HasReserved)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Reserved);
			}
			if (instance.HasRejoin)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.Rejoin);
			}
			stream.WriteByte(32);
			ProtocolParser.WriteUInt32(stream, instance.ServiceType);
		}

		// Token: 0x06005471 RID: 21617 RVA: 0x00103C04 File Offset: 0x00101E04
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.ChannelDescription.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
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
			num += ProtocolParser.SizeOfUInt32(this.ServiceType);
			return num + 2U;
		}

		// Token: 0x04001ABE RID: 6846
		public bool HasReserved;

		// Token: 0x04001ABF RID: 6847
		private bool _Reserved;

		// Token: 0x04001AC0 RID: 6848
		public bool HasRejoin;

		// Token: 0x04001AC1 RID: 6849
		private bool _Rejoin;
	}
}
