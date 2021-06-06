using System;
using System.IO;

namespace bnet.protocol.friends.v1
{
	// Token: 0x0200041E RID: 1054
	public class AcceptInvitationRequest : IProtoBuf
	{
		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06004676 RID: 18038 RVA: 0x000DCEBB File Offset: 0x000DB0BB
		// (set) Token: 0x06004677 RID: 18039 RVA: 0x000DCEC3 File Offset: 0x000DB0C3
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

		// Token: 0x06004678 RID: 18040 RVA: 0x000DCED6 File Offset: 0x000DB0D6
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06004679 RID: 18041 RVA: 0x000DCEDF File Offset: 0x000DB0DF
		// (set) Token: 0x0600467A RID: 18042 RVA: 0x000DCEE7 File Offset: 0x000DB0E7
		public ulong InvitationId { get; set; }

		// Token: 0x0600467B RID: 18043 RVA: 0x000DCEF0 File Offset: 0x000DB0F0
		public void SetInvitationId(ulong val)
		{
			this.InvitationId = val;
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x0600467C RID: 18044 RVA: 0x000DCEF9 File Offset: 0x000DB0F9
		// (set) Token: 0x0600467D RID: 18045 RVA: 0x000DCF01 File Offset: 0x000DB101
		public AcceptInvitationOptions Options
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

		// Token: 0x0600467E RID: 18046 RVA: 0x000DCF14 File Offset: 0x000DB114
		public void SetOptions(AcceptInvitationOptions val)
		{
			this.Options = val;
		}

		// Token: 0x0600467F RID: 18047 RVA: 0x000DCF20 File Offset: 0x000DB120
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			num ^= this.InvitationId.GetHashCode();
			if (this.HasOptions)
			{
				num ^= this.Options.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004680 RID: 18048 RVA: 0x000DCF78 File Offset: 0x000DB178
		public override bool Equals(object obj)
		{
			AcceptInvitationRequest acceptInvitationRequest = obj as AcceptInvitationRequest;
			return acceptInvitationRequest != null && this.HasAgentId == acceptInvitationRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(acceptInvitationRequest.AgentId)) && this.InvitationId.Equals(acceptInvitationRequest.InvitationId) && this.HasOptions == acceptInvitationRequest.HasOptions && (!this.HasOptions || this.Options.Equals(acceptInvitationRequest.Options));
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06004681 RID: 18049 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004682 RID: 18050 RVA: 0x000DD000 File Offset: 0x000DB200
		public static AcceptInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AcceptInvitationRequest>(bs, 0, -1);
		}

		// Token: 0x06004683 RID: 18051 RVA: 0x000DD00A File Offset: 0x000DB20A
		public void Deserialize(Stream stream)
		{
			AcceptInvitationRequest.Deserialize(stream, this);
		}

		// Token: 0x06004684 RID: 18052 RVA: 0x000DD014 File Offset: 0x000DB214
		public static AcceptInvitationRequest Deserialize(Stream stream, AcceptInvitationRequest instance)
		{
			return AcceptInvitationRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004685 RID: 18053 RVA: 0x000DD020 File Offset: 0x000DB220
		public static AcceptInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			AcceptInvitationRequest acceptInvitationRequest = new AcceptInvitationRequest();
			AcceptInvitationRequest.DeserializeLengthDelimited(stream, acceptInvitationRequest);
			return acceptInvitationRequest;
		}

		// Token: 0x06004686 RID: 18054 RVA: 0x000DD03C File Offset: 0x000DB23C
		public static AcceptInvitationRequest DeserializeLengthDelimited(Stream stream, AcceptInvitationRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AcceptInvitationRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004687 RID: 18055 RVA: 0x000DD064 File Offset: 0x000DB264
		public static AcceptInvitationRequest Deserialize(Stream stream, AcceptInvitationRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					if (num != 25)
					{
						if (num != 34)
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
							instance.Options = AcceptInvitationOptions.DeserializeLengthDelimited(stream);
						}
						else
						{
							AcceptInvitationOptions.DeserializeLengthDelimited(stream, instance.Options);
						}
					}
					else
					{
						instance.InvitationId = binaryReader.ReadUInt64();
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

		// Token: 0x06004688 RID: 18056 RVA: 0x000DD153 File Offset: 0x000DB353
		public void Serialize(Stream stream)
		{
			AcceptInvitationRequest.Serialize(stream, this);
		}

		// Token: 0x06004689 RID: 18057 RVA: 0x000DD15C File Offset: 0x000DB35C
		public static void Serialize(Stream stream, AcceptInvitationRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			stream.WriteByte(25);
			binaryWriter.Write(instance.InvitationId);
			if (instance.HasOptions)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				AcceptInvitationOptions.Serialize(stream, instance.Options);
			}
		}

		// Token: 0x0600468A RID: 18058 RVA: 0x000DD1DC File Offset: 0x000DB3DC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			num += 8U;
			if (this.HasOptions)
			{
				num += 1U;
				uint serializedSize2 = this.Options.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num + 1U;
		}

		// Token: 0x0400179E RID: 6046
		public bool HasAgentId;

		// Token: 0x0400179F RID: 6047
		private EntityId _AgentId;

		// Token: 0x040017A1 RID: 6049
		public bool HasOptions;

		// Token: 0x040017A2 RID: 6050
		private AcceptInvitationOptions _Options;
	}
}
