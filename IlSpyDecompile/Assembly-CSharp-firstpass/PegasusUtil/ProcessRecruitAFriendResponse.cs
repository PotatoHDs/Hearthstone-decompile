using System.IO;

namespace PegasusUtil
{
	public class ProcessRecruitAFriendResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 342
		}

		public RAFServiceStatus RafServiceStatus { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ RafServiceStatus.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ProcessRecruitAFriendResponse processRecruitAFriendResponse = obj as ProcessRecruitAFriendResponse;
			if (processRecruitAFriendResponse == null)
			{
				return false;
			}
			if (!RafServiceStatus.Equals(processRecruitAFriendResponse.RafServiceStatus))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProcessRecruitAFriendResponse Deserialize(Stream stream, ProcessRecruitAFriendResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProcessRecruitAFriendResponse DeserializeLengthDelimited(Stream stream)
		{
			ProcessRecruitAFriendResponse processRecruitAFriendResponse = new ProcessRecruitAFriendResponse();
			DeserializeLengthDelimited(stream, processRecruitAFriendResponse);
			return processRecruitAFriendResponse;
		}

		public static ProcessRecruitAFriendResponse DeserializeLengthDelimited(Stream stream, ProcessRecruitAFriendResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProcessRecruitAFriendResponse Deserialize(Stream stream, ProcessRecruitAFriendResponse instance, long limit)
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
					instance.RafServiceStatus = (RAFServiceStatus)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ProcessRecruitAFriendResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.RafServiceStatus);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)RafServiceStatus) + 1;
		}
	}
}
