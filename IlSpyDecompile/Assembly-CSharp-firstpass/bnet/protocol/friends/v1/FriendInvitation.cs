using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v1
{
	public class FriendInvitation : IProtoBuf
	{
		private List<uint> _Role = new List<uint>();

		private List<Attribute> _Attribute = new List<Attribute>();

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
			foreach (uint item in Role)
			{
				num ^= item.GetHashCode();
			}
			foreach (Attribute item2 in Attribute)
			{
				num ^= item2.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FriendInvitation friendInvitation = obj as FriendInvitation;
			if (friendInvitation == null)
			{
				return false;
			}
			if (Role.Count != friendInvitation.Role.Count)
			{
				return false;
			}
			for (int i = 0; i < Role.Count; i++)
			{
				if (!Role[i].Equals(friendInvitation.Role[i]))
				{
					return false;
				}
			}
			if (Attribute.Count != friendInvitation.Attribute.Count)
			{
				return false;
			}
			for (int j = 0; j < Attribute.Count; j++)
			{
				if (!Attribute[j].Equals(friendInvitation.Attribute[j]))
				{
					return false;
				}
			}
			return true;
		}

		public static FriendInvitation ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FriendInvitation>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FriendInvitation Deserialize(Stream stream, FriendInvitation instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FriendInvitation DeserializeLengthDelimited(Stream stream)
		{
			FriendInvitation friendInvitation = new FriendInvitation();
			DeserializeLengthDelimited(stream, friendInvitation);
			return friendInvitation;
		}

		public static FriendInvitation DeserializeLengthDelimited(Stream stream, FriendInvitation instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FriendInvitation Deserialize(Stream stream, FriendInvitation instance, long limit)
		{
			if (instance.Role == null)
			{
				instance.Role = new List<uint>();
			}
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
				case 18:
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

		public static void Serialize(Stream stream, FriendInvitation instance)
		{
			if (instance.Role.Count > 0)
			{
				stream.WriteByte(18);
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
			foreach (Attribute item3 in instance.Attribute)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, item3.GetSerializedSize());
				bnet.protocol.Attribute.Serialize(stream, item3);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
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
				foreach (Attribute item2 in Attribute)
				{
					num++;
					uint serializedSize = item2.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
