using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.sns.v1
{
	// Token: 0x020002FB RID: 763
	public class GetFacebookBnetFriendsRequest : IProtoBuf
	{
		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06002DC4 RID: 11716 RVA: 0x0009C7F8 File Offset: 0x0009A9F8
		// (set) Token: 0x06002DC5 RID: 11717 RVA: 0x0009C800 File Offset: 0x0009AA00
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

		// Token: 0x06002DC6 RID: 11718 RVA: 0x0009C813 File Offset: 0x0009AA13
		public void SetIdentity(Identity val)
		{
			this.Identity = val;
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06002DC7 RID: 11719 RVA: 0x0009C81C File Offset: 0x0009AA1C
		// (set) Token: 0x06002DC8 RID: 11720 RVA: 0x0009C824 File Offset: 0x0009AA24
		public GetFacebookBnetFriendsRequest.Types.ProfilePictureType ProfilePictureType
		{
			get
			{
				return this._ProfilePictureType;
			}
			set
			{
				this._ProfilePictureType = value;
				this.HasProfilePictureType = true;
			}
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x0009C834 File Offset: 0x0009AA34
		public void SetProfilePictureType(GetFacebookBnetFriendsRequest.Types.ProfilePictureType val)
		{
			this.ProfilePictureType = val;
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06002DCA RID: 11722 RVA: 0x0009C83D File Offset: 0x0009AA3D
		// (set) Token: 0x06002DCB RID: 11723 RVA: 0x0009C845 File Offset: 0x0009AA45
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

		// Token: 0x06002DCC RID: 11724 RVA: 0x0009C855 File Offset: 0x0009AA55
		public void SetToken(uint val)
		{
			this.Token = val;
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06002DCD RID: 11725 RVA: 0x0009C85E File Offset: 0x0009AA5E
		// (set) Token: 0x06002DCE RID: 11726 RVA: 0x0009C866 File Offset: 0x0009AA66
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

		// Token: 0x06002DCF RID: 11727 RVA: 0x0009C879 File Offset: 0x0009AA79
		public void SetAddress(ObjectAddress val)
		{
			this.Address = val;
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x0009C884 File Offset: 0x0009AA84
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasIdentity)
			{
				num ^= this.Identity.GetHashCode();
			}
			if (this.HasProfilePictureType)
			{
				num ^= this.ProfilePictureType.GetHashCode();
			}
			if (this.HasToken)
			{
				num ^= this.Token.GetHashCode();
			}
			if (this.HasAddress)
			{
				num ^= this.Address.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x0009C904 File Offset: 0x0009AB04
		public override bool Equals(object obj)
		{
			GetFacebookBnetFriendsRequest getFacebookBnetFriendsRequest = obj as GetFacebookBnetFriendsRequest;
			return getFacebookBnetFriendsRequest != null && this.HasIdentity == getFacebookBnetFriendsRequest.HasIdentity && (!this.HasIdentity || this.Identity.Equals(getFacebookBnetFriendsRequest.Identity)) && this.HasProfilePictureType == getFacebookBnetFriendsRequest.HasProfilePictureType && (!this.HasProfilePictureType || this.ProfilePictureType.Equals(getFacebookBnetFriendsRequest.ProfilePictureType)) && this.HasToken == getFacebookBnetFriendsRequest.HasToken && (!this.HasToken || this.Token.Equals(getFacebookBnetFriendsRequest.Token)) && this.HasAddress == getFacebookBnetFriendsRequest.HasAddress && (!this.HasAddress || this.Address.Equals(getFacebookBnetFriendsRequest.Address));
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06002DD2 RID: 11730 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002DD3 RID: 11731 RVA: 0x0009C9DB File Offset: 0x0009ABDB
		public static GetFacebookBnetFriendsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFacebookBnetFriendsRequest>(bs, 0, -1);
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x0009C9E5 File Offset: 0x0009ABE5
		public void Deserialize(Stream stream)
		{
			GetFacebookBnetFriendsRequest.Deserialize(stream, this);
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x0009C9EF File Offset: 0x0009ABEF
		public static GetFacebookBnetFriendsRequest Deserialize(Stream stream, GetFacebookBnetFriendsRequest instance)
		{
			return GetFacebookBnetFriendsRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x0009C9FC File Offset: 0x0009ABFC
		public static GetFacebookBnetFriendsRequest DeserializeLengthDelimited(Stream stream)
		{
			GetFacebookBnetFriendsRequest getFacebookBnetFriendsRequest = new GetFacebookBnetFriendsRequest();
			GetFacebookBnetFriendsRequest.DeserializeLengthDelimited(stream, getFacebookBnetFriendsRequest);
			return getFacebookBnetFriendsRequest;
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x0009CA18 File Offset: 0x0009AC18
		public static GetFacebookBnetFriendsRequest DeserializeLengthDelimited(Stream stream, GetFacebookBnetFriendsRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetFacebookBnetFriendsRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x0009CA40 File Offset: 0x0009AC40
		public static GetFacebookBnetFriendsRequest Deserialize(Stream stream, GetFacebookBnetFriendsRequest instance, long limit)
		{
			instance.ProfilePictureType = GetFacebookBnetFriendsRequest.Types.ProfilePictureType.SMALL;
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
					if (num <= 16)
					{
						if (num != 10)
						{
							if (num == 16)
							{
								instance.ProfilePictureType = (GetFacebookBnetFriendsRequest.Types.ProfilePictureType)ProtocolParser.ReadUInt64(stream);
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
					else
					{
						if (num == 24)
						{
							instance.Token = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 34)
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

		// Token: 0x06002DD9 RID: 11737 RVA: 0x0009CB53 File Offset: 0x0009AD53
		public void Serialize(Stream stream)
		{
			GetFacebookBnetFriendsRequest.Serialize(stream, this);
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x0009CB5C File Offset: 0x0009AD5C
		public static void Serialize(Stream stream, GetFacebookBnetFriendsRequest instance)
		{
			if (instance.HasIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Identity.GetSerializedSize());
				Identity.Serialize(stream, instance.Identity);
			}
			if (instance.HasProfilePictureType)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ProfilePictureType));
			}
			if (instance.HasToken)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Token);
			}
			if (instance.HasAddress)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Address.GetSerializedSize());
				ObjectAddress.Serialize(stream, instance.Address);
			}
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x0009CBFC File Offset: 0x0009ADFC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasIdentity)
			{
				num += 1U;
				uint serializedSize = this.Identity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasProfilePictureType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ProfilePictureType));
			}
			if (this.HasToken)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Token);
			}
			if (this.HasAddress)
			{
				num += 1U;
				uint serializedSize2 = this.Address.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x040012A1 RID: 4769
		public bool HasIdentity;

		// Token: 0x040012A2 RID: 4770
		private Identity _Identity;

		// Token: 0x040012A3 RID: 4771
		public bool HasProfilePictureType;

		// Token: 0x040012A4 RID: 4772
		private GetFacebookBnetFriendsRequest.Types.ProfilePictureType _ProfilePictureType;

		// Token: 0x040012A5 RID: 4773
		public bool HasToken;

		// Token: 0x040012A6 RID: 4774
		private uint _Token;

		// Token: 0x040012A7 RID: 4775
		public bool HasAddress;

		// Token: 0x040012A8 RID: 4776
		private ObjectAddress _Address;

		// Token: 0x020006FA RID: 1786
		public static class Types
		{
			// Token: 0x02000711 RID: 1809
			public enum ProfilePictureType
			{
				// Token: 0x040022F6 RID: 8950
				SMALL,
				// Token: 0x040022F7 RID: 8951
				NORMAL,
				// Token: 0x040022F8 RID: 8952
				LARGE,
				// Token: 0x040022F9 RID: 8953
				SQUARE
			}
		}
	}
}
