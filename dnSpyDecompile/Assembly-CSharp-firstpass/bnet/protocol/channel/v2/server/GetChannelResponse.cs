using System;
using System.IO;
using System.Text;

namespace bnet.protocol.channel.v2.server
{
	// Token: 0x02000497 RID: 1175
	public class GetChannelResponse : IProtoBuf
	{
		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x060051DF RID: 20959 RVA: 0x000FDA1F File Offset: 0x000FBC1F
		// (set) Token: 0x060051E0 RID: 20960 RVA: 0x000FDA27 File Offset: 0x000FBC27
		public Channel Channel
		{
			get
			{
				return this._Channel;
			}
			set
			{
				this._Channel = value;
				this.HasChannel = (value != null);
			}
		}

		// Token: 0x060051E1 RID: 20961 RVA: 0x000FDA3A File Offset: 0x000FBC3A
		public void SetChannel(Channel val)
		{
			this.Channel = val;
		}

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x060051E2 RID: 20962 RVA: 0x000FDA43 File Offset: 0x000FBC43
		// (set) Token: 0x060051E3 RID: 20963 RVA: 0x000FDA4B File Offset: 0x000FBC4B
		public string CollectionId
		{
			get
			{
				return this._CollectionId;
			}
			set
			{
				this._CollectionId = value;
				this.HasCollectionId = (value != null);
			}
		}

		// Token: 0x060051E4 RID: 20964 RVA: 0x000FDA5E File Offset: 0x000FBC5E
		public void SetCollectionId(string val)
		{
			this.CollectionId = val;
		}

		// Token: 0x060051E5 RID: 20965 RVA: 0x000FDA68 File Offset: 0x000FBC68
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannel)
			{
				num ^= this.Channel.GetHashCode();
			}
			if (this.HasCollectionId)
			{
				num ^= this.CollectionId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060051E6 RID: 20966 RVA: 0x000FDAB0 File Offset: 0x000FBCB0
		public override bool Equals(object obj)
		{
			GetChannelResponse getChannelResponse = obj as GetChannelResponse;
			return getChannelResponse != null && this.HasChannel == getChannelResponse.HasChannel && (!this.HasChannel || this.Channel.Equals(getChannelResponse.Channel)) && this.HasCollectionId == getChannelResponse.HasCollectionId && (!this.HasCollectionId || this.CollectionId.Equals(getChannelResponse.CollectionId));
		}

		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x060051E7 RID: 20967 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060051E8 RID: 20968 RVA: 0x000FDB20 File Offset: 0x000FBD20
		public static GetChannelResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetChannelResponse>(bs, 0, -1);
		}

		// Token: 0x060051E9 RID: 20969 RVA: 0x000FDB2A File Offset: 0x000FBD2A
		public void Deserialize(Stream stream)
		{
			GetChannelResponse.Deserialize(stream, this);
		}

		// Token: 0x060051EA RID: 20970 RVA: 0x000FDB34 File Offset: 0x000FBD34
		public static GetChannelResponse Deserialize(Stream stream, GetChannelResponse instance)
		{
			return GetChannelResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060051EB RID: 20971 RVA: 0x000FDB40 File Offset: 0x000FBD40
		public static GetChannelResponse DeserializeLengthDelimited(Stream stream)
		{
			GetChannelResponse getChannelResponse = new GetChannelResponse();
			GetChannelResponse.DeserializeLengthDelimited(stream, getChannelResponse);
			return getChannelResponse;
		}

		// Token: 0x060051EC RID: 20972 RVA: 0x000FDB5C File Offset: 0x000FBD5C
		public static GetChannelResponse DeserializeLengthDelimited(Stream stream, GetChannelResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetChannelResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060051ED RID: 20973 RVA: 0x000FDB84 File Offset: 0x000FBD84
		public static GetChannelResponse Deserialize(Stream stream, GetChannelResponse instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else
					{
						instance.CollectionId = ProtocolParser.ReadString(stream);
					}
				}
				else if (instance.Channel == null)
				{
					instance.Channel = Channel.DeserializeLengthDelimited(stream);
				}
				else
				{
					Channel.DeserializeLengthDelimited(stream, instance.Channel);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060051EE RID: 20974 RVA: 0x000FDC36 File Offset: 0x000FBE36
		public void Serialize(Stream stream)
		{
			GetChannelResponse.Serialize(stream, this);
		}

		// Token: 0x060051EF RID: 20975 RVA: 0x000FDC40 File Offset: 0x000FBE40
		public static void Serialize(Stream stream, GetChannelResponse instance)
		{
			if (instance.HasChannel)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Channel.GetSerializedSize());
				Channel.Serialize(stream, instance.Channel);
			}
			if (instance.HasCollectionId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CollectionId));
			}
		}

		// Token: 0x060051F0 RID: 20976 RVA: 0x000FDCA0 File Offset: 0x000FBEA0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasChannel)
			{
				num += 1U;
				uint serializedSize = this.Channel.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasCollectionId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.CollectionId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x04001A4B RID: 6731
		public bool HasChannel;

		// Token: 0x04001A4C RID: 6732
		private Channel _Channel;

		// Token: 0x04001A4D RID: 6733
		public bool HasCollectionId;

		// Token: 0x04001A4E RID: 6734
		private string _CollectionId;
	}
}
