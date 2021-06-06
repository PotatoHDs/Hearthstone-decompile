using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011B3 RID: 4531
	public class DataUpdateStarted : IProtoBuf
	{
		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x0600C8C1 RID: 51393 RVA: 0x003C3B63 File Offset: 0x003C1D63
		// (set) Token: 0x0600C8C2 RID: 51394 RVA: 0x003C3B6B File Offset: 0x003C1D6B
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

		// Token: 0x0600C8C3 RID: 51395 RVA: 0x003C3B80 File Offset: 0x003C1D80
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C8C4 RID: 51396 RVA: 0x003C3BB0 File Offset: 0x003C1DB0
		public override bool Equals(object obj)
		{
			DataUpdateStarted dataUpdateStarted = obj as DataUpdateStarted;
			return dataUpdateStarted != null && this.HasDeviceInfo == dataUpdateStarted.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(dataUpdateStarted.DeviceInfo));
		}

		// Token: 0x0600C8C5 RID: 51397 RVA: 0x003C3BF5 File Offset: 0x003C1DF5
		public void Deserialize(Stream stream)
		{
			DataUpdateStarted.Deserialize(stream, this);
		}

		// Token: 0x0600C8C6 RID: 51398 RVA: 0x003C3BFF File Offset: 0x003C1DFF
		public static DataUpdateStarted Deserialize(Stream stream, DataUpdateStarted instance)
		{
			return DataUpdateStarted.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C8C7 RID: 51399 RVA: 0x003C3C0C File Offset: 0x003C1E0C
		public static DataUpdateStarted DeserializeLengthDelimited(Stream stream)
		{
			DataUpdateStarted dataUpdateStarted = new DataUpdateStarted();
			DataUpdateStarted.DeserializeLengthDelimited(stream, dataUpdateStarted);
			return dataUpdateStarted;
		}

		// Token: 0x0600C8C8 RID: 51400 RVA: 0x003C3C28 File Offset: 0x003C1E28
		public static DataUpdateStarted DeserializeLengthDelimited(Stream stream, DataUpdateStarted instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DataUpdateStarted.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C8C9 RID: 51401 RVA: 0x003C3C50 File Offset: 0x003C1E50
		public static DataUpdateStarted Deserialize(Stream stream, DataUpdateStarted instance, long limit)
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
				else if (num == 10)
				{
					if (instance.DeviceInfo == null)
					{
						instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
					}
				}
				else
				{
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

		// Token: 0x0600C8CA RID: 51402 RVA: 0x003C3CEA File Offset: 0x003C1EEA
		public void Serialize(Stream stream)
		{
			DataUpdateStarted.Serialize(stream, this);
		}

		// Token: 0x0600C8CB RID: 51403 RVA: 0x003C3CF3 File Offset: 0x003C1EF3
		public static void Serialize(Stream stream, DataUpdateStarted instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
		}

		// Token: 0x0600C8CC RID: 51404 RVA: 0x003C3D24 File Offset: 0x003C1F24
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04009F09 RID: 40713
		public bool HasDeviceInfo;

		// Token: 0x04009F0A RID: 40714
		private DeviceInfo _DeviceInfo;
	}
}
