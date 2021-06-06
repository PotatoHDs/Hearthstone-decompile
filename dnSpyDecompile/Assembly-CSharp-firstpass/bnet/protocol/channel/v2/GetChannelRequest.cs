using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x0200044C RID: 1100
	public class GetChannelRequest : IProtoBuf
	{
		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06004AF5 RID: 19189 RVA: 0x000E995A File Offset: 0x000E7B5A
		// (set) Token: 0x06004AF6 RID: 19190 RVA: 0x000E9962 File Offset: 0x000E7B62
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

		// Token: 0x06004AF7 RID: 19191 RVA: 0x000E9975 File Offset: 0x000E7B75
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x06004AF8 RID: 19192 RVA: 0x000E997E File Offset: 0x000E7B7E
		// (set) Token: 0x06004AF9 RID: 19193 RVA: 0x000E9986 File Offset: 0x000E7B86
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

		// Token: 0x06004AFA RID: 19194 RVA: 0x000E9999 File Offset: 0x000E7B99
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06004AFB RID: 19195 RVA: 0x000E99A2 File Offset: 0x000E7BA2
		// (set) Token: 0x06004AFC RID: 19196 RVA: 0x000E99AA File Offset: 0x000E7BAA
		public bool FetchAttributes
		{
			get
			{
				return this._FetchAttributes;
			}
			set
			{
				this._FetchAttributes = value;
				this.HasFetchAttributes = true;
			}
		}

		// Token: 0x06004AFD RID: 19197 RVA: 0x000E99BA File Offset: 0x000E7BBA
		public void SetFetchAttributes(bool val)
		{
			this.FetchAttributes = val;
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x06004AFE RID: 19198 RVA: 0x000E99C3 File Offset: 0x000E7BC3
		// (set) Token: 0x06004AFF RID: 19199 RVA: 0x000E99CB File Offset: 0x000E7BCB
		public bool FetchMembers
		{
			get
			{
				return this._FetchMembers;
			}
			set
			{
				this._FetchMembers = value;
				this.HasFetchMembers = true;
			}
		}

		// Token: 0x06004B00 RID: 19200 RVA: 0x000E99DB File Offset: 0x000E7BDB
		public void SetFetchMembers(bool val)
		{
			this.FetchMembers = val;
		}

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06004B01 RID: 19201 RVA: 0x000E99E4 File Offset: 0x000E7BE4
		// (set) Token: 0x06004B02 RID: 19202 RVA: 0x000E99EC File Offset: 0x000E7BEC
		public bool FetchInvitations
		{
			get
			{
				return this._FetchInvitations;
			}
			set
			{
				this._FetchInvitations = value;
				this.HasFetchInvitations = true;
			}
		}

		// Token: 0x06004B03 RID: 19203 RVA: 0x000E99FC File Offset: 0x000E7BFC
		public void SetFetchInvitations(bool val)
		{
			this.FetchInvitations = val;
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x06004B04 RID: 19204 RVA: 0x000E9A05 File Offset: 0x000E7C05
		// (set) Token: 0x06004B05 RID: 19205 RVA: 0x000E9A0D File Offset: 0x000E7C0D
		public bool FetchRoles
		{
			get
			{
				return this._FetchRoles;
			}
			set
			{
				this._FetchRoles = value;
				this.HasFetchRoles = true;
			}
		}

		// Token: 0x06004B06 RID: 19206 RVA: 0x000E9A1D File Offset: 0x000E7C1D
		public void SetFetchRoles(bool val)
		{
			this.FetchRoles = val;
		}

		// Token: 0x06004B07 RID: 19207 RVA: 0x000E9A28 File Offset: 0x000E7C28
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
			if (this.HasFetchAttributes)
			{
				num ^= this.FetchAttributes.GetHashCode();
			}
			if (this.HasFetchMembers)
			{
				num ^= this.FetchMembers.GetHashCode();
			}
			if (this.HasFetchInvitations)
			{
				num ^= this.FetchInvitations.GetHashCode();
			}
			if (this.HasFetchRoles)
			{
				num ^= this.FetchRoles.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004B08 RID: 19208 RVA: 0x000E9AD4 File Offset: 0x000E7CD4
		public override bool Equals(object obj)
		{
			GetChannelRequest getChannelRequest = obj as GetChannelRequest;
			return getChannelRequest != null && this.HasAgentId == getChannelRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(getChannelRequest.AgentId)) && this.HasChannelId == getChannelRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(getChannelRequest.ChannelId)) && this.HasFetchAttributes == getChannelRequest.HasFetchAttributes && (!this.HasFetchAttributes || this.FetchAttributes.Equals(getChannelRequest.FetchAttributes)) && this.HasFetchMembers == getChannelRequest.HasFetchMembers && (!this.HasFetchMembers || this.FetchMembers.Equals(getChannelRequest.FetchMembers)) && this.HasFetchInvitations == getChannelRequest.HasFetchInvitations && (!this.HasFetchInvitations || this.FetchInvitations.Equals(getChannelRequest.FetchInvitations)) && this.HasFetchRoles == getChannelRequest.HasFetchRoles && (!this.HasFetchRoles || this.FetchRoles.Equals(getChannelRequest.FetchRoles));
		}

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06004B09 RID: 19209 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004B0A RID: 19210 RVA: 0x000E9BFC File Offset: 0x000E7DFC
		public static GetChannelRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetChannelRequest>(bs, 0, -1);
		}

		// Token: 0x06004B0B RID: 19211 RVA: 0x000E9C06 File Offset: 0x000E7E06
		public void Deserialize(Stream stream)
		{
			GetChannelRequest.Deserialize(stream, this);
		}

		// Token: 0x06004B0C RID: 19212 RVA: 0x000E9C10 File Offset: 0x000E7E10
		public static GetChannelRequest Deserialize(Stream stream, GetChannelRequest instance)
		{
			return GetChannelRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004B0D RID: 19213 RVA: 0x000E9C1C File Offset: 0x000E7E1C
		public static GetChannelRequest DeserializeLengthDelimited(Stream stream)
		{
			GetChannelRequest getChannelRequest = new GetChannelRequest();
			GetChannelRequest.DeserializeLengthDelimited(stream, getChannelRequest);
			return getChannelRequest;
		}

		// Token: 0x06004B0E RID: 19214 RVA: 0x000E9C38 File Offset: 0x000E7E38
		public static GetChannelRequest DeserializeLengthDelimited(Stream stream, GetChannelRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetChannelRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004B0F RID: 19215 RVA: 0x000E9C60 File Offset: 0x000E7E60
		public static GetChannelRequest Deserialize(Stream stream, GetChannelRequest instance, long limit)
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
				else
				{
					if (num <= 40)
					{
						if (num != 10)
						{
							if (num != 18)
							{
								if (num == 40)
								{
									instance.FetchAttributes = ProtocolParser.ReadBool(stream);
									continue;
								}
							}
							else
							{
								if (instance.ChannelId == null)
								{
									instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
									continue;
								}
								ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
								continue;
							}
						}
						else
						{
							if (instance.AgentId == null)
							{
								instance.AgentId = GameAccountHandle.DeserializeLengthDelimited(stream);
								continue;
							}
							GameAccountHandle.DeserializeLengthDelimited(stream, instance.AgentId);
							continue;
						}
					}
					else
					{
						if (num == 48)
						{
							instance.FetchMembers = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 56)
						{
							instance.FetchInvitations = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 64)
						{
							instance.FetchRoles = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
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

		// Token: 0x06004B10 RID: 19216 RVA: 0x000E9DA3 File Offset: 0x000E7FA3
		public void Serialize(Stream stream)
		{
			GetChannelRequest.Serialize(stream, this);
		}

		// Token: 0x06004B11 RID: 19217 RVA: 0x000E9DAC File Offset: 0x000E7FAC
		public static void Serialize(Stream stream, GetChannelRequest instance)
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
			if (instance.HasFetchAttributes)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.FetchAttributes);
			}
			if (instance.HasFetchMembers)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.FetchMembers);
			}
			if (instance.HasFetchInvitations)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.FetchInvitations);
			}
			if (instance.HasFetchRoles)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteBool(stream, instance.FetchRoles);
			}
		}

		// Token: 0x06004B12 RID: 19218 RVA: 0x000E9E84 File Offset: 0x000E8084
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
			if (this.HasFetchAttributes)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasFetchMembers)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasFetchInvitations)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasFetchRoles)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04001890 RID: 6288
		public bool HasAgentId;

		// Token: 0x04001891 RID: 6289
		private GameAccountHandle _AgentId;

		// Token: 0x04001892 RID: 6290
		public bool HasChannelId;

		// Token: 0x04001893 RID: 6291
		private ChannelId _ChannelId;

		// Token: 0x04001894 RID: 6292
		public bool HasFetchAttributes;

		// Token: 0x04001895 RID: 6293
		private bool _FetchAttributes;

		// Token: 0x04001896 RID: 6294
		public bool HasFetchMembers;

		// Token: 0x04001897 RID: 6295
		private bool _FetchMembers;

		// Token: 0x04001898 RID: 6296
		public bool HasFetchInvitations;

		// Token: 0x04001899 RID: 6297
		private bool _FetchInvitations;

		// Token: 0x0400189A RID: 6298
		public bool HasFetchRoles;

		// Token: 0x0400189B RID: 6299
		private bool _FetchRoles;
	}
}
