using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class DraftBeginning : IProtoBuf
	{
		public enum PacketID
		{
			ID = 246
		}

		private List<CardDef> _ChoiceList = new List<CardDef>();

		public bool HasDeprecatedWins;

		private int _DeprecatedWins;

		public bool HasCurrentSession;

		private ArenaSession _CurrentSession;

		private List<DraftSlotType> _UniqueSlotTypes = new List<DraftSlotType>();

		public long DeckId { get; set; }

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
			foreach (CardDef choice in ChoiceList)
			{
				hashCode ^= choice.GetHashCode();
			}
			if (HasDeprecatedWins)
			{
				hashCode ^= DeprecatedWins.GetHashCode();
			}
			hashCode ^= MaxSlot.GetHashCode();
			if (HasCurrentSession)
			{
				hashCode ^= CurrentSession.GetHashCode();
			}
			hashCode ^= SlotType.GetHashCode();
			foreach (DraftSlotType uniqueSlotType in UniqueSlotTypes)
			{
				hashCode ^= uniqueSlotType.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			DraftBeginning draftBeginning = obj as DraftBeginning;
			if (draftBeginning == null)
			{
				return false;
			}
			if (!DeckId.Equals(draftBeginning.DeckId))
			{
				return false;
			}
			if (ChoiceList.Count != draftBeginning.ChoiceList.Count)
			{
				return false;
			}
			for (int i = 0; i < ChoiceList.Count; i++)
			{
				if (!ChoiceList[i].Equals(draftBeginning.ChoiceList[i]))
				{
					return false;
				}
			}
			if (HasDeprecatedWins != draftBeginning.HasDeprecatedWins || (HasDeprecatedWins && !DeprecatedWins.Equals(draftBeginning.DeprecatedWins)))
			{
				return false;
			}
			if (!MaxSlot.Equals(draftBeginning.MaxSlot))
			{
				return false;
			}
			if (HasCurrentSession != draftBeginning.HasCurrentSession || (HasCurrentSession && !CurrentSession.Equals(draftBeginning.CurrentSession)))
			{
				return false;
			}
			if (!SlotType.Equals(draftBeginning.SlotType))
			{
				return false;
			}
			if (UniqueSlotTypes.Count != draftBeginning.UniqueSlotTypes.Count)
			{
				return false;
			}
			for (int j = 0; j < UniqueSlotTypes.Count; j++)
			{
				if (!UniqueSlotTypes[j].Equals(draftBeginning.UniqueSlotTypes[j]))
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

		public static DraftBeginning Deserialize(Stream stream, DraftBeginning instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DraftBeginning DeserializeLengthDelimited(Stream stream)
		{
			DraftBeginning draftBeginning = new DraftBeginning();
			DeserializeLengthDelimited(stream, draftBeginning);
			return draftBeginning;
		}

		public static DraftBeginning DeserializeLengthDelimited(Stream stream, DraftBeginning instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DraftBeginning Deserialize(Stream stream, DraftBeginning instance, long limit)
		{
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
				case 26:
					instance.ChoiceList.Add(CardDef.DeserializeLengthDelimited(stream));
					continue;
				case 32:
					instance.DeprecatedWins = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.MaxSlot = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 50:
					if (instance.CurrentSession == null)
					{
						instance.CurrentSession = ArenaSession.DeserializeLengthDelimited(stream);
					}
					else
					{
						ArenaSession.DeserializeLengthDelimited(stream, instance.CurrentSession);
					}
					continue;
				case 56:
					instance.SlotType = (DraftSlotType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 64:
					instance.UniqueSlotTypes.Add((DraftSlotType)ProtocolParser.ReadUInt64(stream));
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

		public static void Serialize(Stream stream, DraftBeginning instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
			if (instance.ChoiceList.Count > 0)
			{
				foreach (CardDef choice in instance.ChoiceList)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, choice.GetSerializedSize());
					CardDef.Serialize(stream, choice);
				}
			}
			if (instance.HasDeprecatedWins)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeprecatedWins);
			}
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.MaxSlot);
			if (instance.HasCurrentSession)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.CurrentSession.GetSerializedSize());
				ArenaSession.Serialize(stream, instance.CurrentSession);
			}
			stream.WriteByte(56);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.SlotType);
			if (instance.UniqueSlotTypes.Count <= 0)
			{
				return;
			}
			foreach (DraftSlotType uniqueSlotType in instance.UniqueSlotTypes)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)uniqueSlotType);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)DeckId);
			if (ChoiceList.Count > 0)
			{
				foreach (CardDef choice in ChoiceList)
				{
					num++;
					uint serializedSize = choice.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasDeprecatedWins)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DeprecatedWins);
			}
			num += ProtocolParser.SizeOfUInt64((ulong)MaxSlot);
			if (HasCurrentSession)
			{
				num++;
				uint serializedSize2 = CurrentSession.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			num += ProtocolParser.SizeOfUInt64((ulong)SlotType);
			if (UniqueSlotTypes.Count > 0)
			{
				foreach (DraftSlotType uniqueSlotType in UniqueSlotTypes)
				{
					num++;
					num += ProtocolParser.SizeOfUInt64((ulong)uniqueSlotType);
				}
			}
			return num + 3;
		}
	}
}
