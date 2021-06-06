using System;
using System.IO;
using bnet.protocol.channel.v1;

namespace bnet.protocol.channel.v2.server
{
	// Token: 0x02000494 RID: 1172
	public class CreateChannelResponse : IProtoBuf
	{
		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x060051A3 RID: 20899 RVA: 0x000FD14B File Offset: 0x000FB34B
		// (set) Token: 0x060051A4 RID: 20900 RVA: 0x000FD153 File Offset: 0x000FB353
		public ChannelId ChannelId
		{
			get
			{
				return this._ChannelId;
			}
			set
			{
				this._ChannelId = value;
				this.HasChannelId = (value != null);
			}
		}

		// Token: 0x060051A5 RID: 20901 RVA: 0x000FD166 File Offset: 0x000FB366
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x060051A6 RID: 20902 RVA: 0x000FD170 File Offset: 0x000FB370
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060051A7 RID: 20903 RVA: 0x000FD1A0 File Offset: 0x000FB3A0
		public override bool Equals(object obj)
		{
			CreateChannelResponse createChannelResponse = obj as CreateChannelResponse;
			return createChannelResponse != null && this.HasChannelId == createChannelResponse.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(createChannelResponse.ChannelId));
		}

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x060051A8 RID: 20904 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060051A9 RID: 20905 RVA: 0x000FD1E5 File Offset: 0x000FB3E5
		public static CreateChannelResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateChannelResponse>(bs, 0, -1);
		}

		// Token: 0x060051AA RID: 20906 RVA: 0x000FD1EF File Offset: 0x000FB3EF
		public void Deserialize(Stream stream)
		{
			CreateChannelResponse.Deserialize(stream, this);
		}

		// Token: 0x060051AB RID: 20907 RVA: 0x000FD1F9 File Offset: 0x000FB3F9
		public static CreateChannelResponse Deserialize(Stream stream, CreateChannelResponse instance)
		{
			return CreateChannelResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060051AC RID: 20908 RVA: 0x000FD204 File Offset: 0x000FB404
		public static CreateChannelResponse DeserializeLengthDelimited(Stream stream)
		{
			CreateChannelResponse createChannelResponse = new CreateChannelResponse();
			CreateChannelResponse.DeserializeLengthDelimited(stream, createChannelResponse);
			return createChannelResponse;
		}

		// Token: 0x060051AD RID: 20909 RVA: 0x000FD220 File Offset: 0x000FB420
		public static CreateChannelResponse DeserializeLengthDelimited(Stream stream, CreateChannelResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateChannelResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060051AE RID: 20910 RVA: 0x000FD248 File Offset: 0x000FB448
		public static CreateChannelResponse Deserialize(Stream stream, CreateChannelResponse instance, long limit)
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
					if (instance.ChannelId == null)
					{
						instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
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

		// Token: 0x060051AF RID: 20911 RVA: 0x000FD2E2 File Offset: 0x000FB4E2
		public void Serialize(Stream stream)
		{
			CreateChannelResponse.Serialize(stream, this);
		}

		// Token: 0x060051B0 RID: 20912 RVA: 0x000FD2EB File Offset: 0x000FB4EB
		public static void Serialize(Stream stream, CreateChannelResponse instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
		}

		// Token: 0x060051B1 RID: 20913 RVA: 0x000FD31C File Offset: 0x000FB51C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize = this.ChannelId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001A3D RID: 6717
		public bool HasChannelId;

		// Token: 0x04001A3E RID: 6718
		private ChannelId _ChannelId;
	}
}
