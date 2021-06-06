using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class FavoriteHeroesResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 318
		}

		private List<FavoriteHero> _FavoriteHeroes = new List<FavoriteHero>();

		public List<FavoriteHero> FavoriteHeroes
		{
			get
			{
				return _FavoriteHeroes;
			}
			set
			{
				_FavoriteHeroes = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (FavoriteHero favoriteHero in FavoriteHeroes)
			{
				num ^= favoriteHero.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FavoriteHeroesResponse favoriteHeroesResponse = obj as FavoriteHeroesResponse;
			if (favoriteHeroesResponse == null)
			{
				return false;
			}
			if (FavoriteHeroes.Count != favoriteHeroesResponse.FavoriteHeroes.Count)
			{
				return false;
			}
			for (int i = 0; i < FavoriteHeroes.Count; i++)
			{
				if (!FavoriteHeroes[i].Equals(favoriteHeroesResponse.FavoriteHeroes[i]))
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

		public static FavoriteHeroesResponse Deserialize(Stream stream, FavoriteHeroesResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FavoriteHeroesResponse DeserializeLengthDelimited(Stream stream)
		{
			FavoriteHeroesResponse favoriteHeroesResponse = new FavoriteHeroesResponse();
			DeserializeLengthDelimited(stream, favoriteHeroesResponse);
			return favoriteHeroesResponse;
		}

		public static FavoriteHeroesResponse DeserializeLengthDelimited(Stream stream, FavoriteHeroesResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FavoriteHeroesResponse Deserialize(Stream stream, FavoriteHeroesResponse instance, long limit)
		{
			if (instance.FavoriteHeroes == null)
			{
				instance.FavoriteHeroes = new List<FavoriteHero>();
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
					instance.FavoriteHeroes.Add(FavoriteHero.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, FavoriteHeroesResponse instance)
		{
			if (instance.FavoriteHeroes.Count <= 0)
			{
				return;
			}
			foreach (FavoriteHero favoriteHero in instance.FavoriteHeroes)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, favoriteHero.GetSerializedSize());
				FavoriteHero.Serialize(stream, favoriteHero);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (FavoriteHeroes.Count > 0)
			{
				foreach (FavoriteHero favoriteHero in FavoriteHeroes)
				{
					num++;
					uint serializedSize = favoriteHero.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
