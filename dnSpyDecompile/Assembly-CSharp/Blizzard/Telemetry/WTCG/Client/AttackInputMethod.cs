using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x02001192 RID: 4498
	public class AttackInputMethod : IProtoBuf
	{
		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x0600C62E RID: 50734 RVA: 0x003B9E6A File Offset: 0x003B806A
		// (set) Token: 0x0600C62F RID: 50735 RVA: 0x003B9E72 File Offset: 0x003B8072
		public DeviceInfo DeviceInfo
		{
			get
			{
				return this._DeviceInfo;
			}
			set
			{
				this._DeviceInfo = value;
				this.HasDeviceInfo = (value != null);
			}
		}

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x0600C630 RID: 50736 RVA: 0x003B9E85 File Offset: 0x003B8085
		// (set) Token: 0x0600C631 RID: 50737 RVA: 0x003B9E8D File Offset: 0x003B808D
		public long TotalNumAttacks
		{
			get
			{
				return this._TotalNumAttacks;
			}
			set
			{
				this._TotalNumAttacks = value;
				this.HasTotalNumAttacks = true;
			}
		}

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x0600C632 RID: 50738 RVA: 0x003B9E9D File Offset: 0x003B809D
		// (set) Token: 0x0600C633 RID: 50739 RVA: 0x003B9EA5 File Offset: 0x003B80A5
		public long TotalClickAttacks
		{
			get
			{
				return this._TotalClickAttacks;
			}
			set
			{
				this._TotalClickAttacks = value;
				this.HasTotalClickAttacks = true;
			}
		}

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x0600C634 RID: 50740 RVA: 0x003B9EB5 File Offset: 0x003B80B5
		// (set) Token: 0x0600C635 RID: 50741 RVA: 0x003B9EBD File Offset: 0x003B80BD
		public int PercentClickAttacks
		{
			get
			{
				return this._PercentClickAttacks;
			}
			set
			{
				this._PercentClickAttacks = value;
				this.HasPercentClickAttacks = true;
			}
		}

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x0600C636 RID: 50742 RVA: 0x003B9ECD File Offset: 0x003B80CD
		// (set) Token: 0x0600C637 RID: 50743 RVA: 0x003B9ED5 File Offset: 0x003B80D5
		public long TotalDragAttacks
		{
			get
			{
				return this._TotalDragAttacks;
			}
			set
			{
				this._TotalDragAttacks = value;
				this.HasTotalDragAttacks = true;
			}
		}

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x0600C638 RID: 50744 RVA: 0x003B9EE5 File Offset: 0x003B80E5
		// (set) Token: 0x0600C639 RID: 50745 RVA: 0x003B9EED File Offset: 0x003B80ED
		public int PercentDragAttacks
		{
			get
			{
				return this._PercentDragAttacks;
			}
			set
			{
				this._PercentDragAttacks = value;
				this.HasPercentDragAttacks = true;
			}
		}

		// Token: 0x0600C63A RID: 50746 RVA: 0x003B9F00 File Offset: 0x003B8100
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasTotalNumAttacks)
			{
				num ^= this.TotalNumAttacks.GetHashCode();
			}
			if (this.HasTotalClickAttacks)
			{
				num ^= this.TotalClickAttacks.GetHashCode();
			}
			if (this.HasPercentClickAttacks)
			{
				num ^= this.PercentClickAttacks.GetHashCode();
			}
			if (this.HasTotalDragAttacks)
			{
				num ^= this.TotalDragAttacks.GetHashCode();
			}
			if (this.HasPercentDragAttacks)
			{
				num ^= this.PercentDragAttacks.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C63B RID: 50747 RVA: 0x003B9FB0 File Offset: 0x003B81B0
		public override bool Equals(object obj)
		{
			AttackInputMethod attackInputMethod = obj as AttackInputMethod;
			return attackInputMethod != null && this.HasDeviceInfo == attackInputMethod.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(attackInputMethod.DeviceInfo)) && this.HasTotalNumAttacks == attackInputMethod.HasTotalNumAttacks && (!this.HasTotalNumAttacks || this.TotalNumAttacks.Equals(attackInputMethod.TotalNumAttacks)) && this.HasTotalClickAttacks == attackInputMethod.HasTotalClickAttacks && (!this.HasTotalClickAttacks || this.TotalClickAttacks.Equals(attackInputMethod.TotalClickAttacks)) && this.HasPercentClickAttacks == attackInputMethod.HasPercentClickAttacks && (!this.HasPercentClickAttacks || this.PercentClickAttacks.Equals(attackInputMethod.PercentClickAttacks)) && this.HasTotalDragAttacks == attackInputMethod.HasTotalDragAttacks && (!this.HasTotalDragAttacks || this.TotalDragAttacks.Equals(attackInputMethod.TotalDragAttacks)) && this.HasPercentDragAttacks == attackInputMethod.HasPercentDragAttacks && (!this.HasPercentDragAttacks || this.PercentDragAttacks.Equals(attackInputMethod.PercentDragAttacks));
		}

		// Token: 0x0600C63C RID: 50748 RVA: 0x003BA0DB File Offset: 0x003B82DB
		public void Deserialize(Stream stream)
		{
			AttackInputMethod.Deserialize(stream, this);
		}

		// Token: 0x0600C63D RID: 50749 RVA: 0x003BA0E5 File Offset: 0x003B82E5
		public static AttackInputMethod Deserialize(Stream stream, AttackInputMethod instance)
		{
			return AttackInputMethod.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C63E RID: 50750 RVA: 0x003BA0F0 File Offset: 0x003B82F0
		public static AttackInputMethod DeserializeLengthDelimited(Stream stream)
		{
			AttackInputMethod attackInputMethod = new AttackInputMethod();
			AttackInputMethod.DeserializeLengthDelimited(stream, attackInputMethod);
			return attackInputMethod;
		}

		// Token: 0x0600C63F RID: 50751 RVA: 0x003BA10C File Offset: 0x003B830C
		public static AttackInputMethod DeserializeLengthDelimited(Stream stream, AttackInputMethod instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AttackInputMethod.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C640 RID: 50752 RVA: 0x003BA134 File Offset: 0x003B8334
		public static AttackInputMethod Deserialize(Stream stream, AttackInputMethod instance, long limit)
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
						if (num != 10)
						{
							if (num == 16)
							{
								instance.TotalNumAttacks = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 24)
							{
								instance.TotalClickAttacks = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (instance.DeviceInfo == null)
							{
								instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
								continue;
							}
							DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
							continue;
						}
					}
					else
					{
						if (num == 32)
						{
							instance.PercentClickAttacks = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 40)
						{
							instance.TotalDragAttacks = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 48)
						{
							instance.PercentDragAttacks = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600C641 RID: 50753 RVA: 0x003BA259 File Offset: 0x003B8459
		public void Serialize(Stream stream)
		{
			AttackInputMethod.Serialize(stream, this);
		}

		// Token: 0x0600C642 RID: 50754 RVA: 0x003BA264 File Offset: 0x003B8464
		public static void Serialize(Stream stream, AttackInputMethod instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasTotalNumAttacks)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.TotalNumAttacks);
			}
			if (instance.HasTotalClickAttacks)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.TotalClickAttacks);
			}
			if (instance.HasPercentClickAttacks)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PercentClickAttacks));
			}
			if (instance.HasTotalDragAttacks)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.TotalDragAttacks);
			}
			if (instance.HasPercentDragAttacks)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PercentDragAttacks));
			}
		}

		// Token: 0x0600C643 RID: 50755 RVA: 0x003BA32C File Offset: 0x003B852C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasTotalNumAttacks)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.TotalNumAttacks);
			}
			if (this.HasTotalClickAttacks)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.TotalClickAttacks);
			}
			if (this.HasPercentClickAttacks)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.PercentClickAttacks));
			}
			if (this.HasTotalDragAttacks)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.TotalDragAttacks);
			}
			if (this.HasPercentDragAttacks)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.PercentDragAttacks));
			}
			return num;
		}

		// Token: 0x04009DE3 RID: 40419
		public bool HasDeviceInfo;

		// Token: 0x04009DE4 RID: 40420
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009DE5 RID: 40421
		public bool HasTotalNumAttacks;

		// Token: 0x04009DE6 RID: 40422
		private long _TotalNumAttacks;

		// Token: 0x04009DE7 RID: 40423
		public bool HasTotalClickAttacks;

		// Token: 0x04009DE8 RID: 40424
		private long _TotalClickAttacks;

		// Token: 0x04009DE9 RID: 40425
		public bool HasPercentClickAttacks;

		// Token: 0x04009DEA RID: 40426
		private int _PercentClickAttacks;

		// Token: 0x04009DEB RID: 40427
		public bool HasTotalDragAttacks;

		// Token: 0x04009DEC RID: 40428
		private long _TotalDragAttacks;

		// Token: 0x04009DED RID: 40429
		public bool HasPercentDragAttacks;

		// Token: 0x04009DEE RID: 40430
		private int _PercentDragAttacks;
	}
}
