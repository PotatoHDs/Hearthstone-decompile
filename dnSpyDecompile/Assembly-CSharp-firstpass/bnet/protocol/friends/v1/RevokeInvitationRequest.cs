using System;
using System.IO;

namespace bnet.protocol.friends.v1
{
	// Token: 0x0200041D RID: 1053
	public class RevokeInvitationRequest : IProtoBuf
	{
		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06004663 RID: 18019 RVA: 0x000DCBE9 File Offset: 0x000DADE9
		// (set) Token: 0x06004664 RID: 18020 RVA: 0x000DCBF1 File Offset: 0x000DADF1
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

		// Token: 0x06004665 RID: 18021 RVA: 0x000DCC04 File Offset: 0x000DAE04
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06004666 RID: 18022 RVA: 0x000DCC0D File Offset: 0x000DAE0D
		// (set) Token: 0x06004667 RID: 18023 RVA: 0x000DCC15 File Offset: 0x000DAE15
		public ulong InvitationId
		{
			get
			{
				return this._InvitationId;
			}
			set
			{
				this._InvitationId = value;
				this.HasInvitationId = true;
			}
		}

		// Token: 0x06004668 RID: 18024 RVA: 0x000DCC25 File Offset: 0x000DAE25
		public void SetInvitationId(ulong val)
		{
			this.InvitationId = val;
		}

		// Token: 0x06004669 RID: 18025 RVA: 0x000DCC30 File Offset: 0x000DAE30
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasInvitationId)
			{
				num ^= this.InvitationId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600466A RID: 18026 RVA: 0x000DCC7C File Offset: 0x000DAE7C
		public override bool Equals(object obj)
		{
			RevokeInvitationRequest revokeInvitationRequest = obj as RevokeInvitationRequest;
			return revokeInvitationRequest != null && this.HasAgentId == revokeInvitationRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(revokeInvitationRequest.AgentId)) && this.HasInvitationId == revokeInvitationRequest.HasInvitationId && (!this.HasInvitationId || this.InvitationId.Equals(revokeInvitationRequest.InvitationId));
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x0600466B RID: 18027 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600466C RID: 18028 RVA: 0x000DCCEF File Offset: 0x000DAEEF
		public static RevokeInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RevokeInvitationRequest>(bs, 0, -1);
		}

		// Token: 0x0600466D RID: 18029 RVA: 0x000DCCF9 File Offset: 0x000DAEF9
		public void Deserialize(Stream stream)
		{
			RevokeInvitationRequest.Deserialize(stream, this);
		}

		// Token: 0x0600466E RID: 18030 RVA: 0x000DCD03 File Offset: 0x000DAF03
		public static RevokeInvitationRequest Deserialize(Stream stream, RevokeInvitationRequest instance)
		{
			return RevokeInvitationRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600466F RID: 18031 RVA: 0x000DCD10 File Offset: 0x000DAF10
		public static RevokeInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			RevokeInvitationRequest revokeInvitationRequest = new RevokeInvitationRequest();
			RevokeInvitationRequest.DeserializeLengthDelimited(stream, revokeInvitationRequest);
			return revokeInvitationRequest;
		}

		// Token: 0x06004670 RID: 18032 RVA: 0x000DCD2C File Offset: 0x000DAF2C
		public static RevokeInvitationRequest DeserializeLengthDelimited(Stream stream, RevokeInvitationRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RevokeInvitationRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004671 RID: 18033 RVA: 0x000DCD54 File Offset: 0x000DAF54
		public static RevokeInvitationRequest Deserialize(Stream stream, RevokeInvitationRequest instance, long limit)
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
					if (num != 17)
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

		// Token: 0x06004672 RID: 18034 RVA: 0x000DCE0D File Offset: 0x000DB00D
		public void Serialize(Stream stream)
		{
			RevokeInvitationRequest.Serialize(stream, this);
		}

		// Token: 0x06004673 RID: 18035 RVA: 0x000DCE18 File Offset: 0x000DB018
		public static void Serialize(Stream stream, RevokeInvitationRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.HasInvitationId)
			{
				stream.WriteByte(17);
				binaryWriter.Write(instance.InvitationId);
			}
		}

		// Token: 0x06004674 RID: 18036 RVA: 0x000DCE78 File Offset: 0x000DB078
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasInvitationId)
			{
				num += 1U;
				num += 8U;
			}
			return num;
		}

		// Token: 0x0400179A RID: 6042
		public bool HasAgentId;

		// Token: 0x0400179B RID: 6043
		private EntityId _AgentId;

		// Token: 0x0400179C RID: 6044
		public bool HasInvitationId;

		// Token: 0x0400179D RID: 6045
		private ulong _InvitationId;
	}
}
