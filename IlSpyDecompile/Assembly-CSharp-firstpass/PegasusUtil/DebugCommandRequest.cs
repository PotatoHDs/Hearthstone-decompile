using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	public class DebugCommandRequest : IProtoBuf
	{
		public enum PacketID
		{
			ID = 323,
			System = 0
		}

		private List<string> _Args = new List<string>();

		public string Command { get; set; }

		public List<string> Args
		{
			get
			{
				return _Args;
			}
			set
			{
				_Args = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Command.GetHashCode();
			foreach (string arg in Args)
			{
				hashCode ^= arg.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			DebugCommandRequest debugCommandRequest = obj as DebugCommandRequest;
			if (debugCommandRequest == null)
			{
				return false;
			}
			if (!Command.Equals(debugCommandRequest.Command))
			{
				return false;
			}
			if (Args.Count != debugCommandRequest.Args.Count)
			{
				return false;
			}
			for (int i = 0; i < Args.Count; i++)
			{
				if (!Args[i].Equals(debugCommandRequest.Args[i]))
				{
					return false;
				}
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DebugCommandRequest Deserialize(Stream stream, DebugCommandRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DebugCommandRequest DeserializeLengthDelimited(Stream stream)
		{
			DebugCommandRequest debugCommandRequest = new DebugCommandRequest();
			DeserializeLengthDelimited(stream, debugCommandRequest);
			return debugCommandRequest;
		}

		public static DebugCommandRequest DeserializeLengthDelimited(Stream stream, DebugCommandRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DebugCommandRequest Deserialize(Stream stream, DebugCommandRequest instance, long limit)
		{
			if (instance.Args == null)
			{
				instance.Args = new List<string>();
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
					instance.Command = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					instance.Args.Add(ProtocolParser.ReadString(stream));
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

		public static void Serialize(Stream stream, DebugCommandRequest instance)
		{
			if (instance.Command == null)
			{
				throw new ArgumentNullException("Command", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Command));
			if (instance.Args.Count <= 0)
			{
				return;
			}
			foreach (string arg in instance.Args)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(arg));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(Command);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (Args.Count > 0)
			{
				foreach (string arg in Args)
				{
					num++;
					uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(arg);
					num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
				}
			}
			return num + 1;
		}
	}
}
