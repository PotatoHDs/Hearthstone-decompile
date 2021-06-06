using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004B5 RID: 1205
	public class InvitationAddedNotification : IProtoBuf
	{
		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x0600541E RID: 21534 RVA: 0x00102F23 File Offset: 0x00101123
		// (set) Token: 0x0600541F RID: 21535 RVA: 0x00102F2B File Offset: 0x0010112B
		public Invitation Invitation { get; set; }

		// Token: 0x06005420 RID: 21536 RVA: 0x00102F34 File Offset: 0x00101134
		public void SetInvitation(Invitation val)
		{
			this.Invitation = val;
		}

		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x06005421 RID: 21537 RVA: 0x00102F3D File Offset: 0x0010113D
		// (set) Token: 0x06005422 RID: 21538 RVA: 0x00102F45 File Offset: 0x00101145
		public SubscriberId SubscriberId
		{
			get
			{
				return this._SubscriberId;
			}
			set
			{
				this._SubscriberId = value;
				this.HasSubscriberId = (value != null);
			}
		}

		// Token: 0x06005423 RID: 21539 RVA: 0x00102F58 File Offset: 0x00101158
		public void SetSubscriberId(SubscriberId val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x06005424 RID: 21540 RVA: 0x00102F64 File Offset: 0x00101164
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Invitation.GetHashCode();
			if (this.HasSubscriberId)
			{
				num ^= this.SubscriberId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005425 RID: 21541 RVA: 0x00102FA4 File Offset: 0x001011A4
		public override bool Equals(object obj)
		{
			InvitationAddedNotification invitationAddedNotification = obj as InvitationAddedNotification;
			return invitationAddedNotification != null && this.Invitation.Equals(invitationAddedNotification.Invitation) && this.HasSubscriberId == invitationAddedNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(invitationAddedNotification.SubscriberId));
		}

		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x06005426 RID: 21542 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005427 RID: 21543 RVA: 0x00102FFE File Offset: 0x001011FE
		public static InvitationAddedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<InvitationAddedNotification>(bs, 0, -1);
		}

		// Token: 0x06005428 RID: 21544 RVA: 0x00103008 File Offset: 0x00101208
		public void Deserialize(Stream stream)
		{
			InvitationAddedNotification.Deserialize(stream, this);
		}

		// Token: 0x06005429 RID: 21545 RVA: 0x00103012 File Offset: 0x00101212
		public static InvitationAddedNotification Deserialize(Stream stream, InvitationAddedNotification instance)
		{
			return InvitationAddedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600542A RID: 21546 RVA: 0x00103020 File Offset: 0x00101220
		public static InvitationAddedNotification DeserializeLengthDelimited(Stream stream)
		{
			InvitationAddedNotification invitationAddedNotification = new InvitationAddedNotification();
			InvitationAddedNotification.DeserializeLengthDelimited(stream, invitationAddedNotification);
			return invitationAddedNotification;
		}

		// Token: 0x0600542B RID: 21547 RVA: 0x0010303C File Offset: 0x0010123C
		public static InvitationAddedNotification DeserializeLengthDelimited(Stream stream, InvitationAddedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return InvitationAddedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x0600542C RID: 21548 RVA: 0x00103064 File Offset: 0x00101264
		public static InvitationAddedNotification Deserialize(Stream stream, InvitationAddedNotification instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.SubscriberId == null)
					{
						instance.SubscriberId = SubscriberId.DeserializeLengthDelimited(stream);
					}
					else
					{
						SubscriberId.DeserializeLengthDelimited(stream, instance.SubscriberId);
					}
				}
				else if (instance.Invitation == null)
				{
					instance.Invitation = Invitation.DeserializeLengthDelimited(stream);
				}
				else
				{
					Invitation.DeserializeLengthDelimited(stream, instance.Invitation);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600542D RID: 21549 RVA: 0x00103136 File Offset: 0x00101336
		public void Serialize(Stream stream)
		{
			InvitationAddedNotification.Serialize(stream, this);
		}

		// Token: 0x0600542E RID: 21550 RVA: 0x00103140 File Offset: 0x00101340
		public static void Serialize(Stream stream, InvitationAddedNotification instance)
		{
			if (instance.Invitation == null)
			{
				throw new ArgumentNullException("Invitation", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Invitation.GetSerializedSize());
			Invitation.Serialize(stream, instance.Invitation);
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				SubscriberId.Serialize(stream, instance.SubscriberId);
			}
		}

		// Token: 0x0600542F RID: 21551 RVA: 0x001031B8 File Offset: 0x001013B8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Invitation.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasSubscriberId)
			{
				num += 1U;
				uint serializedSize2 = this.SubscriberId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num + 1U;
		}

		// Token: 0x04001AB3 RID: 6835
		public bool HasSubscriberId;

		// Token: 0x04001AB4 RID: 6836
		private SubscriberId _SubscriberId;
	}
}
