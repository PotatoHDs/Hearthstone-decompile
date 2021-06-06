using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class MatchmakerControlProperties : IProtoBuf
	{
		public bool HasAcceptNewEntries;

		private bool _AcceptNewEntries;

		public bool AcceptNewEntries
		{
			get
			{
				return _AcceptNewEntries;
			}
			set
			{
				_AcceptNewEntries = value;
				HasAcceptNewEntries = true;
			}
		}

		public bool IsInitialized => true;

		public void SetAcceptNewEntries(bool val)
		{
			AcceptNewEntries = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAcceptNewEntries)
			{
				num ^= AcceptNewEntries.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			MatchmakerControlProperties matchmakerControlProperties = obj as MatchmakerControlProperties;
			if (matchmakerControlProperties == null)
			{
				return false;
			}
			if (HasAcceptNewEntries != matchmakerControlProperties.HasAcceptNewEntries || (HasAcceptNewEntries && !AcceptNewEntries.Equals(matchmakerControlProperties.AcceptNewEntries)))
			{
				return false;
			}
			return true;
		}

		public static MatchmakerControlProperties ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MatchmakerControlProperties>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static MatchmakerControlProperties Deserialize(Stream stream, MatchmakerControlProperties instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static MatchmakerControlProperties DeserializeLengthDelimited(Stream stream)
		{
			MatchmakerControlProperties matchmakerControlProperties = new MatchmakerControlProperties();
			DeserializeLengthDelimited(stream, matchmakerControlProperties);
			return matchmakerControlProperties;
		}

		public static MatchmakerControlProperties DeserializeLengthDelimited(Stream stream, MatchmakerControlProperties instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static MatchmakerControlProperties Deserialize(Stream stream, MatchmakerControlProperties instance, long limit)
		{
			instance.AcceptNewEntries = true;
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
				case 8:
					instance.AcceptNewEntries = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, MatchmakerControlProperties instance)
		{
			if (instance.HasAcceptNewEntries)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.AcceptNewEntries);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAcceptNewEntries)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
