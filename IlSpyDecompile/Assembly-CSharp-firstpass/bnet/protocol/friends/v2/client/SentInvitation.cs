using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.friends.v2.client.Types;
using bnet.protocol.v2;

namespace bnet.protocol.friends.v2.client
{
	public class SentInvitation : IProtoBuf
	{
		public bool HasId;

		private ulong _Id;

		public bool HasTargetName;

		private string _TargetName;

		public bool HasLevel;

		private FriendLevel _Level;

		public bool HasProgram;

		private uint _Program;

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

		public void SetTargetName(string val)
		{
			TargetName = val;
		}

		public void SetLevel(FriendLevel val)
		{
			Level = val;
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
			if (HasTargetName)
			{
				num ^= TargetName.GetHashCode();
			}
			if (HasLevel)
			{
				num ^= Level.GetHashCode();
			}
			if (HasProgram)
			{
				num ^= Program.GetHashCode();
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
			if (HasLevel != sentInvitation.HasLevel || (HasLevel && !Level.Equals(sentInvitation.Level)))
			{
				return false;
			}
			if (HasProgram != sentInvitation.HasProgram || (HasProgram && !Program.Equals(sentInvitation.Program)))
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
			if (HasCreationTimeUs != sentInvitation.HasCreationTimeUs || (HasCreationTimeUs && !CreationTimeUs.Equals(sentInvitation.CreationTimeUs)))
			{
				return false;
			}
			if (HasModifiedTimeUs != sentInvitation.HasModifiedTimeUs || (HasModifiedTimeUs && !ModifiedTimeUs.Equals(sentInvitation.ModifiedTimeUs)))
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
				case 18:
					instance.TargetName = ProtocolParser.ReadString(stream);
					continue;
				case 24:
					instance.Level = (FriendLevel)ProtocolParser.ReadUInt64(stream);
					continue;
				case 37:
					instance.Program = binaryReader.ReadUInt32();
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

		public static void Serialize(Stream stream, SentInvitation instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.Id);
			}
			if (instance.HasTargetName)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TargetName));
			}
			if (instance.HasLevel)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Level);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.Program);
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
			if (HasTargetName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(TargetName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasLevel)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Level);
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
