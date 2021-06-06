using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.v2;

namespace bnet.protocol.channel.v2
{
	public class AttributeAssignment : IProtoBuf
	{
		public bool HasMemberId;

		private GameAccountHandle _MemberId;

		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		public GameAccountHandle MemberId
		{
			get
			{
				return _MemberId;
			}
			set
			{
				_MemberId = value;
				HasMemberId = value != null;
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

		public void SetMemberId(GameAccountHandle val)
		{
			MemberId = val;
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
			if (HasMemberId)
			{
				num ^= MemberId.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute item in Attribute)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AttributeAssignment attributeAssignment = obj as AttributeAssignment;
			if (attributeAssignment == null)
			{
				return false;
			}
			if (HasMemberId != attributeAssignment.HasMemberId || (HasMemberId && !MemberId.Equals(attributeAssignment.MemberId)))
			{
				return false;
			}
			if (Attribute.Count != attributeAssignment.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(attributeAssignment.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static AttributeAssignment ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AttributeAssignment>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AttributeAssignment Deserialize(Stream stream, AttributeAssignment instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AttributeAssignment DeserializeLengthDelimited(Stream stream)
		{
			AttributeAssignment attributeAssignment = new AttributeAssignment();
			DeserializeLengthDelimited(stream, attributeAssignment);
			return attributeAssignment;
		}

		public static AttributeAssignment DeserializeLengthDelimited(Stream stream, AttributeAssignment instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AttributeAssignment Deserialize(Stream stream, AttributeAssignment instance, long limit)
		{
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
					if (instance.MemberId == null)
					{
						instance.MemberId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.MemberId);
					}
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

		public static void Serialize(Stream stream, AttributeAssignment instance)
		{
			if (instance.HasMemberId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.MemberId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.MemberId);
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
			if (HasMemberId)
			{
				num++;
				uint serializedSize = MemberId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute item in Attribute)
				{
					num++;
					uint serializedSize2 = item.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
				return num;
			}
			return num;
		}
	}
}
