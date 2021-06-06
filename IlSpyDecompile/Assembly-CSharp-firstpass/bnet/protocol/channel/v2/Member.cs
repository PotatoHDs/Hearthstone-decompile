using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;
using bnet.protocol.v2;

namespace bnet.protocol.channel.v2
{
	public class Member : IProtoBuf
	{
		public bool HasId;

		private GameAccountHandle _Id;

		public bool HasBattleTag;

		private string _BattleTag;

		public bool HasVoiceId;

		private string _VoiceId;

		private List<uint> _Role = new List<uint>();

		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		public GameAccountHandle Id
		{
			get
			{
				return _Id;
			}
			set
			{
				_Id = value;
				HasId = value != null;
			}
		}

		public string BattleTag
		{
			get
			{
				return _BattleTag;
			}
			set
			{
				_BattleTag = value;
				HasBattleTag = value != null;
			}
		}

		public string VoiceId
		{
			get
			{
				return _VoiceId;
			}
			set
			{
				_VoiceId = value;
				HasVoiceId = value != null;
			}
		}

		public List<uint> Role
		{
			get
			{
				return _Role;
			}
			set
			{
				_Role = value;
			}
		}

		public List<uint> RoleList => _Role;

		public int RoleCount => _Role.Count;

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

		public bool IsInitialized => true;

		public void SetId(GameAccountHandle val)
		{
			Id = val;
		}

		public void SetBattleTag(string val)
		{
			BattleTag = val;
		}

		public void SetVoiceId(string val)
		{
			VoiceId = val;
		}

		public void AddRole(uint val)
		{
			_Role.Add(val);
		}

		public void ClearRole()
		{
			_Role.Clear();
		}

		public void SetRole(List<uint> val)
		{
			Role = val;
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

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasId)
			{
				num ^= Id.GetHashCode();
			}
			if (HasBattleTag)
			{
				num ^= BattleTag.GetHashCode();
			}
			if (HasVoiceId)
			{
				num ^= VoiceId.GetHashCode();
			}
			foreach (uint item in Role)
			{
				num ^= item.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute item2 in Attribute)
			{
				num ^= item2.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Member member = obj as Member;
			if (member == null)
			{
				return false;
			}
			if (HasId != member.HasId || (HasId && !Id.Equals(member.Id)))
			{
				return false;
			}
			if (HasBattleTag != member.HasBattleTag || (HasBattleTag && !BattleTag.Equals(member.BattleTag)))
			{
				return false;
			}
			if (HasVoiceId != member.HasVoiceId || (HasVoiceId && !VoiceId.Equals(member.VoiceId)))
			{
				return false;
			}
			if (Role.Count != member.Role.Count)
			{
				return false;
			}
			for (int i = 0; i < Role.Count; i++)
			{
				if (!Role[i].Equals(member.Role[i]))
				{
					return false;
				}
			}
			if (Attribute.Count != member.Attribute.Count)
			{
				return false;
			}
			for (int j = 0; j < Attribute.Count; j++)
			{
				if (!Attribute[j].Equals(member.Attribute[j]))
				{
					return false;
				}
			}
			return true;
		}

		public static Member ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Member>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Member Deserialize(Stream stream, Member instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Member DeserializeLengthDelimited(Stream stream)
		{
			Member member = new Member();
			DeserializeLengthDelimited(stream, member);
			return member;
		}

		public static Member DeserializeLengthDelimited(Stream stream, Member instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Member Deserialize(Stream stream, Member instance, long limit)
		{
			if (instance.Role == null)
			{
				instance.Role = new List<uint>();
			}
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
					if (instance.Id == null)
					{
						instance.Id = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.Id);
					}
					continue;
				case 18:
					instance.BattleTag = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.VoiceId = ProtocolParser.ReadString(stream);
					continue;
				case 34:
				{
					long num2 = ProtocolParser.ReadUInt32(stream);
					num2 += stream.Position;
					while (stream.Position < num2)
					{
						instance.Role.Add(ProtocolParser.ReadUInt32(stream));
					}
					if (stream.Position == num2)
					{
						continue;
					}
					throw new ProtocolBufferException("Read too many bytes in packed data");
				}
				case 42:
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

		public static void Serialize(Stream stream, Member instance)
		{
			if (instance.HasId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Id.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.Id);
			}
			if (instance.HasBattleTag)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattleTag));
			}
			if (instance.HasVoiceId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.VoiceId));
			}
			if (instance.Role.Count > 0)
			{
				stream.WriteByte(34);
				uint num = 0u;
				foreach (uint item in instance.Role)
				{
					num += ProtocolParser.SizeOfUInt32(item);
				}
				ProtocolParser.WriteUInt32(stream, num);
				foreach (uint item2 in instance.Role)
				{
					ProtocolParser.WriteUInt32(stream, item2);
				}
			}
			if (instance.Attribute.Count <= 0)
			{
				return;
			}
			foreach (bnet.protocol.v2.Attribute item3 in instance.Attribute)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, item3.GetSerializedSize());
				bnet.protocol.v2.Attribute.Serialize(stream, item3);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasId)
			{
				num++;
				uint serializedSize = Id.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasBattleTag)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(BattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasVoiceId)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(VoiceId);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (Role.Count > 0)
			{
				num++;
				uint num2 = num;
				foreach (uint item in Role)
				{
					num += ProtocolParser.SizeOfUInt32(item);
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			if (Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute item2 in Attribute)
				{
					num++;
					uint serializedSize2 = item2.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
				return num;
			}
			return num;
		}
	}
}
