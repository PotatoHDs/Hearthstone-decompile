using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class GameProperties : IProtoBuf
	{
		private List<Attribute> _CreationAttributes = new List<Attribute>();

		public bool HasFilter;

		private AttributeFilter _Filter;

		public bool HasCreate;

		private bool _Create;

		public bool HasOpen;

		private bool _Open;

		public bool HasProgram;

		private uint _Program;

		public List<Attribute> CreationAttributes
		{
			get
			{
				return _CreationAttributes;
			}
			set
			{
				_CreationAttributes = value;
			}
		}

		public List<Attribute> CreationAttributesList => _CreationAttributes;

		public int CreationAttributesCount => _CreationAttributes.Count;

		public AttributeFilter Filter
		{
			get
			{
				return _Filter;
			}
			set
			{
				_Filter = value;
				HasFilter = value != null;
			}
		}

		public bool Create
		{
			get
			{
				return _Create;
			}
			set
			{
				_Create = value;
				HasCreate = true;
			}
		}

		public bool Open
		{
			get
			{
				return _Open;
			}
			set
			{
				_Open = value;
				HasOpen = true;
			}
		}

		public uint Program
		{
			get
			{
				return _Program;
			}
			set
			{
				_Program = value;
				HasProgram = true;
			}
		}

		public bool IsInitialized => true;

		public void AddCreationAttributes(Attribute val)
		{
			_CreationAttributes.Add(val);
		}

		public void ClearCreationAttributes()
		{
			_CreationAttributes.Clear();
		}

		public void SetCreationAttributes(List<Attribute> val)
		{
			CreationAttributes = val;
		}

		public void SetFilter(AttributeFilter val)
		{
			Filter = val;
		}

		public void SetCreate(bool val)
		{
			Create = val;
		}

		public void SetOpen(bool val)
		{
			Open = val;
		}

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (Attribute creationAttribute in CreationAttributes)
			{
				num ^= creationAttribute.GetHashCode();
			}
			if (HasFilter)
			{
				num ^= Filter.GetHashCode();
			}
			if (HasCreate)
			{
				num ^= Create.GetHashCode();
			}
			if (HasOpen)
			{
				num ^= Open.GetHashCode();
			}
			if (HasProgram)
			{
				num ^= Program.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameProperties gameProperties = obj as GameProperties;
			if (gameProperties == null)
			{
				return false;
			}
			if (CreationAttributes.Count != gameProperties.CreationAttributes.Count)
			{
				return false;
			}
			for (int i = 0; i < CreationAttributes.Count; i++)
			{
				if (!CreationAttributes[i].Equals(gameProperties.CreationAttributes[i]))
				{
					return false;
				}
			}
			if (HasFilter != gameProperties.HasFilter || (HasFilter && !Filter.Equals(gameProperties.Filter)))
			{
				return false;
			}
			if (HasCreate != gameProperties.HasCreate || (HasCreate && !Create.Equals(gameProperties.Create)))
			{
				return false;
			}
			if (HasOpen != gameProperties.HasOpen || (HasOpen && !Open.Equals(gameProperties.Open)))
			{
				return false;
			}
			if (HasProgram != gameProperties.HasProgram || (HasProgram && !Program.Equals(gameProperties.Program)))
			{
				return false;
			}
			return true;
		}

		public static GameProperties ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameProperties>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameProperties Deserialize(Stream stream, GameProperties instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameProperties DeserializeLengthDelimited(Stream stream)
		{
			GameProperties gameProperties = new GameProperties();
			DeserializeLengthDelimited(stream, gameProperties);
			return gameProperties;
		}

		public static GameProperties DeserializeLengthDelimited(Stream stream, GameProperties instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameProperties Deserialize(Stream stream, GameProperties instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.CreationAttributes == null)
			{
				instance.CreationAttributes = new List<Attribute>();
			}
			instance.Create = false;
			instance.Open = true;
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
					instance.CreationAttributes.Add(Attribute.DeserializeLengthDelimited(stream));
					continue;
				case 18:
					if (instance.Filter == null)
					{
						instance.Filter = AttributeFilter.DeserializeLengthDelimited(stream);
					}
					else
					{
						AttributeFilter.DeserializeLengthDelimited(stream, instance.Filter);
					}
					continue;
				case 24:
					instance.Create = ProtocolParser.ReadBool(stream);
					continue;
				case 32:
					instance.Open = ProtocolParser.ReadBool(stream);
					continue;
				case 45:
					instance.Program = binaryReader.ReadUInt32();
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

		public static void Serialize(Stream stream, GameProperties instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.CreationAttributes.Count > 0)
			{
				foreach (Attribute creationAttribute in instance.CreationAttributes)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, creationAttribute.GetSerializedSize());
					Attribute.Serialize(stream, creationAttribute);
				}
			}
			if (instance.HasFilter)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Filter.GetSerializedSize());
				AttributeFilter.Serialize(stream, instance.Filter);
			}
			if (instance.HasCreate)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.Create);
			}
			if (instance.HasOpen)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.Open);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.Program);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (CreationAttributes.Count > 0)
			{
				foreach (Attribute creationAttribute in CreationAttributes)
				{
					num++;
					uint serializedSize = creationAttribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasFilter)
			{
				num++;
				uint serializedSize2 = Filter.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasCreate)
			{
				num++;
				num++;
			}
			if (HasOpen)
			{
				num++;
				num++;
			}
			if (HasProgram)
			{
				num++;
				num += 4;
			}
			return num;
		}
	}
}
