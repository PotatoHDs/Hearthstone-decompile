using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001C0 RID: 448
	public class PowerHistoryHide : IProtoBuf
	{
		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001C71 RID: 7281 RVA: 0x000644C2 File Offset: 0x000626C2
		// (set) Token: 0x06001C72 RID: 7282 RVA: 0x000644CA File Offset: 0x000626CA
		public int Entity { get; set; }

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001C73 RID: 7283 RVA: 0x000644D3 File Offset: 0x000626D3
		// (set) Token: 0x06001C74 RID: 7284 RVA: 0x000644DB File Offset: 0x000626DB
		public int Zone { get; set; }

		// Token: 0x06001C75 RID: 7285 RVA: 0x000644E4 File Offset: 0x000626E4
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Entity.GetHashCode() ^ this.Zone.GetHashCode();
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x0006451C File Offset: 0x0006271C
		public override bool Equals(object obj)
		{
			PowerHistoryHide powerHistoryHide = obj as PowerHistoryHide;
			return powerHistoryHide != null && this.Entity.Equals(powerHistoryHide.Entity) && this.Zone.Equals(powerHistoryHide.Zone);
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x00064566 File Offset: 0x00062766
		public void Deserialize(Stream stream)
		{
			PowerHistoryHide.Deserialize(stream, this);
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x00064570 File Offset: 0x00062770
		public static PowerHistoryHide Deserialize(Stream stream, PowerHistoryHide instance)
		{
			return PowerHistoryHide.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x0006457C File Offset: 0x0006277C
		public static PowerHistoryHide DeserializeLengthDelimited(Stream stream)
		{
			PowerHistoryHide powerHistoryHide = new PowerHistoryHide();
			PowerHistoryHide.DeserializeLengthDelimited(stream, powerHistoryHide);
			return powerHistoryHide;
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x00064598 File Offset: 0x00062798
		public static PowerHistoryHide DeserializeLengthDelimited(Stream stream, PowerHistoryHide instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PowerHistoryHide.Deserialize(stream, instance, num);
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x000645C0 File Offset: 0x000627C0
		public static PowerHistoryHide Deserialize(Stream stream, PowerHistoryHide instance, long limit)
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
						instance.Zone = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Entity = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x00064659 File Offset: 0x00062859
		public void Serialize(Stream stream)
		{
			PowerHistoryHide.Serialize(stream, this);
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x00064662 File Offset: 0x00062862
		public static void Serialize(Stream stream, PowerHistoryHide instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Entity));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Zone));
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x0006468D File Offset: 0x0006288D
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.Entity)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Zone)) + 2U;
		}
	}
}
