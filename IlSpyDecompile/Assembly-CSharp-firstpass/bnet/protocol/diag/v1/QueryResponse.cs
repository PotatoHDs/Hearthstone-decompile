using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.diag.v1
{
	public class QueryResponse : IProtoBuf
	{
		private List<string> _Columns = new List<string>();

		private List<Row> _Rows = new List<Row>();

		public string Name { get; set; }

		public List<string> Columns
		{
			get
			{
				return _Columns;
			}
			set
			{
				_Columns = value;
			}
		}

		public List<string> ColumnsList => _Columns;

		public int ColumnsCount => _Columns.Count;

		public List<Row> Rows
		{
			get
			{
				return _Rows;
			}
			set
			{
				_Rows = value;
			}
		}

		public List<Row> RowsList => _Rows;

		public int RowsCount => _Rows.Count;

		public bool IsInitialized => true;

		public void SetName(string val)
		{
			Name = val;
		}

		public void AddColumns(string val)
		{
			_Columns.Add(val);
		}

		public void ClearColumns()
		{
			_Columns.Clear();
		}

		public void SetColumns(List<string> val)
		{
			Columns = val;
		}

		public void AddRows(Row val)
		{
			_Rows.Add(val);
		}

		public void ClearRows()
		{
			_Rows.Clear();
		}

		public void SetRows(List<Row> val)
		{
			Rows = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Name.GetHashCode();
			foreach (string column in Columns)
			{
				hashCode ^= column.GetHashCode();
			}
			foreach (Row row in Rows)
			{
				hashCode ^= row.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			QueryResponse queryResponse = obj as QueryResponse;
			if (queryResponse == null)
			{
				return false;
			}
			if (!Name.Equals(queryResponse.Name))
			{
				return false;
			}
			if (Columns.Count != queryResponse.Columns.Count)
			{
				return false;
			}
			for (int i = 0; i < Columns.Count; i++)
			{
				if (!Columns[i].Equals(queryResponse.Columns[i]))
				{
					return false;
				}
			}
			if (Rows.Count != queryResponse.Rows.Count)
			{
				return false;
			}
			for (int j = 0; j < Rows.Count; j++)
			{
				if (!Rows[j].Equals(queryResponse.Rows[j]))
				{
					return false;
				}
			}
			return true;
		}

		public static QueryResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<QueryResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static QueryResponse Deserialize(Stream stream, QueryResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static QueryResponse DeserializeLengthDelimited(Stream stream)
		{
			QueryResponse queryResponse = new QueryResponse();
			DeserializeLengthDelimited(stream, queryResponse);
			return queryResponse;
		}

		public static QueryResponse DeserializeLengthDelimited(Stream stream, QueryResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

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
					instance.Name = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					instance.Columns.Add(ProtocolParser.ReadString(stream));
					continue;
				case 26:
					instance.Rows.Add(Row.DeserializeLengthDelimited(stream));
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
				foreach (string column in instance.Columns)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(column));
				}
			}
			if (instance.Rows.Count <= 0)
			{
				return;
			}
			foreach (Row row in instance.Rows)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, row.GetSerializedSize());
				Row.Serialize(stream, row);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (Columns.Count > 0)
			{
				foreach (string column in Columns)
				{
					num++;
					uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(column);
					num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
				}
			}
			if (Rows.Count > 0)
			{
				foreach (Row row in Rows)
				{
					num++;
					uint serializedSize = row.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num + 1;
		}
	}
}
