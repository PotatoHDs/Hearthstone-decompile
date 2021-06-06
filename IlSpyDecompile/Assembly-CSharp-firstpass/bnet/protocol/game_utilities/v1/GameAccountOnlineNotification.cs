using System;
using System.IO;
using System.Text;

namespace bnet.protocol.game_utilities.v1
{
	public class GameAccountOnlineNotification : IProtoBuf
	{
		public bool HasHost;

		private ProcessId _Host;

		public bool HasSessionId;

		private string _SessionId;

		public EntityId GameAccountId { get; set; }

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

		public string SessionId
		{
			get
			{
				return _SessionId;
			}
			set
			{
				_SessionId = value;
				HasSessionId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetGameAccountId(EntityId val)
		{
			GameAccountId = val;
		}

		public void SetHost(ProcessId val)
		{
			Host = val;
		}

		public void SetSessionId(string val)
		{
			SessionId = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= GameAccountId.GetHashCode();
			if (HasHost)
			{
				hashCode ^= Host.GetHashCode();
			}
			if (HasSessionId)
			{
				hashCode ^= SessionId.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			GameAccountOnlineNotification gameAccountOnlineNotification = obj as GameAccountOnlineNotification;
			if (gameAccountOnlineNotification == null)
			{
				return false;
			}
			if (!GameAccountId.Equals(gameAccountOnlineNotification.GameAccountId))
			{
				return false;
			}
			if (HasHost != gameAccountOnlineNotification.HasHost || (HasHost && !Host.Equals(gameAccountOnlineNotification.Host)))
			{
				return false;
			}
			if (HasSessionId != gameAccountOnlineNotification.HasSessionId || (HasSessionId && !SessionId.Equals(gameAccountOnlineNotification.SessionId)))
			{
				return false;
			}
			return true;
		}

		public static GameAccountOnlineNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountOnlineNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameAccountOnlineNotification Deserialize(Stream stream, GameAccountOnlineNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameAccountOnlineNotification DeserializeLengthDelimited(Stream stream)
		{
			GameAccountOnlineNotification gameAccountOnlineNotification = new GameAccountOnlineNotification();
			DeserializeLengthDelimited(stream, gameAccountOnlineNotification);
			return gameAccountOnlineNotification;
		}

		public static GameAccountOnlineNotification DeserializeLengthDelimited(Stream stream, GameAccountOnlineNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameAccountOnlineNotification Deserialize(Stream stream, GameAccountOnlineNotification instance, long limit)
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
					if (instance.GameAccountId == null)
					{
						instance.GameAccountId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.GameAccountId);
					}
					continue;
				case 18:
					if (instance.Host == null)
					{
						instance.Host = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.Host);
					}
					continue;
				case 26:
					instance.SessionId = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, GameAccountOnlineNotification instance)
		{
			if (instance.GameAccountId == null)
			{
				throw new ArgumentNullException("GameAccountId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
			EntityId.Serialize(stream, instance.GameAccountId);
			if (instance.HasHost)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
			if (instance.HasSessionId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SessionId));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = GameAccountId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasHost)
			{
				num++;
				uint serializedSize2 = Host.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasSessionId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(SessionId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num + 1;
		}
	}
}
