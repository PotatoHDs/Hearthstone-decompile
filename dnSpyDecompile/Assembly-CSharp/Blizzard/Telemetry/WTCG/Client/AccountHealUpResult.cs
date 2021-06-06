using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x0200118C RID: 4492
	public class AccountHealUpResult : IProtoBuf
	{
		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x0600C5C2 RID: 50626 RVA: 0x003B8780 File Offset: 0x003B6980
		// (set) Token: 0x0600C5C3 RID: 50627 RVA: 0x003B8788 File Offset: 0x003B6988
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

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x0600C5C4 RID: 50628 RVA: 0x003B879B File Offset: 0x003B699B
		// (set) Token: 0x0600C5C5 RID: 50629 RVA: 0x003B87A3 File Offset: 0x003B69A3
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

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x0600C5C6 RID: 50630 RVA: 0x003B87B6 File Offset: 0x003B69B6
		// (set) Token: 0x0600C5C7 RID: 50631 RVA: 0x003B87BE File Offset: 0x003B69BE
		public AccountHealUpResult.HealUpResult Result
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

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x0600C5C8 RID: 50632 RVA: 0x003B87CE File Offset: 0x003B69CE
		// (set) Token: 0x0600C5C9 RID: 50633 RVA: 0x003B87D6 File Offset: 0x003B69D6
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

		// Token: 0x0600C5CA RID: 50634 RVA: 0x003B87E8 File Offset: 0x003B69E8
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
			return num;
		}

		// Token: 0x0600C5CB RID: 50635 RVA: 0x003B8868 File Offset: 0x003B6A68
		public override bool Equals(object obj)
		{
			AccountHealUpResult accountHealUpResult = obj as AccountHealUpResult;
			return accountHealUpResult != null && this.HasPlayer == accountHealUpResult.HasPlayer && (!this.HasPlayer || this.Player.Equals(accountHealUpResult.Player)) && this.HasDeviceInfo == accountHealUpResult.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(accountHealUpResult.DeviceInfo)) && this.HasResult == accountHealUpResult.HasResult && (!this.HasResult || this.Result.Equals(accountHealUpResult.Result)) && this.HasErrorCode == accountHealUpResult.HasErrorCode && (!this.HasErrorCode || this.ErrorCode.Equals(accountHealUpResult.ErrorCode));
		}

		// Token: 0x0600C5CC RID: 50636 RVA: 0x003B893F File Offset: 0x003B6B3F
		public void Deserialize(Stream stream)
		{
			AccountHealUpResult.Deserialize(stream, this);
		}

		// Token: 0x0600C5CD RID: 50637 RVA: 0x003B8949 File Offset: 0x003B6B49
		public static AccountHealUpResult Deserialize(Stream stream, AccountHealUpResult instance)
		{
			return AccountHealUpResult.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C5CE RID: 50638 RVA: 0x003B8954 File Offset: 0x003B6B54
		public static AccountHealUpResult DeserializeLengthDelimited(Stream stream)
		{
			AccountHealUpResult accountHealUpResult = new AccountHealUpResult();
			AccountHealUpResult.DeserializeLengthDelimited(stream, accountHealUpResult);
			return accountHealUpResult;
		}

		// Token: 0x0600C5CF RID: 50639 RVA: 0x003B8970 File Offset: 0x003B6B70
		public static AccountHealUpResult DeserializeLengthDelimited(Stream stream, AccountHealUpResult instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AccountHealUpResult.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C5D0 RID: 50640 RVA: 0x003B8998 File Offset: 0x003B6B98
		public static AccountHealUpResult Deserialize(Stream stream, AccountHealUpResult instance, long limit)
		{
			instance.Result = AccountHealUpResult.HealUpResult.SUCCESS;
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
							instance.Result = (AccountHealUpResult.HealUpResult)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.ErrorCode = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600C5D1 RID: 50641 RVA: 0x003B8AAC File Offset: 0x003B6CAC
		public void Serialize(Stream stream)
		{
			AccountHealUpResult.Serialize(stream, this);
		}

		// Token: 0x0600C5D2 RID: 50642 RVA: 0x003B8AB8 File Offset: 0x003B6CB8
		public static void Serialize(Stream stream, AccountHealUpResult instance)
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
		}

		// Token: 0x0600C5D3 RID: 50643 RVA: 0x003B8B5C File Offset: 0x003B6D5C
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
			return num;
		}

		// Token: 0x04009DB9 RID: 40377
		public bool HasPlayer;

		// Token: 0x04009DBA RID: 40378
		private Player _Player;

		// Token: 0x04009DBB RID: 40379
		public bool HasDeviceInfo;

		// Token: 0x04009DBC RID: 40380
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009DBD RID: 40381
		public bool HasResult;

		// Token: 0x04009DBE RID: 40382
		private AccountHealUpResult.HealUpResult _Result;

		// Token: 0x04009DBF RID: 40383
		public bool HasErrorCode;

		// Token: 0x04009DC0 RID: 40384
		private int _ErrorCode;

		// Token: 0x0200293B RID: 10555
		public enum HealUpResult
		{
			// Token: 0x0400FC39 RID: 64569
			SUCCESS,
			// Token: 0x0400FC3A RID: 64570
			CANCELED,
			// Token: 0x0400FC3B RID: 64571
			FAILURE
		}
	}
}
