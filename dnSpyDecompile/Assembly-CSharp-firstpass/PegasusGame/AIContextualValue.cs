using System;
using System.IO;
using System.Text;

namespace PegasusGame
{
	// Token: 0x02000199 RID: 409
	public class AIContextualValue : IProtoBuf
	{
		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060019AC RID: 6572 RVA: 0x0005AD99 File Offset: 0x00058F99
		// (set) Token: 0x060019AD RID: 6573 RVA: 0x0005ADA1 File Offset: 0x00058FA1
		public string EntityName { get; set; }

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060019AE RID: 6574 RVA: 0x0005ADAA File Offset: 0x00058FAA
		// (set) Token: 0x060019AF RID: 6575 RVA: 0x0005ADB2 File Offset: 0x00058FB2
		public int EntityID { get; set; }

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060019B0 RID: 6576 RVA: 0x0005ADBB File Offset: 0x00058FBB
		// (set) Token: 0x060019B1 RID: 6577 RVA: 0x0005ADC3 File Offset: 0x00058FC3
		public int ContextualScore { get; set; }

		// Token: 0x060019B2 RID: 6578 RVA: 0x0005ADCC File Offset: 0x00058FCC
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.EntityName.GetHashCode() ^ this.EntityID.GetHashCode() ^ this.ContextualScore.GetHashCode();
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x0005AE10 File Offset: 0x00059010
		public override bool Equals(object obj)
		{
			AIContextualValue aicontextualValue = obj as AIContextualValue;
			return aicontextualValue != null && this.EntityName.Equals(aicontextualValue.EntityName) && this.EntityID.Equals(aicontextualValue.EntityID) && this.ContextualScore.Equals(aicontextualValue.ContextualScore);
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x0005AE6F File Offset: 0x0005906F
		public void Deserialize(Stream stream)
		{
			AIContextualValue.Deserialize(stream, this);
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x0005AE79 File Offset: 0x00059079
		public static AIContextualValue Deserialize(Stream stream, AIContextualValue instance)
		{
			return AIContextualValue.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x0005AE84 File Offset: 0x00059084
		public static AIContextualValue DeserializeLengthDelimited(Stream stream)
		{
			AIContextualValue aicontextualValue = new AIContextualValue();
			AIContextualValue.DeserializeLengthDelimited(stream, aicontextualValue);
			return aicontextualValue;
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x0005AEA0 File Offset: 0x000590A0
		public static AIContextualValue DeserializeLengthDelimited(Stream stream, AIContextualValue instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AIContextualValue.Deserialize(stream, instance, num);
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x0005AEC8 File Offset: 0x000590C8
		public static AIContextualValue Deserialize(Stream stream, AIContextualValue instance, long limit)
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
							instance.ContextualScore = (int)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.EntityID = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.EntityName = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x0005AF78 File Offset: 0x00059178
		public void Serialize(Stream stream)
		{
			AIContextualValue.Serialize(stream, this);
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x0005AF84 File Offset: 0x00059184
		public static void Serialize(Stream stream, AIContextualValue instance)
		{
			if (instance.EntityName == null)
			{
				throw new ArgumentNullException("EntityName", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.EntityName));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.EntityID));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ContextualScore));
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x0005AFF4 File Offset: 0x000591F4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.EntityName);
			return num + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + ProtocolParser.SizeOfUInt64((ulong)((long)this.EntityID)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.ContextualScore)) + 3U;
		}
	}
}
