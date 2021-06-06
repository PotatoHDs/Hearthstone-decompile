using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class LeaguePromoteSelfResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 368
		}

		public ErrorCode ErrorCode { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ ErrorCode.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			LeaguePromoteSelfResponse leaguePromoteSelfResponse = obj as LeaguePromoteSelfResponse;
			if (leaguePromoteSelfResponse == null)
			{
				return false;
			}
			if (!ErrorCode.Equals(leaguePromoteSelfResponse.ErrorCode))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static LeaguePromoteSelfResponse Deserialize(Stream stream, LeaguePromoteSelfResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static LeaguePromoteSelfResponse DeserializeLengthDelimited(Stream stream)
		{
			LeaguePromoteSelfResponse leaguePromoteSelfResponse = new LeaguePromoteSelfResponse();
			DeserializeLengthDelimited(stream, leaguePromoteSelfResponse);
			return leaguePromoteSelfResponse;
		}

		public static LeaguePromoteSelfResponse DeserializeLengthDelimited(Stream stream, LeaguePromoteSelfResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static LeaguePromoteSelfResponse Deserialize(Stream stream, LeaguePromoteSelfResponse instance, long limit)
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
					instance.ErrorCode = (ErrorCode)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, LeaguePromoteSelfResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ErrorCode);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)ErrorCode) + 1;
		}
	}
}
