using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.friends.v2.client.Types;
using bnet.protocol.v2;

namespace bnet.protocol.friends.v2.client
{
	public class Friend : IProtoBuf
	{
		public bool HasId;

		private ulong _Id;

		public bool HasLevel;

		private FriendLevel _Level;

		public bool HasBattleTag;

		private string _BattleTag;

		public bool HasFullName;

		private string _FullName;

		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		public bool HasCreationTimeUs;

		private ulong _CreationTimeUs;

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

		public string FullName
		{
			get
			{
				return _FullName;
			}
			set
			{
				_FullName = value;
				HasFullName = value != null;
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

		public ulong CreationTimeUs
		{
			get
			{
				return _CreationTimeUs;
			}
			set
			{
				_CreationTimeUs = value;
				HasCreationTimeUs = true;
			}
		}

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

		public void SetBattleTag(string val)
		{
			BattleTag = val;
		}

		public void SetFullName(string val)
		{
			FullName = val;
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

		public void SetCreationTimeUs(ulong val)
		{
			CreationTimeUs = val;
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
			if (HasBattleTag)
			{
				num ^= BattleTag.GetHashCode();
			}
			if (HasFullName)
			{
				num ^= FullName.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute item in Attribute)
			{
				num ^= item.GetHashCode();
			}
			if (HasCreationTimeUs)
			{
				num ^= CreationTimeUs.GetHashCode();
			}
			if (HasModifiedTimeUs)
			{
				num ^= ModifiedTimeUs.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Friend friend = obj as Friend;
			if (friend == null)
			{
				return false;
			}
			if (HasId != friend.HasId || (HasId && !Id.Equals(friend.Id)))
			{
				return false;
			}
			if (HasLevel != friend.HasLevel || (HasLevel && !Level.Equals(friend.Level)))
			{
				return false;
			}
			if (HasBattleTag != friend.HasBattleTag || (HasBattleTag && !BattleTag.Equals(friend.BattleTag)))
			{
				return false;
			}
			if (HasFullName != friend.HasFullName || (HasFullName && !FullName.Equals(friend.FullName)))
			{
				return false;
			}
			if (Attribute.Count != friend.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(friend.Attribute[i]))
				{
					return false;
				}
			}
			if (HasCreationTimeUs != friend.HasCreationTimeUs || (HasCreationTimeUs && !CreationTimeUs.Equals(friend.CreationTimeUs)))
			{
				return false;
			}
			if (HasModifiedTimeUs != friend.HasModifiedTimeUs || (HasModifiedTimeUs && !ModifiedTimeUs.Equals(friend.ModifiedTimeUs)))
			{
				return false;
			}
			return true;
		}

		public static Friend ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Friend>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Friend Deserialize(Stream stream, Friend instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Friend DeserializeLengthDelimited(Stream stream)
		{
			Friend friend = new Friend();
			DeserializeLengthDelimited(stream, friend);
			return friend;
		}

		public static Friend DeserializeLengthDelimited(Stream stream, Friend instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Friend Deserialize(Stream stream, Friend instance, long limit)
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
				case 26:
					instance.BattleTag = ProtocolParser.ReadString(stream);
					continue;
				case 34:
					instance.FullName = ProtocolParser.ReadString(stream);
					continue;
				case 42:
					instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
					continue;
				case 48:
					instance.CreationTimeUs = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, Friend instance)
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
			if (instance.HasBattleTag)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattleTag));
			}
			if (instance.HasFullName)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FullName));
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
			if (instance.HasCreationTimeUs)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, instance.CreationTimeUs);
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
			if (HasBattleTag)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(BattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasFullName)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(FullName);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
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
			if (HasCreationTimeUs)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(CreationTimeUs);
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
