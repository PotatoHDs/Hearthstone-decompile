using System;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.sns.v1
{
	// Token: 0x02000305 RID: 773
	public class FacebookBnetFriend : IProtoBuf
	{
		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06002E95 RID: 11925 RVA: 0x0009E8A7 File Offset: 0x0009CAA7
		// (set) Token: 0x06002E96 RID: 11926 RVA: 0x0009E8AF File Offset: 0x0009CAAF
		public AccountId BnetId
		{
			get
			{
				return this._BnetId;
			}
			set
			{
				this._BnetId = value;
				this.HasBnetId = (value != null);
			}
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x0009E8C2 File Offset: 0x0009CAC2
		public void SetBnetId(AccountId val)
		{
			this.BnetId = val;
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06002E98 RID: 11928 RVA: 0x0009E8CB File Offset: 0x0009CACB
		// (set) Token: 0x06002E99 RID: 11929 RVA: 0x0009E8D3 File Offset: 0x0009CAD3
		public string FbId
		{
			get
			{
				return this._FbId;
			}
			set
			{
				this._FbId = value;
				this.HasFbId = (value != null);
			}
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x0009E8E6 File Offset: 0x0009CAE6
		public void SetFbId(string val)
		{
			this.FbId = val;
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06002E9B RID: 11931 RVA: 0x0009E8EF File Offset: 0x0009CAEF
		// (set) Token: 0x06002E9C RID: 11932 RVA: 0x0009E8F7 File Offset: 0x0009CAF7
		public string LastName
		{
			get
			{
				return this._LastName;
			}
			set
			{
				this._LastName = value;
				this.HasLastName = (value != null);
			}
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x0009E90A File Offset: 0x0009CB0A
		public void SetLastName(string val)
		{
			this.LastName = val;
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06002E9E RID: 11934 RVA: 0x0009E913 File Offset: 0x0009CB13
		// (set) Token: 0x06002E9F RID: 11935 RVA: 0x0009E91B File Offset: 0x0009CB1B
		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set
			{
				this._FirstName = value;
				this.HasFirstName = (value != null);
			}
		}

		// Token: 0x06002EA0 RID: 11936 RVA: 0x0009E92E File Offset: 0x0009CB2E
		public void SetFirstName(string val)
		{
			this.FirstName = val;
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06002EA1 RID: 11937 RVA: 0x0009E937 File Offset: 0x0009CB37
		// (set) Token: 0x06002EA2 RID: 11938 RVA: 0x0009E93F File Offset: 0x0009CB3F
		public string ProfilePicture
		{
			get
			{
				return this._ProfilePicture;
			}
			set
			{
				this._ProfilePicture = value;
				this.HasProfilePicture = (value != null);
			}
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x0009E952 File Offset: 0x0009CB52
		public void SetProfilePicture(string val)
		{
			this.ProfilePicture = val;
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06002EA4 RID: 11940 RVA: 0x0009E95B File Offset: 0x0009CB5B
		// (set) Token: 0x06002EA5 RID: 11941 RVA: 0x0009E963 File Offset: 0x0009CB63
		public string DisplayName
		{
			get
			{
				return this._DisplayName;
			}
			set
			{
				this._DisplayName = value;
				this.HasDisplayName = (value != null);
			}
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x0009E976 File Offset: 0x0009CB76
		public void SetDisplayName(string val)
		{
			this.DisplayName = val;
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x0009E980 File Offset: 0x0009CB80
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasBnetId)
			{
				num ^= this.BnetId.GetHashCode();
			}
			if (this.HasFbId)
			{
				num ^= this.FbId.GetHashCode();
			}
			if (this.HasLastName)
			{
				num ^= this.LastName.GetHashCode();
			}
			if (this.HasFirstName)
			{
				num ^= this.FirstName.GetHashCode();
			}
			if (this.HasProfilePicture)
			{
				num ^= this.ProfilePicture.GetHashCode();
			}
			if (this.HasDisplayName)
			{
				num ^= this.DisplayName.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x0009EA20 File Offset: 0x0009CC20
		public override bool Equals(object obj)
		{
			FacebookBnetFriend facebookBnetFriend = obj as FacebookBnetFriend;
			return facebookBnetFriend != null && this.HasBnetId == facebookBnetFriend.HasBnetId && (!this.HasBnetId || this.BnetId.Equals(facebookBnetFriend.BnetId)) && this.HasFbId == facebookBnetFriend.HasFbId && (!this.HasFbId || this.FbId.Equals(facebookBnetFriend.FbId)) && this.HasLastName == facebookBnetFriend.HasLastName && (!this.HasLastName || this.LastName.Equals(facebookBnetFriend.LastName)) && this.HasFirstName == facebookBnetFriend.HasFirstName && (!this.HasFirstName || this.FirstName.Equals(facebookBnetFriend.FirstName)) && this.HasProfilePicture == facebookBnetFriend.HasProfilePicture && (!this.HasProfilePicture || this.ProfilePicture.Equals(facebookBnetFriend.ProfilePicture)) && this.HasDisplayName == facebookBnetFriend.HasDisplayName && (!this.HasDisplayName || this.DisplayName.Equals(facebookBnetFriend.DisplayName));
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06002EA9 RID: 11945 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x0009EB3C File Offset: 0x0009CD3C
		public static FacebookBnetFriend ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FacebookBnetFriend>(bs, 0, -1);
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x0009EB46 File Offset: 0x0009CD46
		public void Deserialize(Stream stream)
		{
			FacebookBnetFriend.Deserialize(stream, this);
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x0009EB50 File Offset: 0x0009CD50
		public static FacebookBnetFriend Deserialize(Stream stream, FacebookBnetFriend instance)
		{
			return FacebookBnetFriend.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x0009EB5C File Offset: 0x0009CD5C
		public static FacebookBnetFriend DeserializeLengthDelimited(Stream stream)
		{
			FacebookBnetFriend facebookBnetFriend = new FacebookBnetFriend();
			FacebookBnetFriend.DeserializeLengthDelimited(stream, facebookBnetFriend);
			return facebookBnetFriend;
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x0009EB78 File Offset: 0x0009CD78
		public static FacebookBnetFriend DeserializeLengthDelimited(Stream stream, FacebookBnetFriend instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FacebookBnetFriend.Deserialize(stream, instance, num);
		}

		// Token: 0x06002EAF RID: 11951 RVA: 0x0009EBA0 File Offset: 0x0009CDA0
		public static FacebookBnetFriend Deserialize(Stream stream, FacebookBnetFriend instance, long limit)
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
				else
				{
					if (num <= 26)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								instance.FbId = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 26)
							{
								instance.LastName = ProtocolParser.ReadString(stream);
								continue;
							}
						}
						else
						{
							if (instance.BnetId == null)
							{
								instance.BnetId = AccountId.DeserializeLengthDelimited(stream);
								continue;
							}
							AccountId.DeserializeLengthDelimited(stream, instance.BnetId);
							continue;
						}
					}
					else
					{
						if (num == 34)
						{
							instance.FirstName = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 42)
						{
							instance.ProfilePicture = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 50)
						{
							instance.DisplayName = ProtocolParser.ReadString(stream);
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

		// Token: 0x06002EB0 RID: 11952 RVA: 0x0009ECC3 File Offset: 0x0009CEC3
		public void Serialize(Stream stream)
		{
			FacebookBnetFriend.Serialize(stream, this);
		}

		// Token: 0x06002EB1 RID: 11953 RVA: 0x0009ECCC File Offset: 0x0009CECC
		public static void Serialize(Stream stream, FacebookBnetFriend instance)
		{
			if (instance.HasBnetId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.BnetId.GetSerializedSize());
				AccountId.Serialize(stream, instance.BnetId);
			}
			if (instance.HasFbId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FbId));
			}
			if (instance.HasLastName)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.LastName));
			}
			if (instance.HasFirstName)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FirstName));
			}
			if (instance.HasProfilePicture)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ProfilePicture));
			}
			if (instance.HasDisplayName)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DisplayName));
			}
		}

		// Token: 0x06002EB2 RID: 11954 RVA: 0x0009EDC4 File Offset: 0x0009CFC4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasBnetId)
			{
				num += 1U;
				uint serializedSize = this.BnetId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasFbId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.FbId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasLastName)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.LastName);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasFirstName)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.FirstName);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasProfilePicture)
			{
				num += 1U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.ProfilePicture);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (this.HasDisplayName)
			{
				num += 1U;
				uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(this.DisplayName);
				num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			}
			return num;
		}

		// Token: 0x040012D2 RID: 4818
		public bool HasBnetId;

		// Token: 0x040012D3 RID: 4819
		private AccountId _BnetId;

		// Token: 0x040012D4 RID: 4820
		public bool HasFbId;

		// Token: 0x040012D5 RID: 4821
		private string _FbId;

		// Token: 0x040012D6 RID: 4822
		public bool HasLastName;

		// Token: 0x040012D7 RID: 4823
		private string _LastName;

		// Token: 0x040012D8 RID: 4824
		public bool HasFirstName;

		// Token: 0x040012D9 RID: 4825
		private string _FirstName;

		// Token: 0x040012DA RID: 4826
		public bool HasProfilePicture;

		// Token: 0x040012DB RID: 4827
		private string _ProfilePicture;

		// Token: 0x040012DC RID: 4828
		public bool HasDisplayName;

		// Token: 0x040012DD RID: 4829
		private string _DisplayName;
	}
}
