using System;
using System.IO;

namespace bnet.protocol.channel.v2
{
	// Token: 0x0200044D RID: 1101
	public class GetChannelResponse : IProtoBuf
	{
		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06004B14 RID: 19220 RVA: 0x000E9F1A File Offset: 0x000E811A
		// (set) Token: 0x06004B15 RID: 19221 RVA: 0x000E9F22 File Offset: 0x000E8122
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

		// Token: 0x06004B16 RID: 19222 RVA: 0x000E9F35 File Offset: 0x000E8135
		public void SetChannel(Channel val)
		{
			this.Channel = val;
		}

		// Token: 0x06004B17 RID: 19223 RVA: 0x000E9F40 File Offset: 0x000E8140
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannel)
			{
				num ^= this.Channel.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004B18 RID: 19224 RVA: 0x000E9F70 File Offset: 0x000E8170
		public override bool Equals(object obj)
		{
			GetChannelResponse getChannelResponse = obj as GetChannelResponse;
			return getChannelResponse != null && this.HasChannel == getChannelResponse.HasChannel && (!this.HasChannel || this.Channel.Equals(getChannelResponse.Channel));
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06004B19 RID: 19225 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004B1A RID: 19226 RVA: 0x000E9FB5 File Offset: 0x000E81B5
		public static GetChannelResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetChannelResponse>(bs, 0, -1);
		}

		// Token: 0x06004B1B RID: 19227 RVA: 0x000E9FBF File Offset: 0x000E81BF
		public void Deserialize(Stream stream)
		{
			GetChannelResponse.Deserialize(stream, this);
		}

		// Token: 0x06004B1C RID: 19228 RVA: 0x000E9FC9 File Offset: 0x000E81C9
		public static GetChannelResponse Deserialize(Stream stream, GetChannelResponse instance)
		{
			return GetChannelResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004B1D RID: 19229 RVA: 0x000E9FD4 File Offset: 0x000E81D4
		public static GetChannelResponse DeserializeLengthDelimited(Stream stream)
		{
			GetChannelResponse getChannelResponse = new GetChannelResponse();
			GetChannelResponse.DeserializeLengthDelimited(stream, getChannelResponse);
			return getChannelResponse;
		}

		// Token: 0x06004B1E RID: 19230 RVA: 0x000E9FF0 File Offset: 0x000E81F0
		public static GetChannelResponse DeserializeLengthDelimited(Stream stream, GetChannelResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetChannelResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06004B1F RID: 19231 RVA: 0x000EA018 File Offset: 0x000E8218
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
				else if (num == 10)
				{
					if (instance.Channel == null)
					{
						instance.Channel = Channel.DeserializeLengthDelimited(stream);
					}
					else
					{
						Channel.DeserializeLengthDelimited(stream, instance.Channel);
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

		// Token: 0x06004B20 RID: 19232 RVA: 0x000EA0B2 File Offset: 0x000E82B2
		public void Serialize(Stream stream)
		{
			GetChannelResponse.Serialize(stream, this);
		}

		// Token: 0x06004B21 RID: 19233 RVA: 0x000EA0BB File Offset: 0x000E82BB
		public static void Serialize(Stream stream, GetChannelResponse instance)
		{
			if (instance.HasChannel)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Channel.GetSerializedSize());
				Channel.Serialize(stream, instance.Channel);
			}
		}

		// Token: 0x06004B22 RID: 19234 RVA: 0x000EA0EC File Offset: 0x000E82EC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasChannel)
			{
				num += 1U;
				uint serializedSize = this.Channel.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x0400189C RID: 6300
		public bool HasChannel;

		// Token: 0x0400189D RID: 6301
		private Channel _Channel;
	}
}
