using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011EC RID: 4588
	public class RepairPrestep : IProtoBuf
	{
		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x0600CCFA RID: 52474 RVA: 0x003D2CAA File Offset: 0x003D0EAA
		// (set) Token: 0x0600CCFB RID: 52475 RVA: 0x003D2CB2 File Offset: 0x003D0EB2
		public int DoubletapFingers
		{
			get
			{
				return this._DoubletapFingers;
			}
			set
			{
				this._DoubletapFingers = value;
				this.HasDoubletapFingers = true;
			}
		}

		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x0600CCFC RID: 52476 RVA: 0x003D2CC2 File Offset: 0x003D0EC2
		// (set) Token: 0x0600CCFD RID: 52477 RVA: 0x003D2CCA File Offset: 0x003D0ECA
		public int Locales
		{
			get
			{
				return this._Locales;
			}
			set
			{
				this._Locales = value;
				this.HasLocales = true;
			}
		}

		// Token: 0x0600CCFE RID: 52478 RVA: 0x003D2CDC File Offset: 0x003D0EDC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDoubletapFingers)
			{
				num ^= this.DoubletapFingers.GetHashCode();
			}
			if (this.HasLocales)
			{
				num ^= this.Locales.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CCFF RID: 52479 RVA: 0x003D2D28 File Offset: 0x003D0F28
		public override bool Equals(object obj)
		{
			RepairPrestep repairPrestep = obj as RepairPrestep;
			return repairPrestep != null && this.HasDoubletapFingers == repairPrestep.HasDoubletapFingers && (!this.HasDoubletapFingers || this.DoubletapFingers.Equals(repairPrestep.DoubletapFingers)) && this.HasLocales == repairPrestep.HasLocales && (!this.HasLocales || this.Locales.Equals(repairPrestep.Locales));
		}

		// Token: 0x0600CD00 RID: 52480 RVA: 0x003D2D9E File Offset: 0x003D0F9E
		public void Deserialize(Stream stream)
		{
			RepairPrestep.Deserialize(stream, this);
		}

		// Token: 0x0600CD01 RID: 52481 RVA: 0x003D2DA8 File Offset: 0x003D0FA8
		public static RepairPrestep Deserialize(Stream stream, RepairPrestep instance)
		{
			return RepairPrestep.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CD02 RID: 52482 RVA: 0x003D2DB4 File Offset: 0x003D0FB4
		public static RepairPrestep DeserializeLengthDelimited(Stream stream)
		{
			RepairPrestep repairPrestep = new RepairPrestep();
			RepairPrestep.DeserializeLengthDelimited(stream, repairPrestep);
			return repairPrestep;
		}

		// Token: 0x0600CD03 RID: 52483 RVA: 0x003D2DD0 File Offset: 0x003D0FD0
		public static RepairPrestep DeserializeLengthDelimited(Stream stream, RepairPrestep instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RepairPrestep.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CD04 RID: 52484 RVA: 0x003D2DF8 File Offset: 0x003D0FF8
		public static RepairPrestep Deserialize(Stream stream, RepairPrestep instance, long limit)
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
						instance.Locales = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.DoubletapFingers = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600CD05 RID: 52485 RVA: 0x003D2E91 File Offset: 0x003D1091
		public void Serialize(Stream stream)
		{
			RepairPrestep.Serialize(stream, this);
		}

		// Token: 0x0600CD06 RID: 52486 RVA: 0x003D2E9A File Offset: 0x003D109A
		public static void Serialize(Stream stream, RepairPrestep instance)
		{
			if (instance.HasDoubletapFingers)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DoubletapFingers));
			}
			if (instance.HasLocales)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Locales));
			}
		}

		// Token: 0x0600CD07 RID: 52487 RVA: 0x003D2ED8 File Offset: 0x003D10D8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDoubletapFingers)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DoubletapFingers));
			}
			if (this.HasLocales)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Locales));
			}
			return num;
		}

		// Token: 0x0400A0CF RID: 41167
		public bool HasDoubletapFingers;

		// Token: 0x0400A0D0 RID: 41168
		private int _DoubletapFingers;

		// Token: 0x0400A0D1 RID: 41169
		public bool HasLocales;

		// Token: 0x0400A0D2 RID: 41170
		private int _Locales;
	}
}
