using System;
using System.IO;
using PegasusShared;

namespace PegasusGame
{
	// Token: 0x020001D1 RID: 465
	public class SpectatorRemoved : IProtoBuf
	{
		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001DAA RID: 7594 RVA: 0x00068597 File Offset: 0x00066797
		// (set) Token: 0x06001DAB RID: 7595 RVA: 0x0006859F File Offset: 0x0006679F
		public int ReasonCode { get; set; }

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001DAC RID: 7596 RVA: 0x000685A8 File Offset: 0x000667A8
		// (set) Token: 0x06001DAD RID: 7597 RVA: 0x000685B0 File Offset: 0x000667B0
		public BnetId RemovedBy
		{
			get
			{
				return this._RemovedBy;
			}
			set
			{
				this._RemovedBy = value;
				this.HasRemovedBy = (value != null);
			}
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x000685C4 File Offset: 0x000667C4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ReasonCode.GetHashCode();
			if (this.HasRemovedBy)
			{
				num ^= this.RemovedBy.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x00068608 File Offset: 0x00066808
		public override bool Equals(object obj)
		{
			SpectatorRemoved spectatorRemoved = obj as SpectatorRemoved;
			return spectatorRemoved != null && this.ReasonCode.Equals(spectatorRemoved.ReasonCode) && this.HasRemovedBy == spectatorRemoved.HasRemovedBy && (!this.HasRemovedBy || this.RemovedBy.Equals(spectatorRemoved.RemovedBy));
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x00068665 File Offset: 0x00066865
		public void Deserialize(Stream stream)
		{
			SpectatorRemoved.Deserialize(stream, this);
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x0006866F File Offset: 0x0006686F
		public static SpectatorRemoved Deserialize(Stream stream, SpectatorRemoved instance)
		{
			return SpectatorRemoved.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x0006867C File Offset: 0x0006687C
		public static SpectatorRemoved DeserializeLengthDelimited(Stream stream)
		{
			SpectatorRemoved spectatorRemoved = new SpectatorRemoved();
			SpectatorRemoved.DeserializeLengthDelimited(stream, spectatorRemoved);
			return spectatorRemoved;
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x00068698 File Offset: 0x00066898
		public static SpectatorRemoved DeserializeLengthDelimited(Stream stream, SpectatorRemoved instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SpectatorRemoved.Deserialize(stream, instance, num);
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x000686C0 File Offset: 0x000668C0
		public static SpectatorRemoved Deserialize(Stream stream, SpectatorRemoved instance, long limit)
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
					if (num != 18)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.RemovedBy == null)
					{
						instance.RemovedBy = BnetId.DeserializeLengthDelimited(stream);
					}
					else
					{
						BnetId.DeserializeLengthDelimited(stream, instance.RemovedBy);
					}
				}
				else
				{
					instance.ReasonCode = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x00068772 File Offset: 0x00066972
		public void Serialize(Stream stream)
		{
			SpectatorRemoved.Serialize(stream, this);
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x0006877C File Offset: 0x0006697C
		public static void Serialize(Stream stream, SpectatorRemoved instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ReasonCode));
			if (instance.HasRemovedBy)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.RemovedBy.GetSerializedSize());
				BnetId.Serialize(stream, instance.RemovedBy);
			}
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x000687CC File Offset: 0x000669CC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ReasonCode));
			if (this.HasRemovedBy)
			{
				num += 1U;
				uint serializedSize = this.RemovedBy.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num + 1U;
		}

		// Token: 0x04000ABC RID: 2748
		public bool HasRemovedBy;

		// Token: 0x04000ABD RID: 2749
		private BnetId _RemovedBy;

		// Token: 0x0200065C RID: 1628
		public enum SpectatorRemovedReason
		{
			// Token: 0x04002151 RID: 8529
			SPECTATOR_REMOVED_REASON_KICKED,
			// Token: 0x04002152 RID: 8530
			SPECTATOR_REMOVED_REASON_GAMEOVER
		}
	}
}
