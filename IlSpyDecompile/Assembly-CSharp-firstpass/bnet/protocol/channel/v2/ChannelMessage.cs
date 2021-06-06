using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;
using bnet.protocol.v2;

namespace bnet.protocol.channel.v2
{
	public class ChannelMessage : IProtoBuf
	{
		public bool HasAuthorId;

		private GameAccountHandle _AuthorId;

		public bool HasContent;

		private string _Content;

		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		public bool HasCreationTime;

		private ulong _CreationTime;

		public GameAccountHandle AuthorId
		{
			get
			{
				return _AuthorId;
			}
			set
			{
				_AuthorId = value;
				HasAuthorId = value != null;
			}
		}

		public string Content
		{
			get
			{
				return _Content;
			}
			set
			{
				_Content = value;
				HasContent = value != null;
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

		public ulong CreationTime
		{
			get
			{
				return _CreationTime;
			}
			set
			{
				_CreationTime = value;
				HasCreationTime = true;
			}
		}

		public bool IsInitialized => true;

		public void SetAuthorId(GameAccountHandle val)
		{
			AuthorId = val;
		}

		public void SetContent(string val)
		{
			Content = val;
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

		public void SetCreationTime(ulong val)
		{
			CreationTime = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAuthorId)
			{
				num ^= AuthorId.GetHashCode();
			}
			if (HasContent)
			{
				num ^= Content.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute item in Attribute)
			{
				num ^= item.GetHashCode();
			}
			if (HasCreationTime)
			{
				num ^= CreationTime.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ChannelMessage channelMessage = obj as ChannelMessage;
			if (channelMessage == null)
			{
				return false;
			}
			if (HasAuthorId != channelMessage.HasAuthorId || (HasAuthorId && !AuthorId.Equals(channelMessage.AuthorId)))
			{
				return false;
			}
			if (HasContent != channelMessage.HasContent || (HasContent && !Content.Equals(channelMessage.Content)))
			{
				return false;
			}
			if (Attribute.Count != channelMessage.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(channelMessage.Attribute[i]))
				{
					return false;
				}
			}
			if (HasCreationTime != channelMessage.HasCreationTime || (HasCreationTime && !CreationTime.Equals(channelMessage.CreationTime)))
			{
				return false;
			}
			return true;
		}

		public static ChannelMessage ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelMessage>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ChannelMessage Deserialize(Stream stream, ChannelMessage instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ChannelMessage DeserializeLengthDelimited(Stream stream)
		{
			ChannelMessage channelMessage = new ChannelMessage();
			DeserializeLengthDelimited(stream, channelMessage);
			return channelMessage;
		}

		public static ChannelMessage DeserializeLengthDelimited(Stream stream, ChannelMessage instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ChannelMessage Deserialize(Stream stream, ChannelMessage instance, long limit)
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
					if (instance.AuthorId == null)
					{
						instance.AuthorId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.AuthorId);
					}
					continue;
				case 26:
					instance.Content = ProtocolParser.ReadString(stream);
					continue;
				case 34:
					instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
					continue;
				case 40:
					instance.CreationTime = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ChannelMessage instance)
		{
			if (instance.HasAuthorId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AuthorId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AuthorId);
			}
			if (instance.HasContent)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Content));
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute item in instance.Attribute)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, item);
				}
			}
			if (instance.HasCreationTime)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, instance.CreationTime);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAuthorId)
			{
				num++;
				uint serializedSize = AuthorId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasContent)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Content);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute item in Attribute)
				{
					num++;
					uint serializedSize2 = item.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (HasCreationTime)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(CreationTime);
			}
			return num;
		}
	}
}
