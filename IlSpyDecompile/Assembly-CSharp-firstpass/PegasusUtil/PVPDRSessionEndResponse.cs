using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class PVPDRSessionEndResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 389
		}

		public bool HasNewRating;

		private int _NewRating;

		public bool HasNewPaidRating;

		private int _NewPaidRating;

		public ErrorCode ErrorCode { get; set; }

		public int NewRating
		{
			get
			{
				return _NewRating;
			}
			set
			{
				_NewRating = value;
				HasNewRating = true;
			}
		}

		public int NewPaidRating
		{
			get
			{
				return _NewPaidRating;
			}
			set
			{
				_NewPaidRating = value;
				HasNewPaidRating = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= ErrorCode.GetHashCode();
			if (HasNewRating)
			{
				hashCode ^= NewRating.GetHashCode();
			}
			if (HasNewPaidRating)
			{
				hashCode ^= NewPaidRating.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			PVPDRSessionEndResponse pVPDRSessionEndResponse = obj as PVPDRSessionEndResponse;
			if (pVPDRSessionEndResponse == null)
			{
				return false;
			}
			if (!ErrorCode.Equals(pVPDRSessionEndResponse.ErrorCode))
			{
				return false;
			}
			if (HasNewRating != pVPDRSessionEndResponse.HasNewRating || (HasNewRating && !NewRating.Equals(pVPDRSessionEndResponse.NewRating)))
			{
				return false;
			}
			if (HasNewPaidRating != pVPDRSessionEndResponse.HasNewPaidRating || (HasNewPaidRating && !NewPaidRating.Equals(pVPDRSessionEndResponse.NewPaidRating)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PVPDRSessionEndResponse Deserialize(Stream stream, PVPDRSessionEndResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PVPDRSessionEndResponse DeserializeLengthDelimited(Stream stream)
		{
			PVPDRSessionEndResponse pVPDRSessionEndResponse = new PVPDRSessionEndResponse();
			DeserializeLengthDelimited(stream, pVPDRSessionEndResponse);
			return pVPDRSessionEndResponse;
		}

		public static PVPDRSessionEndResponse DeserializeLengthDelimited(Stream stream, PVPDRSessionEndResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PVPDRSessionEndResponse Deserialize(Stream stream, PVPDRSessionEndResponse instance, long limit)
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
					instance.NewRating = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.NewPaidRating = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, PVPDRSessionEndResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ErrorCode);
			if (instance.HasNewRating)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.NewRating);
			}
			if (instance.HasNewPaidRating)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.NewPaidRating);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)ErrorCode);
			if (HasNewRating)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)NewRating);
			}
			if (HasNewPaidRating)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)NewPaidRating);
			}
			return num + 1;
		}
	}
}
