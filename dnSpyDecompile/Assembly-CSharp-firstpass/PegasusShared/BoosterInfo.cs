using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x0200011E RID: 286
	public class BoosterInfo : IProtoBuf
	{
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x00041AD9 File Offset: 0x0003FCD9
		// (set) Token: 0x060012C3 RID: 4803 RVA: 0x00041AE1 File Offset: 0x0003FCE1
		public int Type { get; set; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x00041AEA File Offset: 0x0003FCEA
		// (set) Token: 0x060012C5 RID: 4805 RVA: 0x00041AF2 File Offset: 0x0003FCF2
		public int Count { get; set; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x00041AFB File Offset: 0x0003FCFB
		// (set) Token: 0x060012C7 RID: 4807 RVA: 0x00041B03 File Offset: 0x0003FD03
		public int EverGrantedCount
		{
			get
			{
				return this._EverGrantedCount;
			}
			set
			{
				this._EverGrantedCount = value;
				this.HasEverGrantedCount = true;
			}
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x00041B14 File Offset: 0x0003FD14
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Type.GetHashCode();
			num ^= this.Count.GetHashCode();
			if (this.HasEverGrantedCount)
			{
				num ^= this.EverGrantedCount.GetHashCode();
			}
			return num;
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x00041B6C File Offset: 0x0003FD6C
		public override bool Equals(object obj)
		{
			BoosterInfo boosterInfo = obj as BoosterInfo;
			return boosterInfo != null && this.Type.Equals(boosterInfo.Type) && this.Count.Equals(boosterInfo.Count) && this.HasEverGrantedCount == boosterInfo.HasEverGrantedCount && (!this.HasEverGrantedCount || this.EverGrantedCount.Equals(boosterInfo.EverGrantedCount));
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x00041BE4 File Offset: 0x0003FDE4
		public void Deserialize(Stream stream)
		{
			BoosterInfo.Deserialize(stream, this);
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x00041BEE File Offset: 0x0003FDEE
		public static BoosterInfo Deserialize(Stream stream, BoosterInfo instance)
		{
			return BoosterInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x00041BFC File Offset: 0x0003FDFC
		public static BoosterInfo DeserializeLengthDelimited(Stream stream)
		{
			BoosterInfo boosterInfo = new BoosterInfo();
			BoosterInfo.DeserializeLengthDelimited(stream, boosterInfo);
			return boosterInfo;
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00041C18 File Offset: 0x0003FE18
		public static BoosterInfo DeserializeLengthDelimited(Stream stream, BoosterInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BoosterInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00041C40 File Offset: 0x0003FE40
		public static BoosterInfo Deserialize(Stream stream, BoosterInfo instance, long limit)
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
						if (num != 32)
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
							instance.EverGrantedCount = (int)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.Count = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Type = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x00041CF1 File Offset: 0x0003FEF1
		public void Serialize(Stream stream)
		{
			BoosterInfo.Serialize(stream, this);
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x00041CFC File Offset: 0x0003FEFC
		public static void Serialize(Stream stream, BoosterInfo instance)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Type));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Count));
			if (instance.HasEverGrantedCount)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.EverGrantedCount));
			}
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x00041D50 File Offset: 0x0003FF50
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Type));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Count));
			if (this.HasEverGrantedCount)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.EverGrantedCount));
			}
			return num + 2U;
		}

		// Token: 0x040005D8 RID: 1496
		public bool HasEverGrantedCount;

		// Token: 0x040005D9 RID: 1497
		private int _EverGrantedCount;
	}
}
