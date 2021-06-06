using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000063 RID: 99
	public class AckAchieveProgress : IProtoBuf
	{
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x0001835F File Offset: 0x0001655F
		// (set) Token: 0x06000640 RID: 1600 RVA: 0x00018367 File Offset: 0x00016567
		public int Id { get; set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x00018370 File Offset: 0x00016570
		// (set) Token: 0x06000642 RID: 1602 RVA: 0x00018378 File Offset: 0x00016578
		public int AckProgress { get; set; }

		// Token: 0x06000643 RID: 1603 RVA: 0x00018384 File Offset: 0x00016584
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Id.GetHashCode() ^ this.AckProgress.GetHashCode();
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x000183BC File Offset: 0x000165BC
		public override bool Equals(object obj)
		{
			AckAchieveProgress ackAchieveProgress = obj as AckAchieveProgress;
			return ackAchieveProgress != null && this.Id.Equals(ackAchieveProgress.Id) && this.AckProgress.Equals(ackAchieveProgress.AckProgress);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00018406 File Offset: 0x00016606
		public void Deserialize(Stream stream)
		{
			AckAchieveProgress.Deserialize(stream, this);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00018410 File Offset: 0x00016610
		public static AckAchieveProgress Deserialize(Stream stream, AckAchieveProgress instance)
		{
			return AckAchieveProgress.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001841C File Offset: 0x0001661C
		public static AckAchieveProgress DeserializeLengthDelimited(Stream stream)
		{
			AckAchieveProgress ackAchieveProgress = new AckAchieveProgress();
			AckAchieveProgress.DeserializeLengthDelimited(stream, ackAchieveProgress);
			return ackAchieveProgress;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00018438 File Offset: 0x00016638
		public static AckAchieveProgress DeserializeLengthDelimited(Stream stream, AckAchieveProgress instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AckAchieveProgress.Deserialize(stream, instance, num);
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00018460 File Offset: 0x00016660
		public static AckAchieveProgress Deserialize(Stream stream, AckAchieveProgress instance, long limit)
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
						instance.AckProgress = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Id = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x000184F9 File Offset: 0x000166F9
		public void Serialize(Stream stream)
		{
			AckAchieveProgress.Serialize(stream, this);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00018502 File Offset: 0x00016702
		public static void Serialize(Stream stream, AckAchieveProgress instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Id));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.AckProgress));
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0001852D File Offset: 0x0001672D
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.Id)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.AckProgress)) + 2U;
		}

		// Token: 0x02000575 RID: 1397
		public enum PacketID
		{
			// Token: 0x04001EB5 RID: 7861
			ID = 243,
			// Token: 0x04001EB6 RID: 7862
			System = 0
		}
	}
}
