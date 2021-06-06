using System;
using System.IO;

namespace bnet.protocol.friends.v1
{
	// Token: 0x02000422 RID: 1058
	public class RevokeAllInvitationsRequest : IProtoBuf
	{
		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x060046C8 RID: 18120 RVA: 0x000DDAFE File Offset: 0x000DBCFE
		// (set) Token: 0x060046C9 RID: 18121 RVA: 0x000DDB06 File Offset: 0x000DBD06
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

		// Token: 0x060046CA RID: 18122 RVA: 0x000DDB19 File Offset: 0x000DBD19
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x060046CB RID: 18123 RVA: 0x000DDB24 File Offset: 0x000DBD24
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060046CC RID: 18124 RVA: 0x000DDB54 File Offset: 0x000DBD54
		public override bool Equals(object obj)
		{
			RevokeAllInvitationsRequest revokeAllInvitationsRequest = obj as RevokeAllInvitationsRequest;
			return revokeAllInvitationsRequest != null && this.HasAgentId == revokeAllInvitationsRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(revokeAllInvitationsRequest.AgentId));
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x060046CD RID: 18125 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060046CE RID: 18126 RVA: 0x000DDB99 File Offset: 0x000DBD99
		public static RevokeAllInvitationsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RevokeAllInvitationsRequest>(bs, 0, -1);
		}

		// Token: 0x060046CF RID: 18127 RVA: 0x000DDBA3 File Offset: 0x000DBDA3
		public void Deserialize(Stream stream)
		{
			RevokeAllInvitationsRequest.Deserialize(stream, this);
		}

		// Token: 0x060046D0 RID: 18128 RVA: 0x000DDBAD File Offset: 0x000DBDAD
		public static RevokeAllInvitationsRequest Deserialize(Stream stream, RevokeAllInvitationsRequest instance)
		{
			return RevokeAllInvitationsRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060046D1 RID: 18129 RVA: 0x000DDBB8 File Offset: 0x000DBDB8
		public static RevokeAllInvitationsRequest DeserializeLengthDelimited(Stream stream)
		{
			RevokeAllInvitationsRequest revokeAllInvitationsRequest = new RevokeAllInvitationsRequest();
			RevokeAllInvitationsRequest.DeserializeLengthDelimited(stream, revokeAllInvitationsRequest);
			return revokeAllInvitationsRequest;
		}

		// Token: 0x060046D2 RID: 18130 RVA: 0x000DDBD4 File Offset: 0x000DBDD4
		public static RevokeAllInvitationsRequest DeserializeLengthDelimited(Stream stream, RevokeAllInvitationsRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RevokeAllInvitationsRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060046D3 RID: 18131 RVA: 0x000DDBFC File Offset: 0x000DBDFC
		public static RevokeAllInvitationsRequest Deserialize(Stream stream, RevokeAllInvitationsRequest instance, long limit)
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
					if (instance.AgentId == null)
					{
						instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
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

		// Token: 0x060046D4 RID: 18132 RVA: 0x000DDC96 File Offset: 0x000DBE96
		public void Serialize(Stream stream)
		{
			RevokeAllInvitationsRequest.Serialize(stream, this);
		}

		// Token: 0x060046D5 RID: 18133 RVA: 0x000DDC9F File Offset: 0x000DBE9F
		public static void Serialize(Stream stream, RevokeAllInvitationsRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
		}

		// Token: 0x060046D6 RID: 18134 RVA: 0x000DDCD0 File Offset: 0x000DBED0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x040017AE RID: 6062
		public bool HasAgentId;

		// Token: 0x040017AF RID: 6063
		private EntityId _AgentId;
	}
}
