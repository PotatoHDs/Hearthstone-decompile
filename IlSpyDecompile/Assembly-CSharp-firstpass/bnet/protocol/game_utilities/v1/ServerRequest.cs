using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.game_utilities.v1
{
	public class ServerRequest : IProtoBuf
	{
		private List<Attribute> _Attribute = new List<Attribute>();

		public bool HasHost;

		private ProcessId _Host;

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

		public uint Program { get; set; }

		public ProcessId Host
		{
			get
			{
				return _Host;
			}
			set
			{
				_Host = value;
				HasHost = value != null;
			}
		}

		public bool IsInitialized => true;

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

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public void SetHost(ProcessId val)
		{
			Host = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (Attribute item in Attribute)
			{
				num ^= item.GetHashCode();
			}
			num ^= Program.GetHashCode();
			if (HasHost)
			{
				num ^= Host.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ServerRequest serverRequest = obj as ServerRequest;
			if (serverRequest == null)
			{
				return false;
			}
			if (Attribute.Count != serverRequest.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(serverRequest.Attribute[i]))
				{
					return false;
				}
			}
			if (!Program.Equals(serverRequest.Program))
			{
				return false;
			}
			if (HasHost != serverRequest.HasHost || (HasHost && !Host.Equals(serverRequest.Host)))
			{
				return false;
			}
			return true;
		}

		public static ServerRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ServerRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ServerRequest Deserialize(Stream stream, ServerRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ServerRequest DeserializeLengthDelimited(Stream stream)
		{
			ServerRequest serverRequest = new ServerRequest();
			DeserializeLengthDelimited(stream, serverRequest);
			return serverRequest;
		}

		public static ServerRequest DeserializeLengthDelimited(Stream stream, ServerRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ServerRequest Deserialize(Stream stream, ServerRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
					continue;
				case 21:
					instance.Program = binaryReader.ReadUInt32();
					continue;
				case 26:
					if (instance.Host == null)
					{
						instance.Host = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.Host);
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

		public static void Serialize(Stream stream, ServerRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute item in instance.Attribute)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, item);
				}
			}
			stream.WriteByte(21);
			binaryWriter.Write(instance.Program);
			if (instance.HasHost)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Attribute.Count > 0)
			{
				foreach (Attribute item in Attribute)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += 4;
			if (HasHost)
			{
				num++;
				uint serializedSize2 = Host.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num + 1;
		}
	}
}
