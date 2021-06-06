using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.profanity.v1
{
	public class WordFilters : IProtoBuf
	{
		private List<WordFilter> _Filters = new List<WordFilter>();

		public List<WordFilter> Filters
		{
			get
			{
				return _Filters;
			}
			set
			{
				_Filters = value;
			}
		}

		public List<WordFilter> FiltersList => _Filters;

		public int FiltersCount => _Filters.Count;

		public bool IsInitialized => true;

		public void AddFilters(WordFilter val)
		{
			_Filters.Add(val);
		}

		public void ClearFilters()
		{
			_Filters.Clear();
		}

		public void SetFilters(List<WordFilter> val)
		{
			Filters = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (WordFilter filter in Filters)
			{
				num ^= filter.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			WordFilters wordFilters = obj as WordFilters;
			if (wordFilters == null)
			{
				return false;
			}
			if (Filters.Count != wordFilters.Filters.Count)
			{
				return false;
			}
			for (int i = 0; i < Filters.Count; i++)
			{
				if (!Filters[i].Equals(wordFilters.Filters[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static WordFilters ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<WordFilters>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static WordFilters Deserialize(Stream stream, WordFilters instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static WordFilters DeserializeLengthDelimited(Stream stream)
		{
			WordFilters wordFilters = new WordFilters();
			DeserializeLengthDelimited(stream, wordFilters);
			return wordFilters;
		}

		public static WordFilters DeserializeLengthDelimited(Stream stream, WordFilters instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static WordFilters Deserialize(Stream stream, WordFilters instance, long limit)
		{
			if (instance.Filters == null)
			{
				instance.Filters = new List<WordFilter>();
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
					instance.Filters.Add(WordFilter.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, WordFilters instance)
		{
			if (instance.Filters.Count <= 0)
			{
				return;
			}
			foreach (WordFilter filter in instance.Filters)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, filter.GetSerializedSize());
				WordFilter.Serialize(stream, filter);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Filters.Count > 0)
			{
				foreach (WordFilter filter in Filters)
				{
					num++;
					uint serializedSize = filter.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
