using System.Collections.Generic;
using System.IO;
using bnet.protocol.friends.v2.client.Types;
using bnet.protocol.v2;

namespace bnet.protocol.friends.v2.client
{
	public class ReceivedInvitation : IProtoBuf
	{
		public bool HasId;

		private ulong _Id;

		public bool HasInviter;

		private UserDescription _Inviter;

		public bool HasInvitee;

		private UserDescription _Invitee;

		public bool HasLevel;

		private FriendLevel _Level;

		public bool HasProgram;

		private uint _Program;

		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		public bool HasCreationTimeUs;

		private ulong _CreationTimeUs;

		public bool HasModifiedTimeUs;

		private ulong _ModifiedTimeUs;

		public bool HasExpirationTimeUs;

		private ulong _ExpirationTimeUs;

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

		public UserDescription Inviter
		{
			get
			{
				return _Inviter;
			}
			set
			{
				_Inviter = value;
				HasInviter = value != null;
			}
		}

		public UserDescription Invitee
		{
			get
			{
				return _Invitee;
			}
			set
			{
				_Invitee = value;
				HasInvitee = value != null;
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

		public ulong ExpirationTimeUs
		{
			get
			{
				return _ExpirationTimeUs;
			}
			set
			{
				_ExpirationTimeUs = value;
				HasExpirationTimeUs = true;
			}
		}

		public bool IsInitialized => true;

		public void SetId(ulong val)
		{
			Id = val;
		}

		public void SetInviter(UserDescription val)
		{
			Inviter = val;
		}

		public void SetInvitee(UserDescription val)
		{
			Invitee = val;
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

		public void SetExpirationTimeUs(ulong val)
		{
			ExpirationTimeUs = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasId)
			{
				num ^= Id.GetHashCode();
			}
			if (HasInviter)
			{
				num ^= Inviter.GetHashCode();
			}
			if (HasInvitee)
			{
				num ^= Invitee.GetHashCode();
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
			if (HasExpirationTimeUs)
			{
				num ^= ExpirationTimeUs.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ReceivedInvitation receivedInvitation = obj as ReceivedInvitation;
			if (receivedInvitation == null)
			{
				return false;
			}
			if (HasId != receivedInvitation.HasId || (HasId && !Id.Equals(receivedInvitation.Id)))
			{
				return false;
			}
			if (HasInviter != receivedInvitation.HasInviter || (HasInviter && !Inviter.Equals(receivedInvitation.Inviter)))
			{
				return false;
			}
			if (HasInvitee != receivedInvitation.HasInvitee || (HasInvitee && !Invitee.Equals(receivedInvitation.Invitee)))
			{
				return false;
			}
			if (HasLevel != receivedInvitation.HasLevel || (HasLevel && !Level.Equals(receivedInvitation.Level)))
			{
				return false;
			}
			if (HasProgram != receivedInvitation.HasProgram || (HasProgram && !Program.Equals(receivedInvitation.Program)))
			{
				return false;
			}
			if (Attribute.Count != receivedInvitation.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(receivedInvitation.Attribute[i]))
				{
					return false;
				}
			}
			if (HasCreationTimeUs != receivedInvitation.HasCreationTimeUs || (HasCreationTimeUs && !CreationTimeUs.Equals(receivedInvitation.CreationTimeUs)))
			{
				return false;
			}
			if (HasModifiedTimeUs != receivedInvitation.HasModifiedTimeUs || (HasModifiedTimeUs && !ModifiedTimeUs.Equals(receivedInvitation.ModifiedTimeUs)))
			{
				return false;
			}
			if (HasExpirationTimeUs != receivedInvitation.HasExpirationTimeUs || (HasExpirationTimeUs && !ExpirationTimeUs.Equals(receivedInvitation.ExpirationTimeUs)))
			{
				return false;
			}
			return true;
		}

		public static ReceivedInvitation ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ReceivedInvitation>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ReceivedInvitation Deserialize(Stream stream, ReceivedInvitation instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ReceivedInvitation DeserializeLengthDelimited(Stream stream)
		{
			ReceivedInvitation receivedInvitation = new ReceivedInvitation();
			DeserializeLengthDelimited(stream, receivedInvitation);
			return receivedInvitation;
		}

		public static ReceivedInvitation DeserializeLengthDelimited(Stream stream, ReceivedInvitation instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ReceivedInvitation Deserialize(Stream stream, ReceivedInvitation instance, long limit)
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
					if (instance.Inviter == null)
					{
						instance.Inviter = UserDescription.DeserializeLengthDelimited(stream);
					}
					else
					{
						UserDescription.DeserializeLengthDelimited(stream, instance.Inviter);
					}
					continue;
				case 26:
					if (instance.Invitee == null)
					{
						instance.Invitee = UserDescription.DeserializeLengthDelimited(stream);
					}
					else
					{
						UserDescription.DeserializeLengthDelimited(stream, instance.Invitee);
					}
					continue;
				case 32:
					instance.Level = (FriendLevel)ProtocolParser.ReadUInt64(stream);
					continue;
				case 45:
					instance.Program = binaryReader.ReadUInt32();
					continue;
				case 50:
					instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
					continue;
				case 56:
					instance.CreationTimeUs = ProtocolParser.ReadUInt64(stream);
					continue;
				case 64:
					instance.ModifiedTimeUs = ProtocolParser.ReadUInt64(stream);
					continue;
				case 72:
					instance.ExpirationTimeUs = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ReceivedInvitation instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.Id);
			}
			if (instance.HasInviter)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Inviter.GetSerializedSize());
				UserDescription.Serialize(stream, instance.Inviter);
			}
			if (instance.HasInvitee)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Invitee.GetSerializedSize());
				UserDescription.Serialize(stream, instance.Invitee);
			}
			if (instance.HasLevel)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Level);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.Program);
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute item in instance.Attribute)
				{
					stream.WriteByte(50);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, item);
				}
			}
			if (instance.HasCreationTimeUs)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, instance.CreationTimeUs);
			}
			if (instance.HasModifiedTimeUs)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, instance.ModifiedTimeUs);
			}
			if (instance.HasExpirationTimeUs)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, instance.ExpirationTimeUs);
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
			if (HasInviter)
			{
				num++;
				uint serializedSize = Inviter.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasInvitee)
			{
				num++;
				uint serializedSize2 = Invitee.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
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
					uint serializedSize3 = item.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
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
			if (HasExpirationTimeUs)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(ExpirationTimeUs);
			}
			return num;
		}
	}
}
