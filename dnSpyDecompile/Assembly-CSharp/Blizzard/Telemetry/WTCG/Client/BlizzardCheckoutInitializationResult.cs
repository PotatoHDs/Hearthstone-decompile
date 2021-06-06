using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011A1 RID: 4513
	public class BlizzardCheckoutInitializationResult : IProtoBuf
	{
		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x0600C77D RID: 51069 RVA: 0x003BF535 File Offset: 0x003BD735
		// (set) Token: 0x0600C77E RID: 51070 RVA: 0x003BF53D File Offset: 0x003BD73D
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

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x0600C77F RID: 51071 RVA: 0x003BF550 File Offset: 0x003BD750
		// (set) Token: 0x0600C780 RID: 51072 RVA: 0x003BF558 File Offset: 0x003BD758
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

		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x0600C781 RID: 51073 RVA: 0x003BF56B File Offset: 0x003BD76B
		// (set) Token: 0x0600C782 RID: 51074 RVA: 0x003BF573 File Offset: 0x003BD773
		public bool Success
		{
			get
			{
				return this._Success;
			}
			set
			{
				this._Success = value;
				this.HasSuccess = true;
			}
		}

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x0600C783 RID: 51075 RVA: 0x003BF583 File Offset: 0x003BD783
		// (set) Token: 0x0600C784 RID: 51076 RVA: 0x003BF58B File Offset: 0x003BD78B
		public string FailureReason
		{
			get
			{
				return this._FailureReason;
			}
			set
			{
				this._FailureReason = value;
				this.HasFailureReason = (value != null);
			}
		}

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x0600C785 RID: 51077 RVA: 0x003BF59E File Offset: 0x003BD79E
		// (set) Token: 0x0600C786 RID: 51078 RVA: 0x003BF5A6 File Offset: 0x003BD7A6
		public string FailureDetails
		{
			get
			{
				return this._FailureDetails;
			}
			set
			{
				this._FailureDetails = value;
				this.HasFailureDetails = (value != null);
			}
		}

		// Token: 0x0600C787 RID: 51079 RVA: 0x003BF5BC File Offset: 0x003BD7BC
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
			if (this.HasSuccess)
			{
				num ^= this.Success.GetHashCode();
			}
			if (this.HasFailureReason)
			{
				num ^= this.FailureReason.GetHashCode();
			}
			if (this.HasFailureDetails)
			{
				num ^= this.FailureDetails.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C788 RID: 51080 RVA: 0x003BF648 File Offset: 0x003BD848
		public override bool Equals(object obj)
		{
			BlizzardCheckoutInitializationResult blizzardCheckoutInitializationResult = obj as BlizzardCheckoutInitializationResult;
			return blizzardCheckoutInitializationResult != null && this.HasPlayer == blizzardCheckoutInitializationResult.HasPlayer && (!this.HasPlayer || this.Player.Equals(blizzardCheckoutInitializationResult.Player)) && this.HasDeviceInfo == blizzardCheckoutInitializationResult.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(blizzardCheckoutInitializationResult.DeviceInfo)) && this.HasSuccess == blizzardCheckoutInitializationResult.HasSuccess && (!this.HasSuccess || this.Success.Equals(blizzardCheckoutInitializationResult.Success)) && this.HasFailureReason == blizzardCheckoutInitializationResult.HasFailureReason && (!this.HasFailureReason || this.FailureReason.Equals(blizzardCheckoutInitializationResult.FailureReason)) && this.HasFailureDetails == blizzardCheckoutInitializationResult.HasFailureDetails && (!this.HasFailureDetails || this.FailureDetails.Equals(blizzardCheckoutInitializationResult.FailureDetails));
		}

		// Token: 0x0600C789 RID: 51081 RVA: 0x003BF73C File Offset: 0x003BD93C
		public void Deserialize(Stream stream)
		{
			BlizzardCheckoutInitializationResult.Deserialize(stream, this);
		}

		// Token: 0x0600C78A RID: 51082 RVA: 0x003BF746 File Offset: 0x003BD946
		public static BlizzardCheckoutInitializationResult Deserialize(Stream stream, BlizzardCheckoutInitializationResult instance)
		{
			return BlizzardCheckoutInitializationResult.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C78B RID: 51083 RVA: 0x003BF754 File Offset: 0x003BD954
		public static BlizzardCheckoutInitializationResult DeserializeLengthDelimited(Stream stream)
		{
			BlizzardCheckoutInitializationResult blizzardCheckoutInitializationResult = new BlizzardCheckoutInitializationResult();
			BlizzardCheckoutInitializationResult.DeserializeLengthDelimited(stream, blizzardCheckoutInitializationResult);
			return blizzardCheckoutInitializationResult;
		}

		// Token: 0x0600C78C RID: 51084 RVA: 0x003BF770 File Offset: 0x003BD970
		public static BlizzardCheckoutInitializationResult DeserializeLengthDelimited(Stream stream, BlizzardCheckoutInitializationResult instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BlizzardCheckoutInitializationResult.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C78D RID: 51085 RVA: 0x003BF798 File Offset: 0x003BD998
		public static BlizzardCheckoutInitializationResult Deserialize(Stream stream, BlizzardCheckoutInitializationResult instance, long limit)
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
						if (num == 24)
						{
							instance.Success = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 34)
						{
							instance.FailureReason = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 42)
						{
							instance.FailureDetails = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600C78E RID: 51086 RVA: 0x003BF8BC File Offset: 0x003BDABC
		public void Serialize(Stream stream)
		{
			BlizzardCheckoutInitializationResult.Serialize(stream, this);
		}

		// Token: 0x0600C78F RID: 51087 RVA: 0x003BF8C8 File Offset: 0x003BDAC8
		public static void Serialize(Stream stream, BlizzardCheckoutInitializationResult instance)
		{
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
			if (instance.HasSuccess)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.Success);
			}
			if (instance.HasFailureReason)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FailureReason));
			}
			if (instance.HasFailureDetails)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FailureDetails));
			}
		}

		// Token: 0x0600C790 RID: 51088 RVA: 0x003BF998 File Offset: 0x003BDB98
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
			if (this.HasSuccess)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasFailureReason)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.FailureReason);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasFailureDetails)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.FailureDetails);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x04009E8C RID: 40588
		public bool HasPlayer;

		// Token: 0x04009E8D RID: 40589
		private Player _Player;

		// Token: 0x04009E8E RID: 40590
		public bool HasDeviceInfo;

		// Token: 0x04009E8F RID: 40591
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009E90 RID: 40592
		public bool HasSuccess;

		// Token: 0x04009E91 RID: 40593
		private bool _Success;

		// Token: 0x04009E92 RID: 40594
		public bool HasFailureReason;

		// Token: 0x04009E93 RID: 40595
		private string _FailureReason;

		// Token: 0x04009E94 RID: 40596
		public bool HasFailureDetails;

		// Token: 0x04009E95 RID: 40597
		private string _FailureDetails;
	}
}
