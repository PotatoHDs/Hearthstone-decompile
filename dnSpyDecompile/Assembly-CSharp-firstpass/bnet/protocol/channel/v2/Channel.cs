using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.channel.v2.Types;
using bnet.protocol.v2;

namespace bnet.protocol.channel.v2
{
	// Token: 0x0200047D RID: 1149
	public class Channel : IProtoBuf
	{
		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06004F77 RID: 20343 RVA: 0x000F6896 File Offset: 0x000F4A96
		// (set) Token: 0x06004F78 RID: 20344 RVA: 0x000F689E File Offset: 0x000F4A9E
		public ChannelId Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.HasId = (value != null);
			}
		}

		// Token: 0x06004F79 RID: 20345 RVA: 0x000F68B1 File Offset: 0x000F4AB1
		public void SetId(ChannelId val)
		{
			this.Id = val;
		}

		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06004F7A RID: 20346 RVA: 0x000F68BA File Offset: 0x000F4ABA
		// (set) Token: 0x06004F7B RID: 20347 RVA: 0x000F68C2 File Offset: 0x000F4AC2
		public UniqueChannelType Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
				this.HasType = (value != null);
			}
		}

		// Token: 0x06004F7C RID: 20348 RVA: 0x000F68D5 File Offset: 0x000F4AD5
		public void SetType(UniqueChannelType val)
		{
			this.Type = val;
		}

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06004F7D RID: 20349 RVA: 0x000F68DE File Offset: 0x000F4ADE
		// (set) Token: 0x06004F7E RID: 20350 RVA: 0x000F68E6 File Offset: 0x000F4AE6
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
				this.HasName = (value != null);
			}
		}

		// Token: 0x06004F7F RID: 20351 RVA: 0x000F68F9 File Offset: 0x000F4AF9
		public void SetName(string val)
		{
			this.Name = val;
		}

		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06004F80 RID: 20352 RVA: 0x000F6902 File Offset: 0x000F4B02
		// (set) Token: 0x06004F81 RID: 20353 RVA: 0x000F690A File Offset: 0x000F4B0A
		public PrivacyLevel PrivacyLevel
		{
			get
			{
				return this._PrivacyLevel;
			}
			set
			{
				this._PrivacyLevel = value;
				this.HasPrivacyLevel = true;
			}
		}

		// Token: 0x06004F82 RID: 20354 RVA: 0x000F691A File Offset: 0x000F4B1A
		public void SetPrivacyLevel(PrivacyLevel val)
		{
			this.PrivacyLevel = val;
		}

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06004F83 RID: 20355 RVA: 0x000F6923 File Offset: 0x000F4B23
		// (set) Token: 0x06004F84 RID: 20356 RVA: 0x000F692B File Offset: 0x000F4B2B
		public List<bnet.protocol.v2.Attribute> Attribute
		{
			get
			{
				return this._Attribute;
			}
			set
			{
				this._Attribute = value;
			}
		}

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06004F85 RID: 20357 RVA: 0x000F6923 File Offset: 0x000F4B23
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06004F86 RID: 20358 RVA: 0x000F6934 File Offset: 0x000F4B34
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06004F87 RID: 20359 RVA: 0x000F6941 File Offset: 0x000F4B41
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06004F88 RID: 20360 RVA: 0x000F694F File Offset: 0x000F4B4F
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06004F89 RID: 20361 RVA: 0x000F695C File Offset: 0x000F4B5C
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06004F8A RID: 20362 RVA: 0x000F6965 File Offset: 0x000F4B65
		// (set) Token: 0x06004F8B RID: 20363 RVA: 0x000F696D File Offset: 0x000F4B6D
		public List<Member> Member
		{
			get
			{
				return this._Member;
			}
			set
			{
				this._Member = value;
			}
		}

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x06004F8C RID: 20364 RVA: 0x000F6965 File Offset: 0x000F4B65
		public List<Member> MemberList
		{
			get
			{
				return this._Member;
			}
		}

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x06004F8D RID: 20365 RVA: 0x000F6976 File Offset: 0x000F4B76
		public int MemberCount
		{
			get
			{
				return this._Member.Count;
			}
		}

		// Token: 0x06004F8E RID: 20366 RVA: 0x000F6983 File Offset: 0x000F4B83
		public void AddMember(Member val)
		{
			this._Member.Add(val);
		}

		// Token: 0x06004F8F RID: 20367 RVA: 0x000F6991 File Offset: 0x000F4B91
		public void ClearMember()
		{
			this._Member.Clear();
		}

		// Token: 0x06004F90 RID: 20368 RVA: 0x000F699E File Offset: 0x000F4B9E
		public void SetMember(List<Member> val)
		{
			this.Member = val;
		}

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x06004F91 RID: 20369 RVA: 0x000F69A7 File Offset: 0x000F4BA7
		// (set) Token: 0x06004F92 RID: 20370 RVA: 0x000F69AF File Offset: 0x000F4BAF
		public List<ChannelInvitation> Invitation
		{
			get
			{
				return this._Invitation;
			}
			set
			{
				this._Invitation = value;
			}
		}

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06004F93 RID: 20371 RVA: 0x000F69A7 File Offset: 0x000F4BA7
		public List<ChannelInvitation> InvitationList
		{
			get
			{
				return this._Invitation;
			}
		}

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06004F94 RID: 20372 RVA: 0x000F69B8 File Offset: 0x000F4BB8
		public int InvitationCount
		{
			get
			{
				return this._Invitation.Count;
			}
		}

		// Token: 0x06004F95 RID: 20373 RVA: 0x000F69C5 File Offset: 0x000F4BC5
		public void AddInvitation(ChannelInvitation val)
		{
			this._Invitation.Add(val);
		}

		// Token: 0x06004F96 RID: 20374 RVA: 0x000F69D3 File Offset: 0x000F4BD3
		public void ClearInvitation()
		{
			this._Invitation.Clear();
		}

		// Token: 0x06004F97 RID: 20375 RVA: 0x000F69E0 File Offset: 0x000F4BE0
		public void SetInvitation(List<ChannelInvitation> val)
		{
			this.Invitation = val;
		}

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06004F98 RID: 20376 RVA: 0x000F69E9 File Offset: 0x000F4BE9
		// (set) Token: 0x06004F99 RID: 20377 RVA: 0x000F69F1 File Offset: 0x000F4BF1
		public ChannelRoleSet RoleSet
		{
			get
			{
				return this._RoleSet;
			}
			set
			{
				this._RoleSet = value;
				this.HasRoleSet = (value != null);
			}
		}

		// Token: 0x06004F9A RID: 20378 RVA: 0x000F6A04 File Offset: 0x000F4C04
		public void SetRoleSet(ChannelRoleSet val)
		{
			this.RoleSet = val;
		}

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06004F9B RID: 20379 RVA: 0x000F6A0D File Offset: 0x000F4C0D
		// (set) Token: 0x06004F9C RID: 20380 RVA: 0x000F6A15 File Offset: 0x000F4C15
		public PublicChannelState PublicChannelState
		{
			get
			{
				return this._PublicChannelState;
			}
			set
			{
				this._PublicChannelState = value;
				this.HasPublicChannelState = (value != null);
			}
		}

		// Token: 0x06004F9D RID: 20381 RVA: 0x000F6A28 File Offset: 0x000F4C28
		public void SetPublicChannelState(PublicChannelState val)
		{
			this.PublicChannelState = val;
		}

		// Token: 0x06004F9E RID: 20382 RVA: 0x000F6A34 File Offset: 0x000F4C34
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasId)
			{
				num ^= this.Id.GetHashCode();
			}
			if (this.HasType)
			{
				num ^= this.Type.GetHashCode();
			}
			if (this.HasName)
			{
				num ^= this.Name.GetHashCode();
			}
			if (this.HasPrivacyLevel)
			{
				num ^= this.PrivacyLevel.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			foreach (Member member in this.Member)
			{
				num ^= member.GetHashCode();
			}
			foreach (ChannelInvitation channelInvitation in this.Invitation)
			{
				num ^= channelInvitation.GetHashCode();
			}
			if (this.HasRoleSet)
			{
				num ^= this.RoleSet.GetHashCode();
			}
			if (this.HasPublicChannelState)
			{
				num ^= this.PublicChannelState.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004F9F RID: 20383 RVA: 0x000F6BB4 File Offset: 0x000F4DB4
		public override bool Equals(object obj)
		{
			Channel channel = obj as Channel;
			if (channel == null)
			{
				return false;
			}
			if (this.HasId != channel.HasId || (this.HasId && !this.Id.Equals(channel.Id)))
			{
				return false;
			}
			if (this.HasType != channel.HasType || (this.HasType && !this.Type.Equals(channel.Type)))
			{
				return false;
			}
			if (this.HasName != channel.HasName || (this.HasName && !this.Name.Equals(channel.Name)))
			{
				return false;
			}
			if (this.HasPrivacyLevel != channel.HasPrivacyLevel || (this.HasPrivacyLevel && !this.PrivacyLevel.Equals(channel.PrivacyLevel)))
			{
				return false;
			}
			if (this.Attribute.Count != channel.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(channel.Attribute[i]))
				{
					return false;
				}
			}
			if (this.Member.Count != channel.Member.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Member.Count; j++)
			{
				if (!this.Member[j].Equals(channel.Member[j]))
				{
					return false;
				}
			}
			if (this.Invitation.Count != channel.Invitation.Count)
			{
				return false;
			}
			for (int k = 0; k < this.Invitation.Count; k++)
			{
				if (!this.Invitation[k].Equals(channel.Invitation[k]))
				{
					return false;
				}
			}
			return this.HasRoleSet == channel.HasRoleSet && (!this.HasRoleSet || this.RoleSet.Equals(channel.RoleSet)) && this.HasPublicChannelState == channel.HasPublicChannelState && (!this.HasPublicChannelState || this.PublicChannelState.Equals(channel.PublicChannelState));
		}

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x06004FA0 RID: 20384 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004FA1 RID: 20385 RVA: 0x000F6DD7 File Offset: 0x000F4FD7
		public static Channel ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Channel>(bs, 0, -1);
		}

		// Token: 0x06004FA2 RID: 20386 RVA: 0x000F6DE1 File Offset: 0x000F4FE1
		public void Deserialize(Stream stream)
		{
			Channel.Deserialize(stream, this);
		}

		// Token: 0x06004FA3 RID: 20387 RVA: 0x000F6DEB File Offset: 0x000F4FEB
		public static Channel Deserialize(Stream stream, Channel instance)
		{
			return Channel.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004FA4 RID: 20388 RVA: 0x000F6DF8 File Offset: 0x000F4FF8
		public static Channel DeserializeLengthDelimited(Stream stream)
		{
			Channel channel = new Channel();
			Channel.DeserializeLengthDelimited(stream, channel);
			return channel;
		}

		// Token: 0x06004FA5 RID: 20389 RVA: 0x000F6E14 File Offset: 0x000F5014
		public static Channel DeserializeLengthDelimited(Stream stream, Channel instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Channel.Deserialize(stream, instance, num);
		}

		// Token: 0x06004FA6 RID: 20390 RVA: 0x000F6E3C File Offset: 0x000F503C
		public static Channel Deserialize(Stream stream, Channel instance, long limit)
		{
			instance.PrivacyLevel = PrivacyLevel.PRIVACY_LEVEL_OPEN;
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.v2.Attribute>();
			}
			if (instance.Member == null)
			{
				instance.Member = new List<Member>();
			}
			if (instance.Invitation == null)
			{
				instance.Invitation = new List<ChannelInvitation>();
			}
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
					if (num <= 32)
					{
						if (num <= 18)
						{
							if (num != 10)
							{
								if (num == 18)
								{
									if (instance.Type == null)
									{
										instance.Type = UniqueChannelType.DeserializeLengthDelimited(stream);
										continue;
									}
									UniqueChannelType.DeserializeLengthDelimited(stream, instance.Type);
									continue;
								}
							}
							else
							{
								if (instance.Id == null)
								{
									instance.Id = ChannelId.DeserializeLengthDelimited(stream);
									continue;
								}
								ChannelId.DeserializeLengthDelimited(stream, instance.Id);
								continue;
							}
						}
						else
						{
							if (num == 26)
							{
								instance.Name = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 32)
							{
								instance.PrivacyLevel = (PrivacyLevel)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
					}
					else if (num <= 50)
					{
						if (num == 42)
						{
							instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 50)
						{
							instance.Member.Add(bnet.protocol.channel.v2.Member.DeserializeLengthDelimited(stream));
							continue;
						}
					}
					else
					{
						if (num == 58)
						{
							instance.Invitation.Add(ChannelInvitation.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 66)
						{
							if (instance.RoleSet == null)
							{
								instance.RoleSet = ChannelRoleSet.DeserializeLengthDelimited(stream);
								continue;
							}
							ChannelRoleSet.DeserializeLengthDelimited(stream, instance.RoleSet);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					if (field != 110U)
					{
						ProtocolParser.SkipKey(stream, key);
					}
					else if (key.WireType == Wire.LengthDelimited)
					{
						if (instance.PublicChannelState == null)
						{
							instance.PublicChannelState = PublicChannelState.DeserializeLengthDelimited(stream);
						}
						else
						{
							PublicChannelState.DeserializeLengthDelimited(stream, instance.PublicChannelState);
						}
					}
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004FA7 RID: 20391 RVA: 0x000F7073 File Offset: 0x000F5273
		public void Serialize(Stream stream)
		{
			Channel.Serialize(stream, this);
		}

		// Token: 0x06004FA8 RID: 20392 RVA: 0x000F707C File Offset: 0x000F527C
		public static void Serialize(Stream stream, Channel instance)
		{
			if (instance.HasId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Id.GetSerializedSize());
				ChannelId.Serialize(stream, instance.Id);
			}
			if (instance.HasType)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Type.GetSerializedSize());
				UniqueChannelType.Serialize(stream, instance.Type);
			}
			if (instance.HasName)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.HasPrivacyLevel)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PrivacyLevel));
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.Member.Count > 0)
			{
				foreach (Member member in instance.Member)
				{
					stream.WriteByte(50);
					ProtocolParser.WriteUInt32(stream, member.GetSerializedSize());
					bnet.protocol.channel.v2.Member.Serialize(stream, member);
				}
			}
			if (instance.Invitation.Count > 0)
			{
				foreach (ChannelInvitation channelInvitation in instance.Invitation)
				{
					stream.WriteByte(58);
					ProtocolParser.WriteUInt32(stream, channelInvitation.GetSerializedSize());
					ChannelInvitation.Serialize(stream, channelInvitation);
				}
			}
			if (instance.HasRoleSet)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteUInt32(stream, instance.RoleSet.GetSerializedSize());
				ChannelRoleSet.Serialize(stream, instance.RoleSet);
			}
			if (instance.HasPublicChannelState)
			{
				stream.WriteByte(242);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.PublicChannelState.GetSerializedSize());
				PublicChannelState.Serialize(stream, instance.PublicChannelState);
			}
		}

		// Token: 0x06004FA9 RID: 20393 RVA: 0x000F72C0 File Offset: 0x000F54C0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasId)
			{
				num += 1U;
				uint serializedSize = this.Id.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasType)
			{
				num += 1U;
				uint serializedSize2 = this.Type.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasPrivacyLevel)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.PrivacyLevel));
			}
			if (this.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize3 = attribute.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (this.Member.Count > 0)
			{
				foreach (Member member in this.Member)
				{
					num += 1U;
					uint serializedSize4 = member.GetSerializedSize();
					num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
				}
			}
			if (this.Invitation.Count > 0)
			{
				foreach (ChannelInvitation channelInvitation in this.Invitation)
				{
					num += 1U;
					uint serializedSize5 = channelInvitation.GetSerializedSize();
					num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
				}
			}
			if (this.HasRoleSet)
			{
				num += 1U;
				uint serializedSize6 = this.RoleSet.GetSerializedSize();
				num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
			}
			if (this.HasPublicChannelState)
			{
				num += 2U;
				uint serializedSize7 = this.PublicChannelState.GetSerializedSize();
				num += serializedSize7 + ProtocolParser.SizeOfUInt32(serializedSize7);
			}
			return num;
		}

		// Token: 0x040019AE RID: 6574
		public bool HasId;

		// Token: 0x040019AF RID: 6575
		private ChannelId _Id;

		// Token: 0x040019B0 RID: 6576
		public bool HasType;

		// Token: 0x040019B1 RID: 6577
		private UniqueChannelType _Type;

		// Token: 0x040019B2 RID: 6578
		public bool HasName;

		// Token: 0x040019B3 RID: 6579
		private string _Name;

		// Token: 0x040019B4 RID: 6580
		public bool HasPrivacyLevel;

		// Token: 0x040019B5 RID: 6581
		private PrivacyLevel _PrivacyLevel;

		// Token: 0x040019B6 RID: 6582
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		// Token: 0x040019B7 RID: 6583
		private List<Member> _Member = new List<Member>();

		// Token: 0x040019B8 RID: 6584
		private List<ChannelInvitation> _Invitation = new List<ChannelInvitation>();

		// Token: 0x040019B9 RID: 6585
		public bool HasRoleSet;

		// Token: 0x040019BA RID: 6586
		private ChannelRoleSet _RoleSet;

		// Token: 0x040019BB RID: 6587
		public bool HasPublicChannelState;

		// Token: 0x040019BC RID: 6588
		private PublicChannelState _PublicChannelState;
	}
}
