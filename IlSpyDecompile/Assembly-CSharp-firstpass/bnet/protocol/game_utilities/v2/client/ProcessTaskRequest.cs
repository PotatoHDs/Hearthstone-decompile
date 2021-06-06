using System.Collections.Generic;
using System.IO;
using bnet.protocol.v2;

namespace bnet.protocol.game_utilities.v2.client
{
	public class ProcessTaskRequest : IProtoBuf
	{
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		private List<bnet.protocol.v2.Attribute> _Payload = new List<bnet.protocol.v2.Attribute>();

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

		public List<bnet.protocol.v2.Attribute> Payload
		{
			get
			{
				return _Payload;
			}
			set
			{
				_Payload = value;
			}
		}

		public List<bnet.protocol.v2.Attribute> PayloadList => _Payload;

		public int PayloadCount => _Payload.Count;

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

		public void AddPayload(bnet.protocol.v2.Attribute val)
		{
			_Payload.Add(val);
		}

		public void ClearPayload()
		{
			_Payload.Clear();
		}

		public void SetPayload(List<bnet.protocol.v2.Attribute> val)
		{
			Payload = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (bnet.protocol.v2.Attribute item in Attribute)
			{
				num ^= item.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute item2 in Payload)
			{
				num ^= item2.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ProcessTaskRequest processTaskRequest = obj as ProcessTaskRequest;
			if (processTaskRequest == null)
			{
				return false;
			}
			if (Attribute.Count != processTaskRequest.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(processTaskRequest.Attribute[i]))
				{
					return false;
				}
			}
			if (Payload.Count != processTaskRequest.Payload.Count)
			{
				return false;
			}
			for (int j = 0; j < Payload.Count; j++)
			{
				if (!Payload[j].Equals(processTaskRequest.Payload[j]))
				{
					return false;
				}
			}
			return true;
		}

		public static ProcessTaskRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ProcessTaskRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProcessTaskRequest Deserialize(Stream stream, ProcessTaskRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProcessTaskRequest DeserializeLengthDelimited(Stream stream)
		{
			ProcessTaskRequest processTaskRequest = new ProcessTaskRequest();
			DeserializeLengthDelimited(stream, processTaskRequest);
			return processTaskRequest;
		}

		public static ProcessTaskRequest DeserializeLengthDelimited(Stream stream, ProcessTaskRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProcessTaskRequest Deserialize(Stream stream, ProcessTaskRequest instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.v2.Attribute>();
			}
			if (instance.Payload == null)
			{
				instance.Payload = new List<bnet.protocol.v2.Attribute>();
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
				case 18:
					instance.Payload.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, ProcessTaskRequest instance)
		{
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute item in instance.Attribute)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, item);
				}
			}
			if (instance.Payload.Count <= 0)
			{
				return;
			}
			foreach (bnet.protocol.v2.Attribute item2 in instance.Payload)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, item2.GetSerializedSize());
				bnet.protocol.v2.Attribute.Serialize(stream, item2);
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
			}
			if (Payload.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute item2 in Payload)
				{
					num++;
					uint serializedSize2 = item2.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
				return num;
			}
			return num;
		}
	}
}
