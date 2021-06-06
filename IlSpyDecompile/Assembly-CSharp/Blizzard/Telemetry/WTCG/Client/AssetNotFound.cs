using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class AssetNotFound : IProtoBuf
	{
		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasAssetType;

		private string _AssetType;

		public bool HasAssetGuid;

		private string _AssetGuid;

		public bool HasFilePath;

		private string _FilePath;

		public bool HasLegacyName;

		private string _LegacyName;

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

		public string AssetType
		{
			get
			{
				return _AssetType;
			}
			set
			{
				_AssetType = value;
				HasAssetType = value != null;
			}
		}

		public string AssetGuid
		{
			get
			{
				return _AssetGuid;
			}
			set
			{
				_AssetGuid = value;
				HasAssetGuid = value != null;
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

		public string LegacyName
		{
			get
			{
				return _LegacyName;
			}
			set
			{
				_LegacyName = value;
				HasLegacyName = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasDeviceInfo)
			{
				num ^= DeviceInfo.GetHashCode();
			}
			if (HasAssetType)
			{
				num ^= AssetType.GetHashCode();
			}
			if (HasAssetGuid)
			{
				num ^= AssetGuid.GetHashCode();
			}
			if (HasFilePath)
			{
				num ^= FilePath.GetHashCode();
			}
			if (HasLegacyName)
			{
				num ^= LegacyName.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AssetNotFound assetNotFound = obj as AssetNotFound;
			if (assetNotFound == null)
			{
				return false;
			}
			if (HasDeviceInfo != assetNotFound.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(assetNotFound.DeviceInfo)))
			{
				return false;
			}
			if (HasAssetType != assetNotFound.HasAssetType || (HasAssetType && !AssetType.Equals(assetNotFound.AssetType)))
			{
				return false;
			}
			if (HasAssetGuid != assetNotFound.HasAssetGuid || (HasAssetGuid && !AssetGuid.Equals(assetNotFound.AssetGuid)))
			{
				return false;
			}
			if (HasFilePath != assetNotFound.HasFilePath || (HasFilePath && !FilePath.Equals(assetNotFound.FilePath)))
			{
				return false;
			}
			if (HasLegacyName != assetNotFound.HasLegacyName || (HasLegacyName && !LegacyName.Equals(assetNotFound.LegacyName)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AssetNotFound Deserialize(Stream stream, AssetNotFound instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AssetNotFound DeserializeLengthDelimited(Stream stream)
		{
			AssetNotFound assetNotFound = new AssetNotFound();
			DeserializeLengthDelimited(stream, assetNotFound);
			return assetNotFound;
		}

		public static AssetNotFound DeserializeLengthDelimited(Stream stream, AssetNotFound instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AssetNotFound Deserialize(Stream stream, AssetNotFound instance, long limit)
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
					instance.AssetType = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.AssetGuid = ProtocolParser.ReadString(stream);
					continue;
				case 34:
					instance.FilePath = ProtocolParser.ReadString(stream);
					continue;
				case 42:
					instance.LegacyName = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, AssetNotFound instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasAssetType)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AssetType));
			}
			if (instance.HasAssetGuid)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AssetGuid));
			}
			if (instance.HasFilePath)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FilePath));
			}
			if (instance.HasLegacyName)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.LegacyName));
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
			if (HasAssetType)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(AssetType);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasAssetGuid)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(AssetGuid);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasFilePath)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(FilePath);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasLegacyName)
			{
				num++;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(LegacyName);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			return num;
		}
	}
}
