using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.report.v2.Types;

namespace bnet.protocol.report.v2
{
	public class UserOptions : IProtoBuf
	{
		public bool HasTargetId;

		private AccountId _TargetId;

		public bool HasType;

		private IssueType _Type;

		public bool HasSource;

		private UserSource _Source;

		public bool HasItem;

		private ReportItem _Item;

		public AccountId TargetId
		{
			get
			{
				return _TargetId;
			}
			set
			{
				_TargetId = value;
				HasTargetId = value != null;
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

		public UserSource Source
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

		public void SetTargetId(AccountId val)
		{
			TargetId = val;
		}

		public void SetType(IssueType val)
		{
			Type = val;
		}

		public void SetSource(UserSource val)
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
			if (HasTargetId)
			{
				num ^= TargetId.GetHashCode();
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
			UserOptions userOptions = obj as UserOptions;
			if (userOptions == null)
			{
				return false;
			}
			if (HasTargetId != userOptions.HasTargetId || (HasTargetId && !TargetId.Equals(userOptions.TargetId)))
			{
				return false;
			}
			if (HasType != userOptions.HasType || (HasType && !Type.Equals(userOptions.Type)))
			{
				return false;
			}
			if (HasSource != userOptions.HasSource || (HasSource && !Source.Equals(userOptions.Source)))
			{
				return false;
			}
			if (HasItem != userOptions.HasItem || (HasItem && !Item.Equals(userOptions.Item)))
			{
				return false;
			}
			return true;
		}

		public static UserOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UserOptions>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UserOptions Deserialize(Stream stream, UserOptions instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UserOptions DeserializeLengthDelimited(Stream stream)
		{
			UserOptions userOptions = new UserOptions();
			DeserializeLengthDelimited(stream, userOptions);
			return userOptions;
		}

		public static UserOptions DeserializeLengthDelimited(Stream stream, UserOptions instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UserOptions Deserialize(Stream stream, UserOptions instance, long limit)
		{
			instance.Type = IssueType.ISSUE_TYPE_SPAM;
			instance.Source = UserSource.USER_SOURCE_OTHER;
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
					if (instance.TargetId == null)
					{
						instance.TargetId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.TargetId);
					}
					continue;
				case 16:
					instance.Type = (IssueType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.Source = (UserSource)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
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

		public static void Serialize(Stream stream, UserOptions instance)
		{
			if (instance.HasTargetId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
				AccountId.Serialize(stream, instance.TargetId);
			}
			if (instance.HasType)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Type);
			}
			if (instance.HasSource)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Source);
			}
			if (instance.HasItem)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Item.GetSerializedSize());
				ReportItem.Serialize(stream, instance.Item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasTargetId)
			{
				num++;
				uint serializedSize = TargetId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
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
				uint serializedSize2 = Item.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
