using System.IO;
using System.Text;

namespace BobNetProto
{
	public class DebugConsoleResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 124
		}

		public enum ResponseType
		{
			CONSOLE_OUTPUT,
			LOG_MESSAGE
		}

		public bool HasResponse;

		private string _Response;

		public bool HasResponseType_;

		private ResponseType _ResponseType_;

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

		public ResponseType ResponseType_
		{
			get
			{
				return _ResponseType_;
			}
			set
			{
				_ResponseType_ = value;
				HasResponseType_ = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasResponse)
			{
				num ^= Response.GetHashCode();
			}
			if (HasResponseType_)
			{
				num ^= ResponseType_.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			DebugConsoleResponse debugConsoleResponse = obj as DebugConsoleResponse;
			if (debugConsoleResponse == null)
			{
				return false;
			}
			if (HasResponse != debugConsoleResponse.HasResponse || (HasResponse && !Response.Equals(debugConsoleResponse.Response)))
			{
				return false;
			}
			if (HasResponseType_ != debugConsoleResponse.HasResponseType_ || (HasResponseType_ && !ResponseType_.Equals(debugConsoleResponse.ResponseType_)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DebugConsoleResponse Deserialize(Stream stream, DebugConsoleResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DebugConsoleResponse DeserializeLengthDelimited(Stream stream)
		{
			DebugConsoleResponse debugConsoleResponse = new DebugConsoleResponse();
			DeserializeLengthDelimited(stream, debugConsoleResponse);
			return debugConsoleResponse;
		}

		public static DebugConsoleResponse DeserializeLengthDelimited(Stream stream, DebugConsoleResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DebugConsoleResponse Deserialize(Stream stream, DebugConsoleResponse instance, long limit)
		{
			instance.ResponseType_ = ResponseType.CONSOLE_OUTPUT;
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
					instance.Response = ProtocolParser.ReadString(stream);
					continue;
				case 16:
					instance.ResponseType_ = (ResponseType)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, DebugConsoleResponse instance)
		{
			if (instance.HasResponse)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Response));
			}
			if (instance.HasResponseType_)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ResponseType_);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasResponse)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Response);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasResponseType_)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ResponseType_);
			}
			return num;
		}
	}
}
