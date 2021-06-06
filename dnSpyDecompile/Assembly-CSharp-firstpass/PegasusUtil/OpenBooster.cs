using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000058 RID: 88
	public class OpenBooster : IProtoBuf
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x00016C3A File Offset: 0x00014E3A
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x00016C42 File Offset: 0x00014E42
		public int BoosterType { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00016C4B File Offset: 0x00014E4B
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x00016C53 File Offset: 0x00014E53
		public long FsgId
		{
			get
			{
				return this._FsgId;
			}
			set
			{
				this._FsgId = value;
				this.HasFsgId = true;
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00016C64 File Offset: 0x00014E64
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.BoosterType.GetHashCode();
			if (this.HasFsgId)
			{
				num ^= this.FsgId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00016CA8 File Offset: 0x00014EA8
		public override bool Equals(object obj)
		{
			OpenBooster openBooster = obj as OpenBooster;
			return openBooster != null && this.BoosterType.Equals(openBooster.BoosterType) && this.HasFsgId == openBooster.HasFsgId && (!this.HasFsgId || this.FsgId.Equals(openBooster.FsgId));
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00016D08 File Offset: 0x00014F08
		public void Deserialize(Stream stream)
		{
			OpenBooster.Deserialize(stream, this);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00016D12 File Offset: 0x00014F12
		public static OpenBooster Deserialize(Stream stream, OpenBooster instance)
		{
			return OpenBooster.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00016D20 File Offset: 0x00014F20
		public static OpenBooster DeserializeLengthDelimited(Stream stream)
		{
			OpenBooster openBooster = new OpenBooster();
			OpenBooster.DeserializeLengthDelimited(stream, openBooster);
			return openBooster;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00016D3C File Offset: 0x00014F3C
		public static OpenBooster DeserializeLengthDelimited(Stream stream, OpenBooster instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return OpenBooster.Deserialize(stream, instance, num);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00016D64 File Offset: 0x00014F64
		public static OpenBooster Deserialize(Stream stream, OpenBooster instance, long limit)
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
				else if (num != 16)
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
						instance.FsgId = (long)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.BoosterType = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00016DFD File Offset: 0x00014FFD
		public void Serialize(Stream stream)
		{
			OpenBooster.Serialize(stream, this);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00016E06 File Offset: 0x00015006
		public static void Serialize(Stream stream, OpenBooster instance)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BoosterType));
			if (instance.HasFsgId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FsgId);
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00016E3C File Offset: 0x0001503C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BoosterType));
			if (this.HasFsgId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.FsgId);
			}
			return num + 1U;
		}

		// Token: 0x04000206 RID: 518
		public bool HasFsgId;

		// Token: 0x04000207 RID: 519
		private long _FsgId;

		// Token: 0x0200056A RID: 1386
		public enum PacketID
		{
			// Token: 0x04001E96 RID: 7830
			ID = 225,
			// Token: 0x04001E97 RID: 7831
			System = 0
		}
	}
}
