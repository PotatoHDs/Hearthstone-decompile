using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusUtil
{
	public class ReportBlizzardCheckoutStatus : IProtoBuf
	{
		public enum PacketID
		{
			ID = 366,
			System = 1
		}

		public bool HasTransactionId;

		private string _TransactionId;

		public bool HasProductId;

		private string _ProductId;

		public bool HasCurrency;

		private string _Currency;

		public bool HasClientUnixTime;

		private long _ClientUnixTime;

		public BlizzardCheckoutStatus Status { get; set; }

		public string TransactionId
		{
			get
			{
				return _TransactionId;
			}
			set
			{
				_TransactionId = value;
				HasTransactionId = value != null;
			}
		}

		public string ProductId
		{
			get
			{
				return _ProductId;
			}
			set
			{
				_ProductId = value;
				HasProductId = value != null;
			}
		}

		public string Currency
		{
			get
			{
				return _Currency;
			}
			set
			{
				_Currency = value;
				HasCurrency = value != null;
			}
		}

		public long ClientUnixTime
		{
			get
			{
				return _ClientUnixTime;
			}
			set
			{
				_ClientUnixTime = value;
				HasClientUnixTime = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Status.GetHashCode();
			if (HasTransactionId)
			{
				hashCode ^= TransactionId.GetHashCode();
			}
			if (HasProductId)
			{
				hashCode ^= ProductId.GetHashCode();
			}
			if (HasCurrency)
			{
				hashCode ^= Currency.GetHashCode();
			}
			if (HasClientUnixTime)
			{
				hashCode ^= ClientUnixTime.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ReportBlizzardCheckoutStatus reportBlizzardCheckoutStatus = obj as ReportBlizzardCheckoutStatus;
			if (reportBlizzardCheckoutStatus == null)
			{
				return false;
			}
			if (!Status.Equals(reportBlizzardCheckoutStatus.Status))
			{
				return false;
			}
			if (HasTransactionId != reportBlizzardCheckoutStatus.HasTransactionId || (HasTransactionId && !TransactionId.Equals(reportBlizzardCheckoutStatus.TransactionId)))
			{
				return false;
			}
			if (HasProductId != reportBlizzardCheckoutStatus.HasProductId || (HasProductId && !ProductId.Equals(reportBlizzardCheckoutStatus.ProductId)))
			{
				return false;
			}
			if (HasCurrency != reportBlizzardCheckoutStatus.HasCurrency || (HasCurrency && !Currency.Equals(reportBlizzardCheckoutStatus.Currency)))
			{
				return false;
			}
			if (HasClientUnixTime != reportBlizzardCheckoutStatus.HasClientUnixTime || (HasClientUnixTime && !ClientUnixTime.Equals(reportBlizzardCheckoutStatus.ClientUnixTime)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ReportBlizzardCheckoutStatus Deserialize(Stream stream, ReportBlizzardCheckoutStatus instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ReportBlizzardCheckoutStatus DeserializeLengthDelimited(Stream stream)
		{
			ReportBlizzardCheckoutStatus reportBlizzardCheckoutStatus = new ReportBlizzardCheckoutStatus();
			DeserializeLengthDelimited(stream, reportBlizzardCheckoutStatus);
			return reportBlizzardCheckoutStatus;
		}

		public static ReportBlizzardCheckoutStatus DeserializeLengthDelimited(Stream stream, ReportBlizzardCheckoutStatus instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ReportBlizzardCheckoutStatus Deserialize(Stream stream, ReportBlizzardCheckoutStatus instance, long limit)
		{
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
					instance.Status = (BlizzardCheckoutStatus)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					instance.TransactionId = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.ProductId = ProtocolParser.ReadString(stream);
					continue;
				case 34:
					instance.Currency = ProtocolParser.ReadString(stream);
					continue;
				case 40:
					instance.ClientUnixTime = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ReportBlizzardCheckoutStatus instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Status);
			if (instance.HasTransactionId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TransactionId));
			}
			if (instance.HasProductId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ProductId));
			}
			if (instance.HasCurrency)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Currency));
			}
			if (instance.HasClientUnixTime)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ClientUnixTime);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Status);
			if (HasTransactionId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(TransactionId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasProductId)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(ProductId);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasCurrency)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(Currency);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasClientUnixTime)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ClientUnixTime);
			}
			return num + 1;
		}
	}
}
