using System;
using System.IO;

namespace bnet.protocol.authentication.v1
{
	public class ModuleLoadRequest : IProtoBuf
	{
		public bool HasMessage;

		private byte[] _Message;

		public ContentHandle ModuleHandle { get; set; }

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

		public void SetModuleHandle(ContentHandle val)
		{
			ModuleHandle = val;
		}

		public void SetMessage(byte[] val)
		{
			Message = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= ModuleHandle.GetHashCode();
			if (HasMessage)
			{
				hashCode ^= Message.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ModuleLoadRequest moduleLoadRequest = obj as ModuleLoadRequest;
			if (moduleLoadRequest == null)
			{
				return false;
			}
			if (!ModuleHandle.Equals(moduleLoadRequest.ModuleHandle))
			{
				return false;
			}
			if (HasMessage != moduleLoadRequest.HasMessage || (HasMessage && !Message.Equals(moduleLoadRequest.Message)))
			{
				return false;
			}
			return true;
		}

		public static ModuleLoadRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ModuleLoadRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ModuleLoadRequest Deserialize(Stream stream, ModuleLoadRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ModuleLoadRequest DeserializeLengthDelimited(Stream stream)
		{
			ModuleLoadRequest moduleLoadRequest = new ModuleLoadRequest();
			DeserializeLengthDelimited(stream, moduleLoadRequest);
			return moduleLoadRequest;
		}

		public static ModuleLoadRequest DeserializeLengthDelimited(Stream stream, ModuleLoadRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ModuleLoadRequest Deserialize(Stream stream, ModuleLoadRequest instance, long limit)
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
				case 10:
					if (instance.ModuleHandle == null)
					{
						instance.ModuleHandle = ContentHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						ContentHandle.DeserializeLengthDelimited(stream, instance.ModuleHandle);
					}
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

		public static void Serialize(Stream stream, ModuleLoadRequest instance)
		{
			if (instance.ModuleHandle == null)
			{
				throw new ArgumentNullException("ModuleHandle", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.ModuleHandle.GetSerializedSize());
			ContentHandle.Serialize(stream, instance.ModuleHandle);
			if (instance.HasMessage)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, instance.Message);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = ModuleHandle.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasMessage)
			{
				num++;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(Message.Length) + Message.Length);
			}
			return num + 1;
		}
	}
}
