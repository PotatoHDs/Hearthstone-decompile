using System;
using System.IO;

namespace bnet.protocol.report.v1
{
	// Token: 0x02000326 RID: 806
	public class SendReportRequest : IProtoBuf
	{
		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x0600313F RID: 12607 RVA: 0x000A59BD File Offset: 0x000A3BBD
		// (set) Token: 0x06003140 RID: 12608 RVA: 0x000A59C5 File Offset: 0x000A3BC5
		public Report Report { get; set; }

		// Token: 0x06003141 RID: 12609 RVA: 0x000A59CE File Offset: 0x000A3BCE
		public void SetReport(Report val)
		{
			this.Report = val;
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06003142 RID: 12610 RVA: 0x000A59D7 File Offset: 0x000A3BD7
		// (set) Token: 0x06003143 RID: 12611 RVA: 0x000A59DF File Offset: 0x000A3BDF
		public uint Program
		{
			get
			{
				return this._Program;
			}
			set
			{
				this._Program = value;
				this.HasProgram = true;
			}
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x000A59EF File Offset: 0x000A3BEF
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x000A59F8 File Offset: 0x000A3BF8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Report.GetHashCode();
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003146 RID: 12614 RVA: 0x000A5A3C File Offset: 0x000A3C3C
		public override bool Equals(object obj)
		{
			SendReportRequest sendReportRequest = obj as SendReportRequest;
			return sendReportRequest != null && this.Report.Equals(sendReportRequest.Report) && this.HasProgram == sendReportRequest.HasProgram && (!this.HasProgram || this.Program.Equals(sendReportRequest.Program));
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06003147 RID: 12615 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003148 RID: 12616 RVA: 0x000A5A99 File Offset: 0x000A3C99
		public static SendReportRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendReportRequest>(bs, 0, -1);
		}

		// Token: 0x06003149 RID: 12617 RVA: 0x000A5AA3 File Offset: 0x000A3CA3
		public void Deserialize(Stream stream)
		{
			SendReportRequest.Deserialize(stream, this);
		}

		// Token: 0x0600314A RID: 12618 RVA: 0x000A5AAD File Offset: 0x000A3CAD
		public static SendReportRequest Deserialize(Stream stream, SendReportRequest instance)
		{
			return SendReportRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600314B RID: 12619 RVA: 0x000A5AB8 File Offset: 0x000A3CB8
		public static SendReportRequest DeserializeLengthDelimited(Stream stream)
		{
			SendReportRequest sendReportRequest = new SendReportRequest();
			SendReportRequest.DeserializeLengthDelimited(stream, sendReportRequest);
			return sendReportRequest;
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x000A5AD4 File Offset: 0x000A3CD4
		public static SendReportRequest DeserializeLengthDelimited(Stream stream, SendReportRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SendReportRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x000A5AFC File Offset: 0x000A3CFC
		public static SendReportRequest Deserialize(Stream stream, SendReportRequest instance, long limit)
		{
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else if (num != 10)
				{
					if (num != 16)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else
					{
						instance.Program = ProtocolParser.ReadUInt32(stream);
					}
				}
				else if (instance.Report == null)
				{
					instance.Report = Report.DeserializeLengthDelimited(stream);
				}
				else
				{
					Report.DeserializeLengthDelimited(stream, instance.Report);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x000A5BAE File Offset: 0x000A3DAE
		public void Serialize(Stream stream)
		{
			SendReportRequest.Serialize(stream, this);
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x000A5BB8 File Offset: 0x000A3DB8
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

		// Token: 0x06003150 RID: 12624 RVA: 0x000A5C20 File Offset: 0x000A3E20
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Report.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasProgram)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Program);
			}
			return num + 1U;
		}

		// Token: 0x04001398 RID: 5016
		public bool HasProgram;

		// Token: 0x04001399 RID: 5017
		private uint _Program;
	}
}
