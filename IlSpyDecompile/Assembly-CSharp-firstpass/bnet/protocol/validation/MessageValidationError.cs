using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.validation
{
	public class MessageValidationError : IProtoBuf
	{
		private List<FieldValidationError> _FieldError = new List<FieldValidationError>();

		public bool HasCustomError;

		private string _CustomError;

		public List<FieldValidationError> FieldError
		{
			get
			{
				return _FieldError;
			}
			set
			{
				_FieldError = value;
			}
		}

		public List<FieldValidationError> FieldErrorList => _FieldError;

		public int FieldErrorCount => _FieldError.Count;

		public string CustomError
		{
			get
			{
				return _CustomError;
			}
			set
			{
				_CustomError = value;
				HasCustomError = value != null;
			}
		}

		public bool IsInitialized => true;

		public void AddFieldError(FieldValidationError val)
		{
			_FieldError.Add(val);
		}

		public void ClearFieldError()
		{
			_FieldError.Clear();
		}

		public void SetFieldError(List<FieldValidationError> val)
		{
			FieldError = val;
		}

		public void SetCustomError(string val)
		{
			CustomError = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (FieldValidationError item in FieldError)
			{
				num ^= item.GetHashCode();
			}
			if (HasCustomError)
			{
				num ^= CustomError.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			MessageValidationError messageValidationError = obj as MessageValidationError;
			if (messageValidationError == null)
			{
				return false;
			}
			if (FieldError.Count != messageValidationError.FieldError.Count)
			{
				return false;
			}
			for (int i = 0; i < FieldError.Count; i++)
			{
				if (!FieldError[i].Equals(messageValidationError.FieldError[i]))
				{
					return false;
				}
			}
			if (HasCustomError != messageValidationError.HasCustomError || (HasCustomError && !CustomError.Equals(messageValidationError.CustomError)))
			{
				return false;
			}
			return true;
		}

		public static MessageValidationError ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MessageValidationError>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static MessageValidationError Deserialize(Stream stream, MessageValidationError instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static MessageValidationError DeserializeLengthDelimited(Stream stream)
		{
			MessageValidationError messageValidationError = new MessageValidationError();
			DeserializeLengthDelimited(stream, messageValidationError);
			return messageValidationError;
		}

		public static MessageValidationError DeserializeLengthDelimited(Stream stream, MessageValidationError instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static MessageValidationError Deserialize(Stream stream, MessageValidationError instance, long limit)
		{
			if (instance.FieldError == null)
			{
				instance.FieldError = new List<FieldValidationError>();
			}
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
					instance.FieldError.Add(FieldValidationError.DeserializeLengthDelimited(stream));
					continue;
				case 18:
					instance.CustomError = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, MessageValidationError instance)
		{
			if (instance.FieldError.Count > 0)
			{
				foreach (FieldValidationError item in instance.FieldError)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					FieldValidationError.Serialize(stream, item);
				}
			}
			if (instance.HasCustomError)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CustomError));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (FieldError.Count > 0)
			{
				foreach (FieldValidationError item in FieldError)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasCustomError)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(CustomError);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
