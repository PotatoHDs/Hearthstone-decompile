using System.IO;
using bnet.protocol.report.v2.Types;

namespace bnet.protocol.report.v2
{
	public class ClubOptions : IProtoBuf
	{
		public bool HasClubId;

		private ulong _ClubId;

		public bool HasStreamId;

		private ulong _StreamId;

		public bool HasType;

		private IssueType _Type;

		public bool HasSource;

		private ClubSource _Source;

		public bool HasItem;

		private ReportItem _Item;

		public ulong ClubId
		{
			get
			{
				return _ClubId;
			}
			set
			{
				_ClubId = value;
				HasClubId = true;
			}
		}

		public ulong StreamId
		{
			get
			{
				return _StreamId;
			}
			set
			{
				_StreamId = value;
				HasStreamId = true;
			}
		}

		public IssueType Type
		{
			get
			{
				return _Type;
			}
			set
			{
				_Type = value;
				HasType = true;
			}
		}

		public ClubSource Source
		{
			get
			{
				return _Source;
			}
			set
			{
				_Source = value;
				HasSource = true;
			}
		}

		public ReportItem Item
		{
			get
			{
				return _Item;
			}
			set
			{
				_Item = value;
				HasItem = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetClubId(ulong val)
		{
			ClubId = val;
		}

		public void SetStreamId(ulong val)
		{
			StreamId = val;
		}

		public void SetType(IssueType val)
		{
			Type = val;
		}

		public void SetSource(ClubSource val)
		{
			Source = val;
		}

		public void SetItem(ReportItem val)
		{
			Item = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasClubId)
			{
				num ^= ClubId.GetHashCode();
			}
			if (HasStreamId)
			{
				num ^= StreamId.GetHashCode();
			}
			if (HasType)
			{
				num ^= Type.GetHashCode();
			}
			if (HasSource)
			{
				num ^= Source.GetHashCode();
			}
			if (HasItem)
			{
				num ^= Item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ClubOptions clubOptions = obj as ClubOptions;
			if (clubOptions == null)
			{
				return false;
			}
			if (HasClubId != clubOptions.HasClubId || (HasClubId && !ClubId.Equals(clubOptions.ClubId)))
			{
				return false;
			}
			if (HasStreamId != clubOptions.HasStreamId || (HasStreamId && !StreamId.Equals(clubOptions.StreamId)))
			{
				return false;
			}
			if (HasType != clubOptions.HasType || (HasType && !Type.Equals(clubOptions.Type)))
			{
				return false;
			}
			if (HasSource != clubOptions.HasSource || (HasSource && !Source.Equals(clubOptions.Source)))
			{
				return false;
			}
			if (HasItem != clubOptions.HasItem || (HasItem && !Item.Equals(clubOptions.Item)))
			{
				return false;
			}
			return true;
		}

		public static ClubOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ClubOptions>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ClubOptions Deserialize(Stream stream, ClubOptions instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ClubOptions DeserializeLengthDelimited(Stream stream)
		{
			ClubOptions clubOptions = new ClubOptions();
			DeserializeLengthDelimited(stream, clubOptions);
			return clubOptions;
		}

		public static ClubOptions DeserializeLengthDelimited(Stream stream, ClubOptions instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ClubOptions Deserialize(Stream stream, ClubOptions instance, long limit)
		{
			instance.Type = IssueType.ISSUE_TYPE_SPAM;
			instance.Source = ClubSource.CLUB_SOURCE_OTHER;
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
					instance.ClubId = ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.StreamId = ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.Type = (IssueType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.Source = (ClubSource)ProtocolParser.ReadUInt64(stream);
					continue;
				case 42:
					if (instance.Item == null)
					{
						instance.Item = ReportItem.DeserializeLengthDelimited(stream);
					}
					else
					{
						ReportItem.DeserializeLengthDelimited(stream, instance.Item);
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

		public static void Serialize(Stream stream, ClubOptions instance)
		{
			if (instance.HasClubId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.ClubId);
			}
			if (instance.HasStreamId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.StreamId);
			}
			if (instance.HasType)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Type);
			}
			if (instance.HasSource)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Source);
			}
			if (instance.HasItem)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.Item.GetSerializedSize());
				ReportItem.Serialize(stream, instance.Item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasClubId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(ClubId);
			}
			if (HasStreamId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(StreamId);
			}
			if (HasType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Type);
			}
			if (HasSource)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Source);
			}
			if (HasItem)
			{
				num++;
				uint serializedSize = Item.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
