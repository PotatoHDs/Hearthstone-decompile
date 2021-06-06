using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000458 RID: 1112
	public class JoinRequest : IProtoBuf
	{
		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x06004BF6 RID: 19446 RVA: 0x000EC411 File Offset: 0x000EA611
		// (set) Token: 0x06004BF7 RID: 19447 RVA: 0x000EC419 File Offset: 0x000EA619
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

		// Token: 0x06004BF8 RID: 19448 RVA: 0x000EC42C File Offset: 0x000EA62C
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x06004BF9 RID: 19449 RVA: 0x000EC435 File Offset: 0x000EA635
		// (set) Token: 0x06004BFA RID: 19450 RVA: 0x000EC43D File Offset: 0x000EA63D
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

		// Token: 0x06004BFB RID: 19451 RVA: 0x000EC450 File Offset: 0x000EA650
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x06004BFC RID: 19452 RVA: 0x000EC459 File Offset: 0x000EA659
		// (set) Token: 0x06004BFD RID: 19453 RVA: 0x000EC461 File Offset: 0x000EA661
		public CreateMemberOptions Member
		{
			get
			{
				return this._Member;
			}
			set
			{
				this._Member = value;
				this.HasMember = (value != null);
			}
		}

		// Token: 0x06004BFE RID: 19454 RVA: 0x000EC474 File Offset: 0x000EA674
		public void SetMember(CreateMemberOptions val)
		{
			this.Member = val;
		}

		// Token: 0x06004BFF RID: 19455 RVA: 0x000EC480 File Offset: 0x000EA680
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
			if (this.HasMember)
			{
				num ^= this.Member.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004C00 RID: 19456 RVA: 0x000EC4DC File Offset: 0x000EA6DC
		public override bool Equals(object obj)
		{
			JoinRequest joinRequest = obj as JoinRequest;
			return joinRequest != null && this.HasAgentId == joinRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(joinRequest.AgentId)) && this.HasChannelId == joinRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(joinRequest.ChannelId)) && this.HasMember == joinRequest.HasMember && (!this.HasMember || this.Member.Equals(joinRequest.Member));
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x06004C01 RID: 19457 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004C02 RID: 19458 RVA: 0x000EC577 File Offset: 0x000EA777
		public static JoinRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<JoinRequest>(bs, 0, -1);
		}

		// Token: 0x06004C03 RID: 19459 RVA: 0x000EC581 File Offset: 0x000EA781
		public void Deserialize(Stream stream)
		{
			JoinRequest.Deserialize(stream, this);
		}

		// Token: 0x06004C04 RID: 19460 RVA: 0x000EC58B File Offset: 0x000EA78B
		public static JoinRequest Deserialize(Stream stream, JoinRequest instance)
		{
			return JoinRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004C05 RID: 19461 RVA: 0x000EC598 File Offset: 0x000EA798
		public static JoinRequest DeserializeLengthDelimited(Stream stream)
		{
			JoinRequest joinRequest = new JoinRequest();
			JoinRequest.DeserializeLengthDelimited(stream, joinRequest);
			return joinRequest;
		}

		// Token: 0x06004C06 RID: 19462 RVA: 0x000EC5B4 File Offset: 0x000EA7B4
		public static JoinRequest DeserializeLengthDelimited(Stream stream, JoinRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return JoinRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004C07 RID: 19463 RVA: 0x000EC5DC File Offset: 0x000EA7DC
		public static JoinRequest Deserialize(Stream stream, JoinRequest instance, long limit)
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
						else if (instance.Member == null)
						{
							instance.Member = CreateMemberOptions.DeserializeLengthDelimited(stream);
						}
						else
						{
							CreateMemberOptions.DeserializeLengthDelimited(stream, instance.Member);
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

		// Token: 0x06004C08 RID: 19464 RVA: 0x000EC6DE File Offset: 0x000EA8DE
		public void Serialize(Stream stream)
		{
			JoinRequest.Serialize(stream, this);
		}

		// Token: 0x06004C09 RID: 19465 RVA: 0x000EC6E8 File Offset: 0x000EA8E8
		public static void Serialize(Stream stream, JoinRequest instance)
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
			if (instance.HasMember)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Member.GetSerializedSize());
				CreateMemberOptions.Serialize(stream, instance.Member);
			}
		}

		// Token: 0x06004C0A RID: 19466 RVA: 0x000EC77C File Offset: 0x000EA97C
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
			if (this.HasMember)
			{
				num += 1U;
				uint serializedSize3 = this.Member.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		// Token: 0x040018CC RID: 6348
		public bool HasAgentId;

		// Token: 0x040018CD RID: 6349
		private GameAccountHandle _AgentId;

		// Token: 0x040018CE RID: 6350
		public bool HasChannelId;

		// Token: 0x040018CF RID: 6351
		private ChannelId _ChannelId;

		// Token: 0x040018D0 RID: 6352
		public bool HasMember;

		// Token: 0x040018D1 RID: 6353
		private CreateMemberOptions _Member;
	}
}
