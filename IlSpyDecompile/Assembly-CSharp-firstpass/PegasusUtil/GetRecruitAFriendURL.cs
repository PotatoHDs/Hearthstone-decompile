using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class GetRecruitAFriendURL : IProtoBuf
	{
		public enum PacketID
		{
			ID = 335,
			System = 2
		}

		public Platform Platform { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Platform.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			GetRecruitAFriendURL getRecruitAFriendURL = obj as GetRecruitAFriendURL;
			if (getRecruitAFriendURL == null)
			{
				return false;
			}
			if (!Platform.Equals(getRecruitAFriendURL.Platform))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetRecruitAFriendURL Deserialize(Stream stream, GetRecruitAFriendURL instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetRecruitAFriendURL DeserializeLengthDelimited(Stream stream)
		{
			GetRecruitAFriendURL getRecruitAFriendURL = new GetRecruitAFriendURL();
			DeserializeLengthDelimited(stream, getRecruitAFriendURL);
			return getRecruitAFriendURL;
		}

		public static GetRecruitAFriendURL DeserializeLengthDelimited(Stream stream, GetRecruitAFriendURL instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetRecruitAFriendURL Deserialize(Stream stream, GetRecruitAFriendURL instance, long limit)
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
					if (instance.Platform == null)
					{
						instance.Platform = Platform.DeserializeLengthDelimited(stream);
					}
					else
					{
						Platform.DeserializeLengthDelimited(stream, instance.Platform);
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

		public static void Serialize(Stream stream, GetRecruitAFriendURL instance)
		{
			if (instance.Platform == null)
			{
				throw new ArgumentNullException("Platform", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Platform.GetSerializedSize());
			Platform.Serialize(stream, instance.Platform);
		}

		public uint GetSerializedSize()
		{
			uint serializedSize = Platform.GetSerializedSize();
			return 0 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 1;
		}
	}
}
