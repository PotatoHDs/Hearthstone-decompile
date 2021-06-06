using System;
using System.IO;

namespace bnet.protocol.channel
{
	// Token: 0x02000447 RID: 1095
	public class ChannelPrivilegeSet : IProtoBuf
	{
		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x06004A61 RID: 19041 RVA: 0x000E7DC2 File Offset: 0x000E5FC2
		// (set) Token: 0x06004A62 RID: 19042 RVA: 0x000E7DCA File Offset: 0x000E5FCA
		public bool CanInvite
		{
			get
			{
				return this._CanInvite;
			}
			set
			{
				this._CanInvite = value;
				this.HasCanInvite = true;
			}
		}

		// Token: 0x06004A63 RID: 19043 RVA: 0x000E7DDA File Offset: 0x000E5FDA
		public void SetCanInvite(bool val)
		{
			this.CanInvite = val;
		}

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x06004A64 RID: 19044 RVA: 0x000E7DE3 File Offset: 0x000E5FE3
		// (set) Token: 0x06004A65 RID: 19045 RVA: 0x000E7DEB File Offset: 0x000E5FEB
		public bool CanInviteWithReservation
		{
			get
			{
				return this._CanInviteWithReservation;
			}
			set
			{
				this._CanInviteWithReservation = value;
				this.HasCanInviteWithReservation = true;
			}
		}

		// Token: 0x06004A66 RID: 19046 RVA: 0x000E7DFB File Offset: 0x000E5FFB
		public void SetCanInviteWithReservation(bool val)
		{
			this.CanInviteWithReservation = val;
		}

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x06004A67 RID: 19047 RVA: 0x000E7E04 File Offset: 0x000E6004
		// (set) Token: 0x06004A68 RID: 19048 RVA: 0x000E7E0C File Offset: 0x000E600C
		public bool CanRevokeOtherInvitation
		{
			get
			{
				return this._CanRevokeOtherInvitation;
			}
			set
			{
				this._CanRevokeOtherInvitation = value;
				this.HasCanRevokeOtherInvitation = true;
			}
		}

