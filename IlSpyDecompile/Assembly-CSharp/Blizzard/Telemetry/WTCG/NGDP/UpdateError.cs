using System.IO;

namespace Blizzard.Telemetry.WTCG.NGDP
{
	public class UpdateError : IProtoBuf
	{
		public bool HasErrorCode;

		private uint _ErrorCode;

		public bool HasElapsedSeconds;

		private float _ElapsedSeconds;

		public uint ErrorCode
		{
			get
			{
				return _ErrorCode;
			}
			set
			{
				_ErrorCode = value;
				HasErrorCode = true;
			}
		}

		public float ElapsedSeconds
		{
			get
			{
				return _ElapsedSeconds;
			}
			set
			{
				_ElapsedSeconds = value;
				HasElapsedSeconds = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasErrorCode)
			{
				num ^= ErrorCode.GetHashCode();
			}
			if (HasElapsedSeconds)
			{
				num ^= ElapsedSeconds.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UpdateError updateError = obj as UpdateError;
			if (updateError == null)
			{
				return false;
			}
			if (HasErrorCode != updateError.HasErrorCode || (HasErrorCode && !ErrorCode.Equals(updateError.ErrorCode)))
			{
				return false;
			}
			if (HasElapsedSeconds != updateError.HasElapsedSeconds || (HasElapsedSeconds && !ElapsedSeconds.Equals(updateError.ElapsedSeconds)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UpdateError Deserialize(Stream stream, UpdateError instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UpdateError DeserializeLengthDelimited(Stream stream)
		{
			UpdateError updateError = new UpdateError();
			DeserializeLengthDelimited(stream, updateError);
			return updateError;
		}

		public static UpdateError DeserializeLengthDelimited(Stream stream, UpdateError instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UpdateError Deserialize(Stream stream, UpdateError instance, long limit)
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
				case 8:
					instance.ErrorCode = ProtocolParser.ReadUInt32(stream);
					continue;
				case 37:
					instance.ElapsedSeconds = binaryReader.ReadSingle();
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

		public static void Serialize(Stream stream, UpdateError instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasErrorCode)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.ErrorCode);
			}
			if (instance.HasElapsedSeconds)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.ElapsedSeconds);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasErrorCode)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(ErrorCode);
			}
			if (HasElapsedSeconds)
			{
				num++;
				num += 4;
			}
			return num;
		}
	}
}
