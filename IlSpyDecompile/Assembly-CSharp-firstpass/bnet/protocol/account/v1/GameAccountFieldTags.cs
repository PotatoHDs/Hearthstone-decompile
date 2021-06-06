using System.IO;

namespace bnet.protocol.account.v1
{
	public class GameAccountFieldTags : IProtoBuf
	{
		public bool HasGameLevelInfoTag;

		private uint _GameLevelInfoTag;

		public bool HasGameTimeInfoTag;

		private uint _GameTimeInfoTag;

		public bool HasGameStatusTag;

		private uint _GameStatusTag;

		public bool HasRafInfoTag;

		private uint _RafInfoTag;

		public uint GameLevelInfoTag
		{
			get
			{
				return _GameLevelInfoTag;
			}
			set
			{
				_GameLevelInfoTag = value;
				HasGameLevelInfoTag = true;
			}
		}

		public uint GameTimeInfoTag
		{
			get
			{
				return _GameTimeInfoTag;
			}
			set
			{
				_GameTimeInfoTag = value;
				HasGameTimeInfoTag = true;
			}
		}

		public uint GameStatusTag
		{
			get
			{
				return _GameStatusTag;
			}
			set
			{
				_GameStatusTag = value;
				HasGameStatusTag = true;
			}
		}

		public uint RafInfoTag
		{
			get
			{
				return _RafInfoTag;
			}
			set
			{
				_RafInfoTag = value;
				HasRafInfoTag = true;
			}
		}

		public bool IsInitialized => true;

		public void SetGameLevelInfoTag(uint val)
		{
			GameLevelInfoTag = val;
		}

		public void SetGameTimeInfoTag(uint val)
		{
			GameTimeInfoTag = val;
		}

		public void SetGameStatusTag(uint val)
		{
			GameStatusTag = val;
		}

		public void SetRafInfoTag(uint val)
		{
			RafInfoTag = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasGameLevelInfoTag)
			{
				num ^= GameLevelInfoTag.GetHashCode();
			}
			if (HasGameTimeInfoTag)
			{
				num ^= GameTimeInfoTag.GetHashCode();
			}
			if (HasGameStatusTag)
			{
				num ^= GameStatusTag.GetHashCode();
			}
			if (HasRafInfoTag)
			{
				num ^= RafInfoTag.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameAccountFieldTags gameAccountFieldTags = obj as GameAccountFieldTags;
			if (gameAccountFieldTags == null)
			{
				return false;
			}
			if (HasGameLevelInfoTag != gameAccountFieldTags.HasGameLevelInfoTag || (HasGameLevelInfoTag && !GameLevelInfoTag.Equals(gameAccountFieldTags.GameLevelInfoTag)))
			{
				return false;
			}
			if (HasGameTimeInfoTag != gameAccountFieldTags.HasGameTimeInfoTag || (HasGameTimeInfoTag && !GameTimeInfoTag.Equals(gameAccountFieldTags.GameTimeInfoTag)))
			{
				return false;
			}
			if (HasGameStatusTag != gameAccountFieldTags.HasGameStatusTag || (HasGameStatusTag && !GameStatusTag.Equals(gameAccountFieldTags.GameStatusTag)))
			{
				return false;
			}
			if (HasRafInfoTag != gameAccountFieldTags.HasRafInfoTag || (HasRafInfoTag && !RafInfoTag.Equals(gameAccountFieldTags.RafInfoTag)))
			{
				return false;
			}
			return true;
		}

		public static GameAccountFieldTags ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountFieldTags>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameAccountFieldTags Deserialize(Stream stream, GameAccountFieldTags instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameAccountFieldTags DeserializeLengthDelimited(Stream stream)
		{
			GameAccountFieldTags gameAccountFieldTags = new GameAccountFieldTags();
			DeserializeLengthDelimited(stream, gameAccountFieldTags);
			return gameAccountFieldTags;
		}

		public static GameAccountFieldTags DeserializeLengthDelimited(Stream stream, GameAccountFieldTags instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameAccountFieldTags Deserialize(Stream stream, GameAccountFieldTags instance, long limit)
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
				case 21:
					instance.GameLevelInfoTag = binaryReader.ReadUInt32();
					continue;
				case 29:
					instance.GameTimeInfoTag = binaryReader.ReadUInt32();
					continue;
				case 37:
					instance.GameStatusTag = binaryReader.ReadUInt32();
					continue;
				case 45:
					instance.RafInfoTag = binaryReader.ReadUInt32();
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

		public static void Serialize(Stream stream, GameAccountFieldTags instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasGameLevelInfoTag)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.GameLevelInfoTag);
			}
			if (instance.HasGameTimeInfoTag)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.GameTimeInfoTag);
			}
			if (instance.HasGameStatusTag)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.GameStatusTag);
			}
			if (instance.HasRafInfoTag)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.RafInfoTag);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasGameLevelInfoTag)
			{
				num++;
				num += 4;
			}
			if (HasGameTimeInfoTag)
			{
				num++;
				num += 4;
			}
			if (HasGameStatusTag)
			{
				num++;
				num += 4;
			}
			if (HasRafInfoTag)
			{
				num++;
				num += 4;
			}
			return num;
		}
	}
}
