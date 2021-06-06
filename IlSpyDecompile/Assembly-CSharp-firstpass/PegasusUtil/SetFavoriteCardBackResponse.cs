using System.IO;

namespace PegasusUtil
{
	public class SetFavoriteCardBackResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 292
		}

		public bool Success { get; set; }

		public int CardBack { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Success.GetHashCode() ^ CardBack.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			SetFavoriteCardBackResponse setFavoriteCardBackResponse = obj as SetFavoriteCardBackResponse;
			if (setFavoriteCardBackResponse == null)
			{
				return false;
			}
			if (!Success.Equals(setFavoriteCardBackResponse.Success))
			{
				return false;
			}
			if (!CardBack.Equals(setFavoriteCardBackResponse.CardBack))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SetFavoriteCardBackResponse Deserialize(Stream stream, SetFavoriteCardBackResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SetFavoriteCardBackResponse DeserializeLengthDelimited(Stream stream)
		{
			SetFavoriteCardBackResponse setFavoriteCardBackResponse = new SetFavoriteCardBackResponse();
			DeserializeLengthDelimited(stream, setFavoriteCardBackResponse);
			return setFavoriteCardBackResponse;
		}

		public static SetFavoriteCardBackResponse DeserializeLengthDelimited(Stream stream, SetFavoriteCardBackResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SetFavoriteCardBackResponse Deserialize(Stream stream, SetFavoriteCardBackResponse instance, long limit)
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
				case 16:
					instance.CardBack = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, SetFavoriteCardBackResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteBool(stream, instance.Success);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.CardBack);
		}

		public uint GetSerializedSize()
		{
			return 0 + 1 + ProtocolParser.SizeOfUInt64((ulong)CardBack) + 2;
		}
	}
}
