using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011DA RID: 4570
	public class MASDKAuthResult : IProtoBuf
	{
		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x0600CBAC RID: 52140 RVA: 0x003CE31B File Offset: 0x003CC51B
		// (set) Token: 0x0600CBAD RID: 52141 RVA: 0x003CE323 File Offset: 0x003CC523
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

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x0600CBAE RID: 52142 RVA: 0x003CE336 File Offset: 0x003CC536
		// (set) Token: 0x0600CBAF RID: 52143 RVA: 0x003CE33E File Offset: 0x003CC53E
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

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x0600CBB0 RID: 52144 RVA: 0x003CE351 File Offset: 0x003CC551
		// (set) Token: 0x0600CBB1 RID: 52145 RVA: 0x003CE359 File Offset: 0x003CC559
		public MASDKAuthResult.AuthResult Result
		{
			get
			{
				return this._Result;
			}
			set
			{
				this._Result = value;
				this.HasResult = true;
			}
		}

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x0600CBB2 RID: 52146 RVA: 0x003CE369 File Offset: 0x003CC569
		// (set) Token: 0x0600CBB3 RID: 52147 RVA: 0x003CE371 File Offset: 0x003CC571
		public int ErrorCode
		{
			get
			{
				return this._ErrorCode;
			}
			set
			{
				this._ErrorCode = value;
				this.HasErrorCode = true;
			}
		}

		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x0600CBB4 RID: 52148 RVA: 0x003CE381 File Offset: 0x003CC581
		// (set) Token: 0x0600CBB5 RID: 52149 RVA: 0x003CE389 File Offset: 0x003CC589
		public string Source
		{
			get
			{
				return this._Source;
			}
			set
			{
				this._Source = value;
				this.HasSource = (value != null);
			}
		}

		// Token: 0x0600CBB6 RID: 52150 RVA: 0x003CE39C File Offset: 0x003CC59C
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
			if (this.HasResult)
			{
				num ^= this.Result.GetHashCode();
			}
			if (this.HasErrorCode)
			{
				num ^= this.ErrorCode.GetHashCode();
			}
			if (this.HasSource)
			{
				num ^= this.Source.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CBB7 RID: 52151 RVA: 0x003CE430 File Offset: 0x003CC630
		public override bool Equals(object obj)
		{
			MASDKAuthResult masdkauthResult = obj as MASDKAuthResult;
			return masdkauthResult != null && this.HasPlayer == masdkauthResult.HasPlayer && (!this.HasPlayer || this.Player.Equals(masdkauthResult.Player)) && this.HasDeviceInfo == masdkauthResult.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(masdkauthResult.DeviceInfo)) && this.HasResult == masdkauthResult.HasResult && (!this.HasResult || this.Result.Equals(masdkauthResult.Result)) && this.HasErrorCode == masdkauthResult.HasErrorCode && (!this.HasErrorCode || this.ErrorCode.Equals(masdkauthResult.ErrorCode)) && this.HasSource == masdkauthResult.HasSource && (!this.HasSource || this.Source.Equals(masdkauthResult.Source));
		}

		// Token: 0x0600CBB8 RID: 52152 RVA: 0x003CE532 File Offset: 0x003CC732
		public void Deserialize(Stream stream)
		{
			MASDKAuthResult.Deserialize(stream, this);
		}

		// Token: 0x0600CBB9 RID: 52153 RVA: 0x003CE53C File Offset: 0x003CC73C
		public static MASDKAuthResult Deserialize(Stream stream, MASDKAuthResult instance)
		{
			return MASDKAuthResult.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CBBA RID: 52154 RVA: 0x003CE548 File Offset: 0x003CC748
		public static MASDKAuthResult DeserializeLengthDelimited(Stream stream)
		{
			MASDKAuthResult masdkauthResult = new MASDKAuthResult();
			MASDKAuthResult.DeserializeLengthDelimited(stream, masdkauthResult);
			return masdkauthResult;
		}

		// Token: 0x0600CBBB RID: 52155 RVA: 0x003CE564 File Offset: 0x003CC764
		public static MASDKAuthResult DeserializeLengthDelimited(Stream stream, MASDKAuthResult instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MASDKAuthResult.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CBBC RID: 52156 RVA: 0x003CE58C File Offset: 0x003CC78C
		public static MASDKAuthResult Deserialize(Stream stream, MASDKAuthResult instance, long limit)
		{
			instance.Result = MASDKAuthResult.AuthResult.SUCCESS;
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
							instance.Result = (MASDKAuthResult.AuthResult)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.ErrorCode = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 42)
						{
							instance.Source = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600CBBD RID: 52157 RVA: 0x003CE6B9 File Offset: 0x003CC8B9
		public void Serialize(Stream stream)
		{
			MASDKAuthResult.Serialize(stream, this);
		}

		// Token: 0x0600CBBE RID: 52158 RVA: 0x003CE6C4 File Offset: 0x003CC8C4
		public static void Serialize(Stream stream, MASDKAuthResult instance)
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
			if (instance.HasResult)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Result));
			}
			if (instance.HasErrorCode)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ErrorCode));
			}
			if (instance.HasSource)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Source));
			}
		}

		// Token: 0x0600CBBF RID: 52159 RVA: 0x003CE78C File Offset: 0x003CC98C
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
			if (this.HasResult)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Result));
			}
			if (this.HasErrorCode)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ErrorCode));
			}
			if (this.HasSource)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Source);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x0400A047 RID: 41031
		public bool HasPlayer;

		// Token: 0x0400A048 RID: 41032
		private Player _Player;

		// Token: 0x0400A049 RID: 41033
		public bool HasDeviceInfo;

		// Token: 0x0400A04A RID: 41034
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A04B RID: 41035
		public bool HasResult;

		// Token: 0x0400A04C RID: 41036
		private MASDKAuthResult.AuthResult _Result;

		// Token: 0x0400A04D RID: 41037
		public bool HasErrorCode;

		// Token: 0x0400A04E RID: 41038
		private int _ErrorCode;

		// Token: 0x0400A04F RID: 41039
		public bool HasSource;

		// Token: 0x0400A050 RID: 41040
		private string _Source;

		// Token: 0x02002946 RID: 10566
		public enum AuthResult
		{
			// Token: 0x0400FC72 RID: 64626
			SUCCESS,
			// Token: 0x0400FC73 RID: 64627
			CANCELED,
			// Token: 0x0400FC74 RID: 64628
			FAILURE
		}
	}
}
