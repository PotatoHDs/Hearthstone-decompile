using System;
using System.IO;
using bnet.protocol.channel.v1;

namespace bnet.protocol.channel.v2.server
{
	// Token: 0x0200049C RID: 1180
	public class UnsubscribeRequest : IProtoBuf
	{
		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x0600523F RID: 21055 RVA: 0x000FE867 File Offset: 0x000FCA67
		// (set) Token: 0x06005240 RID: 21056 RVA: 0x000FE86F File Offset: 0x000FCA6F
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

		// Token: 0x06005241 RID: 21057 RVA: 0x000FE882 File Offset: 0x000FCA82
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x06005242 RID: 21058 RVA: 0x000FE88C File Offset: 0x000FCA8C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005243 RID: 21059 RVA: 0x000FE8BC File Offset: 0x000FCABC
		public override bool Equals(object obj)
		{
			UnsubscribeRequest unsubscribeRequest = obj as UnsubscribeRequest;
			return unsubscribeRequest != null && this.HasChannelId == unsubscribeRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(unsubscribeRequest.ChannelId));
		}

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06005244 RID: 21060 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005245 RID: 21061 RVA: 0x000FE901 File Offset: 0x000FCB01
		public static UnsubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnsubscribeRequest>(bs, 0, -1);
		}

		// Token: 0x06005246 RID: 21062 RVA: 0x000FE90B File Offset: 0x000FCB0B
		public void Deserialize(Stream stream)
		{
			UnsubscribeRequest.Deserialize(stream, this);
		}

		// Token: 0x06005247 RID: 21063 RVA: 0x000FE915 File Offset: 0x000FCB15
		public static UnsubscribeRequest Deserialize(Stream stream, UnsubscribeRequest instance)
		{
			return UnsubscribeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005248 RID: 21064 RVA: 0x000FE920 File Offset: 0x000FCB20
		public static UnsubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			UnsubscribeRequest unsubscribeRequest = new UnsubscribeRequest();
			UnsubscribeRequest.DeserializeLengthDelimited(stream, unsubscribeRequest);
			return unsubscribeRequest;
		}

		// Token: 0x06005249 RID: 21065 RVA: 0x000FE93C File Offset: 0x000FCB3C
		public static UnsubscribeRequest DeserializeLengthDelimited(Stream stream, UnsubscribeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UnsubscribeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600524A RID: 21066 RVA: 0x000FE964 File Offset: 0x000FCB64
		public static UnsubscribeRequest Deserialize(Stream stream, UnsubscribeRequest instance, long limit)
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

		// Token: 0x0600524B RID: 21067 RVA: 0x000FE9FE File Offset: 0x000FCBFE
		public void Serialize(Stream stream)
		{
			UnsubscribeRequest.Serialize(stream, this);
		}

		// Token: 0x0600524C RID: 21068 RVA: 0x000FEA07 File Offset: 0x000FCC07
		public static void Serialize(Stream stream, UnsubscribeRequest instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
		}

		// Token: 0x0600524D RID: 21069 RVA: 0x000FEA38 File Offset: 0x000FCC38
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

		// Token: 0x04001A5C RID: 6748
		public bool HasChannelId;

		// Token: 0x04001A5D RID: 6749
		private ChannelId _ChannelId;
	}
}
