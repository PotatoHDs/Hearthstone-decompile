using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x02001205 RID: 4613
	public class ThirdPartyPurchaseSubmitResponseDeviceNotification : IProtoBuf
	{
		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x0600CEE7 RID: 52967 RVA: 0x003DA108 File Offset: 0x003D8308
		// (set) Token: 0x0600CEE8 RID: 52968 RVA: 0x003DA110 File Offset: 0x003D8310
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

		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x0600CEE9 RID: 52969 RVA: 0x003DA123 File Offset: 0x003D8323
		// (set) Token: 0x0600CEEA RID: 52970 RVA: 0x003DA12B File Offset: 0x003D832B
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

		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x0600CEEB RID: 52971 RVA: 0x003DA13E File Offset: 0x003D833E
		// (set) Token: 0x0600CEEC RID: 52972 RVA: 0x003DA146 File Offset: 0x003D8346
		public string TransactionId
		{
			get
			{
				return this._TransactionId;
			}
			set
			{
				this._TransactionId = value;
				this.HasTransactionId = (value != null);
			}
		}

		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x0600CEED RID: 52973 RVA: 0x003DA159 File Offset: 0x003D8359
		// (set) Token: 0x0600CEEE RID: 52974 RVA: 0x003DA161 File Offset: 0x003D8361
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

		// Token: 0x0600CEEF RID: 52975 RVA: 0x003DA174 File Offset: 0x003D8374
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
			if (this.HasTransactionId)
			{
				num ^= this.TransactionId.GetHashCode();
			}
			if (this.HasSuccess)
			{
				num ^= this.Success.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CEF0 RID: 52976 RVA: 0x003DA1EC File Offset: 0x003D83EC
		public override bool Equals(object obj)
		{
			ThirdPartyPurchaseSubmitResponseDeviceNotification thirdPartyPurchaseSubmitResponseDeviceNotification = obj as ThirdPartyPurchaseSubmitResponseDeviceNotification;
			return thirdPartyPurchaseSubmitResponseDeviceNotification != null && this.HasPlayer == thirdPartyPurchaseSubmitResponseDeviceNotification.HasPlayer && (!this.HasPlayer || this.Player.Equals(thirdPartyPurchaseSubmitResponseDeviceNotification.Player)) && this.HasDeviceInfo == thirdPartyPurchaseSubmitResponseDeviceNotification.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(thirdPartyPurchaseSubmitResponseDeviceNotification.DeviceInfo)) && this.HasTransactionId == thirdPartyPurchaseSubmitResponseDeviceNotification.HasTransactionId && (!this.HasTransactionId || this.TransactionId.Equals(thirdPartyPurchaseSubmitResponseDeviceNotification.TransactionId)) && this.HasSuccess == thirdPartyPurchaseSubmitResponseDeviceNotification.HasSuccess && (!this.HasSuccess || this.Success.Equals(thirdPartyPurchaseSubmitResponseDeviceNotification.Success));
		}

		// Token: 0x0600CEF1 RID: 52977 RVA: 0x003DA2B5 File Offset: 0x003D84B5
		public void Deserialize(Stream stream)
		{
			ThirdPartyPurchaseSubmitResponseDeviceNotification.Deserialize(stream, this);
		}

		// Token: 0x0600CEF2 RID: 52978 RVA: 0x003DA2BF File Offset: 0x003D84BF
		public static ThirdPartyPurchaseSubmitResponseDeviceNotification Deserialize(Stream stream, ThirdPartyPurchaseSubmitResponseDeviceNotification instance)
		{
			return ThirdPartyPurchaseSubmitResponseDeviceNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CEF3 RID: 52979 RVA: 0x003DA2CC File Offset: 0x003D84CC
		public static ThirdPartyPurchaseSubmitResponseDeviceNotification DeserializeLengthDelimited(Stream stream)
		{
			ThirdPartyPurchaseSubmitResponseDeviceNotification thirdPartyPurchaseSubmitResponseDeviceNotification = new ThirdPartyPurchaseSubmitResponseDeviceNotification();
			ThirdPartyPurchaseSubmitResponseDeviceNotification.DeserializeLengthDelimited(stream, thirdPartyPurchaseSubmitResponseDeviceNotification);
			return thirdPartyPurchaseSubmitResponseDeviceNotification;
		}

		// Token: 0x0600CEF4 RID: 52980 RVA: 0x003DA2E8 File Offset: 0x003D84E8
		public static ThirdPartyPurchaseSubmitResponseDeviceNotification DeserializeLengthDelimited(Stream stream, ThirdPartyPurchaseSubmitResponseDeviceNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ThirdPartyPurchaseSubmitResponseDeviceNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CEF5 RID: 52981 RVA: 0x003DA310 File Offset: 0x003D8510
		public static ThirdPartyPurchaseSubmitResponseDeviceNotification Deserialize(Stream stream, ThirdPartyPurchaseSubmitResponseDeviceNotification instance, long limit)
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
						if (num == 26)
						{
							instance.TransactionId = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 32)
						{
							instance.Success = ProtocolParser.ReadBool(stream);
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

		// Token: 0x0600CEF6 RID: 52982 RVA: 0x003DA41B File Offset: 0x003D861B
		public void Serialize(Stream stream)
		{
			ThirdPartyPurchaseSubmitResponseDeviceNotification.Serialize(stream, this);
		}

		// Token: 0x0600CEF7 RID: 52983 RVA: 0x003DA424 File Offset: 0x003D8624
		public static void Serialize(Stream stream, ThirdPartyPurchaseSubmitResponseDeviceNotification instance)
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
			if (instance.HasTransactionId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TransactionId));
			}
			if (instance.HasSuccess)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.Success);
			}
		}

		// Token: 0x0600CEF8 RID: 52984 RVA: 0x003DA4D0 File Offset: 0x003D86D0
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
			if (this.HasTransactionId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.TransactionId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasSuccess)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x0400A1A7 RID: 41383
		public bool HasPlayer;

		// Token: 0x0400A1A8 RID: 41384
		private Player _Player;

		// Token: 0x0400A1A9 RID: 41385
		public bool HasDeviceInfo;

		// Token: 0x0400A1AA RID: 41386
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A1AB RID: 41387
		public bool HasTransactionId;

		// Token: 0x0400A1AC RID: 41388
		private string _TransactionId;

		// Token: 0x0400A1AD RID: 41389
		public bool HasSuccess;

		// Token: 0x0400A1AE RID: 41390
		private bool _Success;
	}
}
