using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class CreateGameRequest : IProtoBuf
	{
		public bool HasGameHandle;

		private GameHandle _GameHandle;

		private List<Player> _Player = new List<Player>();

		public bool HasCreationProperties;

		private GameCreationProperties _CreationProperties;

		public GameHandle GameHandle
		{
			get
			{
				return _GameHandle;
			}
			set
			{
				_GameHandle = value;
				HasGameHandle = value != null;
			}
		}

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

		public GameCreationProperties CreationProperties
		{
			get
			{
				return _CreationProperties;
			}
			set
			{
				_CreationProperties = value;
				HasCreationProperties = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetGameHandle(GameHandle val)
		{
			GameHandle = val;
		}

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

		public void SetCreationProperties(GameCreationProperties val)
		{
			CreationProperties = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasGameHandle)
			{
				num ^= GameHandle.GetHashCode();
			}
			foreach (Player item in Player)
			{
				num ^= item.GetHashCode();
			}
			if (HasCreationProperties)
			{
				num ^= CreationProperties.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CreateGameRequest createGameRequest = obj as CreateGameRequest;
			if (createGameRequest == null)
			{
				return false;
			}
			if (HasGameHandle != createGameRequest.HasGameHandle || (HasGameHandle && !GameHandle.Equals(createGameRequest.GameHandle)))
			{
				return false;
			}
			if (Player.Count != createGameRequest.Player.Count)
			{
				return false;
			}
			for (int i = 0; i < Player.Count; i++)
			{
				if (!Player[i].Equals(createGameRequest.Player[i]))
				{
					return false;
				}
			}
			if (HasCreationProperties != createGameRequest.HasCreationProperties || (HasCreationProperties && !CreationProperties.Equals(createGameRequest.CreationProperties)))
			{
				return false;
			}
			return true;
		}

		public static CreateGameRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateGameRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CreateGameRequest Deserialize(Stream stream, CreateGameRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CreateGameRequest DeserializeLengthDelimited(Stream stream)
		{
			CreateGameRequest createGameRequest = new CreateGameRequest();
			DeserializeLengthDelimited(stream, createGameRequest);
			return createGameRequest;
		}

		public static CreateGameRequest DeserializeLengthDelimited(Stream stream, CreateGameRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CreateGameRequest Deserialize(Stream stream, CreateGameRequest instance, long limit)
		{
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
					if (instance.GameHandle == null)
					{
						instance.GameHandle = GameHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
					}
					continue;
				case 18:
					instance.Player.Add(bnet.protocol.matchmaking.v1.Player.DeserializeLengthDelimited(stream));
					continue;
				case 26:
					if (instance.CreationProperties == null)
					{
						instance.CreationProperties = GameCreationProperties.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameCreationProperties.DeserializeLengthDelimited(stream, instance.CreationProperties);
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

		public static void Serialize(Stream stream, CreateGameRequest instance)
		{
			if (instance.HasGameHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				GameHandle.Serialize(stream, instance.GameHandle);
			}
			if (instance.Player.Count > 0)
			{
				foreach (Player item in instance.Player)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.matchmaking.v1.Player.Serialize(stream, item);
				}
			}
			if (instance.HasCreationProperties)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.CreationProperties.GetSerializedSize());
				GameCreationProperties.Serialize(stream, instance.CreationProperties);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasGameHandle)
			{
				num++;
				uint serializedSize = GameHandle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (Player.Count > 0)
			{
				foreach (Player item in Player)
				{
					num++;
					uint serializedSize2 = item.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (HasCreationProperties)
			{
				num++;
				uint serializedSize3 = CreationProperties.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}
	}
}
