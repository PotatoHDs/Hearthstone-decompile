using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000451 RID: 1105
	public class SubscribeRequest : IProtoBuf
	{
		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x06004B64 RID: 19300 RVA: 0x000EABA2 File Offset: 0x000E8DA2
		// (set) Token: 0x06004B65 RID: 19301 RVA: 0x000EABAA File Offset: 0x000E8DAA
		public GameAccountHandle AgentId
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

		// Token: 0x06004B66 RID: 19302 RVA: 0x000EABBD File Offset: 0x000E8DBD
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06004B67 RID: 19303 RVA: 0x000EABC6 File Offset: 0x000E8DC6
		// (set) Token: 0x06004B68 RID: 19304 RVA: 0x000EABCE File Offset: 0x000E8DCE
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

		// Token: 0x06004B69 RID: 19305 RVA: 0x000EABE1 File Offset: 0x000E8DE1
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x06004B6A RID: 19306 RVA: 0x000EABEC File Offset: 0x000E8DEC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004B6B RID: 19307 RVA: 0x000EAC34 File Offset: 0x000E8E34
		public override bool Equals(object obj)
		{
			SubscribeRequest subscribeRequest = obj as SubscribeRequest;
			return subscribeRequest != null && this.HasAgentId == subscribeRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(subscribeRequest.AgentId)) && this.HasChannelId == subscribeRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(subscribeRequest.ChannelId));
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x06004B6C RID: 19308 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004B6D RID: 19309 RVA: 0x000EACA4 File Offset: 0x000E8EA4
		public static SubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeRequest>(bs, 0, -1);
		}

		// Token: 0x06004B6E RID: 19310 RVA: 0x000EACAE File Offset: 0x000E8EAE
		public void Deserialize(Stream stream)
		{
			SubscribeRequest.Deserialize(stream, this);
		}

		// Token: 0x06004B6F RID: 19311 RVA: 0x000EACB8 File Offset: 0x000E8EB8
		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance)
		{
			return SubscribeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004B70 RID: 19312 RVA: 0x000EACC4 File Offset: 0x000E8EC4
		public static SubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			SubscribeRequest subscribeRequest = new SubscribeRequest();
			SubscribeRequest.DeserializeLengthDelimited(stream, subscribeRequest);
			return subscribeRequest;
		}

		// Token: 0x06004B71 RID: 19313 RVA: 0x000EACE0 File Offset: 0x000E8EE0
		public static SubscribeRequest DeserializeLengthDelimited(Stream stream, SubscribeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004B72 RID: 19314 RVA: 0x000EAD08 File Offset: 0x000E8F08
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
						instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
				}
				else if (instance.AgentId == null)
				{
					instance.AgentId = GameAccountHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountHandle.DeserializeLengthDelimited(stream, instance.AgentId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004B73 RID: 19315 RVA: 0x000EADDA File Offset: 0x000E8FDA
		public void Serialize(Stream stream)
		{
			SubscribeRequest.Serialize(stream, this);
		}

		// Token: 0x06004B74 RID: 19316 RVA: 0x000EADE4 File Offset: 0x000E8FE4
		public static void Serialize(Stream stream, SubscribeRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
		}

		// Token: 0x06004B75 RID: 19317 RVA: 0x000EAE4C File Offset: 0x000E904C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize2 = this.ChannelId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x040018AB RID: 6315
		public bool HasAgentId;

		// Token: 0x040018AC RID: 6316
		private GameAccountHandle _AgentId;

		// Token: 0x040018AD RID: 6317
		public bool HasChannelId;

		// Token: 0x040018AE RID: 6318
		private ChannelId _ChannelId;
	}
}
