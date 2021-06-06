using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class AttributionItemTransaction : IProtoBuf
	{
		public bool HasApplicationId;

		private string _ApplicationId;

		public bool HasDeviceType;

		private string _DeviceType;

		public bool HasFirstInstallDate;

		private ulong _FirstInstallDate;

		public bool HasBundleId;

		private string _BundleId;

		public bool HasItemId;

		private string _ItemId;

		public bool HasQuantity;

		private int _Quantity;

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
			if (HasItemId)
			{
				num ^= ItemId.GetHashCode();
			}
			if (HasQuantity)
			{
				num ^= Quantity.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AttributionItemTransaction attributionItemTransaction = obj as AttributionItemTransaction;
			if (attributionItemTransaction == null)
			{
				return false;
			}
			if (HasApplicationId != attributionItemTransaction.HasApplicationId || (HasApplicationId && !ApplicationId.Equals(attributionItemTransaction.ApplicationId)))
			{
				return false;
			}
			if (HasDeviceType != attributionItemTransaction.HasDeviceType || (HasDeviceType && !DeviceType.Equals(attributionItemTransaction.DeviceType)))
			{
				return false;
			}
			if (HasFirstInstallDate != attributionItemTransaction.HasFirstInstallDate || (HasFirstInstallDate && !FirstInstallDate.Equals(attributionItemTransaction.FirstInstallDate)))
			{
				return false;
			}
			if (HasBundleId != attributionItemTransaction.HasBundleId || (HasBundleId && !BundleId.Equals(attributionItemTransaction.BundleId)))
			{
				return false;
			}
			if (HasItemId != attributionItemTransaction.HasItemId || (HasItemId && !ItemId.Equals(attributionItemTransaction.ItemId)))
			{
				return false;
			}
			if (HasQuantity != attributionItemTransaction.HasQuantity || (HasQuantity && !Quantity.Equals(attributionItemTransaction.Quantity)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AttributionItemTransaction Deserialize(Stream stream, AttributionItemTransaction instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AttributionItemTransaction DeserializeLengthDelimited(Stream stream)
		{
			AttributionItemTransaction attributionItemTransaction = new AttributionItemTransaction();
			DeserializeLengthDelimited(stream, attributionItemTransaction);
			return attributionItemTransaction;
		}

		public static AttributionItemTransaction DeserializeLengthDelimited(Stream stream, AttributionItemTransaction instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AttributionItemTransaction Deserialize(Stream stream, AttributionItemTransaction instance, long limit)
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
				case 10:
					instance.ItemId = ProtocolParser.ReadString(stream);
					continue;
				case 16:
					instance.Quantity = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, AttributionItemTransaction instance)
		{
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
			if (instance.HasItemId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ItemId));
			}
			if (instance.HasQuantity)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Quantity);
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
			if (HasItemId)
			{
				num++;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(ItemId);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (HasQuantity)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Quantity);
			}
			return num;
		}
	}
}
