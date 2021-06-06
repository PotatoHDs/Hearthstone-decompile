using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011E6 RID: 4582
	public class PreviousInstanceStatus : IProtoBuf
	{
		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x0600CC86 RID: 52358 RVA: 0x003D12A0 File Offset: 0x003CF4A0
		// (set) Token: 0x0600CC87 RID: 52359 RVA: 0x003D12A8 File Offset: 0x003CF4A8
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

		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x0600CC88 RID: 52360 RVA: 0x003D12BB File Offset: 0x003CF4BB
		// (set) Token: 0x0600CC89 RID: 52361 RVA: 0x003D12C3 File Offset: 0x003CF4C3
		public int TotalCrashCount
		{
			get
			{
				return this._TotalCrashCount;
			}
			set
			{
				this._TotalCrashCount = value;
				this.HasTotalCrashCount = true;
			}
		}

		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x0600CC8A RID: 52362 RVA: 0x003D12D3 File Offset: 0x003CF4D3
		// (set) Token: 0x0600CC8B RID: 52363 RVA: 0x003D12DB File Offset: 0x003CF4DB
		public int TotalExceptionCount
		{
			get
			{
				return this._TotalExceptionCount;
			}
			set
			{
				this._TotalExceptionCount = value;
				this.HasTotalExceptionCount = true;
			}
		}

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x0600CC8C RID: 52364 RVA: 0x003D12EB File Offset: 0x003CF4EB
		// (set) Token: 0x0600CC8D RID: 52365 RVA: 0x003D12F3 File Offset: 0x003CF4F3
		public int LowMemoryWarningCount
		{
			get
			{
				return this._LowMemoryWarningCount;
			}
			set
			{
				this._LowMemoryWarningCount = value;
				this.HasLowMemoryWarningCount = true;
			}
		}

		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x0600CC8E RID: 52366 RVA: 0x003D1303 File Offset: 0x003CF503
		// (set) Token: 0x0600CC8F RID: 52367 RVA: 0x003D130B File Offset: 0x003CF50B
		public int CrashInARowCount
		{
			get
			{
				return this._CrashInARowCount;
			}
			set
			{
				this._CrashInARowCount = value;
				this.HasCrashInARowCount = true;
			}
		}

		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x0600CC90 RID: 52368 RVA: 0x003D131B File Offset: 0x003CF51B
		// (set) Token: 0x0600CC91 RID: 52369 RVA: 0x003D1323 File Offset: 0x003CF523
		public int SameExceptionCount
		{
			get
			{
				return this._SameExceptionCount;
			}
			set
			{
				this._SameExceptionCount = value;
				this.HasSameExceptionCount = true;
			}
		}

		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x0600CC92 RID: 52370 RVA: 0x003D1333 File Offset: 0x003CF533
		// (set) Token: 0x0600CC93 RID: 52371 RVA: 0x003D133B File Offset: 0x003CF53B
		public bool Crashed
		{
			get
			{
				return this._Crashed;
			}
			set
			{
				this._Crashed = value;
				this.HasCrashed = true;
			}
		}

		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x0600CC94 RID: 52372 RVA: 0x003D134B File Offset: 0x003CF54B
		// (set) Token: 0x0600CC95 RID: 52373 RVA: 0x003D1353 File Offset: 0x003CF553
		public string ExceptionHash
		{
			get
			{
				return this._ExceptionHash;
			}
			set
			{
				this._ExceptionHash = value;
				this.HasExceptionHash = (value != null);
			}
		}

		// Token: 0x0600CC96 RID: 52374 RVA: 0x003D1368 File Offset: 0x003CF568
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasTotalCrashCount)
			{
				num ^= this.TotalCrashCount.GetHashCode();
			}
			if (this.HasTotalExceptionCount)
			{
				num ^= this.TotalExceptionCount.GetHashCode();
			}
			if (this.HasLowMemoryWarningCount)
			{
				num ^= this.LowMemoryWarningCount.GetHashCode();
			}
			if (this.HasCrashInARowCount)
			{
				num ^= this.CrashInARowCount.GetHashCode();
			}
			if (this.HasSameExceptionCount)
			{
				num ^= this.SameExceptionCount.GetHashCode();
			}
			if (this.HasCrashed)
			{
				num ^= this.Crashed.GetHashCode();
			}
			if (this.HasExceptionHash)
			{
				num ^= this.ExceptionHash.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CC97 RID: 52375 RVA: 0x003D1444 File Offset: 0x003CF644
		public override bool Equals(object obj)
		{
			PreviousInstanceStatus previousInstanceStatus = obj as PreviousInstanceStatus;
			return previousInstanceStatus != null && this.HasDeviceInfo == previousInstanceStatus.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(previousInstanceStatus.DeviceInfo)) && this.HasTotalCrashCount == previousInstanceStatus.HasTotalCrashCount && (!this.HasTotalCrashCount || this.TotalCrashCount.Equals(previousInstanceStatus.TotalCrashCount)) && this.HasTotalExceptionCount == previousInstanceStatus.HasTotalExceptionCount && (!this.HasTotalExceptionCount || this.TotalExceptionCount.Equals(previousInstanceStatus.TotalExceptionCount)) && this.HasLowMemoryWarningCount == previousInstanceStatus.HasLowMemoryWarningCount && (!this.HasLowMemoryWarningCount || this.LowMemoryWarningCount.Equals(previousInstanceStatus.LowMemoryWarningCount)) && this.HasCrashInARowCount == previousInstanceStatus.HasCrashInARowCount && (!this.HasCrashInARowCount || this.CrashInARowCount.Equals(previousInstanceStatus.CrashInARowCount)) && this.HasSameExceptionCount == previousInstanceStatus.HasSameExceptionCount && (!this.HasSameExceptionCount || this.SameExceptionCount.Equals(previousInstanceStatus.SameExceptionCount)) && this.HasCrashed == previousInstanceStatus.HasCrashed && (!this.HasCrashed || this.Crashed.Equals(previousInstanceStatus.Crashed)) && this.HasExceptionHash == previousInstanceStatus.HasExceptionHash && (!this.HasExceptionHash || this.ExceptionHash.Equals(previousInstanceStatus.ExceptionHash));
		}

		// Token: 0x0600CC98 RID: 52376 RVA: 0x003D15C8 File Offset: 0x003CF7C8
		public void Deserialize(Stream stream)
		{
			PreviousInstanceStatus.Deserialize(stream, this);
		}

		// Token: 0x0600CC99 RID: 52377 RVA: 0x003D15D2 File Offset: 0x003CF7D2
		public static PreviousInstanceStatus Deserialize(Stream stream, PreviousInstanceStatus instance)
		{
			return PreviousInstanceStatus.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CC9A RID: 52378 RVA: 0x003D15E0 File Offset: 0x003CF7E0
		public static PreviousInstanceStatus DeserializeLengthDelimited(Stream stream)
		{
			PreviousInstanceStatus previousInstanceStatus = new PreviousInstanceStatus();
			PreviousInstanceStatus.DeserializeLengthDelimited(stream, previousInstanceStatus);
			return previousInstanceStatus;
		}

		// Token: 0x0600CC9B RID: 52379 RVA: 0x003D15FC File Offset: 0x003CF7FC
		public static PreviousInstanceStatus DeserializeLengthDelimited(Stream stream, PreviousInstanceStatus instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PreviousInstanceStatus.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CC9C RID: 52380 RVA: 0x003D1624 File Offset: 0x003CF824
		public static PreviousInstanceStatus Deserialize(Stream stream, PreviousInstanceStatus instance, long limit)
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
					if (num <= 32)
					{
						if (num <= 16)
						{
							if (num != 10)
							{
								if (num == 16)
								{
									instance.TotalCrashCount = (int)ProtocolParser.ReadUInt64(stream);
									continue;
								}
							}
							else
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
							if (num == 24)
							{
								instance.TotalExceptionCount = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 32)
							{
								instance.LowMemoryWarningCount = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
					}
					else if (num <= 48)
					{
						if (num == 40)
						{
							instance.CrashInARowCount = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 48)
						{
							instance.SameExceptionCount = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 56)
						{
							instance.Crashed = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 66)
						{
							instance.ExceptionHash = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600CC9D RID: 52381 RVA: 0x003D179B File Offset: 0x003CF99B
		public void Serialize(Stream stream)
		{
			PreviousInstanceStatus.Serialize(stream, this);
		}

		// Token: 0x0600CC9E RID: 52382 RVA: 0x003D17A4 File Offset: 0x003CF9A4
		public static void Serialize(Stream stream, PreviousInstanceStatus instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasTotalCrashCount)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.TotalCrashCount));
			}
			if (instance.HasTotalExceptionCount)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.TotalExceptionCount));
			}
			if (instance.HasLowMemoryWarningCount)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.LowMemoryWarningCount));
			}
			if (instance.HasCrashInARowCount)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CrashInARowCount));
			}
			if (instance.HasSameExceptionCount)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SameExceptionCount));
			}
			if (instance.HasCrashed)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.Crashed);
			}
			if (instance.HasExceptionHash)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ExceptionHash));
			}
		}

		// Token: 0x0600CC9F RID: 52383 RVA: 0x003D18B4 File Offset: 0x003CFAB4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasTotalCrashCount)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.TotalCrashCount));
			}
			if (this.HasTotalExceptionCount)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.TotalExceptionCount));
			}
			if (this.HasLowMemoryWarningCount)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.LowMemoryWarningCount));
			}
			if (this.HasCrashInARowCount)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CrashInARowCount));
			}
			if (this.HasSameExceptionCount)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SameExceptionCount));
			}
			if (this.HasCrashed)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasExceptionHash)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ExceptionHash);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x0400A09D RID: 41117
		public bool HasDeviceInfo;

		// Token: 0x0400A09E RID: 41118
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A09F RID: 41119
		public bool HasTotalCrashCount;

		// Token: 0x0400A0A0 RID: 41120
		private int _TotalCrashCount;

		// Token: 0x0400A0A1 RID: 41121
		public bool HasTotalExceptionCount;

		// Token: 0x0400A0A2 RID: 41122
		private int _TotalExceptionCount;

		// Token: 0x0400A0A3 RID: 41123
		public bool HasLowMemoryWarningCount;

		// Token: 0x0400A0A4 RID: 41124
		private int _LowMemoryWarningCount;

		// Token: 0x0400A0A5 RID: 41125
		public bool HasCrashInARowCount;

		// Token: 0x0400A0A6 RID: 41126
		private int _CrashInARowCount;

		// Token: 0x0400A0A7 RID: 41127
		public bool HasSameExceptionCount;

		// Token: 0x0400A0A8 RID: 41128
		private int _SameExceptionCount;

		// Token: 0x0400A0A9 RID: 41129
		public bool HasCrashed;

		// Token: 0x0400A0AA RID: 41130
		private bool _Crashed;

		// Token: 0x0400A0AB RID: 41131
		public bool HasExceptionHash;

		// Token: 0x0400A0AC RID: 41132
		private string _ExceptionHash;
	}
}
