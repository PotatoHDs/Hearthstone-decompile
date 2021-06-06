using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011D9 RID: 4569
	public class ManaFilterToggleOff : IProtoBuf
	{
		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x0600CB9F RID: 52127 RVA: 0x003CE129 File Offset: 0x003CC329
		// (set) Token: 0x0600CBA0 RID: 52128 RVA: 0x003CE131 File Offset: 0x003CC331
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

		// Token: 0x0600CBA1 RID: 52129 RVA: 0x003CE144 File Offset: 0x003CC344
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CBA2 RID: 52130 RVA: 0x003CE174 File Offset: 0x003CC374
		public override bool Equals(object obj)
		{
			ManaFilterToggleOff manaFilterToggleOff = obj as ManaFilterToggleOff;
			return manaFilterToggleOff != null && this.HasDeviceInfo == manaFilterToggleOff.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(manaFilterToggleOff.DeviceInfo));
		}

		// Token: 0x0600CBA3 RID: 52131 RVA: 0x003CE1B9 File Offset: 0x003CC3B9
		public void Deserialize(Stream stream)
		{
			ManaFilterToggleOff.Deserialize(stream, this);
		}

		// Token: 0x0600CBA4 RID: 52132 RVA: 0x003CE1C3 File Offset: 0x003CC3C3
		public static ManaFilterToggleOff Deserialize(Stream stream, ManaFilterToggleOff instance)
		{
			return ManaFilterToggleOff.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CBA5 RID: 52133 RVA: 0x003CE1D0 File Offset: 0x003CC3D0
		public static ManaFilterToggleOff DeserializeLengthDelimited(Stream stream)
		{
			ManaFilterToggleOff manaFilterToggleOff = new ManaFilterToggleOff();
			ManaFilterToggleOff.DeserializeLengthDelimited(stream, manaFilterToggleOff);
			return manaFilterToggleOff;
		}

		// Token: 0x0600CBA6 RID: 52134 RVA: 0x003CE1EC File Offset: 0x003CC3EC
		public static ManaFilterToggleOff DeserializeLengthDelimited(Stream stream, ManaFilterToggleOff instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ManaFilterToggleOff.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CBA7 RID: 52135 RVA: 0x003CE214 File Offset: 0x003CC414
		public static ManaFilterToggleOff Deserialize(Stream stream, ManaFilterToggleOff instance, long limit)
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

		// Token: 0x0600CBA8 RID: 52136 RVA: 0x003CE2AE File Offset: 0x003CC4AE
		public void Serialize(Stream stream)
		{
			ManaFilterToggleOff.Serialize(stream, this);
		}

		// Token: 0x0600CBA9 RID: 52137 RVA: 0x003CE2B7 File Offset: 0x003CC4B7
		public static void Serialize(Stream stream, ManaFilterToggleOff instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
		}

		// Token: 0x0600CBAA RID: 52138 RVA: 0x003CE2E8 File Offset: 0x003CC4E8
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

		// Token: 0x0400A045 RID: 41029
		public bool HasDeviceInfo;

		// Token: 0x0400A046 RID: 41030
		private DeviceInfo _DeviceInfo;
	}
}
