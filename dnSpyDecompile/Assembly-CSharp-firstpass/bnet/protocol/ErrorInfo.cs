using System;
using System.IO;

namespace bnet.protocol
{
	// Token: 0x020002B3 RID: 691
	public class ErrorInfo : IProtoBuf
	{
		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x0600283C RID: 10300 RVA: 0x0008E1F2 File Offset: 0x0008C3F2
		// (set) Token: 0x0600283D RID: 10301 RVA: 0x0008E1FA File Offset: 0x0008C3FA
		public ObjectAddress ObjectAddress { get; set; }

		// Token: 0x0600283E RID: 10302 RVA: 0x0008E203 File Offset: 0x0008C403
		public void SetObjectAddress(ObjectAddress val)
		{
			this.ObjectAddress = val;
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x0600283F RID: 10303 RVA: 0x0008E20C File Offset: 0x0008C40C
		// (set) Token: 0x06002840 RID: 10304 RVA: 0x0008E214 File Offset: 0x0008C414
		public uint Status { get; set; }

		// Token: 0x06002841 RID: 10305 RVA: 0x0008E21D File Offset: 0x0008C41D
		public void SetStatus(uint val)
		{
			this.Status = val;
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06002842 RID: 10306 RVA: 0x0008E226 File Offset: 0x0008C426
		// (set) Token: 0x06002843 RID: 10307 RVA: 0x0008E22E File Offset: 0x0008C42E
		public uint ServiceHash { get; set; }

		// Token: 0x06002844 RID: 10308 RVA: 0x0008E237 File Offset: 0x0008C437
		public void SetServiceHash(uint val)
		{
			this.ServiceHash = val;
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06002845 RID: 10309 RVA: 0x0008E240 File Offset: 0x0008C440
		// (set) Token: 0x06002846 RID: 10310 RVA: 0x0008E248 File Offset: 0x0008C448
		public uint MethodId { get; set; }

		// Token: 0x06002847 RID: 10311 RVA: 0x0008E251 File Offset: 0x0008C451
		public void SetMethodId(uint val)
		{
			this.MethodId = val;
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x0008E25C File Offset: 0x0008C45C
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.ObjectAddress.GetHashCode() ^ this.Status.GetHashCode() ^ this.ServiceHash.GetHashCode() ^ this.MethodId.GetHashCode();
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x0008E2B0 File Offset: 0x0008C4B0
		public override bool Equals(object obj)
		{
			ErrorInfo errorInfo = obj as ErrorInfo;
			return errorInfo != null && this.ObjectAddress.Equals(errorInfo.ObjectAddress) && this.Status.Equals(errorInfo.Status) && this.ServiceHash.Equals(errorInfo.ServiceHash) && this.MethodId.Equals(errorInfo.MethodId);
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x0600284A RID: 10314 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600284B RID: 10315 RVA: 0x0008E327 File Offset: 0x0008C527
		public static ErrorInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ErrorInfo>(bs, 0, -1);
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x0008E331 File Offset: 0x0008C531
		public void Deserialize(Stream stream)
		{
			ErrorInfo.Deserialize(stream, this);
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x0008E33B File Offset: 0x0008C53B
		public static ErrorInfo Deserialize(Stream stream, ErrorInfo instance)
		{
			return ErrorInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x0008E348 File Offset: 0x0008C548
		public static ErrorInfo DeserializeLengthDelimited(Stream stream)
		{
			ErrorInfo errorInfo = new ErrorInfo();
			ErrorInfo.DeserializeLengthDelimited(stream, errorInfo);
			return errorInfo;
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x0008E364 File Offset: 0x0008C564
		public static ErrorInfo DeserializeLengthDelimited(Stream stream, ErrorInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ErrorInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x0008E38C File Offset: 0x0008C58C
		public static ErrorInfo Deserialize(Stream stream, ErrorInfo instance, long limit)
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
				else
				{
					if (num <= 16)
					{
						if (num != 10)
						{
							if (num == 16)
							{
								instance.Status = ProtocolParser.ReadUInt32(stream);
								continue;
							}
						}
						else
						{
							if (instance.ObjectAddress == null)
							{
								instance.ObjectAddress = ObjectAddress.DeserializeLengthDelimited(stream);
								continue;
							}
							ObjectAddress.DeserializeLengthDelimited(stream, instance.ObjectAddress);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.ServiceHash = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 32)
						{
							instance.MethodId = ProtocolParser.ReadUInt32(stream);
							continue;
						}
					}
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

		// Token: 0x06002851 RID: 10321 RVA: 0x0008E477 File Offset: 0x0008C677
		public void Serialize(Stream stream)
		{
			ErrorInfo.Serialize(stream, this);
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x0008E480 File Offset: 0x0008C680
		public static void Serialize(Stream stream, ErrorInfo instance)
		{
			if (instance.ObjectAddress == null)
			{
				throw new ArgumentNullException("ObjectAddress", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.ObjectAddress.GetSerializedSize());
			ObjectAddress.Serialize(stream, instance.ObjectAddress);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.Status);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt32(stream, instance.ServiceHash);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt32(stream, instance.MethodId);
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x0008E508 File Offset: 0x0008C708
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.ObjectAddress.GetSerializedSize();
			return num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + ProtocolParser.SizeOfUInt32(this.Status) + ProtocolParser.SizeOfUInt32(this.ServiceHash) + ProtocolParser.SizeOfUInt32(this.MethodId) + 4U;
		}
	}
}
