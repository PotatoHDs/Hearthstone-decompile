using System.IO;

namespace PegasusShared
{
	public class ProfileNoticeDeckRemoved : IProtoBuf
	{
		public enum NoticeID
		{
			ID = 25
		}

		public bool HasDeckId;

		private long _DeckId;

		public long DeckId
		{
			get
			{
				return _DeckId;
			}
			set
			{
				_DeckId = value;
				HasDeckId = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasDeckId)
			{
				num ^= DeckId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ProfileNoticeDeckRemoved profileNoticeDeckRemoved = obj as ProfileNoticeDeckRemoved;
			if (profileNoticeDeckRemoved == null)
			{
				return false;
			}
			if (HasDeckId != profileNoticeDeckRemoved.HasDeckId || (HasDeckId && !DeckId.Equals(profileNoticeDeckRemoved.DeckId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileNoticeDeckRemoved Deserialize(Stream stream, ProfileNoticeDeckRemoved instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileNoticeDeckRemoved DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeDeckRemoved profileNoticeDeckRemoved = new ProfileNoticeDeckRemoved();
			DeserializeLengthDelimited(stream, profileNoticeDeckRemoved);
			return profileNoticeDeckRemoved;
		}

		public static ProfileNoticeDeckRemoved DeserializeLengthDelimited(Stream stream, ProfileNoticeDeckRemoved instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileNoticeDeckRemoved Deserialize(Stream stream, ProfileNoticeDeckRemoved instance, long limit)
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
					instance.DeckId = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ProfileNoticeDeckRemoved instance)
		{
			if (instance.HasDeckId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasDeckId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DeckId);
			}
			return num;
		}
	}
}
