using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusShared
{
	public class DeckRulesetRuleDbRecord : IProtoBuf
	{
		public bool HasAppliesToSubsetId;

		private int _AppliesToSubsetId;

		public bool HasAppliesToIsNot;

		private bool _AppliesToIsNot;

		public bool HasMinValue;

		private int _MinValue;

		public bool HasMaxValue;

		private int _MaxValue;

		public bool HasTag;

		private int _Tag;

		public bool HasTagMinValue;

		private int _TagMinValue;

		public bool HasTagMaxValue;

		private int _TagMaxValue;

		public bool HasStringValue;

		private string _StringValue;

		private List<int> _TargetSubsetIds = new List<int>();

		private List<LocalizedString> _Strings = new List<LocalizedString>();

		public int Id { get; set; }

		public int DeckRulesetId { get; set; }

		public int AppliesToSubsetId
		{
			get
			{
				return _AppliesToSubsetId;
			}
			set
			{
				_AppliesToSubsetId = value;
				HasAppliesToSubsetId = true;
			}
		}

		public bool AppliesToIsNot
		{
			get
			{
				return _AppliesToIsNot;
			}
			set
			{
				_AppliesToIsNot = value;
				HasAppliesToIsNot = true;
			}
		}

		public string RuleType { get; set; }

		public bool RuleIsNot { get; set; }

		public int MinValue
		{
			get
			{
				return _MinValue;
			}
			set
			{
				_MinValue = value;
				HasMinValue = true;
			}
		}

		public int MaxValue
		{
			get
			{
				return _MaxValue;
			}
			set
			{
				_MaxValue = value;
				HasMaxValue = true;
			}
		}

		public int Tag
		{
			get
			{
				return _Tag;
			}
			set
			{
				_Tag = value;
				HasTag = true;
			}
		}

		public int TagMinValue
		{
			get
			{
				return _TagMinValue;
			}
			set
			{
				_TagMinValue = value;
				HasTagMinValue = true;
			}
		}

		public int TagMaxValue
		{
			get
			{
				return _TagMaxValue;
			}
			set
			{
				_TagMaxValue = value;
				HasTagMaxValue = true;
			}
		}

		public string StringValue
		{
			get
			{
				return _StringValue;
			}
			set
			{
				_StringValue = value;
				HasStringValue = value != null;
			}
		}

		public List<int> TargetSubsetIds
		{
			get
			{
				return _TargetSubsetIds;
			}
			set
			{
				_TargetSubsetIds = value;
			}
		}

		public bool ShowInvalidCards { get; set; }

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
			hashCode ^= Id.GetHashCode();
			hashCode ^= DeckRulesetId.GetHashCode();
			if (HasAppliesToSubsetId)
			{
				hashCode ^= AppliesToSubsetId.GetHashCode();
			}
			if (HasAppliesToIsNot)
			{
				hashCode ^= AppliesToIsNot.GetHashCode();
			}
			hashCode ^= RuleType.GetHashCode();
			hashCode ^= RuleIsNot.GetHashCode();
			if (HasMinValue)
			{
				hashCode ^= MinValue.GetHashCode();
			}
			if (HasMaxValue)
			{
				hashCode ^= MaxValue.GetHashCode();
			}
			if (HasTag)
			{
				hashCode ^= Tag.GetHashCode();
			}
			if (HasTagMinValue)
			{
				hashCode ^= TagMinValue.GetHashCode();
			}
			if (HasTagMaxValue)
			{
				hashCode ^= TagMaxValue.GetHashCode();
			}
			if (HasStringValue)
			{
				hashCode ^= StringValue.GetHashCode();
			}
			foreach (int targetSubsetId in TargetSubsetIds)
			{
				hashCode ^= targetSubsetId.GetHashCode();
			}
			hashCode ^= ShowInvalidCards.GetHashCode();
			foreach (LocalizedString @string in Strings)
			{
				hashCode ^= @string.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			DeckRulesetRuleDbRecord deckRulesetRuleDbRecord = obj as DeckRulesetRuleDbRecord;
			if (deckRulesetRuleDbRecord == null)
			{
				return false;
			}
			if (!Id.Equals(deckRulesetRuleDbRecord.Id))
			{
				return false;
			}
			if (!DeckRulesetId.Equals(deckRulesetRuleDbRecord.DeckRulesetId))
			{
				return false;
			}
			if (HasAppliesToSubsetId != deckRulesetRuleDbRecord.HasAppliesToSubsetId || (HasAppliesToSubsetId && !AppliesToSubsetId.Equals(deckRulesetRuleDbRecord.AppliesToSubsetId)))
			{
				return false;
			}
			if (HasAppliesToIsNot != deckRulesetRuleDbRecord.HasAppliesToIsNot || (HasAppliesToIsNot && !AppliesToIsNot.Equals(deckRulesetRuleDbRecord.AppliesToIsNot)))
			{
				return false;
			}
			if (!RuleType.Equals(deckRulesetRuleDbRecord.RuleType))
			{
				return false;
			}
			if (!RuleIsNot.Equals(deckRulesetRuleDbRecord.RuleIsNot))
			{
				return false;
			}
			if (HasMinValue != deckRulesetRuleDbRecord.HasMinValue || (HasMinValue && !MinValue.Equals(deckRulesetRuleDbRecord.MinValue)))
			{
				return false;
			}
			if (HasMaxValue != deckRulesetRuleDbRecord.HasMaxValue || (HasMaxValue && !MaxValue.Equals(deckRulesetRuleDbRecord.MaxValue)))
			{
				return false;
			}
			if (HasTag != deckRulesetRuleDbRecord.HasTag || (HasTag && !Tag.Equals(deckRulesetRuleDbRecord.Tag)))
			{
				return false;
			}
			if (HasTagMinValue != deckRulesetRuleDbRecord.HasTagMinValue || (HasTagMinValue && !TagMinValue.Equals(deckRulesetRuleDbRecord.TagMinValue)))
			{
				return false;
			}
			if (HasTagMaxValue != deckRulesetRuleDbRecord.HasTagMaxValue || (HasTagMaxValue && !TagMaxValue.Equals(deckRulesetRuleDbRecord.TagMaxValue)))
			{
				return false;
			}
			if (HasStringValue != deckRulesetRuleDbRecord.HasStringValue || (HasStringValue && !StringValue.Equals(deckRulesetRuleDbRecord.StringValue)))
			{
				return false;
			}
			if (TargetSubsetIds.Count != deckRulesetRuleDbRecord.TargetSubsetIds.Count)
			{
				return false;
			}
			for (int i = 0; i < TargetSubsetIds.Count; i++)
			{
				if (!TargetSubsetIds[i].Equals(deckRulesetRuleDbRecord.TargetSubsetIds[i]))
				{
					return false;
				}
			}
			if (!ShowInvalidCards.Equals(deckRulesetRuleDbRecord.ShowInvalidCards))
			{
				return false;
			}
			if (Strings.Count != deckRulesetRuleDbRecord.Strings.Count)
			{
				return false;
			}
			for (int j = 0; j < Strings.Count; j++)
			{
				if (!Strings[j].Equals(deckRulesetRuleDbRecord.Strings[j]))
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

		public static DeckRulesetRuleDbRecord Deserialize(Stream stream, DeckRulesetRuleDbRecord instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DeckRulesetRuleDbRecord DeserializeLengthDelimited(Stream stream)
		{
			DeckRulesetRuleDbRecord deckRulesetRuleDbRecord = new DeckRulesetRuleDbRecord();
			DeserializeLengthDelimited(stream, deckRulesetRuleDbRecord);
			return deckRulesetRuleDbRecord;
		}

		public static DeckRulesetRuleDbRecord DeserializeLengthDelimited(Stream stream, DeckRulesetRuleDbRecord instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DeckRulesetRuleDbRecord Deserialize(Stream stream, DeckRulesetRuleDbRecord instance, long limit)
		{
			if (instance.TargetSubsetIds == null)
			{
				instance.TargetSubsetIds = new List<int>();
			}
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
				case 8:
					instance.Id = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.DeckRulesetId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.AppliesToSubsetId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.AppliesToIsNot = ProtocolParser.ReadBool(stream);
					continue;
				case 42:
					instance.RuleType = ProtocolParser.ReadString(stream);
					continue;
				case 48:
					instance.RuleIsNot = ProtocolParser.ReadBool(stream);
					continue;
				case 56:
					instance.MinValue = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 64:
					instance.MaxValue = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 72:
					instance.Tag = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 80:
					instance.TagMinValue = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 88:
					instance.TagMaxValue = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 98:
					instance.StringValue = ProtocolParser.ReadString(stream);
					continue;
				case 104:
					instance.TargetSubsetIds.Add((int)ProtocolParser.ReadUInt64(stream));
					continue;
				case 112:
					instance.ShowInvalidCards = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, DeckRulesetRuleDbRecord instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Id);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckRulesetId);
			if (instance.HasAppliesToSubsetId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.AppliesToSubsetId);
			}
			if (instance.HasAppliesToIsNot)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.AppliesToIsNot);
			}
			if (instance.RuleType == null)
			{
				throw new ArgumentNullException("RuleType", "Required by proto specification.");
			}
			stream.WriteByte(42);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.RuleType));
			stream.WriteByte(48);
			ProtocolParser.WriteBool(stream, instance.RuleIsNot);
			if (instance.HasMinValue)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.MinValue);
			}
			if (instance.HasMaxValue)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.MaxValue);
			}
			if (instance.HasTag)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Tag);
			}
			if (instance.HasTagMinValue)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.TagMinValue);
			}
			if (instance.HasTagMaxValue)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.TagMaxValue);
			}
			if (instance.HasStringValue)
			{
				stream.WriteByte(98);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.StringValue));
			}
			if (instance.TargetSubsetIds.Count > 0)
			{
				foreach (int targetSubsetId in instance.TargetSubsetIds)
				{
					stream.WriteByte(104);
					ProtocolParser.WriteUInt64(stream, (ulong)targetSubsetId);
				}
			}
			stream.WriteByte(112);
			ProtocolParser.WriteBool(stream, instance.ShowInvalidCards);
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
			num += ProtocolParser.SizeOfUInt64((ulong)Id);
			num += ProtocolParser.SizeOfUInt64((ulong)DeckRulesetId);
			if (HasAppliesToSubsetId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)AppliesToSubsetId);
			}
			if (HasAppliesToIsNot)
			{
				num++;
				num++;
			}
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(RuleType);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			num++;
			if (HasMinValue)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)MinValue);
			}
			if (HasMaxValue)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)MaxValue);
			}
			if (HasTag)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Tag);
			}
			if (HasTagMinValue)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)TagMinValue);
			}
			if (HasTagMaxValue)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)TagMaxValue);
			}
			if (HasStringValue)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(StringValue);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (TargetSubsetIds.Count > 0)
			{
				foreach (int targetSubsetId in TargetSubsetIds)
				{
					num++;
					num += ProtocolParser.SizeOfUInt64((ulong)targetSubsetId);
				}
			}
			num++;
			if (Strings.Count > 0)
			{
				foreach (LocalizedString @string in Strings)
				{
					num += 2;
					uint serializedSize = @string.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num + 5;
		}
	}
}
