using System.IO;

namespace bnet.protocol.games.v1
{
	public class SetGameSlotsRequest : IProtoBuf
	{
		public bool HasGameSlots;

		private uint _GameSlots;

		public bool HasCreateGameRate;

		private uint _CreateGameRate;

		public uint GameSlots
		{
			get
			{
				return _GameSlots;
			}
			set
			{
				_GameSlots = value;
				HasGameSlots = true;
			}
		}

		public uint CreateGameRate
		{
			get
			{
				return _CreateGameRate;
			}
			set
			{
				_CreateGameRate = value;
				HasCreateGameRate = true;
			}
		}

		public bool IsInitialized => true;

		public void SetGameSlots(uint val)
		{
			GameSlots = val;
		}

		public void SetCreateGameRate(uint val)
		{
			CreateGameRate = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasGameSlots)
			{
				num ^= GameSlots.GetHashCode();
			}
			if (HasCreateGameRate)
			{
				num ^= CreateGameRate.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SetGameSlotsRequest setGameSlotsRequest = obj as SetGameSlotsRequest;
			if (setGameSlotsRequest == null)
			{
				return false;
			}
			if (HasGameSlots != setGameSlotsRequest.HasGameSlots || (HasGameSlots && !GameSlots.Equals(setGameSlotsRequest.GameSlots)))
			{
				return false;
			}
			if (HasCreateGameRate != setGameSlotsRequest.HasCreateGameRate || (HasCreateGameRate && !CreateGameRate.Equals(setGameSlotsRequest.CreateGameRate)))
			{
				return false;
			}
			return true;
		}

		public static SetGameSlotsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SetGameSlotsRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SetGameSlotsRequest Deserialize(Stream stream, SetGameSlotsRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SetGameSlotsRequest DeserializeLengthDelimited(Stream stream)
		{
			SetGameSlotsRequest setGameSlotsRequest = new SetGameSlotsRequest();
			DeserializeLengthDelimited(stream, setGameSlotsRequest);
			return setGameSlotsRequest;
		}

		public static SetGameSlotsRequest DeserializeLengthDelimited(Stream stream, SetGameSlotsRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SetGameSlotsRequest Deserialize(Stream stream, SetGameSlotsRequest instance, long limit)
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
					instance.GameSlots = ProtocolParser.ReadUInt32(stream);
					continue;
				case 16:
					instance.CreateGameRate = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, SetGameSlotsRequest instance)
		{
			if (instance.HasGameSlots)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.GameSlots);
			}
			if (instance.HasCreateGameRate)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.CreateGameRate);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasGameSlots)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(GameSlots);
			}
			if (HasCreateGameRate)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(CreateGameRate);
			}
			return num;
		}
	}
}
