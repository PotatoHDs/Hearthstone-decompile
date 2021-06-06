using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.report.v1
{
	// Token: 0x02000330 RID: 816
	public class Report : IProtoBuf
	{
		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06003213 RID: 12819 RVA: 0x000A7CD7 File Offset: 0x000A5ED7
		// (set) Token: 0x06003214 RID: 12820 RVA: 0x000A7CDF File Offset: 0x000A5EDF
		public string ReportType { get; set; }

		// Token: 0x06003215 RID: 12821 RVA: 0x000A7CE8 File Offset: 0x000A5EE8
		public void SetReportType(string val)
		{
			this.ReportType = val;
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06003216 RID: 12822 RVA: 0x000A7CF1 File Offset: 0x000A5EF1
		// (set) Token: 0x06003217 RID: 12823 RVA: 0x000A7CF9 File Offset: 0x000A5EF9
		public List<Attribute> Attribute
		{
			get
			{
				return this._Attribute;
			}
			set
			{
				this._Attribute = value;
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06003218 RID: 12824 RVA: 0x000A7CF1 File Offset: 0x000A5EF1
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06003219 RID: 12825 RVA: 0x000A7D02 File Offset: 0x000A5F02
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x0600321A RID: 12826 RVA: 0x000A7D0F File Offset: 0x000A5F0F
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x0600321B RID: 12827 RVA: 0x000A7D1D File Offset: 0x000A5F1D
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x0600321C RID: 12828 RVA: 0x000A7D2A File Offset: 0x000A5F2A
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x0600321D RID: 12829 RVA: 0x000A7D33 File Offset: 0x000A5F33
		// (set) Token: 0x0600321E RID: 12830 RVA: 0x000A7D3B File Offset: 0x000A5F3B
		public int ReportQos
		{
			get
			{
				return this._ReportQos;
			}
			set
			{
				this._ReportQos = value;
				this.HasReportQos = true;
			}
		}

		// Token: 0x0600321F RID: 12831 RVA: 0x000A7D4B File Offset: 0x000A5F4B
		public void SetReportQos(int val)
		{
			this.ReportQos = val;
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06003220 RID: 12832 RVA: 0x000A7D54 File Offset: 0x000A5F54
		// (set) Token: 0x06003221 RID: 12833 RVA: 0x000A7D5C File Offset: 0x000A5F5C
		public EntityId ReportingAccount
		{
			get
			{
				return this._ReportingAccount;
			}
			set
			{
				this._ReportingAccount = value;
				this.HasReportingAccount = (value != null);
			}
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x000A7D6F File Offset: 0x000A5F6F
		public void SetReportingAccount(EntityId val)
		{
			this.ReportingAccount = val;
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06003223 RID: 12835 RVA: 0x000A7D78 File Offset: 0x000A5F78
		// (set) Token: 0x06003224 RID: 12836 RVA: 0x000A7D80 File Offset: 0x000A5F80
		public EntityId ReportingGameAccount
		{
			get
			{
				return this._ReportingGameAccount;
			}
			set
			{
				this._ReportingGameAccount = value;
				this.HasReportingGameAccount = (value != null);
			}
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x000A7D93 File Offset: 0x000A5F93
		public void SetReportingGameAccount(EntityId val)
		{
			this.ReportingGameAccount = val;
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06003226 RID: 12838 RVA: 0x000A7D9C File Offset: 0x000A5F9C
		// (set) Token: 0x06003227 RID: 12839 RVA: 0x000A7DA4 File Offset: 0x000A5FA4
		public ulong ReportTimestamp
		{
			get
			{
				return this._ReportTimestamp;
			}
			set
			{
				this._ReportTimestamp = value;
				this.HasReportTimestamp = true;
			}
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x000A7DB4 File Offset: 0x000A5FB4
		public void SetReportTimestamp(ulong val)
		{
			this.ReportTimestamp = val;
		}

		// Token: 0x06003229 RID: 12841 RVA: 0x000A7DC0 File Offset: 0x000A5FC0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ReportType.GetHashCode();
			foreach (Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			if (this.HasReportQos)
			{
				num ^= this.ReportQos.GetHashCode();
			}
			if (this.HasReportingAccount)
			{
				num ^= this.ReportingAccount.GetHashCode();
			}
			if (this.HasReportingGameAccount)
			{
				num ^= this.ReportingGameAccount.GetHashCode();
			}
			if (this.HasReportTimestamp)
			{
				num ^= this.ReportTimestamp.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600322A RID: 12842 RVA: 0x000A7E90 File Offset: 0x000A6090
		public override bool Equals(object obj)
		{
			Report report = obj as Report;
			if (report == null)
			{
				return false;
			}
			if (!this.ReportType.Equals(report.ReportType))
			{
				return false;
			}
			if (this.Attribute.Count != report.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(report.Attribute[i]))
				{
					return false;
				}
			}
			return this.HasReportQos == report.HasReportQos && (!this.HasReportQos || this.ReportQos.Equals(report.ReportQos)) && this.HasReportingAccount == report.HasReportingAccount && (!this.HasReportingAccount || this.ReportingAccount.Equals(report.ReportingAccount)) && this.HasReportingGameAccount == report.HasReportingGameAccount && (!this.HasReportingGameAccount || this.ReportingGameAccount.Equals(report.ReportingGameAccount)) && this.HasReportTimestamp == report.HasReportTimestamp && (!this.HasReportTimestamp || this.ReportTimestamp.Equals(report.ReportTimestamp));
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x0600322B RID: 12843 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x000A7FC2 File Offset: 0x000A61C2
		public static Report ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Report>(bs, 0, -1);
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x000A7FCC File Offset: 0x000A61CC
		public void Deserialize(Stream stream)
		{
			Report.Deserialize(stream, this);
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x000A7FD6 File Offset: 0x000A61D6
		public static Report Deserialize(Stream stream, Report instance)
		{
			return Report.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600322F RID: 12847 RVA: 0x000A7FE4 File Offset: 0x000A61E4
		public static Report DeserializeLengthDelimited(Stream stream)
		{
			Report report = new Report();
			Report.DeserializeLengthDelimited(stream, report);
			return report;
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x000A8000 File Offset: 0x000A6200
		public static Report DeserializeLengthDelimited(Stream stream, Report instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Report.Deserialize(stream, instance, num);
		}

		// Token: 0x06003231 RID: 12849 RVA: 0x000A8028 File Offset: 0x000A6228
		public static Report Deserialize(Stream stream, Report instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
			}
			instance.ReportQos = 0;
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
					if (num <= 24)
					{
						if (num == 10)
						{
							instance.ReportType = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 18)
						{
							instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 24)
						{
							instance.ReportQos = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else if (num != 34)
					{
						if (num != 42)
						{
							if (num == 49)
							{
								instance.ReportTimestamp = binaryReader.ReadUInt64();
								continue;
							}
						}
						else
						{
							if (instance.ReportingGameAccount == null)
							{
								instance.ReportingGameAccount = EntityId.DeserializeLengthDelimited(stream);
								continue;
							}
							EntityId.DeserializeLengthDelimited(stream, instance.ReportingGameAccount);
							continue;
						}
					}
					else
					{
						if (instance.ReportingAccount == null)
						{
							instance.ReportingAccount = EntityId.DeserializeLengthDelimited(stream);
							continue;
						}
						EntityId.DeserializeLengthDelimited(stream, instance.ReportingAccount);
						continue;
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003232 RID: 12850 RVA: 0x000A818C File Offset: 0x000A638C
		public void Serialize(Stream stream)
		{
			Report.Serialize(stream, this);
		}

		// Token: 0x06003233 RID: 12851 RVA: 0x000A8198 File Offset: 0x000A6398
		public static void Serialize(Stream stream, Report instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.ReportType == null)
			{
				throw new ArgumentNullException("ReportType", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ReportType));
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.HasReportQos)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ReportQos));
			}
			if (instance.HasReportingAccount)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.ReportingAccount.GetSerializedSize());
				EntityId.Serialize(stream, instance.ReportingAccount);
			}
			if (instance.HasReportingGameAccount)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.ReportingGameAccount.GetSerializedSize());
				EntityId.Serialize(stream, instance.ReportingGameAccount);
			}
			if (instance.HasReportTimestamp)
			{
				stream.WriteByte(49);
				binaryWriter.Write(instance.ReportTimestamp);
			}
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x000A82E0 File Offset: 0x000A64E0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ReportType);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (this.Attribute.Count > 0)
			{
				foreach (Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasReportQos)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ReportQos));
			}
			if (this.HasReportingAccount)
			{
				num += 1U;
				uint serializedSize2 = this.ReportingAccount.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasReportingGameAccount)
			{
				num += 1U;
				uint serializedSize3 = this.ReportingGameAccount.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasReportTimestamp)
			{
				num += 1U;
				num += 8U;
			}
			num += 1U;
			return num;
		}

		// Token: 0x040013CA RID: 5066
		private List<Attribute> _Attribute = new List<Attribute>();

		// Token: 0x040013CB RID: 5067
		public bool HasReportQos;

		// Token: 0x040013CC RID: 5068
		private int _ReportQos;

		// Token: 0x040013CD RID: 5069
		public bool HasReportingAccount;

		// Token: 0x040013CE RID: 5070
		private EntityId _ReportingAccount;

		// Token: 0x040013CF RID: 5071
		public bool HasReportingGameAccount;

		// Token: 0x040013D0 RID: 5072
		private EntityId _ReportingGameAccount;

		// Token: 0x040013D1 RID: 5073
		public bool HasReportTimestamp;

		// Token: 0x040013D2 RID: 5074
		private ulong _ReportTimestamp;
	}
}
