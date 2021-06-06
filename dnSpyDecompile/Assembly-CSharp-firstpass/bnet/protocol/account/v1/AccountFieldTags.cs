using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000528 RID: 1320
	public class AccountFieldTags : IProtoBuf
	{
		// Token: 0x170011C3 RID: 4547
		// (get) Token: 0x06005E48 RID: 24136 RVA: 0x0011D9A0 File Offset: 0x0011BBA0
		// (set) Token: 0x06005E49 RID: 24137 RVA: 0x0011D9A8 File Offset: 0x0011BBA8
		public uint AccountLevelInfoTag
		{
			get
			{
				return this._AccountLevelInfoTag;
			}
			set
			{
				this._AccountLevelInfoTag = value;
				this.HasAccountLevelInfoTag = true;
			}
		}

		// Token: 0x06005E4A RID: 24138 RVA: 0x0011D9B8 File Offset: 0x0011BBB8
		public void SetAccountLevelInfoTag(uint val)
		{
			this.AccountLevelInfoTag = val;
		}

		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x06005E4B RID: 24139 RVA: 0x0011D9C1 File Offset: 0x0011BBC1
		// (set) Token: 0x06005E4C RID: 24140 RVA: 0x0011D9C9 File Offset: 0x0011BBC9
		public uint PrivacyInfoTag
		{
			get
			{
				return this._PrivacyInfoTag;
			}
			set
			{
				this._PrivacyInfoTag = value;
				this.HasPrivacyInfoTag = true;
			}
		}

		// Token: 0x06005E4D RID: 24141 RVA: 0x0011D9D9 File Offset: 0x0011BBD9
		public void SetPrivacyInfoTag(uint val)
		{
			this.PrivacyInfoTag = val;
		}

		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x06005E4E RID: 24142 RVA: 0x0011D9E2 File Offset: 0x0011BBE2
		// (set) Token: 0x06005E4F RID: 24143 RVA: 0x0011D9EA File Offset: 0x0011BBEA
		public uint ParentalControlInfoTag
		{
			get
			{
				return this._ParentalControlInfoTag;
			}
			set
			{
				this._ParentalControlInfoTag = value;
				this.HasParentalControlInfoTag = true;
			}
		}

		// Token: 0x06005E50 RID: 24144 RVA: 0x0011D9FA File Offset: 0x0011BBFA
		public void SetParentalControlInfoTag(uint val)
		{
			this.ParentalControlInfoTag = val;
		}

		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x06005E51 RID: 24145 RVA: 0x0011DA03 File Offset: 0x0011BC03
		// (set) Token: 0x06005E52 RID: 24146 RVA: 0x0011DA0B File Offset: 0x0011BC0B
		public List<ProgramTag> GameLevelInfoTags
		{
			get
			{
				return this._GameLevelInfoTags;
			}
			set
			{
				this._GameLevelInfoTags = value;
			}
		}

		// Token: 0x170011C7 RID: 4551
		// (get) Token: 0x06005E53 RID: 24147 RVA: 0x0011DA03 File Offset: 0x0011BC03
		public List<ProgramTag> GameLevelInfoTagsList
		{
			get
			{
				return this._GameLevelInfoTags;
			}
		}

		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x06005E54 RID: 24148 RVA: 0x0011DA14 File Offset: 0x0011BC14
		public int GameLevelInfoTagsCount
		{
			get
			{
				return this._GameLevelInfoTags.Count;
			}
		}

		// Token: 0x06005E55 RID: 24149 RVA: 0x0011DA21 File Offset: 0x0011BC21
		public void AddGameLevelInfoTags(ProgramTag val)
		{
			this._GameLevelInfoTags.Add(val);
		}

		// Token: 0x06005E56 RID: 24150 RVA: 0x0011DA2F File Offset: 0x0011BC2F
		public void ClearGameLevelInfoTags()
		{
			this._GameLevelInfoTags.Clear();
		}

		// Token: 0x06005E57 RID: 24151 RVA: 0x0011DA3C File Offset: 0x0011BC3C
		public void SetGameLevelInfoTags(List<ProgramTag> val)
		{
			this.GameLevelInfoTags = val;
		}

		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x06005E58 RID: 24152 RVA: 0x0011DA45 File Offset: 0x0011BC45
		// (set) Token: 0x06005E59 RID: 24153 RVA: 0x0011DA4D File Offset: 0x0011BC4D
		public List<ProgramTag> GameStatusTags
		{
			get
			{
				return this._GameStatusTags;
			}
			set
			{
				this._GameStatusTags = value;
			}
		}

		// Token: 0x170011CA RID: 4554
		// (get) Token: 0x06005E5A RID: 24154 RVA: 0x0011DA45 File Offset: 0x0011BC45
		public List<ProgramTag> GameStatusTagsList
		{
			get
			{
				return this._GameStatusTags;
			}
		}

		// Token: 0x170011CB RID: 4555
		// (get) Token: 0x06005E5B RID: 24155 RVA: 0x0011DA56 File Offset: 0x0011BC56
		public int GameStatusTagsCount
		{
			get
			{
				return this._GameStatusTags.Count;
			}
		}

		// Token: 0x06005E5C RID: 24156 RVA: 0x0011DA63 File Offset: 0x0011BC63
		public void AddGameStatusTags(ProgramTag val)
		{
			this._GameStatusTags.Add(val);
		}

		// Token: 0x06005E5D RID: 24157 RVA: 0x0011DA71 File Offset: 0x0011BC71
		public void ClearGameStatusTags()
		{
			this._GameStatusTags.Clear();
		}

		// Token: 0x06005E5E RID: 24158 RVA: 0x0011DA7E File Offset: 0x0011BC7E
		public void SetGameStatusTags(List<ProgramTag> val)
		{
			this.GameStatusTags = val;
		}

		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x06005E5F RID: 24159 RVA: 0x0011DA87 File Offset: 0x0011BC87
		// (set) Token: 0x06005E60 RID: 24160 RVA: 0x0011DA8F File Offset: 0x0011BC8F
		public List<RegionTag> GameAccountTags
		{
			get
			{
				return this._GameAccountTags;
			}
			set
			{
				this._GameAccountTags = value;
			}
		}

		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x06005E61 RID: 24161 RVA: 0x0011DA87 File Offset: 0x0011BC87
		public List<RegionTag> GameAccountTagsList
		{
			get
			{
				return this._GameAccountTags;
			}
		}

		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x06005E62 RID: 24162 RVA: 0x0011DA98 File Offset: 0x0011BC98
		public int GameAccountTagsCount
		{
			get
			{
				return this._GameAccountTags.Count;
			}
		}

		// Token: 0x06005E63 RID: 24163 RVA: 0x0011DAA5 File Offset: 0x0011BCA5
		public void AddGameAccountTags(RegionTag val)
		{
			this._GameAccountTags.Add(val);
		}

		// Token: 0x06005E64 RID: 24164 RVA: 0x0011DAB3 File Offset: 0x0011BCB3
		public void ClearGameAccountTags()
		{
			this._GameAccountTags.Clear();
		}

		// Token: 0x06005E65 RID: 24165 RVA: 0x0011DAC0 File Offset: 0x0011BCC0
		public void SetGameAccountTags(List<RegionTag> val)
		{
			this.GameAccountTags = val;
		}

		// Token: 0x06005E66 RID: 24166 RVA: 0x0011DACC File Offset: 0x0011BCCC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAccountLevelInfoTag)
			{
				num ^= this.AccountLevelInfoTag.GetHashCode();
			}
			if (this.HasPrivacyInfoTag)
			{
				num ^= this.PrivacyInfoTag.GetHashCode();
			}
			if (this.HasParentalControlInfoTag)
			{
				num ^= this.ParentalControlInfoTag.GetHashCode();
			}
			foreach (ProgramTag programTag in this.GameLevelInfoTags)
			{
				num ^= programTag.GetHashCode();
			}
			foreach (ProgramTag programTag2 in this.GameStatusTags)
			{
				num ^= programTag2.GetHashCode();
			}
			foreach (RegionTag regionTag in this.GameAccountTags)
			{
				num ^= regionTag.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005E67 RID: 24167 RVA: 0x0011DC08 File Offset: 0x0011BE08
		public override bool Equals(object obj)
		{
			AccountFieldTags accountFieldTags = obj as AccountFieldTags;
			if (accountFieldTags == null)
			{
				return false;
			}
			if (this.HasAccountLevelInfoTag != accountFieldTags.HasAccountLevelInfoTag || (this.HasAccountLevelInfoTag && !this.AccountLevelInfoTag.Equals(accountFieldTags.AccountLevelInfoTag)))
			{
				return false;
			}
			if (this.HasPrivacyInfoTag != accountFieldTags.HasPrivacyInfoTag || (this.HasPrivacyInfoTag && !this.PrivacyInfoTag.Equals(accountFieldTags.PrivacyInfoTag)))
			{
				return false;
			}
			if (this.HasParentalControlInfoTag != accountFieldTags.HasParentalControlInfoTag || (this.HasParentalControlInfoTag && !this.ParentalControlInfoTag.Equals(accountFieldTags.ParentalControlInfoTag)))
			{
				return false;
			}
			if (this.GameLevelInfoTags.Count != accountFieldTags.GameLevelInfoTags.Count)
			{
				return false;
			}
			for (int i = 0; i < this.GameLevelInfoTags.Count; i++)
			{
				if (!this.GameLevelInfoTags[i].Equals(accountFieldTags.GameLevelInfoTags[i]))
				{
					return false;
				}
			}
			if (this.GameStatusTags.Count != accountFieldTags.GameStatusTags.Count)
			{
				return false;
			}
			for (int j = 0; j < this.GameStatusTags.Count; j++)
			{
				if (!this.GameStatusTags[j].Equals(accountFieldTags.GameStatusTags[j]))
				{
					return false;
				}
			}
			if (this.GameAccountTags.Count != accountFieldTags.GameAccountTags.Count)
			{
				return false;
			}
			for (int k = 0; k < this.GameAccountTags.Count; k++)
			{
				if (!this.GameAccountTags[k].Equals(accountFieldTags.GameAccountTags[k]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x06005E68 RID: 24168 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005E69 RID: 24169 RVA: 0x0011DDA5 File Offset: 0x0011BFA5
		public static AccountFieldTags ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountFieldTags>(bs, 0, -1);
		}

		// Token: 0x06005E6A RID: 24170 RVA: 0x0011DDAF File Offset: 0x0011BFAF
		public void Deserialize(Stream stream)
		{
			AccountFieldTags.Deserialize(stream, this);
		}

		// Token: 0x06005E6B RID: 24171 RVA: 0x0011DDB9 File Offset: 0x0011BFB9
		public static AccountFieldTags Deserialize(Stream stream, AccountFieldTags instance)
		{
			return AccountFieldTags.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005E6C RID: 24172 RVA: 0x0011DDC4 File Offset: 0x0011BFC4
		public static AccountFieldTags DeserializeLengthDelimited(Stream stream)
		{
			AccountFieldTags accountFieldTags = new AccountFieldTags();
			AccountFieldTags.DeserializeLengthDelimited(stream, accountFieldTags);
			return accountFieldTags;
		}

		// Token: 0x06005E6D RID: 24173 RVA: 0x0011DDE0 File Offset: 0x0011BFE0
		public static AccountFieldTags DeserializeLengthDelimited(Stream stream, AccountFieldTags instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AccountFieldTags.Deserialize(stream, instance, num);
		}

		// Token: 0x06005E6E RID: 24174 RVA: 0x0011DE08 File Offset: 0x0011C008
		public static AccountFieldTags Deserialize(Stream stream, AccountFieldTags instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.GameLevelInfoTags == null)
			{
				instance.GameLevelInfoTags = new List<ProgramTag>();
			}
			if (instance.GameStatusTags == null)
			{
				instance.GameStatusTags = new List<ProgramTag>();
			}
			if (instance.GameAccountTags == null)
			{
				instance.GameAccountTags = new List<RegionTag>();
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
					if (num <= 37)
					{
						if (num == 21)
						{
							instance.AccountLevelInfoTag = binaryReader.ReadUInt32();
							continue;
						}
						if (num == 29)
						{
							instance.PrivacyInfoTag = binaryReader.ReadUInt32();
							continue;
						}
						if (num == 37)
						{
							instance.ParentalControlInfoTag = binaryReader.ReadUInt32();
							continue;
						}
					}
					else
					{
						if (num == 58)
						{
							instance.GameLevelInfoTags.Add(ProgramTag.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 74)
						{
							instance.GameStatusTags.Add(ProgramTag.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 90)
						{
							instance.GameAccountTags.Add(RegionTag.DeserializeLengthDelimited(stream));
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

		// Token: 0x06005E6F RID: 24175 RVA: 0x0011DF5A File Offset: 0x0011C15A
		public void Serialize(Stream stream)
		{
			AccountFieldTags.Serialize(stream, this);
		}

		// Token: 0x06005E70 RID: 24176 RVA: 0x0011DF64 File Offset: 0x0011C164
		public static void Serialize(Stream stream, AccountFieldTags instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAccountLevelInfoTag)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.AccountLevelInfoTag);
			}
			if (instance.HasPrivacyInfoTag)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.PrivacyInfoTag);
			}
			if (instance.HasParentalControlInfoTag)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.ParentalControlInfoTag);
			}
			if (instance.GameLevelInfoTags.Count > 0)
			{
				foreach (ProgramTag programTag in instance.GameLevelInfoTags)
				{
					stream.WriteByte(58);
					ProtocolParser.WriteUInt32(stream, programTag.GetSerializedSize());
					ProgramTag.Serialize(stream, programTag);
				}
			}
			if (instance.GameStatusTags.Count > 0)
			{
				foreach (ProgramTag programTag2 in instance.GameStatusTags)
				{
					stream.WriteByte(74);
					ProtocolParser.WriteUInt32(stream, programTag2.GetSerializedSize());
					ProgramTag.Serialize(stream, programTag2);
				}
			}
			if (instance.GameAccountTags.Count > 0)
			{
				foreach (RegionTag regionTag in instance.GameAccountTags)
				{
					stream.WriteByte(90);
					ProtocolParser.WriteUInt32(stream, regionTag.GetSerializedSize());
					RegionTag.Serialize(stream, regionTag);
				}
			}
		}

		// Token: 0x06005E71 RID: 24177 RVA: 0x0011E100 File Offset: 0x0011C300
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAccountLevelInfoTag)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasPrivacyInfoTag)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasParentalControlInfoTag)
			{
				num += 1U;
				num += 4U;
			}
			if (this.GameLevelInfoTags.Count > 0)
			{
				foreach (ProgramTag programTag in this.GameLevelInfoTags)
				{
					num += 1U;
					uint serializedSize = programTag.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.GameStatusTags.Count > 0)
			{
				foreach (ProgramTag programTag2 in this.GameStatusTags)
				{
					num += 1U;
					uint serializedSize2 = programTag2.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.GameAccountTags.Count > 0)
			{
				foreach (RegionTag regionTag in this.GameAccountTags)
				{
					num += 1U;
					uint serializedSize3 = regionTag.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			return num;
		}

		// Token: 0x04001CFE RID: 7422
		public bool HasAccountLevelInfoTag;

		// Token: 0x04001CFF RID: 7423
		private uint _AccountLevelInfoTag;

		// Token: 0x04001D00 RID: 7424
		public bool HasPrivacyInfoTag;

		// Token: 0x04001D01 RID: 7425
		private uint _PrivacyInfoTag;

		// Token: 0x04001D02 RID: 7426
		public bool HasParentalControlInfoTag;

		// Token: 0x04001D03 RID: 7427
		private uint _ParentalControlInfoTag;

		// Token: 0x04001D04 RID: 7428
		private List<ProgramTag> _GameLevelInfoTags = new List<ProgramTag>();

		// Token: 0x04001D05 RID: 7429
		private List<ProgramTag> _GameStatusTags = new List<ProgramTag>();

		// Token: 0x04001D06 RID: 7430
		private List<RegionTag> _GameAccountTags = new List<RegionTag>();
	}
}
