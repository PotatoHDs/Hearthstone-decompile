using System.IO;
using System.Text;

namespace Blizzard.Telemetry.CRM
{
	public class RealMoneyTransaction : IProtoBuf
	{
		public bool HasApplicationId;

		private string _ApplicationId;

		public bool HasAppStore;

		private string _AppStore;

		public bool HasReceipt;

		private string _Receipt;

		public bool HasReceiptSignature;

		private string _ReceiptSignature;

		public bool HasProductId;

		private string _ProductId;

		public bool HasItemCost;

		private string _ItemCost;

		public bool HasItemQuantity;

		private string _ItemQuantity;

		public bool HasLocalCurrency;

		private string _LocalCurrency;

		public bool HasTransactionId;

		private string _TransactionId;

		public string ApplicationId
		{
			get
			{
				return _ApplicationId;
			}
			set
			{
				_ApplicationId = value;
				HasApplicationId = value != null;
			}
		}

		public string AppStore
		{
			get
			{
				return _AppStore;
			}
			set
			{
				_AppStore = value;
				HasAppStore = value != null;
			}
		}

		public string Receipt
		{
			get
			{
				return _Receipt;
			}
			set
			{
				_Receipt = value;
				HasReceipt = value != null;
			}
		}

		public string ReceiptSignature
		{
			get
			{
				return _ReceiptSignature;
			}
			set
			{
				_ReceiptSignature = value;
				HasReceiptSignature = value != null;
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

		public string ItemCost
		{
			get
			{
				return _ItemCost;
			}
			set
			{
				_ItemCost = value;
				HasItemCost = value != null;
			}
		}

		public string ItemQuantity
		{
			get
			{
				return _ItemQuantity;
			}
			set
			{
				_ItemQuantity = value;
				HasItemQuantity = value != null;
			}
		}

		public string LocalCurrency
		{
			get
			{
				return _LocalCurrency;
			}
			set
			{
				_LocalCurrency = value;
				HasLocalCurrency = value != null;
			}
		}

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

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasApplicationId)
			{
				num ^= ApplicationId.GetHashCode();
			}
			if (HasAppStore)
			{
				num ^= AppStore.GetHashCode();
			}
			if (HasReceipt)
			{
				num ^= Receipt.GetHashCode();
			}
			if (HasReceiptSignature)
			{
				num ^= ReceiptSignature.GetHashCode();
			}
			if (HasProductId)
			{
				num ^= ProductId.GetHashCode();
			}
			if (HasItemCost)
			{
				num ^= ItemCost.GetHashCode();
			}
			if (HasItemQuantity)
			{
				num ^= ItemQuantity.GetHashCode();
			}
			if (HasLocalCurrency)
			{
				num ^= LocalCurrency.GetHashCode();
			}
			if (HasTransactionId)
			{
				num ^= TransactionId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RealMoneyTransaction realMoneyTransaction = obj as RealMoneyTransaction;
			if (realMoneyTransaction == null)
			{
				return false;
			}
			if (HasApplicationId != realMoneyTransaction.HasApplicationId || (HasApplicationId && !ApplicationId.Equals(realMoneyTransaction.ApplicationId)))
			{
				return false;
			}
			if (HasAppStore != realMoneyTransaction.HasAppStore || (HasAppStore && !AppStore.Equals(realMoneyTransaction.AppStore)))
			{
				return false;
			}
			if (HasReceipt != realMoneyTransaction.HasReceipt || (HasReceipt && !Receipt.Equals(realMoneyTransaction.Receipt)))
			{
				return false;
			}
			if (HasReceiptSignature != realMoneyTransaction.HasReceiptSignature || (HasReceiptSignature && !ReceiptSignature.Equals(realMoneyTransaction.ReceiptSignature)))
			{
				return false;
			}
			if (HasProductId != realMoneyTransaction.HasProductId || (HasProductId && !ProductId.Equals(realMoneyTransaction.ProductId)))
			{
				return false;
			}
			if (HasItemCost != realMoneyTransaction.HasItemCost || (HasItemCost && !ItemCost.Equals(realMoneyTransaction.ItemCost)))
			{
				return false;
			}
			if (HasItemQuantity != realMoneyTransaction.HasItemQuantity || (HasItemQuantity && !ItemQuantity.Equals(realMoneyTransaction.ItemQuantity)))
			{
				return false;
			}
			if (HasLocalCurrency != realMoneyTransaction.HasLocalCurrency || (HasLocalCurrency && !LocalCurrency.Equals(realMoneyTransaction.LocalCurrency)))
			{
				return false;
			}
			if (HasTransactionId != realMoneyTransaction.HasTransactionId || (HasTransactionId && !TransactionId.Equals(realMoneyTransaction.TransactionId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RealMoneyTransaction Deserialize(Stream stream, RealMoneyTransaction instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RealMoneyTransaction DeserializeLengthDelimited(Stream stream)
		{
			RealMoneyTransaction realMoneyTransaction = new RealMoneyTransaction();
			DeserializeLengthDelimited(stream, realMoneyTransaction);
			return realMoneyTransaction;
		}

		public static RealMoneyTransaction DeserializeLengthDelimited(Stream stream, RealMoneyTransaction instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RealMoneyTransaction Deserialize(Stream stream, RealMoneyTransaction instance, long limit)
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
				case 82:
					instance.ApplicationId = ProtocolParser.ReadString(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 20u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.AppStore = ProtocolParser.ReadString(stream);
						}
						break;
					case 30u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.Receipt = ProtocolParser.ReadString(stream);
						}
						break;
					case 40u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.ReceiptSignature = ProtocolParser.ReadString(stream);
						}
						break;
					case 50u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.ProductId = ProtocolParser.ReadString(stream);
						}
						break;
					case 60u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.ItemCost = ProtocolParser.ReadString(stream);
						}
						break;
					case 70u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.ItemQuantity = ProtocolParser.ReadString(stream);
						}
						break;
					case 80u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.LocalCurrency = ProtocolParser.ReadString(stream);
						}
						break;
					case 900u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.TransactionId = ProtocolParser.ReadString(stream);
						}
						break;
					default:
						ProtocolParser.SkipKey(stream, key);
						break;
					}
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

