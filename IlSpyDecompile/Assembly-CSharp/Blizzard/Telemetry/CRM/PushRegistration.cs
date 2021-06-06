using System.IO;
using System.Text;

namespace Blizzard.Telemetry.CRM
{
	public class PushRegistration : IProtoBuf
	{
		public bool HasPushId;

		private string _PushId;

		public bool HasUtcOffset;

		private int _UtcOffset;

		public bool HasTimezone;

		private string _Timezone;

		public bool HasApplicationId;

		private string _ApplicationId;

		public bool HasLanguage;

		private string _Language;

		public bool HasOs;

		private string _Os;

		public bool HasOsVersion;

		private string _OsVersion;

		public bool HasDeviceHeight;

		private string _DeviceHeight;

		public bool HasDeviceWidth;

		private string _DeviceWidth;

		public bool HasDeviceDpi;

		private string _DeviceDpi;

		public string PushId
		{
			get
			{
				return _PushId;
			}
			set
			{
				_PushId = value;
				HasPushId = value != null;
			}
		}

		public int UtcOffset
		{
			get
			{
				return _UtcOffset;
			}
			set
			{
				_UtcOffset = value;
				HasUtcOffset = true;
			}
		}

		public string Timezone
		{
			get
			{
				return _Timezone;
			}
			set
			{
				_Timezone = value;
				HasTimezone = value != null;
			}
		}

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

		public string Language
		{
			get
			{
				return _Language;
			}
			set
			{
				_Language = value;
				HasLanguage = value != null;
			}
		}

		public string Os
		{
			get
			{
				return _Os;
			}
			set
			{
				_Os = value;
				HasOs = value != null;
			}
		}

		public string OsVersion
		{
			get
			{
				return _OsVersion;
			}
			set
			{
				_OsVersion = value;
				HasOsVersion = value != null;
			}
		}

		public string DeviceHeight
		{
			get
			{
				return _DeviceHeight;
			}
			set
			{
				_DeviceHeight = value;
				HasDeviceHeight = value != null;
			}
		}

		public string DeviceWidth
		{
			get
			{
				return _DeviceWidth;
			}
			set
			{
				_DeviceWidth = value;
				HasDeviceWidth = value != null;
			}
		}

