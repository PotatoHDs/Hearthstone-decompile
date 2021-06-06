using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000534 RID: 1332
	public class RAFInfo : IProtoBuf
	{
		// Token: 0x1700122C RID: 4652
		// (get) Token: 0x06005FED RID: 24557 RVA: 0x00122986 File Offset: 0x00120B86
		// (set) Token: 0x06005FEE RID: 24558 RVA: 0x0012298E File Offset: 0x00120B8E
		public byte[] RafInfo
		{
			get
			{
				return this._RafInfo;
			}
			set
			{
				this._RafInfo = value;
				this.HasRafInfo = (value != null);
			}
		}

		// Token: 0x06005FEF RID: 24559 RVA: 0x001229A1 File Offset: 0x00120BA1
		public void SetRafInfo(byte[] val)
		{
			this.RafInfo = val;
		}

		// Token: 0x06005FF0 RID: 24560 RVA: 0x001229AC File Offset: 0x00120BAC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRafInfo)
			{
				num ^= this.RafInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005FF1 RID: 24561 RVA: 0x001229DC File Offset: 0x00120BDC
		public override bool Equals(object obj)
		{
			RAFInfo rafinfo = obj as RAFInfo;
			return rafinfo != null && this.HasRafInfo == rafinfo.HasRafInfo && (!this.HasRafInfo || this.RafInfo.Equals(rafinfo.RafInfo));
		}

		// Token: 0x1700122D RID: 4653
		// (get) Token: 0x06005FF2 RID: 24562 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005FF3 RID: 24563 RVA: 0x00122A21 File Offset: 0x00120C21
		public static RAFInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RAFInfo>(bs, 0, -1);
		}

		// Token: 0x06005FF4 RID: 24564 RVA: 0x00122A2B File Offset: 0x00120C2B
		public void Deserialize(Stream stream)
		{
			RAFInfo.Deserialize(stream, this);
		}

		// Token: 0x06005FF5 RID: 24565 RVA: 0x00122A35 File Offset: 0x00120C35
		public static RAFInfo Deserialize(Stream stream, RAFInfo instance)
		{
			return RAFInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005FF6 RID: 24566 RVA: 0x00122A40 File Offset: 0x00120C40
		public static RAFInfo DeserializeLengthDelimited(Stream stream)
		{
			RAFInfo rafinfo = new RAFInfo();
			RAFInfo.DeserializeLengthDelimited(stream, rafinfo);
			return rafinfo;
		}

		// Token: 0x06005FF7 RID: 24567 RVA: 0x00122A5C File Offset: 0x00120C5C
		public static RAFInfo DeserializeLengthDelimited(Stream stream, RAFInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RAFInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x06005FF8 RID: 24568 RVA: 0x00122A84 File Offset: 0x00120C84
		public static RAFInfo Deserialize(Stream stream, RAFInfo instance, long limit)
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
				else if (num == 10)
				{
					instance.RafInfo = ProtocolParser.ReadBytes(stream);
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

		// Token: 0x06005FF9 RID: 24569 RVA: 0x00122B04 File Offset: 0x00120D04
		public void Serialize(Stream stream)
		{
			RAFInfo.Serialize(stream, this);
		}

		// Token: 0x06005FFA RID: 24570 RVA: 0x00122B0D File Offset: 0x00120D0D
		public static void Serialize(Stream stream, RAFInfo instance)
		{
			if (instance.HasRafInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, instance.RafInfo);
			}
		}

		// Token: 0x06005FFB RID: 24571 RVA: 0x00122B2C File Offset: 0x00120D2C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRafInfo)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.RafInfo.Length) + (uint)this.RafInfo.Length;
			}
			return num;
		}

		// Token: 0x04001D95 RID: 7573
		public bool HasRafInfo;

		// Token: 0x04001D96 RID: 7574
		private byte[] _RafInfo;
	}
}
