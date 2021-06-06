using System.IO;

namespace bnet.protocol.account.v1
{
	public class GameAccountFieldOptions : IProtoBuf
	{
		public bool HasAllFields;

		private bool _AllFields;

		public bool HasFieldGameLevelInfo;

		private bool _FieldGameLevelInfo;

		public bool HasFieldGameTimeInfo;

		private bool _FieldGameTimeInfo;

		public bool HasFieldGameStatus;

		private bool _FieldGameStatus;

		public bool HasFieldRafInfo;

		private bool _FieldRafInfo;

		public bool AllFields
		{
			get
			{
				return _AllFields;
			}
			set
			{
				_AllFields = value;
				HasAllFields = true;
			}
		}

		public bool FieldGameLevelInfo
		{
			get
			{
				return _FieldGameLevelInfo;
			}
			set
			{
				_FieldGameLevelInfo = value;
				HasFieldGameLevelInfo = true;
			}
		}

		public bool FieldGameTimeInfo
		{
			get
			{
				return _FieldGameTimeInfo;
			}
			set
			{
				_FieldGameTimeInfo = value;
				HasFieldGameTimeInfo = true;
			}
		}

		public bool FieldGameStatus
		{
			get
			{
				return _FieldGameStatus;
			}
			set
			{
				_FieldGameStatus = value;
				HasFieldGameStatus = true;
			}
		}

		public bool FieldRafInfo
		{
			get
			{
				return _FieldRafInfo;
			}
			set
			{
				_FieldRafInfo = value;
				HasFieldRafInfo = true;
			}
		}

		public bool IsInitialized => true;

		public void SetAllFields(bool val)
		{
			AllFields = val;
		}

		public void SetFieldGameLevelInfo(bool val)
		{
			FieldGameLevelInfo = val;
		}

		public void SetFieldGameTimeInfo(bool val)
		{
			FieldGameTimeInfo = val;
		}

		public void SetFieldGameStatus(bool val)
		{
			FieldGameStatus = val;
		}

		public void SetFieldRafInfo(bool val)
		{
			FieldRafInfo = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAllFields)
			{
				num ^= AllFields.GetHashCode();
			}
			if (HasFieldGameLevelInfo)
			{
				num ^= FieldGameLevelInfo.GetHashCode();
			}
			if (HasFieldGameTimeInfo)
			{
				num ^= FieldGameTimeInfo.GetHashCode();
			}
			if (HasFieldGameStatus)
			{
				num ^= FieldGameStatus.GetHashCode();
			}
			if (HasFieldRafInfo)
			{
				num ^= FieldRafInfo.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameAccountFieldOptions gameAccountFieldOptions = obj as GameAccountFieldOptions;
			if (gameAccountFieldOptions == null)
			{
				return false;
			}
			if (HasAllFields != gameAccountFieldOptions.HasAllFields || (HasAllFields && !AllFields.Equals(gameAccountFieldOptions.AllFields)))
			{
				return false;
			}
			if (HasFieldGameLevelInfo != gameAccountFieldOptions.HasFieldGameLevelInfo || (HasFieldGameLevelInfo && !FieldGameLevelInfo.Equals(gameAccountFieldOptions.FieldGameLevelInfo)))
			{
				return false;
			}
			if (HasFieldGameTimeInfo != gameAccountFieldOptions.HasFieldGameTimeInfo || (HasFieldGameTimeInfo && !FieldGameTimeInfo.Equals(gameAccountFieldOptions.FieldGameTimeInfo)))
			{
				return false;
			}
			if (HasFieldGameStatus != gameAccountFieldOptions.HasFieldGameStatus || (HasFieldGameStatus && !FieldGameStatus.Equals(gameAccountFieldOptions.FieldGameStatus)))
			{
				return false;
			}
			if (HasFieldRafInfo != gameAccountFieldOptions.HasFieldRafInfo || (HasFieldRafInfo && !FieldRafInfo.Equals(gameAccountFieldOptions.FieldRafInfo)))
			{
				return false;
			}
			return true;
		}

		public static GameAccountFieldOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountFieldOptions>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameAccountFieldOptions Deserialize(Stream stream, GameAccountFieldOptions instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameAccountFieldOptions DeserializeLengthDelimited(Stream stream)
		{
			GameAccountFieldOptions gameAccountFieldOptions = new GameAccountFieldOptions();
			DeserializeLengthDelimited(stream, gameAccountFieldOptions);
			return gameAccountFieldOptions;
		}

		public static GameAccountFieldOptions DeserializeLengthDelimited(Stream stream, GameAccountFieldOptions instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameAccountFieldOptions Deserialize(Stream stream, GameAccountFieldOptions instance, long limit)
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
				case 8:
					instance.AllFields = ProtocolParser.ReadBool(stream);
					continue;
				case 16:
					instance.FieldGameLevelInfo = ProtocolParser.ReadBool(stream);
					continue;
				case 24:
					instance.FieldGameTimeInfo = ProtocolParser.ReadBool(stream);
					continue;
				case 32:
					instance.FieldGameStatus = ProtocolParser.ReadBool(stream);
					continue;
				case 40:
					instance.FieldRafInfo = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, GameAccountFieldOptions instance)
		{
			if (instance.HasAllFields)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.AllFields);
			}
			if (instance.HasFieldGameLevelInfo)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.FieldGameLevelInfo);
			}
			if (instance.HasFieldGameTimeInfo)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.FieldGameTimeInfo);
			}
			if (instance.HasFieldGameStatus)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.FieldGameStatus);
			}
			if (instance.HasFieldRafInfo)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.FieldRafInfo);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAllFields)
			{
				num++;
				num++;
			}
			if (HasFieldGameLevelInfo)
			{
				num++;
				num++;
			}
			if (HasFieldGameTimeInfo)
			{
				num++;
				num++;
			}
			if (HasFieldGameStatus)
			{
				num++;
				num++;
			}
			if (HasFieldRafInfo)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
