using System.IO;

namespace PegasusUtil
{
	public class PVPDRStatsInfoResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 379,
			System = 0
		}

		public bool HasRating;

		private int _Rating;

		public bool HasHighWatermark;

		private int _HighWatermark;

		public bool HasPaidRating;

		private int _PaidRating;

		public int Rating
		{
			get
			{
				return _Rating;
			}
			set
			{
				_Rating = value;
				HasRating = true;
			}
		}

		public int HighWatermark
		{
			get
			{
				return _HighWatermark;
			}
			set
			{
				_HighWatermark = value;
				HasHighWatermark = true;
			}
		}

		public int PaidRating
		{
			get
			{
				return _PaidRating;
			}
			set
			{
				_PaidRating = value;
				HasPaidRating = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasRating)
			{
				num ^= Rating.GetHashCode();
			}
			if (HasHighWatermark)
			{
				num ^= HighWatermark.GetHashCode();
			}
			if (HasPaidRating)
			{
				num ^= PaidRating.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PVPDRStatsInfoResponse pVPDRStatsInfoResponse = obj as PVPDRStatsInfoResponse;
			if (pVPDRStatsInfoResponse == null)
			{
				return false;
			}
			if (HasRating != pVPDRStatsInfoResponse.HasRating || (HasRating && !Rating.Equals(pVPDRStatsInfoResponse.Rating)))
			{
				return false;
			}
			if (HasHighWatermark != pVPDRStatsInfoResponse.HasHighWatermark || (HasHighWatermark && !HighWatermark.Equals(pVPDRStatsInfoResponse.HighWatermark)))
			{
				return false;
			}
			if (HasPaidRating != pVPDRStatsInfoResponse.HasPaidRating || (HasPaidRating && !PaidRating.Equals(pVPDRStatsInfoResponse.PaidRating)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PVPDRStatsInfoResponse Deserialize(Stream stream, PVPDRStatsInfoResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PVPDRStatsInfoResponse DeserializeLengthDelimited(Stream stream)
		{
			PVPDRStatsInfoResponse pVPDRStatsInfoResponse = new PVPDRStatsInfoResponse();
			DeserializeLengthDelimited(stream, pVPDRStatsInfoResponse);
			return pVPDRStatsInfoResponse;
		}

		public static PVPDRStatsInfoResponse DeserializeLengthDelimited(Stream stream, PVPDRStatsInfoResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PVPDRStatsInfoResponse Deserialize(Stream stream, PVPDRStatsInfoResponse instance, long limit)
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
					instance.Rating = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.HighWatermark = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.PaidRating = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, PVPDRStatsInfoResponse instance)
		{
			if (instance.HasRating)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Rating);
			}
			if (instance.HasHighWatermark)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.HighWatermark);
			}
			if (instance.HasPaidRating)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PaidRating);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasRating)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Rating);
			}
			if (HasHighWatermark)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)HighWatermark);
			}
			if (HasPaidRating)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)PaidRating);
			}
			return num;
		}
	}
}
