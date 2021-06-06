using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.report.v2
{
	public class SubmitReportRequest : IProtoBuf
	{
		public bool HasAgentId;

		private AccountId _AgentId;

		public bool HasUserDescription;

		private string _UserDescription;

		public bool HasProgram;

		private uint _Program;

		public bool HasUserOptions;

		private UserOptions _UserOptions;

		public bool HasClubOptions;

		private ClubOptions _ClubOptions;

		public AccountId AgentId
		{
			get
			{
				return _AgentId;
			}
			set
			{
				_AgentId = value;
				HasAgentId = value != null;
			}
		}

		public string UserDescription
		{
			get
			{
				return _UserDescription;
			}
			set
			{
				_UserDescription = value;
				HasUserDescription = value != null;
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

		public UserOptions UserOptions
		{
			get
			{
				return _UserOptions;
			}
			set
			{
				_UserOptions = value;
				HasUserOptions = value != null;
			}
		}

		public ClubOptions ClubOptions
		{
			get
			{
				return _ClubOptions;
			}
			set
			{
				_ClubOptions = value;
				HasClubOptions = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetAgentId(AccountId val)
		{
			AgentId = val;
		}

		public void SetUserDescription(string val)
		{
			UserDescription = val;
		}

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public void SetUserOptions(UserOptions val)
		{
			UserOptions = val;
		}

		public void SetClubOptions(ClubOptions val)
		{
			ClubOptions = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			if (HasUserDescription)
			{
				num ^= UserDescription.GetHashCode();
			}
			if (HasProgram)
			{
				num ^= Program.GetHashCode();
			}
			if (HasUserOptions)
			{
				num ^= UserOptions.GetHashCode();
			}
			if (HasClubOptions)
			{
				num ^= ClubOptions.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SubmitReportRequest submitReportRequest = obj as SubmitReportRequest;
			if (submitReportRequest == null)
			{
				return false;
			}
			if (HasAgentId != submitReportRequest.HasAgentId || (HasAgentId && !AgentId.Equals(submitReportRequest.AgentId)))
			{
				return false;
			}
			if (HasUserDescription != submitReportRequest.HasUserDescription || (HasUserDescription && !UserDescription.Equals(submitReportRequest.UserDescription)))
			{
				return false;
			}
			if (HasProgram != submitReportRequest.HasProgram || (HasProgram && !Program.Equals(submitReportRequest.Program)))
			{
				return false;
			}
			if (HasUserOptions != submitReportRequest.HasUserOptions || (HasUserOptions && !UserOptions.Equals(submitReportRequest.UserOptions)))
			{
				return false;
			}
			if (HasClubOptions != submitReportRequest.HasClubOptions || (HasClubOptions && !ClubOptions.Equals(submitReportRequest.ClubOptions)))
			{
				return false;
			}
			return true;
		}

		public static SubmitReportRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubmitReportRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SubmitReportRequest Deserialize(Stream stream, SubmitReportRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SubmitReportRequest DeserializeLengthDelimited(Stream stream)
		{
			SubmitReportRequest submitReportRequest = new SubmitReportRequest();
			DeserializeLengthDelimited(stream, submitReportRequest);
			return submitReportRequest;
		}

		public static SubmitReportRequest DeserializeLengthDelimited(Stream stream, SubmitReportRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SubmitReportRequest Deserialize(Stream stream, SubmitReportRequest instance, long limit)
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
				case 10:
					if (instance.AgentId == null)
					{
						instance.AgentId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.AgentId);
					}
					continue;
				case 18:
					instance.UserDescription = ProtocolParser.ReadString(stream);
					continue;
				case 24:
					instance.Program = ProtocolParser.ReadUInt32(stream);
					continue;
				case 82:
					if (instance.UserOptions == null)
					{
						instance.UserOptions = UserOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						UserOptions.DeserializeLengthDelimited(stream, instance.UserOptions);
					}
					continue;
				case 90:
					if (instance.ClubOptions == null)
					{
						instance.ClubOptions = ClubOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						ClubOptions.DeserializeLengthDelimited(stream, instance.ClubOptions);
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

		public static void Serialize(Stream stream, SubmitReportRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				AccountId.Serialize(stream, instance.AgentId);
			}
			if (instance.HasUserDescription)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.UserDescription));
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Program);
			}
			if (instance.HasUserOptions)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteUInt32(stream, instance.UserOptions.GetSerializedSize());
				UserOptions.Serialize(stream, instance.UserOptions);
			}
			if (instance.HasClubOptions)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteUInt32(stream, instance.ClubOptions.GetSerializedSize());
				ClubOptions.Serialize(stream, instance.ClubOptions);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAgentId)
			{
				num++;
				uint serializedSize = AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasUserDescription)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(UserDescription);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasProgram)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Program);
			}
			if (HasUserOptions)
			{
				num++;
				uint serializedSize2 = UserOptions.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasClubOptions)
			{
				num++;
				uint serializedSize3 = ClubOptions.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}
	}
}
