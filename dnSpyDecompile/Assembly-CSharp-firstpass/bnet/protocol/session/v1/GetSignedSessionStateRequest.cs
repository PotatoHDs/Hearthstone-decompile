using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.session.v1
{
	// Token: 0x02000311 RID: 785
	public class GetSignedSessionStateRequest : IProtoBuf
	{
		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06002FA6 RID: 12198 RVA: 0x000A169A File Offset: 0x0009F89A
		// (set) Token: 0x06002FA7 RID: 12199 RVA: 0x000A16A2 File Offset: 0x0009F8A2
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

		// Token: 0x06002FA8 RID: 12200 RVA: 0x000A16B5 File Offset: 0x0009F8B5
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x000A16C0 File Offset: 0x0009F8C0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x000A16F0 File Offset: 0x0009F8F0
		public override bool Equals(object obj)
		{
			GetSignedSessionStateRequest getSignedSessionStateRequest = obj as GetSignedSessionStateRequest;
			return getSignedSessionStateRequest != null && this.HasAgentId == getSignedSessionStateRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(getSignedSessionStateRequest.AgentId));
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06002FAB RID: 12203 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x000A1735 File Offset: 0x0009F935
		public static GetSignedSessionStateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetSignedSessionStateRequest>(bs, 0, -1);
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x000A173F File Offset: 0x0009F93F
		public void Deserialize(Stream stream)
		{
			GetSignedSessionStateRequest.Deserialize(stream, this);
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x000A1749 File Offset: 0x0009F949
		public static GetSignedSessionStateRequest Deserialize(Stream stream, GetSignedSessionStateRequest instance)
		{
			return GetSignedSessionStateRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x000A1754 File Offset: 0x0009F954
		public static GetSignedSessionStateRequest DeserializeLengthDelimited(Stream stream)
		{
			GetSignedSessionStateRequest getSignedSessionStateRequest = new GetSignedSessionStateRequest();
			GetSignedSessionStateRequest.DeserializeLengthDelimited(stream, getSignedSessionStateRequest);
			return getSignedSessionStateRequest;
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x000A1770 File Offset: 0x0009F970
		public static GetSignedSessionStateRequest DeserializeLengthDelimited(Stream stream, GetSignedSessionStateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetSignedSessionStateRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002FB1 RID: 12209 RVA: 0x000A1798 File Offset: 0x0009F998
		public static GetSignedSessionStateRequest Deserialize(Stream stream, GetSignedSessionStateRequest instance, long limit)
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
					if (instance.AgentId == null)
					{
						instance.AgentId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.AgentId);
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

		// Token: 0x06002FB2 RID: 12210 RVA: 0x000A1832 File Offset: 0x0009FA32
		public void Serialize(Stream stream)
		{
			GetSignedSessionStateRequest.Serialize(stream, this);
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x000A183B File Offset: 0x0009FA3B
		public static void Serialize(Stream stream, GetSignedSessionStateRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x000A186C File Offset: 0x0009FA6C
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

		// Token: 0x04001315 RID: 4885
		public bool HasAgentId;

		// Token: 0x04001316 RID: 4886
		private GameAccountHandle _AgentId;
	}
}
