using System;
using System.IO;

namespace bnet.protocol.friends.v1
{
	// Token: 0x02000420 RID: 1056
	public class IgnoreInvitationRequest : IProtoBuf
	{
		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x0600469F RID: 18079 RVA: 0x000DD4CF File Offset: 0x000DB6CF
		// (set) Token: 0x060046A0 RID: 18080 RVA: 0x000DD4D7 File Offset: 0x000DB6D7
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

		// Token: 0x060046A1 RID: 18081 RVA: 0x000DD4EA File Offset: 0x000DB6EA
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x060046A2 RID: 18082 RVA: 0x000DD4F3 File Offset: 0x000DB6F3
		// (set) Token: 0x060046A3 RID: 18083 RVA: 0x000DD4FB File Offset: 0x000DB6FB
		public ulong InvitationId { get; set; }

		// Token: 0x060046A4 RID: 18084 RVA: 0x000DD504 File Offset: 0x000DB704
		public void SetInvitationId(ulong val)
		{
			this.InvitationId = val;
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x060046A5 RID: 18085 RVA: 0x000DD50D File Offset: 0x000DB70D
		// (set) Token: 0x060046A6 RID: 18086 RVA: 0x000DD515 File Offset: 0x000DB715
		public uint Program
		{
			get
			{
				return this._Program;
			}
			set
			{
				this._Program = value;
				this.HasProgram = true;
			}
		}

		// Token: 0x060046A7 RID: 18087 RVA: 0x000DD525 File Offset: 0x000DB725
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x060046A8 RID: 18088 RVA: 0x000DD530 File Offset: 0x000DB730
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			num ^= this.InvitationId.GetHashCode();
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			return num;
		}

		// Token: 0x060046A9 RID: 18089 RVA: 0x000DD58C File Offset: 0x000DB78C
		public override bool Equals(object obj)
		{
			IgnoreInvitationRequest ignoreInvitationRequest = obj as IgnoreInvitationRequest;
			return ignoreInvitationRequest != null && this.HasAgentId == ignoreInvitationRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(ignoreInvitationRequest.AgentId)) && this.InvitationId.Equals(ignoreInvitationRequest.InvitationId) && this.HasProgram == ignoreInvitationRequest.HasProgram && (!this.HasProgram || this.Program.Equals(ignoreInvitationRequest.Program));
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x060046AA RID: 18090 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060046AB RID: 18091 RVA: 0x000DD617 File Offset: 0x000DB817
		public static IgnoreInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<IgnoreInvitationRequest>(bs, 0, -1);
		}

		// Token: 0x060046AC RID: 18092 RVA: 0x000DD621 File Offset: 0x000DB821
		public void Deserialize(Stream stream)
		{
			IgnoreInvitationRequest.Deserialize(stream, this);
		}

		// Token: 0x060046AD RID: 18093 RVA: 0x000DD62B File Offset: 0x000DB82B
		public static IgnoreInvitationRequest Deserialize(Stream stream, IgnoreInvitationRequest instance)
		{
			return IgnoreInvitationRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060046AE RID: 18094 RVA: 0x000DD638 File Offset: 0x000DB838
		public static IgnoreInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			IgnoreInvitationRequest ignoreInvitationRequest = new IgnoreInvitationRequest();
			IgnoreInvitationRequest.DeserializeLengthDelimited(stream, ignoreInvitationRequest);
			return ignoreInvitationRequest;
		}

		// Token: 0x060046AF RID: 18095 RVA: 0x000DD654 File Offset: 0x000DB854
		public static IgnoreInvitationRequest DeserializeLengthDelimited(Stream stream, IgnoreInvitationRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return IgnoreInvitationRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060046B0 RID: 18096 RVA: 0x000DD67C File Offset: 0x000DB87C
		public static IgnoreInvitationRequest Deserialize(Stream stream, IgnoreInvitationRequest instance, long limit)
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
						if (num != 37)
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
							instance.Program = binaryReader.ReadUInt32();
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

		// Token: 0x060046B1 RID: 18097 RVA: 0x000DD751 File Offset: 0x000DB951
		public void Serialize(Stream stream)
		{
			IgnoreInvitationRequest.Serialize(stream, this);
		}

		// Token: 0x060046B2 RID: 18098 RVA: 0x000DD75C File Offset: 0x000DB95C
		public static void Serialize(Stream stream, IgnoreInvitationRequest instance)
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
			if (instance.HasProgram)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.Program);
			}
		}

		// Token: 0x060046B3 RID: 18099 RVA: 0x000DD7D0 File Offset: 0x000DB9D0
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
			if (this.HasProgram)
			{
				num += 1U;
				num += 4U;
			}
			return num + 1U;
		}

		// Token: 0x040017A6 RID: 6054
		public bool HasAgentId;

		// Token: 0x040017A7 RID: 6055
		private EntityId _AgentId;

		// Token: 0x040017A9 RID: 6057
		public bool HasProgram;

		// Token: 0x040017AA RID: 6058
		private uint _Program;
	}
}
