using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.v2;

namespace bnet.protocol.channel.v2
{
	public class SendMessageOptions : IProtoBuf
	{
		public bool HasContent;

		private string _Content;

		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

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

		public bool IsInitialized => true;

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

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasContent)
			{
				num ^= Content.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute item in Attribute)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SendMessageOptions sendMessageOptions = obj as SendMessageOptions;
			if (sendMessageOptions == null)
			{
				return false;
			}
			if (HasContent != sendMessageOptions.HasContent || (HasContent && !Content.Equals(sendMessageOptions.Content)))
			{
				return false;
			}
			if (Attribute.Count != sendMessageOptions.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(sendMessageOptions.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static SendMessageOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendMessageOptions>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SendMessageOptions Deserialize(Stream stream, SendMessageOptions instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SendMessageOptions DeserializeLengthDelimited(Stream stream)
		{
			SendMessageOptions sendMessageOptions = new SendMessageOptions();
			DeserializeLengthDelimited(stream, sendMessageOptions);
			return sendMessageOptions;
		}

		public static SendMessageOptions DeserializeLengthDelimited(Stream stream, SendMessageOptions instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SendMessageOptions Deserialize(Stream stream, SendMessageOptions instance, long limit)
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
				case 34:
					instance.Content = ProtocolParser.ReadString(stream);
					continue;
				case 42:
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

		public static void Serialize(Stream stream, SendMessageOptions instance)
		{
			if (instance.HasContent)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Content));
			}
			if (instance.Attribute.Count <= 0)
			{
				return;
			}
			foreach (bnet.protocol.v2.Attribute item in instance.Attribute)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				bnet.protocol.v2.Attribute.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
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
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
