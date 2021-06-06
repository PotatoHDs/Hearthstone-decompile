using System.IO;

namespace PegasusUtil
{
	public class GetServerTimeRequest : IProtoBuf
	{
		public enum PacketID
		{
			ID = 364,
			System = 0
		}

		public bool HasClientUnixTime;

		private long _ClientUnixTime;

		public long ClientUnixTime
		{
			get
			{
				return _ClientUnixTime;
			}
			set
			{
				_ClientUnixTime = value;
				HasClientUnixTime = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasClientUnixTime)
			{
				num ^= ClientUnixTime.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetServerTimeRequest getServerTimeRequest = obj as GetServerTimeRequest;
			if (getServerTimeRequest == null)
			{
				return false;
			}
			if (HasClientUnixTime != getServerTimeRequest.HasClientUnixTime || (HasClientUnixTime && !ClientUnixTime.Equals(getServerTimeRequest.ClientUnixTime)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetServerTimeRequest Deserialize(Stream stream, GetServerTimeRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetServerTimeRequest DeserializeLengthDelimited(Stream stream)
		{
			GetServerTimeRequest getServerTimeRequest = new GetServerTimeRequest();
			DeserializeLengthDelimited(stream, getServerTimeRequest);
			return getServerTimeRequest;
		}

		public static GetServerTimeRequest DeserializeLengthDelimited(Stream stream, GetServerTimeRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetServerTimeRequest Deserialize(Stream stream, GetServerTimeRequest instance, long limit)
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
					instance.ClientUnixTime = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, GetServerTimeRequest instance)
		{
			if (instance.HasClientUnixTime)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ClientUnixTime);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasClientUnixTime)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ClientUnixTime);
			}
			return num;
		}
	}
}
