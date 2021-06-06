using System;
using System.IO;

namespace bnet.protocol.friends.v1
{
	// Token: 0x0200042C RID: 1068
	public class SentInvitationAddedNotification : IProtoBuf
	{
		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x0600479C RID: 18332 RVA: 0x000DFE5F File Offset: 0x000DE05F
		// (set) Token: 0x0600479D RID: 18333 RVA: 0x000DFE67 File Offset: 0x000DE067
		public EntityId AccountId
		{
			get
			{
				return this._AccountId;
			}
			set
			{
				this._AccountId = value;
				this.HasAccountId = (value != null);
			}
		}

		// Token: 0x0600479E RID: 18334 RVA: 0x000DFE7A File Offset: 0x000DE07A
		public void SetAccountId(EntityId val)
		{
			this.AccountId = val;
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x0600479F RID: 18335 RVA: 0x000DFE83 File Offset: 0x000DE083
		// (set) Token: 0x060047A0 RID: 18336 RVA: 0x000DFE8B File Offset: 0x000DE08B
		public SentInvitation Invitation
		{
			get
			{
				return this._Invitation;
			}
			set
			{
				this._Invitation = value;
				this.HasInvitation = (value != null);
			}
		}

		// Token: 0x060047A1 RID: 18337 RVA: 0x000DFE9E File Offset: 0x000DE09E
		public void SetInvitation(SentInvitation val)
		{
			this.Invitation = val;
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x060047A2 RID: 18338 RVA: 0x000DFEA7 File Offset: 0x000DE0A7
		// (set) Token: 0x060047A3 RID: 18339 RVA: 0x000DFEAF File Offset: 0x000DE0AF
		public ObjectAddress Forward
		{
			get
			{
				return this._Forward;
			}
			set
			{
				this._Forward = value;
				this.HasForward = (value != null);
			}
		}

		// Token: 0x060047A4 RID: 18340 RVA: 0x000DFEC2 File Offset: 0x000DE0C2
		public void SetForward(ObjectAddress val)
		{
			this.Forward = val;
		}

		// Token: 0x060047A5 RID: 18341 RVA: 0x000DFECC File Offset: 0x000DE0CC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAccountId)
			{
				num ^= this.AccountId.GetHashCode();
			}
			if (this.HasInvitation)
			{
				num ^= this.Invitation.GetHashCode();
			}
			if (this.HasForward)
			{
				num ^= this.Forward.GetHashCode();
			}
			return num;
		}

		// Token: 0x060047A6 RID: 18342 RVA: 0x000DFF28 File Offset: 0x000DE128
		public override bool Equals(object obj)
		{
			SentInvitationAddedNotification sentInvitationAddedNotification = obj as SentInvitationAddedNotification;
			return sentInvitationAddedNotification != null && this.HasAccountId == sentInvitationAddedNotification.HasAccountId && (!this.HasAccountId || this.AccountId.Equals(sentInvitationAddedNotification.AccountId)) && this.HasInvitation == sentInvitationAddedNotification.HasInvitation && (!this.HasInvitation || this.Invitation.Equals(sentInvitationAddedNotification.Invitation)) && this.HasForward == sentInvitationAddedNotification.HasForward && (!this.HasForward || this.Forward.Equals(sentInvitationAddedNotification.Forward));
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x060047A7 RID: 18343 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060047A8 RID: 18344 RVA: 0x000DFFC3 File Offset: 0x000DE1C3
		public static SentInvitationAddedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SentInvitationAddedNotification>(bs, 0, -1);
		}

		// Token: 0x060047A9 RID: 18345 RVA: 0x000DFFCD File Offset: 0x000DE1CD
		public void Deserialize(Stream stream)
		{
			SentInvitationAddedNotification.Deserialize(stream, this);
		}

		// Token: 0x060047AA RID: 18346 RVA: 0x000DFFD7 File Offset: 0x000DE1D7
		public static SentInvitationAddedNotification Deserialize(Stream stream, SentInvitationAddedNotification instance)
		{
			return SentInvitationAddedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060047AB RID: 18347 RVA: 0x000DFFE4 File Offset: 0x000DE1E4
		public static SentInvitationAddedNotification DeserializeLengthDelimited(Stream stream)
		{
			SentInvitationAddedNotification sentInvitationAddedNotification = new SentInvitationAddedNotification();
			SentInvitationAddedNotification.DeserializeLengthDelimited(stream, sentInvitationAddedNotification);
			return sentInvitationAddedNotification;
		}

		// Token: 0x060047AC RID: 18348 RVA: 0x000E0000 File Offset: 0x000DE200
		public static SentInvitationAddedNotification DeserializeLengthDelimited(Stream stream, SentInvitationAddedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SentInvitationAddedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x060047AD RID: 18349 RVA: 0x000E0028 File Offset: 0x000DE228
		public static SentInvitationAddedNotification Deserialize(Stream stream, SentInvitationAddedNotification instance, long limit)
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
						else if (instance.Forward == null)
						{
							instance.Forward = ObjectAddress.DeserializeLengthDelimited(stream);
						}
						else
						{
							ObjectAddress.DeserializeLengthDelimited(stream, instance.Forward);
						}
					}
					else if (instance.Invitation == null)
					{
						instance.Invitation = SentInvitation.DeserializeLengthDelimited(stream);
					}
					else
					{
						SentInvitation.DeserializeLengthDelimited(stream, instance.Invitation);
					}
				}
				else if (instance.AccountId == null)
				{
					instance.AccountId = EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					EntityId.DeserializeLengthDelimited(stream, instance.AccountId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060047AE RID: 18350 RVA: 0x000E012A File Offset: 0x000DE32A
		public void Serialize(Stream stream)
		{
			SentInvitationAddedNotification.Serialize(stream, this);
		}

		// Token: 0x060047AF RID: 18351 RVA: 0x000E0134 File Offset: 0x000DE334
		public static void Serialize(Stream stream, SentInvitationAddedNotification instance)
		{
			if (instance.HasAccountId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AccountId);
			}
			if (instance.HasInvitation)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Invitation.GetSerializedSize());
				SentInvitation.Serialize(stream, instance.Invitation);
			}
			if (instance.HasForward)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Forward.GetSerializedSize());
				ObjectAddress.Serialize(stream, instance.Forward);
			}
		}

		// Token: 0x060047B0 RID: 18352 RVA: 0x000E01C8 File Offset: 0x000DE3C8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAccountId)
			{
				num += 1U;
				uint serializedSize = this.AccountId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasInvitation)
			{
				num += 1U;
				uint serializedSize2 = this.Invitation.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasForward)
			{
				num += 1U;
				uint serializedSize3 = this.Forward.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		// Token: 0x040017D1 RID: 6097
		public bool HasAccountId;

		// Token: 0x040017D2 RID: 6098
		private EntityId _AccountId;

		// Token: 0x040017D3 RID: 6099
		public bool HasInvitation;

		// Token: 0x040017D4 RID: 6100
		private SentInvitation _Invitation;

		// Token: 0x040017D5 RID: 6101
		public bool HasForward;

		// Token: 0x040017D6 RID: 6102
		private ObjectAddress _Forward;
	}
}
