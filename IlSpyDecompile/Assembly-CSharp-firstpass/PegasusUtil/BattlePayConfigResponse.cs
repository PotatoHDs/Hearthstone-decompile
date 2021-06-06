using System.Collections.Generic;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusUtil
{
	public class BattlePayConfigResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 238
		}

		private List<Bundle> _Bundles = new List<Bundle>();

		public bool HasCurrencyDeprecated;

		private int _CurrencyDeprecated;

		public bool HasUnavailable;

		private bool _Unavailable;

		public bool HasSecsBeforeAutoCancel;

		private int _SecsBeforeAutoCancel;

		private List<GoldCostBooster> _GoldCostBoosters = new List<GoldCostBooster>();

		public bool HasGoldCostArena;

		private long _GoldCostArena;

		public bool HasDefaultCurrencyCode;

		private string _DefaultCurrencyCode;

		private List<Currency> _Currencies = new List<Currency>();

		public bool HasCheckoutOauthClientId;

		private string _CheckoutOauthClientId;

		public bool HasSaleList;

		private string _SaleList;

		public bool HasCatalogRefreshTime;

		private long _CatalogRefreshTime;

		private List<LocaleMapEntry> _LocaleMap = new List<LocaleMapEntry>();

		public bool HasIgnoreProductTiming;

		private bool _IgnoreProductTiming;

		public bool HasPersonalizedShopPageId;

		private string _PersonalizedShopPageId;

		public bool HasCheckoutKrOnestoreKey;

		private string _CheckoutKrOnestoreKey;

		public List<Bundle> Bundles
		{
			get
			{
				return _Bundles;
			}
			set
			{
				_Bundles = value;
			}
		}

		public int CurrencyDeprecated
		{
			get
			{
				return _CurrencyDeprecated;
			}
			set
			{
				_CurrencyDeprecated = value;
				HasCurrencyDeprecated = true;
			}
		}

		public bool Unavailable
		{
			get
			{
				return _Unavailable;
			}
			set
			{
				_Unavailable = value;
				HasUnavailable = true;
			}
		}

		public int SecsBeforeAutoCancel
		{
			get
			{
				return _SecsBeforeAutoCancel;
			}
			set
			{
				_SecsBeforeAutoCancel = value;
				HasSecsBeforeAutoCancel = true;
			}
		}

		public List<GoldCostBooster> GoldCostBoosters
		{
			get
			{
				return _GoldCostBoosters;
			}
			set
			{
				_GoldCostBoosters = value;
			}
		}

		public long GoldCostArena
		{
			get
			{
				return _GoldCostArena;
			}
			set
			{
				_GoldCostArena = value;
				HasGoldCostArena = true;
			}
		}

		public string DefaultCurrencyCode
		{
			get
			{
				return _DefaultCurrencyCode;
			}
			set
			{
				_DefaultCurrencyCode = value;
				HasDefaultCurrencyCode = value != null;
			}
		}

		public List<Currency> Currencies
		{
			get
			{
				return _Currencies;
			}
			set
			{
				_Currencies = value;
			}
		}

		public string CheckoutOauthClientId
		{
			get
			{
				return _CheckoutOauthClientId;
			}
			set
			{
				_CheckoutOauthClientId = value;
				HasCheckoutOauthClientId = value != null;
			}
		}

		public string SaleList
		{
			get
			{
				return _SaleList;
			}
			set
			{
				_SaleList = value;
				HasSaleList = value != null;
			}
		}

		public long CatalogRefreshTime
		{
			get
			{
				return _CatalogRefreshTime;
			}
			set
			{
				_CatalogRefreshTime = value;
				HasCatalogRefreshTime = true;
			}
		}

		public List<LocaleMapEntry> LocaleMap
		{
			get
			{
				return _LocaleMap;
			}
			set
			{
				_LocaleMap = value;
			}
		}

		public bool IgnoreProductTiming
		{
			get
			{
				return _IgnoreProductTiming;
			}
			set
			{
				_IgnoreProductTiming = value;
				HasIgnoreProductTiming = true;
			}
		}

		public string PersonalizedShopPageId
		{
			get
			{
				return _PersonalizedShopPageId;
			}
			set
			{
				_PersonalizedShopPageId = value;
				HasPersonalizedShopPageId = value != null;
			}
		}

		public string CheckoutKrOnestoreKey
		{
			get
			{
				return _CheckoutKrOnestoreKey;
			}
			set
			{
				_CheckoutKrOnestoreKey = value;
				HasCheckoutKrOnestoreKey = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (Bundle bundle in Bundles)
			{
				num ^= bundle.GetHashCode();
			}
			if (HasCurrencyDeprecated)
			{
				num ^= CurrencyDeprecated.GetHashCode();
			}
			if (HasUnavailable)
			{
				num ^= Unavailable.GetHashCode();
			}
			if (HasSecsBeforeAutoCancel)
			{
				num ^= SecsBeforeAutoCancel.GetHashCode();
			}
			foreach (GoldCostBooster goldCostBooster in GoldCostBoosters)
			{
				num ^= goldCostBooster.GetHashCode();
			}
			if (HasGoldCostArena)
			{
				num ^= GoldCostArena.GetHashCode();
			}
			if (HasDefaultCurrencyCode)
			{
				num ^= DefaultCurrencyCode.GetHashCode();
			}
			foreach (Currency currency in Currencies)
			{
				num ^= currency.GetHashCode();
			}
			if (HasCheckoutOauthClientId)
			{
				num ^= CheckoutOauthClientId.GetHashCode();
			}
			if (HasSaleList)
			{
				num ^= SaleList.GetHashCode();
			}
			if (HasCatalogRefreshTime)
			{
				num ^= CatalogRefreshTime.GetHashCode();
			}
			foreach (LocaleMapEntry item in LocaleMap)
			{
				num ^= item.GetHashCode();
			}
			if (HasIgnoreProductTiming)
			{
				num ^= IgnoreProductTiming.GetHashCode();
			}
			if (HasPersonalizedShopPageId)
			{
				num ^= PersonalizedShopPageId.GetHashCode();
			}
			if (HasCheckoutKrOnestoreKey)
			{
				num ^= CheckoutKrOnestoreKey.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			BattlePayConfigResponse battlePayConfigResponse = obj as BattlePayConfigResponse;
			if (battlePayConfigResponse == null)
			{
				return false;
			}
			if (Bundles.Count != battlePayConfigResponse.Bundles.Count)
			{
				return false;
			}
			for (int i = 0; i < Bundles.Count; i++)
			{
				if (!Bundles[i].Equals(battlePayConfigResponse.Bundles[i]))
				{
					return false;
				}
			}
			if (HasCurrencyDeprecated != battlePayConfigResponse.HasCurrencyDeprecated || (HasCurrencyDeprecated && !CurrencyDeprecated.Equals(battlePayConfigResponse.CurrencyDeprecated)))
			{
				return false;
			}
			if (HasUnavailable != battlePayConfigResponse.HasUnavailable || (HasUnavailable && !Unavailable.Equals(battlePayConfigResponse.Unavailable)))
			{
				return false;
			}
			if (HasSecsBeforeAutoCancel != battlePayConfigResponse.HasSecsBeforeAutoCancel || (HasSecsBeforeAutoCancel && !SecsBeforeAutoCancel.Equals(battlePayConfigResponse.SecsBeforeAutoCancel)))
			{
				return false;
			}
			if (GoldCostBoosters.Count != battlePayConfigResponse.GoldCostBoosters.Count)
			{
				return false;
			}
			for (int j = 0; j < GoldCostBoosters.Count; j++)
			{
				if (!GoldCostBoosters[j].Equals(battlePayConfigResponse.GoldCostBoosters[j]))
				{
					return false;
				}
			}
			if (HasGoldCostArena != battlePayConfigResponse.HasGoldCostArena || (HasGoldCostArena && !GoldCostArena.Equals(battlePayConfigResponse.GoldCostArena)))
			{
				return false;
			}
			if (HasDefaultCurrencyCode != battlePayConfigResponse.HasDefaultCurrencyCode || (HasDefaultCurrencyCode && !DefaultCurrencyCode.Equals(battlePayConfigResponse.DefaultCurrencyCode)))
			{
				return false;
			}
			if (Currencies.Count != battlePayConfigResponse.Currencies.Count)
			{
				return false;
			}
			for (int k = 0; k < Currencies.Count; k++)
			{
				if (!Currencies[k].Equals(battlePayConfigResponse.Currencies[k]))
				{
					return false;
				}
			}
			if (HasCheckoutOauthClientId != battlePayConfigResponse.HasCheckoutOauthClientId || (HasCheckoutOauthClientId && !CheckoutOauthClientId.Equals(battlePayConfigResponse.CheckoutOauthClientId)))
			{
				return false;
			}
			if (HasSaleList != battlePayConfigResponse.HasSaleList || (HasSaleList && !SaleList.Equals(battlePayConfigResponse.SaleList)))
			{
				return false;
			}
			if (HasCatalogRefreshTime != battlePayConfigResponse.HasCatalogRefreshTime || (HasCatalogRefreshTime && !CatalogRefreshTime.Equals(battlePayConfigResponse.CatalogRefreshTime)))
			{
				return false;
			}
			if (LocaleMap.Count != battlePayConfigResponse.LocaleMap.Count)
			{
				return false;
			}
			for (int l = 0; l < LocaleMap.Count; l++)
			{
				if (!LocaleMap[l].Equals(battlePayConfigResponse.LocaleMap[l]))
				{
					return false;
				}
			}
			if (HasIgnoreProductTiming != battlePayConfigResponse.HasIgnoreProductTiming || (HasIgnoreProductTiming && !IgnoreProductTiming.Equals(battlePayConfigResponse.IgnoreProductTiming)))
			{
				return false;
			}
			if (HasPersonalizedShopPageId != battlePayConfigResponse.HasPersonalizedShopPageId || (HasPersonalizedShopPageId && !PersonalizedShopPageId.Equals(battlePayConfigResponse.PersonalizedShopPageId)))
			{
				return false;
			}
			if (HasCheckoutKrOnestoreKey != battlePayConfigResponse.HasCheckoutKrOnestoreKey || (HasCheckoutKrOnestoreKey && !CheckoutKrOnestoreKey.Equals(battlePayConfigResponse.CheckoutKrOnestoreKey)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BattlePayConfigResponse Deserialize(Stream stream, BattlePayConfigResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BattlePayConfigResponse DeserializeLengthDelimited(Stream stream)
		{
			BattlePayConfigResponse battlePayConfigResponse = new BattlePayConfigResponse();
			DeserializeLengthDelimited(stream, battlePayConfigResponse);
			return battlePayConfigResponse;
		}

		public static BattlePayConfigResponse DeserializeLengthDelimited(Stream stream, BattlePayConfigResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

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
			while (true)
			{
				if (limit >= 0 && stream.Position >= limit)
				{
					if (stream.Position == limit)
					{
						break;
					}
					throw new ProtocolBufferException("Read past max limit");
				}
				int num = stream.ReadByte();
				switch (num)
				{
				case -1:
					break;
				case 10:
					instance.Bundles.Add(Bundle.DeserializeLengthDelimited(stream));
					continue;
				case 16:
					instance.CurrencyDeprecated = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.Unavailable = ProtocolParser.ReadBool(stream);
					continue;
				case 32:
					instance.SecsBeforeAutoCancel = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 42:
					instance.GoldCostBoosters.Add(GoldCostBooster.DeserializeLengthDelimited(stream));
					continue;
				case 48:
					instance.GoldCostArena = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 58:
					instance.DefaultCurrencyCode = ProtocolParser.ReadString(stream);
					continue;
				case 66:
					instance.Currencies.Add(Currency.DeserializeLengthDelimited(stream));
					continue;
				case 74:
					instance.CheckoutOauthClientId = ProtocolParser.ReadString(stream);
					continue;
				case 82:
					instance.SaleList = ProtocolParser.ReadString(stream);
					continue;
				case 88:
					instance.CatalogRefreshTime = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 98:
					instance.LocaleMap.Add(LocaleMapEntry.DeserializeLengthDelimited(stream));
					continue;
				case 104:
					instance.IgnoreProductTiming = ProtocolParser.ReadBool(stream);
					continue;
				case 114:
					instance.PersonalizedShopPageId = ProtocolParser.ReadString(stream);
					continue;
				case 122:
					instance.CheckoutKrOnestoreKey = ProtocolParser.ReadString(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
					continue;
				}
				}
				if (limit < 0)
				{
					break;
				}
				throw new EndOfStreamException();
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

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
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CurrencyDeprecated);
			}
			if (instance.HasUnavailable)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.Unavailable);
			}
			if (instance.HasSecsBeforeAutoCancel)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SecsBeforeAutoCancel);
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
				foreach (LocaleMapEntry item in instance.LocaleMap)
				{
					stream.WriteByte(98);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					LocaleMapEntry.Serialize(stream, item);
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

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Bundles.Count > 0)
			{
				foreach (Bundle bundle in Bundles)
				{
					num++;
					uint serializedSize = bundle.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasCurrencyDeprecated)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CurrencyDeprecated);
			}
			if (HasUnavailable)
			{
				num++;
				num++;
			}
			if (HasSecsBeforeAutoCancel)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)SecsBeforeAutoCancel);
			}
			if (GoldCostBoosters.Count > 0)
			{
				foreach (GoldCostBooster goldCostBooster in GoldCostBoosters)
				{
					num++;
					uint serializedSize2 = goldCostBooster.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (HasGoldCostArena)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)GoldCostArena);
			}
			if (HasDefaultCurrencyCode)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(DefaultCurrencyCode);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (Currencies.Count > 0)
			{
				foreach (Currency currency in Currencies)
				{
					num++;
					uint serializedSize3 = currency.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (HasCheckoutOauthClientId)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(CheckoutOauthClientId);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasSaleList)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(SaleList);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasCatalogRefreshTime)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CatalogRefreshTime);
			}
			if (LocaleMap.Count > 0)
			{
				foreach (LocaleMapEntry item in LocaleMap)
				{
					num++;
					uint serializedSize4 = item.GetSerializedSize();
					num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
				}
			}
			if (HasIgnoreProductTiming)
			{
				num++;
				num++;
			}
			if (HasPersonalizedShopPageId)
			{
				num++;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(PersonalizedShopPageId);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (HasCheckoutKrOnestoreKey)
			{
				num++;
				uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(CheckoutKrOnestoreKey);
				num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			}
			return num;
		}
	}
}
