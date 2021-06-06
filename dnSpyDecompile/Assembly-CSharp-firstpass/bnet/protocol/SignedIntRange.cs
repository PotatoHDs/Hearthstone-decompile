using System;
using System.IO;

namespace bnet.protocol
{
	// Token: 0x020002BA RID: 698
	public class SignedIntRange : IProtoBuf
	{
		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06002920 RID: 10528 RVA: 0x00090C70 File Offset: 0x0008EE70
		// (set) Token: 0x06002921 RID: 10529 RVA: 0x00090C78 File Offset: 0x0008EE78
		public long Min
		{
			get
			{
				return this._Min;
			}
			set
			{
				this._Min = value;
				this.HasMin = true;
			}
		}

		// Token: 0x06002922 RID: 10530 RVA: 0x00090C88 File Offset: 0x0008EE88
		public void SetMin(long val)
		{
			this.Min = val;
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06002923 RID: 10531 RVA: 0x00090C91 File Offset: 0x0008EE91
		// (set) Token: 0x06002924 RID: 10532 RVA: 0x00090C99 File Offset: 0x0008EE99
		public long Max
		{
			get
			{
				return this._Max;
			}
			set
			{
				this._Max = value;
				this.HasMax = true;
			}
		}

		// Token: 0x06002925 RID: 10533 RVA: 0x00090CA9 File Offset: 0x0008EEA9
		public void SetMax(long val)
		{
			this.Max = val;
		}

		// Token: 0x06002926 RID: 10534 RVA: 0x00090CB4 File Offset: 0x0008EEB4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMin)
			{
				num ^= this.Min.GetHashCode();
			}
			if (this.HasMax)
			{
				num ^= this.Max.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002927 RID: 10535 RVA: 0x00090D00 File Offset: 0x0008EF00
		public override bool Equals(object obj)
		{
			SignedIntRange signedIntRange = obj as SignedIntRange;
			return signedIntRange != null && this.HasMin == signedIntRange.HasMin && (!this.HasMin || this.Min.Equals(signedIntRange.Min)) && this.HasMax == signedIntRange.HasMax && (!this.HasMax || this.Max.Equals(signedIntRange.Max));
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06002928 RID: 10536 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002929 RID: 10537 RVA: 0x00090D76 File Offset: 0x0008EF76
		public static SignedIntRange ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SignedIntRange>(bs, 0, -1);
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x00090D80 File Offset: 0x0008EF80
		public void Deserialize(Stream stream)
		{
			SignedIntRange.Deserialize(stream, this);
		}

		// Token: 0x0600292B RID: 10539 RVA: 0x00090D8A File Offset: 0x0008EF8A
		public static SignedIntRange Deserialize(Stream stream, SignedIntRange instance)
		{
			return SignedIntRange.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x00090D98 File Offset: 0x0008EF98
		public static SignedIntRange DeserializeLengthDelimited(Stream stream)
		{
			SignedIntRange signedIntRange = new SignedIntRange();
			SignedIntRange.DeserializeLengthDelimited(stream, signedIntRange);
			return signedIntRange;
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x00090DB4 File Offset: 0x0008EFB4
		public static SignedIntRange DeserializeLengthDelimited(Stream stream, SignedIntRange instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SignedIntRange.Deserialize(stream, instance, num);
		}

		// Token: 0x0600292E RID: 10542 RVA: 0x00090DDC File Offset: 0x0008EFDC
		public static SignedIntRange Deserialize(Stream stream, SignedIntRange instance, long limit)
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
				else if (num != 8)
				{
					if (num != 16)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else
					{
						instance.Max = (long)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Min = (long)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x00090E73 File Offset: 0x0008F073
		public void Serialize(Stream stream)
		{
			SignedIntRange.Serialize(stream, this);
		}

		// Token: 0x06002930 RID: 10544 RVA: 0x00090E7C File Offset: 0x0008F07C
		public static void Serialize(Stream stream, SignedIntRange instance)
		{
			if (instance.HasMin)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Min);
			}
			if (instance.HasMax)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Max);
			}
		}

		// Token: 0x06002931 RID: 10545 RVA: 0x00090EB8 File Offset: 0x0008F0B8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasMin)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.Min);
			}
			if (this.HasMax)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.Max);
			}
			return num;
		}

		// Token: 0x0400119C RID: 4508
		public bool HasMin;

		// Token: 0x0400119D RID: 4509
		private long _Min;

		// Token: 0x0400119E RID: 4510
		public bool HasMax;

		// Token: 0x0400119F RID: 4511
		private long _Max;
	}
}
