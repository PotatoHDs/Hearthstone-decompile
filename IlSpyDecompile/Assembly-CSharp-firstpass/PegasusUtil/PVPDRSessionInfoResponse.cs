using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class PVPDRSessionInfoResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 377
		}

		public bool HasCurrentSeason;

		private PVPDRSeasonInfo _CurrentSeason;

		public bool HasSession;

		private PVPDRSessionInfo _Session;

		public ErrorCode ErrorCode { get; set; }

		public PVPDRSeasonInfo CurrentSeason
		{
			get
			{
				return _CurrentSeason;
			}
			set
			{
				_CurrentSeason = value;
				HasCurrentSeason = value != null;
			}
		}

		public PVPDRSessionInfo Session
		{
			get
			{
				return _Session;
			}
			set
			{
				_Session = value;
				HasSession = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= ErrorCode.GetHashCode();
			if (HasCurrentSeason)
			{
				hashCode ^= CurrentSeason.GetHashCode();
			}
			if (HasSession)
			{
				hashCode ^= Session.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			PVPDRSessionInfoResponse pVPDRSessionInfoResponse = obj as PVPDRSessionInfoResponse;
			if (pVPDRSessionInfoResponse == null)
			{
				return false;
			}
			if (!ErrorCode.Equals(pVPDRSessionInfoResponse.ErrorCode))
			{
				return false;
			}
			if (HasCurrentSeason != pVPDRSessionInfoResponse.HasCurrentSeason || (HasCurrentSeason && !CurrentSeason.Equals(pVPDRSessionInfoResponse.CurrentSeason)))
			{
				return false;
			}
			if (HasSession != pVPDRSessionInfoResponse.HasSession || (HasSession && !Session.Equals(pVPDRSessionInfoResponse.Session)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PVPDRSessionInfoResponse Deserialize(Stream stream, PVPDRSessionInfoResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PVPDRSessionInfoResponse DeserializeLengthDelimited(Stream stream)
		{
			PVPDRSessionInfoResponse pVPDRSessionInfoResponse = new PVPDRSessionInfoResponse();
			DeserializeLengthDelimited(stream, pVPDRSessionInfoResponse);
			return pVPDRSessionInfoResponse;
		}

		public static PVPDRSessionInfoResponse DeserializeLengthDelimited(Stream stream, PVPDRSessionInfoResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PVPDRSessionInfoResponse Deserialize(Stream stream, PVPDRSessionInfoResponse instance, long limit)
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
				case 18:
					if (instance.CurrentSeason == null)
					{
						instance.CurrentSeason = PVPDRSeasonInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						PVPDRSeasonInfo.DeserializeLengthDelimited(stream, instance.CurrentSeason);
					}
					continue;
				case 26:
					if (instance.Session == null)
					{
						instance.Session = PVPDRSessionInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						PVPDRSessionInfo.DeserializeLengthDelimited(stream, instance.Session);
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

		public static void Serialize(Stream stream, PVPDRSessionInfoResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ErrorCode);
			if (instance.HasCurrentSeason)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.CurrentSeason.GetSerializedSize());
				PVPDRSeasonInfo.Serialize(stream, instance.CurrentSeason);
			}
			if (instance.HasSession)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Session.GetSerializedSize());
				PVPDRSessionInfo.Serialize(stream, instance.Session);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)ErrorCode);
			if (HasCurrentSeason)
			{
				num++;
				uint serializedSize = CurrentSeason.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasSession)
			{
				num++;
				uint serializedSize2 = Session.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num + 1;
		}
	}
}
