using System;
using System.IO;

namespace bnet.protocol.friends.v1
{
	// Token: 0x0200041F RID: 1055
	public class DeclineInvitationRequest : IProtoBuf
	{
		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x0600468C RID: 18060 RVA: 0x000DD23A File Offset: 0x000DB43A
		// (set) Token: 0x0600468D RID: 18061 RVA: 0x000DD242 File Offset: 0x000DB442
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

		// Token: 0x0600468E RID: 18062 RVA: 0x000DD255 File Offset: 0x000DB455
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x0600468F RID: 18063 RVA: 0x000DD25E File Offset: 0x000DB45E
		// (set) Token: 0x06004690 RID: 18064 RVA: 0x000DD266 File Offset: 0x000DB466
		public ulong InvitationId { get; set; }

		// Token: 0x06004691 RID: 18065 RVA: 0x000DD26F File Offset: 0x000DB46F
		public void SetInvitationId(ulong val)
		{
			this.InvitationId = val;
		}

		// Token: 0x06004692 RID: 18066 RVA: 0x000DD278 File Offset: 0x000DB478
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			return num ^ this.InvitationId.GetHashCode();
		}

		// Token: 0x06004693 RID: 18067 RVA: 0x000DD2BC File Offset: 0x000DB4BC
		public override bool Equals(object obj)
		{
			DeclineInvitationRequest declineInvitationRequest = obj as DeclineInvitationRequest;
			return declineInvitationRequest != null && this.HasAgentId == declineInvitationRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(declineInvitationRequest.AgentId)) && this.InvitationId.Equals(declineInvitationRequest.InvitationId);
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06004694 RID: 18068 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004695 RID: 18069 RVA: 0x000DD319 File Offset: 0x000DB519
		public static DeclineInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<DeclineInvitationRequest>(bs, 0, -1);
		}

		// Token: 0x06004696 RID: 18070 RVA: 0x000DD323 File Offset: 0x000DB523
		public void Deserialize(Stream stream)
		{
			DeclineInvitationRequest.Deserialize(stream, this);
		}

		// Token: 0x06004697 RID: 18071 RVA: 0x000DD32D File Offset: 0x000DB52D
		public static DeclineInvitationRequest Deserialize(Stream stream, DeclineInvitationRequest instance)
		{
			return DeclineInvitationRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004698 RID: 18072 RVA: 0x000DD338 File Offset: 0x000DB538
		public static DeclineInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			DeclineInvitationRequest declineInvitationRequest = new DeclineInvitationRequest();
			DeclineInvitationRequest.DeserializeLengthDelimited(stream, declineInvitationRequest);
			return declineInvitationRequest;
		}

		// Token: 0x06004699 RID: 18073 RVA: 0x000DD354 File Offset: 0x000DB554
		public static DeclineInvitationRequest DeserializeLengthDelimited(Stream stream, DeclineInvitationRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeclineInvitationRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600469A RID: 18074 RVA: 0x000DD37C File Offset: 0x000DB57C
		public static DeclineInvitationRequest Deserialize(Stream stream, DeclineInvitationRequest instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
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

		// Token: 0x0600469B RID: 18075 RVA: 0x000DD435 File Offset: 0x000DB635
		public void Serialize(Stream stream)
		{
			DeclineInvitationRequest.Serialize(stream, this);
		}

		// Token: 0x0600469C RID: 18076 RVA: 0x000DD440 File Offset: 0x000DB640
		public static void Serialize(Stream stream, DeclineInvitationRequest instance)
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
		}

		// Token: 0x0600469D RID: 18077 RVA: 0x000DD494 File Offset: 0x000DB694
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
			return num + 1U;
		}

		// Token: 0x040017A3 RID: 6051
		public bool HasAgentId;

		// Token: 0x040017A4 RID: 6052
		private EntityId _AgentId;
	}
}
