using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class LanguageChanged : IProtoBuf
	{
		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasPreviousLanguage;

		private string _PreviousLanguage;

		public bool HasNextLanguage;

		private string _NextLanguage;

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

		public string PreviousLanguage
		{
			get
			{
				return _PreviousLanguage;
			}
			set
			{
				_PreviousLanguage = value;
				HasPreviousLanguage = value != null;
			}
		}

		public string NextLanguage
		{
			get
			{
				return _NextLanguage;
			}
			set
			{
				_NextLanguage = value;
				HasNextLanguage = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasDeviceInfo)
			{
				num ^= DeviceInfo.GetHashCode();
			}
			if (HasPreviousLanguage)
			{
				num ^= PreviousLanguage.GetHashCode();
			}
			if (HasNextLanguage)
			{
				num ^= NextLanguage.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			LanguageChanged languageChanged = obj as LanguageChanged;
			if (languageChanged == null)
			{
				return false;
			}
			if (HasDeviceInfo != languageChanged.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(languageChanged.DeviceInfo)))
			{
				return false;
			}
			if (HasPreviousLanguage != languageChanged.HasPreviousLanguage || (HasPreviousLanguage && !PreviousLanguage.Equals(languageChanged.PreviousLanguage)))
			{
				return false;
			}
			if (HasNextLanguage != languageChanged.HasNextLanguage || (HasNextLanguage && !NextLanguage.Equals(languageChanged.NextLanguage)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static LanguageChanged Deserialize(Stream stream, LanguageChanged instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static LanguageChanged DeserializeLengthDelimited(Stream stream)
		{
			LanguageChanged languageChanged = new LanguageChanged();
			DeserializeLengthDelimited(stream, languageChanged);
			return languageChanged;
		}

		public static LanguageChanged DeserializeLengthDelimited(Stream stream, LanguageChanged instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static LanguageChanged Deserialize(Stream stream, LanguageChanged instance, long limit)
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
					instance.PreviousLanguage = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.NextLanguage = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, LanguageChanged instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasPreviousLanguage)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.PreviousLanguage));
			}
			if (instance.HasNextLanguage)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.NextLanguage));
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
			if (HasPreviousLanguage)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(PreviousLanguage);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasNextLanguage)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(NextLanguage);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}
	}
}
