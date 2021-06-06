using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x0200004B RID: 75
	public class AssetResponse : IProtoBuf
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00013B15 File Offset: 0x00011D15
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x00013B1D File Offset: 0x00011D1D
		public AssetKey RequestedKey { get; set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00013B26 File Offset: 0x00011D26
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x00013B2E File Offset: 0x00011D2E
		public DatabaseResult DeprecatedDatabaseResult
		{
			get
			{
				return this._DeprecatedDatabaseResult;
			}
			set
			{
				this._DeprecatedDatabaseResult = value;
				this.HasDeprecatedDatabaseResult = true;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00013B3E File Offset: 0x00011D3E
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x00013B46 File Offset: 0x00011D46
		public ErrorCode ErrorCode { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00013B4F File Offset: 0x00011D4F
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x00013B57 File Offset: 0x00011D57
		public ScenarioDbRecord ScenarioAsset
		{
			get
			{
				return this._ScenarioAsset;
			}
			set
			{
				this._ScenarioAsset = value;
				this.HasScenarioAsset = (value != null);
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x00013B6A File Offset: 0x00011D6A
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x00013B72 File Offset: 0x00011D72
		public SubsetCardListDbRecord SubsetCardListAsset
		{
			get
			{
				return this._SubsetCardListAsset;
			}
			set
			{
				this._SubsetCardListAsset = value;
				this.HasSubsetCardListAsset = (value != null);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00013B85 File Offset: 0x00011D85
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x00013B8D File Offset: 0x00011D8D
		public DeckRulesetDbRecord DeckRulesetAsset
		{
			get
			{
				return this._DeckRulesetAsset;
			}
			set
			{
				this._DeckRulesetAsset = value;
				this.HasDeckRulesetAsset = (value != null);
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00013BA0 File Offset: 0x00011DA0
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x00013BA8 File Offset: 0x00011DA8
		public RewardChestDbRecord RewardChestAsset
		{
			get
			{
				return this._RewardChestAsset;
			}
			set
			{
				this._RewardChestAsset = value;
				this.HasRewardChestAsset = (value != null);
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00013BBB File Offset: 0x00011DBB
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x00013BC3 File Offset: 0x00011DC3
		public GuestHeroDbRecord GuestHeroAsset
		{
			get
			{
				return this._GuestHeroAsset;
			}
			set
			{
				this._GuestHeroAsset = value;
				this.HasGuestHeroAsset = (value != null);
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00013BD6 File Offset: 0x00011DD6
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x00013BDE File Offset: 0x00011DDE
		public DeckTemplateDbRecord DeckTemplateAsset
		{
			get
			{
				return this._DeckTemplateAsset;
			}
			set
			{
				this._DeckTemplateAsset = value;
				this.HasDeckTemplateAsset = (value != null);
			}
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00013BF4 File Offset: 0x00011DF4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.RequestedKey.GetHashCode();
			if (this.HasDeprecatedDatabaseResult)
			{
				num ^= this.DeprecatedDatabaseResult.GetHashCode();
			}
			num ^= this.ErrorCode.GetHashCode();
			if (this.HasScenarioAsset)
			{
				num ^= this.ScenarioAsset.GetHashCode();
			}
			if (this.HasSubsetCardListAsset)
			{
				num ^= this.SubsetCardListAsset.GetHashCode();
			}
			if (this.HasDeckRulesetAsset)
			{
				num ^= this.DeckRulesetAsset.GetHashCode();
			}
			if (this.HasRewardChestAsset)
			{
				num ^= this.RewardChestAsset.GetHashCode();
			}
			if (this.HasGuestHeroAsset)
			{
				num ^= this.GuestHeroAsset.GetHashCode();
			}
			if (this.HasDeckTemplateAsset)
			{
				num ^= this.DeckTemplateAsset.GetHashCode();
			}
			return num;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00013CD8 File Offset: 0x00011ED8
		public override bool Equals(object obj)
		{
			AssetResponse assetResponse = obj as AssetResponse;
			return assetResponse != null && this.RequestedKey.Equals(assetResponse.RequestedKey) && this.HasDeprecatedDatabaseResult == assetResponse.HasDeprecatedDatabaseResult && (!this.HasDeprecatedDatabaseResult || this.DeprecatedDatabaseResult.Equals(assetResponse.DeprecatedDatabaseResult)) && this.ErrorCode.Equals(assetResponse.ErrorCode) && this.HasScenarioAsset == assetResponse.HasScenarioAsset && (!this.HasScenarioAsset || this.ScenarioAsset.Equals(assetResponse.ScenarioAsset)) && this.HasSubsetCardListAsset == assetResponse.HasSubsetCardListAsset && (!this.HasSubsetCardListAsset || this.SubsetCardListAsset.Equals(assetResponse.SubsetCardListAsset)) && this.HasDeckRulesetAsset == assetResponse.HasDeckRulesetAsset && (!this.HasDeckRulesetAsset || this.DeckRulesetAsset.Equals(assetResponse.DeckRulesetAsset)) && this.HasRewardChestAsset == assetResponse.HasRewardChestAsset && (!this.HasRewardChestAsset || this.RewardChestAsset.Equals(assetResponse.RewardChestAsset)) && this.HasGuestHeroAsset == assetResponse.HasGuestHeroAsset && (!this.HasGuestHeroAsset || this.GuestHeroAsset.Equals(assetResponse.GuestHeroAsset)) && this.HasDeckTemplateAsset == assetResponse.HasDeckTemplateAsset && (!this.HasDeckTemplateAsset || this.DeckTemplateAsset.Equals(assetResponse.DeckTemplateAsset));
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00013E65 File Offset: 0x00012065
		public void Deserialize(Stream stream)
		{
			AssetResponse.Deserialize(stream, this);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00013E6F File Offset: 0x0001206F
		public static AssetResponse Deserialize(Stream stream, AssetResponse instance)
		{
			return AssetResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00013E7C File Offset: 0x0001207C
		public static AssetResponse DeserializeLengthDelimited(Stream stream)
		{
			AssetResponse assetResponse = new AssetResponse();
			AssetResponse.DeserializeLengthDelimited(stream, assetResponse);
			return assetResponse;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00013E98 File Offset: 0x00012098
		public static AssetResponse DeserializeLengthDelimited(Stream stream, AssetResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AssetResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00013EC0 File Offset: 0x000120C0
		public static AssetResponse Deserialize(Stream stream, AssetResponse instance, long limit)
		{
			instance.DeprecatedDatabaseResult = DatabaseResult.DB_E_SQL_EX;
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
						if (num != 24)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							uint field = key.Field;
							if (field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							switch (field)
							{
							case 100U:
								if (key.WireType == Wire.LengthDelimited)
								{
									if (instance.ScenarioAsset == null)
									{
										instance.ScenarioAsset = ScenarioDbRecord.DeserializeLengthDelimited(stream);
									}
									else
									{
										ScenarioDbRecord.DeserializeLengthDelimited(stream, instance.ScenarioAsset);
									}
								}
								break;
							case 101U:
								if (key.WireType == Wire.LengthDelimited)
								{
									if (instance.SubsetCardListAsset == null)
									{
										instance.SubsetCardListAsset = SubsetCardListDbRecord.DeserializeLengthDelimited(stream);
									}
									else
									{
										SubsetCardListDbRecord.DeserializeLengthDelimited(stream, instance.SubsetCardListAsset);
									}
								}
								break;
							case 102U:
								if (key.WireType == Wire.LengthDelimited)
								{
									if (instance.DeckRulesetAsset == null)
									{
										instance.DeckRulesetAsset = DeckRulesetDbRecord.DeserializeLengthDelimited(stream);
									}
									else
									{
										DeckRulesetDbRecord.DeserializeLengthDelimited(stream, instance.DeckRulesetAsset);
									}
								}
								break;
							case 103U:
								if (key.WireType == Wire.LengthDelimited)
								{
									if (instance.RewardChestAsset == null)
									{
										instance.RewardChestAsset = RewardChestDbRecord.DeserializeLengthDelimited(stream);
									}
									else
									{
										RewardChestDbRecord.DeserializeLengthDelimited(stream, instance.RewardChestAsset);
									}
								}
								break;
							case 104U:
								if (key.WireType == Wire.LengthDelimited)
								{
									if (instance.GuestHeroAsset == null)
									{
										instance.GuestHeroAsset = GuestHeroDbRecord.DeserializeLengthDelimited(stream);
									}
									else
									{
										GuestHeroDbRecord.DeserializeLengthDelimited(stream, instance.GuestHeroAsset);
									}
								}
								break;
							case 105U:
								if (key.WireType == Wire.LengthDelimited)
								{
									if (instance.DeckTemplateAsset == null)
									{
										instance.DeckTemplateAsset = DeckTemplateDbRecord.DeserializeLengthDelimited(stream);
									}
									else
									{
										DeckTemplateDbRecord.DeserializeLengthDelimited(stream, instance.DeckTemplateAsset);
									}
								}
								break;
							default:
								ProtocolParser.SkipKey(stream, key);
								break;
							}
						}
						else
						{
							instance.ErrorCode = (ErrorCode)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.DeprecatedDatabaseResult = (DatabaseResult)ProtocolParser.ReadUInt64(stream);
					}
				}
				else if (instance.RequestedKey == null)
				{
					instance.RequestedKey = AssetKey.DeserializeLengthDelimited(stream);
				}
				else
				{
					AssetKey.DeserializeLengthDelimited(stream, instance.RequestedKey);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00014107 File Offset: 0x00012307
		public void Serialize(Stream stream)
		{
			AssetResponse.Serialize(stream, this);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00014110 File Offset: 0x00012310
		public static void Serialize(Stream stream, AssetResponse instance)
		{
			if (instance.RequestedKey == null)
			{
				throw new ArgumentNullException("RequestedKey", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.RequestedKey.GetSerializedSize());
			AssetKey.Serialize(stream, instance.RequestedKey);
			if (instance.HasDeprecatedDatabaseResult)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DeprecatedDatabaseResult));
			}
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ErrorCode));
			if (instance.HasScenarioAsset)
			{
				stream.WriteByte(162);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.ScenarioAsset.GetSerializedSize());
				ScenarioDbRecord.Serialize(stream, instance.ScenarioAsset);
			}
			if (instance.HasSubsetCardListAsset)
			{
				stream.WriteByte(170);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.SubsetCardListAsset.GetSerializedSize());
				SubsetCardListDbRecord.Serialize(stream, instance.SubsetCardListAsset);
			}
			if (instance.HasDeckRulesetAsset)
			{
				stream.WriteByte(178);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.DeckRulesetAsset.GetSerializedSize());
				DeckRulesetDbRecord.Serialize(stream, instance.DeckRulesetAsset);
			}
			if (instance.HasRewardChestAsset)
			{
				stream.WriteByte(186);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.RewardChestAsset.GetSerializedSize());
				RewardChestDbRecord.Serialize(stream, instance.RewardChestAsset);
			}
			if (instance.HasGuestHeroAsset)
			{
				stream.WriteByte(194);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.GuestHeroAsset.GetSerializedSize());
				GuestHeroDbRecord.Serialize(stream, instance.GuestHeroAsset);
			}
			if (instance.HasDeckTemplateAsset)
			{
				stream.WriteByte(202);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.DeckTemplateAsset.GetSerializedSize());
				DeckTemplateDbRecord.Serialize(stream, instance.DeckTemplateAsset);
			}
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x000142D8 File Offset: 0x000124D8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.RequestedKey.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasDeprecatedDatabaseResult)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DeprecatedDatabaseResult));
			}
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ErrorCode));
			if (this.HasScenarioAsset)
			{
				num += 2U;
				uint serializedSize2 = this.ScenarioAsset.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasSubsetCardListAsset)
			{
				num += 2U;
				uint serializedSize3 = this.SubsetCardListAsset.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasDeckRulesetAsset)
			{
				num += 2U;
				uint serializedSize4 = this.DeckRulesetAsset.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (this.HasRewardChestAsset)
			{
				num += 2U;
				uint serializedSize5 = this.RewardChestAsset.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			if (this.HasGuestHeroAsset)
			{
				num += 2U;
				uint serializedSize6 = this.GuestHeroAsset.GetSerializedSize();
				num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
			}
			if (this.HasDeckTemplateAsset)
			{
				num += 2U;
				uint serializedSize7 = this.DeckTemplateAsset.GetSerializedSize();
				num += serializedSize7 + ProtocolParser.SizeOfUInt32(serializedSize7);
			}
			return num + 2U;
		}

		// Token: 0x040001BD RID: 445
		public bool HasDeprecatedDatabaseResult;

		// Token: 0x040001BE RID: 446
		private DatabaseResult _DeprecatedDatabaseResult;

		// Token: 0x040001C0 RID: 448
		public bool HasScenarioAsset;

		// Token: 0x040001C1 RID: 449
		private ScenarioDbRecord _ScenarioAsset;

		// Token: 0x040001C2 RID: 450
		public bool HasSubsetCardListAsset;

		// Token: 0x040001C3 RID: 451
		private SubsetCardListDbRecord _SubsetCardListAsset;

		// Token: 0x040001C4 RID: 452
		public bool HasDeckRulesetAsset;

		// Token: 0x040001C5 RID: 453
		private DeckRulesetDbRecord _DeckRulesetAsset;

		// Token: 0x040001C6 RID: 454
		public bool HasRewardChestAsset;

		// Token: 0x040001C7 RID: 455
		private RewardChestDbRecord _RewardChestAsset;

		// Token: 0x040001C8 RID: 456
		public bool HasGuestHeroAsset;

		// Token: 0x040001C9 RID: 457
		private GuestHeroDbRecord _GuestHeroAsset;

		// Token: 0x040001CA RID: 458
		public bool HasDeckTemplateAsset;

		// Token: 0x040001CB RID: 459
		private DeckTemplateDbRecord _DeckTemplateAsset;
	}
}
