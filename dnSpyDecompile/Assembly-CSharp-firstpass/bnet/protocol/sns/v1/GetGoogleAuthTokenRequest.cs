using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.sns.v1
{
	// Token: 0x020002FF RID: 767
	public class GetGoogleAuthTokenRequest : IProtoBuf
	{
		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06002E19 RID: 11801 RVA: 0x0009D57C File Offset: 0x0009B77C
		// (set) Token: 0x06002E1A RID: 11802 RVA: 0x0009D584 File Offset: 0x0009B784
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

		// Token: 0x06002E1B RID: 11803 RVA: 0x0009D597 File Offset: 0x0009B797
		public void SetAccount(AccountId val)
		{
			this.Account = val;
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x0009D5A0 File Offset: 0x0009B7A0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAccount)
			{
				num ^= this.Account.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002E1D RID: 11805 RVA: 0x0009D5D0 File Offset: 0x0009B7D0
		public override bool Equals(object obj)
		{
			GetGoogleAuthTokenRequest getGoogleAuthTokenRequest = obj as GetGoogleAuthTokenRequest;
			return getGoogleAuthTokenRequest != null && this.HasAccount == getGoogleAuthTokenRequest.HasAccount && (!this.HasAccount || this.Account.Equals(getGoogleAuthTokenRequest.Account));
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06002E1E RID: 11806 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002E1F RID: 11807 RVA: 0x0009D615 File Offset: 0x0009B815
		public static GetGoogleAuthTokenRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGoogleAuthTokenRequest>(bs, 0, -1);
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x0009D61F File Offset: 0x0009B81F
		public void Deserialize(Stream stream)
		{
			GetGoogleAuthTokenRequest.Deserialize(stream, this);
		}

		// Token: 0x06002E21 RID: 11809 RVA: 0x0009D629 File Offset: 0x0009B829
		public static GetGoogleAuthTokenRequest Deserialize(Stream stream, GetGoogleAuthTokenRequest instance)
		{
			return GetGoogleAuthTokenRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002E22 RID: 11810 RVA: 0x0009D634 File Offset: 0x0009B834
		public static GetGoogleAuthTokenRequest DeserializeLengthDelimited(Stream stream)
		{
			GetGoogleAuthTokenRequest getGoogleAuthTokenRequest = new GetGoogleAuthTokenRequest();
			GetGoogleAuthTokenRequest.DeserializeLengthDelimited(stream, getGoogleAuthTokenRequest);
			return getGoogleAuthTokenRequest;
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x0009D650 File Offset: 0x0009B850
		public static GetGoogleAuthTokenRequest DeserializeLengthDelimited(Stream stream, GetGoogleAuthTokenRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetGoogleAuthTokenRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002E24 RID: 11812 RVA: 0x0009D678 File Offset: 0x0009B878
		public static GetGoogleAuthTokenRequest Deserialize(Stream stream, GetGoogleAuthTokenRequest instance, long limit)
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

		// Token: 0x06002E25 RID: 11813 RVA: 0x0009D712 File Offset: 0x0009B912
		public void Serialize(Stream stream)
		{
			GetGoogleAuthTokenRequest.Serialize(stream, this);
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x0009D71B File Offset: 0x0009B91B
		public static void Serialize(Stream stream, GetGoogleAuthTokenRequest instance)
		{
			if (instance.HasAccount)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Account.GetSerializedSize());
				AccountId.Serialize(stream, instance.Account);
			}
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x0009D74C File Offset: 0x0009B94C
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

		// Token: 0x040012B7 RID: 4791
		public bool HasAccount;

		// Token: 0x040012B8 RID: 4792
		private AccountId _Account;
	}
}
