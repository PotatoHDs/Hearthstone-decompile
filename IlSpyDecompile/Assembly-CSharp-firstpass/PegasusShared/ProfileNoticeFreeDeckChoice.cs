using System.IO;

namespace PegasusShared
{
	public class ProfileNoticeFreeDeckChoice : IProtoBuf
	{
		public enum NoticeID
		{
			ID = 24
		}

		public bool HasPlayerId;

		private long _PlayerId;

		public long PlayerId
		{
			get
			{
				return _PlayerId;
			}
			set
			{
				_PlayerId = value;
				HasPlayerId = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasPlayerId)
			{
				num ^= PlayerId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ProfileNoticeFreeDeckChoice profileNoticeFreeDeckChoice = obj as ProfileNoticeFreeDeckChoice;
			if (profileNoticeFreeDeckChoice == null)
			{
				return false;
			}
			if (HasPlayerId != profileNoticeFreeDeckChoice.HasPlayerId || (HasPlayerId && !PlayerId.Equals(profileNoticeFreeDeckChoice.PlayerId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileNoticeFreeDeckChoice Deserialize(Stream stream, ProfileNoticeFreeDeckChoice instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileNoticeFreeDeckChoice DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeFreeDeckChoice profileNoticeFreeDeckChoice = new ProfileNoticeFreeDeckChoice();
			DeserializeLengthDelimited(stream, profileNoticeFreeDeckChoice);
			return profileNoticeFreeDeckChoice;
		}

		public static ProfileNoticeFreeDeckChoice DeserializeLengthDelimited(Stream stream, ProfileNoticeFreeDeckChoice instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileNoticeFreeDeckChoice Deserialize(Stream stream, ProfileNoticeFreeDeckChoice instance, long limit)
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
					instance.PlayerId = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ProfileNoticeFreeDeckChoice instance)
		{
			if (instance.HasPlayerId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayerId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasPlayerId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)PlayerId);
			}
			return num;
		}
	}
}
