using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.broadcast.v1
{
	// Token: 0x020004E7 RID: 1255
	public class BroadcastFilter : IProtoBuf
	{
		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x060058A8 RID: 22696 RVA: 0x0010F5A6 File Offset: 0x0010D7A6
		// (set) Token: 0x060058A9 RID: 22697 RVA: 0x0010F5AE File Offset: 0x0010D7AE
		public List<AccountLicense> TargetLicense
		{
			get
			{
				return this._TargetLicense;
			}
			set
			{
				this._TargetLicense = value;
			}
		}

		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x060058AA RID: 22698 RVA: 0x0010F5A6 File Offset: 0x0010D7A6
		public List<AccountLicense> TargetLicenseList
		{
			get
			{
				return this._TargetLicense;
			}
		}

		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x060058AB RID: 22699 RVA: 0x0010F5B7 File Offset: 0x0010D7B7
		public int TargetLicenseCount
		{
			get
			{
				return this._TargetLicense.Count;
			}
		}

		// Token: 0x060058AC RID: 22700 RVA: 0x0010F5C4 File Offset: 0x0010D7C4
		public void AddTargetLicense(AccountLicense val)
		{
			this._TargetLicense.Add(val);
		}

		// Token: 0x060058AD RID: 22701 RVA: 0x0010F5D2 File Offset: 0x0010D7D2
		public void ClearTargetLicense()
		{
			this._TargetLicense.Clear();
		}

		// Token: 0x060058AE RID: 22702 RVA: 0x0010F5DF File Offset: 0x0010D7DF
		public void SetTargetLicense(List<AccountLicense> val)
		{
			this.TargetLicense = val;
		}

		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x060058AF RID: 22703 RVA: 0x0010F5E8 File Offset: 0x0010D7E8
		// (set) Token: 0x060058B0 RID: 22704 RVA: 0x0010F5F0 File Offset: 0x0010D7F0
		public bool EmployeeOnly
		{
			get
			{
				return this._EmployeeOnly;
			}
			set
			{
				this._EmployeeOnly = value;
				this.HasEmployeeOnly = true;
			}
		}

		// Token: 0x060058B1 RID: 22705 RVA: 0x0010F600 File Offset: 0x0010D800
		public void SetEmployeeOnly(bool val)
		{
			this.EmployeeOnly = val;
		}

		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x060058B2 RID: 22706 RVA: 0x0010F609 File Offset: 0x0010D809
		// (set) Token: 0x060058B3 RID: 22707 RVA: 0x0010F611 File Offset: 0x0010D811
		public List<string> TargetProgram
		{
			get
			{
				return this._TargetProgram;
			}
			set
			{
				this._TargetProgram = value;
			}
		}

		// Token: 0x170010B6 RID: 4278
		// (get) Token: 0x060058B4 RID: 22708 RVA: 0x0010F609 File Offset: 0x0010D809
		public List<string> TargetProgramList
		{
			get
			{
				return this._TargetProgram;
			}
		}

		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x060058B5 RID: 22709 RVA: 0x0010F61A File Offset: 0x0010D81A
		public int TargetProgramCount
		{
			get
			{
				return this._TargetProgram.Count;
			}
		}

		// Token: 0x060058B6 RID: 22710 RVA: 0x0010F627 File Offset: 0x0010D827
		public void AddTargetProgram(string val)
		{
			this._TargetProgram.Add(val);
		}

		// Token: 0x060058B7 RID: 22711 RVA: 0x0010F635 File Offset: 0x0010D835
		public void ClearTargetProgram()
		{
			this._TargetProgram.Clear();
		}

		// Token: 0x060058B8 RID: 22712 RVA: 0x0010F642 File Offset: 0x0010D842
		public void SetTargetProgram(List<string> val)
		{
			this.TargetProgram = val;
		}

		// Token: 0x170010B8 RID: 4280
		// (get) Token: 0x060058B9 RID: 22713 RVA: 0x0010F64B File Offset: 0x0010D84B
		// (set) Token: 0x060058BA RID: 22714 RVA: 0x0010F653 File Offset: 0x0010D853
		public List<string> TargetLocale
		{
			get
			{
				return this._TargetLocale;
			}
			set
			{
				this._TargetLocale = value;
			}
		}

		// Token: 0x170010B9 RID: 4281
		// (get) Token: 0x060058BB RID: 22715 RVA: 0x0010F64B File Offset: 0x0010D84B
		public List<string> TargetLocaleList
		{
			get
			{
				return this._TargetLocale;
			}
		}

		// Token: 0x170010BA RID: 4282
		// (get) Token: 0x060058BC RID: 22716 RVA: 0x0010F65C File Offset: 0x0010D85C
		public int TargetLocaleCount
		{
			get
			{
				return this._TargetLocale.Count;
			}
		}

		// Token: 0x060058BD RID: 22717 RVA: 0x0010F669 File Offset: 0x0010D869
		public void AddTargetLocale(string val)
		{
			this._TargetLocale.Add(val);
		}

		// Token: 0x060058BE RID: 22718 RVA: 0x0010F677 File Offset: 0x0010D877
		public void ClearTargetLocale()
		{
			this._TargetLocale.Clear();
		}

		// Token: 0x060058BF RID: 22719 RVA: 0x0010F684 File Offset: 0x0010D884
		public void SetTargetLocale(List<string> val)
		{
			this.TargetLocale = val;
		}

		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x060058C0 RID: 22720 RVA: 0x0010F68D File Offset: 0x0010D88D
		// (set) Token: 0x060058C1 RID: 22721 RVA: 0x0010F695 File Offset: 0x0010D895
		public List<string> TargetCountry
		{
			get
			{
				return this._TargetCountry;
			}
			set
			{
				this._TargetCountry = value;
			}
		}

		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x060058C2 RID: 22722 RVA: 0x0010F68D File Offset: 0x0010D88D
		public List<string> TargetCountryList
		{
			get
			{
				return this._TargetCountry;
			}
		}

		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x060058C3 RID: 22723 RVA: 0x0010F69E File Offset: 0x0010D89E
		public int TargetCountryCount
		{
			get
			{
				return this._TargetCountry.Count;
			}
		}

		// Token: 0x060058C4 RID: 22724 RVA: 0x0010F6AB File Offset: 0x0010D8AB
		public void AddTargetCountry(string val)
		{
			this._TargetCountry.Add(val);
		}

		// Token: 0x060058C5 RID: 22725 RVA: 0x0010F6B9 File Offset: 0x0010D8B9
		public void ClearTargetCountry()
		{
			this._TargetCountry.Clear();
		}

		// Token: 0x060058C6 RID: 22726 RVA: 0x0010F6C6 File Offset: 0x0010D8C6
		public void SetTargetCountry(List<string> val)
		{
			this.TargetCountry = val;
		}

		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x060058C7 RID: 22727 RVA: 0x0010F6CF File Offset: 0x0010D8CF
		// (set) Token: 0x060058C8 RID: 22728 RVA: 0x0010F6D7 File Offset: 0x0010D8D7
		public List<string> TargetIpMask
		{
			get
			{
				return this._TargetIpMask;
			}
			set
			{
				this._TargetIpMask = value;
			}
		}

		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x060058C9 RID: 22729 RVA: 0x0010F6CF File Offset: 0x0010D8CF
		public List<string> TargetIpMaskList
		{
			get
			{
				return this._TargetIpMask;
			}
		}

		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x060058CA RID: 22730 RVA: 0x0010F6E0 File Offset: 0x0010D8E0
		public int TargetIpMaskCount
		{
			get
			{
				return this._TargetIpMask.Count;
			}
		}

		// Token: 0x060058CB RID: 22731 RVA: 0x0010F6ED File Offset: 0x0010D8ED
		public void AddTargetIpMask(string val)
		{
			this._TargetIpMask.Add(val);
		}

		// Token: 0x060058CC RID: 22732 RVA: 0x0010F6FB File Offset: 0x0010D8FB
		public void ClearTargetIpMask()
		{
			this._TargetIpMask.Clear();
		}

		// Token: 0x060058CD RID: 22733 RVA: 0x0010F708 File Offset: 0x0010D908
		public void SetTargetIpMask(List<string> val)
		{
			this.TargetIpMask = val;
		}

		// Token: 0x060058CE RID: 22734 RVA: 0x0010F714 File Offset: 0x0010D914
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (AccountLicense accountLicense in this.TargetLicense)
			{
				num ^= accountLicense.GetHashCode();
			}
			if (this.HasEmployeeOnly)
			{
				num ^= this.EmployeeOnly.GetHashCode();
			}
			foreach (string text in this.TargetProgram)
			{
				num ^= text.GetHashCode();
			}
			foreach (string text2 in this.TargetLocale)
			{
				num ^= text2.GetHashCode();
			}
			foreach (string text3 in this.TargetCountry)
			{
				num ^= text3.GetHashCode();
			}
			foreach (string text4 in this.TargetIpMask)
			{
				num ^= text4.GetHashCode();
			}
			return num;
		}

		// Token: 0x060058CF RID: 22735 RVA: 0x0010F8AC File Offset: 0x0010DAAC
		public override bool Equals(object obj)
		{
			BroadcastFilter broadcastFilter = obj as BroadcastFilter;
			if (broadcastFilter == null)
			{
				return false;
			}
			if (this.TargetLicense.Count != broadcastFilter.TargetLicense.Count)
			{
				return false;
			}
			for (int i = 0; i < this.TargetLicense.Count; i++)
			{
				if (!this.TargetLicense[i].Equals(broadcastFilter.TargetLicense[i]))
				{
					return false;
				}
			}
			if (this.HasEmployeeOnly != broadcastFilter.HasEmployeeOnly || (this.HasEmployeeOnly && !this.EmployeeOnly.Equals(broadcastFilter.EmployeeOnly)))
			{
				return false;
			}
			if (this.TargetProgram.Count != broadcastFilter.TargetProgram.Count)
			{
				return false;
			}
			for (int j = 0; j < this.TargetProgram.Count; j++)
			{
				if (!this.TargetProgram[j].Equals(broadcastFilter.TargetProgram[j]))
				{
					return false;
				}
			}
			if (this.TargetLocale.Count != broadcastFilter.TargetLocale.Count)
			{
				return false;
			}
			for (int k = 0; k < this.TargetLocale.Count; k++)
			{
				if (!this.TargetLocale[k].Equals(broadcastFilter.TargetLocale[k]))
				{
					return false;
				}
			}
			if (this.TargetCountry.Count != broadcastFilter.TargetCountry.Count)
			{
				return false;
			}
			for (int l = 0; l < this.TargetCountry.Count; l++)
			{
				if (!this.TargetCountry[l].Equals(broadcastFilter.TargetCountry[l]))
				{
					return false;
				}
			}
			if (this.TargetIpMask.Count != broadcastFilter.TargetIpMask.Count)
			{
				return false;
			}
			for (int m = 0; m < this.TargetIpMask.Count; m++)
			{
				if (!this.TargetIpMask[m].Equals(broadcastFilter.TargetIpMask[m]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x060058D0 RID: 22736 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060058D1 RID: 22737 RVA: 0x0010FA9B File Offset: 0x0010DC9B
		public static BroadcastFilter ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<BroadcastFilter>(bs, 0, -1);
		}

		// Token: 0x060058D2 RID: 22738 RVA: 0x0010FAA5 File Offset: 0x0010DCA5
		public void Deserialize(Stream stream)
		{
			BroadcastFilter.Deserialize(stream, this);
		}

		// Token: 0x060058D3 RID: 22739 RVA: 0x0010FAAF File Offset: 0x0010DCAF
		public static BroadcastFilter Deserialize(Stream stream, BroadcastFilter instance)
		{
			return BroadcastFilter.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060058D4 RID: 22740 RVA: 0x0010FABC File Offset: 0x0010DCBC
		public static BroadcastFilter DeserializeLengthDelimited(Stream stream)
		{
			BroadcastFilter broadcastFilter = new BroadcastFilter();
			BroadcastFilter.DeserializeLengthDelimited(stream, broadcastFilter);
			return broadcastFilter;
		}

		// Token: 0x060058D5 RID: 22741 RVA: 0x0010FAD8 File Offset: 0x0010DCD8
		public static BroadcastFilter DeserializeLengthDelimited(Stream stream, BroadcastFilter instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BroadcastFilter.Deserialize(stream, instance, num);
		}

		// Token: 0x060058D6 RID: 22742 RVA: 0x0010FB00 File Offset: 0x0010DD00
		public static BroadcastFilter Deserialize(Stream stream, BroadcastFilter instance, long limit)
		{
			if (instance.TargetLicense == null)
			{
				instance.TargetLicense = new List<AccountLicense>();
			}
			if (instance.TargetProgram == null)
			{
				instance.TargetProgram = new List<string>();
			}
			if (instance.TargetLocale == null)
			{
				instance.TargetLocale = new List<string>();
			}
			if (instance.TargetCountry == null)
			{
				instance.TargetCountry = new List<string>();
			}
			if (instance.TargetIpMask == null)
			{
				instance.TargetIpMask = new List<string>();
			}
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
					if (num <= 26)
					{
						if (num == 10)
						{
							instance.TargetLicense.Add(AccountLicense.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 16)
						{
							instance.EmployeeOnly = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 26)
						{
							instance.TargetProgram.Add(ProtocolParser.ReadString(stream));
							continue;
						}
					}
					else
					{
						if (num == 34)
						{
							instance.TargetLocale.Add(ProtocolParser.ReadString(stream));
							continue;
						}
						if (num == 42)
						{
							instance.TargetCountry.Add(ProtocolParser.ReadString(stream));
							continue;
						}
						if (num == 50)
						{
							instance.TargetIpMask.Add(ProtocolParser.ReadString(stream));
							continue;
						}
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

		// Token: 0x060058D7 RID: 22743 RVA: 0x0010FC7B File Offset: 0x0010DE7B
		public void Serialize(Stream stream)
		{
			BroadcastFilter.Serialize(stream, this);
		}

		// Token: 0x060058D8 RID: 22744 RVA: 0x0010FC84 File Offset: 0x0010DE84
		public static void Serialize(Stream stream, BroadcastFilter instance)
		{
			if (instance.TargetLicense.Count > 0)
			{
				foreach (AccountLicense accountLicense in instance.TargetLicense)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, accountLicense.GetSerializedSize());
					AccountLicense.Serialize(stream, accountLicense);
				}
			}
			if (instance.HasEmployeeOnly)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.EmployeeOnly);
			}
			if (instance.TargetProgram.Count > 0)
			{
				foreach (string s in instance.TargetProgram)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(s));
				}
			}
			if (instance.TargetLocale.Count > 0)
			{
				foreach (string s2 in instance.TargetLocale)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(s2));
				}
			}
			if (instance.TargetCountry.Count > 0)
			{
				foreach (string s3 in instance.TargetCountry)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(s3));
				}
			}
			if (instance.TargetIpMask.Count > 0)
			{
				foreach (string s4 in instance.TargetIpMask)
				{
					stream.WriteByte(50);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(s4));
				}
			}
		}

		// Token: 0x060058D9 RID: 22745 RVA: 0x0010FEA4 File Offset: 0x0010E0A4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.TargetLicense.Count > 0)
			{
				foreach (AccountLicense accountLicense in this.TargetLicense)
				{
					num += 1U;
					uint serializedSize = accountLicense.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasEmployeeOnly)
			{
				num += 1U;
				num += 1U;
			}
			if (this.TargetProgram.Count > 0)
			{
				foreach (string s in this.TargetProgram)
				{
					num += 1U;
					uint byteCount = (uint)Encoding.UTF8.GetByteCount(s);
					num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
				}
			}
			if (this.TargetLocale.Count > 0)
			{
				foreach (string s2 in this.TargetLocale)
				{
					num += 1U;
					uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(s2);
					num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
				}
			}
			if (this.TargetCountry.Count > 0)
			{
				foreach (string s3 in this.TargetCountry)
				{
					num += 1U;
					uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(s3);
					num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
				}
			}
			if (this.TargetIpMask.Count > 0)
			{
				foreach (string s4 in this.TargetIpMask)
				{
					num += 1U;
					uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(s4);
					num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
				}
			}
			return num;
		}

		// Token: 0x04001BC1 RID: 7105
		private List<AccountLicense> _TargetLicense = new List<AccountLicense>();

		// Token: 0x04001BC2 RID: 7106
		public bool HasEmployeeOnly;

		// Token: 0x04001BC3 RID: 7107
		private bool _EmployeeOnly;

		// Token: 0x04001BC4 RID: 7108
		private List<string> _TargetProgram = new List<string>();

		// Token: 0x04001BC5 RID: 7109
		private List<string> _TargetLocale = new List<string>();

		// Token: 0x04001BC6 RID: 7110
		private List<string> _TargetCountry = new List<string>();

		// Token: 0x04001BC7 RID: 7111
		private List<string> _TargetIpMask = new List<string>();
	}
}
