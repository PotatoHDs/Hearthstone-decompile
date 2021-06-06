using System.IO;

namespace PegasusUtil
{
	public class HeroXPInfo : IProtoBuf
	{
		public int ClassId { get; set; }

		public int Level { get; set; }

		public long CurrXp { get; set; }

		public long MaxXp { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ ClassId.GetHashCode() ^ Level.GetHashCode() ^ CurrXp.GetHashCode() ^ MaxXp.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			HeroXPInfo heroXPInfo = obj as HeroXPInfo;
			if (heroXPInfo == null)
			{
				return false;
			}
			if (!ClassId.Equals(heroXPInfo.ClassId))
			{
				return false;
			}
			if (!Level.Equals(heroXPInfo.Level))
			{
				return false;
			}
			if (!CurrXp.Equals(heroXPInfo.CurrXp))
			{
				return false;
			}
			if (!MaxXp.Equals(heroXPInfo.MaxXp))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static HeroXPInfo Deserialize(Stream stream, HeroXPInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static HeroXPInfo DeserializeLengthDelimited(Stream stream)
		{
			HeroXPInfo heroXPInfo = new HeroXPInfo();
			DeserializeLengthDelimited(stream, heroXPInfo);
			return heroXPInfo;
		}

		public static HeroXPInfo DeserializeLengthDelimited(Stream stream, HeroXPInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static HeroXPInfo Deserialize(Stream stream, HeroXPInfo instance, long limit)
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
				case 16:
					instance.Level = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.CurrXp = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.MaxXp = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, HeroXPInfo instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ClassId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Level);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.CurrXp);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.MaxXp);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)ClassId) + ProtocolParser.SizeOfUInt64((ulong)Level) + ProtocolParser.SizeOfUInt64((ulong)CurrXp) + ProtocolParser.SizeOfUInt64((ulong)MaxXp) + 4;
		}
	}
}
