using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011AA RID: 4522
	public class Cinematic : IProtoBuf
	{
		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x0600C820 RID: 51232 RVA: 0x003C19DF File Offset: 0x003BFBDF
		// (set) Token: 0x0600C821 RID: 51233 RVA: 0x003C19E7 File Offset: 0x003BFBE7
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

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x0600C822 RID: 51234 RVA: 0x003C19FA File Offset: 0x003BFBFA
		// (set) Token: 0x0600C823 RID: 51235 RVA: 0x003C1A02 File Offset: 0x003BFC02
		public bool Begin
		{
			get
			{
				return this._Begin;
			}
			set
			{
				this._Begin = value;
				this.HasBegin = true;
			}
		}

		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x0600C824 RID: 51236 RVA: 0x003C1A12 File Offset: 0x003BFC12
		// (set) Token: 0x0600C825 RID: 51237 RVA: 0x003C1A1A File Offset: 0x003BFC1A
		public float Duration
		{
			get
			{
				return this._Duration;
			}
			set
			{
				this._Duration = value;
				this.HasDuration = true;
			}
		}

		// Token: 0x0600C826 RID: 51238 RVA: 0x003C1A2C File Offset: 0x003BFC2C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasBegin)
			{
				num ^= this.Begin.GetHashCode();
			}
			if (this.HasDuration)
			{
				num ^= this.Duration.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C827 RID: 51239 RVA: 0x003C1A90 File Offset: 0x003BFC90
		public override bool Equals(object obj)
		{
			Cinematic cinematic = obj as Cinematic;
			return cinematic != null && this.HasDeviceInfo == cinematic.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(cinematic.DeviceInfo)) && this.HasBegin == cinematic.HasBegin && (!this.HasBegin || this.Begin.Equals(cinematic.Begin)) && this.HasDuration == cinematic.HasDuration && (!this.HasDuration || this.Duration.Equals(cinematic.Duration));
		}

		// Token: 0x0600C828 RID: 51240 RVA: 0x003C1B31 File Offset: 0x003BFD31
		public void Deserialize(Stream stream)
		{
			Cinematic.Deserialize(stream, this);
		}

		// Token: 0x0600C829 RID: 51241 RVA: 0x003C1B3B File Offset: 0x003BFD3B
		public static Cinematic Deserialize(Stream stream, Cinematic instance)
		{
			return Cinematic.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C82A RID: 51242 RVA: 0x003C1B48 File Offset: 0x003BFD48
		public static Cinematic DeserializeLengthDelimited(Stream stream)
		{
			Cinematic cinematic = new Cinematic();
			Cinematic.DeserializeLengthDelimited(stream, cinematic);
			return cinematic;
		}

		// Token: 0x0600C82B RID: 51243 RVA: 0x003C1B64 File Offset: 0x003BFD64
		public static Cinematic DeserializeLengthDelimited(Stream stream, Cinematic instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Cinematic.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C82C RID: 51244 RVA: 0x003C1B8C File Offset: 0x003BFD8C
		public static Cinematic Deserialize(Stream stream, Cinematic instance, long limit)
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
					if (num != 16)
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
							instance.Duration = binaryReader.ReadSingle();
						}
					}
					else
					{
						instance.Begin = ProtocolParser.ReadBool(stream);
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

		// Token: 0x0600C82D RID: 51245 RVA: 0x003C1C61 File Offset: 0x003BFE61
		public void Serialize(Stream stream)
		{
			Cinematic.Serialize(stream, this);
		}

		// Token: 0x0600C82E RID: 51246 RVA: 0x003C1C6C File Offset: 0x003BFE6C
		public static void Serialize(Stream stream, Cinematic instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasBegin)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Begin);
			}
			if (instance.HasDuration)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.Duration);
			}
		}

		// Token: 0x0600C82F RID: 51247 RVA: 0x003C1CE8 File Offset: 0x003BFEE8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasBegin)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasDuration)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x04009ECB RID: 40651
		public bool HasDeviceInfo;

		// Token: 0x04009ECC RID: 40652
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009ECD RID: 40653
		public bool HasBegin;

		// Token: 0x04009ECE RID: 40654
		private bool _Begin;

		// Token: 0x04009ECF RID: 40655
		public bool HasDuration;

		// Token: 0x04009ED0 RID: 40656
		private float _Duration;
	}
}
