using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class SetFavoriteHeroResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 320
		}

		public bool HasFavoriteHero;

		private FavoriteHero _FavoriteHero;

		public bool Success { get; set; }

		public FavoriteHero FavoriteHero
		{
			get
			{
				return _FavoriteHero;
			}
			set
			{
				_FavoriteHero = value;
				HasFavoriteHero = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Success.GetHashCode();
			if (HasFavoriteHero)
			{
				hashCode ^= FavoriteHero.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			SetFavoriteHeroResponse setFavoriteHeroResponse = obj as SetFavoriteHeroResponse;
			if (setFavoriteHeroResponse == null)
			{
				return false;
			}
			if (!Success.Equals(setFavoriteHeroResponse.Success))
			{
				return false;
			}
			if (HasFavoriteHero != setFavoriteHeroResponse.HasFavoriteHero || (HasFavoriteHero && !FavoriteHero.Equals(setFavoriteHeroResponse.FavoriteHero)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SetFavoriteHeroResponse Deserialize(Stream stream, SetFavoriteHeroResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SetFavoriteHeroResponse DeserializeLengthDelimited(Stream stream)
		{
			SetFavoriteHeroResponse setFavoriteHeroResponse = new SetFavoriteHeroResponse();
			DeserializeLengthDelimited(stream, setFavoriteHeroResponse);
			return setFavoriteHeroResponse;
		}

		public static SetFavoriteHeroResponse DeserializeLengthDelimited(Stream stream, SetFavoriteHeroResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SetFavoriteHeroResponse Deserialize(Stream stream, SetFavoriteHeroResponse instance, long limit)
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
					instance.Success = ProtocolParser.ReadBool(stream);
					continue;
				case 18:
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

		public static void Serialize(Stream stream, SetFavoriteHeroResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteBool(stream, instance.Success);
			if (instance.HasFavoriteHero)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.FavoriteHero.GetSerializedSize());
				FavoriteHero.Serialize(stream, instance.FavoriteHero);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num++;
			if (HasFavoriteHero)
			{
				num++;
				uint serializedSize = FavoriteHero.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num + 1;
		}
	}
}
