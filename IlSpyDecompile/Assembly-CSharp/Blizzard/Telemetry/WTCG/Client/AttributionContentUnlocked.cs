using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class AttributionContentUnlocked : IProtoBuf
	{
		public bool HasApplicationId;

		private string _ApplicationId;

		public bool HasDeviceType;

		private string _DeviceType;

		public bool HasFirstInstallDate;

		private ulong _FirstInstallDate;

		public bool HasBundleId;

		private string _BundleId;

		public bool HasContentId;

		private string _ContentId;

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

		public string ContentId
		{
			get
			{
				return _ContentId;
			}
			set
			{
				_ContentId = value;
				HasContentId = value != null;
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
			if (HasContentId)
			{
				num ^= ContentId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AttributionContentUnlocked attributionContentUnlocked = obj as AttributionContentUnlocked;
			if (attributionContentUnlocked == null)
			{
				return false;
			}
			if (HasApplicationId != attributionContentUnlocked.HasApplicationId || (HasApplicationId && !ApplicationId.Equals(attributionContentUnlocked.ApplicationId)))
			{
				return false;
			}
			if (HasDeviceType != attributionContentUnlocked.HasDeviceType || (HasDeviceType && !DeviceType.Equals(attributionContentUnlocked.DeviceType)))
			{
				return false;
			}
			if (HasFirstInstallDate != attributionContentUnlocked.HasFirstInstallDate || (HasFirstInstallDate && !FirstInstallDate.Equals(attributionContentUnlocked.FirstInstallDate)))
			{
				return false;
			}
			if (HasBundleId != attributionContentUnlocked.HasBundleId || (HasBundleId && !BundleId.Equals(attributionContentUnlocked.BundleId)))
			{
				return false;
			}
			if (HasContentId != attributionContentUnlocked.HasContentId || (HasContentId && !ContentId.Equals(attributionContentUnlocked.ContentId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AttributionContentUnlocked Deserialize(Stream stream, AttributionContentUnlocked instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AttributionContentUnlocked DeserializeLengthDelimited(Stream stream)
		{
			AttributionContentUnlocked attributionContentUnlocked = new AttributionContentUnlocked();
			DeserializeLengthDelimited(stream, attributionContentUnlocked);
			return attributionContentUnlocked;
		}

		public static AttributionContentUnlocked DeserializeLengthDelimited(Stream stream, AttributionContentUnlocked instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AttributionContentUnlocked Deserialize(Stream stream, AttributionContentUnlocked instance, long limit)
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
				case 42:
					instance.ContentId = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, AttributionContentUnlocked instance)
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
			if (instance.HasContentId)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ContentId));
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
			if (HasContentId)
			{
				num++;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(ContentId);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			return num;
		}
	}
}
