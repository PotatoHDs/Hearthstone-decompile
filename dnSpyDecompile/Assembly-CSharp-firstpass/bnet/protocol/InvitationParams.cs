using System;
using System.IO;
using System.Text;
using bnet.protocol.channel.v1;
using bnet.protocol.friends.v1;

namespace bnet.protocol
{
	// Token: 0x020002AA RID: 682
	public class InvitationParams : IProtoBuf
	{
		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06002750 RID: 10064 RVA: 0x0008B632 File Offset: 0x00089832
		// (set) Token: 0x06002751 RID: 10065 RVA: 0x0008B63A File Offset: 0x0008983A
		public string InvitationMessage
		{
			get
			{
				return this._InvitationMessage;
			}
			set
			{
				this._InvitationMessage = value;
				this.HasInvitationMessage = (value != null);
			}
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x0008B64D File Offset: 0x0008984D
		public void SetInvitationMessage(string val)
		{
			this.InvitationMessage = val;
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06002753 RID: 10067 RVA: 0x0008B656 File Offset: 0x00089856
		// (set) Token: 0x06002754 RID: 10068 RVA: 0x0008B65E File Offset: 0x0008985E
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

		// Token: 0x06002755 RID: 10069 RVA: 0x0008B66E File Offset: 0x0008986E
		public void SetExpirationTime(ulong val)
		{
			this.ExpirationTime = val;
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06002756 RID: 10070 RVA: 0x0008B677 File Offset: 0x00089877
		// (set) Token: 0x06002757 RID: 10071 RVA: 0x0008B67F File Offset: 0x0008987F
		public ChannelInvitationParams ChannelParams
		{
			get
			{
				return this._ChannelParams;
			}
			set
			{
				this._ChannelParams = value;
				this.HasChannelParams = (value != null);
			}
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x0008B692 File Offset: 0x00089892
		public void SetChannelParams(ChannelInvitationParams val)
		{
			this.ChannelParams = val;
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06002759 RID: 10073 RVA: 0x0008B69B File Offset: 0x0008989B
		// (set) Token: 0x0600275A RID: 10074 RVA: 0x0008B6A3 File Offset: 0x000898A3
		public FriendInvitationParams FriendParams
		{
			get
			{
				return this._FriendParams;
			}
			set
			{
				this._FriendParams = value;
				this.HasFriendParams = (value != null);
			}
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x0008B6B6 File Offset: 0x000898B6
		public void SetFriendParams(FriendInvitationParams val)
		{
			this.FriendParams = val;
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x0008B6C0 File Offset: 0x000898C0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasInvitationMessage)
			{
				num ^= this.InvitationMessage.GetHashCode();
			}
			if (this.HasExpirationTime)
			{
				num ^= this.ExpirationTime.GetHashCode();
			}
			if (this.HasChannelParams)
			{
				num ^= this.ChannelParams.GetHashCode();
			}
			if (this.HasFriendParams)
			{
				num ^= this.FriendParams.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x0008B738 File Offset: 0x00089938
		public override bool Equals(object obj)
		{
			InvitationParams invitationParams = obj as InvitationParams;
			return invitationParams != null && this.HasInvitationMessage == invitationParams.HasInvitationMessage && (!this.HasInvitationMessage || this.InvitationMessage.Equals(invitationParams.InvitationMessage)) && this.HasExpirationTime == invitationParams.HasExpirationTime && (!this.HasExpirationTime || this.ExpirationTime.Equals(invitationParams.ExpirationTime)) && this.HasChannelParams == invitationParams.HasChannelParams && (!this.HasChannelParams || this.ChannelParams.Equals(invitationParams.ChannelParams)) && this.HasFriendParams == invitationParams.HasFriendParams && (!this.HasFriendParams || this.FriendParams.Equals(invitationParams.FriendParams));
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x0600275E RID: 10078 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x0008B801 File Offset: 0x00089A01
		public static InvitationParams ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<InvitationParams>(bs, 0, -1);
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x0008B80B File Offset: 0x00089A0B
		public void Deserialize(Stream stream)
		{
			InvitationParams.Deserialize(stream, this);
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x0008B815 File Offset: 0x00089A15
		public static InvitationParams Deserialize(Stream stream, InvitationParams instance)
		{
			return InvitationParams.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x0008B820 File Offset: 0x00089A20
		public static InvitationParams DeserializeLengthDelimited(Stream stream)
		{
			InvitationParams invitationParams = new InvitationParams();
			InvitationParams.DeserializeLengthDelimited(stream, invitationParams);
			return invitationParams;
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x0008B83C File Offset: 0x00089A3C
		public static InvitationParams DeserializeLengthDelimited(Stream stream, InvitationParams instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return InvitationParams.Deserialize(stream, instance, num);
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x0008B864 File Offset: 0x00089A64
		public static InvitationParams Deserialize(Stream stream, InvitationParams instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						uint field = key.Field;
						if (field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						if (field != 103U)
						{
							if (field != 105U)
							{
								ProtocolParser.SkipKey(stream, key);
							}
							else if (key.WireType == Wire.LengthDelimited)
							{
								if (instance.ChannelParams == null)
								{
									instance.ChannelParams = ChannelInvitationParams.DeserializeLengthDelimited(stream);
								}
								else
								{
									ChannelInvitationParams.DeserializeLengthDelimited(stream, instance.ChannelParams);
								}
							}
						}
						else if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.FriendParams == null)
							{
								instance.FriendParams = FriendInvitationParams.DeserializeLengthDelimited(stream);
							}
							else
							{
								FriendInvitationParams.DeserializeLengthDelimited(stream, instance.FriendParams);
							}
						}
					}
					else
					{
						instance.ExpirationTime = ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.InvitationMessage = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x0008B97C File Offset: 0x00089B7C
		public void Serialize(Stream stream)
		{
			InvitationParams.Serialize(stream, this);
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x0008B988 File Offset: 0x00089B88
		public static void Serialize(Stream stream, InvitationParams instance)
		{
			if (instance.HasInvitationMessage)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.InvitationMessage));
			}
			if (instance.HasExpirationTime)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.ExpirationTime);
			}
			if (instance.HasChannelParams)
			{
				stream.WriteByte(202);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.ChannelParams.GetSerializedSize());
				ChannelInvitationParams.Serialize(stream, instance.ChannelParams);
			}
			if (instance.HasFriendParams)
			{
				stream.WriteByte(186);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.FriendParams.GetSerializedSize());
				FriendInvitationParams.Serialize(stream, instance.FriendParams);
			}
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x0008BA48 File Offset: 0x00089C48
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasInvitationMessage)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.InvitationMessage);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasExpirationTime)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.ExpirationTime);
			}
			if (this.HasChannelParams)
			{
				num += 2U;
				uint serializedSize = this.ChannelParams.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasFriendParams)
			{
				num += 2U;
				uint serializedSize2 = this.FriendParams.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x0400112D RID: 4397
		public bool HasInvitationMessage;

		// Token: 0x0400112E RID: 4398
		private string _InvitationMessage;

		// Token: 0x0400112F RID: 4399
		public bool HasExpirationTime;

		// Token: 0x04001130 RID: 4400
		private ulong _ExpirationTime;

		// Token: 0x04001131 RID: 4401
		public bool HasChannelParams;

		// Token: 0x04001132 RID: 4402
		private ChannelInvitationParams _ChannelParams;

		// Token: 0x04001133 RID: 4403
		public bool HasFriendParams;

		// Token: 0x04001134 RID: 4404
		private FriendInvitationParams _FriendParams;
	}
}
