using System;
using System.IO;
using System.Text;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004D7 RID: 1239
	public class MemberAccountInfo : IProtoBuf
	{
		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x06005761 RID: 22369 RVA: 0x0010C2A2 File Offset: 0x0010A4A2
		// (set) Token: 0x06005762 RID: 22370 RVA: 0x0010C2AA File Offset: 0x0010A4AA
		public string BattleTag
		{
			get
			{
				return this._BattleTag;
			}
			set
			{
				this._BattleTag = value;
				this.HasBattleTag = (value != null);
			}
		}

		// Token: 0x06005763 RID: 22371 RVA: 0x0010C2BD File Offset: 0x0010A4BD
		public void SetBattleTag(string val)
		{
			this.BattleTag = val;
		}

		// Token: 0x06005764 RID: 22372 RVA: 0x0010C2C8 File Offset: 0x0010A4C8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasBattleTag)
			{
				num ^= this.BattleTag.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005765 RID: 22373 RVA: 0x0010C2F8 File Offset: 0x0010A4F8
		public override bool Equals(object obj)
		{
			MemberAccountInfo memberAccountInfo = obj as MemberAccountInfo;
			return memberAccountInfo != null && this.HasBattleTag == memberAccountInfo.HasBattleTag && (!this.HasBattleTag || this.BattleTag.Equals(memberAccountInfo.BattleTag));
		}

		// Token: 0x17001079 RID: 4217
		// (get) Token: 0x06005766 RID: 22374 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005767 RID: 22375 RVA: 0x0010C33D File Offset: 0x0010A53D
		public static MemberAccountInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemberAccountInfo>(bs, 0, -1);
		}

		// Token: 0x06005768 RID: 22376 RVA: 0x0010C347 File Offset: 0x0010A547
		public void Deserialize(Stream stream)
		{
			MemberAccountInfo.Deserialize(stream, this);
		}

		// Token: 0x06005769 RID: 22377 RVA: 0x0010C351 File Offset: 0x0010A551
		public static MemberAccountInfo Deserialize(Stream stream, MemberAccountInfo instance)
		{
			return MemberAccountInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600576A RID: 22378 RVA: 0x0010C35C File Offset: 0x0010A55C
		public static MemberAccountInfo DeserializeLengthDelimited(Stream stream)
		{
			MemberAccountInfo memberAccountInfo = new MemberAccountInfo();
			MemberAccountInfo.DeserializeLengthDelimited(stream, memberAccountInfo);
			return memberAccountInfo;
		}

		// Token: 0x0600576B RID: 22379 RVA: 0x0010C378 File Offset: 0x0010A578
		public static MemberAccountInfo DeserializeLengthDelimited(Stream stream, MemberAccountInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MemberAccountInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x0600576C RID: 22380 RVA: 0x0010C3A0 File Offset: 0x0010A5A0
		public static MemberAccountInfo Deserialize(Stream stream, MemberAccountInfo instance, long limit)
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
				else if (num == 26)
				{
					instance.BattleTag = ProtocolParser.ReadString(stream);
				}
				else
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600576D RID: 22381 RVA: 0x0010C420 File Offset: 0x0010A620
		public void Serialize(Stream stream)
		{
			MemberAccountInfo.Serialize(stream, this);
		}

		// Token: 0x0600576E RID: 22382 RVA: 0x0010C429 File Offset: 0x0010A629
		public static void Serialize(Stream stream, MemberAccountInfo instance)
		{
			if (instance.HasBattleTag)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattleTag));
			}
		}

		// Token: 0x0600576F RID: 22383 RVA: 0x0010C454 File Offset: 0x0010A654
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasBattleTag)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.BattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x04001B7B RID: 7035
		public bool HasBattleTag;

		// Token: 0x04001B7C RID: 7036
		private string _BattleTag;
	}
}
