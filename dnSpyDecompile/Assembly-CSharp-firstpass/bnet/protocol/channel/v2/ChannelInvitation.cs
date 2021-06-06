using System;
using System.IO;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000483 RID: 1155
	public class ChannelInvitation : IProtoBuf
	{
		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x06005028 RID: 20520 RVA: 0x000F8B73 File Offset: 0x000F6D73
		// (set) Token: 0x06005029 RID: 20521 RVA: 0x000F8B7B File Offset: 0x000F6D7B
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

		// Token: 0x0600502A RID: 20522 RVA: 0x000F8B8B File Offset: 0x000F6D8B
		public void SetId(ulong val)
		{
			this.Id = val;
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x0600502B RID: 20523 RVA: 0x000F8B94 File Offset: 0x000F6D94
		// (set) Token: 0x0600502C RID: 20524 RVA: 0x000F8B9C File Offset: 0x000F6D9C
		public MemberDescription Inviter
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

		// Token: 0x0600502D RID: 20525 RVA: 0x000F8BAF File Offset: 0x000F6DAF
		public void SetInviter(MemberDescription val)
		{
			this.Inviter = val;
		}

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x0600502E RID: 20526 RVA: 0x000F8BB8 File Offset: 0x000F6DB8
		// (set) Token: 0x0600502F RID: 20527 RVA: 0x000F8BC0 File Offset: 0x000F6DC0
		public MemberDescription Invitee
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

		// Token: 0x06005030 RID: 20528 RVA: 0x000F8BD3 File Offset: 0x000F6DD3
		public void SetInvitee(MemberDescription val)
		{
			this.Invitee = val;
		}

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x06005031 RID: 20529 RVA: 0x000F8BDC File Offset: 0x000F6DDC
		// (set) Token: 0x06005032 RID: 20530 RVA: 0x000F8BE4 File Offset: 0x000F6DE4
		public ChannelDescription Channel
		{
			get
			{
				return this._Channel;
			}
			set
			{
				this._Channel = value;
				this.HasChannel = (value != null);
			}
		}

		// Token: 0x06005033 RID: 20531 RVA: 0x000F8BF7 File Offset: 0x000F6DF7
		public void SetChannel(ChannelDescription val)
		{
			this.Channel = val;
		}

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06005034 RID: 20532 RVA: 0x000F8C00 File Offset: 0x000F6E00
		// (set) Token: 0x06005035 RID: 20533 RVA: 0x000F8C08 File Offset: 0x000F6E08
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

		// Token: 0x06005036 RID: 20534 RVA: 0x000F8C1B File Offset: 0x000F6E1B
		public void SetSlot(ChannelSlot val)
		{
			this.Slot = val;
		}

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x06005037 RID: 20535 RVA: 0x000F8C24 File Offset: 0x000F6E24
		// (set) Token: 0x06005038 RID: 20536 RVA: 0x000F8C2C File Offset: 0x000F6E2C
		public ulong CreationTime
		{
			get
			{
				return this._CreationTime;
			}
			set
			{
				this._CreationTime = value;
				this.HasCreationTime = true;
			}
		}

		// Token: 0x06005039 RID: 20537 RVA: 0x000F8C3C File Offset: 0x000F6E3C
		public void SetCreationTime(ulong val)
		{
			this.CreationTime = val;
		}

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x0600503A RID: 20538 RVA: 0x000F8C45 File Offset: 0x000F6E45
		// (set) Token: 0x0600503B RID: 20539 RVA: 0x000F8C4D File Offset: 0x000F6E4D
		public ulong ExpirationTime
		{
			get
			{
				return this._ExpirationTime;
			}
			set
			{
				this._ExpirationTime = value;
				this.HasExpirationTime = true;
			}
		}

		// Token: 0x0600503C RID: 20540 RVA: 0x000F8C5D File Offset: 0x000F6E5D
		public void SetExpirationTime(ulong val)
		{
			this.ExpirationTime = val;
		}

		// Token: 0x0600503D RID: 20541 RVA: 0x000F8C68 File Offset: 0x000F6E68
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
			if (this.HasChannel)
			{
				num ^= this.Channel.GetHashCode();
			}
			if (this.HasSlot)
			{
				num ^= this.Slot.GetHashCode();
			}
			if (this.HasCreationTime)
			{
				num ^= this.CreationTime.GetHashCode();
			}
			if (this.HasExpirationTime)
			{
				num ^= this.ExpirationTime.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600503E RID: 20542 RVA: 0x000F8D28 File Offset: 0x000F6F28
		public override bool Equals(object obj)
		{
			ChannelInvitation channelInvitation = obj as ChannelInvitation;
			return channelInvitation != null && this.HasId == channelInvitation.HasId && (!this.HasId || this.Id.Equals(channelInvitation.Id)) && this.HasInviter == channelInvitation.HasInviter && (!this.HasInviter || this.Inviter.Equals(channelInvitation.Inviter)) && this.HasInvitee == channelInvitation.HasInvitee && (!this.HasInvitee || this.Invitee.Equals(channelInvitation.Invitee)) && this.HasChannel == channelInvitation.HasChannel && (!this.HasChannel || this.Channel.Equals(channelInvitation.Channel)) && this.HasSlot == channelInvitation.HasSlot && (!this.HasSlot || this.Slot.Equals(channelInvitation.Slot)) && this.HasCreationTime == channelInvitation.HasCreationTime && (!this.HasCreationTime || this.CreationTime.Equals(channelInvitation.CreationTime)) && this.HasExpirationTime == channelInvitation.HasExpirationTime && (!this.HasExpirationTime || this.ExpirationTime.Equals(channelInvitation.ExpirationTime));
		}

		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x0600503F RID: 20543 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005040 RID: 20544 RVA: 0x000F8E78 File Offset: 0x000F7078
		public static ChannelInvitation ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelInvitation>(bs, 0, -1);
		}

		// Token: 0x06005041 RID: 20545 RVA: 0x000F8E82 File Offset: 0x000F7082
		public void Deserialize(Stream stream)
		{
			ChannelInvitation.Deserialize(stream, this);
		}

		// Token: 0x06005042 RID: 20546 RVA: 0x000F8E8C File Offset: 0x000F708C
		public static ChannelInvitation Deserialize(Stream stream, ChannelInvitation instance)
		{
			return ChannelInvitation.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005043 RID: 20547 RVA: 0x000F8E98 File Offset: 0x000F7098
		public static ChannelInvitation DeserializeLengthDelimited(Stream stream)
		{
			ChannelInvitation channelInvitation = new ChannelInvitation();
			ChannelInvitation.DeserializeLengthDelimited(stream, channelInvitation);
			return channelInvitation;
		}

		// Token: 0x06005044 RID: 20548 RVA: 0x000F8EB4 File Offset: 0x000F70B4
		public static ChannelInvitation DeserializeLengthDelimited(Stream stream, ChannelInvitation instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChannelInvitation.Deserialize(stream, instance, num);
		}

		// Token: 0x06005045 RID: 20549 RVA: 0x000F8EDC File Offset: 0x000F70DC
		public static ChannelInvitation Deserialize(Stream stream, ChannelInvitation instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					if (num <= 26)
					{
						if (num == 9)
						{
							instance.Id = binaryReader.ReadUInt64();
							continue;
						}
						if (num != 18)
						{
							if (num == 26)
							{
								if (instance.Invitee == null)
								{
									instance.Invitee = MemberDescription.DeserializeLengthDelimited(stream);
									continue;
								}
								MemberDescription.DeserializeLengthDelimited(stream, instance.Invitee);
								continue;
							}
						}
						else
						{
							if (instance.Inviter == null)
							{
								instance.Inviter = MemberDescription.DeserializeLengthDelimited(stream);
								continue;
							}
							MemberDescription.DeserializeLengthDelimited(stream, instance.Inviter);
							continue;
						}
					}
					else if (num <= 42)
					{
						if (num != 34)
						{
							if (num == 42)
							{
								if (instance.Slot == null)
								{
									instance.Slot = ChannelSlot.DeserializeLengthDelimited(stream);
									continue;
								}
								ChannelSlot.DeserializeLengthDelimited(stream, instance.Slot);
								continue;
							}
						}
						else
						{
							if (instance.Channel == null)
							{
								instance.Channel = ChannelDescription.DeserializeLengthDelimited(stream);
								continue;
							}
							ChannelDescription.DeserializeLengthDelimited(stream, instance.Channel);
							continue;
						}
					}
					else
					{
						if (num == 56)
						{
							instance.CreationTime = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 64)
						{
							instance.ExpirationTime = ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06005046 RID: 20550 RVA: 0x000F9080 File Offset: 0x000F7280
		public void Serialize(Stream stream)
		{
			ChannelInvitation.Serialize(stream, this);
		}

		// Token: 0x06005047 RID: 20551 RVA: 0x000F908C File Offset: 0x000F728C
		public static void Serialize(Stream stream, ChannelInvitation instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasId)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.Id);
			}
			if (instance.HasInviter)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Inviter.GetSerializedSize());
				MemberDescription.Serialize(stream, instance.Inviter);
			}
			if (instance.HasInvitee)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Invitee.GetSerializedSize());
				MemberDescription.Serialize(stream, instance.Invitee);
			}
			if (instance.HasChannel)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Channel.GetSerializedSize());
				ChannelDescription.Serialize(stream, instance.Channel);
			}
			if (instance.HasSlot)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.Slot.GetSerializedSize());
				ChannelSlot.Serialize(stream, instance.Slot);
			}
			if (instance.HasCreationTime)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, instance.CreationTime);
			}
			if (instance.HasExpirationTime)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, instance.ExpirationTime);
			}
		}

		// Token: 0x06005048 RID: 20552 RVA: 0x000F91A8 File Offset: 0x000F73A8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasId)
			{
				num += 1U;
				num += 8U;
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
			if (this.HasChannel)
			{
				num += 1U;
				uint serializedSize3 = this.Channel.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasSlot)
			{
				num += 1U;
				uint serializedSize4 = this.Slot.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (this.HasCreationTime)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.CreationTime);
			}
			if (this.HasExpirationTime)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.ExpirationTime);
			}
			return num;
		}

		// Token: 0x040019DA RID: 6618
		public bool HasId;

		// Token: 0x040019DB RID: 6619
		private ulong _Id;

		// Token: 0x040019DC RID: 6620
		public bool HasInviter;

		// Token: 0x040019DD RID: 6621
		private MemberDescription _Inviter;

		// Token: 0x040019DE RID: 6622
		public bool HasInvitee;

		// Token: 0x040019DF RID: 6623
		private MemberDescription _Invitee;

		// Token: 0x040019E0 RID: 6624
		public bool HasChannel;

		// Token: 0x040019E1 RID: 6625
		private ChannelDescription _Channel;

		// Token: 0x040019E2 RID: 6626
		public bool HasSlot;

		// Token: 0x040019E3 RID: 6627
		private ChannelSlot _Slot;

		// Token: 0x040019E4 RID: 6628
		public bool HasCreationTime;

		// Token: 0x040019E5 RID: 6629
		private ulong _CreationTime;

		// Token: 0x040019E6 RID: 6630
		public bool HasExpirationTime;

		// Token: 0x040019E7 RID: 6631
		private ulong _ExpirationTime;
	}
}
