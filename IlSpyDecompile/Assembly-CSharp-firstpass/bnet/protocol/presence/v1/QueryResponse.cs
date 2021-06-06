using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.presence.v1
{
	public class QueryResponse : IProtoBuf
	{
		private List<Field> _Field = new List<Field>();

		public List<Field> Field
		{
			get
			{
				return _Field;
			}
			set
			{
				_Field = value;
			}
		}

		public List<Field> FieldList => _Field;

		public int FieldCount => _Field.Count;

		public bool IsInitialized => true;

		public void AddField(Field val)
		{
			_Field.Add(val);
		}

		public void ClearField()
		{
			_Field.Clear();
		}

		public void SetField(List<Field> val)
		{
			Field = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (Field item in Field)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			QueryResponse queryResponse = obj as QueryResponse;
			if (queryResponse == null)
			{
				return false;
			}
			if (Field.Count != queryResponse.Field.Count)
			{
				return false;
			}
			for (int i = 0; i < Field.Count; i++)
			{
				if (!Field[i].Equals(queryResponse.Field[i]))
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
			if (instance.Field == null)
			{
				instance.Field = new List<Field>();
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
				case 18:
					instance.Field.Add(bnet.protocol.presence.v1.Field.DeserializeLengthDelimited(stream));
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
			if (instance.Field.Count <= 0)
			{
				return;
			}
			foreach (Field item in instance.Field)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				bnet.protocol.presence.v1.Field.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Field.Count > 0)
			{
				foreach (Field item in Field)
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
