using System.IO;

namespace bnet.protocol.authentication.v1
{
	public class ModuleMessageRequest : IProtoBuf
	{
		public bool HasMessage;

		private byte[] _Message;

		public int ModuleId { get; set; }

		public byte[] Message
		{
			get
			{
				return _Message;
			}
			set
			{
				_Message = value;
				HasMessage = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetModuleId(int val)
		{
			ModuleId = val;
		}

		public void SetMessage(byte[] val)
		{
			Message = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= ModuleId.GetHashCode();
			if (HasMessage)
			{
				hashCode ^= Message.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ModuleMessageRequest moduleMessageRequest = obj as ModuleMessageRequest;
			if (moduleMessageRequest == null)
			{
				return false;
			}
			if (!ModuleId.Equals(moduleMessageRequest.ModuleId))
			{
				return false;
			}
			if (HasMessage != moduleMessageRequest.HasMessage || (HasMessage && !Message.Equals(moduleMessageRequest.Message)))
			{
				return false;
			}
			return true;
		}

		public static ModuleMessageRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ModuleMessageRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ModuleMessageRequest Deserialize(Stream stream, ModuleMessageRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ModuleMessageRequest DeserializeLengthDelimited(Stream stream)
		{
			ModuleMessageRequest moduleMessageRequest = new ModuleMessageRequest();
			DeserializeLengthDelimited(stream, moduleMessageRequest);
			return moduleMessageRequest;
		}

		public static ModuleMessageRequest DeserializeLengthDelimited(Stream stream, ModuleMessageRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ModuleMessageRequest Deserialize(Stream stream, ModuleMessageRequest instance, long limit)
		{
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
					instance.ModuleId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					instance.Message = ProtocolParser.ReadBytes(stream);
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

		public static void Serialize(Stream stream, ModuleMessageRequest instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ModuleId);
			if (instance.HasMessage)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, instance.Message);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)ModuleId);
			if (HasMessage)
			{
				num++;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(Message.Length) + Message.Length);
			}
			return num + 1;
		}
	}
}
