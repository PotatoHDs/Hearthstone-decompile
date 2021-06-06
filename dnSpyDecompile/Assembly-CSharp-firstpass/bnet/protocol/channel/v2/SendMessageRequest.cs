using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000456 RID: 1110
	public class SendMessageRequest : IProtoBuf
	{
		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x06004BCA RID: 19402 RVA: 0x000EBC5D File Offset: 0x000E9E5D
		// (set) Token: 0x06004BCB RID: 19403 RVA: 0x000EBC65 File Offset: 0x000E9E65
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

		// Token: 0x06004BCC RID: 19404 RVA: 0x000EBC78 File Offset: 0x000E9E78
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x06004BCD RID: 19405 RVA: 0x000EBC81 File Offset: 0x000E9E81
		// (set) Token: 0x06004BCE RID: 19406 RVA: 0x000EBC89 File Offset: 0x000E9E89
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

		// Token: 0x06004BCF RID: 19407 RVA: 0x000EBC9C File Offset: 0x000E9E9C
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x06004BD0 RID: 19408 RVA: 0x000EBCA5 File Offset: 0x000E9EA5
		// (set) Token: 0x06004BD1 RID: 19409 RVA: 0x000EBCAD File Offset: 0x000E9EAD
		public SendMessageOptions Options
		{
			get
			{
				return this._Options;
			}
			set
			{
				this._Options = value;
				this.HasOptions = (value != null);
			}
		}

		// Token: 0x06004BD2 RID: 19410 RVA: 0x000EBCC0 File Offset: 0x000E9EC0
		public void SetOptions(SendMessageOptions val)
		{
			this.Options = val;
		}

		// Token: 0x06004BD3 RID: 19411 RVA: 0x000EBCCC File Offset: 0x000E9ECC
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
			if (this.HasOptions)
			{
				num ^= this.Options.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004BD4 RID: 19412 RVA: 0x000EBD28 File Offset: 0x000E9F28
		public override bool Equals(object obj)
		{
			SendMessageRequest sendMessageRequest = obj as SendMessageRequest;
			return sendMessageRequest != null && this.HasAgentId == sendMessageRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(sendMessageRequest.AgentId)) && this.HasChannelId == sendMessageRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(sendMessageRequest.ChannelId)) && this.HasOptions == sendMessageRequest.HasOptions && (!this.HasOptions || this.Options.Equals(sendMessageRequest.Options));
		}

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x06004BD5 RID: 19413 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004BD6 RID: 19414 RVA: 0x000EBDC3 File Offset: 0x000E9FC3
		public static SendMessageRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendMessageRequest>(bs, 0, -1);
		}

		// Token: 0x06004BD7 RID: 19415 RVA: 0x000EBDCD File Offset: 0x000E9FCD
		public void Deserialize(Stream stream)
		{
			SendMessageRequest.Deserialize(stream, this);
		}

		// Token: 0x06004BD8 RID: 19416 RVA: 0x000EBDD7 File Offset: 0x000E9FD7
		public static SendMessageRequest Deserialize(Stream stream, SendMessageRequest instance)
		{
			return SendMessageRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004BD9 RID: 19417 RVA: 0x000EBDE4 File Offset: 0x000E9FE4
		public static SendMessageRequest DeserializeLengthDelimited(Stream stream)
		{
			SendMessageRequest sendMessageRequest = new SendMessageRequest();
			SendMessageRequest.DeserializeLengthDelimited(stream, sendMessageRequest);
			return sendMessageRequest;
		}

		// Token: 0x06004BDA RID: 19418 RVA: 0x000EBE00 File Offset: 0x000EA000
		public static SendMessageRequest DeserializeLengthDelimited(Stream stream, SendMessageRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SendMessageRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004BDB RID: 19419 RVA: 0x000EBE28 File Offset: 0x000EA028
		public static SendMessageRequest Deserialize(Stream stream, SendMessageRequest instance, long limit)
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
						if (num != 26)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else if (instance.Options == null)
						{
							instance.Options = SendMessageOptions.DeserializeLengthDelimited(stream);
						}
						else
						{
							SendMessageOptions.DeserializeLengthDelimited(stream, instance.Options);
						}
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

		// Token: 0x06004BDC RID: 19420 RVA: 0x000EBF2A File Offset: 0x000EA12A
		public void Serialize(Stream stream)
		{
			SendMessageRequest.Serialize(stream, this);
		}

		// Token: 0x06004BDD RID: 19421 RVA: 0x000EBF34 File Offset: 0x000EA134
		public static void Serialize(Stream stream, SendMessageRequest instance)
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
			if (instance.HasOptions)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				SendMessageOptions.Serialize(stream, instance.Options);
			}
		}

		// Token: 0x06004BDE RID: 19422 RVA: 0x000EBFC8 File Offset: 0x000EA1C8
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
			if (this.HasOptions)
			{
				num += 1U;
				uint serializedSize3 = this.Options.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		// Token: 0x040018C0 RID: 6336
		public bool HasAgentId;

		// Token: 0x040018C1 RID: 6337
		private GameAccountHandle _AgentId;

		// Token: 0x040018C2 RID: 6338
		public bool HasChannelId;

		// Token: 0x040018C3 RID: 6339
		private ChannelId _ChannelId;

		// Token: 0x040018C4 RID: 6340
		public bool HasOptions;

		// Token: 0x040018C5 RID: 6341
		private SendMessageOptions _Options;
	}
}
