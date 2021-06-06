using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.report.v1
{
	public class CustomReport : IProtoBuf
	{
		public bool HasType;

		private string _Type;

		public bool HasProgramId;

		private string _ProgramId;

		private List<Attribute> _Attribute = new List<Attribute>();

		public string Type
		{
			get
			{
				return _Type;
			}
			set
			{
				_Type = value;
				HasType = value != null;
			}
		}

		public string ProgramId
		{
			get
			{
				return _ProgramId;
			}
			set
			{
				_ProgramId = value;
				HasProgramId = value != null;
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

		public void SetType(string val)
		{
			Type = val;
		}

		public void SetProgramId(string val)
		{
			ProgramId = val;
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
			if (HasType)
			{
				num ^= Type.GetHashCode();
			}
			if (HasProgramId)
			{
				num ^= ProgramId.GetHashCode();
			}
			foreach (Attribute item in Attribute)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CustomReport customReport = obj as CustomReport;
			if (customReport == null)
			{
				return false;
			}
			if (HasType != customReport.HasType || (HasType && !Type.Equals(customReport.Type)))
			{
				return false;
			}
			if (HasProgramId != customReport.HasProgramId || (HasProgramId && !ProgramId.Equals(customReport.ProgramId)))
			{
				return false;
			}
			if (Attribute.Count != customReport.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(customReport.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static CustomReport ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CustomReport>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CustomReport Deserialize(Stream stream, CustomReport instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CustomReport DeserializeLengthDelimited(Stream stream)
		{
			CustomReport customReport = new CustomReport();
			DeserializeLengthDelimited(stream, customReport);
			return customReport;
		}

		public static CustomReport DeserializeLengthDelimited(Stream stream, CustomReport instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CustomReport Deserialize(Stream stream, CustomReport instance, long limit)
		{
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
				case 10:
					instance.Type = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					instance.ProgramId = ProtocolParser.ReadString(stream);
					continue;
				case 26:
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

		public static void Serialize(Stream stream, CustomReport instance)
		{
			if (instance.HasType)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Type));
			}
			if (instance.HasProgramId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ProgramId));
			}
			if (instance.Attribute.Count <= 0)
			{
				return;
			}
			foreach (Attribute item in instance.Attribute)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				bnet.protocol.Attribute.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasType)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Type);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasProgramId)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(ProgramId);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
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
