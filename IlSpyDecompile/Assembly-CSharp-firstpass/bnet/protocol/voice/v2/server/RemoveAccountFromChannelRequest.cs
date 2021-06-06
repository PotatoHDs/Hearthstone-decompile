using System.IO;
using System.Text;

namespace bnet.protocol.voice.v2.server
{
	public class RemoveAccountFromChannelRequest : IProtoBuf
	{
		public bool HasJoinToken;

		private string _JoinToken;

		public string JoinToken
		{
			get
			{
				return _JoinToken;
			}
			set
			{
				_JoinToken = value;
				HasJoinToken = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetJoinToken(string val)
		{
			JoinToken = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasJoinToken)
			{
				num ^= JoinToken.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RemoveAccountFromChannelRequest removeAccountFromChannelRequest = obj as RemoveAccountFromChannelRequest;
			if (removeAccountFromChannelRequest == null)
			{
				return false;
			}
			if (HasJoinToken != removeAccountFromChannelRequest.HasJoinToken || (HasJoinToken && !JoinToken.Equals(removeAccountFromChannelRequest.JoinToken)))
			{
				return false;
			}
			return true;
		}

		public static RemoveAccountFromChannelRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RemoveAccountFromChannelRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RemoveAccountFromChannelRequest Deserialize(Stream stream, RemoveAccountFromChannelRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RemoveAccountFromChannelRequest DeserializeLengthDelimited(Stream stream)
		{
			RemoveAccountFromChannelRequest removeAccountFromChannelRequest = new RemoveAccountFromChannelRequest();
			DeserializeLengthDelimited(stream, removeAccountFromChannelRequest);
			return removeAccountFromChannelRequest;
		}

		public static RemoveAccountFromChannelRequest DeserializeLengthDelimited(Stream stream, RemoveAccountFromChannelRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RemoveAccountFromChannelRequest Deserialize(Stream stream, RemoveAccountFromChannelRequest instance, long limit)
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
					instance.JoinToken = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, RemoveAccountFromChannelRequest instance)
		{
			if (instance.HasJoinToken)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.JoinToken));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasJoinToken)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(JoinToken);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
