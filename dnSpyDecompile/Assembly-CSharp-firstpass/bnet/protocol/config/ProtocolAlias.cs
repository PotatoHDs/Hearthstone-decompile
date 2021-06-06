using System;
using System.IO;
using System.Text;

namespace bnet.protocol.config
{
	// Token: 0x02000350 RID: 848
	public class ProtocolAlias : IProtoBuf
	{
		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06003539 RID: 13625 RVA: 0x000B08CF File Offset: 0x000AEACF
		// (set) Token: 0x0600353A RID: 13626 RVA: 0x000B08D7 File Offset: 0x000AEAD7
		public string ServerServiceName { get; set; }

		// Token: 0x0600353B RID: 13627 RVA: 0x000B08E0 File Offset: 0x000AEAE0
		public void SetServerServiceName(string val)
		{
			this.ServerServiceName = val;
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x0600353C RID: 13628 RVA: 0x000B08E9 File Offset: 0x000AEAE9
		// (set) Token: 0x0600353D RID: 13629 RVA: 0x000B08F1 File Offset: 0x000AEAF1
		public string ClientServiceName { get; set; }

		// Token: 0x0600353E RID: 13630 RVA: 0x000B08FA File Offset: 0x000AEAFA
		public void SetClientServiceName(string val)
		{
			this.ClientServiceName = val;
		}

		// Token: 0x0600353F RID: 13631 RVA: 0x000B0903 File Offset: 0x000AEB03
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.ServerServiceName.GetHashCode() ^ this.ClientServiceName.GetHashCode();
		}

		// Token: 0x06003540 RID: 13632 RVA: 0x000B0928 File Offset: 0x000AEB28
		public override bool Equals(object obj)
		{
			ProtocolAlias protocolAlias = obj as ProtocolAlias;
			return protocolAlias != null && this.ServerServiceName.Equals(protocolAlias.ServerServiceName) && this.ClientServiceName.Equals(protocolAlias.ClientServiceName);
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06003541 RID: 13633 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003542 RID: 13634 RVA: 0x000B096C File Offset: 0x000AEB6C
		public static ProtocolAlias ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ProtocolAlias>(bs, 0, -1);
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x000B0976 File Offset: 0x000AEB76
		public void Deserialize(Stream stream)
		{
			ProtocolAlias.Deserialize(stream, this);
		}

		// Token: 0x06003544 RID: 13636 RVA: 0x000B0980 File Offset: 0x000AEB80
		public static ProtocolAlias Deserialize(Stream stream, ProtocolAlias instance)
		{
			return ProtocolAlias.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003545 RID: 13637 RVA: 0x000B098C File Offset: 0x000AEB8C
		public static ProtocolAlias DeserializeLengthDelimited(Stream stream)
		{
			ProtocolAlias protocolAlias = new ProtocolAlias();
			ProtocolAlias.DeserializeLengthDelimited(stream, protocolAlias);
			return protocolAlias;
		}

		// Token: 0x06003546 RID: 13638 RVA: 0x000B09A8 File Offset: 0x000AEBA8
		public static ProtocolAlias DeserializeLengthDelimited(Stream stream, ProtocolAlias instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProtocolAlias.Deserialize(stream, instance, num);
		}

		// Token: 0x06003547 RID: 13639 RVA: 0x000B09D0 File Offset: 0x000AEBD0
		public static ProtocolAlias Deserialize(Stream stream, ProtocolAlias instance, long limit)
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
					else
					{
						instance.ClientServiceName = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.ServerServiceName = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003548 RID: 13640 RVA: 0x000B0A68 File Offset: 0x000AEC68
		public void Serialize(Stream stream)
		{
			ProtocolAlias.Serialize(stream, this);
		}

		// Token: 0x06003549 RID: 13641 RVA: 0x000B0A74 File Offset: 0x000AEC74
		public static void Serialize(Stream stream, ProtocolAlias instance)
		{
			if (instance.ServerServiceName == null)
			{
				throw new ArgumentNullException("ServerServiceName", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ServerServiceName));
			if (instance.ClientServiceName == null)
			{
				throw new ArgumentNullException("ClientServiceName", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientServiceName));
		}

		// Token: 0x0600354A RID: 13642 RVA: 0x000B0AF0 File Offset: 0x000AECF0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ServerServiceName);
			uint num2 = num + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount);
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.ClientServiceName);
			return num2 + (ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2) + 2U;
		}
	}
}
