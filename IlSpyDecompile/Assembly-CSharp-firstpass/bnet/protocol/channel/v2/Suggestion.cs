using System.IO;

namespace bnet.protocol.channel.v2
{
	public class Suggestion : IProtoBuf
	{
		public bool HasChannelId;

		private ChannelId _ChannelId;

		public bool HasSuggester;

		private MemberDescription _Suggester;

		public bool HasSuggestee;

		private MemberDescription _Suggestee;

		public bool HasCreationTime;

		private ulong _CreationTime;

		public ChannelId ChannelId
		{
			get
			{
				return _ChannelId;
			}
			set
			{
				_ChannelId = value;
				HasChannelId = value != null;
			}
		}

		public MemberDescription Suggester
		{
			get
			{
				return _Suggester;
			}
			set
			{
				_Suggester = value;
				HasSuggester = value != null;
			}
		}

		public MemberDescription Suggestee
		{
			get
			{
				return _Suggestee;
			}
			set
			{
				_Suggestee = value;
				HasSuggestee = value != null;
			}
		}

		public ulong CreationTime
		{
			get
			{
				return _CreationTime;
			}
			set
			{
				_CreationTime = value;
				HasCreationTime = true;
			}
		}

		public bool IsInitialized => true;

		public void SetChannelId(ChannelId val)
		{
			ChannelId = val;
		}

		public void SetSuggester(MemberDescription val)
		{
			Suggester = val;
		}

		public void SetSuggestee(MemberDescription val)
		{
			Suggestee = val;
		}

		public void SetCreationTime(ulong val)
		{
			CreationTime = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasChannelId)
			{
				num ^= ChannelId.GetHashCode();
			}
			if (HasSuggester)
			{
				num ^= Suggester.GetHashCode();
			}
			if (HasSuggestee)
			{
				num ^= Suggestee.GetHashCode();
			}
			if (HasCreationTime)
			{
				num ^= CreationTime.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Suggestion suggestion = obj as Suggestion;
			if (suggestion == null)
			{
				return false;
			}
			if (HasChannelId != suggestion.HasChannelId || (HasChannelId && !ChannelId.Equals(suggestion.ChannelId)))
			{
				return false;
			}
			if (HasSuggester != suggestion.HasSuggester || (HasSuggester && !Suggester.Equals(suggestion.Suggester)))
			{
				return false;
			}
			if (HasSuggestee != suggestion.HasSuggestee || (HasSuggestee && !Suggestee.Equals(suggestion.Suggestee)))
			{
				return false;
			}
			if (HasCreationTime != suggestion.HasCreationTime || (HasCreationTime && !CreationTime.Equals(suggestion.CreationTime)))
			{
				return false;
			}
			return true;
		}

		public static Suggestion ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Suggestion>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Suggestion Deserialize(Stream stream, Suggestion instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Suggestion DeserializeLengthDelimited(Stream stream)
		{
			Suggestion suggestion = new Suggestion();
			DeserializeLengthDelimited(stream, suggestion);
			return suggestion;
		}

		public static Suggestion DeserializeLengthDelimited(Stream stream, Suggestion instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Suggestion Deserialize(Stream stream, Suggestion instance, long limit)
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
				case 18:
					if (instance.ChannelId == null)
					{
						instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
					continue;
				case 26:
					if (instance.Suggester == null)
					{
						instance.Suggester = MemberDescription.DeserializeLengthDelimited(stream);
					}
					else
					{
						MemberDescription.DeserializeLengthDelimited(stream, instance.Suggester);
					}
					continue;
				case 34:
					if (instance.Suggestee == null)
					{
						instance.Suggestee = MemberDescription.DeserializeLengthDelimited(stream);
					}
					else
					{
						MemberDescription.DeserializeLengthDelimited(stream, instance.Suggestee);
					}
					continue;
				case 56:
					instance.CreationTime = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, Suggestion instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasSuggester)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Suggester.GetSerializedSize());
				MemberDescription.Serialize(stream, instance.Suggester);
			}
			if (instance.HasSuggestee)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Suggestee.GetSerializedSize());
				MemberDescription.Serialize(stream, instance.Suggestee);
			}
			if (instance.HasCreationTime)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, instance.CreationTime);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasChannelId)
			{
				num++;
				uint serializedSize = ChannelId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasSuggester)
			{
				num++;
				uint serializedSize2 = Suggester.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasSuggestee)
			{
				num++;
				uint serializedSize3 = Suggestee.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (HasCreationTime)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(CreationTime);
			}
			return num;
		}
	}
}
