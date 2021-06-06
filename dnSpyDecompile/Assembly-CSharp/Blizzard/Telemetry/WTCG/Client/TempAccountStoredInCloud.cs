using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011F9 RID: 4601
	public class TempAccountStoredInCloud : IProtoBuf
	{
		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x0600CDF5 RID: 52725 RVA: 0x003D6637 File Offset: 0x003D4837
		// (set) Token: 0x0600CDF6 RID: 52726 RVA: 0x003D663F File Offset: 0x003D483F
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

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x0600CDF7 RID: 52727 RVA: 0x003D6652 File Offset: 0x003D4852
		// (set) Token: 0x0600CDF8 RID: 52728 RVA: 0x003D665A File Offset: 0x003D485A
		public bool Stored
		{
			get
			{
				return this._Stored;
			}
			set
			{
				this._Stored = value;
				this.HasStored = true;
			}
		}

		// Token: 0x0600CDF9 RID: 52729 RVA: 0x003D666C File Offset: 0x003D486C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasStored)
			{
				num ^= this.Stored.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CDFA RID: 52730 RVA: 0x003D66B8 File Offset: 0x003D48B8
		public override bool Equals(object obj)
		{
			TempAccountStoredInCloud tempAccountStoredInCloud = obj as TempAccountStoredInCloud;
			return tempAccountStoredInCloud != null && this.HasDeviceInfo == tempAccountStoredInCloud.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(tempAccountStoredInCloud.DeviceInfo)) && this.HasStored == tempAccountStoredInCloud.HasStored && (!this.HasStored || this.Stored.Equals(tempAccountStoredInCloud.Stored));
		}

		// Token: 0x0600CDFB RID: 52731 RVA: 0x003D672B File Offset: 0x003D492B
		public void Deserialize(Stream stream)
		{
			TempAccountStoredInCloud.Deserialize(stream, this);
		}

		// Token: 0x0600CDFC RID: 52732 RVA: 0x003D6735 File Offset: 0x003D4935
		public static TempAccountStoredInCloud Deserialize(Stream stream, TempAccountStoredInCloud instance)
		{
			return TempAccountStoredInCloud.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CDFD RID: 52733 RVA: 0x003D6740 File Offset: 0x003D4940
		public static TempAccountStoredInCloud DeserializeLengthDelimited(Stream stream)
		{
			TempAccountStoredInCloud tempAccountStoredInCloud = new TempAccountStoredInCloud();
			TempAccountStoredInCloud.DeserializeLengthDelimited(stream, tempAccountStoredInCloud);
			return tempAccountStoredInCloud;
		}

		// Token: 0x0600CDFE RID: 52734 RVA: 0x003D675C File Offset: 0x003D495C
		public static TempAccountStoredInCloud DeserializeLengthDelimited(Stream stream, TempAccountStoredInCloud instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return TempAccountStoredInCloud.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CDFF RID: 52735 RVA: 0x003D6784 File Offset: 0x003D4984
		public static TempAccountStoredInCloud Deserialize(Stream stream, TempAccountStoredInCloud instance, long limit)
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
						instance.Stored = ProtocolParser.ReadBool(stream);
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

		// Token: 0x0600CE00 RID: 52736 RVA: 0x003D6836 File Offset: 0x003D4A36
		public void Serialize(Stream stream)
		{
			TempAccountStoredInCloud.Serialize(stream, this);
		}

		// Token: 0x0600CE01 RID: 52737 RVA: 0x003D6840 File Offset: 0x003D4A40
		public static void Serialize(Stream stream, TempAccountStoredInCloud instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasStored)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Stored);
			}
		}

		// Token: 0x0600CE02 RID: 52738 RVA: 0x003D6898 File Offset: 0x003D4A98
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasStored)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x0400A139 RID: 41273
		public bool HasDeviceInfo;

		// Token: 0x0400A13A RID: 41274
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A13B RID: 41275
		public bool HasStored;

		// Token: 0x0400A13C RID: 41276
		private bool _Stored;
	}
}
