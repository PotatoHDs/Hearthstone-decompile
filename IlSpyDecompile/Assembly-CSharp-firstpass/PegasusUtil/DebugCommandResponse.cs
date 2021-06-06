using System.IO;
using System.Text;

namespace PegasusUtil
{
	public class DebugCommandResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 324
		}

		public bool HasResponse;

		private string _Response;

		public bool Success { get; set; }

		public string Response
		{
			get
			{
				return _Response;
			}
			set
			{
				_Response = value;
				HasResponse = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Success.GetHashCode();
			if (HasResponse)
			{
				hashCode ^= Response.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			DebugCommandResponse debugCommandResponse = obj as DebugCommandResponse;
			if (debugCommandResponse == null)
			{
				return false;
			}
			if (!Success.Equals(debugCommandResponse.Success))
			{
				return false;
			}
			if (HasResponse != debugCommandResponse.HasResponse || (HasResponse && !Response.Equals(debugCommandResponse.Response)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DebugCommandResponse Deserialize(Stream stream, DebugCommandResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DebugCommandResponse DeserializeLengthDelimited(Stream stream)
		{
			DebugCommandResponse debugCommandResponse = new DebugCommandResponse();
			DeserializeLengthDelimited(stream, debugCommandResponse);
			return debugCommandResponse;
		}

		public static DebugCommandResponse DeserializeLengthDelimited(Stream stream, DebugCommandResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DebugCommandResponse Deserialize(Stream stream, DebugCommandResponse instance, long limit)
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
					instance.Success = ProtocolParser.ReadBool(stream);
					continue;
				case 18:
					instance.Response = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, DebugCommandResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteBool(stream, instance.Success);
			if (instance.HasResponse)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Response));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num++;
			if (HasResponse)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Response);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num + 1;
		}
	}
}
