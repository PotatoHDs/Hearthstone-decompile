using System.IO;

namespace PegasusShared
{
	public class ProfileNoticeDeckGranted : IProtoBuf
	{
		public enum NoticeID
		{
			ID = 26
		}

		public bool HasDeckDbiId;

		private int _DeckDbiId;

		public bool HasClassId;

		private int _ClassId;

		public bool HasPlayerDeckId;

		private long _PlayerDeckId;

		public int DeckDbiId
		{
			get
			{
				return _DeckDbiId;
			}
			set
			{
				_DeckDbiId = value;
				HasDeckDbiId = true;
			}
		}

		public int ClassId
		{
			get
			{
				return _ClassId;
			}
			set
			{
				_ClassId = value;
				HasClassId = true;
			}
		}

		public long PlayerDeckId
		{
			get
			{
				return _PlayerDeckId;
			}
			set
			{
				_PlayerDeckId = value;
				HasPlayerDeckId = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasDeckDbiId)
			{
				num ^= DeckDbiId.GetHashCode();
			}
			if (HasClassId)
			{
				num ^= ClassId.GetHashCode();
			}
			if (HasPlayerDeckId)
			{
				num ^= PlayerDeckId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ProfileNoticeDeckGranted profileNoticeDeckGranted = obj as ProfileNoticeDeckGranted;
			if (profileNoticeDeckGranted == null)
			{
				return false;
			}
			if (HasDeckDbiId != profileNoticeDeckGranted.HasDeckDbiId || (HasDeckDbiId && !DeckDbiId.Equals(profileNoticeDeckGranted.DeckDbiId)))
			{
				return false;
			}
			if (HasClassId != profileNoticeDeckGranted.HasClassId || (HasClassId && !ClassId.Equals(profileNoticeDeckGranted.ClassId)))
			{
				return false;
			}
			if (HasPlayerDeckId != profileNoticeDeckGranted.HasPlayerDeckId || (HasPlayerDeckId && !PlayerDeckId.Equals(profileNoticeDeckGranted.PlayerDeckId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileNoticeDeckGranted Deserialize(Stream stream, ProfileNoticeDeckGranted instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileNoticeDeckGranted DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeDeckGranted profileNoticeDeckGranted = new ProfileNoticeDeckGranted();
			DeserializeLengthDelimited(stream, profileNoticeDeckGranted);
			return profileNoticeDeckGranted;
		}

		public static ProfileNoticeDeckGranted DeserializeLengthDelimited(Stream stream, ProfileNoticeDeckGranted instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileNoticeDeckGranted Deserialize(Stream stream, ProfileNoticeDeckGranted instance, long limit)
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
					instance.DeckDbiId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.ClassId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.PlayerDeckId = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ProfileNoticeDeckGranted instance)
		{
			if (instance.HasDeckDbiId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckDbiId);
			}
			if (instance.HasClassId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ClassId);
			}
			if (instance.HasPlayerDeckId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayerDeckId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasDeckDbiId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DeckDbiId);
			}
			if (HasClassId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ClassId);
			}
			if (HasPlayerDeckId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)PlayerDeckId);
			}
			return num;
		}
	}
}
