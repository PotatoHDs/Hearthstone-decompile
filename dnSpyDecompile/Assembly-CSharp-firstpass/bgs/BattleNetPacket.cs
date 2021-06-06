using System;
using System.IO;
using bnet.protocol;

namespace bgs
{
	// Token: 0x0200021C RID: 540
	public class BattleNetPacket : PacketFormat
	{
		// Token: 0x060022E4 RID: 8932 RVA: 0x0007B1DE File Offset: 0x000793DE
		public Header GetHeader()
		{
			return this.header;
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x0007B1E6 File Offset: 0x000793E6
		public object GetBody()
		{
			return this.body;
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x0007B1EE File Offset: 0x000793EE
		public BattleNetPacket()
		{
			this.header = null;
			this.body = null;
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x0007B212 File Offset: 0x00079412
		public BattleNetPacket(Header h, IProtoBuf b)
		{
			this.header = h;
			this.body = b;
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x0007B236 File Offset: 0x00079436
		public override bool IsLoaded()
		{
			return this.header != null && this.body != null;
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x0007B24C File Offset: 0x0007944C
		public override int Decode(byte[] bytes, int offset, int available)
		{
			int num = 0;
			if (this.headerSize < 0)
			{
				if (available < 2)
				{
					return num;
				}
				this.headerSize = ((int)bytes[offset] << 8) + (int)bytes[offset + 1];
				available -= 2;
				num += 2;
				offset += 2;
			}
			if (this.header == null)
			{
				if (available < this.headerSize)
				{
					return num;
				}
				this.header = new Header();
				this.header.Deserialize(new MemoryStream(bytes, offset, this.headerSize));
				this.bodySize = (int)(this.header.HasSize ? this.header.Size : 0U);
				if (this.header == null)
				{
					throw new Exception("failed to parse BattleNet packet header");
				}
				available -= this.headerSize;
				num += this.headerSize;
				offset += this.headerSize;
			}
			if (this.body == null)
			{
				if (available < this.bodySize)
				{
					return num;
				}
				byte[] destinationArray = new byte[this.bodySize];
				Array.Copy(bytes, offset, destinationArray, 0, this.bodySize);
				this.body = destinationArray;
				num += this.bodySize;
			}
			return num;
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x0007B350 File Offset: 0x00079550
		public override byte[] Encode()
		{
			if (!(this.body is IProtoBuf))
			{
				return null;
			}
			IProtoBuf protoBuf = (IProtoBuf)this.body;
			int serializedSize = (int)this.header.GetSerializedSize();
			int serializedSize2 = (int)protoBuf.GetSerializedSize();
			byte[] array = new byte[2 + serializedSize + serializedSize2];
			array[0] = (byte)(serializedSize >> 8 & 255);
			array[1] = (byte)(serializedSize & 255);
			this.header.Serialize(new MemoryStream(array, 2, serializedSize));
			protoBuf.Serialize(new MemoryStream(array, 2 + serializedSize, serializedSize2));
			return array;
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x0007B3D0 File Offset: 0x000795D0
		public override string ToString()
		{
			if (this.header == null)
			{
				return "BattleNetPacket (missing header)";
			}
			return string.Format("BattleNetPacket ServiceID: {0}  MethodId: {1}", this.header.ServiceId, this.header.MethodId);
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x00003D71 File Offset: 0x00001F71
		public override bool IsFatalOnError()
		{
			return false;
		}

		// Token: 0x04000E5E RID: 3678
		private Header header;

		// Token: 0x04000E5F RID: 3679
		private object body;

		// Token: 0x04000E60 RID: 3680
		private int headerSize = -1;

		// Token: 0x04000E61 RID: 3681
		private int bodySize = -1;
	}
}
