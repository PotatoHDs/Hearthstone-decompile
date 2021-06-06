using System.IO;

namespace PegasusShared
{
	public class ProfileNoticeMiniSetGranted : IProtoBuf
	{
		public enum NoticeID
		{
			ID = 27
		}

		public bool HasMiniSetId;

		private int _MiniSetId;

		public int MiniSetId
		{
			get
			{
				return _MiniSetId;
			}
			set
			{
				_MiniSetId = value;
				HasMiniSetId = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasMiniSetId)
			{
				num ^= MiniSetId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ProfileNoticeMiniSetGranted profileNoticeMiniSetGranted = obj as ProfileNoticeMiniSetGranted;
			if (profileNoticeMiniSetGranted == null)
			{
				return false;
			}
			if (HasMiniSetId != profileNoticeMiniSetGranted.HasMiniSetId || (HasMiniSetId && !MiniSetId.Equals(profileNoticeMiniSetGranted.MiniSetId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileNoticeMiniSetGranted Deserialize(Stream stream, ProfileNoticeMiniSetGranted instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileNoticeMiniSetGranted DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeMiniSetGranted profileNoticeMiniSetGranted = new ProfileNoticeMiniSetGranted();
			DeserializeLengthDelimited(stream, profileNoticeMiniSetGranted);
			return profileNoticeMiniSetGranted;
		}

		public static ProfileNoticeMiniSetGranted DeserializeLengthDelimited(Stream stream, ProfileNoticeMiniSetGranted instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileNoticeMiniSetGranted Deserialize(Stream stream, ProfileNoticeMiniSetGranted instance, long limit)
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
					instance.MiniSetId = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ProfileNoticeMiniSetGranted instance)
		{
			if (instance.HasMiniSetId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.MiniSetId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasMiniSetId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)MiniSetId);
			}
			return num;
		}
	}
}
