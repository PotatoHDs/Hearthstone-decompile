using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class AttributionPurchase : IProtoBuf
	{
		public class PaymentInfo : IProtoBuf
		{
			public bool HasCurrencyCode;

			private string _CurrencyCode;

			public bool HasIsVirtualCurrency;

			private bool _IsVirtualCurrency;

			public bool HasAmount;

			private float _Amount;

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

			public bool IsVirtualCurrency
			{
				get
				{
					return _IsVirtualCurrency;
				}
				set
				{
					_IsVirtualCurrency = value;
					HasIsVirtualCurrency = true;
				}
			}

			public float Amount
			{
				get
				{
					return _Amount;
				}
				set
				{
					_Amount = value;
					HasAmount = true;
				}
			}

			public override int GetHashCode()
			{
				int num = GetType().GetHashCode();
				if (HasCurrencyCode)
				{
					num ^= CurrencyCode.GetHashCode();
				}
				if (HasIsVirtualCurrency)
				{
					num ^= IsVirtualCurrency.GetHashCode();
				}
				if (HasAmount)
				{
					num ^= Amount.GetHashCode();
				}
				return num;
			}

			public override bool Equals(object obj)
			{
				PaymentInfo paymentInfo = obj as PaymentInfo;
				if (paymentInfo == null)
				{
					return false;
				}
				if (HasCurrencyCode != paymentInfo.HasCurrencyCode || (HasCurrencyCode && !CurrencyCode.Equals(paymentInfo.CurrencyCode)))
				{
					return false;
				}
				if (HasIsVirtualCurrency != paymentInfo.HasIsVirtualCurrency || (HasIsVirtualCurrency && !IsVirtualCurrency.Equals(paymentInfo.IsVirtualCurrency)))
				{
					return false;
				}
				if (HasAmount != paymentInfo.HasAmount || (HasAmount && !Amount.Equals(paymentInfo.Amount)))
				{
					return false;
				}
				return true;
			}

			public void Deserialize(Stream stream)
			{
				Deserialize(stream, this);
			}

			public static PaymentInfo Deserialize(Stream stream, PaymentInfo instance)
			{
				return Deserialize(stream, instance, -1L);
			}

			public static PaymentInfo DeserializeLengthDelimited(Stream stream)
			{
				PaymentInfo paymentInfo = new PaymentInfo();
				DeserializeLengthDelimited(stream, paymentInfo);
				return paymentInfo;
			}

			public static PaymentInfo DeserializeLengthDelimited(Stream stream, PaymentInfo instance)
			{
				long num = ProtocolParser.ReadUInt32(stream);
				num += stream.Position;
				return Deserialize(stream, instance, num);
			}

			public static PaymentInfo Deserialize(Stream stream, PaymentInfo instance, long limit)
			{
				BinaryReader binaryReader = new BinaryReader(stream);
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
						instance.CurrencyCode = ProtocolParser.ReadString(stream);
						continue;
					default:
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						switch (key.Field)
						{
						case 0u:
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						case 20u:
							if (key.WireType == Wire.Varint)
							{
								instance.IsVirtualCurrency = ProtocolParser.ReadBool(stream);
							}
							break;
						case 30u:
							if (key.WireType == Wire.Fixed32)
							{
								instance.Amount = binaryReader.ReadSingle();
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

			public static void Serialize(Stream stream, PaymentInfo instance)
			{
				BinaryWriter binaryWriter = new BinaryWriter(stream);
				if (instance.HasCurrencyCode)
				{
					stream.WriteByte(82);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CurrencyCode));
				}
				if (instance.HasIsVirtualCurrency)
				{
					stream.WriteByte(160);
					stream.WriteByte(1);
					ProtocolParser.WriteBool(stream, instance.IsVirtualCurrency);
				}
				if (instance.HasAmount)
				{
					stream.WriteByte(245);
					stream.WriteByte(1);
					binaryWriter.Write(instance.Amount);
				}
			}

			public uint GetSerializedSize()
			{
				uint num = 0u;
				if (HasCurrencyCode)
				{
					num++;
					uint byteCount = (uint)Encoding.UTF8.GetByteCount(CurrencyCode);
					num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
				}
				if (HasIsVirtualCurrency)
				{
					num += 2;
					num++;
				}
				if (HasAmount)
				{
					num += 2;
					num += 4;
				}
				return num;
			}
		}

		public bool HasApplicationId;

		private string _ApplicationId;

		public bool HasDeviceType;

		private string _DeviceType;

		public bool HasFirstInstallDate;

		private ulong _FirstInstallDate;

		public bool HasBundleId;

		private string _BundleId;

		public bool HasPurchaseType;

		private string _PurchaseType;

		public bool HasTransactionId;

		private string _TransactionId;

		public bool HasQuantity;

		private int _Quantity;

		private List<PaymentInfo> _Payments = new List<PaymentInfo>();

		public bool HasAmount;

		private float _Amount;

		public bool HasCurrency;

		private string _Currency;

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

		public string DeviceType
		{
			get
			{
				return _DeviceType;
			}
			set
			{
				_DeviceType = value;
				HasDeviceType = value != null;
			}
		}

		public ulong FirstInstallDate
		{
			get
			{
				return _FirstInstallDate;
			}
			set
			{
				_FirstInstallDate = value;
				HasFirstInstallDate = true;
			}
		}

		public string BundleId
		{
			get
			{
				return _BundleId;
			}
			set
			{
				_BundleId = value;
				HasBundleId = value != null;
			}
		}

		public string PurchaseType
		{
			get
			{
				return _PurchaseType;
			}
			set
			{
				_PurchaseType = value;
				HasPurchaseType = value != null;
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

		public int Quantity
		{
			get
			{
				return _Quantity;
			}
			set
			{
				_Quantity = value;
				HasQuantity = true;
			}
		}

		public List<PaymentInfo> Payments
		{
			get
			{
				return _Payments;
			}
			set
			{
				_Payments = value;
			}
		}

		public float Amount
		{
			get
			{
				return _Amount;
			}
			set
			{
				_Amount = value;
				HasAmount = true;
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

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasApplicationId)
			{
				num ^= ApplicationId.GetHashCode();
			}
			if (HasDeviceType)
			{
				num ^= DeviceType.GetHashCode();
			}
			if (HasFirstInstallDate)
			{
				num ^= FirstInstallDate.GetHashCode();
			}
			if (HasBundleId)
			{
				num ^= BundleId.GetHashCode();
			}
			if (HasPurchaseType)
			{
				num ^= PurchaseType.GetHashCode();
			}
			if (HasTransactionId)
			{
				num ^= TransactionId.GetHashCode();
			}
			if (HasQuantity)
			{
				num ^= Quantity.GetHashCode();
			}
			foreach (PaymentInfo payment in Payments)
			{
				num ^= payment.GetHashCode();
			}
			if (HasAmount)
			{
				num ^= Amount.GetHashCode();
			}
			if (HasCurrency)
			{
				num ^= Currency.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AttributionPurchase attributionPurchase = obj as AttributionPurchase;
			if (attributionPurchase == null)
			{
				return false;
			}
			if (HasApplicationId != attributionPurchase.HasApplicationId || (HasApplicationId && !ApplicationId.Equals(attributionPurchase.ApplicationId)))
			{
				return false;
			}
			if (HasDeviceType != attributionPurchase.HasDeviceType || (HasDeviceType && !DeviceType.Equals(attributionPurchase.DeviceType)))
			{
				return false;
			}
			if (HasFirstInstallDate != attributionPurchase.HasFirstInstallDate || (HasFirstInstallDate && !FirstInstallDate.Equals(attributionPurchase.FirstInstallDate)))
			{
				return false;
			}
			if (HasBundleId != attributionPurchase.HasBundleId || (HasBundleId && !BundleId.Equals(attributionPurchase.BundleId)))
			{
				return false;
			}
			if (HasPurchaseType != attributionPurchase.HasPurchaseType || (HasPurchaseType && !PurchaseType.Equals(attributionPurchase.PurchaseType)))
			{
				return false;
			}
			if (HasTransactionId != attributionPurchase.HasTransactionId || (HasTransactionId && !TransactionId.Equals(attributionPurchase.TransactionId)))
			{
				return false;
			}
			if (HasQuantity != attributionPurchase.HasQuantity || (HasQuantity && !Quantity.Equals(attributionPurchase.Quantity)))
			{
				return false;
			}
			if (Payments.Count != attributionPurchase.Payments.Count)
			{
				return false;
			}
			for (int i = 0; i < Payments.Count; i++)
			{
				if (!Payments[i].Equals(attributionPurchase.Payments[i]))
				{
					return false;
				}
			}
			if (HasAmount != attributionPurchase.HasAmount || (HasAmount && !Amount.Equals(attributionPurchase.Amount)))
			{
				return false;
			}
			if (HasCurrency != attributionPurchase.HasCurrency || (HasCurrency && !Currency.Equals(attributionPurchase.Currency)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AttributionPurchase Deserialize(Stream stream, AttributionPurchase instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AttributionPurchase DeserializeLengthDelimited(Stream stream)
		{
			AttributionPurchase attributionPurchase = new AttributionPurchase();
			DeserializeLengthDelimited(stream, attributionPurchase);
			return attributionPurchase;
		}

		public static AttributionPurchase DeserializeLengthDelimited(Stream stream, AttributionPurchase instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AttributionPurchase Deserialize(Stream stream, AttributionPurchase instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Payments == null)
			{
				instance.Payments = new List<PaymentInfo>();
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
					instance.PurchaseType = ProtocolParser.ReadString(stream);
					continue;
				case 34:
					instance.TransactionId = ProtocolParser.ReadString(stream);
					continue;
				case 40:
					instance.Quantity = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 50:
					instance.Payments.Add(PaymentInfo.DeserializeLengthDelimited(stream));
					continue;
				case 21:
					instance.Amount = binaryReader.ReadSingle();
					continue;
				case 26:
					instance.Currency = ProtocolParser.ReadString(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 100u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.ApplicationId = ProtocolParser.ReadString(stream);
						}
						break;
					case 101u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.DeviceType = ProtocolParser.ReadString(stream);
						}
						break;
					case 102u:
						if (key.WireType == Wire.Varint)
						{
							instance.FirstInstallDate = ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 103u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.BundleId = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, AttributionPurchase instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasApplicationId)
			{
				stream.WriteByte(162);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ApplicationId));
			}
			if (instance.HasDeviceType)
			{
				stream.WriteByte(170);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeviceType));
			}
			if (instance.HasFirstInstallDate)
			{
				stream.WriteByte(176);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt64(stream, instance.FirstInstallDate);
			}
			if (instance.HasBundleId)
			{
				stream.WriteByte(186);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BundleId));
			}
			if (instance.HasPurchaseType)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.PurchaseType));
			}
			if (instance.HasTransactionId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TransactionId));
			}
			if (instance.HasQuantity)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Quantity);
			}
			if (instance.Payments.Count > 0)
			{
				foreach (PaymentInfo payment in instance.Payments)
				{
					stream.WriteByte(50);
					ProtocolParser.WriteUInt32(stream, payment.GetSerializedSize());
					PaymentInfo.Serialize(stream, payment);
				}
			}
			if (instance.HasAmount)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Amount);
			}
			if (instance.HasCurrency)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Currency));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasApplicationId)
			{
				num += 2;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ApplicationId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasDeviceType)
			{
				num += 2;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(DeviceType);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasFirstInstallDate)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64(FirstInstallDate);
			}
			if (HasBundleId)
			{
				num += 2;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(BundleId);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasPurchaseType)
			{
				num++;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(PurchaseType);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (HasTransactionId)
			{
				num++;
				uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(TransactionId);
				num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			}
			if (HasQuantity)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Quantity);
			}
			if (Payments.Count > 0)
			{
				foreach (PaymentInfo payment in Payments)
				{
					num++;
					uint serializedSize = payment.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasAmount)
			{
				num++;
				num += 4;
			}
			if (HasCurrency)
			{
				num++;
				uint byteCount6 = (uint)Encoding.UTF8.GetByteCount(Currency);
				num += ProtocolParser.SizeOfUInt32(byteCount6) + byteCount6;
			}
			return num;
		}
	}
}
