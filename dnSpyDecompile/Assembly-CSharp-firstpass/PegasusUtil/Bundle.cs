using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x02000047 RID: 71
	public class Bundle : IProtoBuf
	{
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00011CC6 File Offset: 0x0000FEC6
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x00011CCE File Offset: 0x0000FECE
		public string Id { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00011CD7 File Offset: 0x0000FED7
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x00011CDF File Offset: 0x0000FEDF
		public double DeprecatedCost
		{
			get
			{
				return this._DeprecatedCost;
			}
			set
			{
				this._DeprecatedCost = value;
				this.HasDeprecatedCost = true;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x00011CEF File Offset: 0x0000FEEF
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x00011CF7 File Offset: 0x0000FEF7
		public string AppleId
		{
			get
			{
				return this._AppleId;
			}
			set
			{
				this._AppleId = value;
				this.HasAppleId = (value != null);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00011D0A File Offset: 0x0000FF0A
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x00011D12 File Offset: 0x0000FF12
		public string GooglePlayId
		{
			get
			{
				return this._GooglePlayId;
			}
			set
			{
				this._GooglePlayId = value;
				this.HasGooglePlayId = (value != null);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00011D25 File Offset: 0x0000FF25
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x00011D2D File Offset: 0x0000FF2D
		public List<BundleItem> Items
		{
			get
			{
				return this._Items;
			}
			set
			{
				this._Items = value;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00011D36 File Offset: 0x0000FF36
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x00011D3E File Offset: 0x0000FF3E
		public long GoldCost
		{
			get
			{
				return this._GoldCost;
			}
			set
			{
				this._GoldCost = value;
				this.HasGoldCost = true;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00011D4E File Offset: 0x0000FF4E
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x00011D56 File Offset: 0x0000FF56
		public string AmazonId
		{
			get
			{
				return this._AmazonId;
			}
			set
			{
				this._AmazonId = value;
				this.HasAmazonId = (value != null);
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x00011D69 File Offset: 0x0000FF69
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x00011D71 File Offset: 0x0000FF71
		public string ProductEventName
		{
			get
			{
				return this._ProductEventName;
			}
			set
			{
				this._ProductEventName = value;
				this.HasProductEventName = (value != null);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00011D84 File Offset: 0x0000FF84
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x00011D8C File Offset: 0x0000FF8C
		public List<BattlePayProvider> ExclusiveProviders
		{
			get
			{
				return this._ExclusiveProviders;
			}
			set
			{
				this._ExclusiveProviders = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00011D95 File Offset: 0x0000FF95
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x00011D9D File Offset: 0x0000FF9D
		public string RealMoneyProductEventName
		{
			get
			{
				return this._RealMoneyProductEventName;
			}
			set
			{
				this._RealMoneyProductEventName = value;
				this.HasRealMoneyProductEventName = (value != null);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00011DB0 File Offset: 0x0000FFB0
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x00011DB8 File Offset: 0x0000FFB8
		public ulong Cost
		{
			get
			{
				return this._Cost;
			}
			set
			{
				this._Cost = value;
				this.HasCost = true;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00011DC8 File Offset: 0x0000FFC8
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x00011DD0 File Offset: 0x0000FFD0
		public bool IsPrePurchase
		{
			get
			{
				return this._IsPrePurchase;
			}
			set
			{
				this._IsPrePurchase = value;
				this.HasIsPrePurchase = true;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00011DE0 File Offset: 0x0000FFE0
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x00011DE8 File Offset: 0x0000FFE8
		public long PmtProductId
		{
			get
			{
				return this._PmtProductId;
			}
			set
			{
				this._PmtProductId = value;
				this.HasPmtProductId = true;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00011DF8 File Offset: 0x0000FFF8
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x00011E00 File Offset: 0x00010000
		public LocalizedString DisplayName
		{
			get
			{
				return this._DisplayName;
			}
			set
			{
				this._DisplayName = value;
				this.HasDisplayName = (value != null);
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00011E13 File Offset: 0x00010013
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x00011E1B File Offset: 0x0001001B
		public LocalizedString DisplayDesc
		{
			get
			{
				return this._DisplayDesc;
			}
			set
			{
				this._DisplayDesc = value;
				this.HasDisplayDesc = (value != null);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00011E2E File Offset: 0x0001002E
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x00011E36 File Offset: 0x00010036
		public List<ProductAttribute> Attributes
		{
			get
			{
				return this._Attributes;
			}
			set
			{
				this._Attributes = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x00011E3F File Offset: 0x0001003F
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x00011E47 File Offset: 0x00010047
		public bool VisibleOnSalePeriodOnly
		{
			get
			{
				return this._VisibleOnSalePeriodOnly;
			}
			set
			{
				this._VisibleOnSalePeriodOnly = value;
				this.HasVisibleOnSalePeriodOnly = true;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x00011E57 File Offset: 0x00010057
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x00011E5F File Offset: 0x0001005F
		public List<int> SaleIds
		{
			get
			{
				return this._SaleIds;
			}
			set
			{
				this._SaleIds = value;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x00011E68 File Offset: 0x00010068
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x00011E70 File Offset: 0x00010070
		public VirtualCurrencyCost VirtualCurrencyCost
		{
			get
			{
				return this._VirtualCurrencyCost;
			}
			set
			{
				this._VirtualCurrencyCost = value;
				this.HasVirtualCurrencyCost = (value != null);
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x00011E83 File Offset: 0x00010083
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x00011E8B File Offset: 0x0001008B
		public string KronestoreId
		{
			get
			{
				return this._KronestoreId;
			}
			set
			{
				this._KronestoreId = value;
				this.HasKronestoreId = (value != null);
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00011EA0 File Offset: 0x000100A0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			if (this.HasDeprecatedCost)
			{
				num ^= this.DeprecatedCost.GetHashCode();
			}
			if (this.HasAppleId)
			{
				num ^= this.AppleId.GetHashCode();
			}
			if (this.HasGooglePlayId)
			{
				num ^= this.GooglePlayId.GetHashCode();
			}
			foreach (BundleItem bundleItem in this.Items)
			{
				num ^= bundleItem.GetHashCode();
			}
			if (this.HasGoldCost)
			{
				num ^= this.GoldCost.GetHashCode();
			}
			if (this.HasAmazonId)
			{
				num ^= this.AmazonId.GetHashCode();
			}
			if (this.HasProductEventName)
			{
				num ^= this.ProductEventName.GetHashCode();
			}
			foreach (BattlePayProvider battlePayProvider in this.ExclusiveProviders)
			{
				num ^= battlePayProvider.GetHashCode();
			}
			if (this.HasRealMoneyProductEventName)
			{
				num ^= this.RealMoneyProductEventName.GetHashCode();
			}
			if (this.HasCost)
			{
				num ^= this.Cost.GetHashCode();
			}
			if (this.HasIsPrePurchase)
			{
				num ^= this.IsPrePurchase.GetHashCode();
			}
			if (this.HasPmtProductId)
			{
				num ^= this.PmtProductId.GetHashCode();
			}
			if (this.HasDisplayName)
			{
				num ^= this.DisplayName.GetHashCode();
			}
			if (this.HasDisplayDesc)
			{
				num ^= this.DisplayDesc.GetHashCode();
			}
			foreach (ProductAttribute productAttribute in this.Attributes)
			{
				num ^= productAttribute.GetHashCode();
			}
			if (this.HasVisibleOnSalePeriodOnly)
			{
				num ^= this.VisibleOnSalePeriodOnly.GetHashCode();
			}
			foreach (int num2 in this.SaleIds)
			{
				num ^= num2.GetHashCode();
			}
			if (this.HasVirtualCurrencyCost)
			{
				num ^= this.VirtualCurrencyCost.GetHashCode();
			}
			if (this.HasKronestoreId)
			{
				num ^= this.KronestoreId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0001214C File Offset: 0x0001034C
		public override bool Equals(object obj)
		{
			Bundle bundle = obj as Bundle;
			if (bundle == null)
			{
				return false;
			}
			if (!this.Id.Equals(bundle.Id))
			{
				return false;
			}
			if (this.HasDeprecatedCost != bundle.HasDeprecatedCost || (this.HasDeprecatedCost && !this.DeprecatedCost.Equals(bundle.DeprecatedCost)))
			{
				return false;
			}
			if (this.HasAppleId != bundle.HasAppleId || (this.HasAppleId && !this.AppleId.Equals(bundle.AppleId)))
			{
				return false;
			}
			if (this.HasGooglePlayId != bundle.HasGooglePlayId || (this.HasGooglePlayId && !this.GooglePlayId.Equals(bundle.GooglePlayId)))
			{
				return false;
			}
			if (this.Items.Count != bundle.Items.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Items.Count; i++)
			{
				if (!this.Items[i].Equals(bundle.Items[i]))
				{
					return false;
				}
			}
			if (this.HasGoldCost != bundle.HasGoldCost || (this.HasGoldCost && !this.GoldCost.Equals(bundle.GoldCost)))
			{
				return false;
			}
			if (this.HasAmazonId != bundle.HasAmazonId || (this.HasAmazonId && !this.AmazonId.Equals(bundle.AmazonId)))
			{
				return false;
			}
			if (this.HasProductEventName != bundle.HasProductEventName || (this.HasProductEventName && !this.ProductEventName.Equals(bundle.ProductEventName)))
			{
				return false;
			}
			if (this.ExclusiveProviders.Count != bundle.ExclusiveProviders.Count)
			{
				return false;
			}
			for (int j = 0; j < this.ExclusiveProviders.Count; j++)
			{
				if (!this.ExclusiveProviders[j].Equals(bundle.ExclusiveProviders[j]))
				{
					return false;
				}
			}
			if (this.HasRealMoneyProductEventName != bundle.HasRealMoneyProductEventName || (this.HasRealMoneyProductEventName && !this.RealMoneyProductEventName.Equals(bundle.RealMoneyProductEventName)))
			{
				return false;
			}
			if (this.HasCost != bundle.HasCost || (this.HasCost && !this.Cost.Equals(bundle.Cost)))
			{
				return false;
			}
			if (this.HasIsPrePurchase != bundle.HasIsPrePurchase || (this.HasIsPrePurchase && !this.IsPrePurchase.Equals(bundle.IsPrePurchase)))
			{
				return false;
			}
			if (this.HasPmtProductId != bundle.HasPmtProductId || (this.HasPmtProductId && !this.PmtProductId.Equals(bundle.PmtProductId)))
			{
				return false;
			}
			if (this.HasDisplayName != bundle.HasDisplayName || (this.HasDisplayName && !this.DisplayName.Equals(bundle.DisplayName)))
			{
				return false;
			}
			if (this.HasDisplayDesc != bundle.HasDisplayDesc || (this.HasDisplayDesc && !this.DisplayDesc.Equals(bundle.DisplayDesc)))
			{
				return false;
			}
			if (this.Attributes.Count != bundle.Attributes.Count)
			{
				return false;
			}
			for (int k = 0; k < this.Attributes.Count; k++)
			{
				if (!this.Attributes[k].Equals(bundle.Attributes[k]))
				{
					return false;
				}
			}
			if (this.HasVisibleOnSalePeriodOnly != bundle.HasVisibleOnSalePeriodOnly || (this.HasVisibleOnSalePeriodOnly && !this.VisibleOnSalePeriodOnly.Equals(bundle.VisibleOnSalePeriodOnly)))
			{
				return false;
			}
			if (this.SaleIds.Count != bundle.SaleIds.Count)
			{
				return false;
			}
			for (int l = 0; l < this.SaleIds.Count; l++)
			{
				if (!this.SaleIds[l].Equals(bundle.SaleIds[l]))
				{
					return false;
				}
			}
			return this.HasVirtualCurrencyCost == bundle.HasVirtualCurrencyCost && (!this.HasVirtualCurrencyCost || this.VirtualCurrencyCost.Equals(bundle.VirtualCurrencyCost)) && this.HasKronestoreId == bundle.HasKronestoreId && (!this.HasKronestoreId || this.KronestoreId.Equals(bundle.KronestoreId));
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0001257E File Offset: 0x0001077E
		public void Deserialize(Stream stream)
		{
			Bundle.Deserialize(stream, this);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00012588 File Offset: 0x00010788
		public static Bundle Deserialize(Stream stream, Bundle instance)
		{
			return Bundle.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00012594 File Offset: 0x00010794
		public static Bundle DeserializeLengthDelimited(Stream stream)
		{
			Bundle bundle = new Bundle();
			Bundle.DeserializeLengthDelimited(stream, bundle);
			return bundle;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x000125B0 File Offset: 0x000107B0
		public static Bundle DeserializeLengthDelimited(Stream stream, Bundle instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Bundle.Deserialize(stream, instance, num);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x000125D8 File Offset: 0x000107D8
		public static Bundle Deserialize(Stream stream, Bundle instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Items == null)
			{
				instance.Items = new List<BundleItem>();
			}
			if (instance.ExclusiveProviders == null)
			{
				instance.ExclusiveProviders = new List<BattlePayProvider>();
			}
			if (instance.Attributes == null)
			{
				instance.Attributes = new List<ProductAttribute>();
			}
			if (instance.SaleIds == null)
			{
				instance.SaleIds = new List<int>();
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
					if (num <= 58)
					{
						if (num <= 26)
						{
							if (num == 10)
							{
								instance.Id = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 17)
							{
								instance.DeprecatedCost = binaryReader.ReadDouble();
								continue;
							}
							if (num == 26)
							{
								instance.AppleId = ProtocolParser.ReadString(stream);
								continue;
							}
						}
						else if (num <= 42)
						{
							if (num == 34)
							{
								instance.GooglePlayId = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 42)
							{
								instance.Items.Add(BundleItem.DeserializeLengthDelimited(stream));
								continue;
							}
						}
						else
						{
							if (num == 48)
							{
								instance.GoldCost = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 58)
							{
								instance.AmazonId = ProtocolParser.ReadString(stream);
								continue;
							}
						}
					}
					else if (num <= 90)
					{
						if (num == 74)
						{
							instance.ProductEventName = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 80)
						{
							instance.ExclusiveProviders.Add((BattlePayProvider)ProtocolParser.ReadUInt64(stream));
							continue;
						}
						if (num == 90)
						{
							instance.RealMoneyProductEventName = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else if (num <= 104)
					{
						if (num == 96)
						{
							instance.Cost = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 104)
						{
							instance.IsPrePurchase = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					else
					{
						if (num == 112)
						{
							instance.PmtProductId = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 122)
						{
							if (instance.DisplayName == null)
							{
								instance.DisplayName = LocalizedString.DeserializeLengthDelimited(stream);
								continue;
							}
							LocalizedString.DeserializeLengthDelimited(stream, instance.DisplayName);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					switch (field)
					{
					case 16U:
						if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.DisplayDesc == null)
							{
								instance.DisplayDesc = LocalizedString.DeserializeLengthDelimited(stream);
							}
							else
							{
								LocalizedString.DeserializeLengthDelimited(stream, instance.DisplayDesc);
							}
						}
						break;
					case 17U:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.Attributes.Add(ProductAttribute.DeserializeLengthDelimited(stream));
						}
						break;
					case 18U:
						if (key.WireType == Wire.Varint)
						{
							instance.VisibleOnSalePeriodOnly = ProtocolParser.ReadBool(stream);
						}
						break;
					case 19U:
						if (key.WireType == Wire.Varint)
						{
							instance.SaleIds.Add((int)ProtocolParser.ReadUInt64(stream));
						}
						break;
					case 20U:
						if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.VirtualCurrencyCost == null)
							{
								instance.VirtualCurrencyCost = VirtualCurrencyCost.DeserializeLengthDelimited(stream);
							}
							else
							{
								VirtualCurrencyCost.DeserializeLengthDelimited(stream, instance.VirtualCurrencyCost);
							}
						}
						break;
					case 21U:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.KronestoreId = ProtocolParser.ReadString(stream);
						}
						break;
					default:
						ProtocolParser.SkipKey(stream, key);
						break;
					}
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0001296C File Offset: 0x00010B6C
		public void Serialize(Stream stream)
		{
			Bundle.Serialize(stream, this);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00012978 File Offset: 0x00010B78
		public static void Serialize(Stream stream, Bundle instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.Id == null)
			{
				throw new ArgumentNullException("Id", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Id));
			if (instance.HasDeprecatedCost)
			{
				stream.WriteByte(17);
				binaryWriter.Write(instance.DeprecatedCost);
			}
			if (instance.HasAppleId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AppleId));
			}
			if (instance.HasGooglePlayId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.GooglePlayId));
			}
			if (instance.Items.Count > 0)
			{
				foreach (BundleItem bundleItem in instance.Items)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, bundleItem.GetSerializedSize());
					BundleItem.Serialize(stream, bundleItem);
				}
			}
			if (instance.HasGoldCost)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GoldCost);
			}
			if (instance.HasAmazonId)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AmazonId));
			}
			if (instance.HasProductEventName)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ProductEventName));
			}
			if (instance.ExclusiveProviders.Count > 0)
			{
				foreach (BattlePayProvider battlePayProvider in instance.ExclusiveProviders)
				{
					stream.WriteByte(80);
					ProtocolParser.WriteUInt64(stream, (ulong)((long)battlePayProvider));
				}
			}
			if (instance.HasRealMoneyProductEventName)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.RealMoneyProductEventName));
			}
			if (instance.HasCost)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt64(stream, instance.Cost);
			}
			if (instance.HasIsPrePurchase)
			{
				stream.WriteByte(104);
				ProtocolParser.WriteBool(stream, instance.IsPrePurchase);
			}
			if (instance.HasPmtProductId)
			{
				stream.WriteByte(112);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PmtProductId);
			}
			if (instance.HasDisplayName)
			{
				stream.WriteByte(122);
				ProtocolParser.WriteUInt32(stream, instance.DisplayName.GetSerializedSize());
				LocalizedString.Serialize(stream, instance.DisplayName);
			}
			if (instance.HasDisplayDesc)
			{
				stream.WriteByte(130);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.DisplayDesc.GetSerializedSize());
				LocalizedString.Serialize(stream, instance.DisplayDesc);
			}
			if (instance.Attributes.Count > 0)
			{
				foreach (ProductAttribute productAttribute in instance.Attributes)
				{
					stream.WriteByte(138);
					stream.WriteByte(1);
					ProtocolParser.WriteUInt32(stream, productAttribute.GetSerializedSize());
					ProductAttribute.Serialize(stream, productAttribute);
				}
			}
			if (instance.HasVisibleOnSalePeriodOnly)
			{
				stream.WriteByte(144);
				stream.WriteByte(1);
				ProtocolParser.WriteBool(stream, instance.VisibleOnSalePeriodOnly);
			}
			if (instance.SaleIds.Count > 0)
			{
				foreach (int num in instance.SaleIds)
				{
					stream.WriteByte(152);
					stream.WriteByte(1);
					ProtocolParser.WriteUInt64(stream, (ulong)((long)num));
				}
			}
			if (instance.HasVirtualCurrencyCost)
			{
				stream.WriteByte(162);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.VirtualCurrencyCost.GetSerializedSize());
				VirtualCurrencyCost.Serialize(stream, instance.VirtualCurrencyCost);
			}
			if (instance.HasKronestoreId)
			{
				stream.WriteByte(170);
				stream.WriteByte(1);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.KronestoreId));
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00012D98 File Offset: 0x00010F98
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Id);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (this.HasDeprecatedCost)
			{
				num += 1U;
				num += 8U;
			}
			if (this.HasAppleId)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.AppleId);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasGooglePlayId)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.GooglePlayId);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.Items.Count > 0)
			{
				foreach (BundleItem bundleItem in this.Items)
				{
					num += 1U;
					uint serializedSize = bundleItem.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasGoldCost)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.GoldCost);
			}
			if (this.HasAmazonId)
			{
				num += 1U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.AmazonId);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (this.HasProductEventName)
			{
				num += 1U;
				uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(this.ProductEventName);
				num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			}
			if (this.ExclusiveProviders.Count > 0)
			{
				foreach (BattlePayProvider battlePayProvider in this.ExclusiveProviders)
				{
					num += 1U;
					num += ProtocolParser.SizeOfUInt64((ulong)((long)battlePayProvider));
				}
			}
			if (this.HasRealMoneyProductEventName)
			{
				num += 1U;
				uint byteCount6 = (uint)Encoding.UTF8.GetByteCount(this.RealMoneyProductEventName);
				num += ProtocolParser.SizeOfUInt32(byteCount6) + byteCount6;
			}
			if (this.HasCost)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Cost);
			}
			if (this.HasIsPrePurchase)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasPmtProductId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.PmtProductId);
			}
			if (this.HasDisplayName)
			{
				num += 1U;
				uint serializedSize2 = this.DisplayName.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasDisplayDesc)
			{
				num += 2U;
				uint serializedSize3 = this.DisplayDesc.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.Attributes.Count > 0)
			{
				foreach (ProductAttribute productAttribute in this.Attributes)
				{
					num += 2U;
					uint serializedSize4 = productAttribute.GetSerializedSize();
					num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
				}
			}
			if (this.HasVisibleOnSalePeriodOnly)
			{
				num += 2U;
				num += 1U;
			}
			if (this.SaleIds.Count > 0)
			{
				foreach (int num2 in this.SaleIds)
				{
					num += 2U;
					num += ProtocolParser.SizeOfUInt64((ulong)((long)num2));
				}
			}
			if (this.HasVirtualCurrencyCost)
			{
				num += 2U;
				uint serializedSize5 = this.VirtualCurrencyCost.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			if (this.HasKronestoreId)
			{
				num += 2U;
				uint byteCount7 = (uint)Encoding.UTF8.GetByteCount(this.KronestoreId);
				num += ProtocolParser.SizeOfUInt32(byteCount7) + byteCount7;
			}
			num += 1U;
			return num;
		}

		// Token: 0x0400018B RID: 395
		public bool HasDeprecatedCost;

		// Token: 0x0400018C RID: 396
		private double _DeprecatedCost;

		// Token: 0x0400018D RID: 397
		public bool HasAppleId;

		// Token: 0x0400018E RID: 398
		private string _AppleId;

		// Token: 0x0400018F RID: 399
		public bool HasGooglePlayId;

		// Token: 0x04000190 RID: 400
		private string _GooglePlayId;

		// Token: 0x04000191 RID: 401
		private List<BundleItem> _Items = new List<BundleItem>();

		// Token: 0x04000192 RID: 402
		public bool HasGoldCost;

		// Token: 0x04000193 RID: 403
		private long _GoldCost;

		// Token: 0x04000194 RID: 404
		public bool HasAmazonId;

		// Token: 0x04000195 RID: 405
		private string _AmazonId;

		// Token: 0x04000196 RID: 406
		public bool HasProductEventName;

		// Token: 0x04000197 RID: 407
		private string _ProductEventName;

		// Token: 0x04000198 RID: 408
		private List<BattlePayProvider> _ExclusiveProviders = new List<BattlePayProvider>();

		// Token: 0x04000199 RID: 409
		public bool HasRealMoneyProductEventName;

		// Token: 0x0400019A RID: 410
		private string _RealMoneyProductEventName;

		// Token: 0x0400019B RID: 411
		public bool HasCost;

		// Token: 0x0400019C RID: 412
		private ulong _Cost;

		// Token: 0x0400019D RID: 413
		public bool HasIsPrePurchase;

		// Token: 0x0400019E RID: 414
		private bool _IsPrePurchase;

		// Token: 0x0400019F RID: 415
		public bool HasPmtProductId;

		// Token: 0x040001A0 RID: 416
		private long _PmtProductId;

		// Token: 0x040001A1 RID: 417
		public bool HasDisplayName;

		// Token: 0x040001A2 RID: 418
		private LocalizedString _DisplayName;

		// Token: 0x040001A3 RID: 419
		public bool HasDisplayDesc;

		// Token: 0x040001A4 RID: 420
		private LocalizedString _DisplayDesc;

		// Token: 0x040001A5 RID: 421
		private List<ProductAttribute> _Attributes = new List<ProductAttribute>();

		// Token: 0x040001A6 RID: 422
		public bool HasVisibleOnSalePeriodOnly;

		// Token: 0x040001A7 RID: 423
		private bool _VisibleOnSalePeriodOnly;

		// Token: 0x040001A8 RID: 424
		private List<int> _SaleIds = new List<int>();

		// Token: 0x040001A9 RID: 425
		public bool HasVirtualCurrencyCost;

		// Token: 0x040001AA RID: 426
		private VirtualCurrencyCost _VirtualCurrencyCost;

		// Token: 0x040001AB RID: 427
		public bool HasKronestoreId;

		// Token: 0x040001AC RID: 428
		private string _KronestoreId;
	}
}
