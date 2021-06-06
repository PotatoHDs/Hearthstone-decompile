using System.IO;

namespace bnet.protocol.games.v1
{
	public class HostRoute : IProtoBuf
	{
		public bool HasHost;

		private ProcessId _Host;

		public bool HasProxy;

		private ProcessId _Proxy;

		public ProcessId Host
		{
			get
			{
				return _Host;
			}
			set
			{
				_Host = value;
				HasHost = value != null;
			}
		}

		public ProcessId Proxy
		{
			get
			{
				return _Proxy;
			}
			set
			{
				_Proxy = value;
				HasProxy = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetHost(ProcessId val)
		{
			Host = val;
		}

		public void SetProxy(ProcessId val)
		{
			Proxy = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasHost)
			{
				num ^= Host.GetHashCode();
			}
			if (HasProxy)
			{
				num ^= Proxy.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			HostRoute hostRoute = obj as HostRoute;
			if (hostRoute == null)
			{
				return false;
			}
			if (HasHost != hostRoute.HasHost || (HasHost && !Host.Equals(hostRoute.Host)))
			{
				return false;
			}
			if (HasProxy != hostRoute.HasProxy || (HasProxy && !Proxy.Equals(hostRoute.Proxy)))
			{
				return false;
			}
			return true;
		}

		public static HostRoute ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<HostRoute>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static HostRoute Deserialize(Stream stream, HostRoute instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static HostRoute DeserializeLengthDelimited(Stream stream)
		{
			HostRoute hostRoute = new HostRoute();
			DeserializeLengthDelimited(stream, hostRoute);
			return hostRoute;
		}

		public static HostRoute DeserializeLengthDelimited(Stream stream, HostRoute instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static HostRoute Deserialize(Stream stream, HostRoute instance, long limit)
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
					if (instance.Host == null)
					{
						instance.Host = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.Host);
					}
					continue;
				case 18:
					if (instance.Proxy == null)
					{
						instance.Proxy = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.Proxy);
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

		public static void Serialize(Stream stream, HostRoute instance)
		{
			if (instance.HasHost)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
			if (instance.HasProxy)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Proxy.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Proxy);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasHost)
			{
				num++;
				uint serializedSize = Host.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasProxy)
			{
				num++;
				uint serializedSize2 = Proxy.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
