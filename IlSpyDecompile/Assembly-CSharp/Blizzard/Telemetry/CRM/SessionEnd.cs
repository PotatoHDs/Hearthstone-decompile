using System.IO;
using System.Text;

namespace Blizzard.Telemetry.CRM
{
	public class SessionEnd : IProtoBuf
	{
		public bool HasApplicationId;

		private string _ApplicationId;

		public string ApplicationId
		{
			get
			{
				return _ApplicationId;
			}
			set
			{
				_ApplicationId = value;
				HasApplicationId = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasApplicationId)
			{
				num ^= ApplicationId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SessionEnd sessionEnd = obj as SessionEnd;
			if (sessionEnd == null)
			{
				return false;
			}
			if (HasApplicationId != sessionEnd.HasApplicationId || (HasApplicationId && !ApplicationId.Equals(sessionEnd.ApplicationId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SessionEnd Deserialize(Stream stream, SessionEnd instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SessionEnd DeserializeLengthDelimited(Stream stream)
		{
			SessionEnd sessionEnd = new SessionEnd();
			DeserializeLengthDelimited(stream, sessionEnd);
			return sessionEnd;
		}

		public static SessionEnd DeserializeLengthDelimited(Stream stream, SessionEnd instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SessionEnd Deserialize(Stream stream, SessionEnd instance, long limit)
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
				if (num == -1)
				{
					if (limit < 0)
					{
						break;
					}
					throw new EndOfStreamException();
				}
				Key key = ProtocolParser.ReadKey((byte)num, stream);
				switch (key.Field)
				{
				case 0u:
					throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
				case 30u:
					if (key.WireType == Wire.LengthDelimited)
					{
						instance.ApplicationId = ProtocolParser.ReadString(stream);
					}
					break;
				default:
					ProtocolParser.SkipKey(stream, key);
					break;
				}
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, SessionEnd instance)
		{
			if (instance.HasApplicationId)
			{
				stream.WriteByte(242);
				stream.WriteByte(1);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ApplicationId));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasApplicationId)
			{
				num += 2;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ApplicationId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
