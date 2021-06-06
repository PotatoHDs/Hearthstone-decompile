using System.IO;

namespace bnet.protocol.account.v1
{
	public class GetLicensesRequest : IProtoBuf
	{
		public bool HasTargetId;

		private EntityId _TargetId;

		public bool HasFetchAccountLicenses;

		private bool _FetchAccountLicenses;

		public bool HasFetchGameAccountLicenses;

		private bool _FetchGameAccountLicenses;

		public bool HasFetchDynamicAccountLicenses;

		private bool _FetchDynamicAccountLicenses;

		public bool HasProgram;

		private uint _Program;

		public bool HasExcludeUnknownProgram;

		private bool _ExcludeUnknownProgram;

		public EntityId TargetId
		{
			get
			{
				return _TargetId;
			}
			set
			{
				_TargetId = value;
				HasTargetId = value != null;
			}
		}

		public bool FetchAccountLicenses
		{
			get
			{
				return _FetchAccountLicenses;
			}
			set
			{
				_FetchAccountLicenses = value;
				HasFetchAccountLicenses = true;
			}
		}

		public bool FetchGameAccountLicenses
		{
			get
			{
				return _FetchGameAccountLicenses;
			}
			set
			{
				_FetchGameAccountLicenses = value;
				HasFetchGameAccountLicenses = true;
			}
		}

		public bool FetchDynamicAccountLicenses
		{
			get
			{
				return _FetchDynamicAccountLicenses;
			}
			set
			{
				_FetchDynamicAccountLicenses = value;
				HasFetchDynamicAccountLicenses = true;
			}
		}

		public uint Program
		{
			get
			{
				return _Program;
			}
			set
			{
				_Program = value;
				HasProgram = true;
			}
		}

		public bool ExcludeUnknownProgram
		{
			get
			{
				return _ExcludeUnknownProgram;
			}
			set
			{
				_ExcludeUnknownProgram = value;
				HasExcludeUnknownProgram = true;
			}
		}

		public bool IsInitialized => true;

		public void SetTargetId(EntityId val)
		{
			TargetId = val;
		}

		public void SetFetchAccountLicenses(bool val)
		{
			FetchAccountLicenses = val;
		}

		public void SetFetchGameAccountLicenses(bool val)
		{
			FetchGameAccountLicenses = val;
		}

		public void SetFetchDynamicAccountLicenses(bool val)
		{
			FetchDynamicAccountLicenses = val;
		}

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public void SetExcludeUnknownProgram(bool val)
		{
			ExcludeUnknownProgram = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasTargetId)
			{
				num ^= TargetId.GetHashCode();
			}
			if (HasFetchAccountLicenses)
			{
				num ^= FetchAccountLicenses.GetHashCode();
			}
			if (HasFetchGameAccountLicenses)
			{
				num ^= FetchGameAccountLicenses.GetHashCode();
			}
			if (HasFetchDynamicAccountLicenses)
			{
				num ^= FetchDynamicAccountLicenses.GetHashCode();
			}
			if (HasProgram)
			{
				num ^= Program.GetHashCode();
			}
			if (HasExcludeUnknownProgram)
			{
				num ^= ExcludeUnknownProgram.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetLicensesRequest getLicensesRequest = obj as GetLicensesRequest;
			if (getLicensesRequest == null)
			{
				return false;
			}
			if (HasTargetId != getLicensesRequest.HasTargetId || (HasTargetId && !TargetId.Equals(getLicensesRequest.TargetId)))
			{
				return false;
			}
			if (HasFetchAccountLicenses != getLicensesRequest.HasFetchAccountLicenses || (HasFetchAccountLicenses && !FetchAccountLicenses.Equals(getLicensesRequest.FetchAccountLicenses)))
			{
				return false;
			}
			if (HasFetchGameAccountLicenses != getLicensesRequest.HasFetchGameAccountLicenses || (HasFetchGameAccountLicenses && !FetchGameAccountLicenses.Equals(getLicensesRequest.FetchGameAccountLicenses)))
			{
				return false;
			}
			if (HasFetchDynamicAccountLicenses != getLicensesRequest.HasFetchDynamicAccountLicenses || (HasFetchDynamicAccountLicenses && !FetchDynamicAccountLicenses.Equals(getLicensesRequest.FetchDynamicAccountLicenses)))
			{
				return false;
			}
			if (HasProgram != getLicensesRequest.HasProgram || (HasProgram && !Program.Equals(getLicensesRequest.Program)))
			{
				return false;
			}
			if (HasExcludeUnknownProgram != getLicensesRequest.HasExcludeUnknownProgram || (HasExcludeUnknownProgram && !ExcludeUnknownProgram.Equals(getLicensesRequest.ExcludeUnknownProgram)))
			{
				return false;
			}
			return true;
		}

		public static GetLicensesRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetLicensesRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetLicensesRequest Deserialize(Stream stream, GetLicensesRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetLicensesRequest DeserializeLengthDelimited(Stream stream)
		{
			GetLicensesRequest getLicensesRequest = new GetLicensesRequest();
			DeserializeLengthDelimited(stream, getLicensesRequest);
			return getLicensesRequest;
		}

		public static GetLicensesRequest DeserializeLengthDelimited(Stream stream, GetLicensesRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetLicensesRequest Deserialize(Stream stream, GetLicensesRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.ExcludeUnknownProgram = false;
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
					if (instance.TargetId == null)
					{
						instance.TargetId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.TargetId);
					}
					continue;
				case 16:
					instance.FetchAccountLicenses = ProtocolParser.ReadBool(stream);
					continue;
				case 24:
					instance.FetchGameAccountLicenses = ProtocolParser.ReadBool(stream);
					continue;
				case 32:
					instance.FetchDynamicAccountLicenses = ProtocolParser.ReadBool(stream);
					continue;
				case 45:
					instance.Program = binaryReader.ReadUInt32();
					continue;
				case 48:
					instance.ExcludeUnknownProgram = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, GetLicensesRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasTargetId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
				EntityId.Serialize(stream, instance.TargetId);
			}
			if (instance.HasFetchAccountLicenses)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.FetchAccountLicenses);
			}
			if (instance.HasFetchGameAccountLicenses)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.FetchGameAccountLicenses);
			}
			if (instance.HasFetchDynamicAccountLicenses)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.FetchDynamicAccountLicenses);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasExcludeUnknownProgram)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.ExcludeUnknownProgram);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasTargetId)
			{
				num++;
				uint serializedSize = TargetId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasFetchAccountLicenses)
			{
				num++;
				num++;
			}
			if (HasFetchGameAccountLicenses)
			{
				num++;
				num++;
			}
			if (HasFetchDynamicAccountLicenses)
			{
				num++;
				num++;
			}
			if (HasProgram)
			{
				num++;
				num += 4;
			}
			if (HasExcludeUnknownProgram)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
