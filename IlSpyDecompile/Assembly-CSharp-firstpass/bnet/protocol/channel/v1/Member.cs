using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	public class Member : IProtoBuf
	{
		public Identity Identity { get; set; }

		public MemberState State { get; set; }

		public bool IsInitialized => true;

		public void SetIdentity(Identity val)
		{
			Identity = val;
		}

		public void SetState(MemberState val)
		{
			State = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Identity.GetHashCode() ^ State.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			Member member = obj as Member;
			if (member == null)
			{
				return false;
			}
			if (!Identity.Equals(member.Identity))
			{
				return false;
			}
			if (!State.Equals(member.State))
			{
				return false;
			}
			return true;
		}

		public static Member ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Member>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Member Deserialize(Stream stream, Member instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Member DeserializeLengthDelimited(Stream stream)
		{
			Member member = new Member();
			DeserializeLengthDelimited(stream, member);
			return member;
		}

		public static Member DeserializeLengthDelimited(Stream stream, Member instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Member Deserialize(Stream stream, Member instance, long limit)
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
					if (instance.Identity == null)
					{
						instance.Identity = Identity.DeserializeLengthDelimited(stream);
					}
					else
					{
						Identity.DeserializeLengthDelimited(stream, instance.Identity);
					}
					continue;
				case 18:
					if (instance.State == null)
					{
						instance.State = MemberState.DeserializeLengthDelimited(stream);
					}
					else
					{
						MemberState.DeserializeLengthDelimited(stream, instance.State);
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

		public static void Serialize(Stream stream, Member instance)
		{
			if (instance.Identity == null)
			{
				throw new ArgumentNullException("Identity", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Identity.GetSerializedSize());
			Identity.Serialize(stream, instance.Identity);
			if (instance.State == null)
			{
				throw new ArgumentNullException("State", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.State.GetSerializedSize());
			MemberState.Serialize(stream, instance.State);
		}

		public uint GetSerializedSize()
		{
			uint serializedSize = Identity.GetSerializedSize();
			uint num = 0 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize));
			uint serializedSize2 = State.GetSerializedSize();
			return num + (serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2)) + 2;
		}
	}
}
