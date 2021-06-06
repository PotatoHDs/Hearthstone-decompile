using System.Collections.Generic;
using System.IO;

namespace PegasusShared
{
	public class DeckRulesetDbRecord : IProtoBuf
	{
		private List<DeckRulesetRuleDbRecord> _Rules = new List<DeckRulesetRuleDbRecord>();

		public int Id { get; set; }

		public List<DeckRulesetRuleDbRecord> Rules
		{
			get
			{
				return _Rules;
			}
			set
			{
				_Rules = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Id.GetHashCode();
			foreach (DeckRulesetRuleDbRecord rule in Rules)
			{
				hashCode ^= rule.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			DeckRulesetDbRecord deckRulesetDbRecord = obj as DeckRulesetDbRecord;
			if (deckRulesetDbRecord == null)
			{
				return false;
			}
			if (!Id.Equals(deckRulesetDbRecord.Id))
			{
				return false;
			}
			if (Rules.Count != deckRulesetDbRecord.Rules.Count)
			{
				return false;
			}
			for (int i = 0; i < Rules.Count; i++)
			{
				if (!Rules[i].Equals(deckRulesetDbRecord.Rules[i]))
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

		public static DeckRulesetDbRecord Deserialize(Stream stream, DeckRulesetDbRecord instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DeckRulesetDbRecord DeserializeLengthDelimited(Stream stream)
		{
			DeckRulesetDbRecord deckRulesetDbRecord = new DeckRulesetDbRecord();
			DeserializeLengthDelimited(stream, deckRulesetDbRecord);
			return deckRulesetDbRecord;
		}

		public static DeckRulesetDbRecord DeserializeLengthDelimited(Stream stream, DeckRulesetDbRecord instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DeckRulesetDbRecord Deserialize(Stream stream, DeckRulesetDbRecord instance, long limit)
		{
			if (instance.Rules == null)
			{
				instance.Rules = new List<DeckRulesetRuleDbRecord>();
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
				case 18:
					instance.Rules.Add(DeckRulesetRuleDbRecord.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, DeckRulesetDbRecord instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Id);
			if (instance.Rules.Count <= 0)
			{
				return;
			}
			foreach (DeckRulesetRuleDbRecord rule in instance.Rules)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, rule.GetSerializedSize());
				DeckRulesetRuleDbRecord.Serialize(stream, rule);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Id);
			if (Rules.Count > 0)
			{
				foreach (DeckRulesetRuleDbRecord rule in Rules)
				{
					num++;
					uint serializedSize = rule.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num + 1;
		}
	}
}
