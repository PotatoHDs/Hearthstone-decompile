using System.IO;

namespace PegasusUtil
{
	public class SetFavoriteCoin : IProtoBuf
	{
		public enum PacketID
		{
			ID = 609,
			System = 0
		}

		public bool HasCoin;

		private int _Coin;

		public int Coin
		{
			get
			{
				return _Coin;
			}
			set
			{
				_Coin = value;
				HasCoin = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasCoin)
			{
				num ^= Coin.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SetFavoriteCoin setFavoriteCoin = obj as SetFavoriteCoin;
			if (setFavoriteCoin == null)
			{
				return false;
			}
			if (HasCoin != setFavoriteCoin.HasCoin || (HasCoin && !Coin.Equals(setFavoriteCoin.Coin)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SetFavoriteCoin Deserialize(Stream stream, SetFavoriteCoin instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SetFavoriteCoin DeserializeLengthDelimited(Stream stream)
		{
			SetFavoriteCoin setFavoriteCoin = new SetFavoriteCoin();
			DeserializeLengthDelimited(stream, setFavoriteCoin);
			return setFavoriteCoin;
		}

		public static SetFavoriteCoin DeserializeLengthDelimited(Stream stream, SetFavoriteCoin instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SetFavoriteCoin Deserialize(Stream stream, SetFavoriteCoin instance, long limit)
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
					instance.Coin = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, SetFavoriteCoin instance)
		{
			if (instance.HasCoin)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Coin);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasCoin)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Coin);
			}
			return num;
		}
	}
}
