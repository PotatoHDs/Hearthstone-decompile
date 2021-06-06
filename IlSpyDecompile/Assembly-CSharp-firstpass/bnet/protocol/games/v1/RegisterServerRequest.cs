using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class RegisterServerRequest : IProtoBuf
	{
		private List<Attribute> _Attribute = new List<Attribute>();

		public bool HasCost;

		private uint _Cost;

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

		public uint Program { get; set; }

		public uint Cost
		{
			get
			{
				return _Cost;
			}
			set
			{
				_Cost = value;
				HasCost = true;
			}
		}

		public bool IsInitialized => true;

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

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public void SetCost(uint val)
		{
			Cost = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (Attribute item in Attribute)
			{
				num ^= item.GetHashCode();
			}
			num ^= Program.GetHashCode();
			if (HasCost)
			{
				num ^= Cost.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RegisterServerRequest registerServerRequest = obj as RegisterServerRequest;
			if (registerServerRequest == null)
			{
				return false;
			}
			if (Attribute.Count != registerServerRequest.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(registerServerRequest.Attribute[i]))
				{
					return false;
				}
			}
			if (!Program.Equals(registerServerRequest.Program))
			{
				return false;
			}
			if (HasCost != registerServerRequest.HasCost || (HasCost && !Cost.Equals(registerServerRequest.Cost)))
			{
				return false;
			}
			return true;
		}

		public static RegisterServerRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RegisterServerRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RegisterServerRequest Deserialize(Stream stream, RegisterServerRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RegisterServerRequest DeserializeLengthDelimited(Stream stream)
		{
			RegisterServerRequest registerServerRequest = new RegisterServerRequest();
			DeserializeLengthDelimited(stream, registerServerRequest);
			return registerServerRequest;
		}

		public static RegisterServerRequest DeserializeLengthDelimited(Stream stream, RegisterServerRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RegisterServerRequest Deserialize(Stream stream, RegisterServerRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
			}
			instance.Cost = 0u;
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
					instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
					continue;
				case 29:
					instance.Program = binaryReader.ReadUInt32();
					continue;
				case 32:
					instance.Cost = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, RegisterServerRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute item in instance.Attribute)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, item);
				}
			}
			stream.WriteByte(29);
			binaryWriter.Write(instance.Program);
			if (instance.HasCost)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.Cost);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Attribute.Count > 0)
			{
				foreach (Attribute item in Attribute)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += 4;
			if (HasCost)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Cost);
			}
			return num + 1;
		}
	}
}
