using System.Collections.Generic;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class ShopBalanceAvailable : IProtoBuf
	{
		public bool HasPlayer;

		private Player _Player;

		private List<Balance> _Balances = new List<Balance>();

		public Player Player
		{
			get
			{
				return _Player;
			}
			set
			{
				_Player = value;
				HasPlayer = value != null;
			}
		}

		public List<Balance> Balances
		{
			get
			{
				return _Balances;
			}
			set
			{
				_Balances = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasPlayer)
			{
				num ^= Player.GetHashCode();
			}
			foreach (Balance balance in Balances)
			{
				num ^= balance.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ShopBalanceAvailable shopBalanceAvailable = obj as ShopBalanceAvailable;
			if (shopBalanceAvailable == null)
			{
				return false;
			}
			if (HasPlayer != shopBalanceAvailable.HasPlayer || (HasPlayer && !Player.Equals(shopBalanceAvailable.Player)))
			{
				return false;
			}
			if (Balances.Count != shopBalanceAvailable.Balances.Count)
			{
				return false;
			}
			for (int i = 0; i < Balances.Count; i++)
			{
				if (!Balances[i].Equals(shopBalanceAvailable.Balances[i]))
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

		public static ShopBalanceAvailable Deserialize(Stream stream, ShopBalanceAvailable instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ShopBalanceAvailable DeserializeLengthDelimited(Stream stream)
		{
			ShopBalanceAvailable shopBalanceAvailable = new ShopBalanceAvailable();
			DeserializeLengthDelimited(stream, shopBalanceAvailable);
			return shopBalanceAvailable;
		}

		public static ShopBalanceAvailable DeserializeLengthDelimited(Stream stream, ShopBalanceAvailable instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ShopBalanceAvailable Deserialize(Stream stream, ShopBalanceAvailable instance, long limit)
		{
			if (instance.Balances == null)
			{
				instance.Balances = new List<Balance>();
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
				case 10:
					if (instance.Player == null)
					{
						instance.Player = Player.DeserializeLengthDelimited(stream);
					}
					else
					{
						Player.DeserializeLengthDelimited(stream, instance.Player);
					}
					continue;
				case 18:
					instance.Balances.Add(Balance.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, ShopBalanceAvailable instance)
		{
			if (instance.HasPlayer)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.Balances.Count <= 0)
			{
				return;
			}
			foreach (Balance balance in instance.Balances)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, balance.GetSerializedSize());
				Balance.Serialize(stream, balance);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasPlayer)
			{
				num++;
				uint serializedSize = Player.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (Balances.Count > 0)
			{
				foreach (Balance balance in Balances)
				{
					num++;
					uint serializedSize2 = balance.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
				return num;
			}
			return num;
		}
	}
}
