using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.friends.v2.client.Types;
using bnet.protocol.v2;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x02000410 RID: 1040
	public class ReceivedInvitation : IProtoBuf
	{
		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06004521 RID: 17697 RVA: 0x000D91EA File Offset: 0x000D73EA
		// (set) Token: 0x06004522 RID: 17698 RVA: 0x000D91F2 File Offset: 0x000D73F2
		public ulong Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.HasId = true;
			}
		}

		// Token: 0x06004523 RID: 17699 RVA: 0x000D9202 File Offset: 0x000D7402
		public void SetId(ulong val)
		{
			this.Id = val;
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06004524 RID: 17700 RVA: 0x000D920B File Offset: 0x000D740B
		// (set) Token: 0x06004525 RID: 17701 RVA: 0x000D9213 File Offset: 0x000D7413
		public UserDescription Inviter
		{
			get
			{
				return this._Inviter;
			}
			set
			{
				this._Inviter = value;
				this.HasInviter = (value != null);
			}
		}

		// Token: 0x06004526 RID: 17702 RVA: 0x000D9226 File Offset: 0x000D7426
		public void SetInviter(UserDescription val)
		{
			this.Inviter = val;
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06004527 RID: 17703 RVA: 0x000D922F File Offset: 0x000D742F
		// (set) Token: 0x06004528 RID: 17704 RVA: 0x000D9237 File Offset: 0x000D7437
		public UserDescription Invitee
		{
			get
			{
				return this._Invitee;
			}
			set
			{
				this._Invitee = value;
				this.HasInvitee = (value != null);
			}
		}

		// Token: 0x06004529 RID: 17705 RVA: 0x000D924A File Offset: 0x000D744A
		public void SetInvitee(UserDescription val)
		{
			this.Invitee = val;
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x0600452A RID: 17706 RVA: 0x000D9253 File Offset: 0x000D7453
		// (set) Token: 0x0600452B RID: 17707 RVA: 0x000D925B File Offset: 0x000D745B
		public FriendLevel Level
		{
			get
			{
				return this._Level;
			}
			set
			{
				this._Level = value;
				this.HasLevel = true;
			}
		}

		// Token: 0x0600452C RID: 17708 RVA: 0x000D926B File Offset: 0x000D746B
		public void SetLevel(FriendLevel val)
		{
			this.Level = val;
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x0600452D RID: 17709 RVA: 0x000D9274 File Offset: 0x000D7474
		// (set) Token: 0x0600452E RID: 17710 RVA: 0x000D927C File Offset: 0x000D747C
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

		// Token: 0x0600452F RID: 17711 RVA: 0x000D928C File Offset: 0x000D748C
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06004530 RID: 17712 RVA: 0x000D9295 File Offset: 0x000D7495
		// (set) Token: 0x06004531 RID: 17713 RVA: 0x000D929D File Offset: 0x000D749D
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

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06004532 RID: 17714 RVA: 0x000D9295 File Offset: 0x000D7495
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x06004533 RID: 17715 RVA: 0x000D92A6 File Offset: 0x000D74A6
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06004534 RID: 17716 RVA: 0x000D92B3 File Offset: 0x000D74B3
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06004535 RID: 17717 RVA: 0x000D92C1 File Offset: 0x000D74C1
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06004536 RID: 17718 RVA: 0x000D92CE File Offset: 0x000D74CE
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x06004537 RID: 17719 RVA: 0x000D92D7 File Offset: 0x000D74D7
		// (set) Token: 0x06004538 RID: 17720 RVA: 0x000D92DF File Offset: 0x000D74DF
		public ulong CreationTimeUs
		{
			get
			{
				return this._CreationTimeUs;
			}
			set
			{
				this._CreationTimeUs = value;
				this.HasCreationTimeUs = true;
			}
		}

		// Token: 0x06004539 RID: 17721 RVA: 0x000D92EF File Offset: 0x000D74EF
		public void SetCreationTimeUs(ulong val)
		{
			this.CreationTimeUs = val;
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x0600453A RID: 17722 RVA: 0x000D92F8 File Offset: 0x000D74F8
		// (set) Token: 0x0600453B RID: 17723 RVA: 0x000D9300 File Offset: 0x000D7500
		public ulong ModifiedTimeUs
		{
			get
			{
				return this._ModifiedTimeUs;
			}
			set
			{
				this._ModifiedTimeUs = value;
				this.HasModifiedTimeUs = true;
			}
		}

		// Token: 0x0600453C RID: 17724 RVA: 0x000D9310 File Offset: 0x000D7510
		public void SetModifiedTimeUs(ulong val)
		{
			this.ModifiedTimeUs = val;
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x0600453D RID: 17725 RVA: 0x000D9319 File Offset: 0x000D7519
		// (set) Token: 0x0600453E RID: 17726 RVA: 0x000D9321 File Offset: 0x000D7521
		public ulong ExpirationTimeUs
		{
			get
			{
				return this._ExpirationTimeUs;
			}
			set
			{
				this._ExpirationTimeUs = value;
				this.HasExpirationTimeUs = true;
			}
		}

		// Token: 0x0600453F RID: 17727 RVA: 0x000D9331 File Offset: 0x000D7531
		public void SetExpirationTimeUs(ulong val)
		{
			this.ExpirationTimeUs = val;
		}

		// Token: 0x06004540 RID: 17728 RVA: 0x000D933C File Offset: 0x000D753C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasId)
			{
				num ^= this.Id.GetHashCode();
			}
			if (this.HasInviter)
			{
				num ^= this.Inviter.GetHashCode();
			}
			if (this.HasInvitee)
			{
				num ^= this.Invitee.GetHashCode();
			}
			if (this.HasLevel)
			{
				num ^= this.Level.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			if (this.HasCreationTimeUs)
			{
				num ^= this.CreationTimeUs.GetHashCode();
			}
			if (this.HasModifiedTimeUs)
			{
				num ^= this.ModifiedTimeUs.GetHashCode();
			}
			if (this.HasExpirationTimeUs)
			{
				num ^= this.ExpirationTimeUs.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004541 RID: 17729 RVA: 0x000D946C File Offset: 0x000D766C
		public override bool Equals(object obj)
		{
			ReceivedInvitation receivedInvitation = obj as ReceivedInvitation;
			if (receivedInvitation == null)
			{
				return false;
			}
			if (this.HasId != receivedInvitation.HasId || (this.HasId && !this.Id.Equals(receivedInvitation.Id)))
			{
				return false;
			}
			if (this.HasInviter != receivedInvitation.HasInviter || (this.HasInviter && !this.Inviter.Equals(receivedInvitation.Inviter)))
			{
				return false;
			}
			if (this.HasInvitee != receivedInvitation.HasInvitee || (this.HasInvitee && !this.Invitee.Equals(receivedInvitation.Invitee)))
			{
				return false;
			}
			if (this.HasLevel != receivedInvitation.HasLevel || (this.HasLevel && !this.Level.Equals(receivedInvitation.Level)))
			{
				return false;
			}
			if (this.HasProgram != receivedInvitation.HasProgram || (this.HasProgram && !this.Program.Equals(receivedInvitation.Program)))
			{
				return false;
			}
			if (this.Attribute.Count != receivedInvitation.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(receivedInvitation.Attribute[i]))
				{
					return false;
				}
			}
			return this.HasCreationTimeUs == receivedInvitation.HasCreationTimeUs && (!this.HasCreationTimeUs || this.CreationTimeUs.Equals(receivedInvitation.CreationTimeUs)) && this.HasModifiedTimeUs == receivedInvitation.HasModifiedTimeUs && (!this.HasModifiedTimeUs || this.ModifiedTimeUs.Equals(receivedInvitation.ModifiedTimeUs)) && this.HasExpirationTimeUs == receivedInvitation.HasExpirationTimeUs && (!this.HasExpirationTimeUs || this.ExpirationTimeUs.Equals(receivedInvitation.ExpirationTimeUs));
		}

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06004542 RID: 17730 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004543 RID: 17731 RVA: 0x000D9652 File Offset: 0x000D7852
		public static ReceivedInvitation ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ReceivedInvitation>(bs, 0, -1);
		}

		// Token: 0x06004544 RID: 17732 RVA: 0x000D965C File Offset: 0x000D785C
		public void Deserialize(Stream stream)
		{
			ReceivedInvitation.Deserialize(stream, this);
		}

		// Token: 0x06004545 RID: 17733 RVA: 0x000D9666 File Offset: 0x000D7866
		public static ReceivedInvitation Deserialize(Stream stream, ReceivedInvitation instance)
		{
			return ReceivedInvitation.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004546 RID: 17734 RVA: 0x000D9674 File Offset: 0x000D7874
		public static ReceivedInvitation DeserializeLengthDelimited(Stream stream)
		{
			ReceivedInvitation receivedInvitation = new ReceivedInvitation();
			ReceivedInvitation.DeserializeLengthDelimited(stream, receivedInvitation);
			return receivedInvitation;
		}

		// Token: 0x06004547 RID: 17735 RVA: 0x000D9690 File Offset: 0x000D7890
		public static ReceivedInvitation DeserializeLengthDelimited(Stream stream, ReceivedInvitation instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ReceivedInvitation.Deserialize(stream, instance, num);
		}

		// Token: 0x06004548 RID: 17736 RVA: 0x000D96B8 File Offset: 0x000D78B8
		public static ReceivedInvitation Deserialize(Stream stream, ReceivedInvitation instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.Level = FriendLevel.FRIEND_LEVEL_BATTLE_TAG;
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.v2.Attribute>();
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
							if (num == 8)
							{
								instance.Id = ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 18)
							{
								if (instance.Inviter == null)
								{
									instance.Inviter = UserDescription.DeserializeLengthDelimited(stream);
									continue;
								}
								UserDescription.DeserializeLengthDelimited(stream, instance.Inviter);
								continue;
							}
						}
						else if (num != 26)
						{
							if (num == 32)
							{
								instance.Level = (FriendLevel)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (instance.Invitee == null)
							{
								instance.Invitee = UserDescription.DeserializeLengthDelimited(stream);
								continue;
							}
							UserDescription.DeserializeLengthDelimited(stream, instance.Invitee);
							continue;
						}
					}
					else if (num <= 50)
					{
						if (num == 45)
						{
							instance.Program = binaryReader.ReadUInt32();
							continue;
						}
						if (num == 50)
						{
							instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
							continue;
						}
					}
					else
					{
						if (num == 56)
						{
							instance.CreationTimeUs = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 64)
						{
							instance.ModifiedTimeUs = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 72)
						{
							instance.ExpirationTimeUs = ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06004549 RID: 17737 RVA: 0x000D9886 File Offset: 0x000D7A86
		public void Serialize(Stream stream)
		{
			ReceivedInvitation.Serialize(stream, this);
		}

		// Token: 0x0600454A RID: 17738 RVA: 0x000D9890 File Offset: 0x000D7A90
		public static void Serialize(Stream stream, ReceivedInvitation instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.Id);
			}
			if (instance.HasInviter)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Inviter.GetSerializedSize());
				UserDescription.Serialize(stream, instance.Inviter);
			}
			if (instance.HasInvitee)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Invitee.GetSerializedSize());
				UserDescription.Serialize(stream, instance.Invitee);
			}
			if (instance.HasLevel)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Level));
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.Program);
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(50);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.HasCreationTimeUs)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, instance.CreationTimeUs);
			}
			if (instance.HasModifiedTimeUs)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, instance.ModifiedTimeUs);
			}
			if (instance.HasExpirationTimeUs)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, instance.ExpirationTimeUs);
			}
		}

		// Token: 0x0600454B RID: 17739 RVA: 0x000D9A10 File Offset: 0x000D7C10
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Id);
			}
			if (this.HasInviter)
			{
				num += 1U;
				uint serializedSize = this.Inviter.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasInvitee)
			{
				num += 1U;
				uint serializedSize2 = this.Invitee.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasLevel)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Level));
			}
			if (this.HasProgram)
			{
				num += 1U;
				num += 4U;
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
			if (this.HasCreationTimeUs)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.CreationTimeUs);
			}
			if (this.HasModifiedTimeUs)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.ModifiedTimeUs);
			}
			if (this.HasExpirationTimeUs)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.ExpirationTimeUs);
			}
			return num;
		}

		// Token: 0x04001745 RID: 5957
		public bool HasId;

		// Token: 0x04001746 RID: 5958
		private ulong _Id;

		// Token: 0x04001747 RID: 5959
		public bool HasInviter;

		// Token: 0x04001748 RID: 5960
		private UserDescription _Inviter;

		// Token: 0x04001749 RID: 5961
		public bool HasInvitee;

		// Token: 0x0400174A RID: 5962
		private UserDescription _Invitee;

		// Token: 0x0400174B RID: 5963
		public bool HasLevel;

		// Token: 0x0400174C RID: 5964
		private FriendLevel _Level;

		// Token: 0x0400174D RID: 5965
		public bool HasProgram;

		// Token: 0x0400174E RID: 5966
		private uint _Program;

		// Token: 0x0400174F RID: 5967
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		// Token: 0x04001750 RID: 5968
		public bool HasCreationTimeUs;

		// Token: 0x04001751 RID: 5969
		private ulong _CreationTimeUs;

		// Token: 0x04001752 RID: 5970
		public bool HasModifiedTimeUs;

		// Token: 0x04001753 RID: 5971
		private ulong _ModifiedTimeUs;

		// Token: 0x04001754 RID: 5972
		public bool HasExpirationTimeUs;

		// Token: 0x04001755 RID: 5973
		private ulong _ExpirationTimeUs;
	}
}
