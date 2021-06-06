using System.IO;
using System.Text;

namespace bnet.protocol.challenge.v1
{
	public class ChallengeExternalResult : IProtoBuf
	{
		public bool HasRequestToken;

		private string _RequestToken;

		public bool HasPassed;

		private bool _Passed;

		public string RequestToken
		{
			get
			{
				return _RequestToken;
			}
			set
			{
				_RequestToken = value;
				HasRequestToken = value != null;
			}
		}

		public bool Passed
		{
			get
			{
				return _Passed;
			}
			set
			{
				_Passed = value;
				HasPassed = true;
			}
		}

		public bool IsInitialized => true;

		public void SetRequestToken(string val)
		{
			RequestToken = val;
		}

		public void SetPassed(bool val)
		{
			Passed = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasRequestToken)
			{
				num ^= RequestToken.GetHashCode();
			}
			if (HasPassed)
			{
				num ^= Passed.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ChallengeExternalResult challengeExternalResult = obj as ChallengeExternalResult;
			if (challengeExternalResult == null)
			{
				return false;
			}
			if (HasRequestToken != challengeExternalResult.HasRequestToken || (HasRequestToken && !RequestToken.Equals(challengeExternalResult.RequestToken)))
			{
				return false;
			}
			if (HasPassed != challengeExternalResult.HasPassed || (HasPassed && !Passed.Equals(challengeExternalResult.Passed)))
			{
				return false;
			}
			return true;
		}

		public static ChallengeExternalResult ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChallengeExternalResult>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ChallengeExternalResult Deserialize(Stream stream, ChallengeExternalResult instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ChallengeExternalResult DeserializeLengthDelimited(Stream stream)
		{
			ChallengeExternalResult challengeExternalResult = new ChallengeExternalResult();
			DeserializeLengthDelimited(stream, challengeExternalResult);
			return challengeExternalResult;
		}

		public static ChallengeExternalResult DeserializeLengthDelimited(Stream stream, ChallengeExternalResult instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ChallengeExternalResult Deserialize(Stream stream, ChallengeExternalResult instance, long limit)
		{
			instance.Passed = true;
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
					instance.RequestToken = ProtocolParser.ReadString(stream);
					continue;
				case 16:
					instance.Passed = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, ChallengeExternalResult instance)
		{
			if (instance.HasRequestToken)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.RequestToken));
			}
			if (instance.HasPassed)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Passed);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasRequestToken)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(RequestToken);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasPassed)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
