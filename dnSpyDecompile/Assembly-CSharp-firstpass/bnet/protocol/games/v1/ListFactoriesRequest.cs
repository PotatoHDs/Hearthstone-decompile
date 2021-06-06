using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000373 RID: 883
	public class ListFactoriesRequest : IProtoBuf
	{
		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06003810 RID: 14352 RVA: 0x000B7D4F File Offset: 0x000B5F4F
		// (set) Token: 0x06003811 RID: 14353 RVA: 0x000B7D57 File Offset: 0x000B5F57
		public AttributeFilter Filter { get; set; }

		// Token: 0x06003812 RID: 14354 RVA: 0x000B7D60 File Offset: 0x000B5F60
		public void SetFilter(AttributeFilter val)
		{
			this.Filter = val;
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06003813 RID: 14355 RVA: 0x000B7D69 File Offset: 0x000B5F69
		// (set) Token: 0x06003814 RID: 14356 RVA: 0x000B7D71 File Offset: 0x000B5F71
		public uint StartIndex
		{
			get
			{
				return this._StartIndex;
			}
			set
			{
				this._StartIndex = value;
				this.HasStartIndex = true;
			}
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x000B7D81 File Offset: 0x000B5F81
		public void SetStartIndex(uint val)
		{
			this.StartIndex = val;
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06003816 RID: 14358 RVA: 0x000B7D8A File Offset: 0x000B5F8A
		// (set) Token: 0x06003817 RID: 14359 RVA: 0x000B7D92 File Offset: 0x000B5F92
		public uint MaxResults
		{
			get
			{
				return this._MaxResults;
			}
			set
			{
				this._MaxResults = value;
				this.HasMaxResults = true;
			}
		}

		// Token: 0x06003818 RID: 14360 RVA: 0x000B7DA2 File Offset: 0x000B5FA2
		public void SetMaxResults(uint val)
		{
			this.MaxResults = val;
		}

		// Token: 0x06003819 RID: 14361 RVA: 0x000B7DAC File Offset: 0x000B5FAC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Filter.GetHashCode();
			if (this.HasStartIndex)
			{
				num ^= this.StartIndex.GetHashCode();
			}
			if (this.HasMaxResults)
			{
				num ^= this.MaxResults.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600381A RID: 14362 RVA: 0x000B7E08 File Offset: 0x000B6008
		public override bool Equals(object obj)
		{
			ListFactoriesRequest listFactoriesRequest = obj as ListFactoriesRequest;
			return listFactoriesRequest != null && this.Filter.Equals(listFactoriesRequest.Filter) && this.HasStartIndex == listFactoriesRequest.HasStartIndex && (!this.HasStartIndex || this.StartIndex.Equals(listFactoriesRequest.StartIndex)) && this.HasMaxResults == listFactoriesRequest.HasMaxResults && (!this.HasMaxResults || this.MaxResults.Equals(listFactoriesRequest.MaxResults));
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x0600381B RID: 14363 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600381C RID: 14364 RVA: 0x000B7E93 File Offset: 0x000B6093
		public static ListFactoriesRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ListFactoriesRequest>(bs, 0, -1);
		}

		// Token: 0x0600381D RID: 14365 RVA: 0x000B7E9D File Offset: 0x000B609D
		public void Deserialize(Stream stream)
		{
			ListFactoriesRequest.Deserialize(stream, this);
		}

		// Token: 0x0600381E RID: 14366 RVA: 0x000B7EA7 File Offset: 0x000B60A7
		public static ListFactoriesRequest Deserialize(Stream stream, ListFactoriesRequest instance)
		{
			return ListFactoriesRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600381F RID: 14367 RVA: 0x000B7EB4 File Offset: 0x000B60B4
		public static ListFactoriesRequest DeserializeLengthDelimited(Stream stream)
		{
			ListFactoriesRequest listFactoriesRequest = new ListFactoriesRequest();
			ListFactoriesRequest.DeserializeLengthDelimited(stream, listFactoriesRequest);
			return listFactoriesRequest;
		}

		// Token: 0x06003820 RID: 14368 RVA: 0x000B7ED0 File Offset: 0x000B60D0
		public static ListFactoriesRequest DeserializeLengthDelimited(Stream stream, ListFactoriesRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ListFactoriesRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003821 RID: 14369 RVA: 0x000B7EF8 File Offset: 0x000B60F8
		public static ListFactoriesRequest Deserialize(Stream stream, ListFactoriesRequest instance, long limit)
		{
			instance.StartIndex = 0U;
			instance.MaxResults = 100U;
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
				else if (num != 10)
				{
					if (num != 16)
					{
						if (num != 24)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else
						{
							instance.MaxResults = ProtocolParser.ReadUInt32(stream);
						}
					}
					else
					{
						instance.StartIndex = ProtocolParser.ReadUInt32(stream);
					}
				}
				else if (instance.Filter == null)
				{
					instance.Filter = AttributeFilter.DeserializeLengthDelimited(stream);
				}
				else
				{
					AttributeFilter.DeserializeLengthDelimited(stream, instance.Filter);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003822 RID: 14370 RVA: 0x000B7FD5 File Offset: 0x000B61D5
		public void Serialize(Stream stream)
		{
			ListFactoriesRequest.Serialize(stream, this);
		}

		// Token: 0x06003823 RID: 14371 RVA: 0x000B7FE0 File Offset: 0x000B61E0
		public static void Serialize(Stream stream, ListFactoriesRequest instance)
		{
			if (instance.Filter == null)
			{
				throw new ArgumentNullException("Filter", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Filter.GetSerializedSize());
			AttributeFilter.Serialize(stream, instance.Filter);
			if (instance.HasStartIndex)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.StartIndex);
			}
			if (instance.HasMaxResults)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.MaxResults);
			}
		}

		// Token: 0x06003824 RID: 14372 RVA: 0x000B8064 File Offset: 0x000B6264
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Filter.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasStartIndex)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.StartIndex);
			}
			if (this.HasMaxResults)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.MaxResults);
			}
			return num + 1U;
		}

		// Token: 0x040014E6 RID: 5350
		public bool HasStartIndex;

		// Token: 0x040014E7 RID: 5351
		private uint _StartIndex;

		// Token: 0x040014E8 RID: 5352
		public bool HasMaxResults;

		// Token: 0x040014E9 RID: 5353
		private uint _MaxResults;
	}
}
