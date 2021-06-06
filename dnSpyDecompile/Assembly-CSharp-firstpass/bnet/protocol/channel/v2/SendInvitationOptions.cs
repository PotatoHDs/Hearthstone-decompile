using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000481 RID: 1153
	public class SendInvitationOptions : IProtoBuf
	{
		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x06004FFF RID: 20479 RVA: 0x000F84B4 File Offset: 0x000F66B4
		// (set) Token: 0x06005000 RID: 20480 RVA: 0x000F84BC File Offset: 0x000F66BC
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

		// Token: 0x06005001 RID: 20481 RVA: 0x000F84CF File Offset: 0x000F66CF
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x06005002 RID: 20482 RVA: 0x000F84D8 File Offset: 0x000F66D8
		// (set) Token: 0x06005003 RID: 20483 RVA: 0x000F84E0 File Offset: 0x000F66E0
		public GameAccountHandle TargetId
		{
			get
			{
				return this._TargetId;
			}
			set
			{
				this._TargetId = value;
				this.HasTargetId = (value != null);
			}
		}

		// Token: 0x06005004 RID: 20484 RVA: 0x000F84F3 File Offset: 0x000F66F3
		public void SetTargetId(GameAccountHandle val)
		{
			this.TargetId = val;
		}

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x06005005 RID: 20485 RVA: 0x000F84FC File Offset: 0x000F66FC
		// (set) Token: 0x06005006 RID: 20486 RVA: 0x000F8504 File Offset: 0x000F6704
		public ChannelSlot Slot
		{
			get
			{
				return this._Slot;
			}
			set
			{
				this._Slot = value;
				this.HasSlot = (value != null);
			}
		}

		// Token: 0x06005007 RID: 20487 RVA: 0x000F8517 File Offset: 0x000F6717
		public void SetSlot(ChannelSlot val)
		{
			this.Slot = val;
		}

		// Token: 0x06005008 RID: 20488 RVA: 0x000F8520 File Offset: 0x000F6720
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			if (this.HasTargetId)
			{
				num ^= this.TargetId.GetHashCode();
			}
			if (this.HasSlot)
			{
				num ^= this.Slot.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005009 RID: 20489 RVA: 0x000F857C File Offset: 0x000F677C
		public override bool Equals(object obj)
		{
			SendInvitationOptions sendInvitationOptions = obj as SendInvitationOptions;
			return sendInvitationOptions != null && this.HasChannelId == sendInvitationOptions.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(sendInvitationOptions.ChannelId)) && this.HasTargetId == sendInvitationOptions.HasTargetId && (!this.HasTargetId || this.TargetId.Equals(sendInvitationOptions.TargetId)) && this.HasSlot == sendInvitationOptions.HasSlot && (!this.HasSlot || this.Slot.Equals(sendInvitationOptions.Slot));
		}

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x0600500A RID: 20490 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600500B RID: 20491 RVA: 0x000F8617 File Offset: 0x000F6817
		public static SendInvitationOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendInvitationOptions>(bs, 0, -1);
		}

		// Token: 0x0600500C RID: 20492 RVA: 0x000F8621 File Offset: 0x000F6821
		public void Deserialize(Stream stream)
		{
			SendInvitationOptions.Deserialize(stream, this);
		}

		// Token: 0x0600500D RID: 20493 RVA: 0x000F862B File Offset: 0x000F682B
		public static SendInvitationOptions Deserialize(Stream stream, SendInvitationOptions instance)
		{
			return SendInvitationOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600500E RID: 20494 RVA: 0x000F8638 File Offset: 0x000F6838
		public static SendInvitationOptions DeserializeLengthDelimited(Stream stream)
		{
			SendInvitationOptions sendInvitationOptions = new SendInvitationOptions();
			SendInvitationOptions.DeserializeLengthDelimited(stream, sendInvitationOptions);
			return sendInvitationOptions;
		}

		// Token: 0x0600500F RID: 20495 RVA: 0x000F8654 File Offset: 0x000F6854
		public static SendInvitationOptions DeserializeLengthDelimited(Stream stream, SendInvitationOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SendInvitationOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x06005010 RID: 20496 RVA: 0x000F867C File Offset: 0x000F687C
		public static SendInvitationOptions Deserialize(Stream stream, SendInvitationOptions instance, long limit)
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
						else if (instance.Slot == null)
						{
							instance.Slot = ChannelSlot.DeserializeLengthDelimited(stream);
						}
						else
						{
							ChannelSlot.DeserializeLengthDelimited(stream, instance.Slot);
						}
					}
					else if (instance.TargetId == null)
					{
						instance.TargetId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.TargetId);
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
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005011 RID: 20497 RVA: 0x000F877E File Offset: 0x000F697E
		public void Serialize(Stream stream)
		{
			SendInvitationOptions.Serialize(stream, this);
		}

		// Token: 0x06005012 RID: 20498 RVA: 0x000F8788 File Offset: 0x000F6988
		public static void Serialize(Stream stream, SendInvitationOptions instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasTargetId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.TargetId);
			}
			if (instance.HasSlot)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Slot.GetSerializedSize());
				ChannelSlot.Serialize(stream, instance.Slot);
			}
		}

		// Token: 0x06005013 RID: 20499 RVA: 0x000F881C File Offset: 0x000F6A1C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize = this.ChannelId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasTargetId)
			{
				num += 1U;
				uint serializedSize2 = this.TargetId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasSlot)
			{
				num += 1U;
				uint serializedSize3 = this.Slot.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		// Token: 0x040019D0 RID: 6608
		public bool HasChannelId;

		// Token: 0x040019D1 RID: 6609
		private ChannelId _ChannelId;

		// Token: 0x040019D2 RID: 6610
		public bool HasTargetId;

		// Token: 0x040019D3 RID: 6611
		private GameAccountHandle _TargetId;

		// Token: 0x040019D4 RID: 6612
		public bool HasSlot;

		// Token: 0x040019D5 RID: 6613
		private ChannelSlot _Slot;
	}
}
