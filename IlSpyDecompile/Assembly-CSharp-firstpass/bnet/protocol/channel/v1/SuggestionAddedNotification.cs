using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	public class SuggestionAddedNotification : IProtoBuf
	{
		public bool HasSubscriberId;

		private SubscriberId _SubscriberId;

		public InvitationSuggestion Suggestion { get; set; }

		public SubscriberId SubscriberId
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

		public bool IsInitialized => true;

		public void SetSuggestion(InvitationSuggestion val)
		{
			Suggestion = val;
		}

		public void SetSubscriberId(SubscriberId val)
		{
			SubscriberId = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Suggestion.GetHashCode();
			if (HasSubscriberId)
			{
				hashCode ^= SubscriberId.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			SuggestionAddedNotification suggestionAddedNotification = obj as SuggestionAddedNotification;
			if (suggestionAddedNotification == null)
			{
				return false;
			}
			if (!Suggestion.Equals(suggestionAddedNotification.Suggestion))
			{
				return false;
			}
			if (HasSubscriberId != suggestionAddedNotification.HasSubscriberId || (HasSubscriberId && !SubscriberId.Equals(suggestionAddedNotification.SubscriberId)))
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
					if (instance.Suggestion == null)
					{
						instance.Suggestion = InvitationSuggestion.DeserializeLengthDelimited(stream);
					}
					else
					{
						InvitationSuggestion.DeserializeLengthDelimited(stream, instance.Suggestion);
					}
					continue;
				case 18:
					if (instance.SubscriberId == null)
					{
						instance.SubscriberId = SubscriberId.DeserializeLengthDelimited(stream);
					}
					else
					{
						SubscriberId.DeserializeLengthDelimited(stream, instance.SubscriberId);
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
			if (instance.Suggestion == null)
			{
				throw new ArgumentNullException("Suggestion", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Suggestion.GetSerializedSize());
			InvitationSuggestion.Serialize(stream, instance.Suggestion);
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				SubscriberId.Serialize(stream, instance.SubscriberId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = Suggestion.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasSubscriberId)
			{
				num++;
				uint serializedSize2 = SubscriberId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num + 1;
		}
	}
}
