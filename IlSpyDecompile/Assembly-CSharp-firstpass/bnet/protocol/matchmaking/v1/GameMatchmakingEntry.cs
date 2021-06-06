using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class GameMatchmakingEntry : IProtoBuf
	{
		public bool HasOptions;

		private GameMatchmakingOptions _Options;

		public bool HasRequestId;

		private RequestId _RequestId;

		public GameMatchmakingOptions Options
		{
			get
			{
				return _Options;
			}
			set
			{
				_Options = value;
				HasOptions = value != null;
			}
		}

		public RequestId RequestId
		{
			get
			{
				return _RequestId;
			}
			set
			{
				_RequestId = value;
				HasRequestId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetOptions(GameMatchmakingOptions val)
		{
			Options = val;
		}

		public void SetRequestId(RequestId val)
		{
			RequestId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasOptions)
			{
				num ^= Options.GetHashCode();
			}
			if (HasRequestId)
			{
				num ^= RequestId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameMatchmakingEntry gameMatchmakingEntry = obj as GameMatchmakingEntry;
			if (gameMatchmakingEntry == null)
			{
				return false;
			}
			if (HasOptions != gameMatchmakingEntry.HasOptions || (HasOptions && !Options.Equals(gameMatchmakingEntry.Options)))
			{
				return false;
			}
			if (HasRequestId != gameMatchmakingEntry.HasRequestId || (HasRequestId && !RequestId.Equals(gameMatchmakingEntry.RequestId)))
			{
				return false;
			}
			return true;
		}

		public static GameMatchmakingEntry ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameMatchmakingEntry>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameMatchmakingEntry Deserialize(Stream stream, GameMatchmakingEntry instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameMatchmakingEntry DeserializeLengthDelimited(Stream stream)
		{
			GameMatchmakingEntry gameMatchmakingEntry = new GameMatchmakingEntry();
			DeserializeLengthDelimited(stream, gameMatchmakingEntry);
			return gameMatchmakingEntry;
		}

		public static GameMatchmakingEntry DeserializeLengthDelimited(Stream stream, GameMatchmakingEntry instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameMatchmakingEntry Deserialize(Stream stream, GameMatchmakingEntry instance, long limit)
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
					if (instance.Options == null)
					{
						instance.Options = GameMatchmakingOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameMatchmakingOptions.DeserializeLengthDelimited(stream, instance.Options);
					}
					continue;
				case 18:
					if (instance.RequestId == null)
					{
						instance.RequestId = RequestId.DeserializeLengthDelimited(stream);
					}
					else
					{
						RequestId.DeserializeLengthDelimited(stream, instance.RequestId);
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

		public static void Serialize(Stream stream, GameMatchmakingEntry instance)
		{
			if (instance.HasOptions)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				GameMatchmakingOptions.Serialize(stream, instance.Options);
			}
			if (instance.HasRequestId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.RequestId.GetSerializedSize());
				RequestId.Serialize(stream, instance.RequestId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasOptions)
			{
				num++;
				uint serializedSize = Options.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasRequestId)
			{
				num++;
				uint serializedSize2 = RequestId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
