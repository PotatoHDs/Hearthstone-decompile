using System.IO;

namespace PegasusGame
{
	public class GameCanceled : IProtoBuf
	{
		public enum Reason
		{
			OPPONENT_TIMEOUT = 1,
			PLAYER_LOADING_TIMEOUT,
			PLAYER_LOADING_DISCONNECTED
		}

		public enum PacketID
		{
			ID = 12
		}

		public Reason Reason_ { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Reason_.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			GameCanceled gameCanceled = obj as GameCanceled;
			if (gameCanceled == null)
			{
				return false;
			}
			if (!Reason_.Equals(gameCanceled.Reason_))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameCanceled Deserialize(Stream stream, GameCanceled instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameCanceled DeserializeLengthDelimited(Stream stream)
		{
			GameCanceled gameCanceled = new GameCanceled();
			DeserializeLengthDelimited(stream, gameCanceled);
			return gameCanceled;
		}

		public static GameCanceled DeserializeLengthDelimited(Stream stream, GameCanceled instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameCanceled Deserialize(Stream stream, GameCanceled instance, long limit)
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
					instance.Reason_ = (Reason)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, GameCanceled instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Reason_);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)Reason_) + 1;
		}
	}
}
