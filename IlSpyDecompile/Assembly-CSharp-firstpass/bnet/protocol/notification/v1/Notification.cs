using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.notification.v1
{
	public class Notification : IProtoBuf
	{
		public bool HasSenderId;

		private EntityId _SenderId;

		private List<Attribute> _Attribute = new List<Attribute>();

		public bool HasSenderAccountId;

		private EntityId _SenderAccountId;

		public bool HasTargetAccountId;

		private EntityId _TargetAccountId;

		public bool HasSenderBattleTag;

		private string _SenderBattleTag;

		public bool HasTargetBattleTag;

		private string _TargetBattleTag;

		public bool HasPeer;

		private ProcessId _Peer;

		public bool HasForwardingIdentity;

		private bnet.protocol.account.v1.Identity _ForwardingIdentity;

		public EntityId SenderId
		{
			get
			{
				return _SenderId;
			}
			set
			{
				_SenderId = value;
				HasSenderId = value != null;
			}
		}

		public EntityId TargetId { get; set; }

		public string Type { get; set; }

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

		public EntityId SenderAccountId
		{
			get
			{
				return _SenderAccountId;
			}
			set
			{
				_SenderAccountId = value;
				HasSenderAccountId = value != null;
			}
		}

		public EntityId TargetAccountId
		{
			get
			{
				return _TargetAccountId;
			}
			set
			{
				_TargetAccountId = value;
				HasTargetAccountId = value != null;
			}
		}

		public string SenderBattleTag
		{
			get
			{
				return _SenderBattleTag;
			}
			set
			{
				_SenderBattleTag = value;
				HasSenderBattleTag = value != null;
			}
		}

		public string TargetBattleTag
		{
			get
			{
				return _TargetBattleTag;
			}
			set
			{
				_TargetBattleTag = value;
				HasTargetBattleTag = value != null;
			}
		}

		public ProcessId Peer
		{
			get
			{
				return _Peer;
			}
			set
			{
				_Peer = value;
				HasPeer = value != null;
			}
		}

		public bnet.protocol.account.v1.Identity ForwardingIdentity
		{
			get
			{
				return _ForwardingIdentity;
			}
			set
			{
				_ForwardingIdentity = value;
				HasForwardingIdentity = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetSenderId(EntityId val)
		{
			SenderId = val;
		}

		public void SetTargetId(EntityId val)
		{
			TargetId = val;
		}

		public void SetType(string val)
		{
			Type = val;
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

		public void SetSenderAccountId(EntityId val)
		{
			SenderAccountId = val;
		}

		public void SetTargetAccountId(EntityId val)
		{
			TargetAccountId = val;
		}

		public void SetSenderBattleTag(string val)
		{
			SenderBattleTag = val;
		}

		public void SetTargetBattleTag(string val)
		{
			TargetBattleTag = val;
		}

		public void SetPeer(ProcessId val)
		{
			Peer = val;
		}

		public void SetForwardingIdentity(bnet.protocol.account.v1.Identity val)
		{
			ForwardingIdentity = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasSenderId)
			{
				num ^= SenderId.GetHashCode();
			}
			num ^= TargetId.GetHashCode();
			num ^= Type.GetHashCode();
			foreach (Attribute item in Attribute)
			{
				num ^= item.GetHashCode();
			}
			if (HasSenderAccountId)
			{
				num ^= SenderAccountId.GetHashCode();
			}
			if (HasTargetAccountId)
			{
				num ^= TargetAccountId.GetHashCode();
			}
			if (HasSenderBattleTag)
			{
				num ^= SenderBattleTag.GetHashCode();
			}
			if (HasTargetBattleTag)
			{
				num ^= TargetBattleTag.GetHashCode();
			}
			if (HasPeer)
			{
				num ^= Peer.GetHashCode();
			}
			if (HasForwardingIdentity)
			{
				num ^= ForwardingIdentity.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Notification notification = obj as Notification;
			if (notification == null)
			{
				return false;
			}
			if (HasSenderId != notification.HasSenderId || (HasSenderId && !SenderId.Equals(notification.SenderId)))
			{
				return false;
			}
			if (!TargetId.Equals(notification.TargetId))
			{
				return false;
			}
			if (!Type.Equals(notification.Type))
			{
				return false;
			}
			if (Attribute.Count != notification.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(notification.Attribute[i]))
				{
					return false;
				}
			}
			if (HasSenderAccountId != notification.HasSenderAccountId || (HasSenderAccountId && !SenderAccountId.Equals(notification.SenderAccountId)))
			{
				return false;
			}
			if (HasTargetAccountId != notification.HasTargetAccountId || (HasTargetAccountId && !TargetAccountId.Equals(notification.TargetAccountId)))
			{
				return false;
			}
			if (HasSenderBattleTag != notification.HasSenderBattleTag || (HasSenderBattleTag && !SenderBattleTag.Equals(notification.SenderBattleTag)))
			{
				return false;
			}
			if (HasTargetBattleTag != notification.HasTargetBattleTag || (HasTargetBattleTag && !TargetBattleTag.Equals(notification.TargetBattleTag)))
			{
				return false;
			}
			if (HasPeer != notification.HasPeer || (HasPeer && !Peer.Equals(notification.Peer)))
			{
				return false;
			}
			if (HasForwardingIdentity != notification.HasForwardingIdentity || (HasForwardingIdentity && !ForwardingIdentity.Equals(notification.ForwardingIdentity)))
			{
				return false;
			}
			return true;
		}

		public static Notification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Notification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Notification Deserialize(Stream stream, Notification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Notification DeserializeLengthDelimited(Stream stream)
		{
			Notification notification = new Notification();
			DeserializeLengthDelimited(stream, notification);
			return notification;
		}

		public static Notification DeserializeLengthDelimited(Stream stream, Notification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Notification Deserialize(Stream stream, Notification instance, long limit)
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
					if (instance.SenderId == null)
					{
						instance.SenderId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.SenderId);
					}
					continue;
				case 18:
					if (instance.TargetId == null)
					{
						instance.TargetId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.TargetId);
					}
					continue;
				case 26:
					instance.Type = ProtocolParser.ReadString(stream);
					continue;
				case 34:
					instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
					continue;
				case 42:
					if (instance.SenderAccountId == null)
					{
						instance.SenderAccountId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.SenderAccountId);
					}
					continue;
				case 50:
					if (instance.TargetAccountId == null)
					{
						instance.TargetAccountId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.TargetAccountId);
					}
					continue;
				case 58:
					instance.SenderBattleTag = ProtocolParser.ReadString(stream);
					continue;
				case 66:
					instance.TargetBattleTag = ProtocolParser.ReadString(stream);
					continue;
				case 74:
					if (instance.Peer == null)
					{
						instance.Peer = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.Peer);
					}
					continue;
				case 82:
					if (instance.ForwardingIdentity == null)
					{
						instance.ForwardingIdentity = bnet.protocol.account.v1.Identity.DeserializeLengthDelimited(stream);
					}
					else
					{
						bnet.protocol.account.v1.Identity.DeserializeLengthDelimited(stream, instance.ForwardingIdentity);
					}
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

		public static void Serialize(Stream stream, Notification instance)
		{
			if (instance.HasSenderId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.SenderId.GetSerializedSize());
				EntityId.Serialize(stream, instance.SenderId);
			}
			if (instance.TargetId == null)
			{
				throw new ArgumentNullException("TargetId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
			EntityId.Serialize(stream, instance.TargetId);
			if (instance.Type == null)
			{
				throw new ArgumentNullException("Type", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Type));
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute item in instance.Attribute)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, item);
				}
			}
			if (instance.HasSenderAccountId)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.SenderAccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.SenderAccountId);
			}
			if (instance.HasTargetAccountId)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.TargetAccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.TargetAccountId);
			}
			if (instance.HasSenderBattleTag)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SenderBattleTag));
			}
			if (instance.HasTargetBattleTag)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TargetBattleTag));
			}
			if (instance.HasPeer)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteUInt32(stream, instance.Peer.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Peer);
			}
			if (instance.HasForwardingIdentity)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteUInt32(stream, instance.ForwardingIdentity.GetSerializedSize());
				bnet.protocol.account.v1.Identity.Serialize(stream, instance.ForwardingIdentity);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasSenderId)
			{
				num++;
				uint serializedSize = SenderId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = TargetId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(Type);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (Attribute.Count > 0)
			{
				foreach (Attribute item in Attribute)
				{
					num++;
					uint serializedSize3 = item.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (HasSenderAccountId)
			{
				num++;
				uint serializedSize4 = SenderAccountId.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (HasTargetAccountId)
			{
				num++;
				uint serializedSize5 = TargetAccountId.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			if (HasSenderBattleTag)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(SenderBattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasTargetBattleTag)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(TargetBattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasPeer)
			{
				num++;
				uint serializedSize6 = Peer.GetSerializedSize();
				num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
			}
			if (HasForwardingIdentity)
			{
				num++;
				uint serializedSize7 = ForwardingIdentity.GetSerializedSize();
				num += serializedSize7 + ProtocolParser.SizeOfUInt32(serializedSize7);
			}
			return num + 2;
		}
	}
}
