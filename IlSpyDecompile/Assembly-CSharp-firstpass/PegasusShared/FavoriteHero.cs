using System;
using System.IO;

namespace PegasusShared
{
	public class FavoriteHero : IProtoBuf
	{
		public int ClassId { get; set; }

		public CardDef Hero { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ ClassId.GetHashCode() ^ Hero.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			FavoriteHero favoriteHero = obj as FavoriteHero;
			if (favoriteHero == null)
			{
				return false;
			}
			if (!ClassId.Equals(favoriteHero.ClassId))
			{
				return false;
			}
			if (!Hero.Equals(favoriteHero.Hero))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FavoriteHero Deserialize(Stream stream, FavoriteHero instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FavoriteHero DeserializeLengthDelimited(Stream stream)
		{
			FavoriteHero favoriteHero = new FavoriteHero();
			DeserializeLengthDelimited(stream, favoriteHero);
			return favoriteHero;
		}

		public static FavoriteHero DeserializeLengthDelimited(Stream stream, FavoriteHero instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FavoriteHero Deserialize(Stream stream, FavoriteHero instance, long limit)
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
					instance.ClassId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					if (instance.Hero == null)
					{
						instance.Hero = CardDef.DeserializeLengthDelimited(stream);
					}
					else
					{
						CardDef.DeserializeLengthDelimited(stream, instance.Hero);
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

		public static void Serialize(Stream stream, FavoriteHero instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ClassId);
			if (instance.Hero == null)
			{
				throw new ArgumentNullException("Hero", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.Hero.GetSerializedSize());
			CardDef.Serialize(stream, instance.Hero);
		}

		public uint GetSerializedSize()
		{
			uint num = 0 + ProtocolParser.SizeOfUInt64((ulong)ClassId);
			uint serializedSize = Hero.GetSerializedSize();
			return num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 2;
		}
	}
}
