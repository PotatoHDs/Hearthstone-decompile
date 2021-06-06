using System.IO;

namespace PegasusUtil
{
	public class Subscribe : IProtoBuf
	{
		public enum PacketID
		{
			ID = 314
		}

		public bool HasFirstSubscribeForRoute;

		private bool _FirstSubscribeForRoute;

		public bool HasFirstSubscribe;

		private bool _FirstSubscribe;

		public bool HasUtilSystemId;

		private int _UtilSystemId;

		public bool FirstSubscribeForRoute
		{
			get
			{
				return _FirstSubscribeForRoute;
			}
			set
			{
				_FirstSubscribeForRoute = value;
				HasFirstSubscribeForRoute = true;
			}
		}

		public bool FirstSubscribe
		{
			get
			{
				return _FirstSubscribe;
			}
			set
			{
				_FirstSubscribe = value;
				HasFirstSubscribe = true;
			}
		}

		public int UtilSystemId
		{
			get
			{
				return _UtilSystemId;
			}
			set
			{
				_UtilSystemId = value;
				HasUtilSystemId = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasFirstSubscribeForRoute)
			{
				num ^= FirstSubscribeForRoute.GetHashCode();
			}
			if (HasFirstSubscribe)
			{
				num ^= FirstSubscribe.GetHashCode();
			}
			if (HasUtilSystemId)
			{
				num ^= UtilSystemId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Subscribe subscribe = obj as Subscribe;
			if (subscribe == null)
			{
				return false;
			}
			if (HasFirstSubscribeForRoute != subscribe.HasFirstSubscribeForRoute || (HasFirstSubscribeForRoute && !FirstSubscribeForRoute.Equals(subscribe.FirstSubscribeForRoute)))
			{
				return false;
			}
			if (HasFirstSubscribe != subscribe.HasFirstSubscribe || (HasFirstSubscribe && !FirstSubscribe.Equals(subscribe.FirstSubscribe)))
			{
				return false;
			}
			if (HasUtilSystemId != subscribe.HasUtilSystemId || (HasUtilSystemId && !UtilSystemId.Equals(subscribe.UtilSystemId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Subscribe Deserialize(Stream stream, Subscribe instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Subscribe DeserializeLengthDelimited(Stream stream)
		{
			Subscribe subscribe = new Subscribe();
			DeserializeLengthDelimited(stream, subscribe);
			return subscribe;
		}

		public static Subscribe DeserializeLengthDelimited(Stream stream, Subscribe instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Subscribe Deserialize(Stream stream, Subscribe instance, long limit)
		{
			instance.FirstSubscribeForRoute = false;
			instance.FirstSubscribe = false;
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
					instance.FirstSubscribeForRoute = ProtocolParser.ReadBool(stream);
					continue;
				case 16:
					instance.FirstSubscribe = ProtocolParser.ReadBool(stream);
					continue;
				case 24:
					instance.UtilSystemId = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, Subscribe instance)
		{
			if (instance.HasFirstSubscribeForRoute)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.FirstSubscribeForRoute);
			}
			if (instance.HasFirstSubscribe)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.FirstSubscribe);
			}
			if (instance.HasUtilSystemId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.UtilSystemId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasFirstSubscribeForRoute)
			{
				num++;
				num++;
			}
			if (HasFirstSubscribe)
			{
				num++;
				num++;
			}
			if (HasUtilSystemId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)UtilSystemId);
			}
			return num;
		}
	}
}
