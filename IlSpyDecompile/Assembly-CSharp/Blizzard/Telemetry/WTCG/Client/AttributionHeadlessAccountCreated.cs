using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class AttributionHeadlessAccountCreated : IProtoBuf
	{
		public bool HasApplicationId;

		private string _ApplicationId;

		public bool HasDeviceType;

		private string _DeviceType;

		public bool HasFirstInstallDate;

		private ulong _FirstInstallDate;

		public bool HasBundleId;

		private string _BundleId;

		public bool HasIdentifier;

		private IdentifierInfo _Identifier;

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

		public IdentifierInfo Identifier
		{
			get
			{
				return _Identifier;
			}
			set
			{
				_Identifier = value;
				HasIdentifier = value != null;
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
			if (HasIdentifier)
			{
				num ^= Identifier.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AttributionHeadlessAccountCreated attributionHeadlessAccountCreated = obj as AttributionHeadlessAccountCreated;
			if (attributionHeadlessAccountCreated == null)
			{
				return false;
			}
			if (HasApplicationId != attributionHeadlessAccountCreated.HasApplicationId || (HasApplicationId && !ApplicationId.Equals(attributionHeadlessAccountCreated.ApplicationId)))
			{
				return false;
			}
			if (HasDeviceType != attributionHeadlessAccountCreated.HasDeviceType || (HasDeviceType && !DeviceType.Equals(attributionHeadlessAccountCreated.DeviceType)))
			{
				return false;
			}
			if (HasFirstInstallDate != attributionHeadlessAccountCreated.HasFirstInstallDate || (HasFirstInstallDate && !FirstInstallDate.Equals(attributionHeadlessAccountCreated.FirstInstallDate)))
			{
				return false;
			}
			if (HasBundleId != attributionHeadlessAccountCreated.HasBundleId || (HasBundleId && !BundleId.Equals(attributionHeadlessAccountCreated.BundleId)))
			{
				return false;
			}
			if (HasIdentifier != attributionHeadlessAccountCreated.HasIdentifier || (HasIdentifier && !Identifier.Equals(attributionHeadlessAccountCreated.Identifier)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AttributionHeadlessAccountCreated Deserialize(Stream stream, AttributionHeadlessAccountCreated instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AttributionHeadlessAccountCreated DeserializeLengthDelimited(Stream stream)
		{
			AttributionHeadlessAccountCreated attributionHeadlessAccountCreated = new AttributionHeadlessAccountCreated();
			DeserializeLengthDelimited(stream, attributionHeadlessAccountCreated);
			return attributionHeadlessAccountCreated;
		}

		public static AttributionHeadlessAccountCreated DeserializeLengthDelimited(Stream stream, AttributionHeadlessAccountCreated instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AttributionHeadlessAccountCreated Deserialize(Stream stream, AttributionHeadlessAccountCreated instance, long limit)
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
				if (num == -1)
				{
					if (limit < 0)
					{
						break;
					}
					throw new EndOfStreamException();
				}
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
				case 1000u:
					if (key.WireType == Wire.LengthDelimited)
					{
						if (instance.Identifier == null)
						{
							instance.Identifier = IdentifierInfo.DeserializeLengthDelimited(stream);
						}
						else
						{
							IdentifierInfo.DeserializeLengthDelimited(stream, instance.Identifier);
						}
					}
					break;
				default:
					ProtocolParser.SkipKey(stream, key);
					break;
				}
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, AttributionHeadlessAccountCreated instance)
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
			if (instance.HasIdentifier)
			{
				stream.WriteByte(194);
				stream.WriteByte(62);
				ProtocolParser.WriteUInt32(stream, instance.Identifier.GetSerializedSize());
				IdentifierInfo.Serialize(stream, instance.Identifier);
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
			if (HasIdentifier)
			{
				num += 2;
				uint serializedSize = Identifier.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
