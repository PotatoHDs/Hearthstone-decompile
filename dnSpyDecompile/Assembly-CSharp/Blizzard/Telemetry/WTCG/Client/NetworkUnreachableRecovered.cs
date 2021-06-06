using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011E1 RID: 4577
	public class NetworkUnreachableRecovered : IProtoBuf
	{
		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x0600CC31 RID: 52273 RVA: 0x003D0151 File Offset: 0x003CE351
		// (set) Token: 0x0600CC32 RID: 52274 RVA: 0x003D0159 File Offset: 0x003CE359
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

		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x0600CC33 RID: 52275 RVA: 0x003D016C File Offset: 0x003CE36C
		// (set) Token: 0x0600CC34 RID: 52276 RVA: 0x003D0174 File Offset: 0x003CE374
		public int OutageSeconds
		{
			get
			{
				return this._OutageSeconds;
			}
			set
			{
				this._OutageSeconds = value;
				this.HasOutageSeconds = true;
			}
		}

		// Token: 0x0600CC35 RID: 52277 RVA: 0x003D0184 File Offset: 0x003CE384
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasOutageSeconds)
			{
				num ^= this.OutageSeconds.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CC36 RID: 52278 RVA: 0x003D01D0 File Offset: 0x003CE3D0
		public override bool Equals(object obj)
		{
			NetworkUnreachableRecovered networkUnreachableRecovered = obj as NetworkUnreachableRecovered;
			return networkUnreachableRecovered != null && this.HasDeviceInfo == networkUnreachableRecovered.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(networkUnreachableRecovered.DeviceInfo)) && this.HasOutageSeconds == networkUnreachableRecovered.HasOutageSeconds && (!this.HasOutageSeconds || this.OutageSeconds.Equals(networkUnreachableRecovered.OutageSeconds));
		}

		// Token: 0x0600CC37 RID: 52279 RVA: 0x003D0243 File Offset: 0x003CE443
		public void Deserialize(Stream stream)
		{
			NetworkUnreachableRecovered.Deserialize(stream, this);
		}

		// Token: 0x0600CC38 RID: 52280 RVA: 0x003D024D File Offset: 0x003CE44D
		public static NetworkUnreachableRecovered Deserialize(Stream stream, NetworkUnreachableRecovered instance)
		{
			return NetworkUnreachableRecovered.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CC39 RID: 52281 RVA: 0x003D0258 File Offset: 0x003CE458
		public static NetworkUnreachableRecovered DeserializeLengthDelimited(Stream stream)
		{
			NetworkUnreachableRecovered networkUnreachableRecovered = new NetworkUnreachableRecovered();
			NetworkUnreachableRecovered.DeserializeLengthDelimited(stream, networkUnreachableRecovered);
			return networkUnreachableRecovered;
		}

		// Token: 0x0600CC3A RID: 52282 RVA: 0x003D0274 File Offset: 0x003CE474
		public static NetworkUnreachableRecovered DeserializeLengthDelimited(Stream stream, NetworkUnreachableRecovered instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return NetworkUnreachableRecovered.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CC3B RID: 52283 RVA: 0x003D029C File Offset: 0x003CE49C
		public static NetworkUnreachableRecovered Deserialize(Stream stream, NetworkUnreachableRecovered instance, long limit)
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
					if (num != 16)
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
						instance.OutageSeconds = (int)ProtocolParser.ReadUInt64(stream);
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
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600CC3C RID: 52284 RVA: 0x003D034F File Offset: 0x003CE54F
		public void Serialize(Stream stream)
		{
			NetworkUnreachableRecovered.Serialize(stream, this);
		}

		// Token: 0x0600CC3D RID: 52285 RVA: 0x003D0358 File Offset: 0x003CE558
		public static void Serialize(Stream stream, NetworkUnreachableRecovered instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasOutageSeconds)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.OutageSeconds));
			}
		}

		// Token: 0x0600CC3E RID: 52286 RVA: 0x003D03B0 File Offset: 0x003CE5B0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasOutageSeconds)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.OutageSeconds));
			}
			return num;
		}

		// Token: 0x0400A07F RID: 41087
		public bool HasDeviceInfo;

		// Token: 0x0400A080 RID: 41088
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A081 RID: 41089
		public bool HasOutageSeconds;

		// Token: 0x0400A082 RID: 41090
		private int _OutageSeconds;
	}
}
