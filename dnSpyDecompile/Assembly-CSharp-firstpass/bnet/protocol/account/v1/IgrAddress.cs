using System;
using System.IO;
using System.Text;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000540 RID: 1344
	public class IgrAddress : IProtoBuf
	{
		// Token: 0x17001265 RID: 4709
		// (get) Token: 0x06006106 RID: 24838 RVA: 0x001259A7 File Offset: 0x00123BA7
		// (set) Token: 0x06006107 RID: 24839 RVA: 0x001259AF File Offset: 0x00123BAF
		public string ClientAddress
		{
			get
			{
				return this._ClientAddress;
			}
			set
			{
				this._ClientAddress = value;
				this.HasClientAddress = (value != null);
			}
		}

		// Token: 0x06006108 RID: 24840 RVA: 0x001259C2 File Offset: 0x00123BC2
		public void SetClientAddress(string val)
		{
			this.ClientAddress = val;
		}

		// Token: 0x17001266 RID: 4710
		// (get) Token: 0x06006109 RID: 24841 RVA: 0x001259CB File Offset: 0x00123BCB
		// (set) Token: 0x0600610A RID: 24842 RVA: 0x001259D3 File Offset: 0x00123BD3
		public uint Region
		{
			get
			{
				return this._Region;
			}
			set
			{
				this._Region = value;
				this.HasRegion = true;
			}
		}

		// Token: 0x0600610B RID: 24843 RVA: 0x001259E3 File Offset: 0x00123BE3
		public void SetRegion(uint val)
		{
			this.Region = val;
		}

		// Token: 0x0600610C RID: 24844 RVA: 0x001259EC File Offset: 0x00123BEC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasClientAddress)
			{
				num ^= this.ClientAddress.GetHashCode();
			}
			if (this.HasRegion)
			{
				num ^= this.Region.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600610D RID: 24845 RVA: 0x00125A38 File Offset: 0x00123C38
		public override bool Equals(object obj)
		{
			IgrAddress igrAddress = obj as IgrAddress;
			return igrAddress != null && this.HasClientAddress == igrAddress.HasClientAddress && (!this.HasClientAddress || this.ClientAddress.Equals(igrAddress.ClientAddress)) && this.HasRegion == igrAddress.HasRegion && (!this.HasRegion || this.Region.Equals(igrAddress.Region));
		}

		// Token: 0x17001267 RID: 4711
		// (get) Token: 0x0600610E RID: 24846 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600610F RID: 24847 RVA: 0x00125AAB File Offset: 0x00123CAB
		public static IgrAddress ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<IgrAddress>(bs, 0, -1);
		}

		// Token: 0x06006110 RID: 24848 RVA: 0x00125AB5 File Offset: 0x00123CB5
		public void Deserialize(Stream stream)
		{
			IgrAddress.Deserialize(stream, this);
		}

		// Token: 0x06006111 RID: 24849 RVA: 0x00125ABF File Offset: 0x00123CBF
		public static IgrAddress Deserialize(Stream stream, IgrAddress instance)
		{
			return IgrAddress.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06006112 RID: 24850 RVA: 0x00125ACC File Offset: 0x00123CCC
		public static IgrAddress DeserializeLengthDelimited(Stream stream)
		{
			IgrAddress igrAddress = new IgrAddress();
			IgrAddress.DeserializeLengthDelimited(stream, igrAddress);
			return igrAddress;
		}

		// Token: 0x06006113 RID: 24851 RVA: 0x00125AE8 File Offset: 0x00123CE8
		public static IgrAddress DeserializeLengthDelimited(Stream stream, IgrAddress instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return IgrAddress.Deserialize(stream, instance, num);
		}

		// Token: 0x06006114 RID: 24852 RVA: 0x00125B10 File Offset: 0x00123D10
		public static IgrAddress Deserialize(Stream stream, IgrAddress instance, long limit)
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
						instance.Region = ProtocolParser.ReadUInt32(stream);
					}
				}
				else
				{
					instance.ClientAddress = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06006115 RID: 24853 RVA: 0x00125BA8 File Offset: 0x00123DA8
		public void Serialize(Stream stream)
		{
			IgrAddress.Serialize(stream, this);
		}

		// Token: 0x06006116 RID: 24854 RVA: 0x00125BB4 File Offset: 0x00123DB4
		public static void Serialize(Stream stream, IgrAddress instance)
		{
			if (instance.HasClientAddress)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientAddress));
			}
			if (instance.HasRegion)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Region);
			}
		}

		// Token: 0x06006117 RID: 24855 RVA: 0x00125C04 File Offset: 0x00123E04
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasClientAddress)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ClientAddress);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasRegion)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Region);
			}
			return num;
		}

		// Token: 0x04001DD6 RID: 7638
		public bool HasClientAddress;

		// Token: 0x04001DD7 RID: 7639
		private string _ClientAddress;

		// Token: 0x04001DD8 RID: 7640
		public bool HasRegion;

		// Token: 0x04001DD9 RID: 7641
		private uint _Region;
	}
}
