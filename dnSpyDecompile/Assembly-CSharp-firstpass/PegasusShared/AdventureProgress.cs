using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000143 RID: 323
	public class AdventureProgress : IProtoBuf
	{
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x00048F91 File Offset: 0x00047191
		// (set) Token: 0x06001550 RID: 5456 RVA: 0x00048F99 File Offset: 0x00047199
		public int WingId { get; set; }

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x00048FA2 File Offset: 0x000471A2
		// (set) Token: 0x06001552 RID: 5458 RVA: 0x00048FAA File Offset: 0x000471AA
		public int Progress { get; set; }

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x00048FB3 File Offset: 0x000471B3
		// (set) Token: 0x06001554 RID: 5460 RVA: 0x00048FBB File Offset: 0x000471BB
		public int Ack
		{
			get
			{
				return this._Ack;
			}
			set
			{
				this._Ack = value;
				this.HasAck = true;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001555 RID: 5461 RVA: 0x00048FCB File Offset: 0x000471CB
		// (set) Token: 0x06001556 RID: 5462 RVA: 0x00048FD3 File Offset: 0x000471D3
		public ulong Flags_ { get; set; }

		// Token: 0x06001557 RID: 5463 RVA: 0x00048FDC File Offset: 0x000471DC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.WingId.GetHashCode();
			num ^= this.Progress.GetHashCode();
			if (this.HasAck)
			{
				num ^= this.Ack.GetHashCode();
			}
			return num ^ this.Flags_.GetHashCode();
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x00049044 File Offset: 0x00047244
		public override bool Equals(object obj)
		{
			AdventureProgress adventureProgress = obj as AdventureProgress;
			return adventureProgress != null && this.WingId.Equals(adventureProgress.WingId) && this.Progress.Equals(adventureProgress.Progress) && this.HasAck == adventureProgress.HasAck && (!this.HasAck || this.Ack.Equals(adventureProgress.Ack)) && this.Flags_.Equals(adventureProgress.Flags_);
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x000490D4 File Offset: 0x000472D4
		public void Deserialize(Stream stream)
		{
			AdventureProgress.Deserialize(stream, this);
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x000490DE File Offset: 0x000472DE
		public static AdventureProgress Deserialize(Stream stream, AdventureProgress instance)
		{
			return AdventureProgress.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x000490EC File Offset: 0x000472EC
		public static AdventureProgress DeserializeLengthDelimited(Stream stream)
		{
			AdventureProgress adventureProgress = new AdventureProgress();
			AdventureProgress.DeserializeLengthDelimited(stream, adventureProgress);
			return adventureProgress;
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x00049108 File Offset: 0x00047308
		public static AdventureProgress DeserializeLengthDelimited(Stream stream, AdventureProgress instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AdventureProgress.Deserialize(stream, instance, num);
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x00049130 File Offset: 0x00047330
		public static AdventureProgress Deserialize(Stream stream, AdventureProgress instance, long limit)
		{
			instance.Ack = 0;
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
							instance.WingId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.Progress = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.Ack = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.Flags_ = ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600155E RID: 5470 RVA: 0x0004920A File Offset: 0x0004740A
		public void Serialize(Stream stream)
		{
			AdventureProgress.Serialize(stream, this);
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x00049214 File Offset: 0x00047414
		public static void Serialize(Stream stream, AdventureProgress instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.WingId));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Progress));
			if (instance.HasAck)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Ack));
			}
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, instance.Flags_);
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x0004927C File Offset: 0x0004747C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.WingId));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Progress));
			if (this.HasAck)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Ack));
			}
			num += ProtocolParser.SizeOfUInt64(this.Flags_);
			return num + 3U;
		}

		// Token: 0x0400068C RID: 1676
		public bool HasAck;

		// Token: 0x0400068D RID: 1677
		private int _Ack;

		// Token: 0x02000638 RID: 1592
		public enum Flags
		{
			// Token: 0x040020DB RID: 8411
			OWNED = 1,
			// Token: 0x040020DC RID: 8412
			DEFEAT_HEROIC_MISSION_1,
			// Token: 0x040020DD RID: 8413
			DEFEAT_HEROIC_MISSION_2 = 4,
			// Token: 0x040020DE RID: 8414
			DEFEAT_HEROIC_MISSION_3 = 8,
			// Token: 0x040020DF RID: 8415
			DEFEAT_HEROIC_MISSION_4 = 16,
			// Token: 0x040020E0 RID: 8416
			DEFEAT_CLASS_CHALLENGE_MISSION_1 = 256,
			// Token: 0x040020E1 RID: 8417
			DEFEAT_CLASS_CHALLENGE_MISSION_2 = 512,
			// Token: 0x040020E2 RID: 8418
			DEFEAT_CLASS_CHALLENGE_MISSION_3 = 1024
		}
	}
}
