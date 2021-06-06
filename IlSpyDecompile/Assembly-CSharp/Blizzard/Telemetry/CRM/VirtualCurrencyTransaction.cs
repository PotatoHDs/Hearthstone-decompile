using System.IO;
using System.Text;

namespace Blizzard.Telemetry.CRM
{
	public class VirtualCurrencyTransaction : IProtoBuf
	{
		public bool HasApplicationId;

		private string _ApplicationId;

		public bool HasItemId;

		private string _ItemId;

		public bool HasItemCost;

		private string _ItemCost;

		public bool HasItemQuantity;

		private string _ItemQuantity;

		public bool HasCurrency;

		private string _Currency;

		public bool HasPayload;

		private string _Payload;

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

		public string ItemId
		{
			get
			{
				return _ItemId;
			}
			set
			{
				_ItemId = value;
				HasItemId = value != null;
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

		public string Payload
		{
			get
			{
				return _Payload;
			}
			set
			{
				_Payload = value;
				HasPayload = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasApplicationId)
			{
				num ^= ApplicationId.GetHashCode();
			}
			if (HasItemId)
			{
				num ^= ItemId.GetHashCode();
			}
			if (HasItemCost)
			{
				num ^= ItemCost.GetHashCode();
			}
			if (HasItemQuantity)
			{
				num ^= ItemQuantity.GetHashCode();
			}
			if (HasCurrency)
			{
				num ^= Currency.GetHashCode();
			}
			if (HasPayload)
			{
				num ^= Payload.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			VirtualCurrencyTransaction virtualCurrencyTransaction = obj as VirtualCurrencyTransaction;
			if (virtualCurrencyTransaction == null)
			{
				return false;
			}
			if (HasApplicationId != virtualCurrencyTransaction.HasApplicationId || (HasApplicationId && !ApplicationId.Equals(virtualCurrencyTransaction.ApplicationId)))
			{
				return false;
			}
			if (HasItemId != virtualCurrencyTransaction.HasItemId || (HasItemId && !ItemId.Equals(virtualCurrencyTransaction.ItemId)))
			{
				return false;
			}
			if (HasItemCost != virtualCurrencyTransaction.HasItemCost || (HasItemCost && !ItemCost.Equals(virtualCurrencyTransaction.ItemCost)))
			{
				return false;
			}
			if (HasItemQuantity != virtualCurrencyTransaction.HasItemQuantity || (HasItemQuantity && !ItemQuantity.Equals(virtualCurrencyTransaction.ItemQuantity)))
			{
				return false;
			}
			if (HasCurrency != virtualCurrencyTransaction.HasCurrency || (HasCurrency && !Currency.Equals(virtualCurrencyTransaction.Currency)))
			{
				return false;
			}
			if (HasPayload != virtualCurrencyTransaction.HasPayload || (HasPayload && !Payload.Equals(virtualCurrencyTransaction.Payload)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static VirtualCurrencyTransaction Deserialize(Stream stream, VirtualCurrencyTransaction instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static VirtualCurrencyTransaction DeserializeLengthDelimited(Stream stream)
		{
			VirtualCurrencyTransaction virtualCurrencyTransaction = new VirtualCurrencyTransaction();
			DeserializeLengthDelimited(stream, virtualCurrencyTransaction);
			return virtualCurrencyTransaction;
		}

		public static VirtualCurrencyTransaction DeserializeLengthDelimited(Stream stream, VirtualCurrencyTransaction instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static VirtualCurrencyTransaction Deserialize(Stream stream, VirtualCurrencyTransaction instance, long limit)
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
							instance.ItemId = ProtocolParser.ReadString(stream);
						}
						break;
					case 30u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.ItemCost = ProtocolParser.ReadString(stream);
						}
						break;
					case 40u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.ItemQuantity = ProtocolParser.ReadString(stream);
						}
						break;
					case 50u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.Currency = ProtocolParser.ReadString(stream);
						}
						break;
					case 60u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.Payload = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, VirtualCurrencyTransaction instance)
		{
			if (instance.HasApplicationId)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ApplicationId));
			}
			if (instance.HasItemId)
			{
				stream.WriteByte(162);
				stream.WriteByte(1);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ItemId));
			}
			if (instance.HasItemCost)
			{
				stream.WriteByte(242);
				stream.WriteByte(1);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ItemCost));
			}
			if (instance.HasItemQuantity)
			{
				stream.WriteByte(194);
				stream.WriteByte(2);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ItemQuantity));
			}
			if (instance.HasCurrency)
			{
				stream.WriteByte(146);
				stream.WriteByte(3);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Currency));
			}
			if (instance.HasPayload)
			{
				stream.WriteByte(226);
				stream.WriteByte(3);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Payload));
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
			if (HasItemId)
			{
				num += 2;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(ItemId);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasItemCost)
			{
				num += 2;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(ItemCost);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasItemQuantity)
			{
				num += 2;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(ItemQuantity);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (HasCurrency)
			{
				num += 2;
				uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(Currency);
				num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			}
			if (HasPayload)
			{
				num += 2;
				uint byteCount6 = (uint)Encoding.UTF8.GetByteCount(Payload);
				num += ProtocolParser.SizeOfUInt32(byteCount6) + byteCount6;
			}
			return num;
		}
	}
}
