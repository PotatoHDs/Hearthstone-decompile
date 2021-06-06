using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000538 RID: 1336
	public class CAIS : IProtoBuf
	{
		// Token: 0x1700123C RID: 4668
		// (get) Token: 0x06006045 RID: 24645 RVA: 0x00123766 File Offset: 0x00121966
		// (set) Token: 0x06006046 RID: 24646 RVA: 0x0012376E File Offset: 0x0012196E
		public uint PlayedMinutes
		{
			get
			{
				return this._PlayedMinutes;
			}
			set
			{
				this._PlayedMinutes = value;
				this.HasPlayedMinutes = true;
			}
		}

		// Token: 0x06006047 RID: 24647 RVA: 0x0012377E File Offset: 0x0012197E
		public void SetPlayedMinutes(uint val)
		{
			this.PlayedMinutes = val;
		}

		// Token: 0x1700123D RID: 4669
		// (get) Token: 0x06006048 RID: 24648 RVA: 0x00123787 File Offset: 0x00121987
		// (set) Token: 0x06006049 RID: 24649 RVA: 0x0012378F File Offset: 0x0012198F
		public uint RestedMinutes
		{
			get
			{
				return this._RestedMinutes;
			}
			set
			{
				this._RestedMinutes = value;
				this.HasRestedMinutes = true;
			}
		}

		// Token: 0x0600604A RID: 24650 RVA: 0x0012379F File Offset: 0x0012199F
		public void SetRestedMinutes(uint val)
		{
			this.RestedMinutes = val;
		}

		// Token: 0x1700123E RID: 4670
		// (get) Token: 0x0600604B RID: 24651 RVA: 0x001237A8 File Offset: 0x001219A8
		// (set) Token: 0x0600604C RID: 24652 RVA: 0x001237B0 File Offset: 0x001219B0
		public ulong LastHeardTime
		{
			get
			{
				return this._LastHeardTime;
			}
			set
			{
				this._LastHeardTime = value;
				this.HasLastHeardTime = true;
			}
		}

		// Token: 0x0600604D RID: 24653 RVA: 0x001237C0 File Offset: 0x001219C0
		public void SetLastHeardTime(ulong val)
		{
			this.LastHeardTime = val;
		}

		// Token: 0x0600604E RID: 24654 RVA: 0x001237CC File Offset: 0x001219CC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPlayedMinutes)
			{
				num ^= this.PlayedMinutes.GetHashCode();
			}
			if (this.HasRestedMinutes)
			{
				num ^= this.RestedMinutes.GetHashCode();
			}
			if (this.HasLastHeardTime)
			{
				num ^= this.LastHeardTime.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600604F RID: 24655 RVA: 0x00123834 File Offset: 0x00121A34
		public override bool Equals(object obj)
		{
			CAIS cais = obj as CAIS;
			return cais != null && this.HasPlayedMinutes == cais.HasPlayedMinutes && (!this.HasPlayedMinutes || this.PlayedMinutes.Equals(cais.PlayedMinutes)) && this.HasRestedMinutes == cais.HasRestedMinutes && (!this.HasRestedMinutes || this.RestedMinutes.Equals(cais.RestedMinutes)) && this.HasLastHeardTime == cais.HasLastHeardTime && (!this.HasLastHeardTime || this.LastHeardTime.Equals(cais.LastHeardTime));
		}

		// Token: 0x1700123F RID: 4671
		// (get) Token: 0x06006050 RID: 24656 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006051 RID: 24657 RVA: 0x001238D8 File Offset: 0x00121AD8
		public static CAIS ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CAIS>(bs, 0, -1);
		}

		// Token: 0x06006052 RID: 24658 RVA: 0x001238E2 File Offset: 0x00121AE2
		public void Deserialize(Stream stream)
		{
			CAIS.Deserialize(stream, this);
		}

		// Token: 0x06006053 RID: 24659 RVA: 0x001238EC File Offset: 0x00121AEC
		public static CAIS Deserialize(Stream stream, CAIS instance)
		{
			return CAIS.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06006054 RID: 24660 RVA: 0x001238F8 File Offset: 0x00121AF8
		public static CAIS DeserializeLengthDelimited(Stream stream)
		{
			CAIS cais = new CAIS();
			CAIS.DeserializeLengthDelimited(stream, cais);
			return cais;
		}

		// Token: 0x06006055 RID: 24661 RVA: 0x00123914 File Offset: 0x00121B14
		public static CAIS DeserializeLengthDelimited(Stream stream, CAIS instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CAIS.Deserialize(stream, instance, num);
		}

		// Token: 0x06006056 RID: 24662 RVA: 0x0012393C File Offset: 0x00121B3C
		public static CAIS Deserialize(Stream stream, CAIS instance, long limit)
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
						if (num != 24)
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
							instance.LastHeardTime = ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.RestedMinutes = ProtocolParser.ReadUInt32(stream);
					}
				}
				else
				{
					instance.PlayedMinutes = ProtocolParser.ReadUInt32(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06006057 RID: 24663 RVA: 0x001239E9 File Offset: 0x00121BE9
		public void Serialize(Stream stream)
		{
			CAIS.Serialize(stream, this);
		}

		// Token: 0x06006058 RID: 24664 RVA: 0x001239F4 File Offset: 0x00121BF4
		public static void Serialize(Stream stream, CAIS instance)
		{
			if (instance.HasPlayedMinutes)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.PlayedMinutes);
			}
			if (instance.HasRestedMinutes)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.RestedMinutes);
			}
			if (instance.HasLastHeardTime)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.LastHeardTime);
			}
		}

		// Token: 0x06006059 RID: 24665 RVA: 0x00123A54 File Offset: 0x00121C54
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasPlayedMinutes)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.PlayedMinutes);
			}
			if (this.HasRestedMinutes)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.RestedMinutes);
			}
			if (this.HasLastHeardTime)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.LastHeardTime);
			}
			return num;
		}

		// Token: 0x04001DAD RID: 7597
		public bool HasPlayedMinutes;

		// Token: 0x04001DAE RID: 7598
		private uint _PlayedMinutes;

		// Token: 0x04001DAF RID: 7599
		public bool HasRestedMinutes;

		// Token: 0x04001DB0 RID: 7600
		private uint _RestedMinutes;

		// Token: 0x04001DB1 RID: 7601
		public bool HasLastHeardTime;

		// Token: 0x04001DB2 RID: 7602
		private ulong _LastHeardTime;
	}
}
