using System;
using System.IO;

namespace bnet.protocol.friends.v1
{
	// Token: 0x0200042B RID: 1067
	public class InvitationNotification : IProtoBuf
	{
		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06004783 RID: 18307 RVA: 0x000DF9D1 File Offset: 0x000DDBD1
		// (set) Token: 0x06004784 RID: 18308 RVA: 0x000DF9D9 File Offset: 0x000DDBD9
		public ReceivedInvitation Invitation { get; set; }

		// Token: 0x06004785 RID: 18309 RVA: 0x000DF9E2 File Offset: 0x000DDBE2
		public void SetInvitation(ReceivedInvitation val)
		{
			this.Invitation = val;
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x06004786 RID: 18310 RVA: 0x000DF9EB File Offset: 0x000DDBEB
		// (set) Token: 0x06004787 RID: 18311 RVA: 0x000DF9F3 File Offset: 0x000DDBF3
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

		// Token: 0x06004788 RID: 18312 RVA: 0x000DFA03 File Offset: 0x000DDC03
		public void SetReason(uint val)
		{
			this.Reason = val;
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06004789 RID: 18313 RVA: 0x000DFA0C File Offset: 0x000DDC0C
		// (set) Token: 0x0600478A RID: 18314 RVA: 0x000DFA14 File Offset: 0x000DDC14
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

		// Token: 0x0600478B RID: 18315 RVA: 0x000DFA27 File Offset: 0x000DDC27
		public void SetAccountId(EntityId val)
		{
			this.AccountId = val;
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x0600478C RID: 18316 RVA: 0x000DFA30 File Offset: 0x000DDC30
		// (set) Token: 0x0600478D RID: 18317 RVA: 0x000DFA38 File Offset: 0x000DDC38
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

		// Token: 0x0600478E RID: 18318 RVA: 0x000DFA4B File Offset: 0x000DDC4B
		public void SetForward(ObjectAddress val)
		{
			this.Forward = val;
		}

		// Token: 0x0600478F RID: 18319 RVA: 0x000DFA54 File Offset: 0x000DDC54
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Invitation.GetHashCode();
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			if (this.HasAccountId)
			{
				num ^= this.AccountId.GetHashCode();
			}
			if (this.HasForward)
			{
				num ^= this.Forward.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004790 RID: 18320 RVA: 0x000DFAC4 File Offset: 0x000DDCC4
		public override bool Equals(object obj)
		{
			InvitationNotification invitationNotification = obj as InvitationNotification;
			return invitationNotification != null && this.Invitation.Equals(invitationNotification.Invitation) && this.HasReason == invitationNotification.HasReason && (!this.HasReason || this.Reason.Equals(invitationNotification.Reason)) && this.HasAccountId == invitationNotification.HasAccountId && (!this.HasAccountId || this.AccountId.Equals(invitationNotification.AccountId)) && this.HasForward == invitationNotification.HasForward && (!this.HasForward || this.Forward.Equals(invitationNotification.Forward));
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x06004791 RID: 18321 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004792 RID: 18322 RVA: 0x000DFB77 File Offset: 0x000DDD77
		public static InvitationNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<InvitationNotification>(bs, 0, -1);
		}

		// Token: 0x06004793 RID: 18323 RVA: 0x000DFB81 File Offset: 0x000DDD81
		public void Deserialize(Stream stream)
		{
			InvitationNotification.Deserialize(stream, this);
		}

		// Token: 0x06004794 RID: 18324 RVA: 0x000DFB8B File Offset: 0x000DDD8B
		public static InvitationNotification Deserialize(Stream stream, InvitationNotification instance)
		{
			return InvitationNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004795 RID: 18325 RVA: 0x000DFB98 File Offset: 0x000DDD98
		public static InvitationNotification DeserializeLengthDelimited(Stream stream)
		{
			InvitationNotification invitationNotification = new InvitationNotification();
			InvitationNotification.DeserializeLengthDelimited(stream, invitationNotification);
			return invitationNotification;
		}

		// Token: 0x06004796 RID: 18326 RVA: 0x000DFBB4 File Offset: 0x000DDDB4
		public static InvitationNotification DeserializeLengthDelimited(Stream stream, InvitationNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return InvitationNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06004797 RID: 18327 RVA: 0x000DFBDC File Offset: 0x000DDDDC
		public static InvitationNotification Deserialize(Stream stream, InvitationNotification instance, long limit)
		{
			instance.Reason = 0U;
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
					if (num <= 24)
					{
						if (num != 10)
						{
							if (num == 24)
							{
								instance.Reason = ProtocolParser.ReadUInt32(stream);
								continue;
							}
						}
						else
						{
							if (instance.Invitation == null)
							{
								instance.Invitation = ReceivedInvitation.DeserializeLengthDelimited(stream);
								continue;
							}
							ReceivedInvitation.DeserializeLengthDelimited(stream, instance.Invitation);
							continue;
						}
					}
					else if (num != 42)
					{
						if (num == 50)
						{
							if (instance.Forward == null)
							{
								instance.Forward = ObjectAddress.DeserializeLengthDelimited(stream);
								continue;
							}
							ObjectAddress.DeserializeLengthDelimited(stream, instance.Forward);
							continue;
						}
					}
					else
					{
						if (instance.AccountId == null)
						{
							instance.AccountId = EntityId.DeserializeLengthDelimited(stream);
							continue;
						}
						EntityId.DeserializeLengthDelimited(stream, instance.AccountId);
						continue;
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

		// Token: 0x06004798 RID: 18328 RVA: 0x000DFD0B File Offset: 0x000DDF0B
		public void Serialize(Stream stream)
		{
			InvitationNotification.Serialize(stream, this);
		}

		// Token: 0x06004799 RID: 18329 RVA: 0x000DFD14 File Offset: 0x000DDF14
		public static void Serialize(Stream stream, InvitationNotification instance)
		{
			if (instance.Invitation == null)
			{
				throw new ArgumentNullException("Invitation", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Invitation.GetSerializedSize());
			ReceivedInvitation.Serialize(stream, instance.Invitation);
			if (instance.HasReason)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
			}
			if (instance.HasAccountId)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AccountId);
			}
			if (instance.HasForward)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.Forward.GetSerializedSize());
				ObjectAddress.Serialize(stream, instance.Forward);
			}
		}

		// Token: 0x0600479A RID: 18330 RVA: 0x000DFDD4 File Offset: 0x000DDFD4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Invitation.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasReason)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Reason);
			}
			if (this.HasAccountId)
			{
				num += 1U;
				uint serializedSize2 = this.AccountId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasForward)
			{
				num += 1U;
				uint serializedSize3 = this.Forward.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num + 1U;
		}

		// Token: 0x040017CB RID: 6091
		public bool HasReason;

		// Token: 0x040017CC RID: 6092
		private uint _Reason;

		// Token: 0x040017CD RID: 6093
		public bool HasAccountId;

		// Token: 0x040017CE RID: 6094
		private EntityId _AccountId;

		// Token: 0x040017CF RID: 6095
		public bool HasForward;

		// Token: 0x040017D0 RID: 6096
		private ObjectAddress _Forward;
	}
}
