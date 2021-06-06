using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000163 RID: 355
	public class FSGPatron : IProtoBuf
	{
		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001859 RID: 6233 RVA: 0x000554F0 File Offset: 0x000536F0
		// (set) Token: 0x0600185A RID: 6234 RVA: 0x000554F8 File Offset: 0x000536F8
		public BnetId GameAccount { get; set; }

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x00055501 File Offset: 0x00053701
		// (set) Token: 0x0600185C RID: 6236 RVA: 0x00055509 File Offset: 0x00053709
		public BnetId BnetAccount { get; set; }

		// Token: 0x0600185D RID: 6237 RVA: 0x00055512 File Offset: 0x00053712
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.GameAccount.GetHashCode() ^ this.BnetAccount.GetHashCode();
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x00055538 File Offset: 0x00053738
		public override bool Equals(object obj)
		{
			FSGPatron fsgpatron = obj as FSGPatron;
			return fsgpatron != null && this.GameAccount.Equals(fsgpatron.GameAccount) && this.BnetAccount.Equals(fsgpatron.BnetAccount);
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x0005557C File Offset: 0x0005377C
		public void Deserialize(Stream stream)
		{
			FSGPatron.Deserialize(stream, this);
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x00055586 File Offset: 0x00053786
		public static FSGPatron Deserialize(Stream stream, FSGPatron instance)
		{
			return FSGPatron.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x00055594 File Offset: 0x00053794
		public static FSGPatron DeserializeLengthDelimited(Stream stream)
		{
			FSGPatron fsgpatron = new FSGPatron();
			FSGPatron.DeserializeLengthDelimited(stream, fsgpatron);
			return fsgpatron;
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x000555B0 File Offset: 0x000537B0
		public static FSGPatron DeserializeLengthDelimited(Stream stream, FSGPatron instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FSGPatron.Deserialize(stream, instance, num);
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x000555D8 File Offset: 0x000537D8
		public static FSGPatron Deserialize(Stream stream, FSGPatron instance, long limit)
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
					else if (instance.BnetAccount == null)
					{
						instance.BnetAccount = BnetId.DeserializeLengthDelimited(stream);
					}
					else
					{
						BnetId.DeserializeLengthDelimited(stream, instance.BnetAccount);
					}
				}
				else if (instance.GameAccount == null)
				{
					instance.GameAccount = BnetId.DeserializeLengthDelimited(stream);
				}
				else
				{
					BnetId.DeserializeLengthDelimited(stream, instance.GameAccount);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x000556AA File Offset: 0x000538AA
		public void Serialize(Stream stream)
		{
			FSGPatron.Serialize(stream, this);
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x000556B4 File Offset: 0x000538B4
		public static void Serialize(Stream stream, FSGPatron instance)
		{
			if (instance.GameAccount == null)
			{
				throw new ArgumentNullException("GameAccount", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameAccount.GetSerializedSize());
			BnetId.Serialize(stream, instance.GameAccount);
			if (instance.BnetAccount == null)
			{
				throw new ArgumentNullException("BnetAccount", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.BnetAccount.GetSerializedSize());
			BnetId.Serialize(stream, instance.BnetAccount);
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x0005573C File Offset: 0x0005393C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.GameAccount.GetSerializedSize();
			uint num2 = num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize));
			uint serializedSize2 = this.BnetAccount.GetSerializedSize();
			return num2 + (serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2)) + 2U;
		}
	}
}
