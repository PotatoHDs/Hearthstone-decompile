using System;
using System.IO;
using System.Text;
using bnet.protocol.channel.v1;

namespace bnet.protocol
{
	// Token: 0x020002A9 RID: 681
	public class Invitation : IProtoBuf
	{
		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06002728 RID: 10024 RVA: 0x0008ADF8 File Offset: 0x00088FF8
		// (set) Token: 0x06002729 RID: 10025 RVA: 0x0008AE00 File Offset: 0x00089000
		public ulong Id { get; set; }

		// Token: 0x0600272A RID: 10026 RVA: 0x0008AE09 File Offset: 0x00089009
		public void SetId(ulong val)
		{
			this.Id = val;
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x0600272B RID: 10027 RVA: 0x0008AE12 File Offset: 0x00089012
		// (set) Token: 0x0600272C RID: 10028 RVA: 0x0008AE1A File Offset: 0x0008901A
		public Identity InviterIdentity { get; set; }

		// Token: 0x0600272D RID: 10029 RVA: 0x0008AE23 File Offset: 0x00089023
		public void SetInviterIdentity(Identity val)
		{
			this.InviterIdentity = val;
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x0600272E RID: 10030 RVA: 0x0008AE2C File Offset: 0x0008902C
		// (set) Token: 0x0600272F RID: 10031 RVA: 0x0008AE34 File Offset: 0x00089034
		public Identity InviteeIdentity { get; set; }

		// Token: 0x06002730 RID: 10032 RVA: 0x0008AE3D File Offset: 0x0008903D
		public void SetInviteeIdentity(Identity val)
		{
			this.InviteeIdentity = val;
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06002731 RID: 10033 RVA: 0x0008AE46 File Offset: 0x00089046
		// (set) Token: 0x06002732 RID: 10034 RVA: 0x0008AE4E File Offset: 0x0008904E
		public string InviterName
		{
			get
			{
				return this._InviterName;
			}
			set
			{
				this._InviterName = value;
				this.HasInviterName = (value != null);
			}
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x0008AE61 File Offset: 0x00089061
		public void SetInviterName(string val)
		{
			this.InviterName = val;
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06002734 RID: 10036 RVA: 0x0008AE6A File Offset: 0x0008906A
		// (set) Token: 0x06002735 RID: 10037 RVA: 0x0008AE72 File Offset: 0x00089072
		public string InviteeName
		{
			get
			{
				return this._InviteeName;
			}
			set
			{
				this._InviteeName = value;
				this.HasInviteeName = (value != null);
			}
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x0008AE85 File Offset: 0x00089085
		public void SetInviteeName(string val)
		{
			this.InviteeName = val;
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06002737 RID: 10039 RVA: 0x0008AE8E File Offset: 0x0008908E
		// (set) Token: 0x06002738 RID: 10040 RVA: 0x0008AE96 File Offset: 0x00089096
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

		// Token: 0x06002739 RID: 10041 RVA: 0x0008AEA9 File Offset: 0x000890A9
		public void SetInvitationMessage(string val)
		{
			this.InvitationMessage = val;
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x0600273A RID: 10042 RVA: 0x0008AEB2 File Offset: 0x000890B2
		// (set) Token: 0x0600273B RID: 10043 RVA: 0x0008AEBA File Offset: 0x000890BA
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

		// Token: 0x0600273C RID: 10044 RVA: 0x0008AECA File Offset: 0x000890CA
		public void SetCreationTime(ulong val)
		{
			this.CreationTime = val;
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x0600273D RID: 10045 RVA: 0x0008AED3 File Offset: 0x000890D3
		// (set) Token: 0x0600273E RID: 10046 RVA: 0x0008AEDB File Offset: 0x000890DB
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

		// Token: 0x0600273F RID: 10047 RVA: 0x0008AEEB File Offset: 0x000890EB
		public void SetExpirationTime(ulong val)
		{
			this.ExpirationTime = val;
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06002740 RID: 10048 RVA: 0x0008AEF4 File Offset: 0x000890F4
		// (set) Token: 0x06002741 RID: 10049 RVA: 0x0008AEFC File Offset: 0x000890FC
		public ChannelInvitation ChannelInvitation
		{
			get
			{
				return this._ChannelInvitation;
			}
			set
			{
				this._ChannelInvitation = value;
				this.HasChannelInvitation = (value != null);
			}
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x0008AF0F File Offset: 0x0008910F
		public void SetChannelInvitation(ChannelInvitation val)
		{
			this.ChannelInvitation = val;
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x0008AF18 File Offset: 0x00089118
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			num ^= this.InviterIdentity.GetHashCode();
			num ^= this.InviteeIdentity.GetHashCode();
			if (this.HasInviterName)
			{
				num ^= this.InviterName.GetHashCode();
			}
			if (this.HasInviteeName)
			{
				num ^= this.InviteeName.GetHashCode();
			}
			if (this.HasInvitationMessage)
			{
				num ^= this.InvitationMessage.GetHashCode();
			}
			if (this.HasCreationTime)
			{
				num ^= this.CreationTime.GetHashCode();
			}
			if (this.HasExpirationTime)
			{
				num ^= this.ExpirationTime.GetHashCode();
			}
			if (this.HasChannelInvitation)
			{
				num ^= this.ChannelInvitation.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x0008AFEC File Offset: 0x000891EC
		public override bool Equals(object obj)
		{
			Invitation invitation = obj as Invitation;
			return invitation != null && this.Id.Equals(invitation.Id) && this.InviterIdentity.Equals(invitation.InviterIdentity) && this.InviteeIdentity.Equals(invitation.InviteeIdentity) && this.HasInviterName == invitation.HasInviterName && (!this.HasInviterName || this.InviterName.Equals(invitation.InviterName)) && this.HasInviteeName == invitation.HasInviteeName && (!this.HasInviteeName || this.InviteeName.Equals(invitation.InviteeName)) && this.HasInvitationMessage == invitation.HasInvitationMessage && (!this.HasInvitationMessage || this.InvitationMessage.Equals(invitation.InvitationMessage)) && this.HasCreationTime == invitation.HasCreationTime && (!this.HasCreationTime || this.CreationTime.Equals(invitation.CreationTime)) && this.HasExpirationTime == invitation.HasExpirationTime && (!this.HasExpirationTime || this.ExpirationTime.Equals(invitation.ExpirationTime)) && this.HasChannelInvitation == invitation.HasChannelInvitation && (!this.HasChannelInvitation || this.ChannelInvitation.Equals(invitation.ChannelInvitation));
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06002745 RID: 10053 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x0008B150 File Offset: 0x00089350
		public static Invitation ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Invitation>(bs, 0, -1);
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x0008B15A File Offset: 0x0008935A
		public void Deserialize(Stream stream)
		{
			Invitation.Deserialize(stream, this);
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x0008B164 File Offset: 0x00089364
		public static Invitation Deserialize(Stream stream, Invitation instance)
		{
			return Invitation.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x0008B170 File Offset: 0x00089370
		public static Invitation DeserializeLengthDelimited(Stream stream)
		{
			Invitation invitation = new Invitation();
			Invitation.DeserializeLengthDelimited(stream, invitation);
			return invitation;
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x0008B18C File Offset: 0x0008938C
		public static Invitation DeserializeLengthDelimited(Stream stream, Invitation instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Invitation.Deserialize(stream, instance, num);
		}

		// Token: 0x0600274B RID: 10059 RVA: 0x0008B1B4 File Offset: 0x000893B4
		public static Invitation Deserialize(Stream stream, Invitation instance, long limit)
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
					if (num <= 34)
					{
						if (num <= 18)
						{
							if (num == 9)
							{
								instance.Id = binaryReader.ReadUInt64();
								continue;
							}
							if (num == 18)
							{
								if (instance.InviterIdentity == null)
								{
									instance.InviterIdentity = Identity.DeserializeLengthDelimited(stream);
									continue;
								}
								Identity.DeserializeLengthDelimited(stream, instance.InviterIdentity);
								continue;
							}
						}
						else if (num != 26)
						{
							if (num == 34)
							{
								instance.InviterName = ProtocolParser.ReadString(stream);
								continue;
							}
						}
						else
						{
							if (instance.InviteeIdentity == null)
							{
								instance.InviteeIdentity = Identity.DeserializeLengthDelimited(stream);
								continue;
							}
							Identity.DeserializeLengthDelimited(stream, instance.InviteeIdentity);
							continue;
						}
					}
					else if (num <= 50)
					{
						if (num == 42)
						{
							instance.InviteeName = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 50)
						{
							instance.InvitationMessage = ProtocolParser.ReadString(stream);
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
					uint field = key.Field;
					if (field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					if (field != 105U)
					{
						ProtocolParser.SkipKey(stream, key);
					}
					else if (key.WireType == Wire.LengthDelimited)
					{
						if (instance.ChannelInvitation == null)
						{
							instance.ChannelInvitation = ChannelInvitation.DeserializeLengthDelimited(stream);
						}
						else
						{
							ChannelInvitation.DeserializeLengthDelimited(stream, instance.ChannelInvitation);
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

		// Token: 0x0600274C RID: 10060 RVA: 0x0008B385 File Offset: 0x00089585
		public void Serialize(Stream stream)
		{
			Invitation.Serialize(stream, this);
		}

		// Token: 0x0600274D RID: 10061 RVA: 0x0008B390 File Offset: 0x00089590
		public static void Serialize(Stream stream, Invitation instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(9);
			binaryWriter.Write(instance.Id);
			if (instance.InviterIdentity == null)
			{
				throw new ArgumentNullException("InviterIdentity", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.InviterIdentity.GetSerializedSize());
			Identity.Serialize(stream, instance.InviterIdentity);
			if (instance.InviteeIdentity == null)
			{
				throw new ArgumentNullException("InviteeIdentity", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteUInt32(stream, instance.InviteeIdentity.GetSerializedSize());
			Identity.Serialize(stream, instance.InviteeIdentity);
			if (instance.HasInviterName)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.InviterName));
			}
			if (instance.HasInviteeName)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.InviteeName));
			}
			if (instance.HasInvitationMessage)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.InvitationMessage));
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
			if (instance.HasChannelInvitation)
			{
				stream.WriteByte(202);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.ChannelInvitation.GetSerializedSize());
				ChannelInvitation.Serialize(stream, instance.ChannelInvitation);
			}
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x0008B514 File Offset: 0x00089714
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += 8U;
			uint serializedSize = this.InviterIdentity.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			uint serializedSize2 = this.InviteeIdentity.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			if (this.HasInviterName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.InviterName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasInviteeName)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.InviteeName);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasInvitationMessage)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.InvitationMessage);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
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
			if (this.HasChannelInvitation)
			{
				num += 2U;
				uint serializedSize3 = this.ChannelInvitation.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num + 3U;
		}

		// Token: 0x04001121 RID: 4385
		public bool HasInviterName;

		// Token: 0x04001122 RID: 4386
		private string _InviterName;

		// Token: 0x04001123 RID: 4387
		public bool HasInviteeName;

		// Token: 0x04001124 RID: 4388
		private string _InviteeName;

		// Token: 0x04001125 RID: 4389
		public bool HasInvitationMessage;

		// Token: 0x04001126 RID: 4390
		private string _InvitationMessage;

		// Token: 0x04001127 RID: 4391
		public bool HasCreationTime;

		// Token: 0x04001128 RID: 4392
		private ulong _CreationTime;

		// Token: 0x04001129 RID: 4393
		public bool HasExpirationTime;

		// Token: 0x0400112A RID: 4394
		private ulong _ExpirationTime;

		// Token: 0x0400112B RID: 4395
		public bool HasChannelInvitation;

		// Token: 0x0400112C RID: 4396
		private ChannelInvitation _ChannelInvitation;
	}
}
