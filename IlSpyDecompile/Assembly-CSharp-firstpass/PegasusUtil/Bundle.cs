using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusUtil
{
	public class Bundle : IProtoBuf
	{
		public bool HasDeprecatedCost;

		private double _DeprecatedCost;

		public bool HasAppleId;

		private string _AppleId;

		public bool HasGooglePlayId;

		private string _GooglePlayId;

		private List<BundleItem> _Items = new List<BundleItem>();

		public bool HasGoldCost;

		private long _GoldCost;

		public bool HasAmazonId;

		private string _AmazonId;

		public bool HasProductEventName;

		private string _ProductEventName;

		private List<BattlePayProvider> _ExclusiveProviders = new List<BattlePayProvider>();

		public bool HasRealMoneyProductEventName;

		private string _RealMoneyProductEventName;

		public bool HasCost;

		private ulong _Cost;

		public bool HasIsPrePurchase;

		private bool _IsPrePurchase;

		public bool HasPmtProductId;

		private long _PmtProductId;

		public bool HasDisplayName;

		private LocalizedString _DisplayName;

		public bool HasDisplayDesc;

		private LocalizedString _DisplayDesc;

		private List<ProductAttribute> _Attributes = new List<ProductAttribute>();

		public bool HasVisibleOnSalePeriodOnly;

		private bool _VisibleOnSalePeriodOnly;

		private List<int> _SaleIds = new List<int>();

		public bool HasVirtualCurrencyCost;

		private VirtualCurrencyCost _VirtualCurrencyCost;

		public bool HasKronestoreId;

		private string _KronestoreId;

		public string Id { get; set; }

		public double DeprecatedCost
		{
			get
			{
				return _DeprecatedCost;
			}
			set
			{
				_DeprecatedCost = value;
				HasDeprecatedCost = true;
			}
		}

		public string AppleId
		{
			get
			{
				return _AppleId;
			}
			set
			{
				_AppleId = value;
				HasAppleId = value != null;
			}
		}

		public string GooglePlayId
		{
			get
			{
				return _GooglePlayId;
			}
			set
			{
				_GooglePlayId = value;
				HasGooglePlayId = value != null;
			}
		}

		public List<BundleItem> Items
		{
			get
			{
				return _Items;
			}
			set
			{
				_Items = value;
			}
		}

		public long GoldCost
		{
			get
			{
				return _GoldCost;
			}
			set
			{
				_GoldCost = value;
				HasGoldCost = true;
			}
		}

		public string AmazonId
		{
			get
			{
				return _AmazonId;
			}
			set
			{
				_AmazonId = value;
				HasAmazonId = value != null;
			}
		}

		public string ProductEventName
		{
			get
			{
				return _ProductEventName;
			}
			set
			{
				_ProductEventName = value;
				HasProductEventName = value != null;
			}
		}

		public List<BattlePayProvider> ExclusiveProviders
		{
			get
			{
				return _ExclusiveProviders;
			}
			set
			{
				_ExclusiveProviders = value;
			}
		}

		public string RealMoneyProductEventName
		{
			get
			{
				return _RealMoneyProductEventName;
			}
			set
			{
				_RealMoneyProductEventName = value;
				HasRealMoneyProductEventName = value != null;
			}
		}

		public ulong Cost
		{
			get
			{
				return _Cost;
			}
			set
			{
				_Cost = value;
				HasCost = true;
			}
		}

		public bool IsPrePurchase
		{
			get
			{
				return _IsPrePurchase;
			}
			set
			{
				_IsPrePurchase = value;
				HasIsPrePurchase = true;
			}
		}

		public long PmtProductId
		{
			get
			{
				return _PmtProductId;
			}
			set
			{
				_PmtProductId = value;
				HasPmtProductId = true;
			}
		}

		public LocalizedString DisplayName
		{
			get
			{
				return _DisplayName;
			}
			set
			{
				_DisplayName = value;
				HasDisplayName = value != null;
			}
		}

		public LocalizedString DisplayDesc
		{
			get
			{
				return _DisplayDesc;
			}
			set
			{
				_DisplayDesc = value;
				HasDisplayDesc = value != null;
			}
		}

		public List<ProductAttribute> Attributes
		{
			get
			{
				return _Attributes;
			}
			set
			{
				_Attributes = value;
			}
		}

		public bool VisibleOnSalePeriodOnly
		{
			get
			{
				return _VisibleOnSalePeriodOnly;
			}
			set
			{
				_VisibleOnSalePeriodOnly = value;
				HasVisibleOnSalePeriodOnly = true;
			}
		}

		public List<int> SaleIds
		{
			get
			{
				return _SaleIds;
			}
			set
			{
				_SaleIds = value;
			}
		}

		public VirtualCurrencyCost VirtualCurrencyCost
		{
			get
			{
				return _VirtualCurrencyCost;
			}
			set
			{
				_VirtualCurrencyCost = value;
				HasVirtualCurrencyCost = value != null;
			}
		}

		public string KronestoreId
		{
			get
			{
				return _KronestoreId;
			}
			set
			{
				_KronestoreId = value;
				HasKronestoreId = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Id.GetHashCode();
			if (HasDeprecatedCost)
			{
				hashCode ^= DeprecatedCost.GetHashCode();
			}
			if (HasAppleId)
			{
				hashCode ^= AppleId.GetHashCode();
			}
			if (HasGooglePlayId)
			{
				hashCode ^= GooglePlayId.GetHashCode();
			}
			foreach (BundleItem item in Items)
			{
				hashCode ^= item.GetHashCode();
			}
			if (HasGoldCost)
			{
				hashCode ^= GoldCost.GetHashCode();
			}
			if (HasAmazonId)
			{
				hashCode ^= AmazonId.GetHashCode();
			}
			if (HasProductEventName)
			{
				hashCode ^= ProductEventName.GetHashCode();
			}
			foreach (BattlePayProvider exclusiveProvider in ExclusiveProviders)
			{
				hashCode ^= exclusiveProvider.GetHashCode();
			}
			if (HasRealMoneyProductEventName)
			{
				hashCode ^= RealMoneyProductEventName.GetHashCode();
			}
			if (HasCost)
			{
				hashCode ^= Cost.GetHashCode();
			}
			if (HasIsPrePurchase)
			{
				hashCode ^= IsPrePurchase.GetHashCode();
			}
			if (HasPmtProductId)
			{
				hashCode ^= PmtProductId.GetHashCode();
			}
			if (HasDisplayName)
			{
				hashCode ^= DisplayName.GetHashCode();
			}
			if (HasDisplayDesc)
			{
				hashCode ^= DisplayDesc.GetHashCode();
			}
			foreach (ProductAttribute attribute in Attributes)
			{
				hashCode ^= attribute.GetHashCode();
			}
			if (HasVisibleOnSalePeriodOnly)
			{
				hashCode ^= VisibleOnSalePeriodOnly.GetHashCode();
			}
			foreach (int saleId in SaleIds)
			{
				hashCode ^= saleId.GetHashCode();
			}
			if (HasVirtualCurrencyCost)
			{
				hashCode ^= VirtualCurrencyCost.GetHashCode();
			}
			if (HasKronestoreId)
			{
				hashCode ^= KronestoreId.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			Bundle bundle = obj as Bundle;
			if (bundle == null)
			{
				return false;
			}
			if (!Id.Equals(bundle.Id))
			{
				return false;
			}
			if (HasDeprecatedCost != bundle.HasDeprecatedCost || (HasDeprecatedCost && !DeprecatedCost.Equals(bundle.DeprecatedCost)))
			{
				return false;
			}
			if (HasAppleId != bundle.HasAppleId || (HasAppleId && !AppleId.Equals(bundle.AppleId)))
			{
				return false;
			}
			if (HasGooglePlayId != bundle.HasGooglePlayId || (HasGooglePlayId && !GooglePlayId.Equals(bundle.GooglePlayId)))
			{
				return false;
			}
			if (Items.Count != bundle.Items.Count)
			{
				return false;
			}
			for (int i = 0; i < Items.Count; i++)
			{
				if (!Items[i].Equals(bundle.Items[i]))
				{
					return false;
				}
			}
			if (HasGoldCost != bundle.HasGoldCost || (HasGoldCost && !GoldCost.Equals(bundle.GoldCost)))
			{
				return false;
			}
			if (HasAmazonId != bundle.HasAmazonId || (HasAmazonId && !AmazonId.Equals(bundle.AmazonId)))
			{
				return false;
			}
			if (HasProductEventName != bundle.HasProductEventName || (HasProductEventName && !ProductEventName.Equals(bundle.ProductEventName)))
			{
				return false;
			}
			if (ExclusiveProviders.Count != bundle.ExclusiveProviders.Count)
			{
				return false;
			}
			for (int j = 0; j < ExclusiveProviders.Count; j++)
			{
				if (!ExclusiveProviders[j].Equals(bundle.ExclusiveProviders[j]))
				{
					return false;
				}
			}
			if (HasRealMoneyProductEventName != bundle.HasRealMoneyProductEventName || (HasRealMoneyProductEventName && !RealMoneyProductEventName.Equals(bundle.RealMoneyProductEventName)))
			{
				return false;
			}
			if (HasCost != bundle.HasCost || (HasCost && !Cost.Equals(bundle.Cost)))
			{
				return false;
			}
			if (HasIsPrePurchase != bundle.HasIsPrePurchase || (HasIsPrePurchase && !IsPrePurchase.Equals(bundle.IsPrePurchase)))
			{
				return false;
			}
			if (HasPmtProductId != bundle.HasPmtProductId || (HasPmtProductId && !PmtProductId.Equals(bundle.PmtProductId)))
			{
				return false;
			}
			if (HasDisplayName != bundle.HasDisplayName || (HasDisplayName && !DisplayName.Equals(bundle.DisplayName)))
			{
				return false;
			}
			if (HasDisplayDesc != bundle.HasDisplayDesc || (HasDisplayDesc && !DisplayDesc.Equals(bundle.DisplayDesc)))
			{
				return false;
			}
			if (Attributes.Count != bundle.Attributes.Count)
			{
				return false;
			}
			for (int k = 0; k < Attributes.Count; k++)
			{
				if (!Attributes[k].Equals(bundle.Attributes[k]))
				{
					return false;
				}
			}
			if (HasVisibleOnSalePeriodOnly != bundle.HasVisibleOnSalePeriodOnly || (HasVisibleOnSalePeriodOnly && !VisibleOnSalePeriodOnly.Equals(bundle.VisibleOnSalePeriodOnly)))
			{
				return false;
			}
			if (SaleIds.Count != bundle.SaleIds.Count)
			{
				return false;
			}
			for (int l = 0; l < SaleIds.Count; l++)
			{
				if (!SaleIds[l].Equals(bundle.SaleIds[l]))
				{
					return false;
				}
			}
			if (HasVirtualCurrencyCost != bundle.HasVirtualCurrencyCost || (HasVirtualCurrencyCost && !VirtualCurrencyCost.Equals(bundle.VirtualCurrencyCost)))
			{
				return false;
			}
			if (HasKronestoreId != bundle.HasKronestoreId || (HasKronestoreId && !KronestoreId.Equals(bundle.KronestoreId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Bundle Deserialize(Stream stream, Bundle instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Bundle DeserializeLengthDelimited(Stream stream)
		{
			Bundle bundle = new Bundle();
			DeserializeLengthDelimited(stream, bundle);
			return bundle;
		}

		public static Bundle DeserializeLengthDelimited(Stream stream, Bundle instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

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
					instance.Id = ProtocolParser.ReadString(stream);
					continue;
				case 17:
					instance.DeprecatedCost = binaryReader.ReadDouble();
					continue;
				case 26:
					instance.AppleId = ProtocolParser.ReadString(stream);
					continue;
				case 34:
					instance.GooglePlayId = ProtocolParser.ReadString(stream);
					continue;
				case 42:
					instance.Items.Add(BundleItem.DeserializeLengthDelimited(stream));
					continue;
				case 48:
					instance.GoldCost = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 58:
					instance.AmazonId = ProtocolParser.ReadString(stream);
					continue;
				case 74:
					instance.ProductEventName = ProtocolParser.ReadString(stream);
					continue;
				case 80:
					instance.ExclusiveProviders.Add((BattlePayProvider)ProtocolParser.ReadUInt64(stream));
					continue;
				case 90:
					instance.RealMoneyProductEventName = ProtocolParser.ReadString(stream);
					continue;
				case 96:
					instance.Cost = ProtocolParser.ReadUInt64(stream);
					continue;
				case 104:
					instance.IsPrePurchase = ProtocolParser.ReadBool(stream);
					continue;
				case 112:
					instance.PmtProductId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 122:
					if (instance.DisplayName == null)
					{
						instance.DisplayName = LocalizedString.DeserializeLengthDelimited(stream);
					}
					else
					{
						LocalizedString.DeserializeLengthDelimited(stream, instance.DisplayName);
					}
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 16u:
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
					case 17u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.Attributes.Add(ProductAttribute.DeserializeLengthDelimited(stream));
						}
						break;
					case 18u:
						if (key.WireType == Wire.Varint)
						{
							instance.VisibleOnSalePeriodOnly = ProtocolParser.ReadBool(stream);
						}
						break;
					case 19u:
						if (key.WireType == Wire.Varint)
						{
							instance.SaleIds.Add((int)ProtocolParser.ReadUInt64(stream));
						}
						break;
					case 20u:
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
					case 21u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.KronestoreId = ProtocolParser.ReadString(stream);
						}
						break;
					default:
						ProtocolParser.SkipKey(stream, key);
						break;
					}
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
				foreach (BundleItem item in instance.Items)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					BundleItem.Serialize(stream, item);
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
				foreach (BattlePayProvider exclusiveProvider in instance.ExclusiveProviders)
				{
					stream.WriteByte(80);
					ProtocolParser.WriteUInt64(stream, (ulong)exclusiveProvider);
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
				foreach (ProductAttribute attribute in instance.Attributes)
				{
					stream.WriteByte(138);
					stream.WriteByte(1);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					ProductAttribute.Serialize(stream, attribute);
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
				foreach (int saleId in instance.SaleIds)
				{
					stream.WriteByte(152);
					stream.WriteByte(1);
					ProtocolParser.WriteUInt64(stream, (ulong)saleId);
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

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(Id);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (HasDeprecatedCost)
			{
				num++;
				num += 8;
			}
			if (HasAppleId)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(AppleId);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasGooglePlayId)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(GooglePlayId);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (Items.Count > 0)
			{
				foreach (BundleItem item in Items)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasGoldCost)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)GoldCost);
			}
			if (HasAmazonId)
			{
				num++;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(AmazonId);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (HasProductEventName)
			{
				num++;
				uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(ProductEventName);
				num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			}
			if (ExclusiveProviders.Count > 0)
			{
				foreach (BattlePayProvider exclusiveProvider in ExclusiveProviders)
				{
					num++;
					num += ProtocolParser.SizeOfUInt64((ulong)exclusiveProvider);
				}
			}
			if (HasRealMoneyProductEventName)
			{
				num++;
				uint byteCount6 = (uint)Encoding.UTF8.GetByteCount(RealMoneyProductEventName);
				num += ProtocolParser.SizeOfUInt32(byteCount6) + byteCount6;
			}
			if (HasCost)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(Cost);
			}
			if (HasIsPrePurchase)
			{
				num++;
				num++;
			}
			if (HasPmtProductId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)PmtProductId);
			}
			if (HasDisplayName)
			{
				num++;
				uint serializedSize2 = DisplayName.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasDisplayDesc)
			{
				num += 2;
				uint serializedSize3 = DisplayDesc.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (Attributes.Count > 0)
			{
				foreach (ProductAttribute attribute in Attributes)
				{
					num += 2;
					uint serializedSize4 = attribute.GetSerializedSize();
					num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
				}
			}
			if (HasVisibleOnSalePeriodOnly)
			{
				num += 2;
				num++;
			}
			if (SaleIds.Count > 0)
			{
				foreach (int saleId in SaleIds)
				{
					num += 2;
					num += ProtocolParser.SizeOfUInt64((ulong)saleId);
				}
			}
			if (HasVirtualCurrencyCost)
			{
				num += 2;
				uint serializedSize5 = VirtualCurrencyCost.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			if (HasKronestoreId)
			{
				num += 2;
				uint byteCount7 = (uint)Encoding.UTF8.GetByteCount(KronestoreId);
				num += ProtocolParser.SizeOfUInt32(byteCount7) + byteCount7;
			}
			return num + 1;
		}
	}
}
