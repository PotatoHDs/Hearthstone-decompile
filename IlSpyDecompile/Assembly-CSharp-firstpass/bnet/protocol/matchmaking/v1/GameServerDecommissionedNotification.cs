using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class GameServerDecommissionedNotification : IProtoBuf
	{
		public bool HasGameServerGuid;

		private ulong _GameServerGuid;

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

		public void SetGameServerGuid(ulong val)
		{
			GameServerGuid = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasGameServerGuid)
			{
				num ^= GameServerGuid.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameServerDecommissionedNotification gameServerDecommissionedNotification = obj as GameServerDecommissionedNotification;
			if (gameServerDecommissionedNotification == null)
			{
				return false;
			}
			if (HasGameServerGuid != gameServerDecommissionedNotification.HasGameServerGuid || (HasGameServerGuid && !GameServerGuid.Equals(gameServerDecommissionedNotification.GameServerGuid)))
			{
				return false;
			}
			return true;
		}

		public static GameServerDecommissionedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameServerDecommissionedNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameServerDecommissionedNotification Deserialize(Stream stream, GameServerDecommissionedNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameServerDecommissionedNotification DeserializeLengthDelimited(Stream stream)
		{
			GameServerDecommissionedNotification gameServerDecommissionedNotification = new GameServerDecommissionedNotification();
			DeserializeLengthDelimited(stream, gameServerDecommissionedNotification);
			return gameServerDecommissionedNotification;
		}

		public static GameServerDecommissionedNotification DeserializeLengthDelimited(Stream stream, GameServerDecommissionedNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameServerDecommissionedNotification Deserialize(Stream stream, GameServerDecommissionedNotification instance, long limit)
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
				case 9:
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

		public static void Serialize(Stream stream, GameServerDecommissionedNotification instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasGameServerGuid)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.GameServerGuid);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasGameServerGuid)
			{
				num++;
				num += 8;
			}
			return num;
		}
	}
}
