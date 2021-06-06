using System.IO;

namespace bnet.protocol.account.v1
{
	public class GameSessionUpdateInfo : IProtoBuf
	{
		public bool HasCais;

		private CAIS _Cais;

		public CAIS Cais
		{
			get
			{
				return _Cais;
			}
			set
			{
				_Cais = value;
				HasCais = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetCais(CAIS val)
		{
			Cais = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasCais)
			{
				num ^= Cais.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameSessionUpdateInfo gameSessionUpdateInfo = obj as GameSessionUpdateInfo;
			if (gameSessionUpdateInfo == null)
			{
				return false;
			}
			if (HasCais != gameSessionUpdateInfo.HasCais || (HasCais && !Cais.Equals(gameSessionUpdateInfo.Cais)))
			{
				return false;
			}
			return true;
		}

		public static GameSessionUpdateInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameSessionUpdateInfo>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameSessionUpdateInfo Deserialize(Stream stream, GameSessionUpdateInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameSessionUpdateInfo DeserializeLengthDelimited(Stream stream)
		{
			GameSessionUpdateInfo gameSessionUpdateInfo = new GameSessionUpdateInfo();
			DeserializeLengthDelimited(stream, gameSessionUpdateInfo);
			return gameSessionUpdateInfo;
		}

		public static GameSessionUpdateInfo DeserializeLengthDelimited(Stream stream, GameSessionUpdateInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameSessionUpdateInfo Deserialize(Stream stream, GameSessionUpdateInfo instance, long limit)
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
				case 66:
					if (instance.Cais == null)
					{
						instance.Cais = CAIS.DeserializeLengthDelimited(stream);
					}
					else
					{
						CAIS.DeserializeLengthDelimited(stream, instance.Cais);
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

		public static void Serialize(Stream stream, GameSessionUpdateInfo instance)
		{
			if (instance.HasCais)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteUInt32(stream, instance.Cais.GetSerializedSize());
				CAIS.Serialize(stream, instance.Cais);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasCais)
			{
				num++;
				uint serializedSize = Cais.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
