using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000462 RID: 1122
	public class SendSuggestionRequest : IProtoBuf
	{
		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x06004CCC RID: 19660 RVA: 0x000EE8AE File Offset: 0x000ECAAE
		// (set) Token: 0x06004CCD RID: 19661 RVA: 0x000EE8B6 File Offset: 0x000ECAB6
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

		// Token: 0x06004CCE RID: 19662 RVA: 0x000EE8C9 File Offset: 0x000ECAC9
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06004CCF RID: 19663 RVA: 0x000EE8D2 File Offset: 0x000ECAD2
		// (set) Token: 0x06004CD0 RID: 19664 RVA: 0x000EE8DA File Offset: 0x000ECADA
		public SendSuggestionOptions Options
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

		// Token: 0x06004CD1 RID: 19665 RVA: 0x000EE8ED File Offset: 0x000ECAED
		public void SetOptions(SendSuggestionOptions val)
		{
			this.Options = val;
		}

		// Token: 0x06004CD2 RID: 19666 RVA: 0x000EE8F8 File Offset: 0x000ECAF8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasOptions)
			{
				num ^= this.Options.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004CD3 RID: 19667 RVA: 0x000EE940 File Offset: 0x000ECB40
		public override bool Equals(object obj)
		{
			SendSuggestionRequest sendSuggestionRequest = obj as SendSuggestionRequest;
			return sendSuggestionRequest != null && this.HasAgentId == sendSuggestionRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(sendSuggestionRequest.AgentId)) && this.HasOptions == sendSuggestionRequest.HasOptions && (!this.HasOptions || this.Options.Equals(sendSuggestionRequest.Options));
		}

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x06004CD4 RID: 19668 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004CD5 RID: 19669 RVA: 0x000EE9B0 File Offset: 0x000ECBB0
		public static SendSuggestionRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendSuggestionRequest>(bs, 0, -1);
		}

		// Token: 0x06004CD6 RID: 19670 RVA: 0x000EE9BA File Offset: 0x000ECBBA
		public void Deserialize(Stream stream)
		{
			SendSuggestionRequest.Deserialize(stream, this);
		}

		// Token: 0x06004CD7 RID: 19671 RVA: 0x000EE9C4 File Offset: 0x000ECBC4
		public static SendSuggestionRequest Deserialize(Stream stream, SendSuggestionRequest instance)
		{
			return SendSuggestionRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004CD8 RID: 19672 RVA: 0x000EE9D0 File Offset: 0x000ECBD0
		public static SendSuggestionRequest DeserializeLengthDelimited(Stream stream)
		{
			SendSuggestionRequest sendSuggestionRequest = new SendSuggestionRequest();
			SendSuggestionRequest.DeserializeLengthDelimited(stream, sendSuggestionRequest);
			return sendSuggestionRequest;
		}

		// Token: 0x06004CD9 RID: 19673 RVA: 0x000EE9EC File Offset: 0x000ECBEC
		public static SendSuggestionRequest DeserializeLengthDelimited(Stream stream, SendSuggestionRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SendSuggestionRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004CDA RID: 19674 RVA: 0x000EEA14 File Offset: 0x000ECC14
		public static SendSuggestionRequest Deserialize(Stream stream, SendSuggestionRequest instance, long limit)
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
					else if (instance.Options == null)
					{
						instance.Options = SendSuggestionOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						SendSuggestionOptions.DeserializeLengthDelimited(stream, instance.Options);
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

		// Token: 0x06004CDB RID: 19675 RVA: 0x000EEAE6 File Offset: 0x000ECCE6
		public void Serialize(Stream stream)
		{
			SendSuggestionRequest.Serialize(stream, this);
		}

		// Token: 0x06004CDC RID: 19676 RVA: 0x000EEAF0 File Offset: 0x000ECCF0
		public static void Serialize(Stream stream, SendSuggestionRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasOptions)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				SendSuggestionOptions.Serialize(stream, instance.Options);
			}
		}

		// Token: 0x06004CDD RID: 19677 RVA: 0x000EEB58 File Offset: 0x000ECD58
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasOptions)
			{
				num += 1U;
				uint serializedSize2 = this.Options.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x04001904 RID: 6404
		public bool HasAgentId;

		// Token: 0x04001905 RID: 6405
		private GameAccountHandle _AgentId;

		// Token: 0x04001906 RID: 6406
		public bool HasOptions;

		// Token: 0x04001907 RID: 6407
		private SendSuggestionOptions _Options;
	}
}
