using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.diag.v1
{
	// Token: 0x0200043B RID: 1083
	public class QueryResponse : IProtoBuf
	{
		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x06004945 RID: 18757 RVA: 0x000E4E23 File Offset: 0x000E3023
		// (set) Token: 0x06004946 RID: 18758 RVA: 0x000E4E2B File Offset: 0x000E302B
		public string Name { get; set; }

		// Token: 0x06004947 RID: 18759 RVA: 0x000E4E34 File Offset: 0x000E3034
		public void SetName(string val)
		{
			this.Name = val;
		}

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06004948 RID: 18760 RVA: 0x000E4E3D File Offset: 0x000E303D
		// (set) Token: 0x06004949 RID: 18761 RVA: 0x000E4E45 File Offset: 0x000E3045
		public List<string> Columns
		{
			get
			{
				return this._Columns;
			}
			set
			{
				this._Columns = value;
			}
		}

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x0600494A RID: 18762 RVA: 0x000E4E3D File Offset: 0x000E303D
		public List<string> ColumnsList
		{
			get
			{
				return this._Columns;
			}
		}

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x0600494B RID: 18763 RVA: 0x000E4E4E File Offset: 0x000E304E
		public int ColumnsCount
		{
			get
			{
				return this._Columns.Count;
			}
		}

		// Token: 0x0600494C RID: 18764 RVA: 0x000E4E5B File Offset: 0x000E305B
		public void AddColumns(string val)
		{
			this._Columns.Add(val);
		}

		// Token: 0x0600494D RID: 18765 RVA: 0x000E4E69 File Offset: 0x000E3069
		public void ClearColumns()
		{
			this._Columns.Clear();
		}

		// Token: 0x0600494E RID: 18766 RVA: 0x000E4E76 File Offset: 0x000E3076
		public void SetColumns(List<string> val)
		{
			this.Columns = val;
		}

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x0600494F RID: 18767 RVA: 0x000E4E7F File Offset: 0x000E307F
		// (set) Token: 0x06004950 RID: 18768 RVA: 0x000E4E87 File Offset: 0x000E3087
		public List<Row> Rows
		{
			get
			{
				return this._Rows;
			}
			set
			{
				this._Rows = value;
			}
		}

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06004951 RID: 18769 RVA: 0x000E4E7F File Offset: 0x000E307F
		public List<Row> RowsList
		{
			get
			{
				return this._Rows;
			}
		}

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06004952 RID: 18770 RVA: 0x000E4E90 File Offset: 0x000E3090
		public int RowsCount
		{
			get
			{
				return this._Rows.Count;
			}
		}

		// Token: 0x06004953 RID: 18771 RVA: 0x000E4E9D File Offset: 0x000E309D
		public void AddRows(Row val)
		{
			this._Rows.Add(val);
		}

		// Token: 0x06004954 RID: 18772 RVA: 0x000E4EAB File Offset: 0x000E30AB
		public void ClearRows()
		{
			this._Rows.Clear();
		}

		// Token: 0x06004955 RID: 18773 RVA: 0x000E4EB8 File Offset: 0x000E30B8
		public void SetRows(List<Row> val)
		{
			this.Rows = val;
		}

		// Token: 0x06004956 RID: 18774 RVA: 0x000E4EC4 File Offset: 0x000E30C4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Name.GetHashCode();
			foreach (string text in this.Columns)
			{
				num ^= text.GetHashCode();
			}
			foreach (Row row in this.Rows)
			{
				num ^= row.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004957 RID: 18775 RVA: 0x000E4F7C File Offset: 0x000E317C
		public override bool Equals(object obj)
		{
			QueryResponse queryResponse = obj as QueryResponse;
			if (queryResponse == null)
			{
				return false;
			}
			if (!this.Name.Equals(queryResponse.Name))
			{
				return false;
			}
			if (this.Columns.Count != queryResponse.Columns.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Columns.Count; i++)
			{
				if (!this.Columns[i].Equals(queryResponse.Columns[i]))
				{
					return false;
				}
			}
			if (this.Rows.Count != queryResponse.Rows.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Rows.Count; j++)
			{
				if (!this.Rows[j].Equals(queryResponse.Rows[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x06004958 RID: 18776 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004959 RID: 18777 RVA: 0x000E504D File Offset: 0x000E324D
		public static QueryResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<QueryResponse>(bs, 0, -1);
		}

		// Token: 0x0600495A RID: 18778 RVA: 0x000E5057 File Offset: 0x000E3257
		public void Deserialize(Stream stream)
		{
			QueryResponse.Deserialize(stream, this);
		}

		// Token: 0x0600495B RID: 18779 RVA: 0x000E5061 File Offset: 0x000E3261
		public static QueryResponse Deserialize(Stream stream, QueryResponse instance)
		{
			return QueryResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600495C RID: 18780 RVA: 0x000E506C File Offset: 0x000E326C
		public static QueryResponse DeserializeLengthDelimited(Stream stream)
		{
			QueryResponse queryResponse = new QueryResponse();
			QueryResponse.DeserializeLengthDelimited(stream, queryResponse);
			return queryResponse;
		}

		// Token: 0x0600495D RID: 18781 RVA: 0x000E5088 File Offset: 0x000E3288
		public static QueryResponse DeserializeLengthDelimited(Stream stream, QueryResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return QueryResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x0600495E RID: 18782 RVA: 0x000E50B0 File Offset: 0x000E32B0
		public static QueryResponse Deserialize(Stream stream, QueryResponse instance, long limit)
		{
			if (instance.Columns == null)
			{
				instance.Columns = new List<string>();
			}
			if (instance.Rows == null)
			{
				instance.Rows = new List<Row>();
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
				else if (num != 10)
				{
					if (num != 18)
					{
						if (num != 26)
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
							instance.Rows.Add(Row.DeserializeLengthDelimited(stream));
						}
					}
					else
					{
						instance.Columns.Add(ProtocolParser.ReadString(stream));
					}
				}
				else
				{
					instance.Name = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600495F RID: 18783 RVA: 0x000E518E File Offset: 0x000E338E
		public void Serialize(Stream stream)
		{
			QueryResponse.Serialize(stream, this);
		}

		// Token: 0x06004960 RID: 18784 RVA: 0x000E5198 File Offset: 0x000E3398
		public static void Serialize(Stream stream, QueryResponse instance)
		{
			if (instance.Name == null)
			{
				throw new ArgumentNullException("Name", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			if (instance.Columns.Count > 0)
			{
				foreach (string s in instance.Columns)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(s));
				}
			}
			if (instance.Rows.Count > 0)
			{
				foreach (Row row in instance.Rows)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, row.GetSerializedSize());
					Row.Serialize(stream, row);
				}
			}
		}

		// Token: 0x06004961 RID: 18785 RVA: 0x000E52A8 File Offset: 0x000E34A8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (this.Columns.Count > 0)
			{
				foreach (string s in this.Columns)
				{
					num += 1U;
					uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(s);
					num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
				}
			}
			if (this.Rows.Count > 0)
			{
				foreach (Row row in this.Rows)
				{
					num += 1U;
					uint serializedSize = row.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += 1U;
			return num;
		}

		// Token: 0x0400182F RID: 6191
		private List<string> _Columns = new List<string>();

		// Token: 0x04001830 RID: 6192
		private List<Row> _Rows = new List<Row>();
	}
}
