using System;
using System.IO;
using System.Text;

namespace PegasusGame
{
	// Token: 0x020001C7 RID: 455
	public class PowerHistoryVoTask : IProtoBuf
	{
		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x00065EEA File Offset: 0x000640EA
		// (set) Token: 0x06001CF1 RID: 7409 RVA: 0x00065EF2 File Offset: 0x000640F2
		public string SpellPrefabGuid
		{
			get
			{
				return this._SpellPrefabGuid;
			}
			set
			{
				this._SpellPrefabGuid = value;
				this.HasSpellPrefabGuid = (value != null);
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001CF2 RID: 7410 RVA: 0x00065F05 File Offset: 0x00064105
		// (set) Token: 0x06001CF3 RID: 7411 RVA: 0x00065F0D File Offset: 0x0006410D
		public bool Blocking
		{
			get
			{
				return this._Blocking;
			}
			set
			{
				this._Blocking = value;
				this.HasBlocking = true;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001CF4 RID: 7412 RVA: 0x00065F1D File Offset: 0x0006411D
		// (set) Token: 0x06001CF5 RID: 7413 RVA: 0x00065F25 File Offset: 0x00064125
		public int AdditionalDelayMs
		{
			get
			{
				return this._AdditionalDelayMs;
			}
			set
			{
				this._AdditionalDelayMs = value;
				this.HasAdditionalDelayMs = true;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001CF6 RID: 7414 RVA: 0x00065F35 File Offset: 0x00064135
		// (set) Token: 0x06001CF7 RID: 7415 RVA: 0x00065F3D File Offset: 0x0006413D
		public int SpeakingEntity
		{
			get
			{
				return this._SpeakingEntity;
			}
			set
			{
				this._SpeakingEntity = value;
				this.HasSpeakingEntity = true;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001CF8 RID: 7416 RVA: 0x00065F4D File Offset: 0x0006414D
		// (set) Token: 0x06001CF9 RID: 7417 RVA: 0x00065F55 File Offset: 0x00064155
		public string BrassRingPrefabGuid
		{
			get
			{
				return this._BrassRingPrefabGuid;
			}
			set
			{
				this._BrassRingPrefabGuid = value;
				this.HasBrassRingPrefabGuid = (value != null);
			}
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x00065F68 File Offset: 0x00064168
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasSpellPrefabGuid)
			{
				num ^= this.SpellPrefabGuid.GetHashCode();
			}
			if (this.HasBlocking)
			{
				num ^= this.Blocking.GetHashCode();
			}
			if (this.HasAdditionalDelayMs)
			{
				num ^= this.AdditionalDelayMs.GetHashCode();
			}
			if (this.HasSpeakingEntity)
			{
				num ^= this.SpeakingEntity.GetHashCode();
			}
			if (this.HasBrassRingPrefabGuid)
			{
				num ^= this.BrassRingPrefabGuid.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x00065FFC File Offset: 0x000641FC
		public override bool Equals(object obj)
		{
			PowerHistoryVoTask powerHistoryVoTask = obj as PowerHistoryVoTask;
			return powerHistoryVoTask != null && this.HasSpellPrefabGuid == powerHistoryVoTask.HasSpellPrefabGuid && (!this.HasSpellPrefabGuid || this.SpellPrefabGuid.Equals(powerHistoryVoTask.SpellPrefabGuid)) && this.HasBlocking == powerHistoryVoTask.HasBlocking && (!this.HasBlocking || this.Blocking.Equals(powerHistoryVoTask.Blocking)) && this.HasAdditionalDelayMs == powerHistoryVoTask.HasAdditionalDelayMs && (!this.HasAdditionalDelayMs || this.AdditionalDelayMs.Equals(powerHistoryVoTask.AdditionalDelayMs)) && this.HasSpeakingEntity == powerHistoryVoTask.HasSpeakingEntity && (!this.HasSpeakingEntity || this.SpeakingEntity.Equals(powerHistoryVoTask.SpeakingEntity)) && this.HasBrassRingPrefabGuid == powerHistoryVoTask.HasBrassRingPrefabGuid && (!this.HasBrassRingPrefabGuid || this.BrassRingPrefabGuid.Equals(powerHistoryVoTask.BrassRingPrefabGuid));
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x000660F6 File Offset: 0x000642F6
		public void Deserialize(Stream stream)
		{
			PowerHistoryVoTask.Deserialize(stream, this);
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x00066100 File Offset: 0x00064300
		public static PowerHistoryVoTask Deserialize(Stream stream, PowerHistoryVoTask instance)
		{
			return PowerHistoryVoTask.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x0006610C File Offset: 0x0006430C
		public static PowerHistoryVoTask DeserializeLengthDelimited(Stream stream)
		{
			PowerHistoryVoTask powerHistoryVoTask = new PowerHistoryVoTask();
			PowerHistoryVoTask.DeserializeLengthDelimited(stream, powerHistoryVoTask);
			return powerHistoryVoTask;
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x00066128 File Offset: 0x00064328
		public static PowerHistoryVoTask DeserializeLengthDelimited(Stream stream, PowerHistoryVoTask instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PowerHistoryVoTask.Deserialize(stream, instance, num);
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x00066150 File Offset: 0x00064350
		public static PowerHistoryVoTask Deserialize(Stream stream, PowerHistoryVoTask instance, long limit)
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
						if (num == 10)
						{
							instance.SpellPrefabGuid = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 16)
						{
							instance.Blocking = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.AdditionalDelayMs = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.SpeakingEntity = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 42)
						{
							instance.BrassRingPrefabGuid = ProtocolParser.ReadString(stream);
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

		// Token: 0x06001D01 RID: 7425 RVA: 0x00066239 File Offset: 0x00064439
		public void Serialize(Stream stream)
		{
			PowerHistoryVoTask.Serialize(stream, this);
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x00066244 File Offset: 0x00064444
		public static void Serialize(Stream stream, PowerHistoryVoTask instance)
		{
			if (instance.HasSpellPrefabGuid)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SpellPrefabGuid));
			}
			if (instance.HasBlocking)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Blocking);
			}
			if (instance.HasAdditionalDelayMs)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.AdditionalDelayMs));
			}
			if (instance.HasSpeakingEntity)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SpeakingEntity));
			}
			if (instance.HasBrassRingPrefabGuid)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BrassRingPrefabGuid));
			}
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x000662F4 File Offset: 0x000644F4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasSpellPrefabGuid)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.SpellPrefabGuid);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasBlocking)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasAdditionalDelayMs)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.AdditionalDelayMs));
			}
			if (this.HasSpeakingEntity)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SpeakingEntity));
			}
			if (this.HasBrassRingPrefabGuid)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.BrassRingPrefabGuid);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x04000A80 RID: 2688
		public bool HasSpellPrefabGuid;

		// Token: 0x04000A81 RID: 2689
		private string _SpellPrefabGuid;

		// Token: 0x04000A82 RID: 2690
		public bool HasBlocking;

		// Token: 0x04000A83 RID: 2691
		private bool _Blocking;

		// Token: 0x04000A84 RID: 2692
		public bool HasAdditionalDelayMs;

		// Token: 0x04000A85 RID: 2693
		private int _AdditionalDelayMs;

		// Token: 0x04000A86 RID: 2694
		public bool HasSpeakingEntity;

		// Token: 0x04000A87 RID: 2695
		private int _SpeakingEntity;

		// Token: 0x04000A88 RID: 2696
		public bool HasBrassRingPrefabGuid;

		// Token: 0x04000A89 RID: 2697
		private string _BrassRingPrefabGuid;
	}
}
