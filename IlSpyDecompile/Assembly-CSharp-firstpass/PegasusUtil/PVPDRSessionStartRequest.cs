using System.IO;

namespace PegasusUtil
{
	public class PVPDRSessionStartRequest : IProtoBuf
	{
		public enum PacketID
		{
			ID = 382,
			System = 0
		}

		public bool HasPaidEntry;

		private bool _PaidEntry;

		public bool PaidEntry
		{
			get
			{
				return _PaidEntry;
			}
			set
			{
				_PaidEntry = value;
				HasPaidEntry = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasPaidEntry)
			{
				num ^= PaidEntry.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PVPDRSessionStartRequest pVPDRSessionStartRequest = obj as PVPDRSessionStartRequest;
			if (pVPDRSessionStartRequest == null)
			{
				return false;
			}
			if (HasPaidEntry != pVPDRSessionStartRequest.HasPaidEntry || (HasPaidEntry && !PaidEntry.Equals(pVPDRSessionStartRequest.PaidEntry)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PVPDRSessionStartRequest Deserialize(Stream stream, PVPDRSessionStartRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PVPDRSessionStartRequest DeserializeLengthDelimited(Stream stream)
		{
			PVPDRSessionStartRequest pVPDRSessionStartRequest = new PVPDRSessionStartRequest();
			DeserializeLengthDelimited(stream, pVPDRSessionStartRequest);
			return pVPDRSessionStartRequest;
		}

		public static PVPDRSessionStartRequest DeserializeLengthDelimited(Stream stream, PVPDRSessionStartRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PVPDRSessionStartRequest Deserialize(Stream stream, PVPDRSessionStartRequest instance, long limit)
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
					instance.PaidEntry = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, PVPDRSessionStartRequest instance)
		{
			if (instance.HasPaidEntry)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.PaidEntry);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasPaidEntry)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
