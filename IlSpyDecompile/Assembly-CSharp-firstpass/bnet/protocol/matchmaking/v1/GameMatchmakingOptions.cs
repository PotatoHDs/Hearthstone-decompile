using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class GameMatchmakingOptions : IProtoBuf
	{
		public bool HasMatchmakerFilter;

		private GameMatchmakerFilter _MatchmakerFilter;

		public bool HasCreationProperties;

		private GameCreationProperties _CreationProperties;

		private List<Player> _Player = new List<Player>();

		public GameMatchmakerFilter MatchmakerFilter
		{
			get
			{
				return _MatchmakerFilter;
			}
			set
			{
				_MatchmakerFilter = value;
				HasMatchmakerFilter = value != null;
			}
		}

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

		public bool IsInitialized => true;

		public void SetMatchmakerFilter(GameMatchmakerFilter val)
		{
			MatchmakerFilter = val;
		}

		public void SetCreationProperties(GameCreationProperties val)
		{
			CreationProperties = val;
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

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasMatchmakerFilter)
			{
				num ^= MatchmakerFilter.GetHashCode();
			}
			if (HasCreationProperties)
			{
				num ^= CreationProperties.GetHashCode();
			}
			foreach (Player item in Player)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameMatchmakingOptions gameMatchmakingOptions = obj as GameMatchmakingOptions;
			if (gameMatchmakingOptions == null)
			{
				return false;
			}
			if (HasMatchmakerFilter != gameMatchmakingOptions.HasMatchmakerFilter || (HasMatchmakerFilter && !MatchmakerFilter.Equals(gameMatchmakingOptions.MatchmakerFilter)))
			{
				return false;
			}
			if (HasCreationProperties != gameMatchmakingOptions.HasCreationProperties || (HasCreationProperties && !CreationProperties.Equals(gameMatchmakingOptions.CreationProperties)))
			{
				return false;
			}
			if (Player.Count != gameMatchmakingOptions.Player.Count)
			{
				return false;
			}
			for (int i = 0; i < Player.Count; i++)
			{
				if (!Player[i].Equals(gameMatchmakingOptions.Player[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static GameMatchmakingOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameMatchmakingOptions>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameMatchmakingOptions Deserialize(Stream stream, GameMatchmakingOptions instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameMatchmakingOptions DeserializeLengthDelimited(Stream stream)
		{
			GameMatchmakingOptions gameMatchmakingOptions = new GameMatchmakingOptions();
			DeserializeLengthDelimited(stream, gameMatchmakingOptions);
			return gameMatchmakingOptions;
		}

		public static GameMatchmakingOptions DeserializeLengthDelimited(Stream stream, GameMatchmakingOptions instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameMatchmakingOptions Deserialize(Stream stream, GameMatchmakingOptions instance, long limit)
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
					if (instance.MatchmakerFilter == null)
					{
						instance.MatchmakerFilter = GameMatchmakerFilter.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameMatchmakerFilter.DeserializeLengthDelimited(stream, instance.MatchmakerFilter);
					}
					continue;
				case 18:
					if (instance.CreationProperties == null)
					{
						instance.CreationProperties = GameCreationProperties.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameCreationProperties.DeserializeLengthDelimited(stream, instance.CreationProperties);
					}
					continue;
				case 26:
					instance.Player.Add(bnet.protocol.matchmaking.v1.Player.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, GameMatchmakingOptions instance)
		{
			if (instance.HasMatchmakerFilter)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.MatchmakerFilter.GetSerializedSize());
				GameMatchmakerFilter.Serialize(stream, instance.MatchmakerFilter);
			}
			if (instance.HasCreationProperties)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.CreationProperties.GetSerializedSize());
				GameCreationProperties.Serialize(stream, instance.CreationProperties);
			}
			if (instance.Player.Count <= 0)
			{
				return;
			}
			foreach (Player item in instance.Player)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				bnet.protocol.matchmaking.v1.Player.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasMatchmakerFilter)
			{
				num++;
				uint serializedSize = MatchmakerFilter.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasCreationProperties)
			{
				num++;
				uint serializedSize2 = CreationProperties.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (Player.Count > 0)
			{
				foreach (Player item in Player)
				{
					num++;
					uint serializedSize3 = item.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
				return num;
			}
			return num;
		}
	}
}
