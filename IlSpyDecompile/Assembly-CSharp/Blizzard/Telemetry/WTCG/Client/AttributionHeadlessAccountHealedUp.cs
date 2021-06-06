using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class AttributionHeadlessAccountHealedUp : IProtoBuf
	{
		public bool HasApplicationId;

		private string _ApplicationId;

		public bool HasDeviceType;

		private string _DeviceType;

		public bool HasFirstInstallDate;

		private ulong _FirstInstallDate;

		public bool HasBundleId;

		private string _BundleId;

		public bool HasTemporaryGameAccountId;

		private string _TemporaryGameAccountId;

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

		public string TemporaryGameAccountId
		{
			get
			{
				return _TemporaryGameAccountId;
			}
			set
			{
				_TemporaryGameAccountId = value;
				HasTemporaryGameAccountId = value != null;
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
			if (HasTemporaryGameAccountId)
			{
				num ^= TemporaryGameAccountId.GetHashCode();
			}
			if (HasIdentifier)
			{
				num ^= Identifier.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AttributionHeadlessAccountHealedUp attributionHeadlessAccountHealedUp = obj as AttributionHeadlessAccountHealedUp;
			if (attributionHeadlessAccountHealedUp == null)
			{
				return false;
			}
			if (HasApplicationId != attributionHeadlessAccountHealedUp.HasApplicationId || (HasApplicationId && !ApplicationId.Equals(attributionHeadlessAccountHealedUp.ApplicationId)))
			{
				return false;
			}
			if (HasDeviceType != attributionHeadlessAccountHealedUp.HasDeviceType || (HasDeviceType && !DeviceType.Equals(attributionHeadlessAccountHealedUp.DeviceType)))
			{
				return false;
			}
			if (HasFirstInstallDate != attributionHeadlessAccountHealedUp.HasFirstInstallDate || (HasFirstInstallDate && !FirstInstallDate.Equals(attributionHeadlessAccountHealedUp.FirstInstallDate)))
			{
				return false;
			}
			if (HasBundleId != attributionHeadlessAccountHealedUp.HasBundleId || (HasBundleId && !BundleId.Equals(attributionHeadlessAccountHealedUp.BundleId)))
			{
				return false;
			}
			if (HasTemporaryGameAccountId != attributionHeadlessAccountHealedUp.HasTemporaryGameAccountId || (HasTemporaryGameAccountId && !TemporaryGameAccountId.Equals(attributionHeadlessAccountHealedUp.TemporaryGameAccountId)))
			{
				return false;
			}
			if (HasIdentifier != attributionHeadlessAccountHealedUp.HasIdentifier || (HasIdentifier && !Identifier.Equals(attributionHeadlessAccountHealedUp.Identifier)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AttributionHeadlessAccountHealedUp Deserialize(Stream stream, AttributionHeadlessAccountHealedUp instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AttributionHeadlessAccountHealedUp DeserializeLengthDelimited(Stream stream)
		{
			AttributionHeadlessAccountHealedUp attributionHeadlessAccountHealedUp = new AttributionHeadlessAccountHealedUp();
			DeserializeLengthDelimited(stream, attributionHeadlessAccountHealedUp);
			return attributionHeadlessAccountHealedUp;
		}

		public static AttributionHeadlessAccountHealedUp DeserializeLengthDelimited(Stream stream, AttributionHeadlessAccountHealedUp instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AttributionHeadlessAccountHealedUp Deserialize(Stream stream, AttributionHeadlessAccountHealedUp instance, long limit)
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
					instance.TemporaryGameAccountId = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, AttributionHeadlessAccountHealedUp instance)
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
			if (instance.HasTemporaryGameAccountId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TemporaryGameAccountId));
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
			if (HasTemporaryGameAccountId)
			{
				num++;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(TemporaryGameAccountId);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
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
