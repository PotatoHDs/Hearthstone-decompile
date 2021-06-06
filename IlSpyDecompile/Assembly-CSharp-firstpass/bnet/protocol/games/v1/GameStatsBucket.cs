using System.IO;

namespace bnet.protocol.games.v1
{
	public class GameStatsBucket : IProtoBuf
	{
		public bool HasBucketMin;

		private float _BucketMin;

		public bool HasBucketMax;

		private float _BucketMax;

		public bool HasWaitMilliseconds;

		private uint _WaitMilliseconds;

		public bool HasGamesPerHour;

		private uint _GamesPerHour;

		public bool HasActiveGames;

		private uint _ActiveGames;

		public bool HasActivePlayers;

		private uint _ActivePlayers;

		public bool HasFormingGames;

		private uint _FormingGames;

		public bool HasWaitingPlayers;

		private uint _WaitingPlayers;

		public bool HasOpenJoinableGames;

		private uint _OpenJoinableGames;

		public bool HasPlayersInOpenJoinableGames;

		private uint _PlayersInOpenJoinableGames;

		public bool HasOpenGamesTotal;

		private uint _OpenGamesTotal;

		public bool HasPlayersInOpenGamesTotal;

		private uint _PlayersInOpenGamesTotal;

		public float BucketMin
		{
			get
			{
				return _BucketMin;
			}
			set
			{
				_BucketMin = value;
				HasBucketMin = true;
			}
		}

		public float BucketMax
		{
			get
			{
				return _BucketMax;
			}
			set
			{
				_BucketMax = value;
				HasBucketMax = true;
			}
		}

		public uint WaitMilliseconds
		{
			get
			{
				return _WaitMilliseconds;
			}
			set
			{
				_WaitMilliseconds = value;
				HasWaitMilliseconds = true;
			}
		}

		public uint GamesPerHour
		{
			get
			{
				return _GamesPerHour;
			}
			set
			{
				_GamesPerHour = value;
				HasGamesPerHour = true;
			}
		}

		public uint ActiveGames
		{
			get
			{
				return _ActiveGames;
			}
			set
			{
				_ActiveGames = value;
				HasActiveGames = true;
			}
		}

		public uint ActivePlayers
		{
			get
			{
				return _ActivePlayers;
			}
			set
			{
				_ActivePlayers = value;
				HasActivePlayers = true;
			}
		}

		public uint FormingGames
		{
			get
			{
				return _FormingGames;
			}
			set
			{
				_FormingGames = value;
				HasFormingGames = true;
			}
		}

		public uint WaitingPlayers
		{
			get
			{
				return _WaitingPlayers;
			}
			set
			{
				_WaitingPlayers = value;
				HasWaitingPlayers = true;
			}
		}

		public uint OpenJoinableGames
		{
			get
			{
				return _OpenJoinableGames;
			}
			set
			{
				_OpenJoinableGames = value;
				HasOpenJoinableGames = true;
			}
		}

		public uint PlayersInOpenJoinableGames
		{
			get
			{
				return _PlayersInOpenJoinableGames;
			}
			set
			{
				_PlayersInOpenJoinableGames = value;
				HasPlayersInOpenJoinableGames = true;
			}
		}

		public uint OpenGamesTotal
		{
			get
			{
				return _OpenGamesTotal;
			}
			set
			{
				_OpenGamesTotal = value;
				HasOpenGamesTotal = true;
			}
		}

		public uint PlayersInOpenGamesTotal
		{
			get
			{
				return _PlayersInOpenGamesTotal;
			}
			set
			{
				_PlayersInOpenGamesTotal = value;
				HasPlayersInOpenGamesTotal = true;
			}
		}

		public bool IsInitialized => true;

		public void SetBucketMin(float val)
		{
			BucketMin = val;
		}

		public void SetBucketMax(float val)
		{
			BucketMax = val;
		}

		public void SetWaitMilliseconds(uint val)
		{
			WaitMilliseconds = val;
		}

		public void SetGamesPerHour(uint val)
		{
			GamesPerHour = val;
		}

		public void SetActiveGames(uint val)
		{
			ActiveGames = val;
		}

		public void SetActivePlayers(uint val)
		{
			ActivePlayers = val;
		}

		public void SetFormingGames(uint val)
		{
			FormingGames = val;
		}

		public void SetWaitingPlayers(uint val)
		{
			WaitingPlayers = val;
		}

		public void SetOpenJoinableGames(uint val)
		{
			OpenJoinableGames = val;
		}

