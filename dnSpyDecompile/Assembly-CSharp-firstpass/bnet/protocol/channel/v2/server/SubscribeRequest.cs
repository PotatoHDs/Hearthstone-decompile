using System;
using System.IO;
using bnet.protocol.channel.v1;

namespace bnet.protocol.channel.v2.server
{
	// Token: 0x0200049A RID: 1178
	public class SubscribeRequest : IProtoBuf
	{
		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x0600521F RID: 21023 RVA: 0x000FE45F File Offset: 0x000FC65F
		// (set) Token: 0x06005220 RID: 21024 RVA: 0x000FE467 File Offset: 0x000FC667
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

		// Token: 0x06005221 RID: 21025 RVA: 0x000FE47A File Offset: 0x000FC67A
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x06005222 RID: 21026 RVA: 0x000FE484 File Offset: 0x000FC684
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005223 RID: 21027 RVA: 0x000FE4B4 File Offset: 0x000FC6B4
		public override bool Equals(object obj)
		{
			SubscribeRequest subscribeRequest = obj as SubscribeRequest;
			return subscribeRequest != null && this.HasChannelId == subscribeRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(subscribeRequest.ChannelId));
		}

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x06005224 RID: 21028 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005225 RID: 21029 RVA: 0x000FE4F9 File Offset: 0x000FC6F9
		public static SubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeRequest>(bs, 0, -1);
		}

		// Token: 0x06005226 RID: 21030 RVA: 0x000FE503 File Offset: 0x000FC703
		public void Deserialize(Stream stream)
		{
			SubscribeRequest.Deserialize(stream, this);
		}

		// Token: 0x06005227 RID: 21031 RVA: 0x000FE50D File Offset: 0x000FC70D
		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance)
		{
			return SubscribeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005228 RID: 21032 RVA: 0x000FE518 File Offset: 0x000FC718
		public static SubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			SubscribeRequest subscribeRequest = new SubscribeRequest();
			SubscribeRequest.DeserializeLengthDelimited(stream, subscribeRequest);
			return subscribeRequest;
		}

		// Token: 0x06005229 RID: 21033 RVA: 0x000FE534 File Offset: 0x000FC734
		public static SubscribeRequest DeserializeLengthDelimited(Stream stream, SubscribeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600522A RID: 21034 RVA: 0x000FE55C File Offset: 0x000FC75C
		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance, long limit)
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
				else if (num == 18)
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

		// Token: 0x0600522B RID: 21035 RVA: 0x000FE5F6 File Offset: 0x000FC7F6
		public void Serialize(Stream stream)
		{
			SubscribeRequest.Serialize(stream, this);
		}

		// Token: 0x0600522C RID: 21036 RVA: 0x000FE5FF File Offset: 0x000FC7FF
		public static void Serialize(Stream stream, SubscribeRequest instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
		}

		// Token: 0x0600522D RID: 21037 RVA: 0x000FE630 File Offset: 0x000FC830
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

		// Token: 0x04001A58 RID: 6744
		public bool HasChannelId;

		// Token: 0x04001A59 RID: 6745
		private ChannelId _ChannelId;
	}
}
