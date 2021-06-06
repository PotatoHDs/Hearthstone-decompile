using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011C0 RID: 4544
	public class FlowPerformance : IProtoBuf
	{
		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x0600C9B2 RID: 51634 RVA: 0x003C7078 File Offset: 0x003C5278
		// (set) Token: 0x0600C9B3 RID: 51635 RVA: 0x003C7080 File Offset: 0x003C5280
		public string UniqueId
		{
			get
			{
				return this._UniqueId;
			}
			set
			{
				this._UniqueId = value;
				this.HasUniqueId = (value != null);
			}
		}

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x0600C9B4 RID: 51636 RVA: 0x003C7093 File Offset: 0x003C5293
		// (set) Token: 0x0600C9B5 RID: 51637 RVA: 0x003C709B File Offset: 0x003C529B
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

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x0600C9B6 RID: 51638 RVA: 0x003C70AE File Offset: 0x003C52AE
		// (set) Token: 0x0600C9B7 RID: 51639 RVA: 0x003C70B6 File Offset: 0x003C52B6
		public Player Player
		{
			get
			{
				return this._Player;
			}
			set
			{
				this._Player = value;
				this.HasPlayer = (value != null);
			}
		}

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x0600C9B8 RID: 51640 RVA: 0x003C70C9 File Offset: 0x003C52C9
		// (set) Token: 0x0600C9B9 RID: 51641 RVA: 0x003C70D1 File Offset: 0x003C52D1
		public FlowPerformance.FlowType FlowType_
		{
			get
			{
				return this._FlowType_;
			}
			set
			{
				this._FlowType_ = value;
				this.HasFlowType_ = true;
			}
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x0600C9BA RID: 51642 RVA: 0x003C70E1 File Offset: 0x003C52E1
		// (set) Token: 0x0600C9BB RID: 51643 RVA: 0x003C70E9 File Offset: 0x003C52E9
		public float AverageFps
		{
			get
			{
				return this._AverageFps;
			}
			set
			{
				this._AverageFps = value;
				this.HasAverageFps = true;
			}
		}

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x0600C9BC RID: 51644 RVA: 0x003C70F9 File Offset: 0x003C52F9
		// (set) Token: 0x0600C9BD RID: 51645 RVA: 0x003C7101 File Offset: 0x003C5301
		public float Duration
		{
			get
			{
				return this._Duration;
			}
			set
			{
				this._Duration = value;
				this.HasDuration = true;
			}
		}

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x0600C9BE RID: 51646 RVA: 0x003C7111 File Offset: 0x003C5311
		// (set) Token: 0x0600C9BF RID: 51647 RVA: 0x003C7119 File Offset: 0x003C5319
		public float FpsWarningsThreshold
		{
			get
			{
				return this._FpsWarningsThreshold;
			}
			set
			{
				this._FpsWarningsThreshold = value;
				this.HasFpsWarningsThreshold = true;
			}
		}

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x0600C9C0 RID: 51648 RVA: 0x003C7129 File Offset: 0x003C5329
		// (set) Token: 0x0600C9C1 RID: 51649 RVA: 0x003C7131 File Offset: 0x003C5331
		public int FpsWarningsTotalOccurences
		{
			get
			{
				return this._FpsWarningsTotalOccurences;
			}
			set
			{
				this._FpsWarningsTotalOccurences = value;
				this.HasFpsWarningsTotalOccurences = true;
			}
		}

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x0600C9C2 RID: 51650 RVA: 0x003C7141 File Offset: 0x003C5341
		// (set) Token: 0x0600C9C3 RID: 51651 RVA: 0x003C7149 File Offset: 0x003C5349
		public float FpsWarningsTotalTime
		{
			get
			{
				return this._FpsWarningsTotalTime;
			}
			set
			{
				this._FpsWarningsTotalTime = value;
				this.HasFpsWarningsTotalTime = true;
			}
		}

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x0600C9C4 RID: 51652 RVA: 0x003C7159 File Offset: 0x003C5359
		// (set) Token: 0x0600C9C5 RID: 51653 RVA: 0x003C7161 File Offset: 0x003C5361
		public float FpsWarningsAverageTime
		{
			get
			{
				return this._FpsWarningsAverageTime;
			}
			set
			{
				this._FpsWarningsAverageTime = value;
				this.HasFpsWarningsAverageTime = true;
			}
		}

		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x0600C9C6 RID: 51654 RVA: 0x003C7171 File Offset: 0x003C5371
		// (set) Token: 0x0600C9C7 RID: 51655 RVA: 0x003C7179 File Offset: 0x003C5379
		public float FpsWarningsMaxTime
		{
			get
			{
				return this._FpsWarningsMaxTime;
			}
			set
			{
				this._FpsWarningsMaxTime = value;
				this.HasFpsWarningsMaxTime = true;
			}
		}

		// Token: 0x0600C9C8 RID: 51656 RVA: 0x003C718C File Offset: 0x003C538C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasUniqueId)
			{
				num ^= this.UniqueId.GetHashCode();
			}
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasPlayer)
			{
				num ^= this.Player.GetHashCode();
			}
			if (this.HasFlowType_)
			{
				num ^= this.FlowType_.GetHashCode();
			}
			if (this.HasAverageFps)
			{
				num ^= this.AverageFps.GetHashCode();
			}
			if (this.HasDuration)
			{
				num ^= this.Duration.GetHashCode();
			}
			if (this.HasFpsWarningsThreshold)
			{
				num ^= this.FpsWarningsThreshold.GetHashCode();
			}
			if (this.HasFpsWarningsTotalOccurences)
			{
				num ^= this.FpsWarningsTotalOccurences.GetHashCode();
			}
			if (this.HasFpsWarningsTotalTime)
			{
				num ^= this.FpsWarningsTotalTime.GetHashCode();
			}
			if (this.HasFpsWarningsAverageTime)
			{
				num ^= this.FpsWarningsAverageTime.GetHashCode();
			}
			if (this.HasFpsWarningsMaxTime)
			{
				num ^= this.FpsWarningsMaxTime.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C9C9 RID: 51657 RVA: 0x003C72B8 File Offset: 0x003C54B8
		public override bool Equals(object obj)
		{
			FlowPerformance flowPerformance = obj as FlowPerformance;
			return flowPerformance != null && this.HasUniqueId == flowPerformance.HasUniqueId && (!this.HasUniqueId || this.UniqueId.Equals(flowPerformance.UniqueId)) && this.HasDeviceInfo == flowPerformance.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(flowPerformance.DeviceInfo)) && this.HasPlayer == flowPerformance.HasPlayer && (!this.HasPlayer || this.Player.Equals(flowPerformance.Player)) && this.HasFlowType_ == flowPerformance.HasFlowType_ && (!this.HasFlowType_ || this.FlowType_.Equals(flowPerformance.FlowType_)) && this.HasAverageFps == flowPerformance.HasAverageFps && (!this.HasAverageFps || this.AverageFps.Equals(flowPerformance.AverageFps)) && this.HasDuration == flowPerformance.HasDuration && (!this.HasDuration || this.Duration.Equals(flowPerformance.Duration)) && this.HasFpsWarningsThreshold == flowPerformance.HasFpsWarningsThreshold && (!this.HasFpsWarningsThreshold || this.FpsWarningsThreshold.Equals(flowPerformance.FpsWarningsThreshold)) && this.HasFpsWarningsTotalOccurences == flowPerformance.HasFpsWarningsTotalOccurences && (!this.HasFpsWarningsTotalOccurences || this.FpsWarningsTotalOccurences.Equals(flowPerformance.FpsWarningsTotalOccurences)) && this.HasFpsWarningsTotalTime == flowPerformance.HasFpsWarningsTotalTime && (!this.HasFpsWarningsTotalTime || this.FpsWarningsTotalTime.Equals(flowPerformance.FpsWarningsTotalTime)) && this.HasFpsWarningsAverageTime == flowPerformance.HasFpsWarningsAverageTime && (!this.HasFpsWarningsAverageTime || this.FpsWarningsAverageTime.Equals(flowPerformance.FpsWarningsAverageTime)) && this.HasFpsWarningsMaxTime == flowPerformance.HasFpsWarningsMaxTime && (!this.HasFpsWarningsMaxTime || this.FpsWarningsMaxTime.Equals(flowPerformance.FpsWarningsMaxTime));
		}

		// Token: 0x0600C9CA RID: 51658 RVA: 0x003C74CE File Offset: 0x003C56CE
		public void Deserialize(Stream stream)
		{
			FlowPerformance.Deserialize(stream, this);
		}

		// Token: 0x0600C9CB RID: 51659 RVA: 0x003C74D8 File Offset: 0x003C56D8
		public static FlowPerformance Deserialize(Stream stream, FlowPerformance instance)
		{
			return FlowPerformance.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C9CC RID: 51660 RVA: 0x003C74E4 File Offset: 0x003C56E4
		public static FlowPerformance DeserializeLengthDelimited(Stream stream)
		{
			FlowPerformance flowPerformance = new FlowPerformance();
			FlowPerformance.DeserializeLengthDelimited(stream, flowPerformance);
			return flowPerformance;
		}

		// Token: 0x0600C9CD RID: 51661 RVA: 0x003C7500 File Offset: 0x003C5700
		public static FlowPerformance DeserializeLengthDelimited(Stream stream, FlowPerformance instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FlowPerformance.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C9CE RID: 51662 RVA: 0x003C7528 File Offset: 0x003C5728
		public static FlowPerformance Deserialize(Stream stream, FlowPerformance instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.FlowType_ = FlowPerformance.FlowType.SHOP;
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
					if (num <= 45)
					{
						if (num <= 18)
						{
							if (num == 10)
							{
								instance.UniqueId = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 18)
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
						else if (num != 26)
						{
							if (num == 32)
							{
								instance.FlowType_ = (FlowPerformance.FlowType)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 45)
							{
								instance.AverageFps = binaryReader.ReadSingle();
								continue;
							}
						}
						else
						{
							if (instance.Player == null)
							{
								instance.Player = Player.DeserializeLengthDelimited(stream);
								continue;
							}
							Player.DeserializeLengthDelimited(stream, instance.Player);
							continue;
						}
					}
					else if (num <= 64)
					{
						if (num == 53)
						{
							instance.Duration = binaryReader.ReadSingle();
							continue;
						}
						if (num == 61)
						{
							instance.FpsWarningsThreshold = binaryReader.ReadSingle();
							continue;
						}
						if (num == 64)
						{
							instance.FpsWarningsTotalOccurences = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 77)
						{
							instance.FpsWarningsTotalTime = binaryReader.ReadSingle();
							continue;
						}
						if (num == 85)
						{
							instance.FpsWarningsAverageTime = binaryReader.ReadSingle();
							continue;
						}
						if (num == 93)
						{
							instance.FpsWarningsMaxTime = binaryReader.ReadSingle();
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

		// Token: 0x0600C9CF RID: 51663 RVA: 0x003C7712 File Offset: 0x003C5912
		public void Serialize(Stream stream)
		{
			FlowPerformance.Serialize(stream, this);
		}

		// Token: 0x0600C9D0 RID: 51664 RVA: 0x003C771C File Offset: 0x003C591C
		public static void Serialize(Stream stream, FlowPerformance instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasUniqueId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.UniqueId));
			}
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasPlayer)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.HasFlowType_)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FlowType_));
			}
			if (instance.HasAverageFps)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.AverageFps);
			}
			if (instance.HasDuration)
			{
				stream.WriteByte(53);
				binaryWriter.Write(instance.Duration);
			}
			if (instance.HasFpsWarningsThreshold)
			{
				stream.WriteByte(61);
				binaryWriter.Write(instance.FpsWarningsThreshold);
			}
			if (instance.HasFpsWarningsTotalOccurences)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FpsWarningsTotalOccurences));
			}
			if (instance.HasFpsWarningsTotalTime)
			{
				stream.WriteByte(77);
				binaryWriter.Write(instance.FpsWarningsTotalTime);
			}
			if (instance.HasFpsWarningsAverageTime)
			{
				stream.WriteByte(85);
				binaryWriter.Write(instance.FpsWarningsAverageTime);
			}
			if (instance.HasFpsWarningsMaxTime)
			{
				stream.WriteByte(93);
				binaryWriter.Write(instance.FpsWarningsMaxTime);
			}
		}

		// Token: 0x0600C9D1 RID: 51665 RVA: 0x003C7894 File Offset: 0x003C5A94
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasUniqueId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.UniqueId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasPlayer)
			{
				num += 1U;
				uint serializedSize2 = this.Player.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasFlowType_)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FlowType_));
			}
			if (this.HasAverageFps)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasDuration)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasFpsWarningsThreshold)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasFpsWarningsTotalOccurences)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FpsWarningsTotalOccurences));
			}
			if (this.HasFpsWarningsTotalTime)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasFpsWarningsAverageTime)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasFpsWarningsMaxTime)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x04009F6B RID: 40811
		public bool HasUniqueId;

		// Token: 0x04009F6C RID: 40812
		private string _UniqueId;

		// Token: 0x04009F6D RID: 40813
		public bool HasDeviceInfo;

		// Token: 0x04009F6E RID: 40814
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009F6F RID: 40815
		public bool HasPlayer;

		// Token: 0x04009F70 RID: 40816
		private Player _Player;

		// Token: 0x04009F71 RID: 40817
		public bool HasFlowType_;

		// Token: 0x04009F72 RID: 40818
		private FlowPerformance.FlowType _FlowType_;

		// Token: 0x04009F73 RID: 40819
		public bool HasAverageFps;

		// Token: 0x04009F74 RID: 40820
		private float _AverageFps;

		// Token: 0x04009F75 RID: 40821
		public bool HasDuration;

		// Token: 0x04009F76 RID: 40822
		private float _Duration;

		// Token: 0x04009F77 RID: 40823
		public bool HasFpsWarningsThreshold;

		// Token: 0x04009F78 RID: 40824
		private float _FpsWarningsThreshold;

		// Token: 0x04009F79 RID: 40825
		public bool HasFpsWarningsTotalOccurences;

		// Token: 0x04009F7A RID: 40826
		private int _FpsWarningsTotalOccurences;

		// Token: 0x04009F7B RID: 40827
		public bool HasFpsWarningsTotalTime;

		// Token: 0x04009F7C RID: 40828
		private float _FpsWarningsTotalTime;

		// Token: 0x04009F7D RID: 40829
		public bool HasFpsWarningsAverageTime;

		// Token: 0x04009F7E RID: 40830
		private float _FpsWarningsAverageTime;

		// Token: 0x04009F7F RID: 40831
		public bool HasFpsWarningsMaxTime;

		// Token: 0x04009F80 RID: 40832
		private float _FpsWarningsMaxTime;

		// Token: 0x02002942 RID: 10562
		public enum FlowType
		{
			// Token: 0x0400FC59 RID: 64601
			SHOP = 1,
			// Token: 0x0400FC5A RID: 64602
			COLLECTION_MANAGER,
			// Token: 0x0400FC5B RID: 64603
			GAME,
			// Token: 0x0400FC5C RID: 64604
			JOURNAL,
			// Token: 0x0400FC5D RID: 64605
			ARENA,
			// Token: 0x0400FC5E RID: 64606
			DUELS,
			// Token: 0x0400FC5F RID: 64607
			ADVENTURE
		}
	}
}
