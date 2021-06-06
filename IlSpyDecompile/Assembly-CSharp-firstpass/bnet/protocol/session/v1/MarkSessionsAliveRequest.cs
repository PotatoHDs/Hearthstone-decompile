using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.session.v1
{
	public class MarkSessionsAliveRequest : IProtoBuf
	{
		private List<SessionIdentifier> _Session = new List<SessionIdentifier>();

		public List<SessionIdentifier> Session
		{
			get
			{
				return _Session;
			}
			set
			{
				_Session = value;
			}
		}

		public List<SessionIdentifier> SessionList => _Session;

		public int SessionCount => _Session.Count;

		public bool IsInitialized => true;

		public void AddSession(SessionIdentifier val)
		{
			_Session.Add(val);
		}

		public void ClearSession()
		{
			_Session.Clear();
		}

		public void SetSession(List<SessionIdentifier> val)
		{
			Session = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (SessionIdentifier item in Session)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			MarkSessionsAliveRequest markSessionsAliveRequest = obj as MarkSessionsAliveRequest;
			if (markSessionsAliveRequest == null)
			{
				return false;
			}
			if (Session.Count != markSessionsAliveRequest.Session.Count)
			{
				return false;
			}
			for (int i = 0; i < Session.Count; i++)
			{
				if (!Session[i].Equals(markSessionsAliveRequest.Session[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static MarkSessionsAliveRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MarkSessionsAliveRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static MarkSessionsAliveRequest Deserialize(Stream stream, MarkSessionsAliveRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static MarkSessionsAliveRequest DeserializeLengthDelimited(Stream stream)
		{
			MarkSessionsAliveRequest markSessionsAliveRequest = new MarkSessionsAliveRequest();
			DeserializeLengthDelimited(stream, markSessionsAliveRequest);
			return markSessionsAliveRequest;
		}

		public static MarkSessionsAliveRequest DeserializeLengthDelimited(Stream stream, MarkSessionsAliveRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static MarkSessionsAliveRequest Deserialize(Stream stream, MarkSessionsAliveRequest instance, long limit)
		{
			if (instance.Session == null)
			{
				instance.Session = new List<SessionIdentifier>();
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
					instance.Session.Add(SessionIdentifier.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, MarkSessionsAliveRequest instance)
		{
			if (instance.Session.Count <= 0)
			{
				return;
			}
			foreach (SessionIdentifier item in instance.Session)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				SessionIdentifier.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Session.Count > 0)
			{
				foreach (SessionIdentifier item in Session)
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
