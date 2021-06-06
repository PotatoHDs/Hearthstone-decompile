using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.connection.v1
{
	public class ConnectionMeteringContentHandles : IProtoBuf
	{
		private List<ContentHandle> _ContentHandle = new List<ContentHandle>();

		public List<ContentHandle> ContentHandle
		{
			get
			{
				return _ContentHandle;
			}
			set
			{
				_ContentHandle = value;
			}
		}

		public List<ContentHandle> ContentHandleList => _ContentHandle;

		public int ContentHandleCount => _ContentHandle.Count;

		public bool IsInitialized => true;

		public void AddContentHandle(ContentHandle val)
		{
			_ContentHandle.Add(val);
		}

		public void ClearContentHandle()
		{
			_ContentHandle.Clear();
		}

		public void SetContentHandle(List<ContentHandle> val)
		{
			ContentHandle = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (ContentHandle item in ContentHandle)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ConnectionMeteringContentHandles connectionMeteringContentHandles = obj as ConnectionMeteringContentHandles;
			if (connectionMeteringContentHandles == null)
			{
				return false;
			}
			if (ContentHandle.Count != connectionMeteringContentHandles.ContentHandle.Count)
			{
				return false;
			}
			for (int i = 0; i < ContentHandle.Count; i++)
			{
				if (!ContentHandle[i].Equals(connectionMeteringContentHandles.ContentHandle[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static ConnectionMeteringContentHandles ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ConnectionMeteringContentHandles>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ConnectionMeteringContentHandles Deserialize(Stream stream, ConnectionMeteringContentHandles instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ConnectionMeteringContentHandles DeserializeLengthDelimited(Stream stream)
		{
			ConnectionMeteringContentHandles connectionMeteringContentHandles = new ConnectionMeteringContentHandles();
			DeserializeLengthDelimited(stream, connectionMeteringContentHandles);
			return connectionMeteringContentHandles;
		}

		public static ConnectionMeteringContentHandles DeserializeLengthDelimited(Stream stream, ConnectionMeteringContentHandles instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ConnectionMeteringContentHandles Deserialize(Stream stream, ConnectionMeteringContentHandles instance, long limit)
		{
			if (instance.ContentHandle == null)
			{
				instance.ContentHandle = new List<ContentHandle>();
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
					instance.ContentHandle.Add(bnet.protocol.ContentHandle.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, ConnectionMeteringContentHandles instance)
		{
			if (instance.ContentHandle.Count <= 0)
			{
				return;
			}
			foreach (ContentHandle item in instance.ContentHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				bnet.protocol.ContentHandle.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (ContentHandle.Count > 0)
			{
				foreach (ContentHandle item in ContentHandle)
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
