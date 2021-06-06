using System;
using System.IO;
using System.Text;

namespace bnet.protocol.challenge.v1
{
	// Token: 0x020004E3 RID: 1251
	public class ChallengeExternalResult : IProtoBuf
	{
		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x0600585C RID: 22620 RVA: 0x0010EAB1 File Offset: 0x0010CCB1
		// (set) Token: 0x0600585D RID: 22621 RVA: 0x0010EAB9 File Offset: 0x0010CCB9
		public string RequestToken
		{
			get
			{
				return this._RequestToken;
			}
			set
			{
				this._RequestToken = value;
				this.HasRequestToken = (value != null);
			}
		}

		// Token: 0x0600585E RID: 22622 RVA: 0x0010EACC File Offset: 0x0010CCCC
		public void SetRequestToken(string val)
		{
			this.RequestToken = val;
		}

		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x0600585F RID: 22623 RVA: 0x0010EAD5 File Offset: 0x0010CCD5
		// (set) Token: 0x06005860 RID: 22624 RVA: 0x0010EADD File Offset: 0x0010CCDD
		public bool Passed
		{
			get
			{
				return this._Passed;
			}
			set
			{
				this._Passed = value;
				this.HasPassed = true;
			}
		}

		// Token: 0x06005861 RID: 22625 RVA: 0x0010EAED File Offset: 0x0010CCED
		public void SetPassed(bool val)
		{
			this.Passed = val;
		}

		// Token: 0x06005862 RID: 22626 RVA: 0x0010EAF8 File Offset: 0x0010CCF8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRequestToken)
			{
				num ^= this.RequestToken.GetHashCode();
			}
			if (this.HasPassed)
			{
				num ^= this.Passed.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005863 RID: 22627 RVA: 0x0010EB44 File Offset: 0x0010CD44
		public override bool Equals(object obj)
		{
			ChallengeExternalResult challengeExternalResult = obj as ChallengeExternalResult;
			return challengeExternalResult != null && this.HasRequestToken == challengeExternalResult.HasRequestToken && (!this.HasRequestToken || this.RequestToken.Equals(challengeExternalResult.RequestToken)) && this.HasPassed == challengeExternalResult.HasPassed && (!this.HasPassed || this.Passed.Equals(challengeExternalResult.Passed));
		}

		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x06005864 RID: 22628 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005865 RID: 22629 RVA: 0x0010EBB7 File Offset: 0x0010CDB7
		public static ChallengeExternalResult ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChallengeExternalResult>(bs, 0, -1);
		}

		// Token: 0x06005866 RID: 22630 RVA: 0x0010EBC1 File Offset: 0x0010CDC1
		public void Deserialize(Stream stream)
		{
			ChallengeExternalResult.Deserialize(stream, this);
		}

		// Token: 0x06005867 RID: 22631 RVA: 0x0010EBCB File Offset: 0x0010CDCB
		public static ChallengeExternalResult Deserialize(Stream stream, ChallengeExternalResult instance)
		{
			return ChallengeExternalResult.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005868 RID: 22632 RVA: 0x0010EBD8 File Offset: 0x0010CDD8
		public static ChallengeExternalResult DeserializeLengthDelimited(Stream stream)
		{
			ChallengeExternalResult challengeExternalResult = new ChallengeExternalResult();
			ChallengeExternalResult.DeserializeLengthDelimited(stream, challengeExternalResult);
			return challengeExternalResult;
		}

		// Token: 0x06005869 RID: 22633 RVA: 0x0010EBF4 File Offset: 0x0010CDF4
		public static ChallengeExternalResult DeserializeLengthDelimited(Stream stream, ChallengeExternalResult instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChallengeExternalResult.Deserialize(stream, instance, num);
		}

		// Token: 0x0600586A RID: 22634 RVA: 0x0010EC1C File Offset: 0x0010CE1C
		public static ChallengeExternalResult Deserialize(Stream stream, ChallengeExternalResult instance, long limit)
		{
			instance.Passed = true;
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
						instance.Passed = ProtocolParser.ReadBool(stream);
					}
				}
				else
				{
					instance.RequestToken = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600586B RID: 22635 RVA: 0x0010ECBB File Offset: 0x0010CEBB
		public void Serialize(Stream stream)
		{
			ChallengeExternalResult.Serialize(stream, this);
		}

		// Token: 0x0600586C RID: 22636 RVA: 0x0010ECC4 File Offset: 0x0010CEC4
		public static void Serialize(Stream stream, ChallengeExternalResult instance)
		{
			if (instance.HasRequestToken)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.RequestToken));
			}
			if (instance.HasPassed)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Passed);
			}
		}

		// Token: 0x0600586D RID: 22637 RVA: 0x0010ED14 File Offset: 0x0010CF14
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRequestToken)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.RequestToken);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasPassed)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04001BB1 RID: 7089
		public bool HasRequestToken;

		// Token: 0x04001BB2 RID: 7090
		private string _RequestToken;

		// Token: 0x04001BB3 RID: 7091
		public bool HasPassed;

		// Token: 0x04001BB4 RID: 7092
		private bool _Passed;
	}
}
