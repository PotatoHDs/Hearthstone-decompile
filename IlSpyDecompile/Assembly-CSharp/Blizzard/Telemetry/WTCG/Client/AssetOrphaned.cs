using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class AssetOrphaned : IProtoBuf
	{
		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasFilePath;

		private string _FilePath;

		public bool HasHandleOwner;

		private string _HandleOwner;

		public bool HasHandleType;

		private string _HandleType;

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

		public string FilePath
		{
			get
			{
				return _FilePath;
			}
			set
			{
				_FilePath = value;
				HasFilePath = value != null;
			}
		}

		public string HandleOwner
		{
			get
			{
				return _HandleOwner;
			}
			set
			{
				_HandleOwner = value;
				HasHandleOwner = value != null;
			}
		}

		public string HandleType
		{
			get
			{
				return _HandleType;
			}
			set
			{
				_HandleType = value;
				HasHandleType = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasDeviceInfo)
			{
				num ^= DeviceInfo.GetHashCode();
			}
			if (HasFilePath)
			{
				num ^= FilePath.GetHashCode();
			}
			if (HasHandleOwner)
			{
				num ^= HandleOwner.GetHashCode();
			}
			if (HasHandleType)
			{
				num ^= HandleType.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AssetOrphaned assetOrphaned = obj as AssetOrphaned;
			if (assetOrphaned == null)
			{
				return false;
			}
			if (HasDeviceInfo != assetOrphaned.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(assetOrphaned.DeviceInfo)))
			{
				return false;
			}
			if (HasFilePath != assetOrphaned.HasFilePath || (HasFilePath && !FilePath.Equals(assetOrphaned.FilePath)))
			{
				return false;
			}
			if (HasHandleOwner != assetOrphaned.HasHandleOwner || (HasHandleOwner && !HandleOwner.Equals(assetOrphaned.HandleOwner)))
			{
				return false;
			}
			if (HasHandleType != assetOrphaned.HasHandleType || (HasHandleType && !HandleType.Equals(assetOrphaned.HandleType)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AssetOrphaned Deserialize(Stream stream, AssetOrphaned instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AssetOrphaned DeserializeLengthDelimited(Stream stream)
		{
			AssetOrphaned assetOrphaned = new AssetOrphaned();
			DeserializeLengthDelimited(stream, assetOrphaned);
			return assetOrphaned;
		}

		public static AssetOrphaned DeserializeLengthDelimited(Stream stream, AssetOrphaned instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AssetOrphaned Deserialize(Stream stream, AssetOrphaned instance, long limit)
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
					instance.FilePath = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.HandleOwner = ProtocolParser.ReadString(stream);
					continue;
				case 34:
					instance.HandleType = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, AssetOrphaned instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasFilePath)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FilePath));
			}
			if (instance.HasHandleOwner)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.HandleOwner));
			}
			if (instance.HasHandleType)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.HandleType));
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
			if (HasFilePath)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(FilePath);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasHandleOwner)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(HandleOwner);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasHandleType)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(HandleType);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num;
		}
	}
}
