using System;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusUtil
{
	public class StartThirdPartyPurchase : IProtoBuf
	{
		public enum PacketID
		{
			ID = 312,
			System = 1
		}

		public bool HasDanglingReceiptData;

		private ThirdPartyReceiptData _DanglingReceiptData;

		public BattlePayProvider Provider { get; set; }

		public string ProductId { get; set; }

		public int Quantity { get; set; }

		public ThirdPartyReceiptData DanglingReceiptData
		{
			get
			{
				return _DanglingReceiptData;
			}
			set
			{
				_DanglingReceiptData = value;
				HasDanglingReceiptData = value != null;
			}
		}

		public string DeviceId { get; set; }

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Provider.GetHashCode();
			hashCode ^= ProductId.GetHashCode();
			hashCode ^= Quantity.GetHashCode();
			if (HasDanglingReceiptData)
			{
				hashCode ^= DanglingReceiptData.GetHashCode();
			}
			return hashCode ^ DeviceId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			StartThirdPartyPurchase startThirdPartyPurchase = obj as StartThirdPartyPurchase;
			if (startThirdPartyPurchase == null)
			{
				return false;
			}
			if (!Provider.Equals(startThirdPartyPurchase.Provider))
			{
				return false;
			}
			if (!ProductId.Equals(startThirdPartyPurchase.ProductId))
			{
				return false;
			}
			if (!Quantity.Equals(startThirdPartyPurchase.Quantity))
			{
				return false;
			}
			if (HasDanglingReceiptData != startThirdPartyPurchase.HasDanglingReceiptData || (HasDanglingReceiptData && !DanglingReceiptData.Equals(startThirdPartyPurchase.DanglingReceiptData)))
			{
				return false;
			}
			if (!DeviceId.Equals(startThirdPartyPurchase.DeviceId))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static StartThirdPartyPurchase Deserialize(Stream stream, StartThirdPartyPurchase instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static StartThirdPartyPurchase DeserializeLengthDelimited(Stream stream)
		{
			StartThirdPartyPurchase startThirdPartyPurchase = new StartThirdPartyPurchase();
			DeserializeLengthDelimited(stream, startThirdPartyPurchase);
			return startThirdPartyPurchase;
		}

		public static StartThirdPartyPurchase DeserializeLengthDelimited(Stream stream, StartThirdPartyPurchase instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static StartThirdPartyPurchase Deserialize(Stream stream, StartThirdPartyPurchase instance, long limit)
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
					instance.Provider = (BattlePayProvider)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					instance.ProductId = ProtocolParser.ReadString(stream);
					continue;
				case 24:
					instance.Quantity = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					if (instance.DanglingReceiptData == null)
					{
						instance.DanglingReceiptData = ThirdPartyReceiptData.DeserializeLengthDelimited(stream);
					}
					else
					{
						ThirdPartyReceiptData.DeserializeLengthDelimited(stream, instance.DanglingReceiptData);
					}
					continue;
				case 42:
					instance.DeviceId = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, StartThirdPartyPurchase instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Provider);
			if (instance.ProductId == null)
			{
				throw new ArgumentNullException("ProductId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ProductId));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Quantity);
			if (instance.HasDanglingReceiptData)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.DanglingReceiptData.GetSerializedSize());
				ThirdPartyReceiptData.Serialize(stream, instance.DanglingReceiptData);
			}
			if (instance.DeviceId == null)
			{
				throw new ArgumentNullException("DeviceId", "Required by proto specification.");
			}
			stream.WriteByte(42);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeviceId));
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Provider);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(ProductId);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			num += ProtocolParser.SizeOfUInt64((ulong)Quantity);
			if (HasDanglingReceiptData)
			{
				num++;
				uint serializedSize = DanglingReceiptData.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(DeviceId);
			num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			return num + 4;
		}
	}
}
