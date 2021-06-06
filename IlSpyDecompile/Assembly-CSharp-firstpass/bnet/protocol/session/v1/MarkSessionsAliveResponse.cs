using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.session.v1
{
	public class MarkSessionsAliveResponse : IProtoBuf
	{
		private List<SessionIdentifier> _FailedSession = new List<SessionIdentifier>();

		public List<SessionIdentifier> FailedSession
		{
			get
			{
				return _FailedSession;
			}
			set
			{
				_FailedSession = value;
			}
		}

		public List<SessionIdentifier> FailedSessionList => _FailedSession;

		public int FailedSessionCount => _FailedSession.Count;

		public bool IsInitialized => true;

		public void AddFailedSession(SessionIdentifier val)
		{
			_FailedSession.Add(val);
		}

		public void ClearFailedSession()
		{
			_FailedSession.Clear();
		}

		public void SetFailedSession(List<SessionIdentifier> val)
		{
			FailedSession = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (SessionIdentifier item in FailedSession)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			MarkSessionsAliveResponse markSessionsAliveResponse = obj as MarkSessionsAliveResponse;
			if (markSessionsAliveResponse == null)
			{
				return false;
			}
			if (FailedSession.Count != markSessionsAliveResponse.FailedSession.Count)
			{
				return false;
			}
			for (int i = 0; i < FailedSession.Count; i++)
			{
				if (!FailedSession[i].Equals(markSessionsAliveResponse.FailedSession[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static MarkSessionsAliveResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MarkSessionsAliveResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static MarkSessionsAliveResponse Deserialize(Stream stream, MarkSessionsAliveResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static MarkSessionsAliveResponse DeserializeLengthDelimited(Stream stream)
		{
			MarkSessionsAliveResponse markSessionsAliveResponse = new MarkSessionsAliveResponse();
			DeserializeLengthDelimited(stream, markSessionsAliveResponse);
			return markSessionsAliveResponse;
		}

		public static MarkSessionsAliveResponse DeserializeLengthDelimited(Stream stream, MarkSessionsAliveResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static MarkSessionsAliveResponse Deserialize(Stream stream, MarkSessionsAliveResponse instance, long limit)
		{
			if (instance.FailedSession == null)
			{
				instance.FailedSession = new List<SessionIdentifier>();
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
					instance.FailedSession.Add(SessionIdentifier.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, MarkSessionsAliveResponse instance)
		{
			if (instance.FailedSession.Count <= 0)
			{
				return;
			}
			foreach (SessionIdentifier item in instance.FailedSession)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				SessionIdentifier.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (FailedSession.Count > 0)
			{
				foreach (SessionIdentifier item in FailedSession)
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
