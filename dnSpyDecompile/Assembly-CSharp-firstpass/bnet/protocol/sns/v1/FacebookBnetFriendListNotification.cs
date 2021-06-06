using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.sns.v1
{
	// Token: 0x02000304 RID: 772
	public class FacebookBnetFriendListNotification : IProtoBuf
	{
		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06002E6F RID: 11887 RVA: 0x0009E0BC File Offset: 0x0009C2BC
		// (set) Token: 0x06002E70 RID: 11888 RVA: 0x0009E0C4 File Offset: 0x0009C2C4
		public uint ErrorCode
		{
			get
			{
				return this._ErrorCode;
			}
			set
			{
				this._ErrorCode = value;
				this.HasErrorCode = true;
			}
		}

		// Token: 0x06002E71 RID: 11889 RVA: 0x0009E0D4 File Offset: 0x0009C2D4
		public void SetErrorCode(uint val)
		{
			this.ErrorCode = val;
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06002E72 RID: 11890 RVA: 0x0009E0DD File Offset: 0x0009C2DD
		// (set) Token: 0x06002E73 RID: 11891 RVA: 0x0009E0E5 File Offset: 0x0009C2E5
		public Identity Identity
		{
			get
			{
				return this._Identity;
			}
			set
			{
				this._Identity = value;
				this.HasIdentity = (value != null);
			}
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x0009E0F8 File Offset: 0x0009C2F8
		public void SetIdentity(Identity val)
		{
			this.Identity = val;
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06002E75 RID: 11893 RVA: 0x0009E101 File Offset: 0x0009C301
		// (set) Token: 0x06002E76 RID: 11894 RVA: 0x0009E109 File Offset: 0x0009C309
		public List<FacebookBnetFriend> FriendList
		{
			get
			{
				return this._FriendList;
			}
			set
			{
				this._FriendList = value;
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06002E77 RID: 11895 RVA: 0x0009E101 File Offset: 0x0009C301
		public List<FacebookBnetFriend> FriendListList
		{
			get
			{
				return this._FriendList;
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06002E78 RID: 11896 RVA: 0x0009E112 File Offset: 0x0009C312
		public int FriendListCount
		{
			get
			{
				return this._FriendList.Count;
			}
		}

		// Token: 0x06002E79 RID: 11897 RVA: 0x0009E11F File Offset: 0x0009C31F
		public void AddFriendList(FacebookBnetFriend val)
		{
			this._FriendList.Add(val);
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x0009E12D File Offset: 0x0009C32D
		public void ClearFriendList()
		{
			this._FriendList.Clear();
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x0009E13A File Offset: 0x0009C33A
		public void SetFriendList(List<FacebookBnetFriend> val)
		{
			this.FriendList = val;
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06002E7C RID: 11900 RVA: 0x0009E143 File Offset: 0x0009C343
		// (set) Token: 0x06002E7D RID: 11901 RVA: 0x0009E14B File Offset: 0x0009C34B
		public bool ListEnd
		{
			get
			{
				return this._ListEnd;
			}
			set
			{
				this._ListEnd = value;
				this.HasListEnd = true;
			}
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x0009E15B File Offset: 0x0009C35B
		public void SetListEnd(bool val)
		{
			this.ListEnd = val;
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06002E7F RID: 11903 RVA: 0x0009E164 File Offset: 0x0009C364
		// (set) Token: 0x06002E80 RID: 11904 RVA: 0x0009E16C File Offset: 0x0009C36C
		public uint Token
		{
			get
			{
				return this._Token;
			}
			set
			{
				this._Token = value;
				this.HasToken = true;
			}
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x0009E17C File Offset: 0x0009C37C
		public void SetToken(uint val)
		{
			this.Token = val;
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06002E82 RID: 11906 RVA: 0x0009E185 File Offset: 0x0009C385
		// (set) Token: 0x06002E83 RID: 11907 RVA: 0x0009E18D File Offset: 0x0009C38D
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

		// Token: 0x06002E84 RID: 11908 RVA: 0x0009E1A0 File Offset: 0x0009C3A0
		public void SetFbId(string val)
		{
			this.FbId = val;
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06002E85 RID: 11909 RVA: 0x0009E1A9 File Offset: 0x0009C3A9
		// (set) Token: 0x06002E86 RID: 11910 RVA: 0x0009E1B1 File Offset: 0x0009C3B1
		public ObjectAddress Address
		{
			get
			{
				return this._Address;
			}
			set
			{
				this._Address = value;
				this.HasAddress = (value != null);
			}
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x0009E1C4 File Offset: 0x0009C3C4
		public void SetAddress(ObjectAddress val)
		{
			this.Address = val;
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x0009E1D0 File Offset: 0x0009C3D0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasErrorCode)
			{
				num ^= this.ErrorCode.GetHashCode();
			}
			if (this.HasIdentity)
			{
				num ^= this.Identity.GetHashCode();
			}
			foreach (FacebookBnetFriend facebookBnetFriend in this.FriendList)
			{
				num ^= facebookBnetFriend.GetHashCode();
			}
			if (this.HasListEnd)
			{
				num ^= this.ListEnd.GetHashCode();
			}
			if (this.HasToken)
			{
				num ^= this.Token.GetHashCode();
			}
			if (this.HasFbId)
			{
				num ^= this.FbId.GetHashCode();
			}
			if (this.HasAddress)
			{
				num ^= this.Address.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x0009E2C0 File Offset: 0x0009C4C0
		public override bool Equals(object obj)
		{
			FacebookBnetFriendListNotification facebookBnetFriendListNotification = obj as FacebookBnetFriendListNotification;
			if (facebookBnetFriendListNotification == null)
			{
				return false;
			}
			if (this.HasErrorCode != facebookBnetFriendListNotification.HasErrorCode || (this.HasErrorCode && !this.ErrorCode.Equals(facebookBnetFriendListNotification.ErrorCode)))
			{
				return false;
			}
			if (this.HasIdentity != facebookBnetFriendListNotification.HasIdentity || (this.HasIdentity && !this.Identity.Equals(facebookBnetFriendListNotification.Identity)))
			{
				return false;
			}
			if (this.FriendList.Count != facebookBnetFriendListNotification.FriendList.Count)
			{
				return false;
			}
			for (int i = 0; i < this.FriendList.Count; i++)
			{
				if (!this.FriendList[i].Equals(facebookBnetFriendListNotification.FriendList[i]))
				{
					return false;
				}
			}
			return this.HasListEnd == facebookBnetFriendListNotification.HasListEnd && (!this.HasListEnd || this.ListEnd.Equals(facebookBnetFriendListNotification.ListEnd)) && this.HasToken == facebookBnetFriendListNotification.HasToken && (!this.HasToken || this.Token.Equals(facebookBnetFriendListNotification.Token)) && this.HasFbId == facebookBnetFriendListNotification.HasFbId && (!this.HasFbId || this.FbId.Equals(facebookBnetFriendListNotification.FbId)) && this.HasAddress == facebookBnetFriendListNotification.HasAddress && (!this.HasAddress || this.Address.Equals(facebookBnetFriendListNotification.Address));
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06002E8A RID: 11914 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x0009E436 File Offset: 0x0009C636
		public static FacebookBnetFriendListNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FacebookBnetFriendListNotification>(bs, 0, -1);
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x0009E440 File Offset: 0x0009C640
		public void Deserialize(Stream stream)
		{
			FacebookBnetFriendListNotification.Deserialize(stream, this);
		}

		// Token: 0x06002E8D RID: 11917 RVA: 0x0009E44A File Offset: 0x0009C64A
		public static FacebookBnetFriendListNotification Deserialize(Stream stream, FacebookBnetFriendListNotification instance)
		{
			return FacebookBnetFriendListNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x0009E458 File Offset: 0x0009C658
		public static FacebookBnetFriendListNotification DeserializeLengthDelimited(Stream stream)
		{
			FacebookBnetFriendListNotification facebookBnetFriendListNotification = new FacebookBnetFriendListNotification();
			FacebookBnetFriendListNotification.DeserializeLengthDelimited(stream, facebookBnetFriendListNotification);
			return facebookBnetFriendListNotification;
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x0009E474 File Offset: 0x0009C674
		public static FacebookBnetFriendListNotification DeserializeLengthDelimited(Stream stream, FacebookBnetFriendListNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FacebookBnetFriendListNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06002E90 RID: 11920 RVA: 0x0009E49C File Offset: 0x0009C69C
		public static FacebookBnetFriendListNotification Deserialize(Stream stream, FacebookBnetFriendListNotification instance, long limit)
		{
			if (instance.FriendList == null)
			{
				instance.FriendList = new List<FacebookBnetFriend>();
			}
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
						if (num == 8)
						{
							instance.ErrorCode = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num != 18)
						{
							if (num == 26)
							{
								instance.FriendList.Add(FacebookBnetFriend.DeserializeLengthDelimited(stream));
								continue;
							}
						}
						else
						{
							if (instance.Identity == null)
							{
								instance.Identity = Identity.DeserializeLengthDelimited(stream);
								continue;
							}
							Identity.DeserializeLengthDelimited(stream, instance.Identity);
							continue;
						}
					}
					else if (num <= 40)
					{
						if (num == 32)
						{
							instance.ListEnd = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 40)
						{
							instance.Token = ProtocolParser.ReadUInt32(stream);
							continue;
						}
					}
					else
					{
						if (num == 50)
						{
							instance.FbId = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 58)
						{
							if (instance.Address == null)
							{
								instance.Address = ObjectAddress.DeserializeLengthDelimited(stream);
								continue;
							}
							ObjectAddress.DeserializeLengthDelimited(stream, instance.Address);
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

		// Token: 0x06002E91 RID: 11921 RVA: 0x0009E616 File Offset: 0x0009C816
		public void Serialize(Stream stream)
		{
			FacebookBnetFriendListNotification.Serialize(stream, this);
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x0009E620 File Offset: 0x0009C820
		public static void Serialize(Stream stream, FacebookBnetFriendListNotification instance)
		{
			if (instance.HasErrorCode)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.ErrorCode);
			}
			if (instance.HasIdentity)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Identity.GetSerializedSize());
				Identity.Serialize(stream, instance.Identity);
			}
			if (instance.FriendList.Count > 0)
			{
				foreach (FacebookBnetFriend facebookBnetFriend in instance.FriendList)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, facebookBnetFriend.GetSerializedSize());
					FacebookBnetFriend.Serialize(stream, facebookBnetFriend);
				}
			}
			if (instance.HasListEnd)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.ListEnd);
			}
			if (instance.HasToken)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt32(stream, instance.Token);
			}
			if (instance.HasFbId)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FbId));
			}
			if (instance.HasAddress)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteUInt32(stream, instance.Address.GetSerializedSize());
				ObjectAddress.Serialize(stream, instance.Address);
			}
		}

		// Token: 0x06002E93 RID: 11923 RVA: 0x0009E768 File Offset: 0x0009C968
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasErrorCode)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.ErrorCode);
			}
			if (this.HasIdentity)
			{
				num += 1U;
				uint serializedSize = this.Identity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.FriendList.Count > 0)
			{
				foreach (FacebookBnetFriend facebookBnetFriend in this.FriendList)
				{
					num += 1U;
					uint serializedSize2 = facebookBnetFriend.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasListEnd)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasToken)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Token);
			}
			if (this.HasFbId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.FbId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasAddress)
			{
				num += 1U;
				uint serializedSize3 = this.Address.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		// Token: 0x040012C5 RID: 4805
		public bool HasErrorCode;

		// Token: 0x040012C6 RID: 4806
		private uint _ErrorCode;

		// Token: 0x040012C7 RID: 4807
		public bool HasIdentity;

		// Token: 0x040012C8 RID: 4808
		private Identity _Identity;

		// Token: 0x040012C9 RID: 4809
		private List<FacebookBnetFriend> _FriendList = new List<FacebookBnetFriend>();

		// Token: 0x040012CA RID: 4810
		public bool HasListEnd;

		// Token: 0x040012CB RID: 4811
		private bool _ListEnd;

		// Token: 0x040012CC RID: 4812
		public bool HasToken;

		// Token: 0x040012CD RID: 4813
		private uint _Token;

		// Token: 0x040012CE RID: 4814
		public bool HasFbId;

		// Token: 0x040012CF RID: 4815
		private string _FbId;

		// Token: 0x040012D0 RID: 4816
		public bool HasAddress;

		// Token: 0x040012D1 RID: 4817
		private ObjectAddress _Address;
	}
}
