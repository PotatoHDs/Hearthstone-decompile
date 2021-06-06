using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.NGDP
{
	public class UpdateStarted : IProtoBuf
	{
		public bool HasInstalledVersion;

		private string _InstalledVersion;

		public bool HasTextureFormat;

		private string _TextureFormat;

		public bool HasDataPath;

		private string _DataPath;

		public bool HasAvailableSpaceMB;

		private float _AvailableSpaceMB;

		public string InstalledVersion
		{
			get
			{
				return _InstalledVersion;
			}
			set
			{
				_InstalledVersion = value;
				HasInstalledVersion = value != null;
			}
		}

		public string TextureFormat
		{
			get
			{
				return _TextureFormat;
			}
			set
			{
				_TextureFormat = value;
				HasTextureFormat = value != null;
			}
		}

		public string DataPath
		{
			get
			{
				return _DataPath;
			}
			set
			{
				_DataPath = value;
				HasDataPath = value != null;
			}
		}

		public float AvailableSpaceMB
		{
			get
			{
				return _AvailableSpaceMB;
			}
			set
			{
				_AvailableSpaceMB = value;
				HasAvailableSpaceMB = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasInstalledVersion)
			{
				num ^= InstalledVersion.GetHashCode();
			}
			if (HasTextureFormat)
			{
				num ^= TextureFormat.GetHashCode();
			}
			if (HasDataPath)
			{
				num ^= DataPath.GetHashCode();
			}
			if (HasAvailableSpaceMB)
			{
				num ^= AvailableSpaceMB.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UpdateStarted updateStarted = obj as UpdateStarted;
			if (updateStarted == null)
			{
				return false;
			}
			if (HasInstalledVersion != updateStarted.HasInstalledVersion || (HasInstalledVersion && !InstalledVersion.Equals(updateStarted.InstalledVersion)))
			{
				return false;
			}
			if (HasTextureFormat != updateStarted.HasTextureFormat || (HasTextureFormat && !TextureFormat.Equals(updateStarted.TextureFormat)))
			{
				return false;
			}
			if (HasDataPath != updateStarted.HasDataPath || (HasDataPath && !DataPath.Equals(updateStarted.DataPath)))
			{
				return false;
			}
			if (HasAvailableSpaceMB != updateStarted.HasAvailableSpaceMB || (HasAvailableSpaceMB && !AvailableSpaceMB.Equals(updateStarted.AvailableSpaceMB)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UpdateStarted Deserialize(Stream stream, UpdateStarted instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UpdateStarted DeserializeLengthDelimited(Stream stream)
		{
			UpdateStarted updateStarted = new UpdateStarted();
			DeserializeLengthDelimited(stream, updateStarted);
			return updateStarted;
		}

		public static UpdateStarted DeserializeLengthDelimited(Stream stream, UpdateStarted instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UpdateStarted Deserialize(Stream stream, UpdateStarted instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					instance.InstalledVersion = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					instance.TextureFormat = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.DataPath = ProtocolParser.ReadString(stream);
					continue;
				case 37:
					instance.AvailableSpaceMB = binaryReader.ReadSingle();
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

		public static void Serialize(Stream stream, UpdateStarted instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasInstalledVersion)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.InstalledVersion));
			}
			if (instance.HasTextureFormat)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TextureFormat));
			}
			if (instance.HasDataPath)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DataPath));
			}
			if (instance.HasAvailableSpaceMB)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.AvailableSpaceMB);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasInstalledVersion)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(InstalledVersion);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasTextureFormat)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(TextureFormat);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasDataPath)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(DataPath);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasAvailableSpaceMB)
			{
				num++;
				num += 4;
			}
			return num;
		}
	}
}
