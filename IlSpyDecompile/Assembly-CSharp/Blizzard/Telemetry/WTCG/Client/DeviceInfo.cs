using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class DeviceInfo : IProtoBuf
	{
		public enum OSCategory
		{
			WINDOWS = 1,
			MAC,
			IOS,
			ANDROID
		}

		public enum ConnectionType
		{
			WIRED = 1,
			WIFI,
			CELLULAR,
			UNKNOWN
		}

		public enum ScreenCategory
		{
			PHONE = 1,
			MINI_TABLET,
			TABLET,
			PC
		}

		public bool HasOs;

		private OSCategory _Os;

		public bool HasOsVersion;

		private string _OsVersion;

		public bool HasModel;

		private string _Model;

		public bool HasScreen;

		private ScreenCategory _Screen;

		public bool HasConnectionType_;

		private ConnectionType _ConnectionType_;

		public bool HasDroidTextureCompression;

		private string _DroidTextureCompression;

		public OSCategory Os
		{
			get
			{
				return _Os;
			}
			set
			{
				_Os = value;
				HasOs = true;
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

		public string Model
		{
			get
			{
				return _Model;
			}
			set
			{
				_Model = value;
				HasModel = value != null;
			}
		}

		public ScreenCategory Screen
		{
			get
			{
				return _Screen;
			}
			set
			{
				_Screen = value;
				HasScreen = true;
			}
		}

		public ConnectionType ConnectionType_
		{
			get
			{
				return _ConnectionType_;
			}
			set
			{
				_ConnectionType_ = value;
				HasConnectionType_ = true;
			}
		}

		public string DroidTextureCompression
		{
			get
			{
				return _DroidTextureCompression;
			}
			set
			{
				_DroidTextureCompression = value;
				HasDroidTextureCompression = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasOs)
			{
				num ^= Os.GetHashCode();
			}
			if (HasOsVersion)
			{
				num ^= OsVersion.GetHashCode();
			}
			if (HasModel)
			{
				num ^= Model.GetHashCode();
			}
			if (HasScreen)
			{
				num ^= Screen.GetHashCode();
			}
			if (HasConnectionType_)
			{
				num ^= ConnectionType_.GetHashCode();
			}
			if (HasDroidTextureCompression)
			{
				num ^= DroidTextureCompression.GetHashCode();
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
			if (HasOs != deviceInfo.HasOs || (HasOs && !Os.Equals(deviceInfo.Os)))
			{
				return false;
			}
			if (HasOsVersion != deviceInfo.HasOsVersion || (HasOsVersion && !OsVersion.Equals(deviceInfo.OsVersion)))
			{
				return false;
			}
			if (HasModel != deviceInfo.HasModel || (HasModel && !Model.Equals(deviceInfo.Model)))
			{
				return false;
			}
			if (HasScreen != deviceInfo.HasScreen || (HasScreen && !Screen.Equals(deviceInfo.Screen)))
			{
				return false;
			}
			if (HasConnectionType_ != deviceInfo.HasConnectionType_ || (HasConnectionType_ && !ConnectionType_.Equals(deviceInfo.ConnectionType_)))
			{
				return false;
			}
			if (HasDroidTextureCompression != deviceInfo.HasDroidTextureCompression || (HasDroidTextureCompression && !DroidTextureCompression.Equals(deviceInfo.DroidTextureCompression)))
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
			instance.Os = OSCategory.WINDOWS;
			instance.Screen = ScreenCategory.PHONE;
			instance.ConnectionType_ = ConnectionType.WIRED;
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
				case 8:
					instance.Os = (OSCategory)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					instance.OsVersion = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.Model = ProtocolParser.ReadString(stream);
					continue;
				case 32:
					instance.Screen = (ScreenCategory)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.ConnectionType_ = (ConnectionType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 50:
					instance.DroidTextureCompression = ProtocolParser.ReadString(stream);
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
			if (instance.HasOs)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Os);
			}
			if (instance.HasOsVersion)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.OsVersion));
			}
			if (instance.HasModel)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Model));
			}
			if (instance.HasScreen)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Screen);
			}
			if (instance.HasConnectionType_)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ConnectionType_);
			}
			if (instance.HasDroidTextureCompression)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DroidTextureCompression));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasOs)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Os);
			}
			if (HasOsVersion)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(OsVersion);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasModel)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Model);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasScreen)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Screen);
			}
			if (HasConnectionType_)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ConnectionType_);
			}
			if (HasDroidTextureCompression)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(DroidTextureCompression);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num;
		}
	}
}
