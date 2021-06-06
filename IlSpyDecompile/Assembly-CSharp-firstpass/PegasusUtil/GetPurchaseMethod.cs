using System;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusUtil
{
	public class GetPurchaseMethod : IProtoBuf
	{
		public enum PacketID
		{
			ID = 250,
			System = 1
		}

		public bool HasPlatform;

		private Platform _Platform;

		public bool HasCurrencyCode;

		private string _CurrencyCode;

		public long PmtProductId { get; set; }

		public int Quantity { get; set; }

		public int CurrencyDeprecated { get; set; }

		public string DeviceId { get; set; }

		public Platform Platform
		{
			get
			{
				return _Platform;
			}
			set
			{
				_Platform = value;
				HasPlatform = value != null;
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
			int hashCode = GetType().GetHashCode();
			hashCode ^= PmtProductId.GetHashCode();
			hashCode ^= Quantity.GetHashCode();
			hashCode ^= CurrencyDeprecated.GetHashCode();
			hashCode ^= DeviceId.GetHashCode();
			if (HasPlatform)
			{
				hashCode ^= Platform.GetHashCode();
			}
			if (HasCurrencyCode)
			{
				hashCode ^= CurrencyCode.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			GetPurchaseMethod getPurchaseMethod = obj as GetPurchaseMethod;
			if (getPurchaseMethod == null)
			{
				return false;
			}
			if (!PmtProductId.Equals(getPurchaseMethod.PmtProductId))
			{
				return false;
			}
			if (!Quantity.Equals(getPurchaseMethod.Quantity))
			{
				return false;
			}
			if (!CurrencyDeprecated.Equals(getPurchaseMethod.CurrencyDeprecated))
			{
				return false;
			}
			if (!DeviceId.Equals(getPurchaseMethod.DeviceId))
			{
				return false;
			}
			if (HasPlatform != getPurchaseMethod.HasPlatform || (HasPlatform && !Platform.Equals(getPurchaseMethod.Platform)))
			{
				return false;
			}
			if (HasCurrencyCode != getPurchaseMethod.HasCurrencyCode || (HasCurrencyCode && !CurrencyCode.Equals(getPurchaseMethod.CurrencyCode)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetPurchaseMethod Deserialize(Stream stream, GetPurchaseMethod instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetPurchaseMethod DeserializeLengthDelimited(Stream stream)
		{
			GetPurchaseMethod getPurchaseMethod = new GetPurchaseMethod();
			DeserializeLengthDelimited(stream, getPurchaseMethod);
			return getPurchaseMethod;
		}

		public static GetPurchaseMethod DeserializeLengthDelimited(Stream stream, GetPurchaseMethod instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetPurchaseMethod Deserialize(Stream stream, GetPurchaseMethod instance, long limit)
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
					instance.Quantity = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.CurrencyDeprecated = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					instance.DeviceId = ProtocolParser.ReadString(stream);
					continue;
				case 42:
					if (instance.Platform == null)
					{
						instance.Platform = Platform.DeserializeLengthDelimited(stream);
					}
					else
					{
						Platform.DeserializeLengthDelimited(stream, instance.Platform);
					}
					continue;
				case 50:
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

		public static void Serialize(Stream stream, GetPurchaseMethod instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.PmtProductId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Quantity);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.CurrencyDeprecated);
			if (instance.DeviceId == null)
			{
				throw new ArgumentNullException("DeviceId", "Required by proto specification.");
			}
			stream.WriteByte(34);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeviceId));
			if (instance.HasPlatform)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.Platform.GetSerializedSize());
				Platform.Serialize(stream, instance.Platform);
			}
			if (instance.HasCurrencyCode)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CurrencyCode));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)PmtProductId);
			num += ProtocolParser.SizeOfUInt64((ulong)Quantity);
			num += ProtocolParser.SizeOfUInt64((ulong)CurrencyDeprecated);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(DeviceId);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (HasPlatform)
			{
				num++;
				uint serializedSize = Platform.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasCurrencyCode)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(CurrencyCode);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num + 4;
		}
	}
}
