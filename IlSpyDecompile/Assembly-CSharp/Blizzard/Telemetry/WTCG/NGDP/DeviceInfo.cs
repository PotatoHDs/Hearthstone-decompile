using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.NGDP
{
	public class DeviceInfo : IProtoBuf
	{
		public bool HasAndroidId;

		private string _AndroidId;

		public bool HasAndroidModel;

		private string _AndroidModel;

		public bool HasAndroidSdkVersion;

		private uint _AndroidSdkVersion;

		public bool HasIsConnectedToWifi;

		private bool _IsConnectedToWifi;

		public bool HasGpuTextureFormat;

		private string _GpuTextureFormat;

		public bool HasLocale;

		private string _Locale;

		public bool HasBnetRegion;

		private string _BnetRegion;

		public string AndroidId
		{
			get
			{
				return _AndroidId;
			}
			set
			{
				_AndroidId = value;
				HasAndroidId = value != null;
			}
		}

		public string AndroidModel
		{
			get
			{
				return _AndroidModel;
			}
			set
			{
				_AndroidModel = value;
				HasAndroidModel = value != null;
			}
		}

		public uint AndroidSdkVersion
		{
			get
			{
				return _AndroidSdkVersion;
			}
			set
			{
				_AndroidSdkVersion = value;
				HasAndroidSdkVersion = true;
			}
		}

		public bool IsConnectedToWifi
		{
			get
			{
				return _IsConnectedToWifi;
			}
			set
			{
				_IsConnectedToWifi = value;
				HasIsConnectedToWifi = true;
			}
		}

		public string GpuTextureFormat
		{
			get
			{
				return _GpuTextureFormat;
			}
			set
			{
				_GpuTextureFormat = value;
				HasGpuTextureFormat = value != null;
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

		public string BnetRegion
		{
			get
			{
				return _BnetRegion;
			}
			set
			{
				_BnetRegion = value;
				HasBnetRegion = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAndroidId)
			{
				num ^= AndroidId.GetHashCode();
			}
			if (HasAndroidModel)
			{
				num ^= AndroidModel.GetHashCode();
			}
			if (HasAndroidSdkVersion)
			{
				num ^= AndroidSdkVersion.GetHashCode();
			}
			if (HasIsConnectedToWifi)
			{
				num ^= IsConnectedToWifi.GetHashCode();
			}
			if (HasGpuTextureFormat)
			{
				num ^= GpuTextureFormat.GetHashCode();
			}
			if (HasLocale)
			{
				num ^= Locale.GetHashCode();
			}
			if (HasBnetRegion)
			{
				num ^= BnetRegion.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			DeviceInfo deviceInfo = obj as DeviceInfo;
			if (deviceInfo == null)
			{
				return false;
			}
			if (HasAndroidId != deviceInfo.HasAndroidId || (HasAndroidId && !AndroidId.Equals(deviceInfo.AndroidId)))
			{
				return false;
			}
			if (HasAndroidModel != deviceInfo.HasAndroidModel || (HasAndroidModel && !AndroidModel.Equals(deviceInfo.AndroidModel)))
			{
				return false;
			}
			if (HasAndroidSdkVersion != deviceInfo.HasAndroidSdkVersion || (HasAndroidSdkVersion && !AndroidSdkVersion.Equals(deviceInfo.AndroidSdkVersion)))
			{
				return false;
			}
			if (HasIsConnectedToWifi != deviceInfo.HasIsConnectedToWifi || (HasIsConnectedToWifi && !IsConnectedToWifi.Equals(deviceInfo.IsConnectedToWifi)))
			{
				return false;
			}
			if (HasGpuTextureFormat != deviceInfo.HasGpuTextureFormat || (HasGpuTextureFormat && !GpuTextureFormat.Equals(deviceInfo.GpuTextureFormat)))
			{
				return false;
			}
			if (HasLocale != deviceInfo.HasLocale || (HasLocale && !Locale.Equals(deviceInfo.Locale)))
			{
				return false;
			}
			if (HasBnetRegion != deviceInfo.HasBnetRegion || (HasBnetRegion && !BnetRegion.Equals(deviceInfo.BnetRegion)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DeviceInfo Deserialize(Stream stream, DeviceInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DeviceInfo DeserializeLengthDelimited(Stream stream)
		{
			DeviceInfo deviceInfo = new DeviceInfo();
			DeserializeLengthDelimited(stream, deviceInfo);
			return deviceInfo;
		}

		public static DeviceInfo DeserializeLengthDelimited(Stream stream, DeviceInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DeviceInfo Deserialize(Stream stream, DeviceInfo instance, long limit)
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
					instance.AndroidId = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					instance.AndroidModel = ProtocolParser.ReadString(stream);
					continue;
				case 24:
					instance.AndroidSdkVersion = ProtocolParser.ReadUInt32(stream);
					continue;
				case 32:
					instance.IsConnectedToWifi = ProtocolParser.ReadBool(stream);
					continue;
				case 42:
					instance.GpuTextureFormat = ProtocolParser.ReadString(stream);
					continue;
				case 50:
					instance.Locale = ProtocolParser.ReadString(stream);
					continue;
				case 58:
					instance.BnetRegion = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, DeviceInfo instance)
		{
			if (instance.HasAndroidId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AndroidId));
			}
			if (instance.HasAndroidModel)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AndroidModel));
			}
			if (instance.HasAndroidSdkVersion)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.AndroidSdkVersion);
			}
			if (instance.HasIsConnectedToWifi)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.IsConnectedToWifi);
			}
			if (instance.HasGpuTextureFormat)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.GpuTextureFormat));
			}
			if (instance.HasLocale)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Locale));
			}
			if (instance.HasBnetRegion)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BnetRegion));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAndroidId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(AndroidId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasAndroidModel)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(AndroidModel);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasAndroidSdkVersion)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(AndroidSdkVersion);
			}
			if (HasIsConnectedToWifi)
			{
				num++;
				num++;
			}
			if (HasGpuTextureFormat)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(GpuTextureFormat);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasLocale)
			{
				num++;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(Locale);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (HasBnetRegion)
			{
				num++;
				uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(BnetRegion);
				num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			}
			return num;
		}
	}
}
