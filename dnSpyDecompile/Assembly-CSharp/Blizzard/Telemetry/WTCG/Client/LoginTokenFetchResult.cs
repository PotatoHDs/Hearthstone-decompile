using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011D8 RID: 4568
	public class LoginTokenFetchResult : IProtoBuf
	{
		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x0600CB8E RID: 52110 RVA: 0x003CDD7B File Offset: 0x003CBF7B
		// (set) Token: 0x0600CB8F RID: 52111 RVA: 0x003CDD83 File Offset: 0x003CBF83
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

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x0600CB90 RID: 52112 RVA: 0x003CDD96 File Offset: 0x003CBF96
		// (set) Token: 0x0600CB91 RID: 52113 RVA: 0x003CDD9E File Offset: 0x003CBF9E
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

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x0600CB92 RID: 52114 RVA: 0x003CDDB1 File Offset: 0x003CBFB1
		// (set) Token: 0x0600CB93 RID: 52115 RVA: 0x003CDDB9 File Offset: 0x003CBFB9
		public LoginTokenFetchResult.TokenFetchResult Result
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

		// Token: 0x0600CB94 RID: 52116 RVA: 0x003CDDCC File Offset: 0x003CBFCC
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
			return num;
		}

		// Token: 0x0600CB95 RID: 52117 RVA: 0x003CDE34 File Offset: 0x003CC034
		public override bool Equals(object obj)
		{
			LoginTokenFetchResult loginTokenFetchResult = obj as LoginTokenFetchResult;
			return loginTokenFetchResult != null && this.HasPlayer == loginTokenFetchResult.HasPlayer && (!this.HasPlayer || this.Player.Equals(loginTokenFetchResult.Player)) && this.HasDeviceInfo == loginTokenFetchResult.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(loginTokenFetchResult.DeviceInfo)) && this.HasResult == loginTokenFetchResult.HasResult && (!this.HasResult || this.Result.Equals(loginTokenFetchResult.Result));
		}

		// Token: 0x0600CB96 RID: 52118 RVA: 0x003CDEDD File Offset: 0x003CC0DD
		public void Deserialize(Stream stream)
		{
			LoginTokenFetchResult.Deserialize(stream, this);
		}

		// Token: 0x0600CB97 RID: 52119 RVA: 0x003CDEE7 File Offset: 0x003CC0E7
		public static LoginTokenFetchResult Deserialize(Stream stream, LoginTokenFetchResult instance)
		{
			return LoginTokenFetchResult.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CB98 RID: 52120 RVA: 0x003CDEF4 File Offset: 0x003CC0F4
		public static LoginTokenFetchResult DeserializeLengthDelimited(Stream stream)
		{
			LoginTokenFetchResult loginTokenFetchResult = new LoginTokenFetchResult();
			LoginTokenFetchResult.DeserializeLengthDelimited(stream, loginTokenFetchResult);
			return loginTokenFetchResult;
		}

		// Token: 0x0600CB99 RID: 52121 RVA: 0x003CDF10 File Offset: 0x003CC110
		public static LoginTokenFetchResult DeserializeLengthDelimited(Stream stream, LoginTokenFetchResult instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return LoginTokenFetchResult.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CB9A RID: 52122 RVA: 0x003CDF38 File Offset: 0x003CC138
		public static LoginTokenFetchResult Deserialize(Stream stream, LoginTokenFetchResult instance, long limit)
		{
			instance.Result = LoginTokenFetchResult.TokenFetchResult.SUCCESS;
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
				else if (num != 10)
				{
					if (num != 18)
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
							instance.Result = (LoginTokenFetchResult.TokenFetchResult)ProtocolParser.ReadUInt64(stream);
						}
					}
					else if (instance.DeviceInfo == null)
					{
						instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
					}
				}
				else if (instance.Player == null)
				{
					instance.Player = Player.DeserializeLengthDelimited(stream);
				}
				else
				{
					Player.DeserializeLengthDelimited(stream, instance.Player);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600CB9B RID: 52123 RVA: 0x003CE028 File Offset: 0x003CC228
		public void Serialize(Stream stream)
		{
			LoginTokenFetchResult.Serialize(stream, this);
		}

		// Token: 0x0600CB9C RID: 52124 RVA: 0x003CE034 File Offset: 0x003CC234
		public static void Serialize(Stream stream, LoginTokenFetchResult instance)
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
		}

		// Token: 0x0600CB9D RID: 52125 RVA: 0x003CE0B8 File Offset: 0x003CC2B8
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
			return num;
		}

		// Token: 0x0400A03F RID: 41023
		public bool HasPlayer;

		// Token: 0x0400A040 RID: 41024
		private Player _Player;

		// Token: 0x0400A041 RID: 41025
		public bool HasDeviceInfo;

		// Token: 0x0400A042 RID: 41026
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A043 RID: 41027
		public bool HasResult;

		// Token: 0x0400A044 RID: 41028
		private LoginTokenFetchResult.TokenFetchResult _Result;

		// Token: 0x02002945 RID: 10565
		public enum TokenFetchResult
		{
			// Token: 0x0400FC6E RID: 64622
			SUCCESS,
			// Token: 0x0400FC6F RID: 64623
			CANCELED,
			// Token: 0x0400FC70 RID: 64624
			FAILURE
		}
	}
}
