using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class GameServerRemovedNotification : IProtoBuf
	{
		public bool HasMatchmakerId;

		private uint _MatchmakerId;

		public bool HasGameServer;

		private HostProxyPair _GameServer;

		public bool HasMatchmakerGuid;

		private ulong _MatchmakerGuid;

		public bool HasGameServerGuid;

		private ulong _GameServerGuid;

		public uint MatchmakerId
		{
			get
			{
				return _MatchmakerId;
			}
			set
			{
				_MatchmakerId = value;
				HasMatchmakerId = true;
			}
		}

		public HostProxyPair GameServer
		{
			get
			{
				return _GameServer;
			}
			set
			{
				_GameServer = value;
				HasGameServer = value != null;
			}
		}

		public ulong MatchmakerGuid
		{
			get
			{
				return _MatchmakerGuid;
			}
			set
			{
				_MatchmakerGuid = value;
				HasMatchmakerGuid = true;
			}
		}

		public ulong GameServerGuid
		{
			get
			{
				return _GameServerGuid;
			}
			set
			{
				_GameServerGuid = value;
				HasGameServerGuid = true;
			}
		}

		public bool IsInitialized => true;

		public void SetMatchmakerId(uint val)
		{
			MatchmakerId = val;
		}

		public void SetGameServer(HostProxyPair val)
		{
			GameServer = val;
		}

		public void SetMatchmakerGuid(ulong val)
		{
			MatchmakerGuid = val;
		}

		public void SetGameServerGuid(ulong val)
		{
			GameServerGuid = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasMatchmakerId)
			{
				num ^= MatchmakerId.GetHashCode();
			}
			if (HasGameServer)
			{
				num ^= GameServer.GetHashCode();
			}
			if (HasMatchmakerGuid)
			{
				num ^= MatchmakerGuid.GetHashCode();
			}
			if (HasGameServerGuid)
			{
				num ^= GameServerGuid.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameServerRemovedNotification gameServerRemovedNotification = obj as GameServerRemovedNotification;
			if (gameServerRemovedNotification == null)
			{
				return false;
			}
			if (HasMatchmakerId != gameServerRemovedNotification.HasMatchmakerId || (HasMatchmakerId && !MatchmakerId.Equals(gameServerRemovedNotification.MatchmakerId)))
			{
				return false;
			}
			if (HasGameServer != gameServerRemovedNotification.HasGameServer || (HasGameServer && !GameServer.Equals(gameServerRemovedNotification.GameServer)))
			{
				return false;
			}
			if (HasMatchmakerGuid != gameServerRemovedNotification.HasMatchmakerGuid || (HasMatchmakerGuid && !MatchmakerGuid.Equals(gameServerRemovedNotification.MatchmakerGuid)))
			{
				return false;
			}
			if (HasGameServerGuid != gameServerRemovedNotification.HasGameServerGuid || (HasGameServerGuid && !GameServerGuid.Equals(gameServerRemovedNotification.GameServerGuid)))
			{
				return false;
			}
			return true;
		}

		public static GameServerRemovedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameServerRemovedNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameServerRemovedNotification Deserialize(Stream stream, GameServerRemovedNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameServerRemovedNotification DeserializeLengthDelimited(Stream stream)
		{
			GameServerRemovedNotification gameServerRemovedNotification = new GameServerRemovedNotification();
			DeserializeLengthDelimited(stream, gameServerRemovedNotification);
			return gameServerRemovedNotification;
		}

		public static GameServerRemovedNotification DeserializeLengthDelimited(Stream stream, GameServerRemovedNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameServerRemovedNotification Deserialize(Stream stream, GameServerRemovedNotification instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					instance.MatchmakerId = binaryReader.ReadUInt32();
					continue;
				case 18:
					if (instance.GameServer == null)
					{
						instance.GameServer = HostProxyPair.DeserializeLengthDelimited(stream);
					}
					else
					{
						HostProxyPair.DeserializeLengthDelimited(stream, instance.GameServer);
					}
					continue;
				case 25:
					instance.MatchmakerGuid = binaryReader.ReadUInt64();
					continue;
				case 33:
					instance.GameServerGuid = binaryReader.ReadUInt64();
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

		public static void Serialize(Stream stream, GameServerRemovedNotification instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasMatchmakerId)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.MatchmakerId);
			}
			if (instance.HasGameServer)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameServer.GetSerializedSize());
				HostProxyPair.Serialize(stream, instance.GameServer);
			}
			if (instance.HasMatchmakerGuid)
			{
				stream.WriteByte(25);
				binaryWriter.Write(instance.MatchmakerGuid);
			}
			if (instance.HasGameServerGuid)
			{
				stream.WriteByte(33);
				binaryWriter.Write(instance.GameServerGuid);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasMatchmakerId)
			{
				num++;
				num += 4;
			}
			if (HasGameServer)
			{
				num++;
				uint serializedSize = GameServer.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasMatchmakerGuid)
			{
				num++;
				num += 8;
			}
			if (HasGameServerGuid)
			{
				num++;
				num += 8;
			}
			return num;
		}
	}
}
