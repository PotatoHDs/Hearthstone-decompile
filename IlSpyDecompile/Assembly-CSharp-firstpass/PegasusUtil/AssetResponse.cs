using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class AssetResponse : IProtoBuf
	{
		public bool HasDeprecatedDatabaseResult;

		private DatabaseResult _DeprecatedDatabaseResult;

		public bool HasScenarioAsset;

		private ScenarioDbRecord _ScenarioAsset;

		public bool HasSubsetCardListAsset;

		private SubsetCardListDbRecord _SubsetCardListAsset;

		public bool HasDeckRulesetAsset;

		private DeckRulesetDbRecord _DeckRulesetAsset;

		public bool HasRewardChestAsset;

		private RewardChestDbRecord _RewardChestAsset;

		public bool HasGuestHeroAsset;

		private GuestHeroDbRecord _GuestHeroAsset;

		public bool HasDeckTemplateAsset;

		private DeckTemplateDbRecord _DeckTemplateAsset;

		public AssetKey RequestedKey { get; set; }

		public DatabaseResult DeprecatedDatabaseResult
		{
			get
			{
				return _DeprecatedDatabaseResult;
			}
			set
			{
				_DeprecatedDatabaseResult = value;
				HasDeprecatedDatabaseResult = true;
			}
		}

		public ErrorCode ErrorCode { get; set; }

		public ScenarioDbRecord ScenarioAsset
		{
			get
			{
				return _ScenarioAsset;
			}
			set
			{
				_ScenarioAsset = value;
				HasScenarioAsset = value != null;
			}
		}

		public SubsetCardListDbRecord SubsetCardListAsset
		{
			get
			{
				return _SubsetCardListAsset;
			}
			set
			{
				_SubsetCardListAsset = value;
				HasSubsetCardListAsset = value != null;
			}
		}

		public DeckRulesetDbRecord DeckRulesetAsset
		{
			get
			{
				return _DeckRulesetAsset;
			}
			set
			{
				_DeckRulesetAsset = value;
				HasDeckRulesetAsset = value != null;
			}
		}

		public RewardChestDbRecord RewardChestAsset
		{
			get
			{
				return _RewardChestAsset;
			}
			set
			{
				_RewardChestAsset = value;
				HasRewardChestAsset = value != null;
			}
		}

		public GuestHeroDbRecord GuestHeroAsset
		{
			get
			{
				return _GuestHeroAsset;
			}
			set
			{
				_GuestHeroAsset = value;
				HasGuestHeroAsset = value != null;
			}
		}

		public DeckTemplateDbRecord DeckTemplateAsset
		{
			get
			{
				return _DeckTemplateAsset;
			}
			set
			{
				_DeckTemplateAsset = value;
				HasDeckTemplateAsset = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= RequestedKey.GetHashCode();
			if (HasDeprecatedDatabaseResult)
			{
				hashCode ^= DeprecatedDatabaseResult.GetHashCode();
			}
			hashCode ^= ErrorCode.GetHashCode();
			if (HasScenarioAsset)
			{
				hashCode ^= ScenarioAsset.GetHashCode();
			}
			if (HasSubsetCardListAsset)
			{
				hashCode ^= SubsetCardListAsset.GetHashCode();
			}
			if (HasDeckRulesetAsset)
			{
				hashCode ^= DeckRulesetAsset.GetHashCode();
			}
			if (HasRewardChestAsset)
			{
				hashCode ^= RewardChestAsset.GetHashCode();
			}
			if (HasGuestHeroAsset)
			{
				hashCode ^= GuestHeroAsset.GetHashCode();
			}
			if (HasDeckTemplateAsset)
			{
				hashCode ^= DeckTemplateAsset.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			AssetResponse assetResponse = obj as AssetResponse;
			if (assetResponse == null)
			{
				return false;
			}
			if (!RequestedKey.Equals(assetResponse.RequestedKey))
			{
				return false;
			}
			if (HasDeprecatedDatabaseResult != assetResponse.HasDeprecatedDatabaseResult || (HasDeprecatedDatabaseResult && !DeprecatedDatabaseResult.Equals(assetResponse.DeprecatedDatabaseResult)))
			{
				return false;
			}
			if (!ErrorCode.Equals(assetResponse.ErrorCode))
			{
				return false;
			}
			if (HasScenarioAsset != assetResponse.HasScenarioAsset || (HasScenarioAsset && !ScenarioAsset.Equals(assetResponse.ScenarioAsset)))
			{
				return false;
			}
			if (HasSubsetCardListAsset != assetResponse.HasSubsetCardListAsset || (HasSubsetCardListAsset && !SubsetCardListAsset.Equals(assetResponse.SubsetCardListAsset)))
			{
				return false;
			}
			if (HasDeckRulesetAsset != assetResponse.HasDeckRulesetAsset || (HasDeckRulesetAsset && !DeckRulesetAsset.Equals(assetResponse.DeckRulesetAsset)))
			{
				return false;
			}
			if (HasRewardChestAsset != assetResponse.HasRewardChestAsset || (HasRewardChestAsset && !RewardChestAsset.Equals(assetResponse.RewardChestAsset)))
			{
				return false;
			}
			if (HasGuestHeroAsset != assetResponse.HasGuestHeroAsset || (HasGuestHeroAsset && !GuestHeroAsset.Equals(assetResponse.GuestHeroAsset)))
			{
				return false;
			}
			if (HasDeckTemplateAsset != assetResponse.HasDeckTemplateAsset || (HasDeckTemplateAsset && !DeckTemplateAsset.Equals(assetResponse.DeckTemplateAsset)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AssetResponse Deserialize(Stream stream, AssetResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AssetResponse DeserializeLengthDelimited(Stream stream)
		{
			AssetResponse assetResponse = new AssetResponse();
			DeserializeLengthDelimited(stream, assetResponse);
			return assetResponse;
		}

		public static AssetResponse DeserializeLengthDelimited(Stream stream, AssetResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AssetResponse Deserialize(Stream stream, AssetResponse instance, long limit)
		{
			instance.DeprecatedDatabaseResult = DatabaseResult.DB_E_SQL_EX;
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
					if (instance.RequestedKey == null)
					{
						instance.RequestedKey = AssetKey.DeserializeLengthDelimited(stream);
					}
					else
					{
						AssetKey.DeserializeLengthDelimited(stream, instance.RequestedKey);
					}
					continue;
				case 16:
					instance.DeprecatedDatabaseResult = (DatabaseResult)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.ErrorCode = (ErrorCode)ProtocolParser.ReadUInt64(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 100u:
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
					case 101u:
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
					case 102u:
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
					case 103u:
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
					case 104u:
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
					case 105u:
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
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeprecatedDatabaseResult);
			}
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ErrorCode);
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

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = RequestedKey.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasDeprecatedDatabaseResult)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DeprecatedDatabaseResult);
			}
			num += ProtocolParser.SizeOfUInt64((ulong)ErrorCode);
			if (HasScenarioAsset)
			{
				num += 2;
				uint serializedSize2 = ScenarioAsset.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasSubsetCardListAsset)
			{
				num += 2;
				uint serializedSize3 = SubsetCardListAsset.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (HasDeckRulesetAsset)
			{
				num += 2;
				uint serializedSize4 = DeckRulesetAsset.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (HasRewardChestAsset)
			{
				num += 2;
				uint serializedSize5 = RewardChestAsset.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			if (HasGuestHeroAsset)
			{
				num += 2;
				uint serializedSize6 = GuestHeroAsset.GetSerializedSize();
				num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
			}
			if (HasDeckTemplateAsset)
			{
				num += 2;
				uint serializedSize7 = DeckTemplateAsset.GetSerializedSize();
				num += serializedSize7 + ProtocolParser.SizeOfUInt32(serializedSize7);
			}
			return num + 2;
		}
	}
}
