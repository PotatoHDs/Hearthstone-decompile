using System.IO;
using HSCachedDeckCompletion;
using PegasusShared;

namespace PegasusUtil
{
	public class SmartDeckResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 370
		}

		public bool HasErrorCode;

		private ErrorCode _ErrorCode;

		public bool HasResponseMessage;

		private HSCachedDeckCompletionResponse _ResponseMessage;

		public ErrorCode ErrorCode
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

		public HSCachedDeckCompletionResponse ResponseMessage
		{
			get
			{
				return _ResponseMessage;
			}
			set
			{
				_ResponseMessage = value;
				HasResponseMessage = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasErrorCode)
			{
				num ^= ErrorCode.GetHashCode();
			}
			if (HasResponseMessage)
			{
				num ^= ResponseMessage.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SmartDeckResponse smartDeckResponse = obj as SmartDeckResponse;
			if (smartDeckResponse == null)
			{
				return false;
			}
			if (HasErrorCode != smartDeckResponse.HasErrorCode || (HasErrorCode && !ErrorCode.Equals(smartDeckResponse.ErrorCode)))
			{
				return false;
			}
			if (HasResponseMessage != smartDeckResponse.HasResponseMessage || (HasResponseMessage && !ResponseMessage.Equals(smartDeckResponse.ResponseMessage)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SmartDeckResponse Deserialize(Stream stream, SmartDeckResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SmartDeckResponse DeserializeLengthDelimited(Stream stream)
		{
			SmartDeckResponse smartDeckResponse = new SmartDeckResponse();
			DeserializeLengthDelimited(stream, smartDeckResponse);
			return smartDeckResponse;
		}

		public static SmartDeckResponse DeserializeLengthDelimited(Stream stream, SmartDeckResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SmartDeckResponse Deserialize(Stream stream, SmartDeckResponse instance, long limit)
		{
			instance.ErrorCode = ErrorCode.ERROR_OK;
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
					instance.ErrorCode = (ErrorCode)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					if (instance.ResponseMessage == null)
					{
						instance.ResponseMessage = HSCachedDeckCompletionResponse.DeserializeLengthDelimited(stream);
					}
					else
					{
						HSCachedDeckCompletionResponse.DeserializeLengthDelimited(stream, instance.ResponseMessage);
					}
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

		public static void Serialize(Stream stream, SmartDeckResponse instance)
		{
			if (instance.HasErrorCode)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ErrorCode);
			}
			if (instance.HasResponseMessage)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ResponseMessage.GetSerializedSize());
				HSCachedDeckCompletionResponse.Serialize(stream, instance.ResponseMessage);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasErrorCode)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ErrorCode);
			}
			if (HasResponseMessage)
			{
				num++;
				uint serializedSize = ResponseMessage.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
