using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class CoinUpdate : IProtoBuf
	{
		public enum PacketID
		{
			ID = 611,
			System = 0
		}

		public bool HasFavoriteCoinId;

		private int _FavoriteCoinId;

		private List<int> _AddCoinId = new List<int>();

		private List<int> _RemoveCoinId = new List<int>();

		public int FavoriteCoinId
		{
			get
			{
				return _FavoriteCoinId;
			}
			set
			{
				_FavoriteCoinId = value;
				HasFavoriteCoinId = true;
			}
		}

		public List<int> AddCoinId
		{
			get
			{
				return _AddCoinId;
			}
			set
			{
				_AddCoinId = value;
			}
		}

		public List<int> RemoveCoinId
		{
			get
			{
				return _RemoveCoinId;
			}
			set
			{
				_RemoveCoinId = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasFavoriteCoinId)
			{
				num ^= FavoriteCoinId.GetHashCode();
			}
			foreach (int item in AddCoinId)
			{
				num ^= item.GetHashCode();
			}
			foreach (int item2 in RemoveCoinId)
			{
				num ^= item2.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CoinUpdate coinUpdate = obj as CoinUpdate;
			if (coinUpdate == null)
			{
				return false;
			}
			if (HasFavoriteCoinId != coinUpdate.HasFavoriteCoinId || (HasFavoriteCoinId && !FavoriteCoinId.Equals(coinUpdate.FavoriteCoinId)))
			{
				return false;
			}
			if (AddCoinId.Count != coinUpdate.AddCoinId.Count)
			{
				return false;
			}
			for (int i = 0; i < AddCoinId.Count; i++)
			{
				if (!AddCoinId[i].Equals(coinUpdate.AddCoinId[i]))
				{
					return false;
				}
			}
			if (RemoveCoinId.Count != coinUpdate.RemoveCoinId.Count)
			{
				return false;
			}
			for (int j = 0; j < RemoveCoinId.Count; j++)
			{
				if (!RemoveCoinId[j].Equals(coinUpdate.RemoveCoinId[j]))
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

		public static CoinUpdate Deserialize(Stream stream, CoinUpdate instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CoinUpdate DeserializeLengthDelimited(Stream stream)
		{
			CoinUpdate coinUpdate = new CoinUpdate();
			DeserializeLengthDelimited(stream, coinUpdate);
			return coinUpdate;
		}

		public static CoinUpdate DeserializeLengthDelimited(Stream stream, CoinUpdate instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CoinUpdate Deserialize(Stream stream, CoinUpdate instance, long limit)
		{
			if (instance.AddCoinId == null)
			{
				instance.AddCoinId = new List<int>();
			}
			if (instance.RemoveCoinId == null)
			{
				instance.RemoveCoinId = new List<int>();
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
					instance.FavoriteCoinId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.AddCoinId.Add((int)ProtocolParser.ReadUInt64(stream));
					continue;
				case 24:
					instance.RemoveCoinId.Add((int)ProtocolParser.ReadUInt64(stream));
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

		public static void Serialize(Stream stream, CoinUpdate instance)
		{
			if (instance.HasFavoriteCoinId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FavoriteCoinId);
			}
			if (instance.AddCoinId.Count > 0)
			{
				foreach (int item in instance.AddCoinId)
				{
					stream.WriteByte(16);
					ProtocolParser.WriteUInt64(stream, (ulong)item);
				}
			}
			if (instance.RemoveCoinId.Count <= 0)
			{
				return;
			}
			foreach (int item2 in instance.RemoveCoinId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)item2);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasFavoriteCoinId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)FavoriteCoinId);
			}
			if (AddCoinId.Count > 0)
			{
				foreach (int item in AddCoinId)
				{
					num++;
					num += ProtocolParser.SizeOfUInt64((ulong)item);
				}
			}
			if (RemoveCoinId.Count > 0)
			{
				foreach (int item2 in RemoveCoinId)
				{
					num++;
					num += ProtocolParser.SizeOfUInt64((ulong)item2);
				}
				return num;
			}
			return num;
		}
	}
}
