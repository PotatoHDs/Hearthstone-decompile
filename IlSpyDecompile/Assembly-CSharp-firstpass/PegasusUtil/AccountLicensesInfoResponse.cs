using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class AccountLicensesInfoResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 325
		}

		private List<AccountLicenseInfo> _List = new List<AccountLicenseInfo>();

		public List<AccountLicenseInfo> List
		{
			get
			{
				return _List;
			}
			set
			{
				_List = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (AccountLicenseInfo item in List)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AccountLicensesInfoResponse accountLicensesInfoResponse = obj as AccountLicensesInfoResponse;
			if (accountLicensesInfoResponse == null)
			{
				return false;
			}
			if (List.Count != accountLicensesInfoResponse.List.Count)
			{
				return false;
			}
			for (int i = 0; i < List.Count; i++)
			{
				if (!List[i].Equals(accountLicensesInfoResponse.List[i]))
				{
					return false;
				}
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AccountLicensesInfoResponse Deserialize(Stream stream, AccountLicensesInfoResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AccountLicensesInfoResponse DeserializeLengthDelimited(Stream stream)
		{
			AccountLicensesInfoResponse accountLicensesInfoResponse = new AccountLicensesInfoResponse();
			DeserializeLengthDelimited(stream, accountLicensesInfoResponse);
			return accountLicensesInfoResponse;
		}

		public static AccountLicensesInfoResponse DeserializeLengthDelimited(Stream stream, AccountLicensesInfoResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AccountLicensesInfoResponse Deserialize(Stream stream, AccountLicensesInfoResponse instance, long limit)
		{
			if (instance.List == null)
			{
				instance.List = new List<AccountLicenseInfo>();
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
					instance.List.Add(AccountLicenseInfo.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, AccountLicensesInfoResponse instance)
		{
			if (instance.List.Count <= 0)
			{
				return;
			}
			foreach (AccountLicenseInfo item in instance.List)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				AccountLicenseInfo.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (List.Count > 0)
			{
				foreach (AccountLicenseInfo item in List)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
