using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000AD RID: 173
	public class BattlePayConfigResponse : IProtoBuf
	{
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0002C8C6 File Offset: 0x0002AAC6
		// (set) Token: 0x06000BE4 RID: 3044 RVA: 0x0002C8CE File Offset: 0x0002AACE
		public List<Bundle> Bundles
		{
			get
			{
				return this._Bundles;
			}
			set
			{
				this._Bundles = value;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x0002C8D7 File Offset: 0x0002AAD7
		// (set) Token: 0x06000BE6 RID: 3046 RVA: 0x0002C8DF File Offset: 0x0002AADF
		public int CurrencyDeprecated
		{
			get
			{
				return this._CurrencyDeprecated;
			}
			set
			{
				this._CurrencyDeprecated = value;
				this.HasCurrencyDeprecated = true;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x0002C8EF File Offset: 0x0002AAEF
		// (set) Token: 0x06000BE8 RID: 3048 RVA: 0x0002C8F7 File Offset: 0x0002AAF7
		public bool Unavailable
		{
			get
			{
				return this._Unavailable;
			}
			set
			{
				this._Unavailable = value;
				this.HasUnavailable = true;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x0002C907 File Offset: 0x0002AB07
		// (set) Token: 0x06000BEA RID: 3050 RVA: 0x0002C90F File Offset: 0x0002AB0F
		public int SecsBeforeAutoCancel
		{
			get
			{
				return this._SecsBeforeAutoCancel;
			}
			set
			{
				this._SecsBeforeAutoCancel = value;
				this.HasSecsBeforeAutoCancel = true;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x0002C91F File Offset: 0x0002AB1F
		// (set) Token: 0x06000BEC RID: 3052 RVA: 0x0002C927 File Offset: 0x0002AB27
		public List<GoldCostBooster> GoldCostBoosters
		{
			get
			{
				return this._GoldCostBoosters;
			}
			set
			{
				this._GoldCostBoosters = value;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x0002C930 File Offset: 0x0002AB30
		// (set) Token: 0x06000BEE RID: 3054 RVA: 0x0002C938 File Offset: 0x0002AB38
		public long GoldCostArena
		{
			get
			{
				return this._GoldCostArena;
			}
			set
			{
				this._GoldCostArena = value;
				this.HasGoldCostArena = true;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x0002C948 File Offset: 0x0002AB48
		// (set) Token: 0x06000BF0 RID: 3056 RVA: 0x0002C950 File Offset: 0x0002AB50
		public string DefaultCurrencyCode
		{
			get
			{
				return this._DefaultCurrencyCode;
			}
			set
			{
				this._DefaultCurrencyCode = value;
				this.HasDefaultCurrencyCode = (value != null);
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x0002C963 File Offset: 0x0002AB63
		// (set) Token: 0x06000BF2 RID: 3058 RVA: 0x0002C96B File Offset: 0x0002AB6B
		public List<Currency> Currencies
		{
			get
			{
				return this._Currencies;
			}
			set
			{
				this._Currencies = value;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x0002C974 File Offset: 0x0002AB74
		// (set) Token: 0x06000BF4 RID: 3060 RVA: 0x0002C97C File Offset: 0x0002AB7C
		public string CheckoutOauthClientId
		{
			get
			{
				return this._CheckoutOauthClientId;
			}
			set
			{
				this._CheckoutOauthClientId = value;
				this.HasCheckoutOauthClientId = (value != null);
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0002C98F File Offset: 0x0002AB8F
		// (set) Token: 0x06000BF6 RID: 3062 RVA: 0x0002C997 File Offset: 0x0002AB97
		public string SaleList
		{
			get
			{
				return this._SaleList;
			}
			set
			{
				this._SaleList = value;
				this.HasSaleList = (value != null);
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0002C9AA File Offset: 0x0002ABAA
		// (set) Token: 0x06000BF8 RID: 3064 RVA: 0x0002C9B2 File Offset: 0x0002ABB2
		public long CatalogRefreshTime
		{
			get
			{
				return this._CatalogRefreshTime;
			}
			set
			{
				this._CatalogRefreshTime = value;
				this.HasCatalogRefreshTime = true;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x0002C9C2 File Offset: 0x0002ABC2
		// (set) Token: 0x06000BFA RID: 3066 RVA: 0x0002C9CA File Offset: 0x0002ABCA
		public List<LocaleMapEntry> LocaleMap
		{
			get
			{
				return this._LocaleMap;
			}
			set
			{
				this._LocaleMap = value;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x0002C9D3 File Offset: 0x0002ABD3
		// (set) Token: 0x06000BFC RID: 3068 RVA: 0x0002C9DB File Offset: 0x0002ABDB
		public bool IgnoreProductTiming
		{
			get
			{
				return this._IgnoreProductTiming;
			}
			set
			{
				this._IgnoreProductTiming = value;
				this.HasIgnoreProductTiming = true;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x0002C9EB File Offset: 0x0002ABEB
		// (set) Token: 0x06000BFE RID: 3070 RVA: 0x0002C9F3 File Offset: 0x0002ABF3
		public string PersonalizedShopPageId
		{
			get
			{
				return this._PersonalizedShopPageId;
			}
			set
			{
				this._PersonalizedShopPageId = value;
				this.HasPersonalizedShopPageId = (value != null);
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x0002CA06 File Offset: 0x0002AC06
		// (set) Token: 0x06000C00 RID: 3072 RVA: 0x0002CA0E File Offset: 0x0002AC0E
		public string CheckoutKrOnestoreKey
		{
			get
			{
				return this._CheckoutKrOnestoreKey;
			}
			set
			{
				this._CheckoutKrOnestoreKey = value;
				this.HasCheckoutKrOnestoreKey = (value != null);
			}
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0002CA24 File Offset: 0x0002AC24
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (Bundle bundle in this.Bundles)
			{
				num ^= bundle.GetHashCode();
			}
			if (this.HasCurrencyDeprecated)
			{
				num ^= this.CurrencyDeprecated.GetHashCode();
			}
			if (this.HasUnavailable)
			{
				num ^= this.Unavailable.GetHashCode();
			}
			if (this.HasSecsBeforeAutoCancel)
			{
				num ^= this.SecsBeforeAutoCancel.GetHashCode();
			}
			foreach (GoldCostBooster goldCostBooster in this.GoldCostBoosters)
			{
				num ^= goldCostBooster.GetHashCode();
			}
			if (this.HasGoldCostArena)
			{
				num ^= this.GoldCostArena.GetHashCode();
			}
			if (this.HasDefaultCurrencyCode)
			{
				num ^= this.DefaultCurrencyCode.GetHashCode();
			}
			foreach (Currency currency in this.Currencies)
			{
				num ^= currency.GetHashCode();
			}
			if (this.HasCheckoutOauthClientId)
			{
				num ^= this.CheckoutOauthClientId.GetHashCode();
			}
			if (this.HasSaleList)
			{
				num ^= this.SaleList.GetHashCode();
			}
			if (this.HasCatalogRefreshTime)
			{
				num ^= this.CatalogRefreshTime.GetHashCode();
			}
			foreach (LocaleMapEntry localeMapEntry in this.LocaleMap)
			{
				num ^= localeMapEntry.GetHashCode();
			}
			if (this.HasIgnoreProductTiming)
			{
				num ^= this.IgnoreProductTiming.GetHashCode();
			}
			if (this.HasPersonalizedShopPageId)
			{
				num ^= this.PersonalizedShopPageId.GetHashCode();
			}
			if (this.HasCheckoutKrOnestoreKey)
			{
				num ^= this.CheckoutKrOnestoreKey.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0002CC64 File Offset: 0x0002AE64
		public override bool Equals(object obj)
		{
			BattlePayConfigResponse battlePayConfigResponse = obj as BattlePayConfigResponse;
			if (battlePayConfigResponse == null)
			{
				return false;
			}
			if (this.Bundles.Count != battlePayConfigResponse.Bundles.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Bundles.Count; i++)
			{
				if (!this.Bundles[i].Equals(battlePayConfigResponse.Bundles[i]))
				{
					return false;
				}
			}
			if (this.HasCurrencyDeprecated != battlePayConfigResponse.HasCurrencyDeprecated || (this.HasCurrencyDeprecated && !this.CurrencyDeprecated.Equals(battlePayConfigResponse.CurrencyDeprecated)))
			{
				return false;
			}
			if (this.HasUnavailable != battlePayConfigResponse.HasUnavailable || (this.HasUnavailable && !this.Unavailable.Equals(battlePayConfigResponse.Unavailable)))
			{
				return false;
			}
			if (this.HasSecsBeforeAutoCancel != battlePayConfigResponse.HasSecsBeforeAutoCancel || (this.HasSecsBeforeAutoCancel && !this.SecsBeforeAutoCancel.Equals(battlePayConfigResponse.SecsBeforeAutoCancel)))
			{
				return false;
			}
			if (this.GoldCostBoosters.Count != battlePayConfigResponse.GoldCostBoosters.Count)
			{
				return false;
			}
			for (int j = 0; j < this.GoldCostBoosters.Count; j++)
			{
				if (!this.GoldCostBoosters[j].Equals(battlePayConfigResponse.GoldCostBoosters[j]))
				{
					return false;
				}
			}
			if (this.HasGoldCostArena != battlePayConfigResponse.HasGoldCostArena || (this.HasGoldCostArena && !this.GoldCostArena.Equals(battlePayConfigResponse.GoldCostArena)))
			{
				return false;
			}
			if (this.HasDefaultCurrencyCode != battlePayConfigResponse.HasDefaultCurrencyCode || (this.HasDefaultCurrencyCode && !this.DefaultCurrencyCode.Equals(battlePayConfigResponse.DefaultCurrencyCode)))
			{
				return false;
			}
			if (this.Currencies.Count != battlePayConfigResponse.Currencies.Count)
			{
				return false;
			}
			for (int k = 0; k < this.Currencies.Count; k++)
			{
				if (!this.Currencies[k].Equals(battlePayConfigResponse.Currencies[k]))
				{
					return false;
				}
			}
			if (this.HasCheckoutOauthClientId != battlePayConfigResponse.HasCheckoutOauthClientId || (this.HasCheckoutOauthClientId && !this.CheckoutOauthClientId.Equals(battlePayConfigResponse.CheckoutOauthClientId)))
			{
				return false;
			}
			if (this.HasSaleList != battlePayConfigResponse.HasSaleList || (this.HasSaleList && !this.SaleList.Equals(battlePayConfigResponse.SaleList)))
			{
				return false;
			}
			if (this.HasCatalogRefreshTime != battlePayConfigResponse.HasCatalogRefreshTime || (this.HasCatalogRefreshTime && !this.CatalogRefreshTime.Equals(battlePayConfigResponse.CatalogRefreshTime)))
			{
				return false;
			}
			if (this.LocaleMap.Count != battlePayConfigResponse.LocaleMap.Count)
			{
				return false;
			}
			for (int l = 0; l < this.LocaleMap.Count; l++)
			{
				if (!this.LocaleMap[l].Equals(battlePayConfigResponse.LocaleMap[l]))
				{
					return false;
				}
			}
			return this.HasIgnoreProductTiming == battlePayConfigResponse.HasIgnoreProductTiming && (!this.HasIgnoreProductTiming || this.IgnoreProductTiming.Equals(battlePayConfigResponse.IgnoreProductTiming)) && this.HasPersonalizedShopPageId == battlePayConfigResponse.HasPersonalizedShopPageId && (!this.HasPersonalizedShopPageId || this.PersonalizedShopPageId.Equals(battlePayConfigResponse.PersonalizedShopPageId)) && this.HasCheckoutKrOnestoreKey == battlePayConfigResponse.HasCheckoutKrOnestoreKey && (!this.HasCheckoutKrOnestoreKey || this.CheckoutKrOnestoreKey.Equals(battlePayConfigResponse.CheckoutKrOnestoreKey));
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0002CFC1 File Offset: 0x0002B1C1
		public void Deserialize(Stream stream)
		{
			BattlePayConfigResponse.Deserialize(stream, this);
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0002CFCB File Offset: 0x0002B1CB
		public static BattlePayConfigResponse Deserialize(Stream stream, BattlePayConfigResponse instance)
		{
			return BattlePayConfigResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0002CFD8 File Offset: 0x0002B1D8
		public static BattlePayConfigResponse DeserializeLengthDelimited(Stream stream)
		{
			BattlePayConfigResponse battlePayConfigResponse = new BattlePayConfigResponse();
			BattlePayConfigResponse.DeserializeLengthDelimited(stream, battlePayConfigResponse);
			return battlePayConfigResponse;
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0002CFF4 File Offset: 0x0002B1F4
		public static BattlePayConfigResponse DeserializeLengthDelimited(Stream stream, BattlePayConfigResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BattlePayConfigResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0002D01C File Offset: 0x0002B21C
		public static BattlePayConfigResponse Deserialize(Stream stream, BattlePayConfigResponse instance, long limit)
		{
			if (instance.Bundles == null)
			{
				instance.Bundles = new List<Bundle>();
			}
			if (instance.GoldCostBoosters == null)
			{
				instance.GoldCostBoosters = new List<GoldCostBooster>();
			}
			if (instance.Currencies == null)
			{
				instance.Currencies = new List<Currency>();
			}
			if (instance.LocaleMap == null)
			{
				instance.LocaleMap = new List<LocaleMapEntry>();
			}
			instance.IgnoreProductTiming = false;
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
						if (num <= 24)
						{
							if (num == 10)
							{
								instance.Bundles.Add(Bundle.DeserializeLengthDelimited(stream));
								continue;
							}
							if (num == 16)
							{
								instance.CurrencyDeprecated = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 24)
							{
								instance.Unavailable = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
						else if (num <= 42)
						{
							if (num == 32)
							{
								instance.SecsBeforeAutoCancel = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 42)
							{
								instance.GoldCostBoosters.Add(GoldCostBooster.DeserializeLengthDelimited(stream));
								continue;
							}
						}
						else
						{
							if (num == 48)
							{
								instance.GoldCostArena = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 58)
							{
								instance.DefaultCurrencyCode = ProtocolParser.ReadString(stream);
								continue;
							}
						}
					}
					else if (num <= 88)
					{
						if (num <= 74)
						{
							if (num == 66)
							{
								instance.Currencies.Add(Currency.DeserializeLengthDelimited(stream));
								continue;
							}
							if (num == 74)
							{
								instance.CheckoutOauthClientId = ProtocolParser.ReadString(stream);
								continue;
							}
						}
						else
						{
							if (num == 82)
							{
								instance.SaleList = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 88)
							{
								instance.CatalogRefreshTime = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
					}
					else if (num <= 104)
					{
						if (num == 98)
						{
							instance.LocaleMap.Add(LocaleMapEntry.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 104)
						{
							instance.IgnoreProductTiming = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					else
					{
						if (num == 114)
						{
							instance.PersonalizedShopPageId = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 122)
						{
							instance.CheckoutKrOnestoreKey = ProtocolParser.ReadString(stream);
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

		// Token: 0x06000C08 RID: 3080 RVA: 0x0002D2B3 File Offset: 0x0002B4B3
		public void Serialize(Stream stream)
		{
			BattlePayConfigResponse.Serialize(stream, this);
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0002D2BC File Offset: 0x0002B4BC
		public static void Serialize(Stream stream, BattlePayConfigResponse instance)
		{
			if (instance.Bundles.Count > 0)
			{
				foreach (Bundle bundle in instance.Bundles)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, bundle.GetSerializedSize());
					Bundle.Serialize(stream, bundle);
				}
			}
			if (instance.HasCurrencyDeprecated)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CurrencyDeprecated));
			}
			if (instance.HasUnavailable)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.Unavailable);
			}
			if (instance.HasSecsBeforeAutoCancel)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SecsBeforeAutoCancel));
			}
			if (instance.GoldCostBoosters.Count > 0)
			{
				foreach (GoldCostBooster goldCostBooster in instance.GoldCostBoosters)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, goldCostBooster.GetSerializedSize());
					GoldCostBooster.Serialize(stream, goldCostBooster);
				}
			}
			if (instance.HasGoldCostArena)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GoldCostArena);
			}
			if (instance.HasDefaultCurrencyCode)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DefaultCurrencyCode));
			}
			if (instance.Currencies.Count > 0)
			{
				foreach (Currency currency in instance.Currencies)
				{
					stream.WriteByte(66);
					ProtocolParser.WriteUInt32(stream, currency.GetSerializedSize());
					Currency.Serialize(stream, currency);
				}
			}
			if (instance.HasCheckoutOauthClientId)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CheckoutOauthClientId));
			}
			if (instance.HasSaleList)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SaleList));
			}
			if (instance.HasCatalogRefreshTime)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CatalogRefreshTime);
			}
			if (instance.LocaleMap.Count > 0)
			{
				foreach (LocaleMapEntry localeMapEntry in instance.LocaleMap)
				{
					stream.WriteByte(98);
					ProtocolParser.WriteUInt32(stream, localeMapEntry.GetSerializedSize());
					LocaleMapEntry.Serialize(stream, localeMapEntry);
				}
			}
			if (instance.HasIgnoreProductTiming)
			{
				stream.WriteByte(104);
				ProtocolParser.WriteBool(stream, instance.IgnoreProductTiming);
			}
			if (instance.HasPersonalizedShopPageId)
			{
				stream.WriteByte(114);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.PersonalizedShopPageId));
			}
			if (instance.HasCheckoutKrOnestoreKey)
			{
				stream.WriteByte(122);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CheckoutKrOnestoreKey));
			}
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0002D5D0 File Offset: 0x0002B7D0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Bundles.Count > 0)
			{
				foreach (Bundle bundle in this.Bundles)
				{
					num += 1U;
					uint serializedSize = bundle.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasCurrencyDeprecated)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CurrencyDeprecated));
			}
			if (this.HasUnavailable)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasSecsBeforeAutoCancel)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SecsBeforeAutoCancel));
			}
			if (this.GoldCostBoosters.Count > 0)
			{
				foreach (GoldCostBooster goldCostBooster in this.GoldCostBoosters)
				{
					num += 1U;
					uint serializedSize2 = goldCostBooster.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasGoldCostArena)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.GoldCostArena);
			}
			if (this.HasDefaultCurrencyCode)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.DefaultCurrencyCode);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.Currencies.Count > 0)
			{
				foreach (Currency currency in this.Currencies)
				{
					num += 1U;
					uint serializedSize3 = currency.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (this.HasCheckoutOauthClientId)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.CheckoutOauthClientId);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasSaleList)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.SaleList);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasCatalogRefreshTime)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.CatalogRefreshTime);
			}
			if (this.LocaleMap.Count > 0)
			{
				foreach (LocaleMapEntry localeMapEntry in this.LocaleMap)
				{
					num += 1U;
					uint serializedSize4 = localeMapEntry.GetSerializedSize();
					num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
				}
			}
			if (this.HasIgnoreProductTiming)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasPersonalizedShopPageId)
			{
				num += 1U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.PersonalizedShopPageId);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (this.HasCheckoutKrOnestoreKey)
			{
				num += 1U;
				uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(this.CheckoutKrOnestoreKey);
				num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			}
			return num;
		}

		// Token: 0x04000424 RID: 1060
		private List<Bundle> _Bundles = new List<Bundle>();

		// Token: 0x04000425 RID: 1061
		public bool HasCurrencyDeprecated;

		// Token: 0x04000426 RID: 1062
		private int _CurrencyDeprecated;

		// Token: 0x04000427 RID: 1063
		public bool HasUnavailable;

		// Token: 0x04000428 RID: 1064
		private bool _Unavailable;

		// Token: 0x04000429 RID: 1065
		public bool HasSecsBeforeAutoCancel;

		// Token: 0x0400042A RID: 1066
		private int _SecsBeforeAutoCancel;

		// Token: 0x0400042B RID: 1067
		private List<GoldCostBooster> _GoldCostBoosters = new List<GoldCostBooster>();

		// Token: 0x0400042C RID: 1068
		public bool HasGoldCostArena;

		// Token: 0x0400042D RID: 1069
		private long _GoldCostArena;

		// Token: 0x0400042E RID: 1070
		public bool HasDefaultCurrencyCode;

		// Token: 0x0400042F RID: 1071
		private string _DefaultCurrencyCode;

		// Token: 0x04000430 RID: 1072
		private List<Currency> _Currencies = new List<Currency>();

		// Token: 0x04000431 RID: 1073
		public bool HasCheckoutOauthClientId;

		// Token: 0x04000432 RID: 1074
		private string _CheckoutOauthClientId;

		// Token: 0x04000433 RID: 1075
		public bool HasSaleList;

		// Token: 0x04000434 RID: 1076
		private string _SaleList;

		// Token: 0x04000435 RID: 1077
		public bool HasCatalogRefreshTime;

		// Token: 0x04000436 RID: 1078
		private long _CatalogRefreshTime;

		// Token: 0x04000437 RID: 1079
		private List<LocaleMapEntry> _LocaleMap = new List<LocaleMapEntry>();

		// Token: 0x04000438 RID: 1080
		public bool HasIgnoreProductTiming;

		// Token: 0x04000439 RID: 1081
		private bool _IgnoreProductTiming;

		// Token: 0x0400043A RID: 1082
		public bool HasPersonalizedShopPageId;

		// Token: 0x0400043B RID: 1083
		private string _PersonalizedShopPageId;

		// Token: 0x0400043C RID: 1084
		public bool HasCheckoutKrOnestoreKey;

		// Token: 0x0400043D RID: 1085
		private string _CheckoutKrOnestoreKey;

		// Token: 0x020005B5 RID: 1461
		public enum PacketID
		{
			// Token: 0x04001F70 RID: 8048
			ID = 238
		}
	}
}
