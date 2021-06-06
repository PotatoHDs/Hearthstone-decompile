using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class DraftChoicesAndContents : IProtoBuf
	{
		public enum PacketID
		{
			ID = 248
		}

		private List<DeckCardData> _Cards = new List<DeckCardData>();

		public bool HasDeprecatedWins;

		private int _DeprecatedWins;

		public bool HasDeprecatedLosses;

		private int _DeprecatedLosses;

		public bool HasChest;

		private RewardChest _Chest;

		private List<CardDef> _ChoiceList = new List<CardDef>();

		public bool HasMaxWins;

		private int _MaxWins;

		public bool HasCurrentSession;

		private ArenaSession _CurrentSession;

		public bool HasHeroPowerDef;

		private CardDef _HeroPowerDef;

		private List<DraftSlotType> _UniqueSlotTypes = new List<DraftSlotType>();

		public long DeckId { get; set; }

		public int Slot { get; set; }

		public List<DeckCardData> Cards
		{
			get
			{
				return _Cards;
			}
			set
			{
				_Cards = value;
			}
		}

		public int DeprecatedWins
		{
			get
			{
				return _DeprecatedWins;
			}
			set
			{
				_DeprecatedWins = value;
				HasDeprecatedWins = true;
			}
		}

		public int DeprecatedLosses
		{
			get
			{
				return _DeprecatedLosses;
			}
			set
			{
				_DeprecatedLosses = value;
				HasDeprecatedLosses = true;
			}
		}

		public RewardChest Chest
		{
			get
			{
				return _Chest;
			}
			set
			{
				_Chest = value;
				HasChest = value != null;
			}
		}

		public List<CardDef> ChoiceList
		{
			get
			{
				return _ChoiceList;
			}
			set
			{
				_ChoiceList = value;
			}
		}

		public CardDef HeroDef { get; set; }

		public int MaxWins
		{
			get
			{
				return _MaxWins;
			}
			set
			{
				_MaxWins = value;
				HasMaxWins = true;
			}
		}

		public int MaxSlot { get; set; }

		public ArenaSession CurrentSession
		{
			get
			{
				return _CurrentSession;
			}
			set
			{
				_CurrentSession = value;
				HasCurrentSession = value != null;
			}
		}

		public DraftSlotType SlotType { get; set; }

		public CardDef HeroPowerDef
		{
			get
			{
				return _HeroPowerDef;
			}
			set
			{
				_HeroPowerDef = value;
				HasHeroPowerDef = value != null;
			}
		}

		public List<DraftSlotType> UniqueSlotTypes
		{
			get
			{
				return _UniqueSlotTypes;
			}
			set
			{
				_UniqueSlotTypes = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= DeckId.GetHashCode();
			hashCode ^= Slot.GetHashCode();
			foreach (DeckCardData card in Cards)
			{
				hashCode ^= card.GetHashCode();
			}
			if (HasDeprecatedWins)
			{
				hashCode ^= DeprecatedWins.GetHashCode();
			}
			if (HasDeprecatedLosses)
			{
				hashCode ^= DeprecatedLosses.GetHashCode();
			}
			if (HasChest)
			{
				hashCode ^= Chest.GetHashCode();
			}
			foreach (CardDef choice in ChoiceList)
			{
				hashCode ^= choice.GetHashCode();
			}
			hashCode ^= HeroDef.GetHashCode();
			if (HasMaxWins)
			{
				hashCode ^= MaxWins.GetHashCode();
			}
			hashCode ^= MaxSlot.GetHashCode();
			if (HasCurrentSession)
			{
				hashCode ^= CurrentSession.GetHashCode();
			}
			hashCode ^= SlotType.GetHashCode();
			if (HasHeroPowerDef)
			{
				hashCode ^= HeroPowerDef.GetHashCode();
			}
			foreach (DraftSlotType uniqueSlotType in UniqueSlotTypes)
			{
				hashCode ^= uniqueSlotType.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			DraftChoicesAndContents draftChoicesAndContents = obj as DraftChoicesAndContents;
			if (draftChoicesAndContents == null)
			{
				return false;
			}
			if (!DeckId.Equals(draftChoicesAndContents.DeckId))
			{
				return false;
			}
			if (!Slot.Equals(draftChoicesAndContents.Slot))
			{
				return false;
			}
			if (Cards.Count != draftChoicesAndContents.Cards.Count)
			{
				return false;
			}
			for (int i = 0; i < Cards.Count; i++)
			{
				if (!Cards[i].Equals(draftChoicesAndContents.Cards[i]))
				{
					return false;
				}
			}
			if (HasDeprecatedWins != draftChoicesAndContents.HasDeprecatedWins || (HasDeprecatedWins && !DeprecatedWins.Equals(draftChoicesAndContents.DeprecatedWins)))
			{
				return false;
			}
			if (HasDeprecatedLosses != draftChoicesAndContents.HasDeprecatedLosses || (HasDeprecatedLosses && !DeprecatedLosses.Equals(draftChoicesAndContents.DeprecatedLosses)))
			{
				return false;
			}
			if (HasChest != draftChoicesAndContents.HasChest || (HasChest && !Chest.Equals(draftChoicesAndContents.Chest)))
			{
				return false;
			}
			if (ChoiceList.Count != draftChoicesAndContents.ChoiceList.Count)
			{
				return false;
			}
			for (int j = 0; j < ChoiceList.Count; j++)
			{
				if (!ChoiceList[j].Equals(draftChoicesAndContents.ChoiceList[j]))
				{
					return false;
				}
			}
			if (!HeroDef.Equals(draftChoicesAndContents.HeroDef))
			{
				return false;
			}
			if (HasMaxWins != draftChoicesAndContents.HasMaxWins || (HasMaxWins && !MaxWins.Equals(draftChoicesAndContents.MaxWins)))
			{
				return false;
			}
			if (!MaxSlot.Equals(draftChoicesAndContents.MaxSlot))
			{
				return false;
			}
			if (HasCurrentSession != draftChoicesAndContents.HasCurrentSession || (HasCurrentSession && !CurrentSession.Equals(draftChoicesAndContents.CurrentSession)))
			{
				return false;
			}
			if (!SlotType.Equals(draftChoicesAndContents.SlotType))
			{
				return false;
			}
			if (HasHeroPowerDef != draftChoicesAndContents.HasHeroPowerDef || (HasHeroPowerDef && !HeroPowerDef.Equals(draftChoicesAndContents.HeroPowerDef)))
			{
				return false;
			}
			if (UniqueSlotTypes.Count != draftChoicesAndContents.UniqueSlotTypes.Count)
			{
				return false;
			}
			for (int k = 0; k < UniqueSlotTypes.Count; k++)
			{
				if (!UniqueSlotTypes[k].Equals(draftChoicesAndContents.UniqueSlotTypes[k]))
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

		public static DraftChoicesAndContents Deserialize(Stream stream, DraftChoicesAndContents instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DraftChoicesAndContents DeserializeLengthDelimited(Stream stream)
		{
			DraftChoicesAndContents draftChoicesAndContents = new DraftChoicesAndContents();
			DeserializeLengthDelimited(stream, draftChoicesAndContents);
			return draftChoicesAndContents;
		}

		public static DraftChoicesAndContents DeserializeLengthDelimited(Stream stream, DraftChoicesAndContents instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DraftChoicesAndContents Deserialize(Stream stream, DraftChoicesAndContents instance, long limit)
		{
			if (instance.Cards == null)
			{
				instance.Cards = new List<DeckCardData>();
			}
			if (instance.ChoiceList == null)
			{
				instance.ChoiceList = new List<CardDef>();
			}
			if (instance.UniqueSlotTypes == null)
			{
				instance.UniqueSlotTypes = new List<DraftSlotType>();
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
					instance.DeckId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Slot = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 42:
					instance.Cards.Add(DeckCardData.DeserializeLengthDelimited(stream));
					continue;
				case 48:
					instance.DeprecatedWins = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 56:
					instance.DeprecatedLosses = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 66:
					if (instance.Chest == null)
					{
						instance.Chest = RewardChest.DeserializeLengthDelimited(stream);
					}
					else
					{
						RewardChest.DeserializeLengthDelimited(stream, instance.Chest);
					}
					continue;
				case 74:
					instance.ChoiceList.Add(CardDef.DeserializeLengthDelimited(stream));
					continue;
				case 82:
					if (instance.HeroDef == null)
					{
						instance.HeroDef = CardDef.DeserializeLengthDelimited(stream);
					}
					else
					{
						CardDef.DeserializeLengthDelimited(stream, instance.HeroDef);
					}
					continue;
				case 88:
					instance.MaxWins = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 96:
					instance.MaxSlot = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 106:
					if (instance.CurrentSession == null)
					{
						instance.CurrentSession = ArenaSession.DeserializeLengthDelimited(stream);
					}
					else
					{
						ArenaSession.DeserializeLengthDelimited(stream, instance.CurrentSession);
					}
					continue;
				case 112:
					instance.SlotType = (DraftSlotType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 122:
					if (instance.HeroPowerDef == null)
					{
						instance.HeroPowerDef = CardDef.DeserializeLengthDelimited(stream);
					}
					else
					{
						CardDef.DeserializeLengthDelimited(stream, instance.HeroPowerDef);
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
						if (key.WireType == Wire.Varint)
						{
							instance.UniqueSlotTypes.Add((DraftSlotType)ProtocolParser.ReadUInt64(stream));
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

		public static void Serialize(Stream stream, DraftChoicesAndContents instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Slot);
			if (instance.Cards.Count > 0)
			{
				foreach (DeckCardData card in instance.Cards)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, card.GetSerializedSize());
					DeckCardData.Serialize(stream, card);
				}
			}
			if (instance.HasDeprecatedWins)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeprecatedWins);
			}
			if (instance.HasDeprecatedLosses)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeprecatedLosses);
			}
			if (instance.HasChest)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteUInt32(stream, instance.Chest.GetSerializedSize());
				RewardChest.Serialize(stream, instance.Chest);
			}
			if (instance.ChoiceList.Count > 0)
			{
				foreach (CardDef choice in instance.ChoiceList)
				{
					stream.WriteByte(74);
					ProtocolParser.WriteUInt32(stream, choice.GetSerializedSize());
					CardDef.Serialize(stream, choice);
				}
			}
			if (instance.HeroDef == null)
			{
				throw new ArgumentNullException("HeroDef", "Required by proto specification.");
			}
			stream.WriteByte(82);
			ProtocolParser.WriteUInt32(stream, instance.HeroDef.GetSerializedSize());
			CardDef.Serialize(stream, instance.HeroDef);
			if (instance.HasMaxWins)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.MaxWins);
			}
			stream.WriteByte(96);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.MaxSlot);
			if (instance.HasCurrentSession)
			{
				stream.WriteByte(106);
				ProtocolParser.WriteUInt32(stream, instance.CurrentSession.GetSerializedSize());
				ArenaSession.Serialize(stream, instance.CurrentSession);
			}
			stream.WriteByte(112);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.SlotType);
			if (instance.HasHeroPowerDef)
			{
				stream.WriteByte(122);
				ProtocolParser.WriteUInt32(stream, instance.HeroPowerDef.GetSerializedSize());
				CardDef.Serialize(stream, instance.HeroPowerDef);
			}
			if (instance.UniqueSlotTypes.Count <= 0)
			{
				return;
			}
			foreach (DraftSlotType uniqueSlotType in instance.UniqueSlotTypes)
			{
				stream.WriteByte(128);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)uniqueSlotType);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)DeckId);
			num += ProtocolParser.SizeOfUInt64((ulong)Slot);
			if (Cards.Count > 0)
			{
				foreach (DeckCardData card in Cards)
				{
					num++;
					uint serializedSize = card.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasDeprecatedWins)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DeprecatedWins);
			}
			if (HasDeprecatedLosses)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DeprecatedLosses);
			}
			if (HasChest)
			{
				num++;
				uint serializedSize2 = Chest.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (ChoiceList.Count > 0)
			{
				foreach (CardDef choice in ChoiceList)
				{
					num++;
					uint serializedSize3 = choice.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			uint serializedSize4 = HeroDef.GetSerializedSize();
			num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			if (HasMaxWins)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)MaxWins);
			}
			num += ProtocolParser.SizeOfUInt64((ulong)MaxSlot);
			if (HasCurrentSession)
			{
				num++;
				uint serializedSize5 = CurrentSession.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			num += ProtocolParser.SizeOfUInt64((ulong)SlotType);
			if (HasHeroPowerDef)
			{
				num++;
				uint serializedSize6 = HeroPowerDef.GetSerializedSize();
				num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
			}
			if (UniqueSlotTypes.Count > 0)
			{
				foreach (DraftSlotType uniqueSlotType in UniqueSlotTypes)
				{
					num += 2;
					num += ProtocolParser.SizeOfUInt64((ulong)uniqueSlotType);
				}
			}
			return num + 5;
		}
	}
}
