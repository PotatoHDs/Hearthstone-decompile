using System.Collections.Generic;
using System.IO;

namespace SpectatorProto
{
	public class PartyQuestInfo : IProtoBuf
	{
		private List<int> _QuestIds = new List<int>();

		public List<int> QuestIds
		{
			get
			{
				return _QuestIds;
			}
			set
			{
				_QuestIds = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (int questId in QuestIds)
			{
				num ^= questId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PartyQuestInfo partyQuestInfo = obj as PartyQuestInfo;
			if (partyQuestInfo == null)
			{
				return false;
			}
			if (QuestIds.Count != partyQuestInfo.QuestIds.Count)
			{
				return false;
			}
			for (int i = 0; i < QuestIds.Count; i++)
			{
				if (!QuestIds[i].Equals(partyQuestInfo.QuestIds[i]))
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

		public static PartyQuestInfo Deserialize(Stream stream, PartyQuestInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PartyQuestInfo DeserializeLengthDelimited(Stream stream)
		{
			PartyQuestInfo partyQuestInfo = new PartyQuestInfo();
			DeserializeLengthDelimited(stream, partyQuestInfo);
			return partyQuestInfo;
		}

		public static PartyQuestInfo DeserializeLengthDelimited(Stream stream, PartyQuestInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PartyQuestInfo Deserialize(Stream stream, PartyQuestInfo instance, long limit)
		{
			if (instance.QuestIds == null)
			{
				instance.QuestIds = new List<int>();
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
					instance.QuestIds.Add((int)ProtocolParser.ReadUInt64(stream));
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

		public static void Serialize(Stream stream, PartyQuestInfo instance)
		{
			if (instance.QuestIds.Count <= 0)
			{
				return;
			}
			foreach (int questId in instance.QuestIds)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)questId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (QuestIds.Count > 0)
			{
				foreach (int questId in QuestIds)
				{
					num++;
					num += ProtocolParser.SizeOfUInt64((ulong)questId);
				}
				return num;
			}
			return num;
		}
	}
}
