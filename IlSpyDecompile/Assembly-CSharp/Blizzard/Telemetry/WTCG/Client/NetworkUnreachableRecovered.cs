using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class NetworkUnreachableRecovered : IProtoBuf
	{
		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasOutageSeconds;

		private int _OutageSeconds;

		public DeviceInfo DeviceInfo
		{
			get
			{
				return _DeviceInfo;
			}
			set
			{
				_DeviceInfo = value;
				HasDeviceInfo = value != null;
			}
		}

		public int OutageSeconds
		{
			get
			{
				return _OutageSeconds;
			}
			set
			{
				_OutageSeconds = value;
				HasOutageSeconds = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasDeviceInfo)
			{
				num ^= DeviceInfo.GetHashCode();
			}
			if (HasOutageSeconds)
			{
				num ^= OutageSeconds.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			NetworkUnreachableRecovered networkUnreachableRecovered = obj as NetworkUnreachableRecovered;
			if (networkUnreachableRecovered == null)
			{
				return false;
			}
			if (HasDeviceInfo != networkUnreachableRecovered.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(networkUnreachableRecovered.DeviceInfo)))
			{
				return false;
			}
			if (HasOutageSeconds != networkUnreachableRecovered.HasOutageSeconds || (HasOutageSeconds && !OutageSeconds.Equals(networkUnreachableRecovered.OutageSeconds)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static NetworkUnreachableRecovered Deserialize(Stream stream, NetworkUnreachableRecovered instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static NetworkUnreachableRecovered DeserializeLengthDelimited(Stream stream)
		{
			NetworkUnreachableRecovered networkUnreachableRecovered = new NetworkUnreachableRecovered();
			DeserializeLengthDelimited(stream, networkUnreachableRecovered);
			return networkUnreachableRecovered;
		}

		public static NetworkUnreachableRecovered DeserializeLengthDelimited(Stream stream, NetworkUnreachableRecovered instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static NetworkUnreachableRecovered Deserialize(Stream stream, NetworkUnreachableRecovered instance, long limit)
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
					if (instance.DeviceInfo == null)
					{
						instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
					}
					continue;
				case 16:
					instance.OutageSeconds = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, NetworkUnreachableRecovered instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasOutageSeconds)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.OutageSeconds);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasDeviceInfo)
			{
				num++;
				uint serializedSize = DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasOutageSeconds)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)OutageSeconds);
			}
			return num;
		}
	}
}
