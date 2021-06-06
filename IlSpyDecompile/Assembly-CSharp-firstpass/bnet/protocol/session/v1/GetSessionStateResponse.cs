using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.session.v1
{
	public class GetSessionStateResponse : IProtoBuf
	{
		public bool HasHandle;

		private GameAccountHandle _Handle;

		public bool HasSession;

		private SessionState _Session;

		public GameAccountHandle Handle
		{
			get
			{
				return _Handle;
			}
			set
			{
				_Handle = value;
				HasHandle = value != null;
			}
		}

		public SessionState Session
		{
			get
			{
				return _Session;
			}
			set
			{
				_Session = value;
				HasSession = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetHandle(GameAccountHandle val)
		{
			Handle = val;
		}

		public void SetSession(SessionState val)
		{
			Session = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasHandle)
			{
				num ^= Handle.GetHashCode();
			}
			if (HasSession)
			{
				num ^= Session.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetSessionStateResponse getSessionStateResponse = obj as GetSessionStateResponse;
			if (getSessionStateResponse == null)
			{
				return false;
			}
			if (HasHandle != getSessionStateResponse.HasHandle || (HasHandle && !Handle.Equals(getSessionStateResponse.Handle)))
			{
				return false;
			}
			if (HasSession != getSessionStateResponse.HasSession || (HasSession && !Session.Equals(getSessionStateResponse.Session)))
			{
				return false;
			}
			return true;
		}

		public static GetSessionStateResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetSessionStateResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetSessionStateResponse Deserialize(Stream stream, GetSessionStateResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetSessionStateResponse DeserializeLengthDelimited(Stream stream)
		{
			GetSessionStateResponse getSessionStateResponse = new GetSessionStateResponse();
			DeserializeLengthDelimited(stream, getSessionStateResponse);
			return getSessionStateResponse;
		}

		public static GetSessionStateResponse DeserializeLengthDelimited(Stream stream, GetSessionStateResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetSessionStateResponse Deserialize(Stream stream, GetSessionStateResponse instance, long limit)
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
					if (instance.Handle == null)
					{
						instance.Handle = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.Handle);
					}
					continue;
				case 18:
					if (instance.Session == null)
					{
						instance.Session = SessionState.DeserializeLengthDelimited(stream);
					}
					else
					{
						SessionState.DeserializeLengthDelimited(stream, instance.Session);
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

		public static void Serialize(Stream stream, GetSessionStateResponse instance)
		{
			if (instance.HasHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Handle.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.Handle);
			}
			if (instance.HasSession)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Session.GetSerializedSize());
				SessionState.Serialize(stream, instance.Session);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasHandle)
			{
				num++;
				uint serializedSize = Handle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasSession)
			{
				num++;
				uint serializedSize2 = Session.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
