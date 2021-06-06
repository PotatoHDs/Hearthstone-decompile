using System.Collections.Generic;
using System.IO;
using bnet.protocol.friends.v2.client.Types;
using bnet.protocol.v2;

namespace bnet.protocol.friends.v2.client
{
	public class FriendStateAssignment : IProtoBuf
	{
		public bool HasId;

		private ulong _Id;

		public bool HasLevel;

		private FriendLevel _Level;

		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		public bool HasModifiedTimeUs;

		private ulong _ModifiedTimeUs;

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

		public FriendLevel Level
		{
			get
			{
				return _Level;
			}
			set
			{
				_Level = value;
				HasLevel = true;
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

		public ulong ModifiedTimeUs
		{
			get
			{
				return _ModifiedTimeUs;
			}
			set
			{
				_ModifiedTimeUs = value;
				HasModifiedTimeUs = true;
			}
		}

		public bool IsInitialized => true;

		public void SetId(ulong val)
		{
			Id = val;
		}

		public void SetLevel(FriendLevel val)
		{
			Level = val;
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

		public void SetModifiedTimeUs(ulong val)
		{
			ModifiedTimeUs = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasId)
			{
				num ^= Id.GetHashCode();
			}
			if (HasLevel)
			{
				num ^= Level.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute item in Attribute)
			{
				num ^= item.GetHashCode();
			}
			if (HasModifiedTimeUs)
			{
				num ^= ModifiedTimeUs.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FriendStateAssignment friendStateAssignment = obj as FriendStateAssignment;
			if (friendStateAssignment == null)
			{
				return false;
			}
			if (HasId != friendStateAssignment.HasId || (HasId && !Id.Equals(friendStateAssignment.Id)))
			{
				return false;
			}
			if (HasLevel != friendStateAssignment.HasLevel || (HasLevel && !Level.Equals(friendStateAssignment.Level)))
			{
				return false;
			}
			if (Attribute.Count != friendStateAssignment.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(friendStateAssignment.Attribute[i]))
				{
					return false;
				}
			}
			if (HasModifiedTimeUs != friendStateAssignment.HasModifiedTimeUs || (HasModifiedTimeUs && !ModifiedTimeUs.Equals(friendStateAssignment.ModifiedTimeUs)))
			{
				return false;
			}
			return true;
		}

		public static FriendStateAssignment ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FriendStateAssignment>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FriendStateAssignment Deserialize(Stream stream, FriendStateAssignment instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FriendStateAssignment DeserializeLengthDelimited(Stream stream)
		{
			FriendStateAssignment friendStateAssignment = new FriendStateAssignment();
			DeserializeLengthDelimited(stream, friendStateAssignment);
			return friendStateAssignment;
		}

		public static FriendStateAssignment DeserializeLengthDelimited(Stream stream, FriendStateAssignment instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FriendStateAssignment Deserialize(Stream stream, FriendStateAssignment instance, long limit)
		{
			instance.Level = FriendLevel.FRIEND_LEVEL_BATTLE_TAG;
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
				case 8:
					instance.Id = ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Level = (FriendLevel)ProtocolParser.ReadUInt64(stream);
					continue;
				case 42:
					instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
					continue;
				case 56:
					instance.ModifiedTimeUs = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, FriendStateAssignment instance)
		{
			if (instance.HasId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.Id);
			}
			if (instance.HasLevel)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Level);
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute item in instance.Attribute)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, item);
				}
			}
			if (instance.HasModifiedTimeUs)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, instance.ModifiedTimeUs);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(Id);
			}
			if (HasLevel)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Level);
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
			if (HasModifiedTimeUs)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(ModifiedTimeUs);
			}
			return num;
		}
	}
}
