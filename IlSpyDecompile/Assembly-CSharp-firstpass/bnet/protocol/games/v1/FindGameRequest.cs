using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class FindGameRequest : IProtoBuf
	{
		private List<Player> _Player = new List<Player>();

		public bool HasFactoryId;

		private ulong _FactoryId;

		public bool HasProperties;

		private GameProperties _Properties;

		public bool HasRequestId;

		private ulong _RequestId;

		public List<Player> Player
		{
			get
			{
				return _Player;
			}
			set
			{
				_Player = value;
			}
		}

		public List<Player> PlayerList => _Player;

		public int PlayerCount => _Player.Count;

		public ulong FactoryId
		{
			get
			{
				return _FactoryId;
			}
			set
			{
				_FactoryId = value;
				HasFactoryId = true;
			}
		}

		public GameProperties Properties
		{
			get
			{
				return _Properties;
			}
			set
			{
				_Properties = value;
				HasProperties = value != null;
			}
		}

		public ulong RequestId
		{
			get
			{
				return _RequestId;
			}
			set
			{
				_RequestId = value;
				HasRequestId = true;
			}
		}

		public bool IsInitialized => true;

		public void AddPlayer(Player val)
		{
			_Player.Add(val);
		}

		public void ClearPlayer()
		{
			_Player.Clear();
		}

		public void SetPlayer(List<Player> val)
		{
			Player = val;
		}

		public void SetFactoryId(ulong val)
		{
			FactoryId = val;
		}

		public void SetProperties(GameProperties val)
		{
			Properties = val;
		}

		public void SetRequestId(ulong val)
		{
			RequestId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (Player item in Player)
			{
				num ^= item.GetHashCode();
			}
			if (HasFactoryId)
			{
				num ^= FactoryId.GetHashCode();
			}
			if (HasProperties)
			{
				num ^= Properties.GetHashCode();
			}
			if (HasRequestId)
			{
				num ^= RequestId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FindGameRequest findGameRequest = obj as FindGameRequest;
			if (findGameRequest == null)
			{
				return false;
			}
			if (Player.Count != findGameRequest.Player.Count)
			{
				return false;
			}
			for (int i = 0; i < Player.Count; i++)
			{
				if (!Player[i].Equals(findGameRequest.Player[i]))
				{
					return false;
				}
			}
			if (HasFactoryId != findGameRequest.HasFactoryId || (HasFactoryId && !FactoryId.Equals(findGameRequest.FactoryId)))
			{
				return false;
			}
			if (HasProperties != findGameRequest.HasProperties || (HasProperties && !Properties.Equals(findGameRequest.Properties)))
			{
				return false;
			}
			if (HasRequestId != findGameRequest.HasRequestId || (HasRequestId && !RequestId.Equals(findGameRequest.RequestId)))
			{
				return false;
			}
			return true;
		}

		public static FindGameRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FindGameRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FindGameRequest Deserialize(Stream stream, FindGameRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FindGameRequest DeserializeLengthDelimited(Stream stream)
		{
			FindGameRequest findGameRequest = new FindGameRequest();
			DeserializeLengthDelimited(stream, findGameRequest);
			return findGameRequest;
		}

		public static FindGameRequest DeserializeLengthDelimited(Stream stream, FindGameRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FindGameRequest Deserialize(Stream stream, FindGameRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Player == null)
			{
				instance.Player = new List<Player>();
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
					instance.Player.Add(bnet.protocol.games.v1.Player.DeserializeLengthDelimited(stream));
					continue;
				case 17:
					instance.FactoryId = binaryReader.ReadUInt64();
					continue;
				case 26:
					if (instance.Properties == null)
					{
						instance.Properties = GameProperties.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameProperties.DeserializeLengthDelimited(stream, instance.Properties);
					}
					continue;
				case 41:
					instance.RequestId = binaryReader.ReadUInt64();
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

		public static void Serialize(Stream stream, FindGameRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.Player.Count > 0)
			{
				foreach (Player item in instance.Player)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.games.v1.Player.Serialize(stream, item);
				}
			}
			if (instance.HasFactoryId)
			{
				stream.WriteByte(17);
				binaryWriter.Write(instance.FactoryId);
			}
			if (instance.HasProperties)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Properties.GetSerializedSize());
				GameProperties.Serialize(stream, instance.Properties);
			}
			if (instance.HasRequestId)
			{
				stream.WriteByte(41);
				binaryWriter.Write(instance.RequestId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Player.Count > 0)
			{
				foreach (Player item in Player)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasFactoryId)
			{
				num++;
				num += 8;
			}
			if (HasProperties)
			{
				num++;
				uint serializedSize2 = Properties.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasRequestId)
			{
				num++;
				num += 8;
			}
			return num;
		}
	}
}