		// Token: 0x06004A69 RID: 19049 RVA: 0x000E7E1C File Offset: 0x000E601C
		public void SetCanRevokeOtherInvitation(bool val)
		{
			this.CanRevokeOtherInvitation = val;
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x06004A6A RID: 19050 RVA: 0x000E7E25 File Offset: 0x000E6025
		// (set) Token: 0x06004A6B RID: 19051 RVA: 0x000E7E2D File Offset: 0x000E602D
		public bool CanRevokeOwnInvitation
		{
			get
			{
				return this._CanRevokeOwnInvitation;
			}
			set
			{
				this._CanRevokeOwnInvitation = value;
				this.HasCanRevokeOwnInvitation = true;
			}
		}

		// Token: 0x06004A6C RID: 19052 RVA: 0x000E7E3D File Offset: 0x000E603D
		public void SetCanRevokeOwnInvitation(bool val)
		{
			this.CanRevokeOwnInvitation = val;
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x06004A6D RID: 19053 RVA: 0x000E7E46 File Offset: 0x000E6046
		// (set) Token: 0x06004A6E RID: 19054 RVA: 0x000E7E4E File Offset: 0x000E604E
		public bool CanKick
		{
			get
			{
				return this._CanKick;
			}
			set
			{
				this._CanKick = value;
				this.HasCanKick = true;
			}
		}

		// Token: 0x06004A6F RID: 19055 RVA: 0x000E7E5E File Offset: 0x000E605E
		public void SetCanKick(bool val)
		{
			this.CanKick = val;
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x06004A70 RID: 19056 RVA: 0x000E7E67 File Offset: 0x000E6067
		// (set) Token: 0x06004A71 RID: 19057 RVA: 0x000E7E6F File Offset: 0x000E606F
		public bool CanDissolve
		{
			get
			{
				return this._CanDissolve;
			}
			set
			{
				this._CanDissolve = value;
				this.HasCanDissolve = true;
			}
		}

		// Token: 0x06004A72 RID: 19058 RVA: 0x000E7E7F File Offset: 0x000E607F
		public void SetCanDissolve(bool val)
		{
			this.CanDissolve = val;
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x06004A73 RID: 19059 RVA: 0x000E7E88 File Offset: 0x000E6088
		// (set) Token: 0x06004A74 RID: 19060 RVA: 0x000E7E90 File Offset: 0x000E6090
		public bool CanSetPrivacy
		{
			get
			{
				return this._CanSetPrivacy;
			}
			set
			{
				this._CanSetPrivacy = value;
				this.HasCanSetPrivacy = true;
			}
		}

		// Token: 0x06004A75 RID: 19061 RVA: 0x000E7EA0 File Offset: 0x000E60A0
		public void SetCanSetPrivacy(bool val)
		{
			this.CanSetPrivacy = val;
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x06004A76 RID: 19062 RVA: 0x000E7EA9 File Offset: 0x000E60A9
		// (set) Token: 0x06004A77 RID: 19063 RVA: 0x000E7EB1 File Offset: 0x000E60B1
		public bool CanSendMessage
		{
			get
			{
				return this._CanSendMessage;
			}
			set
			{
				this._CanSendMessage = value;
				this.HasCanSendMessage = true;
			}
		}

		// Token: 0x06004A78 RID: 19064 RVA: 0x000E7EC1 File Offset: 0x000E60C1
		public void SetCanSendMessage(bool val)
		{
			this.CanSendMessage = val;
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x06004A79 RID: 19065 RVA: 0x000E7ECA File Offset: 0x000E60CA
		// (set) Token: 0x06004A7A RID: 19066 RVA: 0x000E7ED2 File Offset: 0x000E60D2
		public bool CanReceiveMessage
		{
			get
			{
				return this._CanReceiveMessage;
			}
			set
			{
				this._CanReceiveMessage = value;
				this.HasCanReceiveMessage = true;
			}
		}

		// Token: 0x06004A7B RID: 19067 RVA: 0x000E7EE2 File Offset: 0x000E60E2
		public void SetCanReceiveMessage(bool val)
		{
			this.CanReceiveMessage = val;
		}

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x06004A7C RID: 19068 RVA: 0x000E7EEB File Offset: 0x000E60EB
		// (set) Token: 0x06004A7D RID: 19069 RVA: 0x000E7EF3 File Offset: 0x000E60F3
		public bool CanSetAttribute
		{
			get
			{
				return this._CanSetAttribute;
			}
			set
			{
				this._CanSetAttribute = value;
				this.HasCanSetAttribute = true;
			}
		}

		// Token: 0x06004A7E RID: 19070 RVA: 0x000E7F03 File Offset: 0x000E6103
		public void SetCanSetAttribute(bool val)
		{
			this.CanSetAttribute = val;
		}

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x06004A7F RID: 19071 RVA: 0x000E7F0C File Offset: 0x000E610C
		// (set) Token: 0x06004A80 RID: 19072 RVA: 0x000E7F14 File Offset: 0x000E6114
		public bool CanSetOtherMemberAttribute
		{
			get
			{
				return this._CanSetOtherMemberAttribute;
			}
			set
			{
				this._CanSetOtherMemberAttribute = value;
				this.HasCanSetOtherMemberAttribute = true;
			}
		}

		// Token: 0x06004A81 RID: 19073 RVA: 0x000E7F24 File Offset: 0x000E6124
		public void SetCanSetOtherMemberAttribute(bool val)
		{
			this.CanSetOtherMemberAttribute = val;
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x06004A82 RID: 19074 RVA: 0x000E7F2D File Offset: 0x000E612D
		// (set) Token: 0x06004A83 RID: 19075 RVA: 0x000E7F35 File Offset: 0x000E6135
		public bool CanSetOwnMemberAttribute
		{
			get
			{
				return this._CanSetOwnMemberAttribute;
			}
			set
			{
				this._CanSetOwnMemberAttribute = value;
				this.HasCanSetOwnMemberAttribute = true;
			}
		}

		// Token: 0x06004A84 RID: 19076 RVA: 0x000E7F45 File Offset: 0x000E6145
		public void SetCanSetOwnMemberAttribute(bool val)
		{
			this.CanSetOwnMemberAttribute = val;
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06004A85 RID: 19077 RVA: 0x000E7F4E File Offset: 0x000E614E
		// (set) Token: 0x06004A86 RID: 19078 RVA: 0x000E7F56 File Offset: 0x000E6156
		public bool CanEnterGame
		{
			get
			{
				return this._CanEnterGame;
			}
			set
			{
				this._CanEnterGame = value;
				this.HasCanEnterGame = true;
			}
		}

		// Token: 0x06004A87 RID: 19079 RVA: 0x000E7F66 File Offset: 0x000E6166
		public void SetCanEnterGame(bool val)
		{
			this.CanEnterGame = val;
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06004A88 RID: 19080 RVA: 0x000E7F6F File Offset: 0x000E616F
		// (set) Token: 0x06004A89 RID: 19081 RVA: 0x000E7F77 File Offset: 0x000E6177
		public bool CanSuggest
		{
			get
			{
				return this._CanSuggest;
			}
			set
			{
				this._CanSuggest = value;
				this.HasCanSuggest = true;
			}
		}

		// Token: 0x06004A8A RID: 19082 RVA: 0x000E7F87 File Offset: 0x000E6187
		public void SetCanSuggest(bool val)
		{
			this.CanSuggest = val;
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06004A8B RID: 19083 RVA: 0x000E7F90 File Offset: 0x000E6190
		// (set) Token: 0x06004A8C RID: 19084 RVA: 0x000E7F98 File Offset: 0x000E6198
		public bool CanApprove
		{
			get
			{
				return this._CanApprove;
			}
			set
			{
				this._CanApprove = value;
				this.HasCanApprove = true;
			}
		}

		// Token: 0x06004A8D RID: 19085 RVA: 0x000E7FA8 File Offset: 0x000E61A8
		public void SetCanApprove(bool val)
		{
			this.CanApprove = val;
		}

		// Token: 0x06004A8E RID: 19086 RVA: 0x000E7FB4 File Offset: 0x000E61B4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasCanInvite)
			{
				num ^= this.CanInvite.GetHashCode();
			}
			if (this.HasCanInviteWithReservation)
			{
				num ^= this.CanInviteWithReservation.GetHashCode();
			}
			if (this.HasCanRevokeOtherInvitation)
			{
				num ^= this.CanRevokeOtherInvitation.GetHashCode();
			}
			if (this.HasCanRevokeOwnInvitation)
			{
				num ^= this.CanRevokeOwnInvitation.GetHashCode();
			}
			if (this.HasCanKick)
			{
				num ^= this.CanKick.GetHashCode();
			}
			if (this.HasCanDissolve)
			{
				num ^= this.CanDissolve.GetHashCode();
			}
			if (this.HasCanSetPrivacy)
			{
				num ^= this.CanSetPrivacy.GetHashCode();
			}
			if (this.HasCanSendMessage)
			{
				num ^= this.CanSendMessage.GetHashCode();
			}
			if (this.HasCanReceiveMessage)
			{
				num ^= this.CanReceiveMessage.GetHashCode();
			}
			if (this.HasCanSetAttribute)
			{
				num ^= this.CanSetAttribute.GetHashCode();
			}
			if (this.HasCanSetOtherMemberAttribute)
			{
				num ^= this.CanSetOtherMemberAttribute.GetHashCode();
			}
			if (this.HasCanSetOwnMemberAttribute)
			{
				num ^= this.CanSetOwnMemberAttribute.GetHashCode();
			}
			if (this.HasCanEnterGame)
			{
				num ^= this.CanEnterGame.GetHashCode();
			}
			if (this.HasCanSuggest)
			{
				num ^= this.CanSuggest.GetHashCode();
			}
			if (this.HasCanApprove)
			{
				num ^= this.CanApprove.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004A8F RID: 19087 RVA: 0x000E8148 File Offset: 0x000E6348
		public override bool Equals(object obj)
		{
			ChannelPrivilegeSet channelPrivilegeSet = obj as ChannelPrivilegeSet;
			return channelPrivilegeSet != null && this.HasCanInvite == channelPrivilegeSet.HasCanInvite && (!this.HasCanInvite || this.CanInvite.Equals(channelPrivilegeSet.CanInvite)) && this.HasCanInviteWithReservation == channelPrivilegeSet.HasCanInviteWithReservation && (!this.HasCanInviteWithReservation || this.CanInviteWithReservation.Equals(channelPrivilegeSet.CanInviteWithReservation)) && this.HasCanRevokeOtherInvitation == channelPrivilegeSet.HasCanRevokeOtherInvitation && (!this.HasCanRevokeOtherInvitation || this.CanRevokeOtherInvitation.Equals(channelPrivilegeSet.CanRevokeOtherInvitation)) && this.HasCanRevokeOwnInvitation == channelPrivilegeSet.HasCanRevokeOwnInvitation && (!this.HasCanRevokeOwnInvitation || this.CanRevokeOwnInvitation.Equals(channelPrivilegeSet.CanRevokeOwnInvitation)) && this.HasCanKick == channelPrivilegeSet.HasCanKick && (!this.HasCanKick || this.CanKick.Equals(channelPrivilegeSet.CanKick)) && this.HasCanDissolve == channelPrivilegeSet.HasCanDissolve && (!this.HasCanDissolve || this.CanDissolve.Equals(channelPrivilegeSet.CanDissolve)) && this.HasCanSetPrivacy == channelPrivilegeSet.HasCanSetPrivacy && (!this.HasCanSetPrivacy || this.CanSetPrivacy.Equals(channelPrivilegeSet.CanSetPrivacy)) && this.HasCanSendMessage == channelPrivilegeSet.HasCanSendMessage && (!this.HasCanSendMessage || this.CanSendMessage.Equals(channelPrivilegeSet.CanSendMessage)) && this.HasCanReceiveMessage == channelPrivilegeSet.HasCanReceiveMessage && (!this.HasCanReceiveMessage || this.CanReceiveMessage.Equals(channelPrivilegeSet.CanReceiveMessage)) && this.HasCanSetAttribute == channelPrivilegeSet.HasCanSetAttribute && (!this.HasCanSetAttribute || this.CanSetAttribute.Equals(channelPrivilegeSet.CanSetAttribute)) && this.HasCanSetOtherMemberAttribute == channelPrivilegeSet.HasCanSetOtherMemberAttribute && (!this.HasCanSetOtherMemberAttribute || this.CanSetOtherMemberAttribute.Equals(channelPrivilegeSet.CanSetOtherMemberAttribute)) && this.HasCanSetOwnMemberAttribute == channelPrivilegeSet.HasCanSetOwnMemberAttribute && (!this.HasCanSetOwnMemberAttribute || this.CanSetOwnMemberAttribute.Equals(channelPrivilegeSet.CanSetOwnMemberAttribute)) && this.HasCanEnterGame == channelPrivilegeSet.HasCanEnterGame && (!this.HasCanEnterGame || this.CanEnterGame.Equals(channelPrivilegeSet.CanEnterGame)) && this.HasCanSuggest == channelPrivilegeSet.HasCanSuggest && (!this.HasCanSuggest || this.CanSuggest.Equals(channelPrivilegeSet.CanSuggest)) && this.HasCanApprove == channelPrivilegeSet.HasCanApprove && (!this.HasCanApprove || this.CanApprove.Equals(channelPrivilegeSet.CanApprove));
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06004A90 RID: 19088 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004A91 RID: 19089 RVA: 0x000E8414 File Offset: 0x000E6614
		public static ChannelPrivilegeSet ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelPrivilegeSet>(bs, 0, -1);
		}

		// Token: 0x06004A92 RID: 19090 RVA: 0x000E841E File Offset: 0x000E661E
		public void Deserialize(Stream stream)
		{
			ChannelPrivilegeSet.Deserialize(stream, this);
		}

		// Token: 0x06004A93 RID: 19091 RVA: 0x000E8428 File Offset: 0x000E6628
		public static ChannelPrivilegeSet Deserialize(Stream stream, ChannelPrivilegeSet instance)
		{
			return ChannelPrivilegeSet.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004A94 RID: 19092 RVA: 0x000E8434 File Offset: 0x000E6634
		public static ChannelPrivilegeSet DeserializeLengthDelimited(Stream stream)
		{
			ChannelPrivilegeSet channelPrivilegeSet = new ChannelPrivilegeSet();
			ChannelPrivilegeSet.DeserializeLengthDelimited(stream, channelPrivilegeSet);
			return channelPrivilegeSet;
		}

		// Token: 0x06004A95 RID: 19093 RVA: 0x000E8450 File Offset: 0x000E6650
		public static ChannelPrivilegeSet DeserializeLengthDelimited(Stream stream, ChannelPrivilegeSet instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChannelPrivilegeSet.Deserialize(stream, instance, num);
		}

		// Token: 0x06004A96 RID: 19094 RVA: 0x000E8478 File Offset: 0x000E6678
		public static ChannelPrivilegeSet Deserialize(Stream stream, ChannelPrivilegeSet instance, long limit)
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
					if (num <= 56)
					{
						if (num <= 24)
						{
							if (num == 8)
							{
								instance.CanInvite = ProtocolParser.ReadBool(stream);
								continue;
							}
							if (num == 16)
							{
								instance.CanInviteWithReservation = ProtocolParser.ReadBool(stream);
								continue;
							}
							if (num == 24)
							{
								instance.CanRevokeOtherInvitation = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
						else if (num <= 40)
						{
							if (num == 32)
							{
								instance.CanRevokeOwnInvitation = ProtocolParser.ReadBool(stream);
								continue;
							}
							if (num == 40)
							{
								instance.CanKick = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
						else
						{
							if (num == 48)
							{
								instance.CanDissolve = ProtocolParser.ReadBool(stream);
								continue;
							}
							if (num == 56)
							{
								instance.CanSetPrivacy = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
					}
					else if (num <= 88)
					{
						if (num <= 72)
						{
							if (num == 64)
							{
								instance.CanSendMessage = ProtocolParser.ReadBool(stream);
								continue;
							}
							if (num == 72)
							{
								instance.CanReceiveMessage = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
						else
						{
							if (num == 80)
							{
								instance.CanSetAttribute = ProtocolParser.ReadBool(stream);
								continue;
							}
							if (num == 88)
							{
								instance.CanSetOtherMemberAttribute = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
					}
					else if (num <= 104)
					{
						if (num == 96)
						{
							instance.CanSetOwnMemberAttribute = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 104)
						{
							instance.CanEnterGame = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					else
					{
						if (num == 112)
						{
							instance.CanSuggest = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 120)
						{
							instance.CanApprove = ProtocolParser.ReadBool(stream);
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

		// Token: 0x06004A97 RID: 19095 RVA: 0x000E86A5 File Offset: 0x000E68A5
		public void Serialize(Stream stream)
		{
			ChannelPrivilegeSet.Serialize(stream, this);
		}

		// Token: 0x06004A98 RID: 19096 RVA: 0x000E86B0 File Offset: 0x000E68B0
		public static void Serialize(Stream stream, ChannelPrivilegeSet instance)
		{
			if (instance.HasCanInvite)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.CanInvite);
			}
			if (instance.HasCanInviteWithReservation)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.CanInviteWithReservation);
			}
			if (instance.HasCanRevokeOtherInvitation)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.CanRevokeOtherInvitation);
			}
			if (instance.HasCanRevokeOwnInvitation)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.CanRevokeOwnInvitation);
			}
			if (instance.HasCanKick)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.CanKick);
			}
			if (instance.HasCanDissolve)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.CanDissolve);
			}
			if (instance.HasCanSetPrivacy)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.CanSetPrivacy);
			}
			if (instance.HasCanSendMessage)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteBool(stream, instance.CanSendMessage);
			}
			if (instance.HasCanReceiveMessage)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteBool(stream, instance.CanReceiveMessage);
			}
			if (instance.HasCanSetAttribute)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteBool(stream, instance.CanSetAttribute);
			}
			if (instance.HasCanSetOtherMemberAttribute)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteBool(stream, instance.CanSetOtherMemberAttribute);
			}
			if (instance.HasCanSetOwnMemberAttribute)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteBool(stream, instance.CanSetOwnMemberAttribute);
			}
			if (instance.HasCanEnterGame)
			{
				stream.WriteByte(104);
				ProtocolParser.WriteBool(stream, instance.CanEnterGame);
			}
			if (instance.HasCanSuggest)
			{
				stream.WriteByte(112);
				ProtocolParser.WriteBool(stream, instance.CanSuggest);
			}
			if (instance.HasCanApprove)
			{
				stream.WriteByte(120);
				ProtocolParser.WriteBool(stream, instance.CanApprove);
			}
		}

		// Token: 0x06004A99 RID: 19097 RVA: 0x000E8860 File Offset: 0x000E6A60
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasCanInvite)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasCanInviteWithReservation)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasCanRevokeOtherInvitation)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasCanRevokeOwnInvitation)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasCanKick)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasCanDissolve)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasCanSetPrivacy)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasCanSendMessage)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasCanReceiveMessage)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasCanSetAttribute)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasCanSetOtherMemberAttribute)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasCanSetOwnMemberAttribute)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasCanEnterGame)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasCanSuggest)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasCanApprove)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04001860 RID: 6240
		public bool HasCanInvite;

		// Token: 0x04001861 RID: 6241
		private bool _CanInvite;

		// Token: 0x04001862 RID: 6242
		public bool HasCanInviteWithReservation;

		// Token: 0x04001863 RID: 6243
		private bool _CanInviteWithReservation;

		// Token: 0x04001864 RID: 6244
		public bool HasCanRevokeOtherInvitation;

		// Token: 0x04001865 RID: 6245
		private bool _CanRevokeOtherInvitation;

		// Token: 0x04001866 RID: 6246
		public bool HasCanRevokeOwnInvitation;

		// Token: 0x04001867 RID: 6247
		private bool _CanRevokeOwnInvitation;

		// Token: 0x04001868 RID: 6248
		public bool HasCanKick;

		// Token: 0x04001869 RID: 6249
		private bool _CanKick;

		// Token: 0x0400186A RID: 6250
		public bool HasCanDissolve;

		// Token: 0x0400186B RID: 6251
		private bool _CanDissolve;

		// Token: 0x0400186C RID: 6252
		public bool HasCanSetPrivacy;

		// Token: 0x0400186D RID: 6253
		private bool _CanSetPrivacy;

		// Token: 0x0400186E RID: 6254
		public bool HasCanSendMessage;

		// Token: 0x0400186F RID: 6255
		private bool _CanSendMessage;

		// Token: 0x04001870 RID: 6256
		public bool HasCanReceiveMessage;

		// Token: 0x04001871 RID: 6257
		private bool _CanReceiveMessage;

		// Token: 0x04001872 RID: 6258
		public bool HasCanSetAttribute;

		// Token: 0x04001873 RID: 6259
		private bool _CanSetAttribute;

		// Token: 0x04001874 RID: 6260
		public bool HasCanSetOtherMemberAttribute;

		// Token: 0x04001875 RID: 6261
		private bool _CanSetOtherMemberAttribute;

		// Token: 0x04001876 RID: 6262
		public bool HasCanSetOwnMemberAttribute;

		// Token: 0x04001877 RID: 6263
		private bool _CanSetOwnMemberAttribute;

		// Token: 0x04001878 RID: 6264
		public bool HasCanEnterGame;

		// Token: 0x04001879 RID: 6265
		private bool _CanEnterGame;

		// Token: 0x0400187A RID: 6266
		public bool HasCanSuggest;

		// Token: 0x0400187B RID: 6267
		private bool _CanSuggest;

		// Token: 0x0400187C RID: 6268
		public bool HasCanApprove;

		// Token: 0x0400187D RID: 6269
		private bool _CanApprove;
	}
}
