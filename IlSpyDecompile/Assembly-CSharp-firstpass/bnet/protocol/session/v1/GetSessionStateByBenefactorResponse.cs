using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.session.v1
{
	public class GetSessionStateByBenefactorResponse : IProtoBuf
	{
		public bool HasBenefactorHandle;

		private GameAccountHandle _BenefactorHandle;

		private List<SessionState> _Session = new List<SessionState>();

		public bool HasMinutesRemaining;

		private uint _MinutesRemaining;

		public GameAccountHandle BenefactorHandle
		{
			get
			{
				return _BenefactorHandle;
			}
			set
			{
				_BenefactorHandle = value;
				HasBenefactorHandle = value != null;
			}
		}

		public List<SessionState> Session
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

		public List<SessionState> SessionList => _Session;

		public int SessionCount => _Session.Count;

		public uint MinutesRemaining
		{
			get
			{
				return _MinutesRemaining;
			}
			set
			{
				_MinutesRemaining = value;
				HasMinutesRemaining = true;
			}
		}

		public bool IsInitialized => true;

		public void SetBenefactorHandle(GameAccountHandle val)
		{
			BenefactorHandle = val;
		}

		public void AddSession(SessionState val)
		{
			_Session.Add(val);
		}

		public void ClearSession()
		{
			_Session.Clear();
		}

		public void SetSession(List<SessionState> val)
		{
			Session = val;
		}

		public void SetMinutesRemaining(uint val)
		{
			MinutesRemaining = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasBenefactorHandle)
			{
				num ^= BenefactorHandle.GetHashCode();
			}
			foreach (SessionState item in Session)
			{
				num ^= item.GetHashCode();
			}
			if (HasMinutesRemaining)
			{
				num ^= MinutesRemaining.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetSessionStateByBenefactorResponse getSessionStateByBenefactorResponse = obj as GetSessionStateByBenefactorResponse;
			if (getSessionStateByBenefactorResponse == null)
			{
				return false;
			}
			if (HasBenefactorHandle != getSessionStateByBenefactorResponse.HasBenefactorHandle || (HasBenefactorHandle && !BenefactorHandle.Equals(getSessionStateByBenefactorResponse.BenefactorHandle)))
			{
				return false;
			}
			if (Session.Count != getSessionStateByBenefactorResponse.Session.Count)
			{
				return false;
			}
			for (int i = 0; i < Session.Count; i++)
			{
				if (!Session[i].Equals(getSessionStateByBenefactorResponse.Session[i]))
				{
					return false;
				}
			}
			if (HasMinutesRemaining != getSessionStateByBenefactorResponse.HasMinutesRemaining || (HasMinutesRemaining && !MinutesRemaining.Equals(getSessionStateByBenefactorResponse.MinutesRemaining)))
			{
				return false;
			}
			return true;
		}

		public static GetSessionStateByBenefactorResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetSessionStateByBenefactorResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetSessionStateByBenefactorResponse Deserialize(Stream stream, GetSessionStateByBenefactorResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetSessionStateByBenefactorResponse DeserializeLengthDelimited(Stream stream)
		{
			GetSessionStateByBenefactorResponse getSessionStateByBenefactorResponse = new GetSessionStateByBenefactorResponse();
			DeserializeLengthDelimited(stream, getSessionStateByBenefactorResponse);
			return getSessionStateByBenefactorResponse;
		}

		public static GetSessionStateByBenefactorResponse DeserializeLengthDelimited(Stream stream, GetSessionStateByBenefactorResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetSessionStateByBenefactorResponse Deserialize(Stream stream, GetSessionStateByBenefactorResponse instance, long limit)
		{
			if (instance.Session == null)
			{
				instance.Session = new List<SessionState>();
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
					if (instance.BenefactorHandle == null)
					{
						instance.BenefactorHandle = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.BenefactorHandle);
					}
					continue;
				case 18:
					instance.Session.Add(SessionState.DeserializeLengthDelimited(stream));
					continue;
				case 24:
					instance.MinutesRemaining = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, GetSessionStateByBenefactorResponse instance)
		{
			if (instance.HasBenefactorHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.BenefactorHandle.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.BenefactorHandle);
			}
			if (instance.Session.Count > 0)
			{
				foreach (SessionState item in instance.Session)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					SessionState.Serialize(stream, item);
				}
			}
			if (instance.HasMinutesRemaining)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.MinutesRemaining);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasBenefactorHandle)
			{
				num++;
				uint serializedSize = BenefactorHandle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (Session.Count > 0)
			{
				foreach (SessionState item in Session)
				{
					num++;
					uint serializedSize2 = item.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (HasMinutesRemaining)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(MinutesRemaining);
			}
			return num;
		}
	}
}
