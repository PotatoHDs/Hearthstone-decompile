using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	public class MemberDescription : IProtoBuf
	{
		public bool HasId;

		private GameAccountHandle _Id;

		public bool HasBattleTag;

		private string _BattleTag;

		public GameAccountHandle Id
		{
			get
			{
				return _Id;
			}
			set
			{
				_Id = value;
				HasId = value != null;
			}
		}

		public string BattleTag
		{
			get
			{
				return _BattleTag;
			}
			set
			{
				_BattleTag = value;
				HasBattleTag = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetId(GameAccountHandle val)
		{
			Id = val;
		}

		public void SetBattleTag(string val)
		{
			BattleTag = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasId)
			{
				num ^= Id.GetHashCode();
			}
			if (HasBattleTag)
			{
				num ^= BattleTag.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			MemberDescription memberDescription = obj as MemberDescription;
			if (memberDescription == null)
			{
				return false;
			}
			if (HasId != memberDescription.HasId || (HasId && !Id.Equals(memberDescription.Id)))
			{
				return false;
			}
			if (HasBattleTag != memberDescription.HasBattleTag || (HasBattleTag && !BattleTag.Equals(memberDescription.BattleTag)))
			{
				return false;
			}
			return true;
		}

		public static MemberDescription ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemberDescription>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static MemberDescription Deserialize(Stream stream, MemberDescription instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static MemberDescription DeserializeLengthDelimited(Stream stream)
		{
			MemberDescription memberDescription = new MemberDescription();
			DeserializeLengthDelimited(stream, memberDescription);
			return memberDescription;
		}

		public static MemberDescription DeserializeLengthDelimited(Stream stream, MemberDescription instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static MemberDescription Deserialize(Stream stream, MemberDescription instance, long limit)
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
					if (instance.Id == null)
					{
						instance.Id = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.Id);
					}
					continue;
				case 18:
					instance.BattleTag = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, MemberDescription instance)
		{
			if (instance.HasId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Id.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.Id);
			}
			if (instance.HasBattleTag)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattleTag));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasId)
			{
				num++;
				uint serializedSize = Id.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasBattleTag)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(BattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
