using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004C5 RID: 1221
	public class GetChannelInfoResponse : IProtoBuf
	{
		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x06005579 RID: 21881 RVA: 0x0010664E File Offset: 0x0010484E
		// (set) Token: 0x0600557A RID: 21882 RVA: 0x00106656 File Offset: 0x00104856
		public ChannelInfo ChannelInfo
		{
			get
			{
				return this._ChannelInfo;
			}
			set
			{
				this._ChannelInfo = value;
				this.HasChannelInfo = (value != null);
			}
		}

		// Token: 0x0600557B RID: 21883 RVA: 0x00106669 File Offset: 0x00104869
		public void SetChannelInfo(ChannelInfo val)
		{
			this.ChannelInfo = val;
		}

		// Token: 0x0600557C RID: 21884 RVA: 0x00106674 File Offset: 0x00104874
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannelInfo)
			{
				num ^= this.ChannelInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600557D RID: 21885 RVA: 0x001066A4 File Offset: 0x001048A4
		public override bool Equals(object obj)
		{
			GetChannelInfoResponse getChannelInfoResponse = obj as GetChannelInfoResponse;
			return getChannelInfoResponse != null && this.HasChannelInfo == getChannelInfoResponse.HasChannelInfo && (!this.HasChannelInfo || this.ChannelInfo.Equals(getChannelInfoResponse.ChannelInfo));
		}

		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x0600557E RID: 21886 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600557F RID: 21887 RVA: 0x001066E9 File Offset: 0x001048E9
		public static GetChannelInfoResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetChannelInfoResponse>(bs, 0, -1);
		}

		// Token: 0x06005580 RID: 21888 RVA: 0x001066F3 File Offset: 0x001048F3
		public void Deserialize(Stream stream)
		{
			GetChannelInfoResponse.Deserialize(stream, this);
		}

		// Token: 0x06005581 RID: 21889 RVA: 0x001066FD File Offset: 0x001048FD
		public static GetChannelInfoResponse Deserialize(Stream stream, GetChannelInfoResponse instance)
		{
			return GetChannelInfoResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005582 RID: 21890 RVA: 0x00106708 File Offset: 0x00104908
		public static GetChannelInfoResponse DeserializeLengthDelimited(Stream stream)
		{
			GetChannelInfoResponse getChannelInfoResponse = new GetChannelInfoResponse();
			GetChannelInfoResponse.DeserializeLengthDelimited(stream, getChannelInfoResponse);
			return getChannelInfoResponse;
		}

		// Token: 0x06005583 RID: 21891 RVA: 0x00106724 File Offset: 0x00104924
		public static GetChannelInfoResponse DeserializeLengthDelimited(Stream stream, GetChannelInfoResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetChannelInfoResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06005584 RID: 21892 RVA: 0x0010674C File Offset: 0x0010494C
		public static GetChannelInfoResponse Deserialize(Stream stream, GetChannelInfoResponse instance, long limit)
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
				else if (num == 10)
				{
					if (instance.ChannelInfo == null)
					{
						instance.ChannelInfo = ChannelInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelInfo.DeserializeLengthDelimited(stream, instance.ChannelInfo);
					}
				}
				else
				{
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

		// Token: 0x06005585 RID: 21893 RVA: 0x001067E6 File Offset: 0x001049E6
		public void Serialize(Stream stream)
		{
			GetChannelInfoResponse.Serialize(stream, this);
		}

		// Token: 0x06005586 RID: 21894 RVA: 0x001067EF File Offset: 0x001049EF
		public static void Serialize(Stream stream, GetChannelInfoResponse instance)
		{
			if (instance.HasChannelInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ChannelInfo.GetSerializedSize());
				ChannelInfo.Serialize(stream, instance.ChannelInfo);
			}
		}

		// Token: 0x06005587 RID: 21895 RVA: 0x00106820 File Offset: 0x00104A20
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasChannelInfo)
			{
				num += 1U;
				uint serializedSize = this.ChannelInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001AFB RID: 6907
		public bool HasChannelInfo;

		// Token: 0x04001AFC RID: 6908
		private ChannelInfo _ChannelInfo;
	}
}
