using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004B6 RID: 1206
	public class InvitationRemovedNotification : IProtoBuf
	{
		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x06005431 RID: 21553 RVA: 0x00103206 File Offset: 0x00101406
		// (set) Token: 0x06005432 RID: 21554 RVA: 0x0010320E File Offset: 0x0010140E
		public Invitation Invitation { get; set; }

		// Token: 0x06005433 RID: 21555 RVA: 0x00103217 File Offset: 0x00101417
		public void SetInvitation(Invitation val)
		{
			this.Invitation = val;
		}

		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x06005434 RID: 21556 RVA: 0x00103220 File Offset: 0x00101420
		// (set) Token: 0x06005435 RID: 21557 RVA: 0x00103228 File Offset: 0x00101428
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

		// Token: 0x06005436 RID: 21558 RVA: 0x00103238 File Offset: 0x00101438
		public void SetReason(uint val)
		{
			this.Reason = val;
		}

		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x06005437 RID: 21559 RVA: 0x00103241 File Offset: 0x00101441
		// (set) Token: 0x06005438 RID: 21560 RVA: 0x00103249 File Offset: 0x00101449
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

		// Token: 0x06005439 RID: 21561 RVA: 0x0010325C File Offset: 0x0010145C
		public void SetSubscriberId(SubscriberId val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x0600543A RID: 21562 RVA: 0x00103268 File Offset: 0x00101468
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Invitation.GetHashCode();
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			if (this.HasSubscriberId)
			{
				num ^= this.SubscriberId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600543B RID: 21563 RVA: 0x001032C0 File Offset: 0x001014C0
		public override bool Equals(object obj)
		{
			InvitationRemovedNotification invitationRemovedNotification = obj as InvitationRemovedNotification;
			return invitationRemovedNotification != null && this.Invitation.Equals(invitationRemovedNotification.Invitation) && this.HasReason == invitationRemovedNotification.HasReason && (!this.HasReason || this.Reason.Equals(invitationRemovedNotification.Reason)) && this.HasSubscriberId == invitationRemovedNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(invitationRemovedNotification.SubscriberId));
		}

		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x0600543C RID: 21564 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600543D RID: 21565 RVA: 0x00103348 File Offset: 0x00101548
		public static InvitationRemovedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<InvitationRemovedNotification>(bs, 0, -1);
		}

		// Token: 0x0600543E RID: 21566 RVA: 0x00103352 File Offset: 0x00101552
		public void Deserialize(Stream stream)
		{
			InvitationRemovedNotification.Deserialize(stream, this);
		}

		// Token: 0x0600543F RID: 21567 RVA: 0x0010335C File Offset: 0x0010155C
		public static InvitationRemovedNotification Deserialize(Stream stream, InvitationRemovedNotification instance)
		{
			return InvitationRemovedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005440 RID: 21568 RVA: 0x00103368 File Offset: 0x00101568
		public static InvitationRemovedNotification DeserializeLengthDelimited(Stream stream)
		{
			InvitationRemovedNotification invitationRemovedNotification = new InvitationRemovedNotification();
			InvitationRemovedNotification.DeserializeLengthDelimited(stream, invitationRemovedNotification);
			return invitationRemovedNotification;
		}

		// Token: 0x06005441 RID: 21569 RVA: 0x00103384 File Offset: 0x00101584
		public static InvitationRemovedNotification DeserializeLengthDelimited(Stream stream, InvitationRemovedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return InvitationRemovedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06005442 RID: 21570 RVA: 0x001033AC File Offset: 0x001015AC
		public static InvitationRemovedNotification Deserialize(Stream stream, InvitationRemovedNotification instance, long limit)
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
					if (num != 16)
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
						else if (instance.SubscriberId == null)
						{
							instance.SubscriberId = SubscriberId.DeserializeLengthDelimited(stream);
						}
						else
						{
							SubscriberId.DeserializeLengthDelimited(stream, instance.SubscriberId);
						}
					}
					else
					{
						instance.Reason = ProtocolParser.ReadUInt32(stream);
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

		// Token: 0x06005443 RID: 21571 RVA: 0x00103494 File Offset: 0x00101694
		public void Serialize(Stream stream)
		{
			InvitationRemovedNotification.Serialize(stream, this);
		}

		// Token: 0x06005444 RID: 21572 RVA: 0x001034A0 File Offset: 0x001016A0
		public static void Serialize(Stream stream, InvitationRemovedNotification instance)
		{
			if (instance.Invitation == null)
			{
				throw new ArgumentNullException("Invitation", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Invitation.GetSerializedSize());
			Invitation.Serialize(stream, instance.Invitation);
			if (instance.HasReason)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				SubscriberId.Serialize(stream, instance.SubscriberId);
			}
		}

		// Token: 0x06005445 RID: 21573 RVA: 0x00103534 File Offset: 0x00101734
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
			if (this.HasSubscriberId)
			{
				num += 1U;
				uint serializedSize2 = this.SubscriberId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num + 1U;
		}

		// Token: 0x04001AB6 RID: 6838
		public bool HasReason;

		// Token: 0x04001AB7 RID: 6839
		private uint _Reason;

		// Token: 0x04001AB8 RID: 6840
		public bool HasSubscriberId;

		// Token: 0x04001AB9 RID: 6841
		private SubscriberId _SubscriberId;
	}
}
