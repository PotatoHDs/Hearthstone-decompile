using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011BC RID: 4540
	public class EndGameScreenInit : IProtoBuf
	{
		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x0600C960 RID: 51552 RVA: 0x003C5D45 File Offset: 0x003C3F45
		// (set) Token: 0x0600C961 RID: 51553 RVA: 0x003C5D4D File Offset: 0x003C3F4D
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

		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x0600C962 RID: 51554 RVA: 0x003C5D60 File Offset: 0x003C3F60
		// (set) Token: 0x0600C963 RID: 51555 RVA: 0x003C5D68 File Offset: 0x003C3F68
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

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x0600C964 RID: 51556 RVA: 0x003C5D7B File Offset: 0x003C3F7B
		// (set) Token: 0x0600C965 RID: 51557 RVA: 0x003C5D83 File Offset: 0x003C3F83
		public float ElapsedTime
		{
			get
			{
				return this._ElapsedTime;
			}
			set
			{
				this._ElapsedTime = value;
				this.HasElapsedTime = true;
			}
		}

		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x0600C966 RID: 51558 RVA: 0x003C5D93 File Offset: 0x003C3F93
		// (set) Token: 0x0600C967 RID: 51559 RVA: 0x003C5D9B File Offset: 0x003C3F9B
		public int MedalInfoRetryCount
		{
			get
			{
				return this._MedalInfoRetryCount;
			}
			set
			{
				this._MedalInfoRetryCount = value;
				this.HasMedalInfoRetryCount = true;
			}
		}

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x0600C968 RID: 51560 RVA: 0x003C5DAB File Offset: 0x003C3FAB
		// (set) Token: 0x0600C969 RID: 51561 RVA: 0x003C5DB3 File Offset: 0x003C3FB3
		public bool MedalInfoRetriesTimedOut
		{
			get
			{
				return this._MedalInfoRetriesTimedOut;
			}
			set
			{
				this._MedalInfoRetriesTimedOut = value;
				this.HasMedalInfoRetriesTimedOut = true;
			}
		}

		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x0600C96A RID: 51562 RVA: 0x003C5DC3 File Offset: 0x003C3FC3
		// (set) Token: 0x0600C96B RID: 51563 RVA: 0x003C5DCB File Offset: 0x003C3FCB
		public bool ShowRankedReward
		{
			get
			{
				return this._ShowRankedReward;
			}
			set
			{
				this._ShowRankedReward = value;
				this.HasShowRankedReward = true;
			}
		}

		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x0600C96C RID: 51564 RVA: 0x003C5DDB File Offset: 0x003C3FDB
		// (set) Token: 0x0600C96D RID: 51565 RVA: 0x003C5DE3 File Offset: 0x003C3FE3
		public bool ShowCardBackProgress
		{
			get
			{
				return this._ShowCardBackProgress;
			}
			set
			{
				this._ShowCardBackProgress = value;
				this.HasShowCardBackProgress = true;
			}
		}

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x0600C96E RID: 51566 RVA: 0x003C5DF3 File Offset: 0x003C3FF3
		// (set) Token: 0x0600C96F RID: 51567 RVA: 0x003C5DFB File Offset: 0x003C3FFB
		public int OtherRewardCount
		{
			get
			{
				return this._OtherRewardCount;
			}
			set
			{
				this._OtherRewardCount = value;
				this.HasOtherRewardCount = true;
			}
		}

		// Token: 0x0600C970 RID: 51568 RVA: 0x003C5E0C File Offset: 0x003C400C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPlayer)
			{
				num ^= this.Player.GetHashCode();
			}
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasElapsedTime)
			{
				num ^= this.ElapsedTime.GetHashCode();
			}
			if (this.HasMedalInfoRetryCount)
			{
				num ^= this.MedalInfoRetryCount.GetHashCode();
			}
			if (this.HasMedalInfoRetriesTimedOut)
			{
				num ^= this.MedalInfoRetriesTimedOut.GetHashCode();
			}
			if (this.HasShowRankedReward)
			{
				num ^= this.ShowRankedReward.GetHashCode();
			}
			if (this.HasShowCardBackProgress)
			{
				num ^= this.ShowCardBackProgress.GetHashCode();
			}
			if (this.HasOtherRewardCount)
			{
				num ^= this.OtherRewardCount.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C971 RID: 51569 RVA: 0x003C5EE8 File Offset: 0x003C40E8
		public override bool Equals(object obj)
		{
			EndGameScreenInit endGameScreenInit = obj as EndGameScreenInit;
			return endGameScreenInit != null && this.HasPlayer == endGameScreenInit.HasPlayer && (!this.HasPlayer || this.Player.Equals(endGameScreenInit.Player)) && this.HasDeviceInfo == endGameScreenInit.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(endGameScreenInit.DeviceInfo)) && this.HasElapsedTime == endGameScreenInit.HasElapsedTime && (!this.HasElapsedTime || this.ElapsedTime.Equals(endGameScreenInit.ElapsedTime)) && this.HasMedalInfoRetryCount == endGameScreenInit.HasMedalInfoRetryCount && (!this.HasMedalInfoRetryCount || this.MedalInfoRetryCount.Equals(endGameScreenInit.MedalInfoRetryCount)) && this.HasMedalInfoRetriesTimedOut == endGameScreenInit.HasMedalInfoRetriesTimedOut && (!this.HasMedalInfoRetriesTimedOut || this.MedalInfoRetriesTimedOut.Equals(endGameScreenInit.MedalInfoRetriesTimedOut)) && this.HasShowRankedReward == endGameScreenInit.HasShowRankedReward && (!this.HasShowRankedReward || this.ShowRankedReward.Equals(endGameScreenInit.ShowRankedReward)) && this.HasShowCardBackProgress == endGameScreenInit.HasShowCardBackProgress && (!this.HasShowCardBackProgress || this.ShowCardBackProgress.Equals(endGameScreenInit.ShowCardBackProgress)) && this.HasOtherRewardCount == endGameScreenInit.HasOtherRewardCount && (!this.HasOtherRewardCount || this.OtherRewardCount.Equals(endGameScreenInit.OtherRewardCount));
		}

		// Token: 0x0600C972 RID: 51570 RVA: 0x003C606C File Offset: 0x003C426C
		public void Deserialize(Stream stream)
		{
			EndGameScreenInit.Deserialize(stream, this);
		}

		// Token: 0x0600C973 RID: 51571 RVA: 0x003C6076 File Offset: 0x003C4276
		public static EndGameScreenInit Deserialize(Stream stream, EndGameScreenInit instance)
		{
			return EndGameScreenInit.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C974 RID: 51572 RVA: 0x003C6084 File Offset: 0x003C4284
		public static EndGameScreenInit DeserializeLengthDelimited(Stream stream)
		{
			EndGameScreenInit endGameScreenInit = new EndGameScreenInit();
			EndGameScreenInit.DeserializeLengthDelimited(stream, endGameScreenInit);
			return endGameScreenInit;
		}

		// Token: 0x0600C975 RID: 51573 RVA: 0x003C60A0 File Offset: 0x003C42A0
		public static EndGameScreenInit DeserializeLengthDelimited(Stream stream, EndGameScreenInit instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return EndGameScreenInit.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C976 RID: 51574 RVA: 0x003C60C8 File Offset: 0x003C42C8
		public static EndGameScreenInit Deserialize(Stream stream, EndGameScreenInit instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					if (num <= 32)
					{
						if (num <= 18)
						{
							if (num != 10)
							{
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
						else
						{
							if (num == 29)
							{
								instance.ElapsedTime = binaryReader.ReadSingle();
								continue;
							}
							if (num == 32)
							{
								instance.MedalInfoRetryCount = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
					}
					else if (num <= 48)
					{
						if (num == 40)
						{
							instance.MedalInfoRetriesTimedOut = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 48)
						{
							instance.ShowRankedReward = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					else
					{
						if (num == 56)
						{
							instance.ShowCardBackProgress = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 64)
						{
							instance.OtherRewardCount = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600C977 RID: 51575 RVA: 0x003C6260 File Offset: 0x003C4460
		public void Serialize(Stream stream)
		{
			EndGameScreenInit.Serialize(stream, this);
		}

		// Token: 0x0600C978 RID: 51576 RVA: 0x003C626C File Offset: 0x003C446C
		public static void Serialize(Stream stream, EndGameScreenInit instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasPlayer)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasElapsedTime)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.ElapsedTime);
			}
			if (instance.HasMedalInfoRetryCount)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MedalInfoRetryCount));
			}
			if (instance.HasMedalInfoRetriesTimedOut)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.MedalInfoRetriesTimedOut);
			}
			if (instance.HasShowRankedReward)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.ShowRankedReward);
			}
			if (instance.HasShowCardBackProgress)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.ShowCardBackProgress);
			}
			if (instance.HasOtherRewardCount)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.OtherRewardCount));
			}
		}

		// Token: 0x0600C979 RID: 51577 RVA: 0x003C6384 File Offset: 0x003C4584
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasPlayer)
			{
				num += 1U;
				uint serializedSize = this.Player.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize2 = this.DeviceInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasElapsedTime)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasMedalInfoRetryCount)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.MedalInfoRetryCount));
			}
			if (this.HasMedalInfoRetriesTimedOut)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasShowRankedReward)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasShowCardBackProgress)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasOtherRewardCount)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.OtherRewardCount));
			}
			return num;
		}

		// Token: 0x04009F45 RID: 40773
		public bool HasPlayer;

		// Token: 0x04009F46 RID: 40774
		private Player _Player;

		// Token: 0x04009F47 RID: 40775
		public bool HasDeviceInfo;

		// Token: 0x04009F48 RID: 40776
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009F49 RID: 40777
		public bool HasElapsedTime;

		// Token: 0x04009F4A RID: 40778
		private float _ElapsedTime;

		// Token: 0x04009F4B RID: 40779
		public bool HasMedalInfoRetryCount;

		// Token: 0x04009F4C RID: 40780
		private int _MedalInfoRetryCount;

		// Token: 0x04009F4D RID: 40781
		public bool HasMedalInfoRetriesTimedOut;

		// Token: 0x04009F4E RID: 40782
		private bool _MedalInfoRetriesTimedOut;

		// Token: 0x04009F4F RID: 40783
		public bool HasShowRankedReward;

		// Token: 0x04009F50 RID: 40784
		private bool _ShowRankedReward;

		// Token: 0x04009F51 RID: 40785
		public bool HasShowCardBackProgress;

		// Token: 0x04009F52 RID: 40786
		private bool _ShowCardBackProgress;

		// Token: 0x04009F53 RID: 40787
		public bool HasOtherRewardCount;

		// Token: 0x04009F54 RID: 40788
		private int _OtherRewardCount;
	}
}
