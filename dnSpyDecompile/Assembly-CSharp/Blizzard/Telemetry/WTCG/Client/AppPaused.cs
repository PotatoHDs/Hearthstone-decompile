using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x0200118E RID: 4494
	public class AppPaused : IProtoBuf
	{
		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x0600C5E6 RID: 50662 RVA: 0x003B8F3C File Offset: 0x003B713C
		// (set) Token: 0x0600C5E7 RID: 50663 RVA: 0x003B8F44 File Offset: 0x003B7144
		public bool PauseStatus
		{
			get
			{
				return this._PauseStatus;
			}
			set
			{
				this._PauseStatus = value;
				this.HasPauseStatus = true;
			}
		}

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x0600C5E8 RID: 50664 RVA: 0x003B8F54 File Offset: 0x003B7154
		// (set) Token: 0x0600C5E9 RID: 50665 RVA: 0x003B8F5C File Offset: 0x003B715C
		public float PauseTime
		{
			get
			{
				return this._PauseTime;
			}
			set
			{
				this._PauseTime = value;
				this.HasPauseTime = true;
			}
		}

		// Token: 0x0600C5EA RID: 50666 RVA: 0x003B8F6C File Offset: 0x003B716C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPauseStatus)
			{
				num ^= this.PauseStatus.GetHashCode();
			}
			if (this.HasPauseTime)
			{
				num ^= this.PauseTime.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C5EB RID: 50667 RVA: 0x003B8FB8 File Offset: 0x003B71B8
		public override bool Equals(object obj)
		{
			AppPaused appPaused = obj as AppPaused;
			return appPaused != null && this.HasPauseStatus == appPaused.HasPauseStatus && (!this.HasPauseStatus || this.PauseStatus.Equals(appPaused.PauseStatus)) && this.HasPauseTime == appPaused.HasPauseTime && (!this.HasPauseTime || this.PauseTime.Equals(appPaused.PauseTime));
		}

		// Token: 0x0600C5EC RID: 50668 RVA: 0x003B902E File Offset: 0x003B722E
		public void Deserialize(Stream stream)
		{
			AppPaused.Deserialize(stream, this);
		}

		// Token: 0x0600C5ED RID: 50669 RVA: 0x003B9038 File Offset: 0x003B7238
		public static AppPaused Deserialize(Stream stream, AppPaused instance)
		{
			return AppPaused.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C5EE RID: 50670 RVA: 0x003B9044 File Offset: 0x003B7244
		public static AppPaused DeserializeLengthDelimited(Stream stream)
		{
			AppPaused appPaused = new AppPaused();
			AppPaused.DeserializeLengthDelimited(stream, appPaused);
			return appPaused;
		}

		// Token: 0x0600C5EF RID: 50671 RVA: 0x003B9060 File Offset: 0x003B7260
		public static AppPaused DeserializeLengthDelimited(Stream stream, AppPaused instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AppPaused.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C5F0 RID: 50672 RVA: 0x003B9088 File Offset: 0x003B7288
		public static AppPaused Deserialize(Stream stream, AppPaused instance, long limit)
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
				else if (num != 8)
				{
					if (num != 21)
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
						instance.PauseTime = binaryReader.ReadSingle();
					}
				}
				else
				{
					instance.PauseStatus = ProtocolParser.ReadBool(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600C5F1 RID: 50673 RVA: 0x003B9126 File Offset: 0x003B7326
		public void Serialize(Stream stream)
		{
			AppPaused.Serialize(stream, this);
		}

		// Token: 0x0600C5F2 RID: 50674 RVA: 0x003B9130 File Offset: 0x003B7330
		public static void Serialize(Stream stream, AppPaused instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasPauseStatus)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.PauseStatus);
			}
			if (instance.HasPauseTime)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.PauseTime);
			}
		}

		// Token: 0x0600C5F3 RID: 50675 RVA: 0x003B917C File Offset: 0x003B737C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasPauseStatus)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasPauseTime)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x04009DC7 RID: 40391
		public bool HasPauseStatus;

		// Token: 0x04009DC8 RID: 40392
		private bool _PauseStatus;

		// Token: 0x04009DC9 RID: 40393
		public bool HasPauseTime;

		// Token: 0x04009DCA RID: 40394
		private float _PauseTime;
	}
}
