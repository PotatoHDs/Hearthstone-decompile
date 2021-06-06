using System.Collections.Generic;
using System.IO;
using bnet.protocol.v2;

namespace bnet.protocol.friends.v2.client
{
	public class UpdateFriendStateOptions : IProtoBuf
	{
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

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
			foreach (bnet.protocol.v2.Attribute item in Attribute)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UpdateFriendStateOptions updateFriendStateOptions = obj as UpdateFriendStateOptions;
			if (updateFriendStateOptions == null)
			{
				return false;
			}
			if (Attribute.Count != updateFriendStateOptions.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(updateFriendStateOptions.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static UpdateFriendStateOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateFriendStateOptions>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UpdateFriendStateOptions Deserialize(Stream stream, UpdateFriendStateOptions instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UpdateFriendStateOptions DeserializeLengthDelimited(Stream stream)
		{
			UpdateFriendStateOptions updateFriendStateOptions = new UpdateFriendStateOptions();
			DeserializeLengthDelimited(stream, updateFriendStateOptions);
			return updateFriendStateOptions;
		}

		public static UpdateFriendStateOptions DeserializeLengthDelimited(Stream stream, UpdateFriendStateOptions instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UpdateFriendStateOptions Deserialize(Stream stream, UpdateFriendStateOptions instance, long limit)
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

		public static void Serialize(Stream stream, UpdateFriendStateOptions instance)
		{
			if (instance.Attribute.Count <= 0)
			{
				return;
			}
			foreach (bnet.protocol.v2.Attribute item in instance.Attribute)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				bnet.protocol.v2.Attribute.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
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
