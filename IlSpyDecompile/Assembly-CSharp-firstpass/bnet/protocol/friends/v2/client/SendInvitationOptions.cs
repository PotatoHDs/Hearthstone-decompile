using System.Collections.Generic;
using System.IO;
using bnet.protocol.friends.v2.client.Types;
using bnet.protocol.v2;

namespace bnet.protocol.friends.v2.client
{
	public class SendInvitationOptions : IProtoBuf
	{
		public bool HasLevel;

		private FriendLevel _Level;

		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

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

		public bool IsInitialized => true;

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

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasLevel)
			{
				num ^= Level.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute item in Attribute)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SendInvitationOptions sendInvitationOptions = obj as SendInvitationOptions;
			if (sendInvitationOptions == null)
			{
				return false;
			}
			if (HasLevel != sendInvitationOptions.HasLevel || (HasLevel && !Level.Equals(sendInvitationOptions.Level)))
			{
				return false;
			}
			if (Attribute.Count != sendInvitationOptions.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(sendInvitationOptions.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static SendInvitationOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendInvitationOptions>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SendInvitationOptions Deserialize(Stream stream, SendInvitationOptions instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SendInvitationOptions DeserializeLengthDelimited(Stream stream)
		{
			SendInvitationOptions sendInvitationOptions = new SendInvitationOptions();
			DeserializeLengthDelimited(stream, sendInvitationOptions);
			return sendInvitationOptions;
		}

		public static SendInvitationOptions DeserializeLengthDelimited(Stream stream, SendInvitationOptions instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SendInvitationOptions Deserialize(Stream stream, SendInvitationOptions instance, long limit)
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
					instance.Level = (FriendLevel)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
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

		public static void Serialize(Stream stream, SendInvitationOptions instance)
		{
			if (instance.HasLevel)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Level);
			}
			if (instance.Attribute.Count <= 0)
			{
				return;
			}
			foreach (bnet.protocol.v2.Attribute item in instance.Attribute)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				bnet.protocol.v2.Attribute.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
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
				return num;
			}
			return num;
		}
	}
}
