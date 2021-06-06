using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000513 RID: 1299
	public class GetLicensesResponse : IProtoBuf
	{
		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x06005C97 RID: 23703 RVA: 0x0011976B File Offset: 0x0011796B
		// (set) Token: 0x06005C98 RID: 23704 RVA: 0x00119773 File Offset: 0x00117973
		public List<AccountLicense> Licenses
		{
			get
			{
				return this._Licenses;
			}
			set
			{
				this._Licenses = value;
			}
		}

		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x06005C99 RID: 23705 RVA: 0x0011976B File Offset: 0x0011796B
		public List<AccountLicense> LicensesList
		{
			get
			{
				return this._Licenses;
			}
		}

		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x06005C9A RID: 23706 RVA: 0x0011977C File Offset: 0x0011797C
		public int LicensesCount
		{
			get
			{
				return this._Licenses.Count;
			}
		}

		// Token: 0x06005C9B RID: 23707 RVA: 0x00119789 File Offset: 0x00117989
		public void AddLicenses(AccountLicense val)
		{
			this._Licenses.Add(val);
		}

		// Token: 0x06005C9C RID: 23708 RVA: 0x00119797 File Offset: 0x00117997
		public void ClearLicenses()
		{
			this._Licenses.Clear();
		}

		// Token: 0x06005C9D RID: 23709 RVA: 0x001197A4 File Offset: 0x001179A4
		public void SetLicenses(List<AccountLicense> val)
		{
			this.Licenses = val;
		}

		// Token: 0x06005C9E RID: 23710 RVA: 0x001197B0 File Offset: 0x001179B0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (AccountLicense accountLicense in this.Licenses)
			{
				num ^= accountLicense.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005C9F RID: 23711 RVA: 0x00119814 File Offset: 0x00117A14
		public override bool Equals(object obj)
		{
			GetLicensesResponse getLicensesResponse = obj as GetLicensesResponse;
			if (getLicensesResponse == null)
			{
				return false;
			}
			if (this.Licenses.Count != getLicensesResponse.Licenses.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Licenses.Count; i++)
			{
				if (!this.Licenses[i].Equals(getLicensesResponse.Licenses[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x06005CA0 RID: 23712 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005CA1 RID: 23713 RVA: 0x0011987F File Offset: 0x00117A7F
		public static GetLicensesResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetLicensesResponse>(bs, 0, -1);
		}

		// Token: 0x06005CA2 RID: 23714 RVA: 0x00119889 File Offset: 0x00117A89
		public void Deserialize(Stream stream)
		{
			GetLicensesResponse.Deserialize(stream, this);
		}

		// Token: 0x06005CA3 RID: 23715 RVA: 0x00119893 File Offset: 0x00117A93
		public static GetLicensesResponse Deserialize(Stream stream, GetLicensesResponse instance)
		{
			return GetLicensesResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005CA4 RID: 23716 RVA: 0x001198A0 File Offset: 0x00117AA0
		public static GetLicensesResponse DeserializeLengthDelimited(Stream stream)
		{
			GetLicensesResponse getLicensesResponse = new GetLicensesResponse();
			GetLicensesResponse.DeserializeLengthDelimited(stream, getLicensesResponse);
			return getLicensesResponse;
		}

		// Token: 0x06005CA5 RID: 23717 RVA: 0x001198BC File Offset: 0x00117ABC
		public static GetLicensesResponse DeserializeLengthDelimited(Stream stream, GetLicensesResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetLicensesResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06005CA6 RID: 23718 RVA: 0x001198E4 File Offset: 0x00117AE4
		public static GetLicensesResponse Deserialize(Stream stream, GetLicensesResponse instance, long limit)
		{
			if (instance.Licenses == null)
			{
				instance.Licenses = new List<AccountLicense>();
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
				else if (num == 10)
				{
					instance.Licenses.Add(AccountLicense.DeserializeLengthDelimited(stream));
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

		// Token: 0x06005CA7 RID: 23719 RVA: 0x0011997C File Offset: 0x00117B7C
		public void Serialize(Stream stream)
		{
			GetLicensesResponse.Serialize(stream, this);
		}

		// Token: 0x06005CA8 RID: 23720 RVA: 0x00119988 File Offset: 0x00117B88
		public static void Serialize(Stream stream, GetLicensesResponse instance)
		{
			if (instance.Licenses.Count > 0)
			{
				foreach (AccountLicense accountLicense in instance.Licenses)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, accountLicense.GetSerializedSize());
					AccountLicense.Serialize(stream, accountLicense);
				}
			}
		}

		// Token: 0x06005CA9 RID: 23721 RVA: 0x00119A00 File Offset: 0x00117C00
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Licenses.Count > 0)
			{
				foreach (AccountLicense accountLicense in this.Licenses)
				{
					num += 1U;
					uint serializedSize = accountLicense.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04001CA7 RID: 7335
		private List<AccountLicense> _Licenses = new List<AccountLicense>();
	}
}
