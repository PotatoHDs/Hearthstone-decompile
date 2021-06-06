using System;
using System.IO;

namespace bnet.protocol.channel.v2.server
{
	// Token: 0x0200049B RID: 1179
	public class SubscribeResponse : IProtoBuf
	{
		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x0600522F RID: 21039 RVA: 0x000FE663 File Offset: 0x000FC863
		// (set) Token: 0x06005230 RID: 21040 RVA: 0x000FE66B File Offset: 0x000FC86B
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

		// Token: 0x06005231 RID: 21041 RVA: 0x000FE67E File Offset: 0x000FC87E
		public void SetChannel(Channel val)
		{
			this.Channel = val;
		}

		// Token: 0x06005232 RID: 21042 RVA: 0x000FE688 File Offset: 0x000FC888
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannel)
			{
				num ^= this.Channel.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005233 RID: 21043 RVA: 0x000FE6B8 File Offset: 0x000FC8B8
		public override bool Equals(object obj)
		{
			SubscribeResponse subscribeResponse = obj as SubscribeResponse;
			return subscribeResponse != null && this.HasChannel == subscribeResponse.HasChannel && (!this.HasChannel || this.Channel.Equals(subscribeResponse.Channel));
		}

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x06005234 RID: 21044 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005235 RID: 21045 RVA: 0x000FE6FD File Offset: 0x000FC8FD
		public static SubscribeResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeResponse>(bs, 0, -1);
		}

		// Token: 0x06005236 RID: 21046 RVA: 0x000FE707 File Offset: 0x000FC907
		public void Deserialize(Stream stream)
		{
			SubscribeResponse.Deserialize(stream, this);
		}

		// Token: 0x06005237 RID: 21047 RVA: 0x000FE711 File Offset: 0x000FC911
		public static SubscribeResponse Deserialize(Stream stream, SubscribeResponse instance)
		{
			return SubscribeResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005238 RID: 21048 RVA: 0x000FE71C File Offset: 0x000FC91C
		public static SubscribeResponse DeserializeLengthDelimited(Stream stream)
		{
			SubscribeResponse subscribeResponse = new SubscribeResponse();
			SubscribeResponse.DeserializeLengthDelimited(stream, subscribeResponse);
			return subscribeResponse;
		}

		// Token: 0x06005239 RID: 21049 RVA: 0x000FE738 File Offset: 0x000FC938
		public static SubscribeResponse DeserializeLengthDelimited(Stream stream, SubscribeResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x0600523A RID: 21050 RVA: 0x000FE760 File Offset: 0x000FC960
		public static SubscribeResponse Deserialize(Stream stream, SubscribeResponse instance, long limit)
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

		// Token: 0x0600523B RID: 21051 RVA: 0x000FE7FA File Offset: 0x000FC9FA
		public void Serialize(Stream stream)
		{
			SubscribeResponse.Serialize(stream, this);
		}

		// Token: 0x0600523C RID: 21052 RVA: 0x000FE803 File Offset: 0x000FCA03
		public static void Serialize(Stream stream, SubscribeResponse instance)
		{
			if (instance.HasChannel)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Channel.GetSerializedSize());
				Channel.Serialize(stream, instance.Channel);
			}
		}

		// Token: 0x0600523D RID: 21053 RVA: 0x000FE834 File Offset: 0x000FCA34
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

		// Token: 0x04001A5A RID: 6746
		public bool HasChannel;

		// Token: 0x04001A5B RID: 6747
		private Channel _Channel;
	}
}
