using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class LocaleDataUpdateStarted : IProtoBuf
	{
		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasLocale;

		private string _Locale;

		public DeviceInfo DeviceInfo
		{
			get
			{
				return _DeviceInfo;
			}
			set
			{
				_DeviceInfo = value;
				HasDeviceInfo = value != null;
			}
		}

		public string Locale
		{
			get
			{
				return _Locale;
			}
			set
			{
				_Locale = value;
				HasLocale = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasDeviceInfo)
			{
				num ^= DeviceInfo.GetHashCode();
			}
			if (HasLocale)
			{
				num ^= Locale.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			LocaleDataUpdateStarted localeDataUpdateStarted = obj as LocaleDataUpdateStarted;
			if (localeDataUpdateStarted == null)
			{
				return false;
			}
			if (HasDeviceInfo != localeDataUpdateStarted.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(localeDataUpdateStarted.DeviceInfo)))
			{
				return false;
			}
			if (HasLocale != localeDataUpdateStarted.HasLocale || (HasLocale && !Locale.Equals(localeDataUpdateStarted.Locale)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static LocaleDataUpdateStarted Deserialize(Stream stream, LocaleDataUpdateStarted instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static LocaleDataUpdateStarted DeserializeLengthDelimited(Stream stream)
		{
			LocaleDataUpdateStarted localeDataUpdateStarted = new LocaleDataUpdateStarted();
			DeserializeLengthDelimited(stream, localeDataUpdateStarted);
			return localeDataUpdateStarted;
		}

		public static LocaleDataUpdateStarted DeserializeLengthDelimited(Stream stream, LocaleDataUpdateStarted instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static LocaleDataUpdateStarted Deserialize(Stream stream, LocaleDataUpdateStarted instance, long limit)
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
					if (instance.DeviceInfo == null)
					{
						instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
					}
					continue;
				case 18:
					instance.Locale = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, LocaleDataUpdateStarted instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasLocale)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Locale));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasDeviceInfo)
			{
				num++;
				uint serializedSize = DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasLocale)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Locale);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
