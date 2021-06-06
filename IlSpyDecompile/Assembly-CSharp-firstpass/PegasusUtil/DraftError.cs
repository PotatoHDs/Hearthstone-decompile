using System.IO;

namespace PegasusUtil
{
	public class DraftError : IProtoBuf
	{
		public enum PacketID
		{
			ID = 251
		}

		public enum ErrorCode
		{
			DE_UNKNOWN,
			DE_NO_LICENSE,
			DE_RETIRE_FIRST,
			DE_NOT_IN_DRAFT,
			DE_BAD_DECK,
			DE_BAD_SLOT,
			DE_BAD_INDEX,
			DE_NOT_IN_DRAFT_BUT_COULD_BE,
			DE_FEATURE_DISABLED,
			DE_SEASON_INCREMENTED
		}

		public bool HasNumTicketsOwned;

		private int _NumTicketsOwned;

		public ErrorCode ErrorCode_ { get; set; }

		public int NumTicketsOwned
		{
			get
			{
				return _NumTicketsOwned;
			}
			set
			{
				_NumTicketsOwned = value;
				HasNumTicketsOwned = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= ErrorCode_.GetHashCode();
			if (HasNumTicketsOwned)
			{
				hashCode ^= NumTicketsOwned.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			DraftError draftError = obj as DraftError;
			if (draftError == null)
			{
				return false;
			}
			if (!ErrorCode_.Equals(draftError.ErrorCode_))
			{
				return false;
			}
			if (HasNumTicketsOwned != draftError.HasNumTicketsOwned || (HasNumTicketsOwned && !NumTicketsOwned.Equals(draftError.NumTicketsOwned)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DraftError Deserialize(Stream stream, DraftError instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DraftError DeserializeLengthDelimited(Stream stream)
		{
			DraftError draftError = new DraftError();
			DeserializeLengthDelimited(stream, draftError);
			return draftError;
		}

		public static DraftError DeserializeLengthDelimited(Stream stream, DraftError instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DraftError Deserialize(Stream stream, DraftError instance, long limit)
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
					instance.ErrorCode_ = (ErrorCode)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.NumTicketsOwned = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, DraftError instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ErrorCode_);
			if (instance.HasNumTicketsOwned)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.NumTicketsOwned);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)ErrorCode_);
			if (HasNumTicketsOwned)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)NumTicketsOwned);
			}
			return num + 1;
		}
	}
}