		public void SetPlayersInOpenJoinableGames(uint val)
		{
			PlayersInOpenJoinableGames = val;
		}

		public void SetOpenGamesTotal(uint val)
		{
			OpenGamesTotal = val;
		}

		public void SetPlayersInOpenGamesTotal(uint val)
		{
			PlayersInOpenGamesTotal = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasBucketMin)
			{
				num ^= BucketMin.GetHashCode();
			}
			if (HasBucketMax)
			{
				num ^= BucketMax.GetHashCode();
			}
			if (HasWaitMilliseconds)
			{
				num ^= WaitMilliseconds.GetHashCode();
			}
			if (HasGamesPerHour)
			{
				num ^= GamesPerHour.GetHashCode();
			}
			if (HasActiveGames)
			{
				num ^= ActiveGames.GetHashCode();
			}
			if (HasActivePlayers)
			{
				num ^= ActivePlayers.GetHashCode();
			}
			if (HasFormingGames)
			{
				num ^= FormingGames.GetHashCode();
			}
			if (HasWaitingPlayers)
			{
				num ^= WaitingPlayers.GetHashCode();
			}
			if (HasOpenJoinableGames)
			{
				num ^= OpenJoinableGames.GetHashCode();
			}
			if (HasPlayersInOpenJoinableGames)
			{
				num ^= PlayersInOpenJoinableGames.GetHashCode();
			}
			if (HasOpenGamesTotal)
			{
				num ^= OpenGamesTotal.GetHashCode();
			}
			if (HasPlayersInOpenGamesTotal)
			{
				num ^= PlayersInOpenGamesTotal.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameStatsBucket gameStatsBucket = obj as GameStatsBucket;
			if (gameStatsBucket == null)
			{
				return false;
			}
			if (HasBucketMin != gameStatsBucket.HasBucketMin || (HasBucketMin && !BucketMin.Equals(gameStatsBucket.BucketMin)))
			{
				return false;
			}
			if (HasBucketMax != gameStatsBucket.HasBucketMax || (HasBucketMax && !BucketMax.Equals(gameStatsBucket.BucketMax)))
			{
				return false;
			}
			if (HasWaitMilliseconds != gameStatsBucket.HasWaitMilliseconds || (HasWaitMilliseconds && !WaitMilliseconds.Equals(gameStatsBucket.WaitMilliseconds)))
			{
				return false;
			}
			if (HasGamesPerHour != gameStatsBucket.HasGamesPerHour || (HasGamesPerHour && !GamesPerHour.Equals(gameStatsBucket.GamesPerHour)))
			{
				return false;
			}
			if (HasActiveGames != gameStatsBucket.HasActiveGames || (HasActiveGames && !ActiveGames.Equals(gameStatsBucket.ActiveGames)))
			{
				return false;
			}
			if (HasActivePlayers != gameStatsBucket.HasActivePlayers || (HasActivePlayers && !ActivePlayers.Equals(gameStatsBucket.ActivePlayers)))
			{
				return false;
			}
			if (HasFormingGames != gameStatsBucket.HasFormingGames || (HasFormingGames && !FormingGames.Equals(gameStatsBucket.FormingGames)))
			{
				return false;
			}
			if (HasWaitingPlayers != gameStatsBucket.HasWaitingPlayers || (HasWaitingPlayers && !WaitingPlayers.Equals(gameStatsBucket.WaitingPlayers)))
			{
				return false;
			}
			if (HasOpenJoinableGames != gameStatsBucket.HasOpenJoinableGames || (HasOpenJoinableGames && !OpenJoinableGames.Equals(gameStatsBucket.OpenJoinableGames)))
			{
				return false;
			}
			if (HasPlayersInOpenJoinableGames != gameStatsBucket.HasPlayersInOpenJoinableGames || (HasPlayersInOpenJoinableGames && !PlayersInOpenJoinableGames.Equals(gameStatsBucket.PlayersInOpenJoinableGames)))
			{
				return false;
			}
			if (HasOpenGamesTotal != gameStatsBucket.HasOpenGamesTotal || (HasOpenGamesTotal && !OpenGamesTotal.Equals(gameStatsBucket.OpenGamesTotal)))
			{
				return false;
			}
			if (HasPlayersInOpenGamesTotal != gameStatsBucket.HasPlayersInOpenGamesTotal || (HasPlayersInOpenGamesTotal && !PlayersInOpenGamesTotal.Equals(gameStatsBucket.PlayersInOpenGamesTotal)))
			{
				return false;
			}
			return true;
		}

