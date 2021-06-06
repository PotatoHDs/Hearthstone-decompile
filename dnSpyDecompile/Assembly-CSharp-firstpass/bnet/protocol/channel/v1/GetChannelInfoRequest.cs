using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004C4 RID: 1220
	public class GetChannelInfoRequest : IProtoBuf
	{
		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x06005566 RID: 21862 RVA: 0x0010636B File Offset: 0x0010456B
		// (set) Token: 0x06005567 RID: 21863 RVA: 0x00106373 File Offset: 0x00104573
		public EntityId AgentId
		{
			get
			{
				return this._AgentId;
			}
			set
			{
				this._AgentId = value;
				this.HasAgentId = (value != null);
			}
		}

		// Token: 0x06005568 RID: 21864 RVA: 0x00106386 File Offset: 0x00104586
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x06005569 RID: 21865 RVA: 0x0010638F File Offset: 0x0010458F
		// (set) Token: 0x0600556A RID: 21866 RVA: 0x00106397 File Offset: 0x00104597
		public EntityId ChannelId { get; set; }

		// Token: 0x0600556B RID: 21867 RVA: 0x001063A0 File Offset: 0x001045A0
		public void SetChannelId(EntityId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x0600556C RID: 21868 RVA: 0x001063AC File Offset: 0x001045AC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			return num ^ this.ChannelId.GetHashCode();
		}

		// Token: 0x0600556D RID: 21869 RVA: 0x001063EC File Offset: 0x001045EC
		public override bool Equals(object obj)
		{
			GetChannelInfoRequest getChannelInfoRequest = obj as GetChannelInfoRequest;
			return getChannelInfoRequest != null && this.HasAgentId == getChannelInfoRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(getChannelInfoRequest.AgentId)) && this.ChannelId.Equals(getChannelInfoRequest.ChannelId);
		}

		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x0600556E RID: 21870 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600556F RID: 21871 RVA: 0x00106446 File Offset: 0x00104646
		public static GetChannelInfoRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetChannelInfoRequest>(bs, 0, -1);
		}

		// Token: 0x06005570 RID: 21872 RVA: 0x00106450 File Offset: 0x00104650
		public void Deserialize(Stream stream)
		{
			GetChannelInfoRequest.Deserialize(stream, this);
		}

		// Token: 0x06005571 RID: 21873 RVA: 0x0010645A File Offset: 0x0010465A
		public static GetChannelInfoRequest Deserialize(Stream stream, GetChannelInfoRequest instance)
		{
			return GetChannelInfoRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005572 RID: 21874 RVA: 0x00106468 File Offset: 0x00104668
		public static GetChannelInfoRequest DeserializeLengthDelimited(Stream stream)
		{
			GetChannelInfoRequest getChannelInfoRequest = new GetChannelInfoRequest();
			GetChannelInfoRequest.DeserializeLengthDelimited(stream, getChannelInfoRequest);
			return getChannelInfoRequest;
		}

		// Token: 0x06005573 RID: 21875 RVA: 0x00106484 File Offset: 0x00104684
		public static GetChannelInfoRequest DeserializeLengthDelimited(Stream stream, GetChannelInfoRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetChannelInfoRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005574 RID: 21876 RVA: 0x001064AC File Offset: 0x001046AC
		public static GetChannelInfoRequest Deserialize(Stream stream, GetChannelInfoRequest instance, long limit)
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
					else if (instance.ChannelId == null)
					{
						instance.ChannelId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
				}
				else if (instance.AgentId == null)
				{
					instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005575 RID: 21877 RVA: 0x0010657E File Offset: 0x0010477E
		public void Serialize(Stream stream)
		{
			GetChannelInfoRequest.Serialize(stream, this);
		}

		// Token: 0x06005576 RID: 21878 RVA: 0x00106588 File Offset: 0x00104788
		public static void Serialize(Stream stream, GetChannelInfoRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.ChannelId == null)
			{
				throw new ArgumentNullException("ChannelId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
			EntityId.Serialize(stream, instance.ChannelId);
		}

		// Token: 0x06005577 RID: 21879 RVA: 0x00106600 File Offset: 0x00104800
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = this.ChannelId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			return num + 1U;
		}

		// Token: 0x04001AF8 RID: 6904
		public bool HasAgentId;

		// Token: 0x04001AF9 RID: 6905
		private EntityId _AgentId;
	}
}
