using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusShared
{
	// Token: 0x0200015E RID: 350
	public class TavernBrawlSeasonSpec : IProtoBuf
	{
		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060017D2 RID: 6098 RVA: 0x000532AB File Offset: 0x000514AB
		// (set) Token: 0x060017D3 RID: 6099 RVA: 0x000532B3 File Offset: 0x000514B3
		public GameContentSeasonSpec GameContentSeason { get; set; }

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x000532BC File Offset: 0x000514BC
		// (set) Token: 0x060017D5 RID: 6101 RVA: 0x000532C4 File Offset: 0x000514C4
		public TavernBrawlPopupType StorePopupType
		{
			get
			{
				return this._StorePopupType;
			}
			set
			{
				this._StorePopupType = value;
				this.HasStorePopupType = true;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x000532D4 File Offset: 0x000514D4
		// (set) Token: 0x060017D7 RID: 6103 RVA: 0x000532DC File Offset: 0x000514DC
		public string RewardDesc
		{
			get
			{
				return this._RewardDesc;
			}
			set
			{
				this._RewardDesc = value;
				this.HasRewardDesc = (value != null);
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x000532EF File Offset: 0x000514EF
		// (set) Token: 0x060017D9 RID: 6105 RVA: 0x000532F7 File Offset: 0x000514F7
		public string MinRewardDesc
		{
			get
			{
				return this._MinRewardDesc;
			}
			set
			{
				this._MinRewardDesc = value;
				this.HasMinRewardDesc = (value != null);
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x0005330A File Offset: 0x0005150A
		// (set) Token: 0x060017DB RID: 6107 RVA: 0x00053312 File Offset: 0x00051512
		public string MaxRewardDesc
		{
			get
			{
				return this._MaxRewardDesc;
			}
			set
			{
				this._MaxRewardDesc = value;
				this.HasMaxRewardDesc = (value != null);
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060017DC RID: 6108 RVA: 0x00053325 File Offset: 0x00051525
		// (set) Token: 0x060017DD RID: 6109 RVA: 0x0005332D File Offset: 0x0005152D
		public string EndConditionDesc
		{
			get
			{
				return this._EndConditionDesc;
			}
			set
			{
				this._EndConditionDesc = value;
				this.HasEndConditionDesc = (value != null);
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060017DE RID: 6110 RVA: 0x00053340 File Offset: 0x00051540
		// (set) Token: 0x060017DF RID: 6111 RVA: 0x00053348 File Offset: 0x00051548
		public string DeprecatedStoreInstructionPrefab
		{
			get
			{
				return this._DeprecatedStoreInstructionPrefab;
			}
			set
			{
				this._DeprecatedStoreInstructionPrefab = value;
				this.HasDeprecatedStoreInstructionPrefab = (value != null);
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x0005335B File Offset: 0x0005155B
		// (set) Token: 0x060017E1 RID: 6113 RVA: 0x00053363 File Offset: 0x00051563
		public string DeprecatedStoreInstructionPrefabPhone
		{
			get
			{
				return this._DeprecatedStoreInstructionPrefabPhone;
			}
			set
			{
				this._DeprecatedStoreInstructionPrefabPhone = value;
				this.HasDeprecatedStoreInstructionPrefabPhone = (value != null);
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060017E2 RID: 6114 RVA: 0x00053376 File Offset: 0x00051576
		// (set) Token: 0x060017E3 RID: 6115 RVA: 0x0005337E File Offset: 0x0005157E
		public TavernBrawlMode DeprecatedBrawlMode
		{
			get
			{
				return this._DeprecatedBrawlMode;
			}
			set
			{
				this._DeprecatedBrawlMode = value;
				this.HasDeprecatedBrawlMode = true;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060017E4 RID: 6116 RVA: 0x0005338E File Offset: 0x0005158E
		// (set) Token: 0x060017E5 RID: 6117 RVA: 0x00053396 File Offset: 0x00051596
		public List<LocalizedString> Strings
		{
			get
			{
				return this._Strings;
			}
			set
			{
				this._Strings = value;
			}
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x000533A0 File Offset: 0x000515A0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.GameContentSeason.GetHashCode();
			if (this.HasStorePopupType)
			{
				num ^= this.StorePopupType.GetHashCode();
			}
			if (this.HasRewardDesc)
			{
				num ^= this.RewardDesc.GetHashCode();
			}
			if (this.HasMinRewardDesc)
			{
				num ^= this.MinRewardDesc.GetHashCode();
			}
			if (this.HasMaxRewardDesc)
			{
				num ^= this.MaxRewardDesc.GetHashCode();
			}
			if (this.HasEndConditionDesc)
			{
				num ^= this.EndConditionDesc.GetHashCode();
			}
			if (this.HasDeprecatedStoreInstructionPrefab)
			{
				num ^= this.DeprecatedStoreInstructionPrefab.GetHashCode();
			}
			if (this.HasDeprecatedStoreInstructionPrefabPhone)
			{
				num ^= this.DeprecatedStoreInstructionPrefabPhone.GetHashCode();
			}
			if (this.HasDeprecatedBrawlMode)
			{
				num ^= this.DeprecatedBrawlMode.GetHashCode();
			}
			foreach (LocalizedString localizedString in this.Strings)
			{
				num ^= localizedString.GetHashCode();
			}
			return num;
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x000534D4 File Offset: 0x000516D4
		public override bool Equals(object obj)
		{
			TavernBrawlSeasonSpec tavernBrawlSeasonSpec = obj as TavernBrawlSeasonSpec;
			if (tavernBrawlSeasonSpec == null)
			{
				return false;
			}
			if (!this.GameContentSeason.Equals(tavernBrawlSeasonSpec.GameContentSeason))
			{
				return false;
			}
			if (this.HasStorePopupType != tavernBrawlSeasonSpec.HasStorePopupType || (this.HasStorePopupType && !this.StorePopupType.Equals(tavernBrawlSeasonSpec.StorePopupType)))
			{
				return false;
			}
			if (this.HasRewardDesc != tavernBrawlSeasonSpec.HasRewardDesc || (this.HasRewardDesc && !this.RewardDesc.Equals(tavernBrawlSeasonSpec.RewardDesc)))
			{
				return false;
			}
			if (this.HasMinRewardDesc != tavernBrawlSeasonSpec.HasMinRewardDesc || (this.HasMinRewardDesc && !this.MinRewardDesc.Equals(tavernBrawlSeasonSpec.MinRewardDesc)))
			{
				return false;
			}
			if (this.HasMaxRewardDesc != tavernBrawlSeasonSpec.HasMaxRewardDesc || (this.HasMaxRewardDesc && !this.MaxRewardDesc.Equals(tavernBrawlSeasonSpec.MaxRewardDesc)))
			{
				return false;
			}
			if (this.HasEndConditionDesc != tavernBrawlSeasonSpec.HasEndConditionDesc || (this.HasEndConditionDesc && !this.EndConditionDesc.Equals(tavernBrawlSeasonSpec.EndConditionDesc)))
			{
				return false;
			}
			if (this.HasDeprecatedStoreInstructionPrefab != tavernBrawlSeasonSpec.HasDeprecatedStoreInstructionPrefab || (this.HasDeprecatedStoreInstructionPrefab && !this.DeprecatedStoreInstructionPrefab.Equals(tavernBrawlSeasonSpec.DeprecatedStoreInstructionPrefab)))
			{
				return false;
			}
			if (this.HasDeprecatedStoreInstructionPrefabPhone != tavernBrawlSeasonSpec.HasDeprecatedStoreInstructionPrefabPhone || (this.HasDeprecatedStoreInstructionPrefabPhone && !this.DeprecatedStoreInstructionPrefabPhone.Equals(tavernBrawlSeasonSpec.DeprecatedStoreInstructionPrefabPhone)))
			{
				return false;
			}
			if (this.HasDeprecatedBrawlMode != tavernBrawlSeasonSpec.HasDeprecatedBrawlMode || (this.HasDeprecatedBrawlMode && !this.DeprecatedBrawlMode.Equals(tavernBrawlSeasonSpec.DeprecatedBrawlMode)))
			{
				return false;
			}
			if (this.Strings.Count != tavernBrawlSeasonSpec.Strings.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Strings.Count; i++)
			{
				if (!this.Strings[i].Equals(tavernBrawlSeasonSpec.Strings[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x000536C8 File Offset: 0x000518C8
		public void Deserialize(Stream stream)
		{
			TavernBrawlSeasonSpec.Deserialize(stream, this);
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x000536D2 File Offset: 0x000518D2
		public static TavernBrawlSeasonSpec Deserialize(Stream stream, TavernBrawlSeasonSpec instance)
		{
			return TavernBrawlSeasonSpec.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x000536E0 File Offset: 0x000518E0
		public static TavernBrawlSeasonSpec DeserializeLengthDelimited(Stream stream)
		{
			TavernBrawlSeasonSpec tavernBrawlSeasonSpec = new TavernBrawlSeasonSpec();
			TavernBrawlSeasonSpec.DeserializeLengthDelimited(stream, tavernBrawlSeasonSpec);
			return tavernBrawlSeasonSpec;
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x000536FC File Offset: 0x000518FC
		public static TavernBrawlSeasonSpec DeserializeLengthDelimited(Stream stream, TavernBrawlSeasonSpec instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return TavernBrawlSeasonSpec.Deserialize(stream, instance, num);
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x00053724 File Offset: 0x00051924
		public static TavernBrawlSeasonSpec Deserialize(Stream stream, TavernBrawlSeasonSpec instance, long limit)
		{
			instance.StorePopupType = TavernBrawlPopupType.POPUP_TYPE_NONE;
			instance.DeprecatedBrawlMode = TavernBrawlMode.TB_MODE_NORMAL;
			if (instance.Strings == null)
			{
				instance.Strings = new List<LocalizedString>();
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
					if (num <= 34)
					{
						if (num <= 16)
						{
							if (num != 10)
							{
								if (num == 16)
								{
									instance.StorePopupType = (TavernBrawlPopupType)ProtocolParser.ReadUInt64(stream);
									continue;
								}
							}
							else
							{
								if (instance.GameContentSeason == null)
								{
									instance.GameContentSeason = GameContentSeasonSpec.DeserializeLengthDelimited(stream);
									continue;
								}
								GameContentSeasonSpec.DeserializeLengthDelimited(stream, instance.GameContentSeason);
								continue;
							}
						}
						else
						{
							if (num == 26)
							{
								instance.RewardDesc = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 34)
							{
								instance.MinRewardDesc = ProtocolParser.ReadString(stream);
								continue;
							}
						}
					}
					else if (num <= 50)
					{
						if (num == 42)
						{
							instance.MaxRewardDesc = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 50)
						{
							instance.EndConditionDesc = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 58)
						{
							instance.DeprecatedStoreInstructionPrefab = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 66)
						{
							instance.DeprecatedStoreInstructionPrefabPhone = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 72)
						{
							instance.DeprecatedBrawlMode = (TavernBrawlMode)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					if (field != 100U)
					{
						ProtocolParser.SkipKey(stream, key);
					}
					else if (key.WireType == Wire.LengthDelimited)
					{
						instance.Strings.Add(LocalizedString.DeserializeLengthDelimited(stream));
					}
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x000538FE File Offset: 0x00051AFE
		public void Serialize(Stream stream)
		{
			TavernBrawlSeasonSpec.Serialize(stream, this);
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x00053908 File Offset: 0x00051B08
		public static void Serialize(Stream stream, TavernBrawlSeasonSpec instance)
		{
			if (instance.GameContentSeason == null)
			{
				throw new ArgumentNullException("GameContentSeason", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameContentSeason.GetSerializedSize());
			GameContentSeasonSpec.Serialize(stream, instance.GameContentSeason);
			if (instance.HasStorePopupType)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.StorePopupType));
			}
			if (instance.HasRewardDesc)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.RewardDesc));
			}
			if (instance.HasMinRewardDesc)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.MinRewardDesc));
			}
			if (instance.HasMaxRewardDesc)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.MaxRewardDesc));
			}
			if (instance.HasEndConditionDesc)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.EndConditionDesc));
			}
			if (instance.HasDeprecatedStoreInstructionPrefab)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeprecatedStoreInstructionPrefab));
			}
			if (instance.HasDeprecatedStoreInstructionPrefabPhone)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeprecatedStoreInstructionPrefabPhone));
			}
			if (instance.HasDeprecatedBrawlMode)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DeprecatedBrawlMode));
			}
			if (instance.Strings.Count > 0)
			{
				foreach (LocalizedString localizedString in instance.Strings)
				{
					stream.WriteByte(162);
					stream.WriteByte(6);
					ProtocolParser.WriteUInt32(stream, localizedString.GetSerializedSize());
					LocalizedString.Serialize(stream, localizedString);
				}
			}
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x00053AE4 File Offset: 0x00051CE4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.GameContentSeason.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasStorePopupType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.StorePopupType));
			}
			if (this.HasRewardDesc)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.RewardDesc);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasMinRewardDesc)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.MinRewardDesc);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasMaxRewardDesc)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.MaxRewardDesc);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasEndConditionDesc)
			{
				num += 1U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.EndConditionDesc);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (this.HasDeprecatedStoreInstructionPrefab)
			{
				num += 1U;
				uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(this.DeprecatedStoreInstructionPrefab);
				num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			}
			if (this.HasDeprecatedStoreInstructionPrefabPhone)
			{
				num += 1U;
				uint byteCount6 = (uint)Encoding.UTF8.GetByteCount(this.DeprecatedStoreInstructionPrefabPhone);
				num += ProtocolParser.SizeOfUInt32(byteCount6) + byteCount6;
			}
			if (this.HasDeprecatedBrawlMode)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DeprecatedBrawlMode));
			}
			if (this.Strings.Count > 0)
			{
				foreach (LocalizedString localizedString in this.Strings)
				{
					num += 2U;
					uint serializedSize2 = localizedString.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			num += 1U;
			return num;
		}

		// Token: 0x04000792 RID: 1938
		public bool HasStorePopupType;

		// Token: 0x04000793 RID: 1939
		private TavernBrawlPopupType _StorePopupType;

		// Token: 0x04000794 RID: 1940
		public bool HasRewardDesc;

		// Token: 0x04000795 RID: 1941
		private string _RewardDesc;

		// Token: 0x04000796 RID: 1942
		public bool HasMinRewardDesc;

		// Token: 0x04000797 RID: 1943
		private string _MinRewardDesc;

		// Token: 0x04000798 RID: 1944
		public bool HasMaxRewardDesc;

		// Token: 0x04000799 RID: 1945
		private string _MaxRewardDesc;

		// Token: 0x0400079A RID: 1946
		public bool HasEndConditionDesc;

		// Token: 0x0400079B RID: 1947
		private string _EndConditionDesc;

		// Token: 0x0400079C RID: 1948
		public bool HasDeprecatedStoreInstructionPrefab;

		// Token: 0x0400079D RID: 1949
		private string _DeprecatedStoreInstructionPrefab;

		// Token: 0x0400079E RID: 1950
		public bool HasDeprecatedStoreInstructionPrefabPhone;

		// Token: 0x0400079F RID: 1951
		private string _DeprecatedStoreInstructionPrefabPhone;

		// Token: 0x040007A0 RID: 1952
		public bool HasDeprecatedBrawlMode;

		// Token: 0x040007A1 RID: 1953
		private TavernBrawlMode _DeprecatedBrawlMode;

		// Token: 0x040007A2 RID: 1954
		private List<LocalizedString> _Strings = new List<LocalizedString>();
	}
}
