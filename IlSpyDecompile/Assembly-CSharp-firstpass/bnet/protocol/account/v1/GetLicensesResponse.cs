using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.account.v1
{
	public class GetLicensesResponse : IProtoBuf
	{
		private List<AccountLicense> _Licenses = new List<AccountLicense>();

		public List<AccountLicense> Licenses
		{
			get
			{
				return _Licenses;
			}
			set
			{
				_Licenses = value;
			}
		}

		public List<AccountLicense> LicensesList => _Licenses;

		public int LicensesCount => _Licenses.Count;

		public bool IsInitialized => true;

		public void AddLicenses(AccountLicense val)
		{
			_Licenses.Add(val);
		}

		public void ClearLicenses()
		{
			_Licenses.Clear();
		}

		public void SetLicenses(List<AccountLicense> val)
		{
			Licenses = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (AccountLicense license in Licenses)
			{
				num ^= license.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetLicensesResponse getLicensesResponse = obj as GetLicensesResponse;
			if (getLicensesResponse == null)
			{
				return false;
			}
			if (Licenses.Count != getLicensesResponse.Licenses.Count)
			{
				return false;
			}
			for (int i = 0; i < Licenses.Count; i++)
			{
				if (!Licenses[i].Equals(getLicensesResponse.Licenses[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static GetLicensesResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetLicensesResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetLicensesResponse Deserialize(Stream stream, GetLicensesResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetLicensesResponse DeserializeLengthDelimited(Stream stream)
		{
			GetLicensesResponse getLicensesResponse = new GetLicensesResponse();
			DeserializeLengthDelimited(stream, getLicensesResponse);
			return getLicensesResponse;
		}

		public static GetLicensesResponse DeserializeLengthDelimited(Stream stream, GetLicensesResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetLicensesResponse Deserialize(Stream stream, GetLicensesResponse instance, long limit)
		{
			if (instance.Licenses == null)
			{
				instance.Licenses = new List<AccountLicense>();
			}
			while (true)
			{
				if (limit >= 0 && stream.Position >= limit)
				{
					if (stream.Position == limit)
					{
						break;
					}
					throw new ProtocolBufferException("Read past max limit");
				}
				int num = stream.ReadByte();
				switch (num)
				{
				case -1:
					break;
				case 10:
					instance.Licenses.Add(AccountLicense.DeserializeLengthDelimited(stream));
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
					continue;
				}
				}
				if (limit < 0)
				{
					break;
				}
				throw new EndOfStreamException();
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetLicensesResponse instance)
		{
			if (instance.Licenses.Count <= 0)
			{
				return;
			}
			foreach (AccountLicense license in instance.Licenses)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, license.GetSerializedSize());
				AccountLicense.Serialize(stream, license);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Licenses.Count > 0)
			{
				foreach (AccountLicense license in Licenses)
				{
					num++;
					uint serializedSize = license.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
