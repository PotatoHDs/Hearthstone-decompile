using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x02000097 RID: 151
	public class ProfileProgress : IProtoBuf
	{
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x00025A01 File Offset: 0x00023C01
		// (set) Token: 0x06000A2E RID: 2606 RVA: 0x00025A09 File Offset: 0x00023C09
		public long Progress { get; set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x00025A12 File Offset: 0x00023C12
		// (set) Token: 0x06000A30 RID: 2608 RVA: 0x00025A1A File Offset: 0x00023C1A
		public int BestForge { get; set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x00025A23 File Offset: 0x00023C23
		// (set) Token: 0x06000A32 RID: 2610 RVA: 0x00025A2B File Offset: 0x00023C2B
		public Date LastForge
		{
			get
			{
				return this._LastForge;
			}
			set
			{
				this._LastForge = value;
				this.HasLastForge = (value != null);
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00025A40 File Offset: 0x00023C40
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Progress.GetHashCode();
			num ^= this.BestForge.GetHashCode();
			if (this.HasLastForge)
			{
				num ^= this.LastForge.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00025A94 File Offset: 0x00023C94
		public override bool Equals(object obj)
		{
			ProfileProgress profileProgress = obj as ProfileProgress;
			return profileProgress != null && this.Progress.Equals(profileProgress.Progress) && this.BestForge.Equals(profileProgress.BestForge) && this.HasLastForge == profileProgress.HasLastForge && (!this.HasLastForge || this.LastForge.Equals(profileProgress.LastForge));
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00025B09 File Offset: 0x00023D09
		public void Deserialize(Stream stream)
		{
			ProfileProgress.Deserialize(stream, this);
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00025B13 File Offset: 0x00023D13
		public static ProfileProgress Deserialize(Stream stream, ProfileProgress instance)
		{
			return ProfileProgress.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00025B20 File Offset: 0x00023D20
		public static ProfileProgress DeserializeLengthDelimited(Stream stream)
		{
			ProfileProgress profileProgress = new ProfileProgress();
			ProfileProgress.DeserializeLengthDelimited(stream, profileProgress);
			return profileProgress;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00025B3C File Offset: 0x00023D3C
		public static ProfileProgress DeserializeLengthDelimited(Stream stream, ProfileProgress instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileProgress.Deserialize(stream, instance, num);
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x00025B64 File Offset: 0x00023D64
		public static ProfileProgress Deserialize(Stream stream, ProfileProgress instance, long limit)
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
						if (num != 26)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else if (instance.LastForge == null)
						{
							instance.LastForge = Date.DeserializeLengthDelimited(stream);
						}
						else
						{
							Date.DeserializeLengthDelimited(stream, instance.LastForge);
						}
					}
					else
					{
						instance.BestForge = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Progress = (long)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00025C32 File Offset: 0x00023E32
		public void Serialize(Stream stream)
		{
			ProfileProgress.Serialize(stream, this);
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00025C3C File Offset: 0x00023E3C
		public static void Serialize(Stream stream, ProfileProgress instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Progress);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BestForge));
			if (instance.HasLastForge)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.LastForge.GetSerializedSize());
				Date.Serialize(stream, instance.LastForge);
			}
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00025CA0 File Offset: 0x00023EA0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)this.Progress);
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BestForge));
			if (this.HasLastForge)
			{
				num += 1U;
				uint serializedSize = this.LastForge.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num + 2U;
		}

		// Token: 0x04000387 RID: 903
		public bool HasLastForge;

		// Token: 0x04000388 RID: 904
		private Date _LastForge;

		// Token: 0x020005A8 RID: 1448
		public enum PacketID
		{
			// Token: 0x04001F55 RID: 8021
			ID = 233
		}
	}
}
