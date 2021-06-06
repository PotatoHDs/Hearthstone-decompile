using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x0200014F RID: 335
	public class ClassExclusionDbRecord : IProtoBuf
	{
		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001629 RID: 5673 RVA: 0x0004BD7D File Offset: 0x00049F7D
		// (set) Token: 0x0600162A RID: 5674 RVA: 0x0004BD85 File Offset: 0x00049F85
		public int ScenarioId { get; set; }

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x0600162B RID: 5675 RVA: 0x0004BD8E File Offset: 0x00049F8E
		// (set) Token: 0x0600162C RID: 5676 RVA: 0x0004BD96 File Offset: 0x00049F96
		public int ClassId { get; set; }

		// Token: 0x0600162D RID: 5677 RVA: 0x0004BDA0 File Offset: 0x00049FA0
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.ScenarioId.GetHashCode() ^ this.ClassId.GetHashCode();
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x0004BDD8 File Offset: 0x00049FD8
		public override bool Equals(object obj)
		{
			ClassExclusionDbRecord classExclusionDbRecord = obj as ClassExclusionDbRecord;
			return classExclusionDbRecord != null && this.ScenarioId.Equals(classExclusionDbRecord.ScenarioId) && this.ClassId.Equals(classExclusionDbRecord.ClassId);
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x0004BE22 File Offset: 0x0004A022
		public void Deserialize(Stream stream)
		{
			ClassExclusionDbRecord.Deserialize(stream, this);
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x0004BE2C File Offset: 0x0004A02C
		public static ClassExclusionDbRecord Deserialize(Stream stream, ClassExclusionDbRecord instance)
		{
			return ClassExclusionDbRecord.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x0004BE38 File Offset: 0x0004A038
		public static ClassExclusionDbRecord DeserializeLengthDelimited(Stream stream)
		{
			ClassExclusionDbRecord classExclusionDbRecord = new ClassExclusionDbRecord();
			ClassExclusionDbRecord.DeserializeLengthDelimited(stream, classExclusionDbRecord);
			return classExclusionDbRecord;
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x0004BE54 File Offset: 0x0004A054
		public static ClassExclusionDbRecord DeserializeLengthDelimited(Stream stream, ClassExclusionDbRecord instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ClassExclusionDbRecord.Deserialize(stream, instance, num);
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x0004BE7C File Offset: 0x0004A07C
		public static ClassExclusionDbRecord Deserialize(Stream stream, ClassExclusionDbRecord instance, long limit)
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
						instance.ClassId = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.ScenarioId = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x0004BF15 File Offset: 0x0004A115
		public void Serialize(Stream stream)
		{
			ClassExclusionDbRecord.Serialize(stream, this);
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x0004BF1E File Offset: 0x0004A11E
		public static void Serialize(Stream stream, ClassExclusionDbRecord instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ScenarioId));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ClassId));
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x0004BF49 File Offset: 0x0004A149
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.ScenarioId)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.ClassId)) + 2U;
		}
	}
}
