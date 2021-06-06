using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.v2
{
	public class AttributeFilter : IProtoBuf
	{
		public static class Types
		{
			public enum Operation
			{
				MATCH_NONE,
				MATCH_ANY,
				MATCH_ALL,
				MATCH_ALL_MOST_SPECIFIC
			}
		}

		public bool HasOp;

		private Types.Operation _Op;

		private List<Attribute> _Attribute = new List<Attribute>();

		public Types.Operation Op
		{
			get
			{
				return _Op;
			}
			set
			{
				_Op = value;
				HasOp = true;
			}
		}

		public List<Attribute> Attribute
		{
			get
			{
				return _Attribute;
			}
			set
			{
				_Attribute = value;
			}
		}

		public List<Attribute> AttributeList => _Attribute;

		public int AttributeCount => _Attribute.Count;

		public bool IsInitialized => true;

		public void SetOp(Types.Operation val)
		{
			Op = val;
		}

		public void AddAttribute(Attribute val)
		{
			_Attribute.Add(val);
		}

		public void ClearAttribute()
		{
			_Attribute.Clear();
		}

		public void SetAttribute(List<Attribute> val)
		{
			Attribute = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasOp)
			{
				num ^= Op.GetHashCode();
			}
			foreach (Attribute item in Attribute)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AttributeFilter attributeFilter = obj as AttributeFilter;
			if (attributeFilter == null)
			{
				return false;
			}
			if (HasOp != attributeFilter.HasOp || (HasOp && !Op.Equals(attributeFilter.Op)))
			{
				return false;
			}
			if (Attribute.Count != attributeFilter.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(attributeFilter.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static AttributeFilter ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AttributeFilter>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AttributeFilter Deserialize(Stream stream, AttributeFilter instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AttributeFilter DeserializeLengthDelimited(Stream stream)
		{
			AttributeFilter attributeFilter = new AttributeFilter();
			DeserializeLengthDelimited(stream, attributeFilter);
			return attributeFilter;
		}

		public static AttributeFilter DeserializeLengthDelimited(Stream stream, AttributeFilter instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AttributeFilter Deserialize(Stream stream, AttributeFilter instance, long limit)
		{
			instance.Op = Types.Operation.MATCH_NONE;
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
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
				case 8:
					instance.Op = (Types.Operation)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, AttributeFilter instance)
		{
			if (instance.HasOp)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Op);
			}
			if (instance.Attribute.Count <= 0)
			{
				return;
			}
			foreach (Attribute item in instance.Attribute)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				bnet.protocol.v2.Attribute.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasOp)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Op);
			}
			if (Attribute.Count > 0)
			{
				foreach (Attribute item in Attribute)
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
