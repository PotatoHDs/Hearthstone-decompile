using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	// Token: 0x0200004F RID: 79
	public class DevBnetIdentify : IProtoBuf
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x00015328 File Offset: 0x00013528
		// (set) Token: 0x06000514 RID: 1300 RVA: 0x00015330 File Offset: 0x00013530
		public string Name { get; set; }

		// Token: 0x06000515 RID: 1301 RVA: 0x00015339 File Offset: 0x00013539
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Name.GetHashCode();
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00015354 File Offset: 0x00013554
		public override bool Equals(object obj)
		{
			DevBnetIdentify devBnetIdentify = obj as DevBnetIdentify;
			return devBnetIdentify != null && this.Name.Equals(devBnetIdentify.Name);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00015383 File Offset: 0x00013583
		public void Deserialize(Stream stream)
		{
			DevBnetIdentify.Deserialize(stream, this);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0001538D File Offset: 0x0001358D
		public static DevBnetIdentify Deserialize(Stream stream, DevBnetIdentify instance)
		{
			return DevBnetIdentify.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00015398 File Offset: 0x00013598
		public static DevBnetIdentify DeserializeLengthDelimited(Stream stream)
		{
			DevBnetIdentify devBnetIdentify = new DevBnetIdentify();
			DevBnetIdentify.DeserializeLengthDelimited(stream, devBnetIdentify);
			return devBnetIdentify;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x000153B4 File Offset: 0x000135B4
		public static DevBnetIdentify DeserializeLengthDelimited(Stream stream, DevBnetIdentify instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DevBnetIdentify.Deserialize(stream, instance, num);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x000153DC File Offset: 0x000135DC
		public static DevBnetIdentify Deserialize(Stream stream, DevBnetIdentify instance, long limit)
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
					instance.Name = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600051C RID: 1308 RVA: 0x0001545C File Offset: 0x0001365C
		public void Serialize(Stream stream)
		{
			DevBnetIdentify.Serialize(stream, this);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00015465 File Offset: 0x00013665
		public static void Serialize(Stream stream, DevBnetIdentify instance)
		{
			if (instance.Name == null)
			{
				throw new ArgumentNullException("Name", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x000154A0 File Offset: 0x000136A0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
			return num + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + 1U;
		}

		// Token: 0x0200055F RID: 1375
		public enum PacketID
		{
			// Token: 0x04001E5B RID: 7771
			ID = 259,
			// Token: 0x04001E5C RID: 7772
			System = 0
		}
	}
}
