using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.report.v1
{
	public class SubmitReportRequest : IProtoBuf
	{
		public bool HasAgentId;

		private GameAccountHandle _AgentId;

		public bool HasReportType;

		private ReportType _ReportType;

		public bool HasProgram;

		private uint _Program;

		public GameAccountHandle AgentId
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

		public ReportType ReportType
		{
			get
			{
				return _ReportType;
			}
			set
			{
				_ReportType = value;
				HasReportType = value != null;
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

		public bool IsInitialized => true;

		public void SetAgentId(GameAccountHandle val)
		{
			AgentId = val;
		}

		public void SetReportType(ReportType val)
		{
			ReportType = val;
		}

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			if (HasReportType)
			{
				num ^= ReportType.GetHashCode();
			}
			if (HasProgram)
			{
				num ^= Program.GetHashCode();
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
			if (HasReportType != submitReportRequest.HasReportType || (HasReportType && !ReportType.Equals(submitReportRequest.ReportType)))
			{
				return false;
			}
			if (HasProgram != submitReportRequest.HasProgram || (HasProgram && !Program.Equals(submitReportRequest.Program)))
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
						instance.AgentId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.AgentId);
					}
					continue;
				case 18:
					if (instance.ReportType == null)
					{
						instance.ReportType = ReportType.DeserializeLengthDelimited(stream);
					}
					else
					{
						ReportType.DeserializeLengthDelimited(stream, instance.ReportType);
					}
					continue;
				case 24:
					instance.Program = ProtocolParser.ReadUInt32(stream);
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
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasReportType)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ReportType.GetSerializedSize());
				ReportType.Serialize(stream, instance.ReportType);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Program);
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
			if (HasReportType)
			{
				num++;
				uint serializedSize2 = ReportType.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasProgram)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Program);
			}
			return num;
		}
	}
}
