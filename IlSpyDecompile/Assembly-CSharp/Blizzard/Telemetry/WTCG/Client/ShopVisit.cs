using System.Collections.Generic;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class ShopVisit : IProtoBuf
	{
		public bool HasPlayer;

		private Player _Player;

		private List<ShopCard> _Cards = new List<ShopCard>();

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

		public List<ShopCard> Cards
		{
			get
			{
				return _Cards;
			}
			set
			{
				_Cards = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasPlayer)
			{
				num ^= Player.GetHashCode();
			}
			foreach (ShopCard card in Cards)
			{
				num ^= card.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ShopVisit shopVisit = obj as ShopVisit;
			if (shopVisit == null)
			{
				return false;
			}
			if (HasPlayer != shopVisit.HasPlayer || (HasPlayer && !Player.Equals(shopVisit.Player)))
			{
				return false;
			}
			if (Cards.Count != shopVisit.Cards.Count)
			{
				return false;
			}
			for (int i = 0; i < Cards.Count; i++)
			{
				if (!Cards[i].Equals(shopVisit.Cards[i]))
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

		public static ShopVisit Deserialize(Stream stream, ShopVisit instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ShopVisit DeserializeLengthDelimited(Stream stream)
		{
			ShopVisit shopVisit = new ShopVisit();
			DeserializeLengthDelimited(stream, shopVisit);
			return shopVisit;
		}

		public static ShopVisit DeserializeLengthDelimited(Stream stream, ShopVisit instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ShopVisit Deserialize(Stream stream, ShopVisit instance, long limit)
		{
			if (instance.Cards == null)
			{
				instance.Cards = new List<ShopCard>();
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
					instance.Cards.Add(ShopCard.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, ShopVisit instance)
		{
			if (instance.HasPlayer)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.Cards.Count <= 0)
			{
				return;
			}
			foreach (ShopCard card in instance.Cards)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, card.GetSerializedSize());
				ShopCard.Serialize(stream, card);
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
			if (Cards.Count > 0)
			{
				foreach (ShopCard card in Cards)
				{
					num++;
					uint serializedSize2 = card.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
				return num;
			}
			return num;
		}
	}
}
