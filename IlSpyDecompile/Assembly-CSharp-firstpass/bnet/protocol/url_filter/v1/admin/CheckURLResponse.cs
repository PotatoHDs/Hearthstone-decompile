using System.IO;
using bnet.protocol.Types;

namespace bnet.protocol.url_filter.v1.admin
{
	public class CheckURLResponse : IProtoBuf
	{
		public bool HasThreatType;

		private ThreatType _ThreatType;

		public ThreatType ThreatType
		{
			get
			{
				return _ThreatType;
			}
			set
			{
				_ThreatType = value;
				HasThreatType = true;
			}
		}

		public bool IsInitialized => true;

		public void SetThreatType(ThreatType val)
		{
			ThreatType = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasThreatType)
			{
				num ^= ThreatType.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CheckURLResponse checkURLResponse = obj as CheckURLResponse;
			if (checkURLResponse == null)
			{
				return false;
			}
			if (HasThreatType != checkURLResponse.HasThreatType || (HasThreatType && !ThreatType.Equals(checkURLResponse.ThreatType)))
			{
				return false;
			}
			return true;
		}

		public static CheckURLResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CheckURLResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CheckURLResponse Deserialize(Stream stream, CheckURLResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CheckURLResponse DeserializeLengthDelimited(Stream stream)
		{
			CheckURLResponse checkURLResponse = new CheckURLResponse();
			DeserializeLengthDelimited(stream, checkURLResponse);
			return checkURLResponse;
		}

		public static CheckURLResponse DeserializeLengthDelimited(Stream stream, CheckURLResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CheckURLResponse Deserialize(Stream stream, CheckURLResponse instance, long limit)
		{
			instance.ThreatType = ThreatType.THREAT_TYPE_OK;
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
					instance.ThreatType = (ThreatType)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, CheckURLResponse instance)
		{
			if (instance.HasThreatType)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ThreatType);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasThreatType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ThreatType);
			}
			return num;
		}
	}
}
