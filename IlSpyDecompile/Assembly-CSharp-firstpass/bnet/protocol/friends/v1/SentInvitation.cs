using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.friends.v1
{
	public class SentInvitation : IProtoBuf
	{
		public bool HasId;

		private ulong _Id;

		public bool HasTargetName;

		private string _TargetName;

		public bool HasRole;

		private uint _Role;

		private List<Attribute> _Attribute = new List<Attribute>();

		public bool HasCreationTime;

		private ulong _CreationTime;

		public bool HasProgram;

		private uint _Program;

		public ulong Id
		{
			get
			{
				return _Id;
			}
			set
			{
				_Id = value;
				HasId = true;
			}
		}

		public string TargetName
		{
			get
			{
				return _TargetName;
			}
			set
			{
				_TargetName = value;
				HasTargetName = value != null;
			}
		}

		public uint Role
		{
			get
			{
				return _Role;
			}
			set
			{
				_Role = value;
				HasRole = true;
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

		public ulong CreationTime
		{
			get
			{
				return _CreationTime;
			}
			set
			{
				_CreationTime = value;
				HasCreationTime = true;
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

		public void SetId(ulong val)
		{
			Id = val;
		}

		public void SetTargetName(string val)
		{
			TargetName = val;
		}

		public void SetRole(uint val)
		{
			Role = val;
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

		public void SetCreationTime(ulong val)
		{
			CreationTime = val;
		}

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasId)
			{
				num ^= Id.GetHashCode();
			}
			if (HasTargetName)
			{
				num ^= TargetName.GetHashCode();
			}
			if (HasRole)
			{
				num ^= Role.GetHashCode();
			}
			foreach (Attribute item in Attribute)
			{
				num ^= item.GetHashCode();
			}
			if (HasCreationTime)
			{
				num ^= CreationTime.GetHashCode();
			}
			if (HasProgram)
			{
				num ^= Program.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SentInvitation sentInvitation = obj as SentInvitation;
			if (sentInvitation == null)
			{
				return false;
			}
			if (HasId != sentInvitation.HasId || (HasId && !Id.Equals(sentInvitation.Id)))
			{
				return false;
			}
			if (HasTargetName != sentInvitation.HasTargetName || (HasTargetName && !TargetName.Equals(sentInvitation.TargetName)))
			{
				return false;
			}
			if (HasRole != sentInvitation.HasRole || (HasRole && !Role.Equals(sentInvitation.Role)))
			{
				return false;
			}
			if (Attribute.Count != sentInvitation.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(sentInvitation.Attribute[i]))
				{
					return false;
				}
			}
			if (HasCreationTime != sentInvitation.HasCreationTime || (HasCreationTime && !CreationTime.Equals(sentInvitation.CreationTime)))
			{
				return false;
			}
			if (HasProgram != sentInvitation.HasProgram || (HasProgram && !Program.Equals(sentInvitation.Program)))
			{
				return false;
			}
			return true;
		}

		public static SentInvitation ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SentInvitation>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SentInvitation Deserialize(Stream stream, SentInvitation instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SentInvitation DeserializeLengthDelimited(Stream stream)
		{
			SentInvitation sentInvitation = new SentInvitation();
			DeserializeLengthDelimited(stream, sentInvitation);
			return sentInvitation;
		}

		public static SentInvitation DeserializeLengthDelimited(Stream stream, SentInvitation instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SentInvitation Deserialize(Stream stream, SentInvitation instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				case 9:
					instance.Id = binaryReader.ReadUInt64();
					continue;
				case 18:
					instance.TargetName = ProtocolParser.ReadString(stream);
					continue;
				case 24:
					instance.Role = ProtocolParser.ReadUInt32(stream);
					continue;
				case 34:
					instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
					continue;
				case 40:
					instance.CreationTime = ProtocolParser.ReadUInt64(stream);
					continue;
				case 53:
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

		public static void Serialize(Stream stream, SentInvitation instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasId)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.Id);
			}
			if (instance.HasTargetName)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TargetName));
			}
			if (instance.HasRole)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Role);
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute item in instance.Attribute)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, item);
				}
			}
			if (instance.HasCreationTime)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, instance.CreationTime);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(53);
				binaryWriter.Write(instance.Program);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasId)
			{
				num++;
				num += 8;
			}
			if (HasTargetName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(TargetName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasRole)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Role);
			}
			if (Attribute.Count > 0)
			{
				foreach (Attribute item in Attribute)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasCreationTime)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(CreationTime);
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
