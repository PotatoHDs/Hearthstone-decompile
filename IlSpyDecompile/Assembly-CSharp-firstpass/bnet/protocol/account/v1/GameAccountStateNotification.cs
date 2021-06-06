using System.IO;

namespace bnet.protocol.account.v1
{
	public class GameAccountStateNotification : IProtoBuf
	{
		public bool HasGameAccountState;

		private GameAccountState _GameAccountState;

		public bool HasSubscriberId;

		private ulong _SubscriberId;

		public bool HasGameAccountTags;

		private GameAccountFieldTags _GameAccountTags;

		public bool HasSubscriptionCompleted;

		private bool _SubscriptionCompleted;

		public GameAccountState GameAccountState
		{
			get
			{
				return _GameAccountState;
			}
			set
			{
				_GameAccountState = value;
				HasGameAccountState = value != null;
			}
		}

		public ulong SubscriberId
		{
			get
			{
				return _SubscriberId;
			}
			set
			{
				_SubscriberId = value;
				HasSubscriberId = true;
			}
		}

		public GameAccountFieldTags GameAccountTags
		{
			get
			{
				return _GameAccountTags;
			}
			set
			{
				_GameAccountTags = value;
				HasGameAccountTags = value != null;
			}
		}

		public bool SubscriptionCompleted
		{
			get
			{
				return _SubscriptionCompleted;
			}
			set
			{
				_SubscriptionCompleted = value;
				HasSubscriptionCompleted = true;
			}
		}

		public bool IsInitialized => true;

		public void SetGameAccountState(GameAccountState val)
		{
			GameAccountState = val;
		}

		public void SetSubscriberId(ulong val)
		{
			SubscriberId = val;
		}

		public void SetGameAccountTags(GameAccountFieldTags val)
		{
			GameAccountTags = val;
		}

		public void SetSubscriptionCompleted(bool val)
		{
			SubscriptionCompleted = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasGameAccountState)
			{
				num ^= GameAccountState.GetHashCode();
			}
			if (HasSubscriberId)
			{
				num ^= SubscriberId.GetHashCode();
			}
			if (HasGameAccountTags)
			{
				num ^= GameAccountTags.GetHashCode();
			}
			if (HasSubscriptionCompleted)
			{
				num ^= SubscriptionCompleted.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameAccountStateNotification gameAccountStateNotification = obj as GameAccountStateNotification;
			if (gameAccountStateNotification == null)
			{
				return false;
			}
			if (HasGameAccountState != gameAccountStateNotification.HasGameAccountState || (HasGameAccountState && !GameAccountState.Equals(gameAccountStateNotification.GameAccountState)))
			{
				return false;
			}
			if (HasSubscriberId != gameAccountStateNotification.HasSubscriberId || (HasSubscriberId && !SubscriberId.Equals(gameAccountStateNotification.SubscriberId)))
			{
				return false;
			}
			if (HasGameAccountTags != gameAccountStateNotification.HasGameAccountTags || (HasGameAccountTags && !GameAccountTags.Equals(gameAccountStateNotification.GameAccountTags)))
			{
				return false;
			}
			if (HasSubscriptionCompleted != gameAccountStateNotification.HasSubscriptionCompleted || (HasSubscriptionCompleted && !SubscriptionCompleted.Equals(gameAccountStateNotification.SubscriptionCompleted)))
			{
				return false;
			}
			return true;
		}

		public static GameAccountStateNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountStateNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameAccountStateNotification Deserialize(Stream stream, GameAccountStateNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameAccountStateNotification DeserializeLengthDelimited(Stream stream)
		{
			GameAccountStateNotification gameAccountStateNotification = new GameAccountStateNotification();
			DeserializeLengthDelimited(stream, gameAccountStateNotification);
			return gameAccountStateNotification;
		}

		public static GameAccountStateNotification DeserializeLengthDelimited(Stream stream, GameAccountStateNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameAccountStateNotification Deserialize(Stream stream, GameAccountStateNotification instance, long limit)
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
					if (instance.GameAccountState == null)
					{
						instance.GameAccountState = GameAccountState.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountState.DeserializeLengthDelimited(stream, instance.GameAccountState);
					}
					continue;
				case 16:
					instance.SubscriberId = ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					if (instance.GameAccountTags == null)
					{
						instance.GameAccountTags = GameAccountFieldTags.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountFieldTags.DeserializeLengthDelimited(stream, instance.GameAccountTags);
					}
					continue;
				case 32:
					instance.SubscriptionCompleted = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, GameAccountStateNotification instance)
		{
			if (instance.HasGameAccountState)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountState.GetSerializedSize());
				GameAccountState.Serialize(stream, instance.GameAccountState);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.SubscriberId);
			}
			if (instance.HasGameAccountTags)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountTags.GetSerializedSize());
				GameAccountFieldTags.Serialize(stream, instance.GameAccountTags);
			}
			if (instance.HasSubscriptionCompleted)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.SubscriptionCompleted);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasGameAccountState)
			{
				num++;
				uint serializedSize = GameAccountState.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasSubscriberId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(SubscriberId);
			}
			if (HasGameAccountTags)
			{
				num++;
				uint serializedSize2 = GameAccountTags.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasSubscriptionCompleted)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
