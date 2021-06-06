using System;
using System.IO;

namespace PegasusGame
{
	public class PowerHistoryResetGame : IProtoBuf
	{
		public PowerHistoryCreateGame CreateGame { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ CreateGame.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			PowerHistoryResetGame powerHistoryResetGame = obj as PowerHistoryResetGame;
			if (powerHistoryResetGame == null)
			{
				return false;
			}
			if (!CreateGame.Equals(powerHistoryResetGame.CreateGame))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PowerHistoryResetGame Deserialize(Stream stream, PowerHistoryResetGame instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PowerHistoryResetGame DeserializeLengthDelimited(Stream stream)
		{
			PowerHistoryResetGame powerHistoryResetGame = new PowerHistoryResetGame();
			DeserializeLengthDelimited(stream, powerHistoryResetGame);
			return powerHistoryResetGame;
		}

		public static PowerHistoryResetGame DeserializeLengthDelimited(Stream stream, PowerHistoryResetGame instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PowerHistoryResetGame Deserialize(Stream stream, PowerHistoryResetGame instance, long limit)
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
					if (instance.CreateGame == null)
					{
						instance.CreateGame = PowerHistoryCreateGame.DeserializeLengthDelimited(stream);
					}
					else
					{
						PowerHistoryCreateGame.DeserializeLengthDelimited(stream, instance.CreateGame);
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

		public static void Serialize(Stream stream, PowerHistoryResetGame instance)
		{
			if (instance.CreateGame == null)
			{
				throw new ArgumentNullException("CreateGame", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.CreateGame.GetSerializedSize());
			PowerHistoryCreateGame.Serialize(stream, instance.CreateGame);
		}

		public uint GetSerializedSize()
		{
			uint serializedSize = CreateGame.GetSerializedSize();
			return 0 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 1;
		}
	}
}
