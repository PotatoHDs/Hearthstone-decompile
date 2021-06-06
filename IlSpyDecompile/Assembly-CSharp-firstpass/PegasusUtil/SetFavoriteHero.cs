using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class SetFavoriteHero : IProtoBuf
	{
		public enum PacketID
		{
			ID = 319,
			System = 0
		}

		public FavoriteHero FavoriteHero { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ FavoriteHero.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			SetFavoriteHero setFavoriteHero = obj as SetFavoriteHero;
			if (setFavoriteHero == null)
			{
				return false;
			}
			if (!FavoriteHero.Equals(setFavoriteHero.FavoriteHero))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SetFavoriteHero Deserialize(Stream stream, SetFavoriteHero instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SetFavoriteHero DeserializeLengthDelimited(Stream stream)
		{
			SetFavoriteHero setFavoriteHero = new SetFavoriteHero();
			DeserializeLengthDelimited(stream, setFavoriteHero);
			return setFavoriteHero;
		}

		public static SetFavoriteHero DeserializeLengthDelimited(Stream stream, SetFavoriteHero instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SetFavoriteHero Deserialize(Stream stream, SetFavoriteHero instance, long limit)
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
					if (instance.FavoriteHero == null)
					{
						instance.FavoriteHero = FavoriteHero.DeserializeLengthDelimited(stream);
					}
					else
					{
						FavoriteHero.DeserializeLengthDelimited(stream, instance.FavoriteHero);
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

		public static void Serialize(Stream stream, SetFavoriteHero instance)
		{
			if (instance.FavoriteHero == null)
			{
				throw new ArgumentNullException("FavoriteHero", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.FavoriteHero.GetSerializedSize());
			FavoriteHero.Serialize(stream, instance.FavoriteHero);
		}

		public uint GetSerializedSize()
		{
			uint serializedSize = FavoriteHero.GetSerializedSize();
			return 0 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 1;
		}
	}
}
