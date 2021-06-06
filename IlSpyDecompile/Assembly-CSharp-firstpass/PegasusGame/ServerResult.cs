using System.IO;

namespace PegasusGame
{
	public class ServerResult : IProtoBuf
	{
		public enum Code
		{
			RESULT_OK,
			RESULT_RETRY,
			RESULT_NOT_EXISTS
		}

		public enum Constants
		{
			DEFAULT_RETRY_SECONDS = 2
		}

		public enum PacketID
		{
			ID = 23
		}

		public bool HasRetryDelaySeconds;

		private float _RetryDelaySeconds;

		public int ResultCode { get; set; }

		public float RetryDelaySeconds
		{
			get
			{
				return _RetryDelaySeconds;
			}
			set
			{
				_RetryDelaySeconds = value;
				HasRetryDelaySeconds = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= ResultCode.GetHashCode();
			if (HasRetryDelaySeconds)
			{
				hashCode ^= RetryDelaySeconds.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ServerResult serverResult = obj as ServerResult;
			if (serverResult == null)
			{
				return false;
			}
			if (!ResultCode.Equals(serverResult.ResultCode))
			{
				return false;
			}
			if (HasRetryDelaySeconds != serverResult.HasRetryDelaySeconds || (HasRetryDelaySeconds && !RetryDelaySeconds.Equals(serverResult.RetryDelaySeconds)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ServerResult Deserialize(Stream stream, ServerResult instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ServerResult DeserializeLengthDelimited(Stream stream)
		{
			ServerResult serverResult = new ServerResult();
			DeserializeLengthDelimited(stream, serverResult);
			return serverResult;
		}

		public static ServerResult DeserializeLengthDelimited(Stream stream, ServerResult instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ServerResult Deserialize(Stream stream, ServerResult instance, long limit)
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
					instance.ResultCode = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 21:
					instance.RetryDelaySeconds = binaryReader.ReadSingle();
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

		public static void Serialize(Stream stream, ServerResult instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ResultCode);
			if (instance.HasRetryDelaySeconds)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.RetryDelaySeconds);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)ResultCode);
			if (HasRetryDelaySeconds)
			{
				num++;
				num += 4;
			}
			return num + 1;
		}
	}
}
