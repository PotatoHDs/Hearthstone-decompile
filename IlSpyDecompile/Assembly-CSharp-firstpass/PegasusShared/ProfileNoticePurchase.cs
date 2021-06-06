using System.IO;
using System.Text;

namespace PegasusShared
{
	public class ProfileNoticePurchase : IProtoBuf
	{
		public enum NoticeID
		{
			ID = 10
		}

		public bool HasPmtProductId;

		private long _PmtProductId;

		public bool HasData;

		private long _Data;

		public bool HasCurrencyDeprecated;

		private int _CurrencyDeprecated;

		public bool HasCurrencyCode;

		private string _CurrencyCode;

		public long PmtProductId
		{
			get
			{
				return _PmtProductId;
			}
			set
			{
				_PmtProductId = value;
				HasPmtProductId = true;
			}
		}

		public long Data
		{
			get
			{
				return _Data;
			}
			set
			{
				_Data = value;
				HasData = true;
			}
		}

		public int CurrencyDeprecated
		{
			get
			{
				return _CurrencyDeprecated;
			}
			set
			{
				_CurrencyDeprecated = value;
				HasCurrencyDeprecated = true;
			}
		}

		public string CurrencyCode
		{
			get
			{
				return _CurrencyCode;
			}
			set
			{
				_CurrencyCode = value;
				HasCurrencyCode = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasPmtProductId)
			{
				num ^= PmtProductId.GetHashCode();
			}
			if (HasData)
			{
				num ^= Data.GetHashCode();
			}
			if (HasCurrencyDeprecated)
			{
				num ^= CurrencyDeprecated.GetHashCode();
			}
			if (HasCurrencyCode)
			{
				num ^= CurrencyCode.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ProfileNoticePurchase profileNoticePurchase = obj as ProfileNoticePurchase;
			if (profileNoticePurchase == null)
			{
				return false;
			}
			if (HasPmtProductId != profileNoticePurchase.HasPmtProductId || (HasPmtProductId && !PmtProductId.Equals(profileNoticePurchase.PmtProductId)))
			{
				return false;
			}
			if (HasData != profileNoticePurchase.HasData || (HasData && !Data.Equals(profileNoticePurchase.Data)))
			{
				return false;
			}
			if (HasCurrencyDeprecated != profileNoticePurchase.HasCurrencyDeprecated || (HasCurrencyDeprecated && !CurrencyDeprecated.Equals(profileNoticePurchase.CurrencyDeprecated)))
			{
				return false;
			}
			if (HasCurrencyCode != profileNoticePurchase.HasCurrencyCode || (HasCurrencyCode && !CurrencyCode.Equals(profileNoticePurchase.CurrencyCode)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileNoticePurchase Deserialize(Stream stream, ProfileNoticePurchase instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileNoticePurchase DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticePurchase profileNoticePurchase = new ProfileNoticePurchase();
			DeserializeLengthDelimited(stream, profileNoticePurchase);
			return profileNoticePurchase;
		}

		public static ProfileNoticePurchase DeserializeLengthDelimited(Stream stream, ProfileNoticePurchase instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileNoticePurchase Deserialize(Stream stream, ProfileNoticePurchase instance, long limit)
		{
			instance.CurrencyCode = "";
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
				case 8:
					instance.PmtProductId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Data = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.CurrencyDeprecated = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					instance.CurrencyCode = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, ProfileNoticePurchase instance)
		{
			if (instance.HasPmtProductId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PmtProductId);
			}
			if (instance.HasData)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Data);
			}
			if (instance.HasCurrencyDeprecated)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CurrencyDeprecated);
			}
			if (instance.HasCurrencyCode)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CurrencyCode));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasPmtProductId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)PmtProductId);
			}
			if (HasData)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Data);
			}
			if (HasCurrencyDeprecated)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CurrencyDeprecated);
			}
			if (HasCurrencyCode)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(CurrencyCode);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
