using System.IO;
using System.Text;

namespace bnet.protocol.channel.v1
{
	public class MemberAccountInfo : IProtoBuf
	{
		public bool HasBattleTag;

		private string _BattleTag;

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

		public void SetBattleTag(string val)
		{
			BattleTag = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasBattleTag)
			{
				num ^= BattleTag.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			MemberAccountInfo memberAccountInfo = obj as MemberAccountInfo;
			if (memberAccountInfo == null)
			{
				return false;
			}
			if (HasBattleTag != memberAccountInfo.HasBattleTag || (HasBattleTag && !BattleTag.Equals(memberAccountInfo.BattleTag)))
			{
				return false;
			}
			return true;
		}

		public static MemberAccountInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemberAccountInfo>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static MemberAccountInfo Deserialize(Stream stream, MemberAccountInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static MemberAccountInfo DeserializeLengthDelimited(Stream stream)
		{
			MemberAccountInfo memberAccountInfo = new MemberAccountInfo();
			DeserializeLengthDelimited(stream, memberAccountInfo);
			return memberAccountInfo;
		}

		public static MemberAccountInfo DeserializeLengthDelimited(Stream stream, MemberAccountInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static MemberAccountInfo Deserialize(Stream stream, MemberAccountInfo instance, long limit)
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
				case 26:
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

		public static void Serialize(Stream stream, MemberAccountInfo instance)
		{
			if (instance.HasBattleTag)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattleTag));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
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
