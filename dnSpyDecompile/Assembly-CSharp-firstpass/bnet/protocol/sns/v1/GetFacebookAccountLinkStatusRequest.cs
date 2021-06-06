using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.sns.v1
{
	// Token: 0x020002FD RID: 765
	public class GetFacebookAccountLinkStatusRequest : IProtoBuf
	{
		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06002DF3 RID: 11763 RVA: 0x0009D010 File Offset: 0x0009B210
		// (set) Token: 0x06002DF4 RID: 11764 RVA: 0x0009D018 File Offset: 0x0009B218
		public AccountId Account
		{
			get
			{
				return this._Account;
			}
			set
			{
				this._Account = value;
				this.HasAccount = (value != null);
			}
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x0009D02B File Offset: 0x0009B22B
		public void SetAccount(AccountId val)
		{
			this.Account = val;
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x0009D034 File Offset: 0x0009B234
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAccount)
			{
				num ^= this.Account.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x0009D064 File Offset: 0x0009B264
		public override bool Equals(object obj)
		{
			GetFacebookAccountLinkStatusRequest getFacebookAccountLinkStatusRequest = obj as GetFacebookAccountLinkStatusRequest;
			return getFacebookAccountLinkStatusRequest != null && this.HasAccount == getFacebookAccountLinkStatusRequest.HasAccount && (!this.HasAccount || this.Account.Equals(getFacebookAccountLinkStatusRequest.Account));
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06002DF8 RID: 11768 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x0009D0A9 File Offset: 0x0009B2A9
		public static GetFacebookAccountLinkStatusRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFacebookAccountLinkStatusRequest>(bs, 0, -1);
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x0009D0B3 File Offset: 0x0009B2B3
		public void Deserialize(Stream stream)
		{
			GetFacebookAccountLinkStatusRequest.Deserialize(stream, this);
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x0009D0BD File Offset: 0x0009B2BD
		public static GetFacebookAccountLinkStatusRequest Deserialize(Stream stream, GetFacebookAccountLinkStatusRequest instance)
		{
			return GetFacebookAccountLinkStatusRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x0009D0C8 File Offset: 0x0009B2C8
		public static GetFacebookAccountLinkStatusRequest DeserializeLengthDelimited(Stream stream)
		{
			GetFacebookAccountLinkStatusRequest getFacebookAccountLinkStatusRequest = new GetFacebookAccountLinkStatusRequest();
			GetFacebookAccountLinkStatusRequest.DeserializeLengthDelimited(stream, getFacebookAccountLinkStatusRequest);
			return getFacebookAccountLinkStatusRequest;
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x0009D0E4 File Offset: 0x0009B2E4
		public static GetFacebookAccountLinkStatusRequest DeserializeLengthDelimited(Stream stream, GetFacebookAccountLinkStatusRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetFacebookAccountLinkStatusRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x0009D10C File Offset: 0x0009B30C
		public static GetFacebookAccountLinkStatusRequest Deserialize(Stream stream, GetFacebookAccountLinkStatusRequest instance, long limit)
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
				else if (num == 10)
				{
					if (instance.Account == null)
					{
						instance.Account = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.Account);
					}
				}
				else
				{
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

		// Token: 0x06002DFF RID: 11775 RVA: 0x0009D1A6 File Offset: 0x0009B3A6
		public void Serialize(Stream stream)
		{
			GetFacebookAccountLinkStatusRequest.Serialize(stream, this);
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x0009D1AF File Offset: 0x0009B3AF
		public static void Serialize(Stream stream, GetFacebookAccountLinkStatusRequest instance)
		{
			if (instance.HasAccount)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Account.GetSerializedSize());
				AccountId.Serialize(stream, instance.Account);
			}
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x0009D1E0 File Offset: 0x0009B3E0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAccount)
			{
				num += 1U;
				uint serializedSize = this.Account.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x040012AF RID: 4783
		public bool HasAccount;

		// Token: 0x040012B0 RID: 4784
		private AccountId _Account;
	}
}
