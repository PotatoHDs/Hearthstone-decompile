using System;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusUtil
{
	public class SubmitThirdPartyReceipt : IProtoBuf
	{
		public enum PacketID
		{
			ID = 293,
			System = 1
		}

		public BattlePayProvider Provider { get; set; }

		public string ProductId { get; set; }

		public int Quantity { get; set; }

		public long TransactionId { get; set; }

		public ThirdPartyReceiptData ReceiptData { get; set; }

		public string DeviceId { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Provider.GetHashCode() ^ ProductId.GetHashCode() ^ Quantity.GetHashCode() ^ TransactionId.GetHashCode() ^ ReceiptData.GetHashCode() ^ DeviceId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			SubmitThirdPartyReceipt submitThirdPartyReceipt = obj as SubmitThirdPartyReceipt;
			if (submitThirdPartyReceipt == null)
			{
				return false;
			}
			if (!Provider.Equals(submitThirdPartyReceipt.Provider))
			{
				return false;
			}
			if (!ProductId.Equals(submitThirdPartyReceipt.ProductId))
			{
				return false;
			}
			if (!Quantity.Equals(submitThirdPartyReceipt.Quantity))
			{
				return false;
			}
			if (!TransactionId.Equals(submitThirdPartyReceipt.TransactionId))
			{
				return false;
			}
			if (!ReceiptData.Equals(submitThirdPartyReceipt.ReceiptData))
			{
				return false;
			}
			if (!DeviceId.Equals(submitThirdPartyReceipt.DeviceId))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SubmitThirdPartyReceipt Deserialize(Stream stream, SubmitThirdPartyReceipt instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SubmitThirdPartyReceipt DeserializeLengthDelimited(Stream stream)
		{
			SubmitThirdPartyReceipt submitThirdPartyReceipt = new SubmitThirdPartyReceipt();
			DeserializeLengthDelimited(stream, submitThirdPartyReceipt);
			return submitThirdPartyReceipt;
		}

		public static SubmitThirdPartyReceipt DeserializeLengthDelimited(Stream stream, SubmitThirdPartyReceipt instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SubmitThirdPartyReceipt Deserialize(Stream stream, SubmitThirdPartyReceipt instance, long limit)
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
				case 32:
					instance.TransactionId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 42:
					if (instance.ReceiptData == null)
					{
						instance.ReceiptData = ThirdPartyReceiptData.DeserializeLengthDelimited(stream);
					}
					else
					{
						ThirdPartyReceiptData.DeserializeLengthDelimited(stream, instance.ReceiptData);
					}
					continue;
				case 50:
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

		public static void Serialize(Stream stream, SubmitThirdPartyReceipt instance)
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
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.TransactionId);
			if (instance.ReceiptData == null)
			{
				throw new ArgumentNullException("ReceiptData", "Required by proto specification.");
			}
			stream.WriteByte(42);
			ProtocolParser.WriteUInt32(stream, instance.ReceiptData.GetSerializedSize());
			ThirdPartyReceiptData.Serialize(stream, instance.ReceiptData);
			if (instance.DeviceId == null)
			{
				throw new ArgumentNullException("DeviceId", "Required by proto specification.");
			}
			stream.WriteByte(50);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeviceId));
		}

		public uint GetSerializedSize()
		{
			uint num = 0 + ProtocolParser.SizeOfUInt64((ulong)Provider);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(ProductId);
			uint num2 = num + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + ProtocolParser.SizeOfUInt64((ulong)Quantity) + ProtocolParser.SizeOfUInt64((ulong)TransactionId);
			uint serializedSize = ReceiptData.GetSerializedSize();
			uint num3 = num2 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize));
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(DeviceId);
			return num3 + (ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2) + 6;
		}
	}
}