		public string DeviceDpi
		{
			get
			{
				return _DeviceDpi;
			}
			set
			{
				_DeviceDpi = value;
				HasDeviceDpi = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasPushId)
			{
				num ^= PushId.GetHashCode();
			}
			if (HasUtcOffset)
			{
				num ^= UtcOffset.GetHashCode();
			}
			if (HasTimezone)
			{
				num ^= Timezone.GetHashCode();
			}
			if (HasApplicationId)
			{
				num ^= ApplicationId.GetHashCode();
			}
			if (HasLanguage)
			{
				num ^= Language.GetHashCode();
			}
			if (HasOs)
			{
				num ^= Os.GetHashCode();
			}
			if (HasOsVersion)
			{
				num ^= OsVersion.GetHashCode();
			}
			if (HasDeviceHeight)
			{
				num ^= DeviceHeight.GetHashCode();
			}
			if (HasDeviceWidth)
			{
				num ^= DeviceWidth.GetHashCode();
			}
			if (HasDeviceDpi)
			{
				num ^= DeviceDpi.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PushRegistration pushRegistration = obj as PushRegistration;
			if (pushRegistration == null)
			{
				return false;
			}
			if (HasPushId != pushRegistration.HasPushId || (HasPushId && !PushId.Equals(pushRegistration.PushId)))
			{
				return false;
			}
			if (HasUtcOffset != pushRegistration.HasUtcOffset || (HasUtcOffset && !UtcOffset.Equals(pushRegistration.UtcOffset)))
			{
				return false;
			}
			if (HasTimezone != pushRegistration.HasTimezone || (HasTimezone && !Timezone.Equals(pushRegistration.Timezone)))
			{
				return false;
			}
			if (HasApplicationId != pushRegistration.HasApplicationId || (HasApplicationId && !ApplicationId.Equals(pushRegistration.ApplicationId)))
			{
				return false;
			}
			if (HasLanguage != pushRegistration.HasLanguage || (HasLanguage && !Language.Equals(pushRegistration.Language)))
			{
				return false;
			}
			if (HasOs != pushRegistration.HasOs || (HasOs && !Os.Equals(pushRegistration.Os)))
			{
				return false;
			}
			if (HasOsVersion != pushRegistration.HasOsVersion || (HasOsVersion && !OsVersion.Equals(pushRegistration.OsVersion)))
			{
				return false;
			}
			if (HasDeviceHeight != pushRegistration.HasDeviceHeight || (HasDeviceHeight && !DeviceHeight.Equals(pushRegistration.DeviceHeight)))
			{
				return false;
			}
			if (HasDeviceWidth != pushRegistration.HasDeviceWidth || (HasDeviceWidth && !DeviceWidth.Equals(pushRegistration.DeviceWidth)))
			{
				return false;
			}
			if (HasDeviceDpi != pushRegistration.HasDeviceDpi || (HasDeviceDpi && !DeviceDpi.Equals(pushRegistration.DeviceDpi)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PushRegistration Deserialize(Stream stream, PushRegistration instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PushRegistration DeserializeLengthDelimited(Stream stream)
		{
			PushRegistration pushRegistration = new PushRegistration();
			DeserializeLengthDelimited(stream, pushRegistration);
			return pushRegistration;
		}

		public static PushRegistration DeserializeLengthDelimited(Stream stream, PushRegistration instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PushRegistration Deserialize(Stream stream, PushRegistration instance, long limit)
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
					instance.PushId = ProtocolParser.ReadString(stream);
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
							instance.UtcOffset = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 30u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.Timezone = ProtocolParser.ReadString(stream);
						}
						break;
					case 40u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.ApplicationId = ProtocolParser.ReadString(stream);
						}
						break;
					case 50u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.Language = ProtocolParser.ReadString(stream);
						}
						break;
					case 60u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.Os = ProtocolParser.ReadString(stream);
						}
						break;
					case 70u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.OsVersion = ProtocolParser.ReadString(stream);
						}
						break;
					case 80u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.DeviceHeight = ProtocolParser.ReadString(stream);
						}
						break;
					case 90u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.DeviceWidth = ProtocolParser.ReadString(stream);
						}
						break;
					case 100u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.DeviceDpi = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, PushRegistration instance)
		{
			if (instance.HasPushId)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.PushId));
			}
			if (instance.HasUtcOffset)
			{
				stream.WriteByte(160);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.UtcOffset);
			}
			if (instance.HasTimezone)
			{
				stream.WriteByte(242);
				stream.WriteByte(1);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Timezone));
			}
			if (instance.HasApplicationId)
			{
				stream.WriteByte(194);
				stream.WriteByte(2);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ApplicationId));
			}
			if (instance.HasLanguage)
			{
				stream.WriteByte(146);
				stream.WriteByte(3);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Language));
			}
			if (instance.HasOs)
			{
				stream.WriteByte(226);
				stream.WriteByte(3);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Os));
			}
			if (instance.HasOsVersion)
			{
				stream.WriteByte(178);
				stream.WriteByte(4);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.OsVersion));
			}
			if (instance.HasDeviceHeight)
			{
				stream.WriteByte(130);
				stream.WriteByte(5);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeviceHeight));
			}
			if (instance.HasDeviceWidth)
			{
				stream.WriteByte(210);
				stream.WriteByte(5);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeviceWidth));
			}
			if (instance.HasDeviceDpi)
			{
				stream.WriteByte(162);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeviceDpi));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasPushId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(PushId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasUtcOffset)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)UtcOffset);
			}
			if (HasTimezone)
			{
				num += 2;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Timezone);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasApplicationId)
			{
				num += 2;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(ApplicationId);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasLanguage)
			{
				num += 2;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(Language);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (HasOs)
			{
				num += 2;
				uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(Os);
				num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			}
			if (HasOsVersion)
			{
				num += 2;
				uint byteCount6 = (uint)Encoding.UTF8.GetByteCount(OsVersion);
				num += ProtocolParser.SizeOfUInt32(byteCount6) + byteCount6;
			}
			if (HasDeviceHeight)
			{
				num += 2;
				uint byteCount7 = (uint)Encoding.UTF8.GetByteCount(DeviceHeight);
				num += ProtocolParser.SizeOfUInt32(byteCount7) + byteCount7;
			}
			if (HasDeviceWidth)
			{
				num += 2;
				uint byteCount8 = (uint)Encoding.UTF8.GetByteCount(DeviceWidth);
				num += ProtocolParser.SizeOfUInt32(byteCount8) + byteCount8;
			}
			if (HasDeviceDpi)
			{
				num += 2;
				uint byteCount9 = (uint)Encoding.UTF8.GetByteCount(DeviceDpi);
				num += ProtocolParser.SizeOfUInt32(byteCount9) + byteCount9;
			}
			return num;
		}
	}
}
