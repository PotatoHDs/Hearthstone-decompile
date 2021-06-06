using System;
using System.IO;
using System.Text;

namespace bnet.protocol.report.v1
{
	// Token: 0x02000328 RID: 808
	public class ReportType : IProtoBuf
	{
		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06003168 RID: 12648 RVA: 0x000A601C File Offset: 0x000A421C
		// (set) Token: 0x06003169 RID: 12649 RVA: 0x000A6024 File Offset: 0x000A4224
		public string UserDescription
		{
			get
			{
				return this._UserDescription;
			}
			set
			{
				this._UserDescription = value;
				this.HasUserDescription = (value != null);
			}
		}

		// Token: 0x0600316A RID: 12650 RVA: 0x000A6037 File Offset: 0x000A4237
		public void SetUserDescription(string val)
		{
			this.UserDescription = val;
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x0600316B RID: 12651 RVA: 0x000A6040 File Offset: 0x000A4240
		// (set) Token: 0x0600316C RID: 12652 RVA: 0x000A6048 File Offset: 0x000A4248
		public CustomReport CustomReport
		{
			get
			{
				return this._CustomReport;
			}
			set
			{
				this._CustomReport = value;
				this.HasCustomReport = (value != null);
			}
		}

		// Token: 0x0600316D RID: 12653 RVA: 0x000A605B File Offset: 0x000A425B
		public void SetCustomReport(CustomReport val)
		{
			this.CustomReport = val;
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x0600316E RID: 12654 RVA: 0x000A6064 File Offset: 0x000A4264
		// (set) Token: 0x0600316F RID: 12655 RVA: 0x000A606C File Offset: 0x000A426C
		public SpamReport SpamReport
		{
			get
			{
				return this._SpamReport;
			}
			set
			{
				this._SpamReport = value;
				this.HasSpamReport = (value != null);
			}
		}

		// Token: 0x06003170 RID: 12656 RVA: 0x000A607F File Offset: 0x000A427F
		public void SetSpamReport(SpamReport val)
		{
			this.SpamReport = val;
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06003171 RID: 12657 RVA: 0x000A6088 File Offset: 0x000A4288
		// (set) Token: 0x06003172 RID: 12658 RVA: 0x000A6090 File Offset: 0x000A4290
		public HarassmentReport HarassmentReport
		{
			get
			{
				return this._HarassmentReport;
			}
			set
			{
				this._HarassmentReport = value;
				this.HasHarassmentReport = (value != null);
			}
		}

		// Token: 0x06003173 RID: 12659 RVA: 0x000A60A3 File Offset: 0x000A42A3
		public void SetHarassmentReport(HarassmentReport val)
		{
			this.HarassmentReport = val;
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06003174 RID: 12660 RVA: 0x000A60AC File Offset: 0x000A42AC
		// (set) Token: 0x06003175 RID: 12661 RVA: 0x000A60B4 File Offset: 0x000A42B4
		public RealLifeThreatReport RealLifeThreatReport
		{
			get
			{
				return this._RealLifeThreatReport;
			}
			set
			{
				this._RealLifeThreatReport = value;
				this.HasRealLifeThreatReport = (value != null);
			}
		}

		// Token: 0x06003176 RID: 12662 RVA: 0x000A60C7 File Offset: 0x000A42C7
		public void SetRealLifeThreatReport(RealLifeThreatReport val)
		{
			this.RealLifeThreatReport = val;
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06003177 RID: 12663 RVA: 0x000A60D0 File Offset: 0x000A42D0
		// (set) Token: 0x06003178 RID: 12664 RVA: 0x000A60D8 File Offset: 0x000A42D8
		public InappropriateBattleTagReport InappropriateBattleTagReport
		{
			get
			{
				return this._InappropriateBattleTagReport;
			}
			set
			{
				this._InappropriateBattleTagReport = value;
				this.HasInappropriateBattleTagReport = (value != null);
			}
		}

		// Token: 0x06003179 RID: 12665 RVA: 0x000A60EB File Offset: 0x000A42EB
		public void SetInappropriateBattleTagReport(InappropriateBattleTagReport val)
		{
			this.InappropriateBattleTagReport = val;
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x0600317A RID: 12666 RVA: 0x000A60F4 File Offset: 0x000A42F4
		// (set) Token: 0x0600317B RID: 12667 RVA: 0x000A60FC File Offset: 0x000A42FC
		public HackingReport HackingReport
		{
			get
			{
				return this._HackingReport;
			}
			set
			{
				this._HackingReport = value;
				this.HasHackingReport = (value != null);
			}
		}

		// Token: 0x0600317C RID: 12668 RVA: 0x000A610F File Offset: 0x000A430F
		public void SetHackingReport(HackingReport val)
		{
			this.HackingReport = val;
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x0600317D RID: 12669 RVA: 0x000A6118 File Offset: 0x000A4318
		// (set) Token: 0x0600317E RID: 12670 RVA: 0x000A6120 File Offset: 0x000A4320
		public BottingReport BottingReport
		{
			get
			{
				return this._BottingReport;
			}
			set
			{
				this._BottingReport = value;
				this.HasBottingReport = (value != null);
			}
		}

		// Token: 0x0600317F RID: 12671 RVA: 0x000A6133 File Offset: 0x000A4333
		public void SetBottingReport(BottingReport val)
		{
			this.BottingReport = val;
		}

		// Token: 0x06003180 RID: 12672 RVA: 0x000A613C File Offset: 0x000A433C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasUserDescription)
			{
				num ^= this.UserDescription.GetHashCode();
			}
			if (this.HasCustomReport)
			{
				num ^= this.CustomReport.GetHashCode();
			}
			if (this.HasSpamReport)
			{
				num ^= this.SpamReport.GetHashCode();
			}
			if (this.HasHarassmentReport)
			{
				num ^= this.HarassmentReport.GetHashCode();
			}
			if (this.HasRealLifeThreatReport)
			{
				num ^= this.RealLifeThreatReport.GetHashCode();
			}
			if (this.HasInappropriateBattleTagReport)
			{
				num ^= this.InappropriateBattleTagReport.GetHashCode();
			}
			if (this.HasHackingReport)
			{
				num ^= this.HackingReport.GetHashCode();
			}
			if (this.HasBottingReport)
			{
				num ^= this.BottingReport.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003181 RID: 12673 RVA: 0x000A6208 File Offset: 0x000A4408
		public override bool Equals(object obj)
		{
			ReportType reportType = obj as ReportType;
			return reportType != null && this.HasUserDescription == reportType.HasUserDescription && (!this.HasUserDescription || this.UserDescription.Equals(reportType.UserDescription)) && this.HasCustomReport == reportType.HasCustomReport && (!this.HasCustomReport || this.CustomReport.Equals(reportType.CustomReport)) && this.HasSpamReport == reportType.HasSpamReport && (!this.HasSpamReport || this.SpamReport.Equals(reportType.SpamReport)) && this.HasHarassmentReport == reportType.HasHarassmentReport && (!this.HasHarassmentReport || this.HarassmentReport.Equals(reportType.HarassmentReport)) && this.HasRealLifeThreatReport == reportType.HasRealLifeThreatReport && (!this.HasRealLifeThreatReport || this.RealLifeThreatReport.Equals(reportType.RealLifeThreatReport)) && this.HasInappropriateBattleTagReport == reportType.HasInappropriateBattleTagReport && (!this.HasInappropriateBattleTagReport || this.InappropriateBattleTagReport.Equals(reportType.InappropriateBattleTagReport)) && this.HasHackingReport == reportType.HasHackingReport && (!this.HasHackingReport || this.HackingReport.Equals(reportType.HackingReport)) && this.HasBottingReport == reportType.HasBottingReport && (!this.HasBottingReport || this.BottingReport.Equals(reportType.BottingReport));
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06003182 RID: 12674 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x000A637A File Offset: 0x000A457A
		public static ReportType ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ReportType>(bs, 0, -1);
		}

		// Token: 0x06003184 RID: 12676 RVA: 0x000A6384 File Offset: 0x000A4584
		public void Deserialize(Stream stream)
		{
			ReportType.Deserialize(stream, this);
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x000A638E File Offset: 0x000A458E
		public static ReportType Deserialize(Stream stream, ReportType instance)
		{
			return ReportType.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x000A639C File Offset: 0x000A459C
		public static ReportType DeserializeLengthDelimited(Stream stream)
		{
			ReportType reportType = new ReportType();
			ReportType.DeserializeLengthDelimited(stream, reportType);
			return reportType;
		}

		// Token: 0x06003187 RID: 12679 RVA: 0x000A63B8 File Offset: 0x000A45B8
		public static ReportType DeserializeLengthDelimited(Stream stream, ReportType instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ReportType.Deserialize(stream, instance, num);
		}

		// Token: 0x06003188 RID: 12680 RVA: 0x000A63E0 File Offset: 0x000A45E0
		public static ReportType Deserialize(Stream stream, ReportType instance, long limit)
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
				else
				{
					if (num <= 90)
					{
						if (num == 10)
						{
							instance.UserDescription = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num != 82)
						{
							if (num == 90)
							{
								if (instance.SpamReport == null)
								{
									instance.SpamReport = SpamReport.DeserializeLengthDelimited(stream);
									continue;
								}
								SpamReport.DeserializeLengthDelimited(stream, instance.SpamReport);
								continue;
							}
						}
						else
						{
							if (instance.CustomReport == null)
							{
								instance.CustomReport = CustomReport.DeserializeLengthDelimited(stream);
								continue;
							}
							CustomReport.DeserializeLengthDelimited(stream, instance.CustomReport);
							continue;
						}
					}
					else if (num <= 106)
					{
						if (num != 98)
						{
							if (num == 106)
							{
								if (instance.RealLifeThreatReport == null)
								{
									instance.RealLifeThreatReport = RealLifeThreatReport.DeserializeLengthDelimited(stream);
									continue;
								}
								RealLifeThreatReport.DeserializeLengthDelimited(stream, instance.RealLifeThreatReport);
								continue;
							}
						}
						else
						{
							if (instance.HarassmentReport == null)
							{
								instance.HarassmentReport = HarassmentReport.DeserializeLengthDelimited(stream);
								continue;
							}
							HarassmentReport.DeserializeLengthDelimited(stream, instance.HarassmentReport);
							continue;
						}
					}
					else if (num != 114)
					{
						if (num == 122)
						{
							if (instance.HackingReport == null)
							{
								instance.HackingReport = HackingReport.DeserializeLengthDelimited(stream);
								continue;
							}
							HackingReport.DeserializeLengthDelimited(stream, instance.HackingReport);
							continue;
						}
					}
					else
					{
						if (instance.InappropriateBattleTagReport == null)
						{
							instance.InappropriateBattleTagReport = InappropriateBattleTagReport.DeserializeLengthDelimited(stream);
							continue;
						}
						InappropriateBattleTagReport.DeserializeLengthDelimited(stream, instance.InappropriateBattleTagReport);
						continue;
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					if (field != 16U)
					{
						ProtocolParser.SkipKey(stream, key);
					}
					else if (key.WireType == Wire.LengthDelimited)
					{
						if (instance.BottingReport == null)
						{
							instance.BottingReport = BottingReport.DeserializeLengthDelimited(stream);
						}
						else
						{
							BottingReport.DeserializeLengthDelimited(stream, instance.BottingReport);
						}
					}
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003189 RID: 12681 RVA: 0x000A65EF File Offset: 0x000A47EF
		public void Serialize(Stream stream)
		{
			ReportType.Serialize(stream, this);
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x000A65F8 File Offset: 0x000A47F8
		public static void Serialize(Stream stream, ReportType instance)
		{
			if (instance.HasUserDescription)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.UserDescription));
			}
			if (instance.HasCustomReport)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteUInt32(stream, instance.CustomReport.GetSerializedSize());
				CustomReport.Serialize(stream, instance.CustomReport);
			}
			if (instance.HasSpamReport)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteUInt32(stream, instance.SpamReport.GetSerializedSize());
				SpamReport.Serialize(stream, instance.SpamReport);
			}
			if (instance.HasHarassmentReport)
			{
				stream.WriteByte(98);
				ProtocolParser.WriteUInt32(stream, instance.HarassmentReport.GetSerializedSize());
				HarassmentReport.Serialize(stream, instance.HarassmentReport);
			}
			if (instance.HasRealLifeThreatReport)
			{
				stream.WriteByte(106);
				ProtocolParser.WriteUInt32(stream, instance.RealLifeThreatReport.GetSerializedSize());
				RealLifeThreatReport.Serialize(stream, instance.RealLifeThreatReport);
			}
			if (instance.HasInappropriateBattleTagReport)
			{
				stream.WriteByte(114);
				ProtocolParser.WriteUInt32(stream, instance.InappropriateBattleTagReport.GetSerializedSize());
				InappropriateBattleTagReport.Serialize(stream, instance.InappropriateBattleTagReport);
			}
			if (instance.HasHackingReport)
			{
				stream.WriteByte(122);
				ProtocolParser.WriteUInt32(stream, instance.HackingReport.GetSerializedSize());
				HackingReport.Serialize(stream, instance.HackingReport);
			}
			if (instance.HasBottingReport)
			{
				stream.WriteByte(130);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.BottingReport.GetSerializedSize());
				BottingReport.Serialize(stream, instance.BottingReport);
			}
		}

		// Token: 0x0600318B RID: 12683 RVA: 0x000A6770 File Offset: 0x000A4970
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasUserDescription)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.UserDescription);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasCustomReport)
			{
				num += 1U;
				uint serializedSize = this.CustomReport.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasSpamReport)
			{
				num += 1U;
				uint serializedSize2 = this.SpamReport.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasHarassmentReport)
			{
				num += 1U;
				uint serializedSize3 = this.HarassmentReport.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasRealLifeThreatReport)
			{
				num += 1U;
				uint serializedSize4 = this.RealLifeThreatReport.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (this.HasInappropriateBattleTagReport)
			{
				num += 1U;
				uint serializedSize5 = this.InappropriateBattleTagReport.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			if (this.HasHackingReport)
			{
				num += 1U;
				uint serializedSize6 = this.HackingReport.GetSerializedSize();
				num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
			}
			if (this.HasBottingReport)
			{
				num += 2U;
				uint serializedSize7 = this.BottingReport.GetSerializedSize();
				num += serializedSize7 + ProtocolParser.SizeOfUInt32(serializedSize7);
			}
			return num;
		}

		// Token: 0x040013A0 RID: 5024
		public bool HasUserDescription;

		// Token: 0x040013A1 RID: 5025
		private string _UserDescription;

		// Token: 0x040013A2 RID: 5026
		public bool HasCustomReport;

		// Token: 0x040013A3 RID: 5027
		private CustomReport _CustomReport;

		// Token: 0x040013A4 RID: 5028
		public bool HasSpamReport;

		// Token: 0x040013A5 RID: 5029
		private SpamReport _SpamReport;

		// Token: 0x040013A6 RID: 5030
		public bool HasHarassmentReport;

		// Token: 0x040013A7 RID: 5031
		private HarassmentReport _HarassmentReport;

		// Token: 0x040013A8 RID: 5032
		public bool HasRealLifeThreatReport;

		// Token: 0x040013A9 RID: 5033
		private RealLifeThreatReport _RealLifeThreatReport;

		// Token: 0x040013AA RID: 5034
		public bool HasInappropriateBattleTagReport;

		// Token: 0x040013AB RID: 5035
		private InappropriateBattleTagReport _InappropriateBattleTagReport;

		// Token: 0x040013AC RID: 5036
		public bool HasHackingReport;

		// Token: 0x040013AD RID: 5037
		private HackingReport _HackingReport;

		// Token: 0x040013AE RID: 5038
		public bool HasBottingReport;

		// Token: 0x040013AF RID: 5039
		private BottingReport _BottingReport;
	}
}
