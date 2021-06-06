using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001A5 RID: 421
	public class PendingEvent : IProtoBuf
	{
		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001A6A RID: 6762 RVA: 0x0005D35E File Offset: 0x0005B55E
		// (set) Token: 0x06001A6B RID: 6763 RVA: 0x0005D366 File Offset: 0x0005B566
		public int Type { get; set; }

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001A6C RID: 6764 RVA: 0x0005D36F File Offset: 0x0005B56F
		// (set) Token: 0x06001A6D RID: 6765 RVA: 0x0005D377 File Offset: 0x0005B577
		public int Command { get; set; }

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001A6E RID: 6766 RVA: 0x0005D380 File Offset: 0x0005B580
		// (set) Token: 0x06001A6F RID: 6767 RVA: 0x0005D388 File Offset: 0x0005B588
		public uint ObserverId { get; set; }

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001A70 RID: 6768 RVA: 0x0005D391 File Offset: 0x0005B591
		// (set) Token: 0x06001A71 RID: 6769 RVA: 0x0005D399 File Offset: 0x0005B599
		public int PlayerId { get; set; }

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001A72 RID: 6770 RVA: 0x0005D3A2 File Offset: 0x0005B5A2
		// (set) Token: 0x06001A73 RID: 6771 RVA: 0x0005D3AA File Offset: 0x0005B5AA
		public int DataType { get; set; }

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001A74 RID: 6772 RVA: 0x0005D3B3 File Offset: 0x0005B5B3
		// (set) Token: 0x06001A75 RID: 6773 RVA: 0x0005D3BB File Offset: 0x0005B5BB
		public byte[] Data
		{
			get
			{
				return this._Data;
			}
			set
			{
				this._Data = value;
				this.HasData = (value != null);
			}
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x0005D3D0 File Offset: 0x0005B5D0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Type.GetHashCode();
			num ^= this.Command.GetHashCode();
			num ^= this.ObserverId.GetHashCode();
			num ^= this.PlayerId.GetHashCode();
			num ^= this.DataType.GetHashCode();
			if (this.HasData)
			{
				num ^= this.Data.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x0005D458 File Offset: 0x0005B658
		public override bool Equals(object obj)
		{
			PendingEvent pendingEvent = obj as PendingEvent;
			return pendingEvent != null && this.Type.Equals(pendingEvent.Type) && this.Command.Equals(pendingEvent.Command) && this.ObserverId.Equals(pendingEvent.ObserverId) && this.PlayerId.Equals(pendingEvent.PlayerId) && this.DataType.Equals(pendingEvent.DataType) && this.HasData == pendingEvent.HasData && (!this.HasData || this.Data.Equals(pendingEvent.Data));
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x0005D515 File Offset: 0x0005B715
		public void Deserialize(Stream stream)
		{
			PendingEvent.Deserialize(stream, this);
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x0005D51F File Offset: 0x0005B71F
		public static PendingEvent Deserialize(Stream stream, PendingEvent instance)
		{
			return PendingEvent.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x0005D52C File Offset: 0x0005B72C
		public static PendingEvent DeserializeLengthDelimited(Stream stream)
		{
			PendingEvent pendingEvent = new PendingEvent();
			PendingEvent.DeserializeLengthDelimited(stream, pendingEvent);
			return pendingEvent;
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x0005D548 File Offset: 0x0005B748
		public static PendingEvent DeserializeLengthDelimited(Stream stream, PendingEvent instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PendingEvent.Deserialize(stream, instance, num);
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x0005D570 File Offset: 0x0005B770
		public static PendingEvent Deserialize(Stream stream, PendingEvent instance, long limit)
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
					if (num <= 24)
					{
						if (num == 8)
						{
							instance.Type = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.Command = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 24)
						{
							instance.ObserverId = ProtocolParser.ReadUInt32(stream);
							continue;
						}
					}
					else
					{
						if (num == 32)
						{
							instance.PlayerId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 40)
						{
							instance.DataType = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 50)
						{
							instance.Data = ProtocolParser.ReadBytes(stream);
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

		// Token: 0x06001A7D RID: 6781 RVA: 0x0005D673 File Offset: 0x0005B873
		public void Serialize(Stream stream)
		{
			PendingEvent.Serialize(stream, this);
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x0005D67C File Offset: 0x0005B87C
		public static void Serialize(Stream stream, PendingEvent instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Type));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Command));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt32(stream, instance.ObserverId);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PlayerId));
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DataType));
			if (instance.HasData)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, instance.Data);
			}
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x0005D70C File Offset: 0x0005B90C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Type));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Command));
			num += ProtocolParser.SizeOfUInt32(this.ObserverId);
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.PlayerId));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DataType));
			if (this.HasData)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Data.Length) + (uint)this.Data.Length;
			}
			return num + 5U;
		}

		// Token: 0x040009D2 RID: 2514
		public bool HasData;

		// Token: 0x040009D3 RID: 2515
		private byte[] _Data;
	}
}
