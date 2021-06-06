using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.voice.v2.server
{
	public class RemoveAllFromChannelRequest : IProtoBuf
	{
		private List<string> _JoinToken = new List<string>();

		public bool HasChannelUri;

		private string _ChannelUri;

		public List<string> JoinToken
		{
			get
			{
				return _JoinToken;
			}
			set
			{
				_JoinToken = value;
			}
		}

		public List<string> JoinTokenList => _JoinToken;

		public int JoinTokenCount => _JoinToken.Count;

		public string ChannelUri
		{
			get
			{
				return _ChannelUri;
			}
			set
			{
				_ChannelUri = value;
				HasChannelUri = value != null;
			}
		}

		public bool IsInitialized => true;

		public void AddJoinToken(string val)
		{
			_JoinToken.Add(val);
		}

		public void ClearJoinToken()
		{
			_JoinToken.Clear();
		}

		public void SetJoinToken(List<string> val)
		{
			JoinToken = val;
		}

		public void SetChannelUri(string val)
		{
			ChannelUri = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (string item in JoinToken)
			{
				num ^= item.GetHashCode();
			}
			if (HasChannelUri)
			{
				num ^= ChannelUri.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RemoveAllFromChannelRequest removeAllFromChannelRequest = obj as RemoveAllFromChannelRequest;
			if (removeAllFromChannelRequest == null)
			{
				return false;
			}
			if (JoinToken.Count != removeAllFromChannelRequest.JoinToken.Count)
			{
				return false;
			}
			for (int i = 0; i < JoinToken.Count; i++)
			{
				if (!JoinToken[i].Equals(removeAllFromChannelRequest.JoinToken[i]))
				{
					return false;
				}
			}
			if (HasChannelUri != removeAllFromChannelRequest.HasChannelUri || (HasChannelUri && !ChannelUri.Equals(removeAllFromChannelRequest.ChannelUri)))
			{
				return false;
			}
			return true;
		}

		public static RemoveAllFromChannelRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RemoveAllFromChannelRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RemoveAllFromChannelRequest Deserialize(Stream stream, RemoveAllFromChannelRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RemoveAllFromChannelRequest DeserializeLengthDelimited(Stream stream)
		{
			RemoveAllFromChannelRequest removeAllFromChannelRequest = new RemoveAllFromChannelRequest();
			DeserializeLengthDelimited(stream, removeAllFromChannelRequest);
			return removeAllFromChannelRequest;
		}

		public static RemoveAllFromChannelRequest DeserializeLengthDelimited(Stream stream, RemoveAllFromChannelRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RemoveAllFromChannelRequest Deserialize(Stream stream, RemoveAllFromChannelRequest instance, long limit)
		{
			if (instance.JoinToken == null)
			{
				instance.JoinToken = new List<string>();
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
					instance.JoinToken.Add(ProtocolParser.ReadString(stream));
					continue;
				case 18:
					instance.ChannelUri = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, RemoveAllFromChannelRequest instance)
		{
			if (instance.JoinToken.Count > 0)
			{
				foreach (string item in instance.JoinToken)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(item));
				}
			}
			if (instance.HasChannelUri)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ChannelUri));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (JoinToken.Count > 0)
			{
				foreach (string item in JoinToken)
				{
					num++;
					uint byteCount = (uint)Encoding.UTF8.GetByteCount(item);
					num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
				}
			}
			if (HasChannelUri)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(ChannelUri);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}
	}
}
