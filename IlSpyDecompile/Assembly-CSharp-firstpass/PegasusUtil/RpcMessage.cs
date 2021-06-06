using System;
using System.IO;

namespace PegasusUtil
{
	public class RpcMessage : IProtoBuf
	{
		public bool HasMessageBody;

		private byte[] _MessageBody;

		public RpcHeader RpcHeader { get; set; }

		public byte[] MessageBody
		{
			get
			{
				return _MessageBody;
			}
			set
			{
				_MessageBody = value;
				HasMessageBody = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= RpcHeader.GetHashCode();
			if (HasMessageBody)
			{
				hashCode ^= MessageBody.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			RpcMessage rpcMessage = obj as RpcMessage;
			if (rpcMessage == null)
			{
				return false;
			}
			if (!RpcHeader.Equals(rpcMessage.RpcHeader))
			{
				return false;
			}
			if (HasMessageBody != rpcMessage.HasMessageBody || (HasMessageBody && !MessageBody.Equals(rpcMessage.MessageBody)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RpcMessage Deserialize(Stream stream, RpcMessage instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RpcMessage DeserializeLengthDelimited(Stream stream)
		{
			RpcMessage rpcMessage = new RpcMessage();
			DeserializeLengthDelimited(stream, rpcMessage);
			return rpcMessage;
		}

		public static RpcMessage DeserializeLengthDelimited(Stream stream, RpcMessage instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RpcMessage Deserialize(Stream stream, RpcMessage instance, long limit)
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
					if (instance.RpcHeader == null)
					{
						instance.RpcHeader = RpcHeader.DeserializeLengthDelimited(stream);
					}
					else
					{
						RpcHeader.DeserializeLengthDelimited(stream, instance.RpcHeader);
					}
					continue;
				case 18:
					instance.MessageBody = ProtocolParser.ReadBytes(stream);
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

		public static void Serialize(Stream stream, RpcMessage instance)
		{
			if (instance.RpcHeader == null)
			{
				throw new ArgumentNullException("RpcHeader", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.RpcHeader.GetSerializedSize());
			RpcHeader.Serialize(stream, instance.RpcHeader);
			if (instance.HasMessageBody)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, instance.MessageBody);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = RpcHeader.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasMessageBody)
			{
				num++;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(MessageBody.Length) + MessageBody.Length);
			}
			return num + 1;
		}
	}
}
