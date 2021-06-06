using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x02001208 RID: 4616
	public class WebLoginError : IProtoBuf
	{
		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x0600CF1C RID: 53020 RVA: 0x003DAC86 File Offset: 0x003D8E86
		// (set) Token: 0x0600CF1D RID: 53021 RVA: 0x003DAC8E File Offset: 0x003D8E8E
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

		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x0600CF1E RID: 53022 RVA: 0x003DACA1 File Offset: 0x003D8EA1
		// (set) Token: 0x0600CF1F RID: 53023 RVA: 0x003DACA9 File Offset: 0x003D8EA9
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

		// Token: 0x0600CF20 RID: 53024 RVA: 0x003DACBC File Offset: 0x003D8EBC
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

		// Token: 0x0600CF21 RID: 53025 RVA: 0x003DAD04 File Offset: 0x003D8F04
		public override bool Equals(object obj)
		{
			WebLoginError webLoginError = obj as WebLoginError;
			return webLoginError != null && this.HasPlayer == webLoginError.HasPlayer && (!this.HasPlayer || this.Player.Equals(webLoginError.Player)) && this.HasDeviceInfo == webLoginError.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(webLoginError.DeviceInfo));
		}

		// Token: 0x0600CF22 RID: 53026 RVA: 0x003DAD74 File Offset: 0x003D8F74
		public void Deserialize(Stream stream)
		{
			WebLoginError.Deserialize(stream, this);
		}

		// Token: 0x0600CF23 RID: 53027 RVA: 0x003DAD7E File Offset: 0x003D8F7E
		public static WebLoginError Deserialize(Stream stream, WebLoginError instance)
		{
			return WebLoginError.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CF24 RID: 53028 RVA: 0x003DAD8C File Offset: 0x003D8F8C
		public static WebLoginError DeserializeLengthDelimited(Stream stream)
		{
			WebLoginError webLoginError = new WebLoginError();
			WebLoginError.DeserializeLengthDelimited(stream, webLoginError);
			return webLoginError;
		}

		// Token: 0x0600CF25 RID: 53029 RVA: 0x003DADA8 File Offset: 0x003D8FA8
		public static WebLoginError DeserializeLengthDelimited(Stream stream, WebLoginError instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return WebLoginError.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CF26 RID: 53030 RVA: 0x003DADD0 File Offset: 0x003D8FD0
		public static WebLoginError Deserialize(Stream stream, WebLoginError instance, long limit)
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

		// Token: 0x0600CF27 RID: 53031 RVA: 0x003DAEA2 File Offset: 0x003D90A2
		public void Serialize(Stream stream)
		{
			WebLoginError.Serialize(stream, this);
		}

		// Token: 0x0600CF28 RID: 53032 RVA: 0x003DAEAC File Offset: 0x003D90AC
		public static void Serialize(Stream stream, WebLoginError instance)
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

		// Token: 0x0600CF29 RID: 53033 RVA: 0x003DAF14 File Offset: 0x003D9114
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

		// Token: 0x0400A1BB RID: 41403
		public bool HasPlayer;

		// Token: 0x0400A1BC RID: 41404
		private Player _Player;

		// Token: 0x0400A1BD RID: 41405
		public bool HasDeviceInfo;

		// Token: 0x0400A1BE RID: 41406
		private DeviceInfo _DeviceInfo;
	}
}
