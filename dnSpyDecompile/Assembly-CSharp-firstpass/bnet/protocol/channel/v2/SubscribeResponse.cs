using System;
using System.IO;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000452 RID: 1106
	public class SubscribeResponse : IProtoBuf
	{
		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x06004B77 RID: 19319 RVA: 0x000EAEA2 File Offset: 0x000E90A2
		// (set) Token: 0x06004B78 RID: 19320 RVA: 0x000EAEAA File Offset: 0x000E90AA
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

		// Token: 0x06004B79 RID: 19321 RVA: 0x000EAEBD File Offset: 0x000E90BD
		public void SetChannel(Channel val)
		{
			this.Channel = val;
		}

		// Token: 0x06004B7A RID: 19322 RVA: 0x000EAEC8 File Offset: 0x000E90C8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannel)
			{
				num ^= this.Channel.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004B7B RID: 19323 RVA: 0x000EAEF8 File Offset: 0x000E90F8
		public override bool Equals(object obj)
		{
			SubscribeResponse subscribeResponse = obj as SubscribeResponse;
			return subscribeResponse != null && this.HasChannel == subscribeResponse.HasChannel && (!this.HasChannel || this.Channel.Equals(subscribeResponse.Channel));
		}

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06004B7C RID: 19324 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004B7D RID: 19325 RVA: 0x000EAF3D File Offset: 0x000E913D
		public static SubscribeResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeResponse>(bs, 0, -1);
		}

		// Token: 0x06004B7E RID: 19326 RVA: 0x000EAF47 File Offset: 0x000E9147
		public void Deserialize(Stream stream)
		{
			SubscribeResponse.Deserialize(stream, this);
		}

		// Token: 0x06004B7F RID: 19327 RVA: 0x000EAF51 File Offset: 0x000E9151
		public static SubscribeResponse Deserialize(Stream stream, SubscribeResponse instance)
		{
			return SubscribeResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004B80 RID: 19328 RVA: 0x000EAF5C File Offset: 0x000E915C
		public static SubscribeResponse DeserializeLengthDelimited(Stream stream)
		{
			SubscribeResponse subscribeResponse = new SubscribeResponse();
			SubscribeResponse.DeserializeLengthDelimited(stream, subscribeResponse);
			return subscribeResponse;
		}

		// Token: 0x06004B81 RID: 19329 RVA: 0x000EAF78 File Offset: 0x000E9178
		public static SubscribeResponse DeserializeLengthDelimited(Stream stream, SubscribeResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06004B82 RID: 19330 RVA: 0x000EAFA0 File Offset: 0x000E91A0
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

		// Token: 0x06004B83 RID: 19331 RVA: 0x000EB03A File Offset: 0x000E923A
		public void Serialize(Stream stream)
		{
			SubscribeResponse.Serialize(stream, this);
		}

		// Token: 0x06004B84 RID: 19332 RVA: 0x000EB043 File Offset: 0x000E9243
		public static void Serialize(Stream stream, SubscribeResponse instance)
		{
			if (instance.HasChannel)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Channel.GetSerializedSize());
				Channel.Serialize(stream, instance.Channel);
			}
		}

		// Token: 0x06004B85 RID: 19333 RVA: 0x000EB074 File Offset: 0x000E9274
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

		// Token: 0x040018AF RID: 6319
		public bool HasChannel;

		// Token: 0x040018B0 RID: 6320
		private Channel _Channel;
	}
}
