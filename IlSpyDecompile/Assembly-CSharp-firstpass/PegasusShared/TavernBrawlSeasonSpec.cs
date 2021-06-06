using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusShared
{
	public class TavernBrawlSeasonSpec : IProtoBuf
	{
		public bool HasStorePopupType;

		private TavernBrawlPopupType _StorePopupType;

		public bool HasRewardDesc;

		private string _RewardDesc;

		public bool HasMinRewardDesc;

		private string _MinRewardDesc;

		public bool HasMaxRewardDesc;

		private string _MaxRewardDesc;

		public bool HasEndConditionDesc;

		private string _EndConditionDesc;

		public bool HasDeprecatedStoreInstructionPrefab;

		private string _DeprecatedStoreInstructionPrefab;

		public bool HasDeprecatedStoreInstructionPrefabPhone;

		private string _DeprecatedStoreInstructionPrefabPhone;

		public bool HasDeprecatedBrawlMode;

		private TavernBrawlMode _DeprecatedBrawlMode;

		private List<LocalizedString> _Strings = new List<LocalizedString>();

		public GameContentSeasonSpec GameContentSeason { get; set; }

		public TavernBrawlPopupType StorePopupType
		{
			get
			{
				return _StorePopupType;
			}
			set
			{
				_StorePopupType = value;
				HasStorePopupType = true;
			}
		}

		public string RewardDesc
		{
			get
			{
				return _RewardDesc;
			}
			set
			{
				_RewardDesc = value;
				HasRewardDesc = value != null;
			}
		}

		public string MinRewardDesc
		{
			get
			{
				return _MinRewardDesc;
			}
			set
			{
				_MinRewardDesc = value;
				HasMinRewardDesc = value != null;
			}
		}

		public string MaxRewardDesc
		{
			get
			{
				return _MaxRewardDesc;
			}
			set
			{
				_MaxRewardDesc = value;
				HasMaxRewardDesc = value != null;
			}
		}

		public string EndConditionDesc
		{
			get
			{
				return _EndConditionDesc;
			}
			set
			{
				_EndConditionDesc = value;
				HasEndConditionDesc = value != null;
			}
		}

		public string DeprecatedStoreInstructionPrefab
		{
			get
			{
				return _DeprecatedStoreInstructionPrefab;
			}
			set
			{
				_DeprecatedStoreInstructionPrefab = value;
				HasDeprecatedStoreInstructionPrefab = value != null;
			}
		}

		public string DeprecatedStoreInstructionPrefabPhone
		{
			get
			{
				return _DeprecatedStoreInstructionPrefabPhone;
			}
			set
			{
				_DeprecatedStoreInstructionPrefabPhone = value;
				HasDeprecatedStoreInstructionPrefabPhone = value != null;
			}
		}

		public TavernBrawlMode DeprecatedBrawlMode
		{
			get
			{
				return _DeprecatedBrawlMode;
			}
			set
			{
				_DeprecatedBrawlMode = value;
				HasDeprecatedBrawlMode = true;
			}
		}

		public List<LocalizedString> Strings
		{
			get
			{
				return _Strings;
			}
			set
			{
				_Strings = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= GameContentSeason.GetHashCode();
			if (HasStorePopupType)
			{
				hashCode ^= StorePopupType.GetHashCode();
			}
			if (HasRewardDesc)
			{
				hashCode ^= RewardDesc.GetHashCode();
			}
			if (HasMinRewardDesc)
			{
				hashCode ^= MinRewardDesc.GetHashCode();
			}
			if (HasMaxRewardDesc)
			{
				hashCode ^= MaxRewardDesc.GetHashCode();
			}
			if (HasEndConditionDesc)
			{
				hashCode ^= EndConditionDesc.GetHashCode();
			}
			if (HasDeprecatedStoreInstructionPrefab)
			{
				hashCode ^= DeprecatedStoreInstructionPrefab.GetHashCode();
			}
			if (HasDeprecatedStoreInstructionPrefabPhone)
			{
				hashCode ^= DeprecatedStoreInstructionPrefabPhone.GetHashCode();
			}
			if (HasDeprecatedBrawlMode)
			{
				hashCode ^= DeprecatedBrawlMode.GetHashCode();
			}
			foreach (LocalizedString @string in Strings)
			{
				hashCode ^= @string.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			TavernBrawlSeasonSpec tavernBrawlSeasonSpec = obj as TavernBrawlSeasonSpec;
			if (tavernBrawlSeasonSpec == null)
			{
				return false;
			}
			if (!GameContentSeason.Equals(tavernBrawlSeasonSpec.GameContentSeason))
			{
				return false;
			}
			if (HasStorePopupType != tavernBrawlSeasonSpec.HasStorePopupType || (HasStorePopupType && !StorePopupType.Equals(tavernBrawlSeasonSpec.StorePopupType)))
			{
				return false;
			}
			if (HasRewardDesc != tavernBrawlSeasonSpec.HasRewardDesc || (HasRewardDesc && !RewardDesc.Equals(tavernBrawlSeasonSpec.RewardDesc)))
			{
				return false;
			}
			if (HasMinRewardDesc != tavernBrawlSeasonSpec.HasMinRewardDesc || (HasMinRewardDesc && !MinRewardDesc.Equals(tavernBrawlSeasonSpec.MinRewardDesc)))
			{
				return false;
			}
			if (HasMaxRewardDesc != tavernBrawlSeasonSpec.HasMaxRewardDesc || (HasMaxRewardDesc && !MaxRewardDesc.Equals(tavernBrawlSeasonSpec.MaxRewardDesc)))
			{
				return false;
			}
			if (HasEndConditionDesc != tavernBrawlSeasonSpec.HasEndConditionDesc || (HasEndConditionDesc && !EndConditionDesc.Equals(tavernBrawlSeasonSpec.EndConditionDesc)))
			{
				return false;
			}
			if (HasDeprecatedStoreInstructionPrefab != tavernBrawlSeasonSpec.HasDeprecatedStoreInstructionPrefab || (HasDeprecatedStoreInstructionPrefab && !DeprecatedStoreInstructionPrefab.Equals(tavernBrawlSeasonSpec.DeprecatedStoreInstructionPrefab)))
			{
				return false;
			}
			if (HasDeprecatedStoreInstructionPrefabPhone != tavernBrawlSeasonSpec.HasDeprecatedStoreInstructionPrefabPhone || (HasDeprecatedStoreInstructionPrefabPhone && !DeprecatedStoreInstructionPrefabPhone.Equals(tavernBrawlSeasonSpec.DeprecatedStoreInstructionPrefabPhone)))
			{
				return false;
			}
			if (HasDeprecatedBrawlMode != tavernBrawlSeasonSpec.HasDeprecatedBrawlMode || (HasDeprecatedBrawlMode && !DeprecatedBrawlMode.Equals(tavernBrawlSeasonSpec.DeprecatedBrawlMode)))
			{
				return false;
			}
			if (Strings.Count != tavernBrawlSeasonSpec.Strings.Count)
			{
				return false;
			}
			for (int i = 0; i < Strings.Count; i++)
			{
				if (!Strings[i].Equals(tavernBrawlSeasonSpec.Strings[i]))
				{
					return false;
				}
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static TavernBrawlSeasonSpec Deserialize(Stream stream, TavernBrawlSeasonSpec instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static TavernBrawlSeasonSpec DeserializeLengthDelimited(Stream stream)
		{
			TavernBrawlSeasonSpec tavernBrawlSeasonSpec = new TavernBrawlSeasonSpec();
			DeserializeLengthDelimited(stream, tavernBrawlSeasonSpec);
			return tavernBrawlSeasonSpec;
		}

		public static TavernBrawlSeasonSpec DeserializeLengthDelimited(Stream stream, TavernBrawlSeasonSpec instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static TavernBrawlSeasonSpec Deserialize(Stream stream, TavernBrawlSeasonSpec instance, long limit)
		{
			instance.StorePopupType = TavernBrawlPopupType.POPUP_TYPE_NONE;
			instance.DeprecatedBrawlMode = TavernBrawlMode.TB_MODE_NORMAL;
			if (instance.Strings == null)
			{
				instance.Strings = new List<LocalizedString>();
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
					if (instance.GameContentSeason == null)
					{
						instance.GameContentSeason = GameContentSeasonSpec.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameContentSeasonSpec.DeserializeLengthDelimited(stream, instance.GameContentSeason);
					}
					continue;
				case 16:
					instance.StorePopupType = (TavernBrawlPopupType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					instance.RewardDesc = ProtocolParser.ReadString(stream);
					continue;
				case 34:
					instance.MinRewardDesc = ProtocolParser.ReadString(stream);
					continue;
				case 42:
					instance.MaxRewardDesc = ProtocolParser.ReadString(stream);
					continue;
				case 50:
					instance.EndConditionDesc = ProtocolParser.ReadString(stream);
					continue;
				case 58:
					instance.DeprecatedStoreInstructionPrefab = ProtocolParser.ReadString(stream);
					continue;
				case 66:
					instance.DeprecatedStoreInstructionPrefabPhone = ProtocolParser.ReadString(stream);
					continue;
				case 72:
					instance.DeprecatedBrawlMode = (TavernBrawlMode)ProtocolParser.ReadUInt64(stream);
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
							instance.Strings.Add(LocalizedString.DeserializeLengthDelimited(stream));
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
				ProtocolParser.WriteUInt64(stream, (ulong)instance.StorePopupType);
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
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeprecatedBrawlMode);
			}
			if (instance.Strings.Count <= 0)
			{
				return;
			}
			foreach (LocalizedString @string in instance.Strings)
			{
				stream.WriteByte(162);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, @string.GetSerializedSize());
				LocalizedString.Serialize(stream, @string);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = GameContentSeason.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasStorePopupType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)StorePopupType);
			}
			if (HasRewardDesc)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(RewardDesc);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasMinRewardDesc)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(MinRewardDesc);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasMaxRewardDesc)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(MaxRewardDesc);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasEndConditionDesc)
			{
				num++;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(EndConditionDesc);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (HasDeprecatedStoreInstructionPrefab)
			{
				num++;
				uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(DeprecatedStoreInstructionPrefab);
				num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			}
			if (HasDeprecatedStoreInstructionPrefabPhone)
			{
				num++;
				uint byteCount6 = (uint)Encoding.UTF8.GetByteCount(DeprecatedStoreInstructionPrefabPhone);
				num += ProtocolParser.SizeOfUInt32(byteCount6) + byteCount6;
			}
			if (HasDeprecatedBrawlMode)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DeprecatedBrawlMode);
			}
			if (Strings.Count > 0)
			{
				foreach (LocalizedString @string in Strings)
				{
					num += 2;
					uint serializedSize2 = @string.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num + 1;
		}
	}
}
