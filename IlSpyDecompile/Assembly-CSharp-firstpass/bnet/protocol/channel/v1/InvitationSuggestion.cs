using System;
using System.IO;
using System.Text;

namespace bnet.protocol.channel.v1
{
	public class InvitationSuggestion : IProtoBuf
	{
		public bool HasChannelId;

		private EntityId _ChannelId;

		public bool HasSuggesterName;

		private string _SuggesterName;

		public bool HasSuggesteeName;

		private string _SuggesteeName;

		public EntityId ChannelId
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

		public EntityId SuggesterId { get; set; }

		public EntityId SuggesteeId { get; set; }

		public string SuggesterName
		{
			get
			{
				return _SuggesterName;
			}
			set
			{
				_SuggesterName = value;
				HasSuggesterName = value != null;
			}
		}

		public string SuggesteeName
		{
			get
			{
				return _SuggesteeName;
			}
			set
			{
				_SuggesteeName = value;
				HasSuggesteeName = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetChannelId(EntityId val)
		{
			ChannelId = val;
		}

		public void SetSuggesterId(EntityId val)
		{
			SuggesterId = val;
		}

		public void SetSuggesteeId(EntityId val)
		{
			SuggesteeId = val;
		}

		public void SetSuggesterName(string val)
		{
			SuggesterName = val;
		}

		public void SetSuggesteeName(string val)
		{
			SuggesteeName = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasChannelId)
			{
				num ^= ChannelId.GetHashCode();
			}
			num ^= SuggesterId.GetHashCode();
			num ^= SuggesteeId.GetHashCode();
			if (HasSuggesterName)
			{
				num ^= SuggesterName.GetHashCode();
			}
			if (HasSuggesteeName)
			{
				num ^= SuggesteeName.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			InvitationSuggestion invitationSuggestion = obj as InvitationSuggestion;
			if (invitationSuggestion == null)
			{
				return false;
			}
			if (HasChannelId != invitationSuggestion.HasChannelId || (HasChannelId && !ChannelId.Equals(invitationSuggestion.ChannelId)))
			{
				return false;
			}
			if (!SuggesterId.Equals(invitationSuggestion.SuggesterId))
			{
				return false;
			}
			if (!SuggesteeId.Equals(invitationSuggestion.SuggesteeId))
			{
				return false;
			}
			if (HasSuggesterName != invitationSuggestion.HasSuggesterName || (HasSuggesterName && !SuggesterName.Equals(invitationSuggestion.SuggesterName)))
			{
				return false;
			}
			if (HasSuggesteeName != invitationSuggestion.HasSuggesteeName || (HasSuggesteeName && !SuggesteeName.Equals(invitationSuggestion.SuggesteeName)))
			{
				return false;
			}
			return true;
		}

		public static InvitationSuggestion ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<InvitationSuggestion>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static InvitationSuggestion Deserialize(Stream stream, InvitationSuggestion instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static InvitationSuggestion DeserializeLengthDelimited(Stream stream)
		{
			InvitationSuggestion invitationSuggestion = new InvitationSuggestion();
			DeserializeLengthDelimited(stream, invitationSuggestion);
			return invitationSuggestion;
		}

		public static InvitationSuggestion DeserializeLengthDelimited(Stream stream, InvitationSuggestion instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static InvitationSuggestion Deserialize(Stream stream, InvitationSuggestion instance, long limit)
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
					if (instance.ChannelId == null)
					{
						instance.ChannelId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
					continue;
				case 18:
					if (instance.SuggesterId == null)
					{
						instance.SuggesterId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.SuggesterId);
					}
					continue;
				case 26:
					if (instance.SuggesteeId == null)
					{
						instance.SuggesteeId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.SuggesteeId);
					}
					continue;
				case 34:
					instance.SuggesterName = ProtocolParser.ReadString(stream);
					continue;
				case 42:
					instance.SuggesteeName = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, InvitationSuggestion instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				EntityId.Serialize(stream, instance.ChannelId);
			}
			if (instance.SuggesterId == null)
			{
				throw new ArgumentNullException("SuggesterId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.SuggesterId.GetSerializedSize());
			EntityId.Serialize(stream, instance.SuggesterId);
			if (instance.SuggesteeId == null)
			{
				throw new ArgumentNullException("SuggesteeId", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteUInt32(stream, instance.SuggesteeId.GetSerializedSize());
			EntityId.Serialize(stream, instance.SuggesteeId);
			if (instance.HasSuggesterName)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SuggesterName));
			}
			if (instance.HasSuggesteeName)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SuggesteeName));
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
			uint serializedSize2 = SuggesterId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			uint serializedSize3 = SuggesteeId.GetSerializedSize();
			num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			if (HasSuggesterName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(SuggesterName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasSuggesteeName)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(SuggesteeName);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num + 2;
		}
	}
}
