using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004D9 RID: 1241
	public class Member : IProtoBuf
	{
		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x06005792 RID: 22418 RVA: 0x0010CBB6 File Offset: 0x0010ADB6
		// (set) Token: 0x06005793 RID: 22419 RVA: 0x0010CBBE File Offset: 0x0010ADBE
		public Identity Identity { get; set; }

		// Token: 0x06005794 RID: 22420 RVA: 0x0010CBC7 File Offset: 0x0010ADC7
		public void SetIdentity(Identity val)
		{
			this.Identity = val;
		}

		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x06005795 RID: 22421 RVA: 0x0010CBD0 File Offset: 0x0010ADD0
		// (set) Token: 0x06005796 RID: 22422 RVA: 0x0010CBD8 File Offset: 0x0010ADD8
		public MemberState State { get; set; }

		// Token: 0x06005797 RID: 22423 RVA: 0x0010CBE1 File Offset: 0x0010ADE1
		public void SetState(MemberState val)
		{
			this.State = val;
		}

		// Token: 0x06005798 RID: 22424 RVA: 0x0010CBEA File Offset: 0x0010ADEA
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Identity.GetHashCode() ^ this.State.GetHashCode();
		}

		// Token: 0x06005799 RID: 22425 RVA: 0x0010CC10 File Offset: 0x0010AE10
		public override bool Equals(object obj)
		{
			Member member = obj as Member;
			return member != null && this.Identity.Equals(member.Identity) && this.State.Equals(member.State);
		}

		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x0600579A RID: 22426 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600579B RID: 22427 RVA: 0x0010CC54 File Offset: 0x0010AE54
		public static Member ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Member>(bs, 0, -1);
		}

		// Token: 0x0600579C RID: 22428 RVA: 0x0010CC5E File Offset: 0x0010AE5E
		public void Deserialize(Stream stream)
		{
			Member.Deserialize(stream, this);
		}

		// Token: 0x0600579D RID: 22429 RVA: 0x0010CC68 File Offset: 0x0010AE68
		public static Member Deserialize(Stream stream, Member instance)
		{
			return Member.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600579E RID: 22430 RVA: 0x0010CC74 File Offset: 0x0010AE74
		public static Member DeserializeLengthDelimited(Stream stream)
		{
			Member member = new Member();
			Member.DeserializeLengthDelimited(stream, member);
			return member;
		}

		// Token: 0x0600579F RID: 22431 RVA: 0x0010CC90 File Offset: 0x0010AE90
		public static Member DeserializeLengthDelimited(Stream stream, Member instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Member.Deserialize(stream, instance, num);
		}

		// Token: 0x060057A0 RID: 22432 RVA: 0x0010CCB8 File Offset: 0x0010AEB8
		public static Member Deserialize(Stream stream, Member instance, long limit)
		{
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else if (num != 10)
				{
					if (num != 18)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.State == null)
					{
						instance.State = MemberState.DeserializeLengthDelimited(stream);
					}
					else
					{
						MemberState.DeserializeLengthDelimited(stream, instance.State);
					}
				}
				else if (instance.Identity == null)
				{
					instance.Identity = Identity.DeserializeLengthDelimited(stream);
				}
				else
				{
					Identity.DeserializeLengthDelimited(stream, instance.Identity);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060057A1 RID: 22433 RVA: 0x0010CD8A File Offset: 0x0010AF8A
		public void Serialize(Stream stream)
		{
			Member.Serialize(stream, this);
		}

		// Token: 0x060057A2 RID: 22434 RVA: 0x0010CD94 File Offset: 0x0010AF94
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

		// Token: 0x060057A3 RID: 22435 RVA: 0x0010CE1C File Offset: 0x0010B01C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Identity.GetSerializedSize();
			uint num2 = num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize));
			uint serializedSize2 = this.State.GetSerializedSize();
			return num2 + (serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2)) + 2U;
		}
	}
}
