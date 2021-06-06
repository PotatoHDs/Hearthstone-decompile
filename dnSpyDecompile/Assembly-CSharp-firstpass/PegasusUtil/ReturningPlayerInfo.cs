using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000DA RID: 218
	public class ReturningPlayerInfo : IProtoBuf
	{
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x0003646B File Offset: 0x0003466B
		// (set) Token: 0x06000EDD RID: 3805 RVA: 0x00036473 File Offset: 0x00034673
		public ReturningPlayerStatus Status { get; set; }

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000EDE RID: 3806 RVA: 0x0003647C File Offset: 0x0003467C
		// (set) Token: 0x06000EDF RID: 3807 RVA: 0x00036484 File Offset: 0x00034684
		public uint AbTestGroup
		{
			get
			{
				return this._AbTestGroup;
			}
			set
			{
				this._AbTestGroup = value;
				this.HasAbTestGroup = true;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x00036494 File Offset: 0x00034694
		// (set) Token: 0x06000EE1 RID: 3809 RVA: 0x0003649C File Offset: 0x0003469C
		public long NotificationSuppressionTimeDays { get; set; }

		// Token: 0x06000EE2 RID: 3810 RVA: 0x000364A8 File Offset: 0x000346A8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Status.GetHashCode();
			if (this.HasAbTestGroup)
			{
				num ^= this.AbTestGroup.GetHashCode();
			}
			return num ^ this.NotificationSuppressionTimeDays.GetHashCode();
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00036504 File Offset: 0x00034704
		public override bool Equals(object obj)
		{
			ReturningPlayerInfo returningPlayerInfo = obj as ReturningPlayerInfo;
			return returningPlayerInfo != null && this.Status.Equals(returningPlayerInfo.Status) && this.HasAbTestGroup == returningPlayerInfo.HasAbTestGroup && (!this.HasAbTestGroup || this.AbTestGroup.Equals(returningPlayerInfo.AbTestGroup)) && this.NotificationSuppressionTimeDays.Equals(returningPlayerInfo.NotificationSuppressionTimeDays);
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00036587 File Offset: 0x00034787
		public void Deserialize(Stream stream)
		{
			ReturningPlayerInfo.Deserialize(stream, this);
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00036591 File Offset: 0x00034791
		public static ReturningPlayerInfo Deserialize(Stream stream, ReturningPlayerInfo instance)
		{
			return ReturningPlayerInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0003659C File Offset: 0x0003479C
		public static ReturningPlayerInfo DeserializeLengthDelimited(Stream stream)
		{
			ReturningPlayerInfo returningPlayerInfo = new ReturningPlayerInfo();
			ReturningPlayerInfo.DeserializeLengthDelimited(stream, returningPlayerInfo);
			return returningPlayerInfo;
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x000365B8 File Offset: 0x000347B8
		public static ReturningPlayerInfo DeserializeLengthDelimited(Stream stream, ReturningPlayerInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ReturningPlayerInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x000365E0 File Offset: 0x000347E0
		public static ReturningPlayerInfo Deserialize(Stream stream, ReturningPlayerInfo instance, long limit)
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
							instance.NotificationSuppressionTimeDays = (long)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.AbTestGroup = ProtocolParser.ReadUInt32(stream);
					}
				}
				else
				{
					instance.Status = (ReturningPlayerStatus)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0003668E File Offset: 0x0003488E
		public void Serialize(Stream stream)
		{
			ReturningPlayerInfo.Serialize(stream, this);
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x00036698 File Offset: 0x00034898
		public static void Serialize(Stream stream, ReturningPlayerInfo instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Status));
			if (instance.HasAbTestGroup)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.AbTestGroup);
			}
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.NotificationSuppressionTimeDays);
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x000366EC File Offset: 0x000348EC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Status));
			if (this.HasAbTestGroup)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.AbTestGroup);
			}
			num += ProtocolParser.SizeOfUInt64((ulong)this.NotificationSuppressionTimeDays);
			return num + 2U;
		}

		// Token: 0x040004EB RID: 1259
		public bool HasAbTestGroup;

		// Token: 0x040004EC RID: 1260
		private uint _AbTestGroup;
	}
}
