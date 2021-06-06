using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x02001209 RID: 4617
	public class WelcomeQuestsAcknowledged : IProtoBuf
	{
		// Token: 0x1700103B RID: 4155
		// (get) Token: 0x0600CF2B RID: 53035 RVA: 0x003DAF6A File Offset: 0x003D916A
		// (set) Token: 0x0600CF2C RID: 53036 RVA: 0x003DAF72 File Offset: 0x003D9172
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

		// Token: 0x1700103C RID: 4156
		// (get) Token: 0x0600CF2D RID: 53037 RVA: 0x003DAF85 File Offset: 0x003D9185
		// (set) Token: 0x0600CF2E RID: 53038 RVA: 0x003DAF8D File Offset: 0x003D918D
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

		// Token: 0x1700103D RID: 4157
		// (get) Token: 0x0600CF2F RID: 53039 RVA: 0x003DAFA0 File Offset: 0x003D91A0
		// (set) Token: 0x0600CF30 RID: 53040 RVA: 0x003DAFA8 File Offset: 0x003D91A8
		public float QuestAckDuration
		{
			get
			{
				return this._QuestAckDuration;
			}
			set
			{
				this._QuestAckDuration = value;
				this.HasQuestAckDuration = true;
			}
		}

		// Token: 0x0600CF31 RID: 53041 RVA: 0x003DAFB8 File Offset: 0x003D91B8
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
			if (this.HasQuestAckDuration)
			{
				num ^= this.QuestAckDuration.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CF32 RID: 53042 RVA: 0x003DB018 File Offset: 0x003D9218
		public override bool Equals(object obj)
		{
			WelcomeQuestsAcknowledged welcomeQuestsAcknowledged = obj as WelcomeQuestsAcknowledged;
			return welcomeQuestsAcknowledged != null && this.HasPlayer == welcomeQuestsAcknowledged.HasPlayer && (!this.HasPlayer || this.Player.Equals(welcomeQuestsAcknowledged.Player)) && this.HasDeviceInfo == welcomeQuestsAcknowledged.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(welcomeQuestsAcknowledged.DeviceInfo)) && this.HasQuestAckDuration == welcomeQuestsAcknowledged.HasQuestAckDuration && (!this.HasQuestAckDuration || this.QuestAckDuration.Equals(welcomeQuestsAcknowledged.QuestAckDuration));
		}

		// Token: 0x0600CF33 RID: 53043 RVA: 0x003DB0B6 File Offset: 0x003D92B6
		public void Deserialize(Stream stream)
		{
			WelcomeQuestsAcknowledged.Deserialize(stream, this);
		}

		// Token: 0x0600CF34 RID: 53044 RVA: 0x003DB0C0 File Offset: 0x003D92C0
		public static WelcomeQuestsAcknowledged Deserialize(Stream stream, WelcomeQuestsAcknowledged instance)
		{
			return WelcomeQuestsAcknowledged.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CF35 RID: 53045 RVA: 0x003DB0CC File Offset: 0x003D92CC
		public static WelcomeQuestsAcknowledged DeserializeLengthDelimited(Stream stream)
		{
			WelcomeQuestsAcknowledged welcomeQuestsAcknowledged = new WelcomeQuestsAcknowledged();
			WelcomeQuestsAcknowledged.DeserializeLengthDelimited(stream, welcomeQuestsAcknowledged);
			return welcomeQuestsAcknowledged;
		}

		// Token: 0x0600CF36 RID: 53046 RVA: 0x003DB0E8 File Offset: 0x003D92E8
		public static WelcomeQuestsAcknowledged DeserializeLengthDelimited(Stream stream, WelcomeQuestsAcknowledged instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return WelcomeQuestsAcknowledged.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CF37 RID: 53047 RVA: 0x003DB110 File Offset: 0x003D9310
		public static WelcomeQuestsAcknowledged Deserialize(Stream stream, WelcomeQuestsAcknowledged instance, long limit)
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
				else if (num != 10)
				{
					if (num != 18)
					{
						if (num != 29)
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
							instance.QuestAckDuration = binaryReader.ReadSingle();
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

		// Token: 0x0600CF38 RID: 53048 RVA: 0x003DB1FF File Offset: 0x003D93FF
		public void Serialize(Stream stream)
		{
			WelcomeQuestsAcknowledged.Serialize(stream, this);
		}

		// Token: 0x0600CF39 RID: 53049 RVA: 0x003DB208 File Offset: 0x003D9408
		public static void Serialize(Stream stream, WelcomeQuestsAcknowledged instance)
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
			if (instance.HasQuestAckDuration)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.QuestAckDuration);
			}
		}

		// Token: 0x0600CF3A RID: 53050 RVA: 0x003DB294 File Offset: 0x003D9494
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
			if (this.HasQuestAckDuration)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x0400A1BF RID: 41407
		public bool HasPlayer;

		// Token: 0x0400A1C0 RID: 41408
		private Player _Player;

		// Token: 0x0400A1C1 RID: 41409
		public bool HasDeviceInfo;

		// Token: 0x0400A1C2 RID: 41410
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A1C3 RID: 41411
		public bool HasQuestAckDuration;

		// Token: 0x0400A1C4 RID: 41412
		private float _QuestAckDuration;
	}
}
