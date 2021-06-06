using System;
using System.IO;

namespace PegasusUtil
{
	public class BattlegroundsRatingInfoResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 373,
			System = 0
		}

		public BattlegroundsPlayerInfo PlayerInfo { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ PlayerInfo.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			BattlegroundsRatingInfoResponse battlegroundsRatingInfoResponse = obj as BattlegroundsRatingInfoResponse;
			if (battlegroundsRatingInfoResponse == null)
			{
				return false;
			}
			if (!PlayerInfo.Equals(battlegroundsRatingInfoResponse.PlayerInfo))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BattlegroundsRatingInfoResponse Deserialize(Stream stream, BattlegroundsRatingInfoResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BattlegroundsRatingInfoResponse DeserializeLengthDelimited(Stream stream)
		{
			BattlegroundsRatingInfoResponse battlegroundsRatingInfoResponse = new BattlegroundsRatingInfoResponse();
			DeserializeLengthDelimited(stream, battlegroundsRatingInfoResponse);
			return battlegroundsRatingInfoResponse;
		}

		public static BattlegroundsRatingInfoResponse DeserializeLengthDelimited(Stream stream, BattlegroundsRatingInfoResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BattlegroundsRatingInfoResponse Deserialize(Stream stream, BattlegroundsRatingInfoResponse instance, long limit)
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
					if (instance.PlayerInfo == null)
					{
						instance.PlayerInfo = BattlegroundsPlayerInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						BattlegroundsPlayerInfo.DeserializeLengthDelimited(stream, instance.PlayerInfo);
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

		public static void Serialize(Stream stream, BattlegroundsRatingInfoResponse instance)
		{
			if (instance.PlayerInfo == null)
			{
				throw new ArgumentNullException("PlayerInfo", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.PlayerInfo.GetSerializedSize());
			BattlegroundsPlayerInfo.Serialize(stream, instance.PlayerInfo);
		}

		public uint GetSerializedSize()
		{
			uint serializedSize = PlayerInfo.GetSerializedSize();
			return 0 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 1;
		}
	}
}
