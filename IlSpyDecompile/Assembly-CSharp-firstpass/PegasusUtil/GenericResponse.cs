using System.IO;

namespace PegasusUtil
{
	public class GenericResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 326
		}

		public enum Result
		{
			RESULT_OK = 0,
			RESULT_REQUEST_IN_PROCESS = 1,
			RESULT_REQUEST_COMPLETE = 2,
			RESULT_UNKNOWN_ERROR = 100,
			RESULT_INTERNAL_ERROR = 101,
			RESULT_DB_ERROR = 102,
			RESULT_INVALID_REQUEST = 103,
			RESULT_LOGIN_LOAD = 104,
			RESULT_DATA_MIGRATION_OR_PLAYER_ID_ERROR = 105,
			RESULT_INTERNAL_RPC_ERROR = 106,
			RESULT_DATA_MIGRATION_REQUIRED = 107
		}

		public bool HasRequestSubId;

		private int _RequestSubId;

		public bool HasGenericData;

		private GenericData _GenericData;

		public Result ResultCode { get; set; }

		public int RequestId { get; set; }

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

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= ResultCode.GetHashCode();
			hashCode ^= RequestId.GetHashCode();
			if (HasRequestSubId)
			{
				hashCode ^= RequestSubId.GetHashCode();
			}
			if (HasGenericData)
			{
				hashCode ^= GenericData.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			GenericResponse genericResponse = obj as GenericResponse;
			if (genericResponse == null)
			{
				return false;
			}
			if (!ResultCode.Equals(genericResponse.ResultCode))
			{
				return false;
			}
			if (!RequestId.Equals(genericResponse.RequestId))
			{
				return false;
			}
			if (HasRequestSubId != genericResponse.HasRequestSubId || (HasRequestSubId && !RequestSubId.Equals(genericResponse.RequestSubId)))
			{
				return false;
			}
			if (HasGenericData != genericResponse.HasGenericData || (HasGenericData && !GenericData.Equals(genericResponse.GenericData)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GenericResponse Deserialize(Stream stream, GenericResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GenericResponse DeserializeLengthDelimited(Stream stream)
		{
			GenericResponse genericResponse = new GenericResponse();
			DeserializeLengthDelimited(stream, genericResponse);
			return genericResponse;
		}

		public static GenericResponse DeserializeLengthDelimited(Stream stream, GenericResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GenericResponse Deserialize(Stream stream, GenericResponse instance, long limit)
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
					instance.ResultCode = (Result)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.RequestId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.RequestSubId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					if (instance.GenericData == null)
					{
						instance.GenericData = GenericData.DeserializeLengthDelimited(stream);
					}
					else
					{
						GenericData.DeserializeLengthDelimited(stream, instance.GenericData);
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

		public static void Serialize(Stream stream, GenericResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ResultCode);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.RequestId);
			if (instance.HasRequestSubId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.RequestSubId);
			}
			if (instance.HasGenericData)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.GenericData.GetSerializedSize());
				GenericData.Serialize(stream, instance.GenericData);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)ResultCode);
			num += ProtocolParser.SizeOfUInt64((ulong)RequestId);
			if (HasRequestSubId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)RequestSubId);
			}
			if (HasGenericData)
			{
				num++;
				uint serializedSize = GenericData.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num + 2;
		}
	}
}
