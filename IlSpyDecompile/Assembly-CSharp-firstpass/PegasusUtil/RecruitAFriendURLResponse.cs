using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	public class RecruitAFriendURLResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 336
		}

		public string RafUrl { get; set; }

		public RAFServiceStatus RafServiceStatus { get; set; }

		public string RafUrlFull { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ RafUrl.GetHashCode() ^ RafServiceStatus.GetHashCode() ^ RafUrlFull.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			RecruitAFriendURLResponse recruitAFriendURLResponse = obj as RecruitAFriendURLResponse;
			if (recruitAFriendURLResponse == null)
			{
				return false;
			}
			if (!RafUrl.Equals(recruitAFriendURLResponse.RafUrl))
			{
				return false;
			}
			if (!RafServiceStatus.Equals(recruitAFriendURLResponse.RafServiceStatus))
			{
				return false;
			}
			if (!RafUrlFull.Equals(recruitAFriendURLResponse.RafUrlFull))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RecruitAFriendURLResponse Deserialize(Stream stream, RecruitAFriendURLResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RecruitAFriendURLResponse DeserializeLengthDelimited(Stream stream)
		{
			RecruitAFriendURLResponse recruitAFriendURLResponse = new RecruitAFriendURLResponse();
			DeserializeLengthDelimited(stream, recruitAFriendURLResponse);
			return recruitAFriendURLResponse;
		}

		public static RecruitAFriendURLResponse DeserializeLengthDelimited(Stream stream, RecruitAFriendURLResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RecruitAFriendURLResponse Deserialize(Stream stream, RecruitAFriendURLResponse instance, long limit)
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
					instance.RafUrl = ProtocolParser.ReadString(stream);
					continue;
				case 16:
					instance.RafServiceStatus = (RAFServiceStatus)ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					instance.RafUrlFull = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, RecruitAFriendURLResponse instance)
		{
			if (instance.RafUrl == null)
			{
				throw new ArgumentNullException("RafUrl", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.RafUrl));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.RafServiceStatus);
			if (instance.RafUrlFull == null)
			{
				throw new ArgumentNullException("RafUrlFull", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.RafUrlFull));
		}

		public uint GetSerializedSize()
		{
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(RafUrl);
			uint num = 0 + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + ProtocolParser.SizeOfUInt64((ulong)RafServiceStatus);
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(RafUrlFull);
			return num + (ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2) + 3;
		}
	}
}
