using System.IO;

namespace PegasusUtil
{
	public class GenericRequest : IProtoBuf
	{
		public bool HasGenericData;

		private GenericData _GenericData;

		public bool HasRequestSubId;

		private int _RequestSubId;

		public int RequestId { get; set; }

		public GenericData GenericData
		{
			get
			{
				return _GenericData;
			}
			set
			{
				_GenericData = value;
				HasGenericData = value != null;
			}
		}

		public int RequestSubId
		{
			get
			{
				return _RequestSubId;
			}
			set
			{
				_RequestSubId = value;
				HasRequestSubId = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= RequestId.GetHashCode();
			if (HasGenericData)
			{
				hashCode ^= GenericData.GetHashCode();
			}
			if (HasRequestSubId)
			{
				hashCode ^= RequestSubId.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			GenericRequest genericRequest = obj as GenericRequest;
			if (genericRequest == null)
			{
				return false;
			}
			if (!RequestId.Equals(genericRequest.RequestId))
			{
				return false;
			}
			if (HasGenericData != genericRequest.HasGenericData || (HasGenericData && !GenericData.Equals(genericRequest.GenericData)))
			{
				return false;
			}
			if (HasRequestSubId != genericRequest.HasRequestSubId || (HasRequestSubId && !RequestSubId.Equals(genericRequest.RequestSubId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GenericRequest Deserialize(Stream stream, GenericRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GenericRequest DeserializeLengthDelimited(Stream stream)
		{
			GenericRequest genericRequest = new GenericRequest();
			DeserializeLengthDelimited(stream, genericRequest);
			return genericRequest;
		}

		public static GenericRequest DeserializeLengthDelimited(Stream stream, GenericRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GenericRequest Deserialize(Stream stream, GenericRequest instance, long limit)
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
					instance.RequestId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					if (instance.GenericData == null)
					{
						instance.GenericData = GenericData.DeserializeLengthDelimited(stream);
					}
					else
					{
						GenericData.DeserializeLengthDelimited(stream, instance.GenericData);
					}
					continue;
				case 24:
					instance.RequestSubId = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, GenericRequest instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.RequestId);
			if (instance.HasGenericData)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GenericData.GetSerializedSize());
				GenericData.Serialize(stream, instance.GenericData);
			}
			if (instance.HasRequestSubId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.RequestSubId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)RequestId);
			if (HasGenericData)
			{
				num++;
				uint serializedSize = GenericData.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasRequestSubId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)RequestSubId);
			}
			return num + 1;
		}
	}
}
