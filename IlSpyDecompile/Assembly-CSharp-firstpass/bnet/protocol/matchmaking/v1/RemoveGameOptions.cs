using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class RemoveGameOptions : IProtoBuf
	{
		public bool HasGameHandle;

		private GameHandle _GameHandle;

		public GameHandle GameHandle
		{
			get
			{
				return _GameHandle;
			}
			set
			{
				_GameHandle = value;
				HasGameHandle = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetGameHandle(GameHandle val)
		{
			GameHandle = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasGameHandle)
			{
				num ^= GameHandle.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RemoveGameOptions removeGameOptions = obj as RemoveGameOptions;
			if (removeGameOptions == null)
			{
				return false;
			}
			if (HasGameHandle != removeGameOptions.HasGameHandle || (HasGameHandle && !GameHandle.Equals(removeGameOptions.GameHandle)))
			{
				return false;
			}
			return true;
		}

		public static RemoveGameOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RemoveGameOptions>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RemoveGameOptions Deserialize(Stream stream, RemoveGameOptions instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RemoveGameOptions DeserializeLengthDelimited(Stream stream)
		{
			RemoveGameOptions removeGameOptions = new RemoveGameOptions();
			DeserializeLengthDelimited(stream, removeGameOptions);
			return removeGameOptions;
		}

		public static RemoveGameOptions DeserializeLengthDelimited(Stream stream, RemoveGameOptions instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RemoveGameOptions Deserialize(Stream stream, RemoveGameOptions instance, long limit)
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
					if (instance.GameHandle == null)
					{
						instance.GameHandle = GameHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
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

		public static void Serialize(Stream stream, RemoveGameOptions instance)
		{
			if (instance.HasGameHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				GameHandle.Serialize(stream, instance.GameHandle);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasGameHandle)
			{
				num++;
				uint serializedSize = GameHandle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
