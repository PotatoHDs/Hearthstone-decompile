using System;
using System.IO;
using System.Text;

namespace bnet.protocol.game_utilities.v1
{
	public class GameAccountOfflineNotification : IProtoBuf
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
			GameAccountOfflineNotification gameAccountOfflineNotification = obj as GameAccountOfflineNotification;
			if (gameAccountOfflineNotification == null)
			{
				return false;
			}
			if (!GameAccountId.Equals(gameAccountOfflineNotification.GameAccountId))
			{
				return false;
			}
			if (HasHost != gameAccountOfflineNotification.HasHost || (HasHost && !Host.Equals(gameAccountOfflineNotification.Host)))
			{
				return false;
			}
			if (HasSessionId != gameAccountOfflineNotification.HasSessionId || (HasSessionId && !SessionId.Equals(gameAccountOfflineNotification.SessionId)))
			{
				return false;
			}
			return true;
		}

		public static GameAccountOfflineNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountOfflineNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameAccountOfflineNotification Deserialize(Stream stream, GameAccountOfflineNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameAccountOfflineNotification DeserializeLengthDelimited(Stream stream)
		{
			GameAccountOfflineNotification gameAccountOfflineNotification = new GameAccountOfflineNotification();
			DeserializeLengthDelimited(stream, gameAccountOfflineNotification);
			return gameAccountOfflineNotification;
		}

		public static GameAccountOfflineNotification DeserializeLengthDelimited(Stream stream, GameAccountOfflineNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameAccountOfflineNotification Deserialize(Stream stream, GameAccountOfflineNotification instance, long limit)
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

		public static void Serialize(Stream stream, GameAccountOfflineNotification instance)
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
