using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.presence.v1
{
	// Token: 0x0200033A RID: 826
	public class QueryResponse : IProtoBuf
	{
		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x0600330C RID: 13068 RVA: 0x000AA85F File Offset: 0x000A8A5F
		// (set) Token: 0x0600330D RID: 13069 RVA: 0x000AA867 File Offset: 0x000A8A67
		public List<Field> Field
		{
			get
			{
				return this._Field;
			}
			set
			{
				this._Field = value;
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x0600330E RID: 13070 RVA: 0x000AA85F File Offset: 0x000A8A5F
		public List<Field> FieldList
		{
			get
			{
				return this._Field;
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x0600330F RID: 13071 RVA: 0x000AA870 File Offset: 0x000A8A70
		public int FieldCount
		{
			get
			{
				return this._Field.Count;
			}
		}

		// Token: 0x06003310 RID: 13072 RVA: 0x000AA87D File Offset: 0x000A8A7D
		public void AddField(Field val)
		{
			this._Field.Add(val);
		}

		// Token: 0x06003311 RID: 13073 RVA: 0x000AA88B File Offset: 0x000A8A8B
		public void ClearField()
		{
			this._Field.Clear();
		}

		// Token: 0x06003312 RID: 13074 RVA: 0x000AA898 File Offset: 0x000A8A98
		public void SetField(List<Field> val)
		{
			this.Field = val;
		}

		// Token: 0x06003313 RID: 13075 RVA: 0x000AA8A4 File Offset: 0x000A8AA4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (Field field in this.Field)
			{
				num ^= field.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003314 RID: 13076 RVA: 0x000AA908 File Offset: 0x000A8B08
		public override bool Equals(object obj)
		{
			QueryResponse queryResponse = obj as QueryResponse;
			if (queryResponse == null)
			{
				return false;
			}
			if (this.Field.Count != queryResponse.Field.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Field.Count; i++)
			{
				if (!this.Field[i].Equals(queryResponse.Field[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06003315 RID: 13077 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003316 RID: 13078 RVA: 0x000AA973 File Offset: 0x000A8B73
		public static QueryResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<QueryResponse>(bs, 0, -1);
		}

		// Token: 0x06003317 RID: 13079 RVA: 0x000AA97D File Offset: 0x000A8B7D
		public void Deserialize(Stream stream)
		{
			QueryResponse.Deserialize(stream, this);
		}

		// Token: 0x06003318 RID: 13080 RVA: 0x000AA987 File Offset: 0x000A8B87
		public static QueryResponse Deserialize(Stream stream, QueryResponse instance)
		{
			return QueryResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003319 RID: 13081 RVA: 0x000AA994 File Offset: 0x000A8B94
		public static QueryResponse DeserializeLengthDelimited(Stream stream)
		{
			QueryResponse queryResponse = new QueryResponse();
			QueryResponse.DeserializeLengthDelimited(stream, queryResponse);
			return queryResponse;
		}

		// Token: 0x0600331A RID: 13082 RVA: 0x000AA9B0 File Offset: 0x000A8BB0
		public static QueryResponse DeserializeLengthDelimited(Stream stream, QueryResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return QueryResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x0600331B RID: 13083 RVA: 0x000AA9D8 File Offset: 0x000A8BD8
		public static QueryResponse Deserialize(Stream stream, QueryResponse instance, long limit)
		{
			if (instance.Field == null)
			{
				instance.Field = new List<Field>();
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
				else if (num == 18)
				{
					instance.Field.Add(bnet.protocol.presence.v1.Field.DeserializeLengthDelimited(stream));
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

		// Token: 0x0600331C RID: 13084 RVA: 0x000AAA70 File Offset: 0x000A8C70
		public void Serialize(Stream stream)
		{
			QueryResponse.Serialize(stream, this);
		}

		// Token: 0x0600331D RID: 13085 RVA: 0x000AAA7C File Offset: 0x000A8C7C
		public static void Serialize(Stream stream, QueryResponse instance)
		{
			if (instance.Field.Count > 0)
			{
				foreach (Field field in instance.Field)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, field.GetSerializedSize());
					bnet.protocol.presence.v1.Field.Serialize(stream, field);
				}
			}
		}

		// Token: 0x0600331E RID: 13086 RVA: 0x000AAAF4 File Offset: 0x000A8CF4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Field.Count > 0)
			{
				foreach (Field field in this.Field)
				{
					num += 1U;
					uint serializedSize = field.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x040013F2 RID: 5106
		private List<Field> _Field = new List<Field>();
	}
}
