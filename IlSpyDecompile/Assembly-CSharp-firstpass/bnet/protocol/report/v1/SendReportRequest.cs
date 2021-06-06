using System;
using System.IO;

namespace bnet.protocol.report.v1
{
	public class SendReportRequest : IProtoBuf
	{
		public bool HasProgram;

		private uint _Program;

		public Report Report { get; set; }

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

		public void SetReport(Report val)
		{
			Report = val;
		}

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Report.GetHashCode();
			if (HasProgram)
			{
				hashCode ^= Program.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			SendReportRequest sendReportRequest = obj as SendReportRequest;
			if (sendReportRequest == null)
			{
				return false;
			}
			if (!Report.Equals(sendReportRequest.Report))
			{
				return false;
			}
			if (HasProgram != sendReportRequest.HasProgram || (HasProgram && !Program.Equals(sendReportRequest.Program)))
			{
				return false;
			}
			return true;
		}

		public static SendReportRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendReportRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SendReportRequest Deserialize(Stream stream, SendReportRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SendReportRequest DeserializeLengthDelimited(Stream stream)
		{
			SendReportRequest sendReportRequest = new SendReportRequest();
			DeserializeLengthDelimited(stream, sendReportRequest);
			return sendReportRequest;
		}

		public static SendReportRequest DeserializeLengthDelimited(Stream stream, SendReportRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SendReportRequest Deserialize(Stream stream, SendReportRequest instance, long limit)
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
					if (instance.Report == null)
					{
						instance.Report = Report.DeserializeLengthDelimited(stream);
					}
					else
					{
						Report.DeserializeLengthDelimited(stream, instance.Report);
					}
					continue;
				case 16:
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

		public static void Serialize(Stream stream, SendReportRequest instance)
		{
			if (instance.Report == null)
			{
				throw new ArgumentNullException("Report", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Report.GetSerializedSize());
			Report.Serialize(stream, instance.Report);
			if (instance.HasProgram)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Program);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = Report.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasProgram)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Program);
			}
			return num + 1;
		}
	}
}
