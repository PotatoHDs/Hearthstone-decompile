using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class DraftChosen : IProtoBuf
	{
		public enum PacketID
		{
			ID = 249
		}

		private List<CardDef> _NextChoiceList = new List<CardDef>();

		public CardDef Chosen { get; set; }

		public List<CardDef> NextChoiceList
		{
			get
			{
				return _NextChoiceList;
			}
			set
			{
				_NextChoiceList = value;
			}
		}

		public DraftSlotType SlotType { get; set; }

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Chosen.GetHashCode();
			foreach (CardDef nextChoice in NextChoiceList)
			{
				hashCode ^= nextChoice.GetHashCode();
			}
			return hashCode ^ SlotType.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			DraftChosen draftChosen = obj as DraftChosen;
			if (draftChosen == null)
			{
				return false;
			}
			if (!Chosen.Equals(draftChosen.Chosen))
			{
				return false;
			}
			if (NextChoiceList.Count != draftChosen.NextChoiceList.Count)
			{
				return false;
			}
			for (int i = 0; i < NextChoiceList.Count; i++)
			{
				if (!NextChoiceList[i].Equals(draftChosen.NextChoiceList[i]))
				{
					return false;
				}
			}
			if (!SlotType.Equals(draftChosen.SlotType))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DraftChosen Deserialize(Stream stream, DraftChosen instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DraftChosen DeserializeLengthDelimited(Stream stream)
		{
			DraftChosen draftChosen = new DraftChosen();
			DeserializeLengthDelimited(stream, draftChosen);
			return draftChosen;
		}

		public static DraftChosen DeserializeLengthDelimited(Stream stream, DraftChosen instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DraftChosen Deserialize(Stream stream, DraftChosen instance, long limit)
		{
			if (instance.NextChoiceList == null)
			{
				instance.NextChoiceList = new List<CardDef>();
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
				case 26:
					if (instance.Chosen == null)
					{
						instance.Chosen = CardDef.DeserializeLengthDelimited(stream);
					}
					else
					{
						CardDef.DeserializeLengthDelimited(stream, instance.Chosen);
					}
					continue;
				case 34:
					instance.NextChoiceList.Add(CardDef.DeserializeLengthDelimited(stream));
					continue;
				case 40:
					instance.SlotType = (DraftSlotType)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, DraftChosen instance)
		{
			if (instance.Chosen == null)
			{
				throw new ArgumentNullException("Chosen", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteUInt32(stream, instance.Chosen.GetSerializedSize());
			CardDef.Serialize(stream, instance.Chosen);
			if (instance.NextChoiceList.Count > 0)
			{
				foreach (CardDef nextChoice in instance.NextChoiceList)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, nextChoice.GetSerializedSize());
					CardDef.Serialize(stream, nextChoice);
				}
			}
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.SlotType);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = Chosen.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (NextChoiceList.Count > 0)
			{
				foreach (CardDef nextChoice in NextChoiceList)
				{
					num++;
					uint serializedSize2 = nextChoice.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			num += ProtocolParser.SizeOfUInt64((ulong)SlotType);
			return num + 2;
		}
	}
}
