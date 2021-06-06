using System.Collections.Generic;
using System.IO;

namespace bnet.protocol
{
	public class AttributeStorage : IProtoBuf
	{
		public bool HasVersion;

		private uint _Version;

		private List<Attribute> _Attribute = new List<Attribute>();

		public uint Version
		{
			get
			{
				return _Version;
			}
			set
			{
				_Version = value;
				HasVersion = true;
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

		public void SetVersion(uint val)
		{
			Version = val;
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
			if (HasVersion)
			{
				num ^= Version.GetHashCode();
			}
			foreach (Attribute item in Attribute)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AttributeStorage attributeStorage = obj as AttributeStorage;
			if (attributeStorage == null)
			{
				return false;
			}
			if (HasVersion != attributeStorage.HasVersion || (HasVersion && !Version.Equals(attributeStorage.Version)))
			{
				return false;
			}
			if (Attribute.Count != attributeStorage.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(attributeStorage.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static AttributeStorage ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AttributeStorage>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AttributeStorage Deserialize(Stream stream, AttributeStorage instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AttributeStorage DeserializeLengthDelimited(Stream stream)
		{
			AttributeStorage attributeStorage = new AttributeStorage();
			DeserializeLengthDelimited(stream, attributeStorage);
			return attributeStorage;
		}

		public static AttributeStorage DeserializeLengthDelimited(Stream stream, AttributeStorage instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AttributeStorage Deserialize(Stream stream, AttributeStorage instance, long limit)
		{
			instance.Version = 0u;
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
					instance.Version = ProtocolParser.ReadUInt32(stream);
					continue;
				case 18:
					instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, AttributeStorage instance)
		{
			if (instance.HasVersion)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.Version);
			}
			if (instance.Attribute.Count <= 0)
			{
				return;
			}
			foreach (Attribute item in instance.Attribute)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				bnet.protocol.Attribute.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasVersion)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Version);
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
