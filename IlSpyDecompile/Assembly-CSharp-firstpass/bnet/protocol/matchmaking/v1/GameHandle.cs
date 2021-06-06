using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class GameHandle : IProtoBuf
	{
		public bool HasMatchmaker;

		private MatchmakerHandle _Matchmaker;

		public bool HasGameServer;

		private HostProxyPair _GameServer;

		public bool HasGameInstanceId;

		private uint _GameInstanceId;

		public bool HasMatchmakerGuid;

		private ulong _MatchmakerGuid;

		public bool HasGameServerGuid;

		private ulong _GameServerGuid;

		public MatchmakerHandle Matchmaker
		{
			get
			{
				return _Matchmaker;
			}
			set
			{
				_Matchmaker = value;
				HasMatchmaker = value != null;
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

		public uint GameInstanceId
		{
			get
			{
				return _GameInstanceId;
			}
			set
			{
				_GameInstanceId = value;
				HasGameInstanceId = true;
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

		public void SetMatchmaker(MatchmakerHandle val)
		{
			Matchmaker = val;
		}

		public void SetGameServer(HostProxyPair val)
		{
			GameServer = val;
		}

		public void SetGameInstanceId(uint val)
		{
			GameInstanceId = val;
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
			if (HasMatchmaker)
			{
				num ^= Matchmaker.GetHashCode();
			}
			if (HasGameServer)
			{
				num ^= GameServer.GetHashCode();
			}
			if (HasGameInstanceId)
			{
				num ^= GameInstanceId.GetHashCode();
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
			GameHandle gameHandle = obj as GameHandle;
			if (gameHandle == null)
			{
				return false;
			}
			if (HasMatchmaker != gameHandle.HasMatchmaker || (HasMatchmaker && !Matchmaker.Equals(gameHandle.Matchmaker)))
			{
				return false;
			}
			if (HasGameServer != gameHandle.HasGameServer || (HasGameServer && !GameServer.Equals(gameHandle.GameServer)))
			{
				return false;
			}
			if (HasGameInstanceId != gameHandle.HasGameInstanceId || (HasGameInstanceId && !GameInstanceId.Equals(gameHandle.GameInstanceId)))
			{
				return false;
			}
			if (HasMatchmakerGuid != gameHandle.HasMatchmakerGuid || (HasMatchmakerGuid && !MatchmakerGuid.Equals(gameHandle.MatchmakerGuid)))
			{
				return false;
			}
			if (HasGameServerGuid != gameHandle.HasGameServerGuid || (HasGameServerGuid && !GameServerGuid.Equals(gameHandle.GameServerGuid)))
			{
				return false;
			}
			return true;
		}

		public static GameHandle ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameHandle>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameHandle Deserialize(Stream stream, GameHandle instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameHandle DeserializeLengthDelimited(Stream stream)
		{
			GameHandle gameHandle = new GameHandle();
			DeserializeLengthDelimited(stream, gameHandle);
			return gameHandle;
		}

		public static GameHandle DeserializeLengthDelimited(Stream stream, GameHandle instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameHandle Deserialize(Stream stream, GameHandle instance, long limit)
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
				case 10:
					if (instance.Matchmaker == null)
					{
						instance.Matchmaker = MatchmakerHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						MatchmakerHandle.DeserializeLengthDelimited(stream, instance.Matchmaker);
					}
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
				case 29:
					instance.GameInstanceId = binaryReader.ReadUInt32();
					continue;
				case 33:
					instance.MatchmakerGuid = binaryReader.ReadUInt64();
					continue;
				case 41:
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

		public static void Serialize(Stream stream, GameHandle instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasMatchmaker)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Matchmaker.GetSerializedSize());
				MatchmakerHandle.Serialize(stream, instance.Matchmaker);
			}
			if (instance.HasGameServer)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameServer.GetSerializedSize());
				HostProxyPair.Serialize(stream, instance.GameServer);
			}
			if (instance.HasGameInstanceId)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.GameInstanceId);
			}
			if (instance.HasMatchmakerGuid)
			{
				stream.WriteByte(33);
				binaryWriter.Write(instance.MatchmakerGuid);
			}
			if (instance.HasGameServerGuid)
			{
				stream.WriteByte(41);
				binaryWriter.Write(instance.GameServerGuid);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasMatchmaker)
			{
				num++;
				uint serializedSize = Matchmaker.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasGameServer)
			{
				num++;
				uint serializedSize2 = GameServer.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasGameInstanceId)
			{
				num++;
				num += 4;
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
