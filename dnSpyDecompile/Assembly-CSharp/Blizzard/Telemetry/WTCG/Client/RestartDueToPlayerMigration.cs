using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011ED RID: 4589
	public class RestartDueToPlayerMigration : IProtoBuf
	{
		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x0600CD09 RID: 52489 RVA: 0x003D2F1E File Offset: 0x003D111E
		// (set) Token: 0x0600CD0A RID: 52490 RVA: 0x003D2F26 File Offset: 0x003D1126
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

		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x0600CD0B RID: 52491 RVA: 0x003D2F39 File Offset: 0x003D1139
		// (set) Token: 0x0600CD0C RID: 52492 RVA: 0x003D2F41 File Offset: 0x003D1141
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

		// Token: 0x0600CD0D RID: 52493 RVA: 0x003D2F54 File Offset: 0x003D1154
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
			return num;
		}

		// Token: 0x0600CD0E RID: 52494 RVA: 0x003D2F9C File Offset: 0x003D119C
		public override bool Equals(object obj)
		{
			RestartDueToPlayerMigration restartDueToPlayerMigration = obj as RestartDueToPlayerMigration;
			return restartDueToPlayerMigration != null && this.HasPlayer == restartDueToPlayerMigration.HasPlayer && (!this.HasPlayer || this.Player.Equals(restartDueToPlayerMigration.Player)) && this.HasDeviceInfo == restartDueToPlayerMigration.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(restartDueToPlayerMigration.DeviceInfo));
		}

		// Token: 0x0600CD0F RID: 52495 RVA: 0x003D300C File Offset: 0x003D120C
		public void Deserialize(Stream stream)
		{
			RestartDueToPlayerMigration.Deserialize(stream, this);
		}

		// Token: 0x0600CD10 RID: 52496 RVA: 0x003D3016 File Offset: 0x003D1216
		public static RestartDueToPlayerMigration Deserialize(Stream stream, RestartDueToPlayerMigration instance)
		{
			return RestartDueToPlayerMigration.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CD11 RID: 52497 RVA: 0x003D3024 File Offset: 0x003D1224
		public static RestartDueToPlayerMigration DeserializeLengthDelimited(Stream stream)
		{
			RestartDueToPlayerMigration restartDueToPlayerMigration = new RestartDueToPlayerMigration();
			RestartDueToPlayerMigration.DeserializeLengthDelimited(stream, restartDueToPlayerMigration);
			return restartDueToPlayerMigration;
		}

		// Token: 0x0600CD12 RID: 52498 RVA: 0x003D3040 File Offset: 0x003D1240
		public static RestartDueToPlayerMigration DeserializeLengthDelimited(Stream stream, RestartDueToPlayerMigration instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RestartDueToPlayerMigration.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CD13 RID: 52499 RVA: 0x003D3068 File Offset: 0x003D1268
		public static RestartDueToPlayerMigration Deserialize(Stream stream, RestartDueToPlayerMigration instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
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

		// Token: 0x0600CD14 RID: 52500 RVA: 0x003D313A File Offset: 0x003D133A
		public void Serialize(Stream stream)
		{
			RestartDueToPlayerMigration.Serialize(stream, this);
		}

		// Token: 0x0600CD15 RID: 52501 RVA: 0x003D3144 File Offset: 0x003D1344
		public static void Serialize(Stream stream, RestartDueToPlayerMigration instance)
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
		}

		// Token: 0x0600CD16 RID: 52502 RVA: 0x003D31AC File Offset: 0x003D13AC
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
			return num;
		}

		// Token: 0x0400A0D3 RID: 41171
		public bool HasPlayer;

		// Token: 0x0400A0D4 RID: 41172
		private Player _Player;

		// Token: 0x0400A0D5 RID: 41173
		public bool HasDeviceInfo;

		// Token: 0x0400A0D6 RID: 41174
		private DeviceInfo _DeviceInfo;
	}
}