		public static GameStatsBucket ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameStatsBucket>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameStatsBucket Deserialize(Stream stream, GameStatsBucket instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameStatsBucket DeserializeLengthDelimited(Stream stream)
		{
			GameStatsBucket gameStatsBucket = new GameStatsBucket();
			DeserializeLengthDelimited(stream, gameStatsBucket);
			return gameStatsBucket;
		}

		public static GameStatsBucket DeserializeLengthDelimited(Stream stream, GameStatsBucket instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameStatsBucket Deserialize(Stream stream, GameStatsBucket instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.BucketMin = 0f;
			instance.BucketMax = 4.2949673E+09f;
			instance.WaitMilliseconds = 0u;
			instance.GamesPerHour = 0u;
			instance.ActiveGames = 0u;
			instance.ActivePlayers = 0u;
			instance.FormingGames = 0u;
			instance.WaitingPlayers = 0u;
			instance.OpenJoinableGames = 0u;
			instance.PlayersInOpenJoinableGames = 0u;
			instance.OpenGamesTotal = 0u;
			instance.PlayersInOpenGamesTotal = 0u;
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
				case 13:
					instance.BucketMin = binaryReader.ReadSingle();
					continue;
				case 21:
					instance.BucketMax = binaryReader.ReadSingle();
					continue;
				case 24:
					instance.WaitMilliseconds = ProtocolParser.ReadUInt32(stream);
					continue;
				case 32:
					instance.GamesPerHour = ProtocolParser.ReadUInt32(stream);
					continue;
				case 40:
					instance.ActiveGames = ProtocolParser.ReadUInt32(stream);
					continue;
				case 48:
					instance.ActivePlayers = ProtocolParser.ReadUInt32(stream);
					continue;
				case 56:
					instance.FormingGames = ProtocolParser.ReadUInt32(stream);
					continue;
				case 64:
					instance.WaitingPlayers = ProtocolParser.ReadUInt32(stream);
					continue;
				case 72:
					instance.OpenJoinableGames = ProtocolParser.ReadUInt32(stream);
					continue;
				case 80:
					instance.PlayersInOpenJoinableGames = ProtocolParser.ReadUInt32(stream);
					continue;
				case 88:
					instance.OpenGamesTotal = ProtocolParser.ReadUInt32(stream);
					continue;
				case 96:
					instance.PlayersInOpenGamesTotal = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, GameStatsBucket instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasBucketMin)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.BucketMin);
			}
			if (instance.HasBucketMax)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.BucketMax);
			}
			if (instance.HasWaitMilliseconds)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.WaitMilliseconds);
			}
			if (instance.HasGamesPerHour)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.GamesPerHour);
			}
			if (instance.HasActiveGames)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt32(stream, instance.ActiveGames);
			}
			if (instance.HasActivePlayers)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt32(stream, instance.ActivePlayers);
			}
			if (instance.HasFormingGames)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt32(stream, instance.FormingGames);
			}
			if (instance.HasWaitingPlayers)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt32(stream, instance.WaitingPlayers);
			}
			if (instance.HasOpenJoinableGames)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt32(stream, instance.OpenJoinableGames);
			}
			if (instance.HasPlayersInOpenJoinableGames)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt32(stream, instance.PlayersInOpenJoinableGames);
			}
			if (instance.HasOpenGamesTotal)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteUInt32(stream, instance.OpenGamesTotal);
			}
			if (instance.HasPlayersInOpenGamesTotal)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt32(stream, instance.PlayersInOpenGamesTotal);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasBucketMin)
			{
				num++;
				num += 4;
			}
			if (HasBucketMax)
			{
				num++;
				num += 4;
			}
			if (HasWaitMilliseconds)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(WaitMilliseconds);
			}
			if (HasGamesPerHour)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(GamesPerHour);
			}
			if (HasActiveGames)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(ActiveGames);
			}
			if (HasActivePlayers)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(ActivePlayers);
			}
			if (HasFormingGames)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(FormingGames);
			}
			if (HasWaitingPlayers)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(WaitingPlayers);
			}
			if (HasOpenJoinableGames)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(OpenJoinableGames);
			}
			if (HasPlayersInOpenJoinableGames)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(PlayersInOpenJoinableGames);
			}
			if (HasOpenGamesTotal)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(OpenGamesTotal);
			}
			if (HasPlayersInOpenGamesTotal)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(PlayersInOpenGamesTotal);
			}
			return num;
		}
	}
}
