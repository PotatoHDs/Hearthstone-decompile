using System;
using System.IO;

namespace bnet.protocol
{
	// Token: 0x020002B9 RID: 697
	public class UnsignedIntRange : IProtoBuf
	{
		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x0600290D RID: 10509 RVA: 0x000909E6 File Offset: 0x0008EBE6
		// (set) Token: 0x0600290E RID: 10510 RVA: 0x000909EE File Offset: 0x0008EBEE
		public ulong Min
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

		// Token: 0x0600290F RID: 10511 RVA: 0x000909FE File Offset: 0x0008EBFE
		public void SetMin(ulong val)
		{
			this.Min = val;
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06002910 RID: 10512 RVA: 0x00090A07 File Offset: 0x0008EC07
		// (set) Token: 0x06002911 RID: 10513 RVA: 0x00090A0F File Offset: 0x0008EC0F
		public ulong Max
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

		// Token: 0x06002912 RID: 10514 RVA: 0x00090A1F File Offset: 0x0008EC1F
		public void SetMax(ulong val)
		{
			this.Max = val;
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x00090A28 File Offset: 0x0008EC28
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

		// Token: 0x06002914 RID: 10516 RVA: 0x00090A74 File Offset: 0x0008EC74
		public override bool Equals(object obj)
		{
			UnsignedIntRange unsignedIntRange = obj as UnsignedIntRange;
			return unsignedIntRange != null && this.HasMin == unsignedIntRange.HasMin && (!this.HasMin || this.Min.Equals(unsignedIntRange.Min)) && this.HasMax == unsignedIntRange.HasMax && (!this.HasMax || this.Max.Equals(unsignedIntRange.Max));
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06002915 RID: 10517 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x00090AEA File Offset: 0x0008ECEA
		public static UnsignedIntRange ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnsignedIntRange>(bs, 0, -1);
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x00090AF4 File Offset: 0x0008ECF4
		public void Deserialize(Stream stream)
		{
			UnsignedIntRange.Deserialize(stream, this);
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x00090AFE File Offset: 0x0008ECFE
		public static UnsignedIntRange Deserialize(Stream stream, UnsignedIntRange instance)
		{
			return UnsignedIntRange.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x00090B0C File Offset: 0x0008ED0C
		public static UnsignedIntRange DeserializeLengthDelimited(Stream stream)
		{
			UnsignedIntRange unsignedIntRange = new UnsignedIntRange();
			UnsignedIntRange.DeserializeLengthDelimited(stream, unsignedIntRange);
			return unsignedIntRange;
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x00090B28 File Offset: 0x0008ED28
		public static UnsignedIntRange DeserializeLengthDelimited(Stream stream, UnsignedIntRange instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UnsignedIntRange.Deserialize(stream, instance, num);
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x00090B50 File Offset: 0x0008ED50
		public static UnsignedIntRange Deserialize(Stream stream, UnsignedIntRange instance, long limit)
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
						instance.Max = ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Min = ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x00090BE7 File Offset: 0x0008EDE7
		public void Serialize(Stream stream)
		{
			UnsignedIntRange.Serialize(stream, this);
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x00090BF0 File Offset: 0x0008EDF0
		public static void Serialize(Stream stream, UnsignedIntRange instance)
		{
			if (instance.HasMin)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.Min);
			}
			if (instance.HasMax)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.Max);
			}
		}

		// Token: 0x0600291E RID: 10526 RVA: 0x00090C2C File Offset: 0x0008EE2C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasMin)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Min);
			}
			if (this.HasMax)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Max);
			}
			return num;
		}

		// Token: 0x04001198 RID: 4504
		public bool HasMin;

		// Token: 0x04001199 RID: 4505
		private ulong _Min;

		// Token: 0x0400119A RID: 4506
		public bool HasMax;

		// Token: 0x0400119B RID: 4507
		private ulong _Max;
	}
}
