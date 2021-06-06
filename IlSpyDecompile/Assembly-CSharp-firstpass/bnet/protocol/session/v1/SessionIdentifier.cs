using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.session.v1
{
	public class SessionIdentifier : IProtoBuf
	{
		public bool HasGameAccount;

		private GameAccountHandle _GameAccount;

		public bool HasSessionId;

		private string _SessionId;

		public GameAccountHandle GameAccount
		{
			get
			{
				return _GameAccount;
			}
			set
			{
				_GameAccount = value;
				HasGameAccount = value != null;
			}
		}

		public string SessionId
		{
			get
			{
				return _SessionId;
			}
			set
			{
				_SessionId = value;
				HasSessionId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetGameAccount(GameAccountHandle val)
		{
			GameAccount = val;
		}

		public void SetSessionId(string val)
		{
			SessionId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasGameAccount)
			{
				num ^= GameAccount.GetHashCode();
			}
			if (HasSessionId)
			{
				num ^= SessionId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SessionIdentifier sessionIdentifier = obj as SessionIdentifier;
			if (sessionIdentifier == null)
			{
				return false;
			}
			if (HasGameAccount != sessionIdentifier.HasGameAccount || (HasGameAccount && !GameAccount.Equals(sessionIdentifier.GameAccount)))
			{
				return false;
			}
			if (HasSessionId != sessionIdentifier.HasSessionId || (HasSessionId && !SessionId.Equals(sessionIdentifier.SessionId)))
			{
				return false;
			}
			return true;
		}

		public static SessionIdentifier ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SessionIdentifier>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SessionIdentifier Deserialize(Stream stream, SessionIdentifier instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SessionIdentifier DeserializeLengthDelimited(Stream stream)
		{
			SessionIdentifier sessionIdentifier = new SessionIdentifier();
			DeserializeLengthDelimited(stream, sessionIdentifier);
			return sessionIdentifier;
		}

		public static SessionIdentifier DeserializeLengthDelimited(Stream stream, SessionIdentifier instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SessionIdentifier Deserialize(Stream stream, SessionIdentifier instance, long limit)
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
					if (instance.GameAccount == null)
					{
						instance.GameAccount = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.GameAccount);
					}
					continue;
				case 18:
					instance.SessionId = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, SessionIdentifier instance)
		{
			if (instance.HasGameAccount)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameAccount.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.GameAccount);
			}
			if (instance.HasSessionId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SessionId));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasGameAccount)
			{
				num++;
				uint serializedSize = GameAccount.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasSessionId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(SessionId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
