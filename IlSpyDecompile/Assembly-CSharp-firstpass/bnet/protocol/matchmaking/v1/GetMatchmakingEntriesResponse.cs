using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class GetMatchmakingEntriesResponse : IProtoBuf
	{
		private List<GameMatchmakingEntry> _Entry = new List<GameMatchmakingEntry>();

		public List<GameMatchmakingEntry> Entry
		{
			get
			{
				return _Entry;
			}
			set
			{
				_Entry = value;
			}
		}

		public List<GameMatchmakingEntry> EntryList => _Entry;

		public int EntryCount => _Entry.Count;

		public bool IsInitialized => true;

		public void AddEntry(GameMatchmakingEntry val)
		{
			_Entry.Add(val);
		}

		public void ClearEntry()
		{
			_Entry.Clear();
		}

		public void SetEntry(List<GameMatchmakingEntry> val)
		{
			Entry = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (GameMatchmakingEntry item in Entry)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetMatchmakingEntriesResponse getMatchmakingEntriesResponse = obj as GetMatchmakingEntriesResponse;
			if (getMatchmakingEntriesResponse == null)
			{
				return false;
			}
			if (Entry.Count != getMatchmakingEntriesResponse.Entry.Count)
			{
				return false;
			}
			for (int i = 0; i < Entry.Count; i++)
			{
				if (!Entry[i].Equals(getMatchmakingEntriesResponse.Entry[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static GetMatchmakingEntriesResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetMatchmakingEntriesResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetMatchmakingEntriesResponse Deserialize(Stream stream, GetMatchmakingEntriesResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetMatchmakingEntriesResponse DeserializeLengthDelimited(Stream stream)
		{
			GetMatchmakingEntriesResponse getMatchmakingEntriesResponse = new GetMatchmakingEntriesResponse();
			DeserializeLengthDelimited(stream, getMatchmakingEntriesResponse);
			return getMatchmakingEntriesResponse;
		}

		public static GetMatchmakingEntriesResponse DeserializeLengthDelimited(Stream stream, GetMatchmakingEntriesResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetMatchmakingEntriesResponse Deserialize(Stream stream, GetMatchmakingEntriesResponse instance, long limit)
		{
			if (instance.Entry == null)
			{
				instance.Entry = new List<GameMatchmakingEntry>();
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
					instance.Entry.Add(GameMatchmakingEntry.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, GetMatchmakingEntriesResponse instance)
		{
			if (instance.Entry.Count <= 0)
			{
				return;
			}
			foreach (GameMatchmakingEntry item in instance.Entry)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				GameMatchmakingEntry.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Entry.Count > 0)
			{
				foreach (GameMatchmakingEntry item in Entry)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
