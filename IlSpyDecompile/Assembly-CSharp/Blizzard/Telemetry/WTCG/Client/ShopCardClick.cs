using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class ShopCardClick : IProtoBuf
	{
		public bool HasPlayer;

		private Player _Player;

		public bool HasShopcard;

		private ShopCard _Shopcard;

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

		public ShopCard Shopcard
		{
			get
			{
				return _Shopcard;
			}
			set
			{
				_Shopcard = value;
				HasShopcard = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasPlayer)
			{
				num ^= Player.GetHashCode();
			}
			if (HasShopcard)
			{
				num ^= Shopcard.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ShopCardClick shopCardClick = obj as ShopCardClick;
			if (shopCardClick == null)
			{
				return false;
			}
			if (HasPlayer != shopCardClick.HasPlayer || (HasPlayer && !Player.Equals(shopCardClick.Player)))
			{
				return false;
			}
			if (HasShopcard != shopCardClick.HasShopcard || (HasShopcard && !Shopcard.Equals(shopCardClick.Shopcard)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ShopCardClick Deserialize(Stream stream, ShopCardClick instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ShopCardClick DeserializeLengthDelimited(Stream stream)
		{
			ShopCardClick shopCardClick = new ShopCardClick();
			DeserializeLengthDelimited(stream, shopCardClick);
			return shopCardClick;
		}

		public static ShopCardClick DeserializeLengthDelimited(Stream stream, ShopCardClick instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ShopCardClick Deserialize(Stream stream, ShopCardClick instance, long limit)
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
					if (instance.Shopcard == null)
					{
						instance.Shopcard = ShopCard.DeserializeLengthDelimited(stream);
					}
					else
					{
						ShopCard.DeserializeLengthDelimited(stream, instance.Shopcard);
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

		public static void Serialize(Stream stream, ShopCardClick instance)
		{
			if (instance.HasPlayer)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.HasShopcard)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Shopcard.GetSerializedSize());
				ShopCard.Serialize(stream, instance.Shopcard);
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
			if (HasShopcard)
			{
				num++;
				uint serializedSize2 = Shopcard.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
