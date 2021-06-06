using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.report.v1
{
	// Token: 0x02000327 RID: 807
	public class SubmitReportRequest : IProtoBuf
	{
		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06003152 RID: 12626 RVA: 0x000A5C65 File Offset: 0x000A3E65
		// (set) Token: 0x06003153 RID: 12627 RVA: 0x000A5C6D File Offset: 0x000A3E6D
		public GameAccountHandle AgentId
		{
			get
			{
				return this._AgentId;
			}
			set
			{
				this._AgentId = value;
				this.HasAgentId = (value != null);
			}
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x000A5C80 File Offset: 0x000A3E80
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06003155 RID: 12629 RVA: 0x000A5C89 File Offset: 0x000A3E89
		// (set) Token: 0x06003156 RID: 12630 RVA: 0x000A5C91 File Offset: 0x000A3E91
		public ReportType ReportType
		{
			get
			{
				return this._ReportType;
			}
			set
			{
				this._ReportType = value;
				this.HasReportType = (value != null);
			}
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x000A5CA4 File Offset: 0x000A3EA4
		public void SetReportType(ReportType val)
		{
			this.ReportType = val;
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06003158 RID: 12632 RVA: 0x000A5CAD File Offset: 0x000A3EAD
		// (set) Token: 0x06003159 RID: 12633 RVA: 0x000A5CB5 File Offset: 0x000A3EB5
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

		// Token: 0x0600315A RID: 12634 RVA: 0x000A5CC5 File Offset: 0x000A3EC5
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x0600315B RID: 12635 RVA: 0x000A5CD0 File Offset: 0x000A3ED0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasReportType)
			{
				num ^= this.ReportType.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600315C RID: 12636 RVA: 0x000A5D30 File Offset: 0x000A3F30
		public override bool Equals(object obj)
		{
			SubmitReportRequest submitReportRequest = obj as SubmitReportRequest;
			return submitReportRequest != null && this.HasAgentId == submitReportRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(submitReportRequest.AgentId)) && this.HasReportType == submitReportRequest.HasReportType && (!this.HasReportType || this.ReportType.Equals(submitReportRequest.ReportType)) && this.HasProgram == submitReportRequest.HasProgram && (!this.HasProgram || this.Program.Equals(submitReportRequest.Program));
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x0600315D RID: 12637 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600315E RID: 12638 RVA: 0x000A5DCE File Offset: 0x000A3FCE
		public static SubmitReportRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubmitReportRequest>(bs, 0, -1);
		}

		// Token: 0x0600315F RID: 12639 RVA: 0x000A5DD8 File Offset: 0x000A3FD8
		public void Deserialize(Stream stream)
		{
			SubmitReportRequest.Deserialize(stream, this);
		}

		// Token: 0x06003160 RID: 12640 RVA: 0x000A5DE2 File Offset: 0x000A3FE2
		public static SubmitReportRequest Deserialize(Stream stream, SubmitReportRequest instance)
		{
			return SubmitReportRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003161 RID: 12641 RVA: 0x000A5DF0 File Offset: 0x000A3FF0
		public static SubmitReportRequest DeserializeLengthDelimited(Stream stream)
		{
			SubmitReportRequest submitReportRequest = new SubmitReportRequest();
			SubmitReportRequest.DeserializeLengthDelimited(stream, submitReportRequest);
			return submitReportRequest;
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x000A5E0C File Offset: 0x000A400C
		public static SubmitReportRequest DeserializeLengthDelimited(Stream stream, SubmitReportRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubmitReportRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x000A5E34 File Offset: 0x000A4034
		public static SubmitReportRequest Deserialize(Stream stream, SubmitReportRequest instance, long limit)
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
					if (num != 18)
					{
						if (num != 24)
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
					else if (instance.ReportType == null)
					{
						instance.ReportType = ReportType.DeserializeLengthDelimited(stream);
					}
					else
					{
						ReportType.DeserializeLengthDelimited(stream, instance.ReportType);
					}
				}
				else if (instance.AgentId == null)
				{
					instance.AgentId = GameAccountHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountHandle.DeserializeLengthDelimited(stream, instance.AgentId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x000A5F1C File Offset: 0x000A411C
		public void Serialize(Stream stream)
		{
			SubmitReportRequest.Serialize(stream, this);
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x000A5F28 File Offset: 0x000A4128
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

		// Token: 0x06003166 RID: 12646 RVA: 0x000A5FAC File Offset: 0x000A41AC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasReportType)
			{
				num += 1U;
				uint serializedSize2 = this.ReportType.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasProgram)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Program);
			}
			return num;
		}

		// Token: 0x0400139A RID: 5018
		public bool HasAgentId;

		// Token: 0x0400139B RID: 5019
		private GameAccountHandle _AgentId;

		// Token: 0x0400139C RID: 5020
		public bool HasReportType;

		// Token: 0x0400139D RID: 5021
		private ReportType _ReportType;

		// Token: 0x0400139E RID: 5022
		public bool HasProgram;

		// Token: 0x0400139F RID: 5023
		private uint _Program;
	}
}