		public static void Serialize(Stream stream, RealMoneyTransaction instance)
		{
			if (instance.HasApplicationId)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ApplicationId));
			}
			if (instance.HasAppStore)
			{
				stream.WriteByte(162);
				stream.WriteByte(1);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AppStore));
			}
			if (instance.HasReceipt)
			{
				stream.WriteByte(242);
				stream.WriteByte(1);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Receipt));
			}
			if (instance.HasReceiptSignature)
			{
				stream.WriteByte(194);
				stream.WriteByte(2);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ReceiptSignature));
			}
			if (instance.HasProductId)
			{
				stream.WriteByte(146);
				stream.WriteByte(3);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ProductId));
			}
			if (instance.HasItemCost)
			{
				stream.WriteByte(226);
				stream.WriteByte(3);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ItemCost));
			}
			if (instance.HasItemQuantity)
			{
				stream.WriteByte(178);
				stream.WriteByte(4);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ItemQuantity));
			}
			if (instance.HasLocalCurrency)
			{
				stream.WriteByte(130);
				stream.WriteByte(5);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.LocalCurrency));
			}
			if (instance.HasTransactionId)
			{
				stream.WriteByte(162);
				stream.WriteByte(56);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TransactionId));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasApplicationId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ApplicationId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasAppStore)
			{
				num += 2;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(AppStore);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasReceipt)
			{
				num += 2;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(Receipt);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasReceiptSignature)
			{
				num += 2;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(ReceiptSignature);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (HasProductId)
			{
				num += 2;
				uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(ProductId);
				num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			}
			if (HasItemCost)
			{
				num += 2;
				uint byteCount6 = (uint)Encoding.UTF8.GetByteCount(ItemCost);
				num += ProtocolParser.SizeOfUInt32(byteCount6) + byteCount6;
			}
			if (HasItemQuantity)
			{
				num += 2;
				uint byteCount7 = (uint)Encoding.UTF8.GetByteCount(ItemQuantity);
				num += ProtocolParser.SizeOfUInt32(byteCount7) + byteCount7;
			}
			if (HasLocalCurrency)
			{
				num += 2;
				uint byteCount8 = (uint)Encoding.UTF8.GetByteCount(LocalCurrency);
				num += ProtocolParser.SizeOfUInt32(byteCount8) + byteCount8;
			}
			if (HasTransactionId)
			{
				num += 2;
				uint byteCount9 = (uint)Encoding.UTF8.GetByteCount(TransactionId);
				num += ProtocolParser.SizeOfUInt32(byteCount9) + byteCount9;
			}
			return num;
		}
	}
}
