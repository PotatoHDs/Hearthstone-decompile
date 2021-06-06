using System.IO;
using PegasusShared;

namespace PegasusFSG
{
	public class InnkeeperSetupGatheringResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 508
		}

		public ErrorCode ErrorCode { get; set; }

		public long FsgId { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ ErrorCode.GetHashCode() ^ FsgId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			InnkeeperSetupGatheringResponse innkeeperSetupGatheringResponse = obj as InnkeeperSetupGatheringResponse;
			if (innkeeperSetupGatheringResponse == null)
			{
				return false;
			}
			if (!ErrorCode.Equals(innkeeperSetupGatheringResponse.ErrorCode))
			{
				return false;
			}
			if (!FsgId.Equals(innkeeperSetupGatheringResponse.FsgId))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static InnkeeperSetupGatheringResponse Deserialize(Stream stream, InnkeeperSetupGatheringResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static InnkeeperSetupGatheringResponse DeserializeLengthDelimited(Stream stream)
		{
			InnkeeperSetupGatheringResponse innkeeperSetupGatheringResponse = new InnkeeperSetupGatheringResponse();
			DeserializeLengthDelimited(stream, innkeeperSetupGatheringResponse);
			return innkeeperSetupGatheringResponse;
		}

		public static InnkeeperSetupGatheringResponse DeserializeLengthDelimited(Stream stream, InnkeeperSetupGatheringResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static InnkeeperSetupGatheringResponse Deserialize(Stream stream, InnkeeperSetupGatheringResponse instance, long limit)
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
					instance.ErrorCode = (ErrorCode)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.FsgId = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, InnkeeperSetupGatheringResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ErrorCode);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.FsgId);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)ErrorCode) + ProtocolParser.SizeOfUInt64((ulong)FsgId) + 2;
		}
	}
}
