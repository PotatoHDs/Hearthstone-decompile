using System;
using System.IO;

namespace PegasusClient
{
	// Token: 0x02000028 RID: 40
	public class FiresideGatheringInfo : IProtoBuf
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00008C19 File Offset: 0x00006E19
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x00008C21 File Offset: 0x00006E21
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

		// Token: 0x060001F9 RID: 505 RVA: 0x00008C34 File Offset: 0x00006E34
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasFsgId)
			{
				num ^= this.FsgId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00008C68 File Offset: 0x00006E68
		public override bool Equals(object obj)
		{
			FiresideGatheringInfo firesideGatheringInfo = obj as FiresideGatheringInfo;
			return firesideGatheringInfo != null && this.HasFsgId == firesideGatheringInfo.HasFsgId && (!this.HasFsgId || this.FsgId.Equals(firesideGatheringInfo.FsgId));
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00008CB0 File Offset: 0x00006EB0
		public void Deserialize(Stream stream)
		{
			FiresideGatheringInfo.Deserialize(stream, this);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00008CBA File Offset: 0x00006EBA
		public static FiresideGatheringInfo Deserialize(Stream stream, FiresideGatheringInfo instance)
		{
			return FiresideGatheringInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00008CC8 File Offset: 0x00006EC8
		public static FiresideGatheringInfo DeserializeLengthDelimited(Stream stream)
		{
			FiresideGatheringInfo firesideGatheringInfo = new FiresideGatheringInfo();
			FiresideGatheringInfo.DeserializeLengthDelimited(stream, firesideGatheringInfo);
			return firesideGatheringInfo;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00008CE4 File Offset: 0x00006EE4
		public static FiresideGatheringInfo DeserializeLengthDelimited(Stream stream, FiresideGatheringInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FiresideGatheringInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00008D0C File Offset: 0x00006F0C
		public static FiresideGatheringInfo Deserialize(Stream stream, FiresideGatheringInfo instance, long limit)
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
				else if (num == 8)
				{
					instance.FsgId = (long)ProtocolParser.ReadUInt64(stream);
				}
				else
				{
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

		// Token: 0x06000200 RID: 512 RVA: 0x00008D8B File Offset: 0x00006F8B
		public void Serialize(Stream stream)
		{
			FiresideGatheringInfo.Serialize(stream, this);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00008D94 File Offset: 0x00006F94
		public static void Serialize(Stream stream, FiresideGatheringInfo instance)
		{
			if (instance.HasFsgId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FsgId);
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00008DB4 File Offset: 0x00006FB4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasFsgId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.FsgId);
			}
			return num;
		}

		// Token: 0x04000084 RID: 132
		public bool HasFsgId;

		// Token: 0x04000085 RID: 133
		private long _FsgId;
	}
}
