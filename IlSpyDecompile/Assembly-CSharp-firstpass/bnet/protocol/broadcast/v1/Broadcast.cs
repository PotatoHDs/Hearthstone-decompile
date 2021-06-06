using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.broadcast.v1
{
	public class Broadcast : IProtoBuf
	{
		private List<Attribute> _PayloadAttribute = new List<Attribute>();

		private List<BroadcastFilter> _Filter = new List<BroadcastFilter>();

		public bool HasSendOnce;

		private bool _SendOnce;

		public string Name { get; set; }

		public List<Attribute> PayloadAttribute
		{
			get
			{
				return _PayloadAttribute;
			}
			set
			{
				_PayloadAttribute = value;
			}
		}

		public List<Attribute> PayloadAttributeList => _PayloadAttribute;

		public int PayloadAttributeCount => _PayloadAttribute.Count;

		public List<BroadcastFilter> Filter
		{
			get
			{
				return _Filter;
			}
			set
			{
				_Filter = value;
			}
		}

		public List<BroadcastFilter> FilterList => _Filter;

		public int FilterCount => _Filter.Count;

		public bool SendOnce
		{
			get
			{
				return _SendOnce;
			}
			set
			{
				_SendOnce = value;
				HasSendOnce = true;
			}
		}

		public bool IsInitialized => true;

		public void SetName(string val)
		{
			Name = val;
		}

		public void AddPayloadAttribute(Attribute val)
		{
			_PayloadAttribute.Add(val);
		}

		public void ClearPayloadAttribute()
		{
			_PayloadAttribute.Clear();
		}

		public void SetPayloadAttribute(List<Attribute> val)
		{
			PayloadAttribute = val;
		}

		public void AddFilter(BroadcastFilter val)
		{
			_Filter.Add(val);
		}

		public void ClearFilter()
		{
			_Filter.Clear();
		}

		public void SetFilter(List<BroadcastFilter> val)
		{
			Filter = val;
		}

		public void SetSendOnce(bool val)
		{
			SendOnce = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Name.GetHashCode();
			foreach (Attribute item in PayloadAttribute)
			{
				hashCode ^= item.GetHashCode();
			}
			foreach (BroadcastFilter item2 in Filter)
			{
				hashCode ^= item2.GetHashCode();
			}
			if (HasSendOnce)
			{
				hashCode ^= SendOnce.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			Broadcast broadcast = obj as Broadcast;
			if (broadcast == null)
			{
				return false;
			}
			if (!Name.Equals(broadcast.Name))
			{
				return false;
			}
			if (PayloadAttribute.Count != broadcast.PayloadAttribute.Count)
			{
				return false;
			}
			for (int i = 0; i < PayloadAttribute.Count; i++)
			{
				if (!PayloadAttribute[i].Equals(broadcast.PayloadAttribute[i]))
				{
					return false;
				}
			}
			if (Filter.Count != broadcast.Filter.Count)
			{
				return false;
			}
			for (int j = 0; j < Filter.Count; j++)
			{
				if (!Filter[j].Equals(broadcast.Filter[j]))
				{
					return false;
				}
			}
			if (HasSendOnce != broadcast.HasSendOnce || (HasSendOnce && !SendOnce.Equals(broadcast.SendOnce)))
			{
				return false;
			}
			return true;
		}

		public static Broadcast ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Broadcast>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Broadcast Deserialize(Stream stream, Broadcast instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Broadcast DeserializeLengthDelimited(Stream stream)
		{
			Broadcast broadcast = new Broadcast();
			DeserializeLengthDelimited(stream, broadcast);
			return broadcast;
		}

		public static Broadcast DeserializeLengthDelimited(Stream stream, Broadcast instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Broadcast Deserialize(Stream stream, Broadcast instance, long limit)
		{
			if (instance.PayloadAttribute == null)
			{
				instance.PayloadAttribute = new List<Attribute>();
			}
			if (instance.Filter == null)
			{
				instance.Filter = new List<BroadcastFilter>();
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
					instance.Name = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					instance.PayloadAttribute.Add(Attribute.DeserializeLengthDelimited(stream));
					continue;
				case 26:
					instance.Filter.Add(BroadcastFilter.DeserializeLengthDelimited(stream));
					continue;
				case 32:
					instance.SendOnce = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, Broadcast instance)
		{
			if (instance.Name == null)
			{
				throw new ArgumentNullException("Name", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			if (instance.PayloadAttribute.Count > 0)
			{
				foreach (Attribute item in instance.PayloadAttribute)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					Attribute.Serialize(stream, item);
				}
			}
			if (instance.Filter.Count > 0)
			{
				foreach (BroadcastFilter item2 in instance.Filter)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, item2.GetSerializedSize());
					BroadcastFilter.Serialize(stream, item2);
				}
			}
			if (instance.HasSendOnce)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.SendOnce);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (PayloadAttribute.Count > 0)
			{
				foreach (Attribute item in PayloadAttribute)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (Filter.Count > 0)
			{
				foreach (BroadcastFilter item2 in Filter)
				{
					num++;
					uint serializedSize2 = item2.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (HasSendOnce)
			{
				num++;
				num++;
			}
			return num + 1;
		}
	}
}
