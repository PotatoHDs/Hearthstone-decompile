using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class MissingAssetError : IProtoBuf
	{
		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasMissingAssetPath;

		private string _MissingAssetPath;

		public bool HasAssetContext;

		private string _AssetContext;

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

		public string MissingAssetPath
		{
			get
			{
				return _MissingAssetPath;
			}
			set
			{
				_MissingAssetPath = value;
				HasMissingAssetPath = value != null;
			}
		}

		public string AssetContext
		{
			get
			{
				return _AssetContext;
			}
			set
			{
				_AssetContext = value;
				HasAssetContext = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasDeviceInfo)
			{
				num ^= DeviceInfo.GetHashCode();
			}
			if (HasMissingAssetPath)
			{
				num ^= MissingAssetPath.GetHashCode();
			}
			if (HasAssetContext)
			{
				num ^= AssetContext.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			MissingAssetError missingAssetError = obj as MissingAssetError;
			if (missingAssetError == null)
			{
				return false;
			}
			if (HasDeviceInfo != missingAssetError.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(missingAssetError.DeviceInfo)))
			{
				return false;
			}
			if (HasMissingAssetPath != missingAssetError.HasMissingAssetPath || (HasMissingAssetPath && !MissingAssetPath.Equals(missingAssetError.MissingAssetPath)))
			{
				return false;
			}
			if (HasAssetContext != missingAssetError.HasAssetContext || (HasAssetContext && !AssetContext.Equals(missingAssetError.AssetContext)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static MissingAssetError Deserialize(Stream stream, MissingAssetError instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static MissingAssetError DeserializeLengthDelimited(Stream stream)
		{
			MissingAssetError missingAssetError = new MissingAssetError();
			DeserializeLengthDelimited(stream, missingAssetError);
			return missingAssetError;
		}

		public static MissingAssetError DeserializeLengthDelimited(Stream stream, MissingAssetError instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static MissingAssetError Deserialize(Stream stream, MissingAssetError instance, long limit)
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
					instance.MissingAssetPath = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.AssetContext = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, MissingAssetError instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasMissingAssetPath)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.MissingAssetPath));
			}
			if (instance.HasAssetContext)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AssetContext));
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
			if (HasMissingAssetPath)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(MissingAssetPath);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasAssetContext)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(AssetContext);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}
	}
}
