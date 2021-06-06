using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class Coins : IProtoBuf
	{
		public enum PacketID
		{
			ID = 608,
			System = 0
		}

		public bool HasFavoriteCoin;

		private int _FavoriteCoin;

		private List<int> _Coins_ = new List<int>();

		public int FavoriteCoin
		{
			get
			{
				return _FavoriteCoin;
			}
			set
			{
				_FavoriteCoin = value;
				HasFavoriteCoin = true;
			}
		}

		public List<int> Coins_
		{
			get
			{
				return _Coins_;
			}
			set
			{
				_Coins_ = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasFavoriteCoin)
			{
				num ^= FavoriteCoin.GetHashCode();
			}
			foreach (int item in Coins_)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Coins coins = obj as Coins;
			if (coins == null)
			{
				return false;
			}
			if (HasFavoriteCoin != coins.HasFavoriteCoin || (HasFavoriteCoin && !FavoriteCoin.Equals(coins.FavoriteCoin)))
			{
				return false;
			}
			if (Coins_.Count != coins.Coins_.Count)
			{
				return false;
			}
			for (int i = 0; i < Coins_.Count; i++)
			{
				if (!Coins_[i].Equals(coins.Coins_[i]))
				{
					return false;
				}
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Coins Deserialize(Stream stream, Coins instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Coins DeserializeLengthDelimited(Stream stream)
		{
			Coins coins = new Coins();
			DeserializeLengthDelimited(stream, coins);
			return coins;
		}

		public static Coins DeserializeLengthDelimited(Stream stream, Coins instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Coins Deserialize(Stream stream, Coins instance, long limit)
		{
			if (instance.Coins_ == null)
			{
				instance.Coins_ = new List<int>();
			}
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
					instance.FavoriteCoin = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Coins_.Add((int)ProtocolParser.ReadUInt64(stream));
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

		public static void Serialize(Stream stream, Coins instance)
		{
			if (instance.HasFavoriteCoin)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FavoriteCoin);
			}
			if (instance.Coins_.Count <= 0)
			{
				return;
			}
			foreach (int item in instance.Coins_)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasFavoriteCoin)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)FavoriteCoin);
			}
			if (Coins_.Count > 0)
			{
				foreach (int item in Coins_)
				{
					num++;
					num += ProtocolParser.SizeOfUInt64((ulong)item);
				}
				return num;
			}
			return num;
		}
	}
}
