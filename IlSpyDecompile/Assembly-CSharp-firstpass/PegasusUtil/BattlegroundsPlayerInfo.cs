using System.IO;

namespace PegasusUtil
{
	public class BattlegroundsPlayerInfo : IProtoBuf
	{
		public int Rating { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Rating.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			BattlegroundsPlayerInfo battlegroundsPlayerInfo = obj as BattlegroundsPlayerInfo;
			if (battlegroundsPlayerInfo == null)
			{
				return false;
			}
			if (!Rating.Equals(battlegroundsPlayerInfo.Rating))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BattlegroundsPlayerInfo Deserialize(Stream stream, BattlegroundsPlayerInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BattlegroundsPlayerInfo DeserializeLengthDelimited(Stream stream)
		{
			BattlegroundsPlayerInfo battlegroundsPlayerInfo = new BattlegroundsPlayerInfo();
			DeserializeLengthDelimited(stream, battlegroundsPlayerInfo);
			return battlegroundsPlayerInfo;
		}

		public static BattlegroundsPlayerInfo DeserializeLengthDelimited(Stream stream, BattlegroundsPlayerInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BattlegroundsPlayerInfo Deserialize(Stream stream, BattlegroundsPlayerInfo instance, long limit)
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
					instance.Rating = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, BattlegroundsPlayerInfo instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Rating);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)Rating) + 1;
		}
	}
}
