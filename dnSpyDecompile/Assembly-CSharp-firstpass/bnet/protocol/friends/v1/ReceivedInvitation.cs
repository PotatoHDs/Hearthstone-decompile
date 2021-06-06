using System;
using System.IO;
using System.Text;

namespace bnet.protocol.friends.v1
{
	// Token: 0x02000430 RID: 1072
	public class ReceivedInvitation : IProtoBuf
	{
		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06004812 RID: 18450 RVA: 0x000E1617 File Offset: 0x000DF817
		// (set) Token: 0x06004813 RID: 18451 RVA: 0x000E161F File Offset: 0x000DF81F
		public ulong Id { get; set; }

		// Token: 0x06004814 RID: 18452 RVA: 0x000E1628 File Offset: 0x000DF828
		public void SetId(ulong val)
		{
			this.Id = val;
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06004815 RID: 18453 RVA: 0x000E1631 File Offset: 0x000DF831
		// (set) Token: 0x06004816 RID: 18454 RVA: 0x000E1639 File Offset: 0x000DF839
		public Identity InviterIdentity { get; set; }

		// Token: 0x06004817 RID: 18455 RVA: 0x000E1642 File Offset: 0x000DF842
		public void SetInviterIdentity(Identity val)
		{
			this.InviterIdentity = val;
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06004818 RID: 18456 RVA: 0x000E164B File Offset: 0x000DF84B
		// (set) Token: 0x06004819 RID: 18457 RVA: 0x000E1653 File Offset: 0x000DF853
		public Identity InviteeIdentity { get; set; }

		// Token: 0x0600481A RID: 18458 RVA: 0x000E165C File Offset: 0x000DF85C
		public void SetInviteeIdentity(Identity val)
		{
			this.InviteeIdentity = val;
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x0600481B RID: 18459 RVA: 0x000E1665 File Offset: 0x000DF865
		// (set) Token: 0x0600481C RID: 18460 RVA: 0x000E166D File Offset: 0x000DF86D
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

		// Token: 0x0600481D RID: 18461 RVA: 0x000E1680 File Offset: 0x000DF880
		public void SetInviterName(string val)
		{
			this.InviterName = val;
		}

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x0600481E RID: 18462 RVA: 0x000E1689 File Offset: 0x000DF889
		// (set) Token: 0x0600481F RID: 18463 RVA: 0x000E1691 File Offset: 0x000DF891
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

		// Token: 0x06004820 RID: 18464 RVA: 0x000E16A4 File Offset: 0x000DF8A4
		public void SetInviteeName(string val)
		{
			this.InviteeName = val;
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x06004821 RID: 18465 RVA: 0x000E16AD File Offset: 0x000DF8AD
		// (set) Token: 0x06004822 RID: 18466 RVA: 0x000E16B5 File Offset: 0x000DF8B5
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

		// Token: 0x06004823 RID: 18467 RVA: 0x000E16C8 File Offset: 0x000DF8C8
		public void SetInvitationMessage(string val)
		{
			this.InvitationMessage = val;
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x06004824 RID: 18468 RVA: 0x000E16D1 File Offset: 0x000DF8D1
		// (set) Token: 0x06004825 RID: 18469 RVA: 0x000E16D9 File Offset: 0x000DF8D9
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

		// Token: 0x06004826 RID: 18470 RVA: 0x000E16E9 File Offset: 0x000DF8E9
		public void SetCreationTime(ulong val)
		{
			this.CreationTime = val;
		}

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x06004827 RID: 18471 RVA: 0x000E16F2 File Offset: 0x000DF8F2
		// (set) Token: 0x06004828 RID: 18472 RVA: 0x000E16FA File Offset: 0x000DF8FA
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

		// Token: 0x06004829 RID: 18473 RVA: 0x000E170A File Offset: 0x000DF90A
		public void SetExpirationTime(ulong val)
		{
			this.ExpirationTime = val;
		}

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x0600482A RID: 18474 RVA: 0x000E1713 File Offset: 0x000DF913
		// (set) Token: 0x0600482B RID: 18475 RVA: 0x000E171B File Offset: 0x000DF91B
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

		// Token: 0x0600482C RID: 18476 RVA: 0x000E172B File Offset: 0x000DF92B
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x0600482D RID: 18477 RVA: 0x000E1734 File Offset: 0x000DF934
		// (set) Token: 0x0600482E RID: 18478 RVA: 0x000E173C File Offset: 0x000DF93C
		public FriendInvitation FriendInvitation
		{
			get
			{
				return this._FriendInvitation;
			}
			set
			{
				this._FriendInvitation = value;
				this.HasFriendInvitation = (value != null);
			}
		}

		// Token: 0x0600482F RID: 18479 RVA: 0x000E174F File Offset: 0x000DF94F
		public void SetFriendInvitation(FriendInvitation val)
		{
			this.FriendInvitation = val;
		}

		// Token: 0x06004830 RID: 18480 RVA: 0x000E1758 File Offset: 0x000DF958
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
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			if (this.HasFriendInvitation)
			{
				num ^= this.FriendInvitation.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004831 RID: 18481 RVA: 0x000E1844 File Offset: 0x000DFA44
		public override bool Equals(object obj)
		{
			ReceivedInvitation receivedInvitation = obj as ReceivedInvitation;
			return receivedInvitation != null && this.Id.Equals(receivedInvitation.Id) && this.InviterIdentity.Equals(receivedInvitation.InviterIdentity) && this.InviteeIdentity.Equals(receivedInvitation.InviteeIdentity) && this.HasInviterName == receivedInvitation.HasInviterName && (!this.HasInviterName || this.InviterName.Equals(receivedInvitation.InviterName)) && this.HasInviteeName == receivedInvitation.HasInviteeName && (!this.HasInviteeName || this.InviteeName.Equals(receivedInvitation.InviteeName)) && this.HasInvitationMessage == receivedInvitation.HasInvitationMessage && (!this.HasInvitationMessage || this.InvitationMessage.Equals(receivedInvitation.InvitationMessage)) && this.HasCreationTime == receivedInvitation.HasCreationTime && (!this.HasCreationTime || this.CreationTime.Equals(receivedInvitation.CreationTime)) && this.HasExpirationTime == receivedInvitation.HasExpirationTime && (!this.HasExpirationTime || this.ExpirationTime.Equals(receivedInvitation.ExpirationTime)) && this.HasProgram == receivedInvitation.HasProgram && (!this.HasProgram || this.Program.Equals(receivedInvitation.Program)) && this.HasFriendInvitation == receivedInvitation.HasFriendInvitation && (!this.HasFriendInvitation || this.FriendInvitation.Equals(receivedInvitation.FriendInvitation));
		}

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x06004832 RID: 18482 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004833 RID: 18483 RVA: 0x000E19D6 File Offset: 0x000DFBD6
		public static ReceivedInvitation ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ReceivedInvitation>(bs, 0, -1);
		}

		// Token: 0x06004834 RID: 18484 RVA: 0x000E19E0 File Offset: 0x000DFBE0
		public void Deserialize(Stream stream)
		{
			ReceivedInvitation.Deserialize(stream, this);
		}

		// Token: 0x06004835 RID: 18485 RVA: 0x000E19EA File Offset: 0x000DFBEA
		public static ReceivedInvitation Deserialize(Stream stream, ReceivedInvitation instance)
		{
			return ReceivedInvitation.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004836 RID: 18486 RVA: 0x000E19F8 File Offset: 0x000DFBF8
		public static ReceivedInvitation DeserializeLengthDelimited(Stream stream)
		{
			ReceivedInvitation receivedInvitation = new ReceivedInvitation();
			ReceivedInvitation.DeserializeLengthDelimited(stream, receivedInvitation);
			return receivedInvitation;
		}

		// Token: 0x06004837 RID: 18487 RVA: 0x000E1A14 File Offset: 0x000DFC14
		public static ReceivedInvitation DeserializeLengthDelimited(Stream stream, ReceivedInvitation instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ReceivedInvitation.Deserialize(stream, instance, num);
		}

		// Token: 0x06004838 RID: 18488 RVA: 0x000E1A3C File Offset: 0x000DFC3C
		public static ReceivedInvitation Deserialize(Stream stream, ReceivedInvitation instance, long limit)
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
						if (num == 77)
						{
							instance.Program = binaryReader.ReadUInt32();
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					if (field != 103U)
					{
						ProtocolParser.SkipKey(stream, key);
					}
					else if (key.WireType == Wire.LengthDelimited)
					{
						if (instance.FriendInvitation == null)
						{
							instance.FriendInvitation = FriendInvitation.DeserializeLengthDelimited(stream);
						}
						else
						{
							FriendInvitation.DeserializeLengthDelimited(stream, instance.FriendInvitation);
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

		// Token: 0x06004839 RID: 18489 RVA: 0x000E1C29 File Offset: 0x000DFE29
		public void Serialize(Stream stream)
		{
			ReceivedInvitation.Serialize(stream, this);
		}

		// Token: 0x0600483A RID: 18490 RVA: 0x000E1C34 File Offset: 0x000DFE34
		public static void Serialize(Stream stream, ReceivedInvitation instance)
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
			if (instance.HasProgram)
			{
				stream.WriteByte(77);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasFriendInvitation)
			{
				stream.WriteByte(186);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.FriendInvitation.GetSerializedSize());
				FriendInvitation.Serialize(stream, instance.FriendInvitation);
			}
		}

		// Token: 0x0600483B RID: 18491 RVA: 0x000E1DD4 File Offset: 0x000DFFD4
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
			if (this.HasProgram)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasFriendInvitation)
			{
				num += 2U;
				uint serializedSize3 = this.FriendInvitation.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num + 3U;
		}

		// Token: 0x040017F4 RID: 6132
		public bool HasInviterName;

		// Token: 0x040017F5 RID: 6133
		private string _InviterName;

		// Token: 0x040017F6 RID: 6134
		public bool HasInviteeName;

		// Token: 0x040017F7 RID: 6135
		private string _InviteeName;

		// Token: 0x040017F8 RID: 6136
		public bool HasInvitationMessage;

		// Token: 0x040017F9 RID: 6137
		private string _InvitationMessage;

		// Token: 0x040017FA RID: 6138
		public bool HasCreationTime;

		// Token: 0x040017FB RID: 6139
		private ulong _CreationTime;

		// Token: 0x040017FC RID: 6140
		public bool HasExpirationTime;

		// Token: 0x040017FD RID: 6141
		private ulong _ExpirationTime;

		// Token: 0x040017FE RID: 6142
		public bool HasProgram;

		// Token: 0x040017FF RID: 6143
		private uint _Program;

		// Token: 0x04001800 RID: 6144
		public bool HasFriendInvitation;

		// Token: 0x04001801 RID: 6145
		private FriendInvitation _FriendInvitation;
	}
}
