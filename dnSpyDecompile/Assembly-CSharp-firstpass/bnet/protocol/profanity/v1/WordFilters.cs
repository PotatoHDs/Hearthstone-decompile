using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.profanity.v1
{
	// Token: 0x02000332 RID: 818
	public class WordFilters : IProtoBuf
	{
		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06003249 RID: 12873 RVA: 0x000A8668 File Offset: 0x000A6868
		// (set) Token: 0x0600324A RID: 12874 RVA: 0x000A8670 File Offset: 0x000A6870
		public List<WordFilter> Filters
		{
			get
			{
				return this._Filters;
			}
			set
			{
				this._Filters = value;
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x0600324B RID: 12875 RVA: 0x000A8668 File Offset: 0x000A6868
		public List<WordFilter> FiltersList
		{
			get
			{
				return this._Filters;
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x0600324C RID: 12876 RVA: 0x000A8679 File Offset: 0x000A6879
		public int FiltersCount
		{
			get
			{
				return this._Filters.Count;
			}
		}

		// Token: 0x0600324D RID: 12877 RVA: 0x000A8686 File Offset: 0x000A6886
		public void AddFilters(WordFilter val)
		{
			this._Filters.Add(val);
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x000A8694 File Offset: 0x000A6894
		public void ClearFilters()
		{
			this._Filters.Clear();
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x000A86A1 File Offset: 0x000A68A1
		public void SetFilters(List<WordFilter> val)
		{
			this.Filters = val;
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x000A86AC File Offset: 0x000A68AC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (WordFilter wordFilter in this.Filters)
			{
				num ^= wordFilter.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x000A8710 File Offset: 0x000A6910
		public override bool Equals(object obj)
		{
			WordFilters wordFilters = obj as WordFilters;
			if (wordFilters == null)
			{
				return false;
			}
			if (this.Filters.Count != wordFilters.Filters.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Filters.Count; i++)
			{
				if (!this.Filters[i].Equals(wordFilters.Filters[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06003252 RID: 12882 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003253 RID: 12883 RVA: 0x000A877B File Offset: 0x000A697B
		public static WordFilters ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<WordFilters>(bs, 0, -1);
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x000A8785 File Offset: 0x000A6985
		public void Deserialize(Stream stream)
		{
			WordFilters.Deserialize(stream, this);
		}

		// Token: 0x06003255 RID: 12885 RVA: 0x000A878F File Offset: 0x000A698F
		public static WordFilters Deserialize(Stream stream, WordFilters instance)
		{
			return WordFilters.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003256 RID: 12886 RVA: 0x000A879C File Offset: 0x000A699C
		public static WordFilters DeserializeLengthDelimited(Stream stream)
		{
			WordFilters wordFilters = new WordFilters();
			WordFilters.DeserializeLengthDelimited(stream, wordFilters);
			return wordFilters;
		}

		// Token: 0x06003257 RID: 12887 RVA: 0x000A87B8 File Offset: 0x000A69B8
		public static WordFilters DeserializeLengthDelimited(Stream stream, WordFilters instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return WordFilters.Deserialize(stream, instance, num);
		}

		// Token: 0x06003258 RID: 12888 RVA: 0x000A87E0 File Offset: 0x000A69E0
		public static WordFilters Deserialize(Stream stream, WordFilters instance, long limit)
		{
			if (instance.Filters == null)
			{
				instance.Filters = new List<WordFilter>();
			}
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else if (num == 10)
				{
					instance.Filters.Add(WordFilter.DeserializeLengthDelimited(stream));
				}
				else
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003259 RID: 12889 RVA: 0x000A8878 File Offset: 0x000A6A78
		public void Serialize(Stream stream)
		{
			WordFilters.Serialize(stream, this);
		}

		// Token: 0x0600325A RID: 12890 RVA: 0x000A8884 File Offset: 0x000A6A84
		public static void Serialize(Stream stream, WordFilters instance)
		{
			if (instance.Filters.Count > 0)
			{
				foreach (WordFilter wordFilter in instance.Filters)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, wordFilter.GetSerializedSize());
					WordFilter.Serialize(stream, wordFilter);
				}
			}
		}

		// Token: 0x0600325B RID: 12891 RVA: 0x000A88FC File Offset: 0x000A6AFC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Filters.Count > 0)
			{
				foreach (WordFilter wordFilter in this.Filters)
				{
					num += 1U;
					uint serializedSize = wordFilter.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x040013D5 RID: 5077
		private List<WordFilter> _Filters = new List<WordFilter>();
	}
}
