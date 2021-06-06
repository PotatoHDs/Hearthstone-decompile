using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004B9 RID: 1209
	public class ChannelInvitationParams : IProtoBuf
	{
		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x06005473 RID: 21619 RVA: 0x00103C5D File Offset: 0x00101E5D
		// (set) Token: 0x06005474 RID: 21620 RVA: 0x00103C65 File Offset: 0x00101E65
		public EntityId ChannelId { get; set; }

		// Token: 0x06005475 RID: 21621 RVA: 0x00103C6E File Offset: 0x00101E6E
		public void SetChannelId(EntityId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x06005476 RID: 21622 RVA: 0x00103C77 File Offset: 0x00101E77
		// (set) Token: 0x06005477 RID: 21623 RVA: 0x00103C7F File Offset: 0x00101E7F
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

		// Token: 0x06005478 RID: 21624 RVA: 0x00103C8F File Offset: 0x00101E8F
		public void SetReserved(bool val)
		{
			this.Reserved = val;
		}

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x06005479 RID: 21625 RVA: 0x00103C98 File Offset: 0x00101E98
		// (set) Token: 0x0600547A RID: 21626 RVA: 0x00103CA0 File Offset: 0x00101EA0
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

		// Token: 0x0600547B RID: 21627 RVA: 0x00103CB0 File Offset: 0x00101EB0
		public void SetRejoin(bool val)
		{
			this.Rejoin = val;
		}

		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x0600547C RID: 21628 RVA: 0x00103CB9 File Offset: 0x00101EB9
		// (set) Token: 0x0600547D RID: 21629 RVA: 0x00103CC1 File Offset: 0x00101EC1
		public uint ServiceType { get; set; }

		// Token: 0x0600547E RID: 21630 RVA: 0x00103CCA File Offset: 0x00101ECA
		public void SetServiceType(uint val)
		{
			this.ServiceType = val;
		}

		// Token: 0x0600547F RID: 21631 RVA: 0x00103CD4 File Offset: 0x00101ED4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ChannelId.GetHashCode();
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

		// Token: 0x06005480 RID: 21632 RVA: 0x00103D40 File Offset: 0x00101F40
		public override bool Equals(object obj)
		{
			ChannelInvitationParams channelInvitationParams = obj as ChannelInvitationParams;
			return channelInvitationParams != null && this.ChannelId.Equals(channelInvitationParams.ChannelId) && this.HasReserved == channelInvitationParams.HasReserved && (!this.HasReserved || this.Reserved.Equals(channelInvitationParams.Reserved)) && this.HasRejoin == channelInvitationParams.HasRejoin && (!this.HasRejoin || this.Rejoin.Equals(channelInvitationParams.Rejoin)) && this.ServiceType.Equals(channelInvitationParams.ServiceType);
		}

		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x06005481 RID: 21633 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005482 RID: 21634 RVA: 0x00103DE3 File Offset: 0x00101FE3
		public static ChannelInvitationParams ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelInvitationParams>(bs, 0, -1);
		}

		// Token: 0x06005483 RID: 21635 RVA: 0x00103DED File Offset: 0x00101FED
		public void Deserialize(Stream stream)
		{
			ChannelInvitationParams.Deserialize(stream, this);
		}

		// Token: 0x06005484 RID: 21636 RVA: 0x00103DF7 File Offset: 0x00101FF7
		public static ChannelInvitationParams Deserialize(Stream stream, ChannelInvitationParams instance)
		{
			return ChannelInvitationParams.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005485 RID: 21637 RVA: 0x00103E04 File Offset: 0x00102004
		public static ChannelInvitationParams DeserializeLengthDelimited(Stream stream)
		{
			ChannelInvitationParams channelInvitationParams = new ChannelInvitationParams();
			ChannelInvitationParams.DeserializeLengthDelimited(stream, channelInvitationParams);
			return channelInvitationParams;
		}

		// Token: 0x06005486 RID: 21638 RVA: 0x00103E20 File Offset: 0x00102020
		public static ChannelInvitationParams DeserializeLengthDelimited(Stream stream, ChannelInvitationParams instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChannelInvitationParams.Deserialize(stream, instance, num);
		}

		// Token: 0x06005487 RID: 21639 RVA: 0x00103E48 File Offset: 0x00102048
		public static ChannelInvitationParams Deserialize(Stream stream, ChannelInvitationParams instance, long limit)
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
							if (instance.ChannelId == null)
							{
								instance.ChannelId = EntityId.DeserializeLengthDelimited(stream);
								continue;
							}
							EntityId.DeserializeLengthDelimited(stream, instance.ChannelId);
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

		// Token: 0x06005488 RID: 21640 RVA: 0x00103F33 File Offset: 0x00102133
		public void Serialize(Stream stream)
		{
			ChannelInvitationParams.Serialize(stream, this);
		}

		// Token: 0x06005489 RID: 21641 RVA: 0x00103F3C File Offset: 0x0010213C
		public static void Serialize(Stream stream, ChannelInvitationParams instance)
		{
			if (instance.ChannelId == null)
			{
				throw new ArgumentNullException("ChannelId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
			EntityId.Serialize(stream, instance.ChannelId);
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

		// Token: 0x0600548A RID: 21642 RVA: 0x00103FD4 File Offset: 0x001021D4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.ChannelId.GetSerializedSize();
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

		// Token: 0x04001AC4 RID: 6852
		public bool HasReserved;

		// Token: 0x04001AC5 RID: 6853
		private bool _Reserved;

		// Token: 0x04001AC6 RID: 6854
		public bool HasRejoin;

		// Token: 0x04001AC7 RID: 6855
		private bool _Rejoin;
	}
}
