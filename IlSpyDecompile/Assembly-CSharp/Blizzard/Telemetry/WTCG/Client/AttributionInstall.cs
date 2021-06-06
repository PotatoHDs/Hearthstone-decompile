using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class AttributionInstall : IProtoBuf
	{
		public bool HasApplicationId;

		private string _ApplicationId;

		public bool HasDeviceType;

		private string _DeviceType;

		public bool HasFirstInstallDate;

		private ulong _FirstInstallDate;

		public bool HasBundleId;

		private string _BundleId;

		public bool HasReferrer;

		private string _Referrer;

		public bool HasAppleSearchAdsJson;

		private string _AppleSearchAdsJson;

		public bool HasAppleSearchAdsErrorCode;

		private int _AppleSearchAdsErrorCode;

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

		public string Referrer
		{
			get
			{
				return _Referrer;
			}
			set
			{
				_Referrer = value;
				HasReferrer = value != null;
			}
		}

		public string AppleSearchAdsJson
		{
			get
			{
				return _AppleSearchAdsJson;
			}
			set
			{
				_AppleSearchAdsJson = value;
				HasAppleSearchAdsJson = value != null;
			}
		}

		public int AppleSearchAdsErrorCode
		{
			get
			{
				return _AppleSearchAdsErrorCode;
			}
			set
			{
				_AppleSearchAdsErrorCode = value;
				HasAppleSearchAdsErrorCode = true;
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
			if (HasReferrer)
			{
				num ^= Referrer.GetHashCode();
			}
			if (HasAppleSearchAdsJson)
			{
				num ^= AppleSearchAdsJson.GetHashCode();
			}
			if (HasAppleSearchAdsErrorCode)
			{
				num ^= AppleSearchAdsErrorCode.GetHashCode();
			}
			if (HasIdentifier)
			{
				num ^= Identifier.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AttributionInstall attributionInstall = obj as AttributionInstall;
			if (attributionInstall == null)
			{
				return false;
			}
			if (HasApplicationId != attributionInstall.HasApplicationId || (HasApplicationId && !ApplicationId.Equals(attributionInstall.ApplicationId)))
			{
				return false;
			}
			if (HasDeviceType != attributionInstall.HasDeviceType || (HasDeviceType && !DeviceType.Equals(attributionInstall.DeviceType)))
			{
				return false;
			}
			if (HasFirstInstallDate != attributionInstall.HasFirstInstallDate || (HasFirstInstallDate && !FirstInstallDate.Equals(attributionInstall.FirstInstallDate)))
			{
				return false;
			}
			if (HasBundleId != attributionInstall.HasBundleId || (HasBundleId && !BundleId.Equals(attributionInstall.BundleId)))
			{
				return false;
			}
			if (HasReferrer != attributionInstall.HasReferrer || (HasReferrer && !Referrer.Equals(attributionInstall.Referrer)))
			{
				return false;
			}
			if (HasAppleSearchAdsJson != attributionInstall.HasAppleSearchAdsJson || (HasAppleSearchAdsJson && !AppleSearchAdsJson.Equals(attributionInstall.AppleSearchAdsJson)))
			{
				return false;
			}
			if (HasAppleSearchAdsErrorCode != attributionInstall.HasAppleSearchAdsErrorCode || (HasAppleSearchAdsErrorCode && !AppleSearchAdsErrorCode.Equals(attributionInstall.AppleSearchAdsErrorCode)))
			{
				return false;
			}
			if (HasIdentifier != attributionInstall.HasIdentifier || (HasIdentifier && !Identifier.Equals(attributionInstall.Identifier)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AttributionInstall Deserialize(Stream stream, AttributionInstall instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AttributionInstall DeserializeLengthDelimited(Stream stream)
		{
			AttributionInstall attributionInstall = new AttributionInstall();
			DeserializeLengthDelimited(stream, attributionInstall);
			return attributionInstall;
		}

		public static AttributionInstall DeserializeLengthDelimited(Stream stream, AttributionInstall instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AttributionInstall Deserialize(Stream stream, AttributionInstall instance, long limit)
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
				case 200u:
					if (key.WireType == Wire.LengthDelimited)
					{
						instance.Referrer = ProtocolParser.ReadString(stream);
					}
					break;
				case 300u:
					if (key.WireType == Wire.LengthDelimited)
					{
						instance.AppleSearchAdsJson = ProtocolParser.ReadString(stream);
					}
					break;
				case 301u:
					if (key.WireType == Wire.Varint)
					{
						instance.AppleSearchAdsErrorCode = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, AttributionInstall instance)
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
			if (instance.HasReferrer)
			{
				stream.WriteByte(194);
				stream.WriteByte(12);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Referrer));
			}
			if (instance.HasAppleSearchAdsJson)
			{
				stream.WriteByte(226);
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AppleSearchAdsJson));
			}
			if (instance.HasAppleSearchAdsErrorCode)
			{
				stream.WriteByte(232);
				stream.WriteByte(18);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.AppleSearchAdsErrorCode);
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
			if (HasReferrer)
			{
				num += 2;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(Referrer);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (HasAppleSearchAdsJson)
			{
				num += 2;
				uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(AppleSearchAdsJson);
				num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			}
			if (HasAppleSearchAdsErrorCode)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)AppleSearchAdsErrorCode);
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
