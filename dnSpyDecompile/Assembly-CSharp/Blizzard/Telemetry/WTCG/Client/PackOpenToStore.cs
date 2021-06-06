using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011E2 RID: 4578
	public class PackOpenToStore : IProtoBuf
	{
		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x0600CC40 RID: 52288 RVA: 0x003D03FE File Offset: 0x003CE5FE
		// (set) Token: 0x0600CC41 RID: 52289 RVA: 0x003D0406 File Offset: 0x003CE606
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

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x0600CC42 RID: 52290 RVA: 0x003D0419 File Offset: 0x003CE619
		// (set) Token: 0x0600CC43 RID: 52291 RVA: 0x003D0421 File Offset: 0x003CE621
		public PackOpenToStore.Path Path_
		{
			get
			{
				return this._Path_;
			}
			set
			{
				this._Path_ = value;
				this.HasPath_ = true;
			}
		}

		// Token: 0x0600CC44 RID: 52292 RVA: 0x003D0434 File Offset: 0x003CE634
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasPath_)
			{
				num ^= this.Path_.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CC45 RID: 52293 RVA: 0x003D0484 File Offset: 0x003CE684
		public override bool Equals(object obj)
		{
			PackOpenToStore packOpenToStore = obj as PackOpenToStore;
			return packOpenToStore != null && this.HasDeviceInfo == packOpenToStore.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(packOpenToStore.DeviceInfo)) && this.HasPath_ == packOpenToStore.HasPath_ && (!this.HasPath_ || this.Path_.Equals(packOpenToStore.Path_));
		}

		// Token: 0x0600CC46 RID: 52294 RVA: 0x003D0502 File Offset: 0x003CE702
		public void Deserialize(Stream stream)
		{
			PackOpenToStore.Deserialize(stream, this);
		}

		// Token: 0x0600CC47 RID: 52295 RVA: 0x003D050C File Offset: 0x003CE70C
		public static PackOpenToStore Deserialize(Stream stream, PackOpenToStore instance)
		{
			return PackOpenToStore.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CC48 RID: 52296 RVA: 0x003D0518 File Offset: 0x003CE718
		public static PackOpenToStore DeserializeLengthDelimited(Stream stream)
		{
			PackOpenToStore packOpenToStore = new PackOpenToStore();
			PackOpenToStore.DeserializeLengthDelimited(stream, packOpenToStore);
			return packOpenToStore;
		}

		// Token: 0x0600CC49 RID: 52297 RVA: 0x003D0534 File Offset: 0x003CE734
		public static PackOpenToStore DeserializeLengthDelimited(Stream stream, PackOpenToStore instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PackOpenToStore.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CC4A RID: 52298 RVA: 0x003D055C File Offset: 0x003CE75C
		public static PackOpenToStore Deserialize(Stream stream, PackOpenToStore instance, long limit)
		{
			instance.Path_ = PackOpenToStore.Path.PACK_OPENING_BUTTON;
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
						instance.Path_ = (PackOpenToStore.Path)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600CC4B RID: 52299 RVA: 0x003D0616 File Offset: 0x003CE816
		public void Serialize(Stream stream)
		{
			PackOpenToStore.Serialize(stream, this);
		}

		// Token: 0x0600CC4C RID: 52300 RVA: 0x003D0620 File Offset: 0x003CE820
		public static void Serialize(Stream stream, PackOpenToStore instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasPath_)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Path_));
			}
		}

		// Token: 0x0600CC4D RID: 52301 RVA: 0x003D0678 File Offset: 0x003CE878
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasPath_)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Path_));
			}
			return num;
		}

		// Token: 0x0400A083 RID: 41091
		public bool HasDeviceInfo;

		// Token: 0x0400A084 RID: 41092
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A085 RID: 41093
		public bool HasPath_;

		// Token: 0x0400A086 RID: 41094
		private PackOpenToStore.Path _Path_;

		// Token: 0x0200294A RID: 10570
		public enum Path
		{
			// Token: 0x0400FC84 RID: 64644
			PACK_OPENING_BUTTON = 1,
			// Token: 0x0400FC85 RID: 64645
			BACK_TO_BOX
		}
	}
}
