using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001AA RID: 426
	public class ChooseOption : IProtoBuf
	{
		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001AE9 RID: 6889 RVA: 0x0005F45A File Offset: 0x0005D65A
		// (set) Token: 0x06001AEA RID: 6890 RVA: 0x0005F462 File Offset: 0x0005D662
		public int Id { get; set; }

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001AEB RID: 6891 RVA: 0x0005F46B File Offset: 0x0005D66B
		// (set) Token: 0x06001AEC RID: 6892 RVA: 0x0005F473 File Offset: 0x0005D673
		public int Index { get; set; }

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001AED RID: 6893 RVA: 0x0005F47C File Offset: 0x0005D67C
		// (set) Token: 0x06001AEE RID: 6894 RVA: 0x0005F484 File Offset: 0x0005D684
		public int Target { get; set; }

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x0005F48D File Offset: 0x0005D68D
		// (set) Token: 0x06001AF0 RID: 6896 RVA: 0x0005F495 File Offset: 0x0005D695
		public int SubOption
		{
			get
			{
				return this._SubOption;
			}
			set
			{
				this._SubOption = value;
				this.HasSubOption = true;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x0005F4A5 File Offset: 0x0005D6A5
		// (set) Token: 0x06001AF2 RID: 6898 RVA: 0x0005F4AD File Offset: 0x0005D6AD
		public int Position
		{
			get
			{
				return this._Position;
			}
			set
			{
				this._Position = value;
				this.HasPosition = true;
			}
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x0005F4C0 File Offset: 0x0005D6C0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			num ^= this.Index.GetHashCode();
			num ^= this.Target.GetHashCode();
			if (this.HasSubOption)
			{
				num ^= this.SubOption.GetHashCode();
			}
			if (this.HasPosition)
			{
				num ^= this.Position.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x0005F540 File Offset: 0x0005D740
		public override bool Equals(object obj)
		{
			ChooseOption chooseOption = obj as ChooseOption;
			return chooseOption != null && this.Id.Equals(chooseOption.Id) && this.Index.Equals(chooseOption.Index) && this.Target.Equals(chooseOption.Target) && this.HasSubOption == chooseOption.HasSubOption && (!this.HasSubOption || this.SubOption.Equals(chooseOption.SubOption)) && this.HasPosition == chooseOption.HasPosition && (!this.HasPosition || this.Position.Equals(chooseOption.Position));
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x0005F5FE File Offset: 0x0005D7FE
		public void Deserialize(Stream stream)
		{
			ChooseOption.Deserialize(stream, this);
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x0005F608 File Offset: 0x0005D808
		public static ChooseOption Deserialize(Stream stream, ChooseOption instance)
		{
			return ChooseOption.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x0005F614 File Offset: 0x0005D814
		public static ChooseOption DeserializeLengthDelimited(Stream stream)
		{
			ChooseOption chooseOption = new ChooseOption();
			ChooseOption.DeserializeLengthDelimited(stream, chooseOption);
			return chooseOption;
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x0005F630 File Offset: 0x0005D830
		public static ChooseOption DeserializeLengthDelimited(Stream stream, ChooseOption instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChooseOption.Deserialize(stream, instance, num);
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x0005F658 File Offset: 0x0005D858
		public static ChooseOption Deserialize(Stream stream, ChooseOption instance, long limit)
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
				else
				{
					if (num <= 16)
					{
						if (num == 8)
						{
							instance.Id = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.Index = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.Target = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.SubOption = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 40)
						{
							instance.Position = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
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

		// Token: 0x06001AFA RID: 6906 RVA: 0x0005F743 File Offset: 0x0005D943
		public void Serialize(Stream stream)
		{
			ChooseOption.Serialize(stream, this);
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x0005F74C File Offset: 0x0005D94C
		public static void Serialize(Stream stream, ChooseOption instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Id));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Index));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Target));
			if (instance.HasSubOption)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SubOption));
			}
			if (instance.HasPosition)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Position));
			}
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x0005F7D4 File Offset: 0x0005D9D4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Id));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Index));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Target));
			if (this.HasSubOption)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SubOption));
			}
			if (this.HasPosition)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Position));
			}
			return num + 3U;
		}

		// Token: 0x040009FA RID: 2554
		public bool HasSubOption;

		// Token: 0x040009FB RID: 2555
		private int _SubOption;

		// Token: 0x040009FC RID: 2556
		public bool HasPosition;

		// Token: 0x040009FD RID: 2557
		private int _Position;

		// Token: 0x02000642 RID: 1602
		public enum PacketID
		{
			// Token: 0x040020F8 RID: 8440
			ID = 2
		}
	}
}
