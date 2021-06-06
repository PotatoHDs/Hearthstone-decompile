using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.v2;

namespace bnet.protocol.matchmaking.v1
{
	public class MatchmakerAttributeInfo : IProtoBuf
	{
		public bool HasName;

		private string _Name;

		public bool HasProgram;

		private uint _Program;

		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		public bool HasIsPrivate;

		private bool _IsPrivate;

		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				_Name = value;
				HasName = value != null;
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

		public List<bnet.protocol.v2.Attribute> Attribute
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

		public List<bnet.protocol.v2.Attribute> AttributeList => _Attribute;

		public int AttributeCount => _Attribute.Count;

		public bool IsPrivate
		{
			get
			{
				return _IsPrivate;
			}
			set
			{
				_IsPrivate = value;
				HasIsPrivate = true;
			}
		}

		public bool IsInitialized => true;

		public void SetName(string val)
		{
			Name = val;
		}

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			_Attribute.Add(val);
		}

		public void ClearAttribute()
		{
			_Attribute.Clear();
		}

		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			Attribute = val;
		}

		public void SetIsPrivate(bool val)
		{
			IsPrivate = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasName)
			{
				num ^= Name.GetHashCode();
			}
			if (HasProgram)
			{
				num ^= Program.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute item in Attribute)
			{
				num ^= item.GetHashCode();
			}
			if (HasIsPrivate)
			{
				num ^= IsPrivate.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			MatchmakerAttributeInfo matchmakerAttributeInfo = obj as MatchmakerAttributeInfo;
			if (matchmakerAttributeInfo == null)
			{
				return false;
			}
			if (HasName != matchmakerAttributeInfo.HasName || (HasName && !Name.Equals(matchmakerAttributeInfo.Name)))
			{
				return false;
			}
			if (HasProgram != matchmakerAttributeInfo.HasProgram || (HasProgram && !Program.Equals(matchmakerAttributeInfo.Program)))
			{
				return false;
			}
			if (Attribute.Count != matchmakerAttributeInfo.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(matchmakerAttributeInfo.Attribute[i]))
				{
					return false;
				}
			}
			if (HasIsPrivate != matchmakerAttributeInfo.HasIsPrivate || (HasIsPrivate && !IsPrivate.Equals(matchmakerAttributeInfo.IsPrivate)))
			{
				return false;
			}
			return true;
		}

		public static MatchmakerAttributeInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MatchmakerAttributeInfo>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static MatchmakerAttributeInfo Deserialize(Stream stream, MatchmakerAttributeInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static MatchmakerAttributeInfo DeserializeLengthDelimited(Stream stream)
		{
			MatchmakerAttributeInfo matchmakerAttributeInfo = new MatchmakerAttributeInfo();
			DeserializeLengthDelimited(stream, matchmakerAttributeInfo);
			return matchmakerAttributeInfo;
		}

		public static MatchmakerAttributeInfo DeserializeLengthDelimited(Stream stream, MatchmakerAttributeInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static MatchmakerAttributeInfo Deserialize(Stream stream, MatchmakerAttributeInfo instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.v2.Attribute>();
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
				case 21:
					instance.Program = binaryReader.ReadUInt32();
					continue;
				case 26:
					instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
					continue;
				case 32:
					instance.IsPrivate = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, MatchmakerAttributeInfo instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasName)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Program);
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute item in instance.Attribute)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, item);
				}
			}
			if (instance.HasIsPrivate)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.IsPrivate);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasProgram)
			{
				num++;
				num += 4;
			}
			if (Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute item in Attribute)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasIsPrivate)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
