using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x0200118F RID: 4495
	public class AppStart : IProtoBuf
	{
		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x0600C5F5 RID: 50677 RVA: 0x003B91AC File Offset: 0x003B73AC
		// (set) Token: 0x0600C5F6 RID: 50678 RVA: 0x003B91B4 File Offset: 0x003B73B4
		public string TestType
		{
			get
			{
				return this._TestType;
			}
			set
			{
				this._TestType = value;
				this.HasTestType = (value != null);
			}
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x0600C5F7 RID: 50679 RVA: 0x003B91C7 File Offset: 0x003B73C7
		// (set) Token: 0x0600C5F8 RID: 50680 RVA: 0x003B91CF File Offset: 0x003B73CF
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

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x0600C5F9 RID: 50681 RVA: 0x003B91DF File Offset: 0x003B73DF
		// (set) Token: 0x0600C5FA RID: 50682 RVA: 0x003B91E7 File Offset: 0x003B73E7
		public string ClientChangelist
		{
			get
			{
				return this._ClientChangelist;
			}
			set
			{
				this._ClientChangelist = value;
				this.HasClientChangelist = (value != null);
			}
		}

		// Token: 0x0600C5FB RID: 50683 RVA: 0x003B91FC File Offset: 0x003B73FC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasTestType)
			{
				num ^= this.TestType.GetHashCode();
			}
			if (this.HasDuration)
			{
				num ^= this.Duration.GetHashCode();
			}
			if (this.HasClientChangelist)
			{
				num ^= this.ClientChangelist.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C5FC RID: 50684 RVA: 0x003B925C File Offset: 0x003B745C
		public override bool Equals(object obj)
		{
			AppStart appStart = obj as AppStart;
			return appStart != null && this.HasTestType == appStart.HasTestType && (!this.HasTestType || this.TestType.Equals(appStart.TestType)) && this.HasDuration == appStart.HasDuration && (!this.HasDuration || this.Duration.Equals(appStart.Duration)) && this.HasClientChangelist == appStart.HasClientChangelist && (!this.HasClientChangelist || this.ClientChangelist.Equals(appStart.ClientChangelist));
		}

		// Token: 0x0600C5FD RID: 50685 RVA: 0x003B92FA File Offset: 0x003B74FA
		public void Deserialize(Stream stream)
		{
			AppStart.Deserialize(stream, this);
		}

		// Token: 0x0600C5FE RID: 50686 RVA: 0x003B9304 File Offset: 0x003B7504
		public static AppStart Deserialize(Stream stream, AppStart instance)
		{
			return AppStart.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C5FF RID: 50687 RVA: 0x003B9310 File Offset: 0x003B7510
		public static AppStart DeserializeLengthDelimited(Stream stream)
		{
			AppStart appStart = new AppStart();
			AppStart.DeserializeLengthDelimited(stream, appStart);
			return appStart;
		}

		// Token: 0x0600C600 RID: 50688 RVA: 0x003B932C File Offset: 0x003B752C
		public static AppStart DeserializeLengthDelimited(Stream stream, AppStart instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AppStart.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C601 RID: 50689 RVA: 0x003B9354 File Offset: 0x003B7554
		public static AppStart Deserialize(Stream stream, AppStart instance, long limit)
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
					if (num != 21)
					{
						if (num != 26)
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
							instance.ClientChangelist = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.Duration = binaryReader.ReadSingle();
					}
				}
				else
				{
					instance.TestType = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600C602 RID: 50690 RVA: 0x003B9409 File Offset: 0x003B7609
		public void Serialize(Stream stream)
		{
			AppStart.Serialize(stream, this);
		}

		// Token: 0x0600C603 RID: 50691 RVA: 0x003B9414 File Offset: 0x003B7614
		public static void Serialize(Stream stream, AppStart instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasTestType)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TestType));
			}
			if (instance.HasDuration)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Duration);
			}
			if (instance.HasClientChangelist)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientChangelist));
			}
		}

		// Token: 0x0600C604 RID: 50692 RVA: 0x003B9490 File Offset: 0x003B7690
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasTestType)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.TestType);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasDuration)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasClientChangelist)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.ClientChangelist);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x04009DCB RID: 40395
		public bool HasTestType;

		// Token: 0x04009DCC RID: 40396
		private string _TestType;

		// Token: 0x04009DCD RID: 40397
		public bool HasDuration;

		// Token: 0x04009DCE RID: 40398
		private float _Duration;

		// Token: 0x04009DCF RID: 40399
		public bool HasClientChangelist;

		// Token: 0x04009DD0 RID: 40400
		private string _ClientChangelist;
	}
}
