using System;
using System.IO;

namespace bnet.protocol
{
	public class ErrorInfo : IProtoBuf
	{
		public ObjectAddress ObjectAddress { get; set; }

		public uint Status { get; set; }

		public uint ServiceHash { get; set; }

		public uint MethodId { get; set; }

		public bool IsInitialized => true;

		public void SetObjectAddress(ObjectAddress val)
		{
			ObjectAddress = val;
		}

		public void SetStatus(uint val)
		{
			Status = val;
		}

		public void SetServiceHash(uint val)
		{
			ServiceHash = val;
		}

		public void SetMethodId(uint val)
		{
			MethodId = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ ObjectAddress.GetHashCode() ^ Status.GetHashCode() ^ ServiceHash.GetHashCode() ^ MethodId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ErrorInfo errorInfo = obj as ErrorInfo;
			if (errorInfo == null)
			{
				return false;
			}
			if (!ObjectAddress.Equals(errorInfo.ObjectAddress))
			{
				return false;
			}
			if (!Status.Equals(errorInfo.Status))
			{
				return false;
			}
			if (!ServiceHash.Equals(errorInfo.ServiceHash))
			{
				return false;
			}
			if (!MethodId.Equals(errorInfo.MethodId))
			{
				return false;
			}
			return true;
		}

		public static ErrorInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ErrorInfo>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ErrorInfo Deserialize(Stream stream, ErrorInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ErrorInfo DeserializeLengthDelimited(Stream stream)
		{
			ErrorInfo errorInfo = new ErrorInfo();
			DeserializeLengthDelimited(stream, errorInfo);
			return errorInfo;
		}

		public static ErrorInfo DeserializeLengthDelimited(Stream stream, ErrorInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ErrorInfo Deserialize(Stream stream, ErrorInfo instance, long limit)
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
					if (instance.ObjectAddress == null)
					{
						instance.ObjectAddress = ObjectAddress.DeserializeLengthDelimited(stream);
					}
					else
					{
						ObjectAddress.DeserializeLengthDelimited(stream, instance.ObjectAddress);
					}
					continue;
				case 16:
					instance.Status = ProtocolParser.ReadUInt32(stream);
					continue;
				case 24:
					instance.ServiceHash = ProtocolParser.ReadUInt32(stream);
					continue;
				case 32:
					instance.MethodId = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, ErrorInfo instance)
		{
			if (instance.ObjectAddress == null)
			{
				throw new ArgumentNullException("ObjectAddress", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.ObjectAddress.GetSerializedSize());
			ObjectAddress.Serialize(stream, instance.ObjectAddress);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.Status);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt32(stream, instance.ServiceHash);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt32(stream, instance.MethodId);
		}

		public uint GetSerializedSize()
		{
			uint serializedSize = ObjectAddress.GetSerializedSize();
			return 0 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + ProtocolParser.SizeOfUInt32(Status) + ProtocolParser.SizeOfUInt32(ServiceHash) + ProtocolParser.SizeOfUInt32(MethodId) + 4;
		}
	}
}
