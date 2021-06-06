using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000390 RID: 912
	public class GetGameStatsBucketsRequest : IProtoBuf
	{
		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06003A62 RID: 14946 RVA: 0x000BD4EB File Offset: 0x000BB6EB
		// (set) Token: 0x06003A63 RID: 14947 RVA: 0x000BD4F3 File Offset: 0x000BB6F3
		public AttributeFilter FactoryFilter
		{
			get
			{
				return this._FactoryFilter;
			}
			set
			{
				this._FactoryFilter = value;
				this.HasFactoryFilter = (value != null);
			}
		}

		// Token: 0x06003A64 RID: 14948 RVA: 0x000BD506 File Offset: 0x000BB706
		public void SetFactoryFilter(AttributeFilter val)
		{
			this.FactoryFilter = val;
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06003A65 RID: 14949 RVA: 0x000BD50F File Offset: 0x000BB70F
		// (set) Token: 0x06003A66 RID: 14950 RVA: 0x000BD517 File Offset: 0x000BB717
		public AttributeFilter GameFilter
		{
			get
			{
				return this._GameFilter;
			}
			set
			{
				this._GameFilter = value;
				this.HasGameFilter = (value != null);
			}
		}

		// Token: 0x06003A67 RID: 14951 RVA: 0x000BD52A File Offset: 0x000BB72A
		public void SetGameFilter(AttributeFilter val)
		{
			this.GameFilter = val;
		}

		// Token: 0x06003A68 RID: 14952 RVA: 0x000BD534 File Offset: 0x000BB734
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasFactoryFilter)
			{
				num ^= this.FactoryFilter.GetHashCode();
			}
			if (this.HasGameFilter)
			{
				num ^= this.GameFilter.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003A69 RID: 14953 RVA: 0x000BD57C File Offset: 0x000BB77C
		public override bool Equals(object obj)
		{
			GetGameStatsBucketsRequest getGameStatsBucketsRequest = obj as GetGameStatsBucketsRequest;
			return getGameStatsBucketsRequest != null && this.HasFactoryFilter == getGameStatsBucketsRequest.HasFactoryFilter && (!this.HasFactoryFilter || this.FactoryFilter.Equals(getGameStatsBucketsRequest.FactoryFilter)) && this.HasGameFilter == getGameStatsBucketsRequest.HasGameFilter && (!this.HasGameFilter || this.GameFilter.Equals(getGameStatsBucketsRequest.GameFilter));
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06003A6A RID: 14954 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003A6B RID: 14955 RVA: 0x000BD5EC File Offset: 0x000BB7EC
		public static GetGameStatsBucketsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGameStatsBucketsRequest>(bs, 0, -1);
		}

		// Token: 0x06003A6C RID: 14956 RVA: 0x000BD5F6 File Offset: 0x000BB7F6
		public void Deserialize(Stream stream)
		{
			GetGameStatsBucketsRequest.Deserialize(stream, this);
		}

		// Token: 0x06003A6D RID: 14957 RVA: 0x000BD600 File Offset: 0x000BB800
		public static GetGameStatsBucketsRequest Deserialize(Stream stream, GetGameStatsBucketsRequest instance)
		{
			return GetGameStatsBucketsRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003A6E RID: 14958 RVA: 0x000BD60C File Offset: 0x000BB80C
		public static GetGameStatsBucketsRequest DeserializeLengthDelimited(Stream stream)
		{
			GetGameStatsBucketsRequest getGameStatsBucketsRequest = new GetGameStatsBucketsRequest();
			GetGameStatsBucketsRequest.DeserializeLengthDelimited(stream, getGameStatsBucketsRequest);
			return getGameStatsBucketsRequest;
		}

		// Token: 0x06003A6F RID: 14959 RVA: 0x000BD628 File Offset: 0x000BB828
		public static GetGameStatsBucketsRequest DeserializeLengthDelimited(Stream stream, GetGameStatsBucketsRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetGameStatsBucketsRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003A70 RID: 14960 RVA: 0x000BD650 File Offset: 0x000BB850
		public static GetGameStatsBucketsRequest Deserialize(Stream stream, GetGameStatsBucketsRequest instance, long limit)
		{
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
					if (num != 18)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.GameFilter == null)
					{
						instance.GameFilter = AttributeFilter.DeserializeLengthDelimited(stream);
					}
					else
					{
						AttributeFilter.DeserializeLengthDelimited(stream, instance.GameFilter);
					}
				}
				else if (instance.FactoryFilter == null)
				{
					instance.FactoryFilter = AttributeFilter.DeserializeLengthDelimited(stream);
				}
				else
				{
					AttributeFilter.DeserializeLengthDelimited(stream, instance.FactoryFilter);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003A71 RID: 14961 RVA: 0x000BD722 File Offset: 0x000BB922
		public void Serialize(Stream stream)
		{
			GetGameStatsBucketsRequest.Serialize(stream, this);
		}

		// Token: 0x06003A72 RID: 14962 RVA: 0x000BD72C File Offset: 0x000BB92C
		public static void Serialize(Stream stream, GetGameStatsBucketsRequest instance)
		{
			if (instance.HasFactoryFilter)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.FactoryFilter.GetSerializedSize());
				AttributeFilter.Serialize(stream, instance.FactoryFilter);
			}
			if (instance.HasGameFilter)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameFilter.GetSerializedSize());
				AttributeFilter.Serialize(stream, instance.GameFilter);
			}
		}

		// Token: 0x06003A73 RID: 14963 RVA: 0x000BD794 File Offset: 0x000BB994
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasFactoryFilter)
			{
				num += 1U;
				uint serializedSize = this.FactoryFilter.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasGameFilter)
			{
				num += 1U;
				uint serializedSize2 = this.GameFilter.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x0400153A RID: 5434
		public bool HasFactoryFilter;

		// Token: 0x0400153B RID: 5435
		private AttributeFilter _FactoryFilter;

		// Token: 0x0400153C RID: 5436
		public bool HasGameFilter;

		// Token: 0x0400153D RID: 5437
		private AttributeFilter _GameFilter;
	}
}
