using System.IO;
using System.Text;

namespace bnet.protocol.connection.v1
{
	public class DisconnectNotification : IProtoBuf
	{
		public bool HasReason;

		private string _Reason;

		public uint ErrorCode { get; set; }

		public string Reason
		{
			get
			{
				return _Reason;
			}
			set
			{
				_Reason = value;
				HasReason = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetErrorCode(uint val)
		{
			ErrorCode = val;
		}

		public void SetReason(string val)
		{
			Reason = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= ErrorCode.GetHashCode();
			if (HasReason)
			{
				hashCode ^= Reason.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			DisconnectNotification disconnectNotification = obj as DisconnectNotification;
			if (disconnectNotification == null)
			{
				return false;
			}
			if (!ErrorCode.Equals(disconnectNotification.ErrorCode))
			{
				return false;
			}
			if (HasReason != disconnectNotification.HasReason || (HasReason && !Reason.Equals(disconnectNotification.Reason)))
			{
				return false;
			}
			return true;
		}

		public static DisconnectNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<DisconnectNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DisconnectNotification Deserialize(Stream stream, DisconnectNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DisconnectNotification DeserializeLengthDelimited(Stream stream)
		{
			DisconnectNotification disconnectNotification = new DisconnectNotification();
			DeserializeLengthDelimited(stream, disconnectNotification);
			return disconnectNotification;
		}

		public static DisconnectNotification DeserializeLengthDelimited(Stream stream, DisconnectNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DisconnectNotification Deserialize(Stream stream, DisconnectNotification instance, long limit)
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
					instance.ErrorCode = ProtocolParser.ReadUInt32(stream);
					continue;
				case 18:
					instance.Reason = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, DisconnectNotification instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.ErrorCode);
			if (instance.HasReason)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Reason));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt32(ErrorCode);
			if (HasReason)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Reason);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num + 1;
		}
	}
}
