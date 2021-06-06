using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.diag.v1
{
	public class Row : IProtoBuf
	{
		private List<string> _Values = new List<string>();

		public List<string> Values
		{
			get
			{
				return _Values;
			}
			set
			{
				_Values = value;
			}
		}

		public List<string> ValuesList => _Values;

		public int ValuesCount => _Values.Count;

		public bool IsInitialized => true;

		public void AddValues(string val)
		{
			_Values.Add(val);
		}

		public void ClearValues()
		{
			_Values.Clear();
		}

		public void SetValues(List<string> val)
		{
			Values = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (string value in Values)
			{
				num ^= value.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Row row = obj as Row;
			if (row == null)
			{
				return false;
			}
			if (Values.Count != row.Values.Count)
			{
				return false;
			}
			for (int i = 0; i < Values.Count; i++)
			{
				if (!Values[i].Equals(row.Values[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static Row ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Row>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Row Deserialize(Stream stream, Row instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Row DeserializeLengthDelimited(Stream stream)
		{
			Row row = new Row();
			DeserializeLengthDelimited(stream, row);
			return row;
		}

		public static Row DeserializeLengthDelimited(Stream stream, Row instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Row Deserialize(Stream stream, Row instance, long limit)
		{
			if (instance.Values == null)
			{
				instance.Values = new List<string>();
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
					instance.Values.Add(ProtocolParser.ReadString(stream));
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

		public static void Serialize(Stream stream, Row instance)
		{
			if (instance.Values.Count <= 0)
			{
				return;
			}
			foreach (string value in instance.Values)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(value));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Values.Count > 0)
			{
				foreach (string value in Values)
				{
					num++;
					uint byteCount = (uint)Encoding.UTF8.GetByteCount(value);
					num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
				}
				return num;
			}
			return num;
		}
	}
}
