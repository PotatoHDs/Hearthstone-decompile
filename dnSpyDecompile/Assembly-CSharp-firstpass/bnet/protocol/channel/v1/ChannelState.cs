using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.presence.v1;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004D6 RID: 1238
	public class ChannelState : IProtoBuf
	{
		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x06005728 RID: 22312 RVA: 0x0010B57B File Offset: 0x0010977B
		// (set) Token: 0x06005729 RID: 22313 RVA: 0x0010B583 File Offset: 0x00109783
		public uint MaxMembers
		{
			get
			{
				return this._MaxMembers;
			}
			set
			{
				this._MaxMembers = value;
				this.HasMaxMembers = true;
			}
		}

		// Token: 0x0600572A RID: 22314 RVA: 0x0010B593 File Offset: 0x00109793
		public void SetMaxMembers(uint val)
		{
			this.MaxMembers = val;
		}

		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x0600572B RID: 22315 RVA: 0x0010B59C File Offset: 0x0010979C
		// (set) Token: 0x0600572C RID: 22316 RVA: 0x0010B5A4 File Offset: 0x001097A4
		public uint MinMembers
		{
			get
			{
				return this._MinMembers;
			}
			set
			{
				this._MinMembers = value;
				this.HasMinMembers = true;
			}
		}

		// Token: 0x0600572D RID: 22317 RVA: 0x0010B5B4 File Offset: 0x001097B4
		public void SetMinMembers(uint val)
		{
			this.MinMembers = val;
		}

		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x0600572E RID: 22318 RVA: 0x0010B5BD File Offset: 0x001097BD
		// (set) Token: 0x0600572F RID: 22319 RVA: 0x0010B5C5 File Offset: 0x001097C5
		public List<Attribute> Attribute
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

		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x06005730 RID: 22320 RVA: 0x0010B5BD File Offset: 0x001097BD
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x06005731 RID: 22321 RVA: 0x0010B5CE File Offset: 0x001097CE
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06005732 RID: 22322 RVA: 0x0010B5DB File Offset: 0x001097DB
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06005733 RID: 22323 RVA: 0x0010B5E9 File Offset: 0x001097E9
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06005734 RID: 22324 RVA: 0x0010B5F6 File Offset: 0x001097F6
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x1700106C RID: 4204
		// (get) Token: 0x06005735 RID: 22325 RVA: 0x0010B5FF File Offset: 0x001097FF
		// (set) Token: 0x06005736 RID: 22326 RVA: 0x0010B607 File Offset: 0x00109807
		public List<Invitation> Invitation
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

		// Token: 0x1700106D RID: 4205
		// (get) Token: 0x06005737 RID: 22327 RVA: 0x0010B5FF File Offset: 0x001097FF
		public List<Invitation> InvitationList
		{
			get
			{
				return this._Invitation;
			}
		}

		// Token: 0x1700106E RID: 4206
		// (get) Token: 0x06005738 RID: 22328 RVA: 0x0010B610 File Offset: 0x00109810
		public int InvitationCount
		{
			get
			{
				return this._Invitation.Count;
			}
		}

		// Token: 0x06005739 RID: 22329 RVA: 0x0010B61D File Offset: 0x0010981D
		public void AddInvitation(Invitation val)
		{
			this._Invitation.Add(val);
		}

		// Token: 0x0600573A RID: 22330 RVA: 0x0010B62B File Offset: 0x0010982B
		public void ClearInvitation()
		{
			this._Invitation.Clear();
		}

		// Token: 0x0600573B RID: 22331 RVA: 0x0010B638 File Offset: 0x00109838
		public void SetInvitation(List<Invitation> val)
		{
			this.Invitation = val;
		}

		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x0600573C RID: 22332 RVA: 0x0010B641 File Offset: 0x00109841
		// (set) Token: 0x0600573D RID: 22333 RVA: 0x0010B649 File Offset: 0x00109849
		public uint Reason
		{
			get
			{
				return this._Reason;
			}
			set
			{
				this._Reason = value;
				this.HasReason = true;
			}
		}

		// Token: 0x0600573E RID: 22334 RVA: 0x0010B659 File Offset: 0x00109859
		public void SetReason(uint val)
		{
			this.Reason = val;
		}

		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x0600573F RID: 22335 RVA: 0x0010B662 File Offset: 0x00109862
		// (set) Token: 0x06005740 RID: 22336 RVA: 0x0010B66A File Offset: 0x0010986A
		public ChannelState.Types.PrivacyLevel PrivacyLevel
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

		// Token: 0x06005741 RID: 22337 RVA: 0x0010B67A File Offset: 0x0010987A
		public void SetPrivacyLevel(ChannelState.Types.PrivacyLevel val)
		{
			this.PrivacyLevel = val;
		}

		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x06005742 RID: 22338 RVA: 0x0010B683 File Offset: 0x00109883
		// (set) Token: 0x06005743 RID: 22339 RVA: 0x0010B68B File Offset: 0x0010988B
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

		// Token: 0x06005744 RID: 22340 RVA: 0x0010B69E File Offset: 0x0010989E
		public void SetName(string val)
		{
			this.Name = val;
		}

		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x06005745 RID: 22341 RVA: 0x0010B6A7 File Offset: 0x001098A7
		// (set) Token: 0x06005746 RID: 22342 RVA: 0x0010B6AF File Offset: 0x001098AF
		public string ChannelType
		{
			get
			{
				return this._ChannelType;
			}
			set
			{
				this._ChannelType = value;
				this.HasChannelType = (value != null);
			}
		}

		// Token: 0x06005747 RID: 22343 RVA: 0x0010B6C2 File Offset: 0x001098C2
		public void SetChannelType(string val)
		{
			this.ChannelType = val;
		}

		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x06005748 RID: 22344 RVA: 0x0010B6CB File Offset: 0x001098CB
		// (set) Token: 0x06005749 RID: 22345 RVA: 0x0010B6D3 File Offset: 0x001098D3
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

		// Token: 0x0600574A RID: 22346 RVA: 0x0010B6E3 File Offset: 0x001098E3
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x0600574B RID: 22347 RVA: 0x0010B6EC File Offset: 0x001098EC
		// (set) Token: 0x0600574C RID: 22348 RVA: 0x0010B6F4 File Offset: 0x001098F4
		public bool SubscribeToPresence
		{
			get
			{
				return this._SubscribeToPresence;
			}
			set
			{
				this._SubscribeToPresence = value;
				this.HasSubscribeToPresence = true;
			}
		}

		// Token: 0x0600574D RID: 22349 RVA: 0x0010B704 File Offset: 0x00109904
		public void SetSubscribeToPresence(bool val)
		{
			this.SubscribeToPresence = val;
		}

		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x0600574E RID: 22350 RVA: 0x0010B70D File Offset: 0x0010990D
		// (set) Token: 0x0600574F RID: 22351 RVA: 0x0010B715 File Offset: 0x00109915
		public ChatChannelState ChannelState_
		{
			get
			{
				return this._ChannelState_;
			}
			set
			{
				this._ChannelState_ = value;
				this.HasChannelState_ = (value != null);
			}
		}

		// Token: 0x06005750 RID: 22352 RVA: 0x0010B728 File Offset: 0x00109928
		public void SetChannelState_(ChatChannelState val)
		{
			this.ChannelState_ = val;
		}

		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x06005751 RID: 22353 RVA: 0x0010B731 File Offset: 0x00109931
		// (set) Token: 0x06005752 RID: 22354 RVA: 0x0010B739 File Offset: 0x00109939
		public ChannelState Presence
		{
			get
			{
				return this._Presence;
			}
			set
			{
				this._Presence = value;
				this.HasPresence = (value != null);
			}
		}

		// Token: 0x06005753 RID: 22355 RVA: 0x0010B74C File Offset: 0x0010994C
		public void SetPresence(ChannelState val)
		{
			this.Presence = val;
		}

		// Token: 0x06005754 RID: 22356 RVA: 0x0010B758 File Offset: 0x00109958
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMaxMembers)
			{
				num ^= this.MaxMembers.GetHashCode();
			}
			if (this.HasMinMembers)
			{
				num ^= this.MinMembers.GetHashCode();
			}
			foreach (Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			foreach (Invitation invitation in this.Invitation)
			{
				num ^= invitation.GetHashCode();
			}
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			if (this.HasPrivacyLevel)
			{
				num ^= this.PrivacyLevel.GetHashCode();
			}
			if (this.HasName)
			{
				num ^= this.Name.GetHashCode();
			}
			if (this.HasChannelType)
			{
				num ^= this.ChannelType.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			if (this.HasSubscribeToPresence)
			{
				num ^= this.SubscribeToPresence.GetHashCode();
			}
			if (this.HasChannelState_)
			{
				num ^= this.ChannelState_.GetHashCode();
			}
			if (this.HasPresence)
			{
				num ^= this.Presence.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005755 RID: 22357 RVA: 0x0010B8F8 File Offset: 0x00109AF8
		public override bool Equals(object obj)
		{
			ChannelState channelState = obj as ChannelState;
			if (channelState == null)
			{
				return false;
			}
			if (this.HasMaxMembers != channelState.HasMaxMembers || (this.HasMaxMembers && !this.MaxMembers.Equals(channelState.MaxMembers)))
			{
				return false;
			}
			if (this.HasMinMembers != channelState.HasMinMembers || (this.HasMinMembers && !this.MinMembers.Equals(channelState.MinMembers)))
			{
				return false;
			}
			if (this.Attribute.Count != channelState.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(channelState.Attribute[i]))
				{
					return false;
				}
			}
			if (this.Invitation.Count != channelState.Invitation.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Invitation.Count; j++)
			{
				if (!this.Invitation[j].Equals(channelState.Invitation[j]))
				{
					return false;
				}
			}
			return this.HasReason == channelState.HasReason && (!this.HasReason || this.Reason.Equals(channelState.Reason)) && this.HasPrivacyLevel == channelState.HasPrivacyLevel && (!this.HasPrivacyLevel || this.PrivacyLevel.Equals(channelState.PrivacyLevel)) && this.HasName == channelState.HasName && (!this.HasName || this.Name.Equals(channelState.Name)) && this.HasChannelType == channelState.HasChannelType && (!this.HasChannelType || this.ChannelType.Equals(channelState.ChannelType)) && this.HasProgram == channelState.HasProgram && (!this.HasProgram || this.Program.Equals(channelState.Program)) && this.HasSubscribeToPresence == channelState.HasSubscribeToPresence && (!this.HasSubscribeToPresence || this.SubscribeToPresence.Equals(channelState.SubscribeToPresence)) && this.HasChannelState_ == channelState.HasChannelState_ && (!this.HasChannelState_ || this.ChannelState_.Equals(channelState.ChannelState_)) && this.HasPresence == channelState.HasPresence && (!this.HasPresence || this.Presence.Equals(channelState.Presence));
		}

		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x06005756 RID: 22358 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005757 RID: 22359 RVA: 0x0010BB81 File Offset: 0x00109D81
		public static ChannelState ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelState>(bs, 0, -1);
		}

		// Token: 0x06005758 RID: 22360 RVA: 0x0010BB8B File Offset: 0x00109D8B
		public void Deserialize(Stream stream)
		{
			ChannelState.Deserialize(stream, this);
		}

		// Token: 0x06005759 RID: 22361 RVA: 0x0010BB95 File Offset: 0x00109D95
		public static ChannelState Deserialize(Stream stream, ChannelState instance)
		{
			return ChannelState.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600575A RID: 22362 RVA: 0x0010BBA0 File Offset: 0x00109DA0
		public static ChannelState DeserializeLengthDelimited(Stream stream)
		{
			ChannelState channelState = new ChannelState();
			ChannelState.DeserializeLengthDelimited(stream, channelState);
			return channelState;
		}

		// Token: 0x0600575B RID: 22363 RVA: 0x0010BBBC File Offset: 0x00109DBC
		public static ChannelState DeserializeLengthDelimited(Stream stream, ChannelState instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChannelState.Deserialize(stream, instance, num);
		}

		// Token: 0x0600575C RID: 22364 RVA: 0x0010BBE4 File Offset: 0x00109DE4
		public static ChannelState Deserialize(Stream stream, ChannelState instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
			}
			if (instance.Invitation == null)
			{
				instance.Invitation = new List<Invitation>();
			}
			instance.PrivacyLevel = ChannelState.Types.PrivacyLevel.PRIVACY_LEVEL_OPEN;
			instance.ChannelType = "default";
			instance.SubscribeToPresence = true;
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
					if (num <= 48)
					{
						if (num <= 16)
						{
							if (num == 8)
							{
								instance.MaxMembers = ProtocolParser.ReadUInt32(stream);
								continue;
							}
							if (num == 16)
							{
								instance.MinMembers = ProtocolParser.ReadUInt32(stream);
								continue;
							}
						}
						else
						{
							if (num == 26)
							{
								instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
								continue;
							}
							if (num == 34)
							{
								instance.Invitation.Add(bnet.protocol.Invitation.DeserializeLengthDelimited(stream));
								continue;
							}
							if (num == 48)
							{
								instance.Reason = ProtocolParser.ReadUInt32(stream);
								continue;
							}
						}
					}
					else if (num <= 66)
					{
						if (num == 56)
						{
							instance.PrivacyLevel = (ChannelState.Types.PrivacyLevel)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 66)
						{
							instance.Name = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 82)
						{
							instance.ChannelType = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 93)
						{
							instance.Program = binaryReader.ReadUInt32();
							continue;
						}
						if (num == 104)
						{
							instance.SubscribeToPresence = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					if (field != 100U)
					{
						if (field != 101U)
						{
							ProtocolParser.SkipKey(stream, key);
						}
						else if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.Presence == null)
							{
								instance.Presence = ChannelState.DeserializeLengthDelimited(stream);
							}
							else
							{
								ChannelState.DeserializeLengthDelimited(stream, instance.Presence);
							}
						}
					}
					else if (key.WireType == Wire.LengthDelimited)
					{
						if (instance.ChannelState_ == null)
						{
							instance.ChannelState_ = ChatChannelState.DeserializeLengthDelimited(stream);
						}
						else
						{
							ChatChannelState.DeserializeLengthDelimited(stream, instance.ChannelState_);
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

		// Token: 0x0600575D RID: 22365 RVA: 0x0010BE35 File Offset: 0x0010A035
		public void Serialize(Stream stream)
		{
			ChannelState.Serialize(stream, this);
		}

		// Token: 0x0600575E RID: 22366 RVA: 0x0010BE40 File Offset: 0x0010A040
		public static void Serialize(Stream stream, ChannelState instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasMaxMembers)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.MaxMembers);
			}
			if (instance.HasMinMembers)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.MinMembers);
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.Invitation.Count > 0)
			{
				foreach (Invitation invitation in instance.Invitation)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, invitation.GetSerializedSize());
					bnet.protocol.Invitation.Serialize(stream, invitation);
				}
			}
			if (instance.HasReason)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
			}
			if (instance.HasPrivacyLevel)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PrivacyLevel));
			}
			if (instance.HasName)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.HasChannelType)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ChannelType));
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(93);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasSubscribeToPresence)
			{
				stream.WriteByte(104);
				ProtocolParser.WriteBool(stream, instance.SubscribeToPresence);
			}
			if (instance.HasChannelState_)
			{
				stream.WriteByte(162);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.ChannelState_.GetSerializedSize());
				ChatChannelState.Serialize(stream, instance.ChannelState_);
			}
			if (instance.HasPresence)
			{
				stream.WriteByte(170);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.Presence.GetSerializedSize());
				ChannelState.Serialize(stream, instance.Presence);
			}
		}

		// Token: 0x0600575F RID: 22367 RVA: 0x0010C088 File Offset: 0x0010A288
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasMaxMembers)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.MaxMembers);
			}
			if (this.HasMinMembers)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.MinMembers);
			}
			if (this.Attribute.Count > 0)
			{
				foreach (Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.Invitation.Count > 0)
			{
				foreach (Invitation invitation in this.Invitation)
				{
					num += 1U;
					uint serializedSize2 = invitation.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasReason)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Reason);
			}
			if (this.HasPrivacyLevel)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.PrivacyLevel));
			}
			if (this.HasName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasChannelType)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.ChannelType);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasProgram)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasSubscribeToPresence)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasChannelState_)
			{
				num += 2U;
				uint serializedSize3 = this.ChannelState_.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasPresence)
			{
				num += 2U;
				uint serializedSize4 = this.Presence.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num;
		}

		// Token: 0x04001B65 RID: 7013
		public bool HasMaxMembers;

		// Token: 0x04001B66 RID: 7014
		private uint _MaxMembers;

		// Token: 0x04001B67 RID: 7015
		public bool HasMinMembers;

		// Token: 0x04001B68 RID: 7016
		private uint _MinMembers;

		// Token: 0x04001B69 RID: 7017
		private List<Attribute> _Attribute = new List<Attribute>();

		// Token: 0x04001B6A RID: 7018
		private List<Invitation> _Invitation = new List<Invitation>();

		// Token: 0x04001B6B RID: 7019
		public bool HasReason;

		// Token: 0x04001B6C RID: 7020
		private uint _Reason;

		// Token: 0x04001B6D RID: 7021
		public bool HasPrivacyLevel;

		// Token: 0x04001B6E RID: 7022
		private ChannelState.Types.PrivacyLevel _PrivacyLevel;

		// Token: 0x04001B6F RID: 7023
		public bool HasName;

		// Token: 0x04001B70 RID: 7024
		private string _Name;

		// Token: 0x04001B71 RID: 7025
		public bool HasChannelType;

		// Token: 0x04001B72 RID: 7026
		private string _ChannelType;

		// Token: 0x04001B73 RID: 7027
		public bool HasProgram;

		// Token: 0x04001B74 RID: 7028
		private uint _Program;

		// Token: 0x04001B75 RID: 7029
		public bool HasSubscribeToPresence;

		// Token: 0x04001B76 RID: 7030
		private bool _SubscribeToPresence;

		// Token: 0x04001B77 RID: 7031
		public bool HasChannelState_;

		// Token: 0x04001B78 RID: 7032
		private ChatChannelState _ChannelState_;

		// Token: 0x04001B79 RID: 7033
		public bool HasPresence;

		// Token: 0x04001B7A RID: 7034
		private ChannelState _Presence;

		// Token: 0x020006FE RID: 1790
		public static class Types
		{
			// Token: 0x02000715 RID: 1813
			public enum PrivacyLevel
			{
				// Token: 0x04002307 RID: 8967
				PRIVACY_LEVEL_OPEN = 1,
				// Token: 0x04002308 RID: 8968
				PRIVACY_LEVEL_OPEN_INVITATION_AND_FRIEND,
				// Token: 0x04002309 RID: 8969
				PRIVACY_LEVEL_OPEN_INVITATION,
				// Token: 0x0400230A RID: 8970
				PRIVACY_LEVEL_CLOSED
			}
		}
	}
}
