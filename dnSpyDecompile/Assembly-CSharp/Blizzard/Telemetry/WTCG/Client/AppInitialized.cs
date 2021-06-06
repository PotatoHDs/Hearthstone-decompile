using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x0200118D RID: 4493
	public class AppInitialized : IProtoBuf
	{
		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x0600C5D5 RID: 50645 RVA: 0x003B8BE8 File Offset: 0x003B6DE8
		// (set) Token: 0x0600C5D6 RID: 50646 RVA: 0x003B8BF0 File Offset: 0x003B6DF0
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

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x0600C5D7 RID: 50647 RVA: 0x003B8C03 File Offset: 0x003B6E03
		// (set) Token: 0x0600C5D8 RID: 50648 RVA: 0x003B8C0B File Offset: 0x003B6E0B
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

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x0600C5D9 RID: 50649 RVA: 0x003B8C1B File Offset: 0x003B6E1B
		// (set) Token: 0x0600C5DA RID: 50650 RVA: 0x003B8C23 File Offset: 0x003B6E23
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

		// Token: 0x0600C5DB RID: 50651 RVA: 0x003B8C38 File Offset: 0x003B6E38
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

		// Token: 0x0600C5DC RID: 50652 RVA: 0x003B8C98 File Offset: 0x003B6E98
		public override bool Equals(object obj)
		{
			AppInitialized appInitialized = obj as AppInitialized;
			return appInitialized != null && this.HasTestType == appInitialized.HasTestType && (!this.HasTestType || this.TestType.Equals(appInitialized.TestType)) && this.HasDuration == appInitialized.HasDuration && (!this.HasDuration || this.Duration.Equals(appInitialized.Duration)) && this.HasClientChangelist == appInitialized.HasClientChangelist && (!this.HasClientChangelist || this.ClientChangelist.Equals(appInitialized.ClientChangelist));
		}

		// Token: 0x0600C5DD RID: 50653 RVA: 0x003B8D36 File Offset: 0x003B6F36
		public void Deserialize(Stream stream)
		{
			AppInitialized.Deserialize(stream, this);
		}

		// Token: 0x0600C5DE RID: 50654 RVA: 0x003B8D40 File Offset: 0x003B6F40
		public static AppInitialized Deserialize(Stream stream, AppInitialized instance)
		{
			return AppInitialized.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C5DF RID: 50655 RVA: 0x003B8D4C File Offset: 0x003B6F4C
		public static AppInitialized DeserializeLengthDelimited(Stream stream)
		{
			AppInitialized appInitialized = new AppInitialized();
			AppInitialized.DeserializeLengthDelimited(stream, appInitialized);
			return appInitialized;
		}

		// Token: 0x0600C5E0 RID: 50656 RVA: 0x003B8D68 File Offset: 0x003B6F68
		public static AppInitialized DeserializeLengthDelimited(Stream stream, AppInitialized instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AppInitialized.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C5E1 RID: 50657 RVA: 0x003B8D90 File Offset: 0x003B6F90
		public static AppInitialized Deserialize(Stream stream, AppInitialized instance, long limit)
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

		// Token: 0x0600C5E2 RID: 50658 RVA: 0x003B8E45 File Offset: 0x003B7045
		public void Serialize(Stream stream)
		{
			AppInitialized.Serialize(stream, this);
		}

		// Token: 0x0600C5E3 RID: 50659 RVA: 0x003B8E50 File Offset: 0x003B7050
		public static void Serialize(Stream stream, AppInitialized instance)
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

		// Token: 0x0600C5E4 RID: 50660 RVA: 0x003B8ECC File Offset: 0x003B70CC
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

		// Token: 0x04009DC1 RID: 40385
		public bool HasTestType;

		// Token: 0x04009DC2 RID: 40386
		private string _TestType;

		// Token: 0x04009DC3 RID: 40387
		public bool HasDuration;

		// Token: 0x04009DC4 RID: 40388
		private float _Duration;

		// Token: 0x04009DC5 RID: 40389
		public bool HasClientChangelist;

		// Token: 0x04009DC6 RID: 40390
		private string _ClientChangelist;
	}
}
