using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.NGDP
{
	public class NotEnoughSpaceError : IProtoBuf
	{
		public bool HasAvailableSpace;

		private ulong _AvailableSpace;

		public bool HasExpectedOrgBytes;

		private ulong _ExpectedOrgBytes;

		public bool HasFilesDir;

		private string _FilesDir;

		public ulong AvailableSpace
		{
			get
			{
				return _AvailableSpace;
			}
			set
			{
				_AvailableSpace = value;
				HasAvailableSpace = true;
			}
		}

		public ulong ExpectedOrgBytes
		{
			get
			{
				return _ExpectedOrgBytes;
			}
			set
			{
				_ExpectedOrgBytes = value;
				HasExpectedOrgBytes = true;
			}
		}

		public string FilesDir
		{
			get
			{
				return _FilesDir;
			}
			set
			{
				_FilesDir = value;
				HasFilesDir = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAvailableSpace)
			{
				num ^= AvailableSpace.GetHashCode();
			}
			if (HasExpectedOrgBytes)
			{
				num ^= ExpectedOrgBytes.GetHashCode();
			}
			if (HasFilesDir)
			{
				num ^= FilesDir.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			NotEnoughSpaceError notEnoughSpaceError = obj as NotEnoughSpaceError;
			if (notEnoughSpaceError == null)
			{
				return false;
			}
			if (HasAvailableSpace != notEnoughSpaceError.HasAvailableSpace || (HasAvailableSpace && !AvailableSpace.Equals(notEnoughSpaceError.AvailableSpace)))
			{
				return false;
			}
			if (HasExpectedOrgBytes != notEnoughSpaceError.HasExpectedOrgBytes || (HasExpectedOrgBytes && !ExpectedOrgBytes.Equals(notEnoughSpaceError.ExpectedOrgBytes)))
			{
				return false;
			}
			if (HasFilesDir != notEnoughSpaceError.HasFilesDir || (HasFilesDir && !FilesDir.Equals(notEnoughSpaceError.FilesDir)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static NotEnoughSpaceError Deserialize(Stream stream, NotEnoughSpaceError instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static NotEnoughSpaceError DeserializeLengthDelimited(Stream stream)
		{
			NotEnoughSpaceError notEnoughSpaceError = new NotEnoughSpaceError();
			DeserializeLengthDelimited(stream, notEnoughSpaceError);
			return notEnoughSpaceError;
		}

		public static NotEnoughSpaceError DeserializeLengthDelimited(Stream stream, NotEnoughSpaceError instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static NotEnoughSpaceError Deserialize(Stream stream, NotEnoughSpaceError instance, long limit)
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
				case 8:
					instance.AvailableSpace = ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.ExpectedOrgBytes = ProtocolParser.ReadUInt64(stream);
					continue;
				case 42:
					instance.FilesDir = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, NotEnoughSpaceError instance)
		{
			if (instance.HasAvailableSpace)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.AvailableSpace);
			}
			if (instance.HasExpectedOrgBytes)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, instance.ExpectedOrgBytes);
			}
			if (instance.HasFilesDir)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FilesDir));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAvailableSpace)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(AvailableSpace);
			}
			if (HasExpectedOrgBytes)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(ExpectedOrgBytes);
			}
			if (HasFilesDir)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(FilesDir);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
