using System.IO;

namespace bnet.protocol.broadcast.v1
{
	public class StartBroadcastResponse : IProtoBuf
	{
		public bool HasConnectedClients;

		private int _ConnectedClients;

		public bool HasFilteredClients;

		private int _FilteredClients;

		public int ConnectedClients
		{
			get
			{
				return _ConnectedClients;
			}
			set
			{
				_ConnectedClients = value;
				HasConnectedClients = true;
			}
		}

		public int FilteredClients
		{
			get
			{
				return _FilteredClients;
			}
			set
			{
				_FilteredClients = value;
				HasFilteredClients = true;
			}
		}

		public bool IsInitialized => true;

		public void SetConnectedClients(int val)
		{
			ConnectedClients = val;
		}

		public void SetFilteredClients(int val)
		{
			FilteredClients = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasConnectedClients)
			{
				num ^= ConnectedClients.GetHashCode();
			}
			if (HasFilteredClients)
			{
				num ^= FilteredClients.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			StartBroadcastResponse startBroadcastResponse = obj as StartBroadcastResponse;
			if (startBroadcastResponse == null)
			{
				return false;
			}
			if (HasConnectedClients != startBroadcastResponse.HasConnectedClients || (HasConnectedClients && !ConnectedClients.Equals(startBroadcastResponse.ConnectedClients)))
			{
				return false;
			}
			if (HasFilteredClients != startBroadcastResponse.HasFilteredClients || (HasFilteredClients && !FilteredClients.Equals(startBroadcastResponse.FilteredClients)))
			{
				return false;
			}
			return true;
		}

		public static StartBroadcastResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<StartBroadcastResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static StartBroadcastResponse Deserialize(Stream stream, StartBroadcastResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static StartBroadcastResponse DeserializeLengthDelimited(Stream stream)
		{
			StartBroadcastResponse startBroadcastResponse = new StartBroadcastResponse();
			DeserializeLengthDelimited(stream, startBroadcastResponse);
			return startBroadcastResponse;
		}

		public static StartBroadcastResponse DeserializeLengthDelimited(Stream stream, StartBroadcastResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static StartBroadcastResponse Deserialize(Stream stream, StartBroadcastResponse instance, long limit)
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
					instance.ConnectedClients = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.FilteredClients = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, StartBroadcastResponse instance)
		{
			if (instance.HasConnectedClients)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ConnectedClients);
			}
			if (instance.HasFilteredClients)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FilteredClients);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasConnectedClients)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ConnectedClients);
			}
			if (HasFilteredClients)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)FilteredClients);
			}
			return num;
		}
	}
}
