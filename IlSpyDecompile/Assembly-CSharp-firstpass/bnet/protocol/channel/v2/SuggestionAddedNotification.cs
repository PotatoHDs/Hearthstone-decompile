using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	public class SuggestionAddedNotification : IProtoBuf
	{
		public bool HasAgentId;

		private GameAccountHandle _AgentId;

		public bool HasSubscriberId;

		private GameAccountHandle _SubscriberId;

		public bool HasSuggestion;

		private Suggestion _Suggestion;

		public GameAccountHandle AgentId
		{
			get
			{
				return _AgentId;
			}
			set
			{
				_AgentId = value;
				HasAgentId = value != null;
			}
		}

		public GameAccountHandle SubscriberId
		{
			get
			{
				return _SubscriberId;
			}
			set
			{
				_SubscriberId = value;
				HasSubscriberId = value != null;
			}
		}

		public Suggestion Suggestion
		{
			get
			{
				return _Suggestion;
			}
			set
			{
				_Suggestion = value;
				HasSuggestion = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetAgentId(GameAccountHandle val)
		{
			AgentId = val;
		}

		public void SetSubscriberId(GameAccountHandle val)
		{
			SubscriberId = val;
		}

		public void SetSuggestion(Suggestion val)
		{
			Suggestion = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			if (HasSubscriberId)
			{
				num ^= SubscriberId.GetHashCode();
			}
			if (HasSuggestion)
			{
				num ^= Suggestion.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SuggestionAddedNotification suggestionAddedNotification = obj as SuggestionAddedNotification;
			if (suggestionAddedNotification == null)
			{
				return false;
			}
			if (HasAgentId != suggestionAddedNotification.HasAgentId || (HasAgentId && !AgentId.Equals(suggestionAddedNotification.AgentId)))
			{
				return false;
			}
			if (HasSubscriberId != suggestionAddedNotification.HasSubscriberId || (HasSubscriberId && !SubscriberId.Equals(suggestionAddedNotification.SubscriberId)))
			{
				return false;
			}
			if (HasSuggestion != suggestionAddedNotification.HasSuggestion || (HasSuggestion && !Suggestion.Equals(suggestionAddedNotification.Suggestion)))
			{
				return false;
			}
			return true;
		}

		public static SuggestionAddedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SuggestionAddedNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SuggestionAddedNotification Deserialize(Stream stream, SuggestionAddedNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SuggestionAddedNotification DeserializeLengthDelimited(Stream stream)
		{
			SuggestionAddedNotification suggestionAddedNotification = new SuggestionAddedNotification();
			DeserializeLengthDelimited(stream, suggestionAddedNotification);
			return suggestionAddedNotification;
		}

		public static SuggestionAddedNotification DeserializeLengthDelimited(Stream stream, SuggestionAddedNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SuggestionAddedNotification Deserialize(Stream stream, SuggestionAddedNotification instance, long limit)
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
					if (instance.AgentId == null)
					{
						instance.AgentId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.AgentId);
					}
					continue;
				case 18:
					if (instance.SubscriberId == null)
					{
						instance.SubscriberId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.SubscriberId);
					}
					continue;
				case 26:
					if (instance.Suggestion == null)
					{
						instance.Suggestion = Suggestion.DeserializeLengthDelimited(stream);
					}
					else
					{
						Suggestion.DeserializeLengthDelimited(stream, instance.Suggestion);
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

		public static void Serialize(Stream stream, SuggestionAddedNotification instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.SubscriberId);
			}
			if (instance.HasSuggestion)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Suggestion.GetSerializedSize());
				Suggestion.Serialize(stream, instance.Suggestion);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAgentId)
			{
				num++;
				uint serializedSize = AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasSubscriberId)
			{
				num++;
				uint serializedSize2 = SubscriberId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasSuggestion)
			{
				num++;
				uint serializedSize3 = Suggestion.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}
	}
}
