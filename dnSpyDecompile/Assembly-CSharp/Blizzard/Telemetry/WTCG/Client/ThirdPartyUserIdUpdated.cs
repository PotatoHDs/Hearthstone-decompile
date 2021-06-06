using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x02001207 RID: 4615
	public class ThirdPartyUserIdUpdated : IProtoBuf
	{
		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x0600CF0B RID: 53003 RVA: 0x003DA902 File Offset: 0x003D8B02
		// (set) Token: 0x0600CF0C RID: 53004 RVA: 0x003DA90A File Offset: 0x003D8B0A
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

		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x0600CF0D RID: 53005 RVA: 0x003DA91D File Offset: 0x003D8B1D
		// (set) Token: 0x0600CF0E RID: 53006 RVA: 0x003DA925 File Offset: 0x003D8B25
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

		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x0600CF0F RID: 53007 RVA: 0x003DA938 File Offset: 0x003D8B38
		// (set) Token: 0x0600CF10 RID: 53008 RVA: 0x003DA940 File Offset: 0x003D8B40
		public bool ValidChange
		{
			get
			{
				return this._ValidChange;
			}
			set
			{
				this._ValidChange = value;
				this.HasValidChange = true;
			}
		}

		// Token: 0x0600CF11 RID: 53009 RVA: 0x003DA950 File Offset: 0x003D8B50
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
			if (this.HasValidChange)
			{
				num ^= this.ValidChange.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CF12 RID: 53010 RVA: 0x003DA9B0 File Offset: 0x003D8BB0
		public override bool Equals(object obj)
		{
			ThirdPartyUserIdUpdated thirdPartyUserIdUpdated = obj as ThirdPartyUserIdUpdated;
			return thirdPartyUserIdUpdated != null && this.HasPlayer == thirdPartyUserIdUpdated.HasPlayer && (!this.HasPlayer || this.Player.Equals(thirdPartyUserIdUpdated.Player)) && this.HasDeviceInfo == thirdPartyUserIdUpdated.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(thirdPartyUserIdUpdated.DeviceInfo)) && this.HasValidChange == thirdPartyUserIdUpdated.HasValidChange && (!this.HasValidChange || this.ValidChange.Equals(thirdPartyUserIdUpdated.ValidChange));
		}

		// Token: 0x0600CF13 RID: 53011 RVA: 0x003DAA4E File Offset: 0x003D8C4E
		public void Deserialize(Stream stream)
		{
			ThirdPartyUserIdUpdated.Deserialize(stream, this);
		}

		// Token: 0x0600CF14 RID: 53012 RVA: 0x003DAA58 File Offset: 0x003D8C58
		public static ThirdPartyUserIdUpdated Deserialize(Stream stream, ThirdPartyUserIdUpdated instance)
		{
			return ThirdPartyUserIdUpdated.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CF15 RID: 53013 RVA: 0x003DAA64 File Offset: 0x003D8C64
		public static ThirdPartyUserIdUpdated DeserializeLengthDelimited(Stream stream)
		{
			ThirdPartyUserIdUpdated thirdPartyUserIdUpdated = new ThirdPartyUserIdUpdated();
			ThirdPartyUserIdUpdated.DeserializeLengthDelimited(stream, thirdPartyUserIdUpdated);
			return thirdPartyUserIdUpdated;
		}

		// Token: 0x0600CF16 RID: 53014 RVA: 0x003DAA80 File Offset: 0x003D8C80
		public static ThirdPartyUserIdUpdated DeserializeLengthDelimited(Stream stream, ThirdPartyUserIdUpdated instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ThirdPartyUserIdUpdated.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CF17 RID: 53015 RVA: 0x003DAAA8 File Offset: 0x003D8CA8
		public static ThirdPartyUserIdUpdated Deserialize(Stream stream, ThirdPartyUserIdUpdated instance, long limit)
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
							instance.ValidChange = ProtocolParser.ReadBool(stream);
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

		// Token: 0x0600CF18 RID: 53016 RVA: 0x003DAB90 File Offset: 0x003D8D90
		public void Serialize(Stream stream)
		{
			ThirdPartyUserIdUpdated.Serialize(stream, this);
		}

		// Token: 0x0600CF19 RID: 53017 RVA: 0x003DAB9C File Offset: 0x003D8D9C
		public static void Serialize(Stream stream, ThirdPartyUserIdUpdated instance)
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
			if (instance.HasValidChange)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.ValidChange);
			}
		}

		// Token: 0x0600CF1A RID: 53018 RVA: 0x003DAC20 File Offset: 0x003D8E20
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
			if (this.HasValidChange)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x0400A1B5 RID: 41397
		public bool HasPlayer;

		// Token: 0x0400A1B6 RID: 41398
		private Player _Player;

		// Token: 0x0400A1B7 RID: 41399
		public bool HasDeviceInfo;

		// Token: 0x0400A1B8 RID: 41400
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A1B9 RID: 41401
		public bool HasValidChange;

		// Token: 0x0400A1BA RID: 41402
		private bool _ValidChange;
	}
}
