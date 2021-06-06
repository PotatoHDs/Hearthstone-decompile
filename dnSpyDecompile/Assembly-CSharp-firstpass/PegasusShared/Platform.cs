using System;
using System.IO;
using System.Text;

namespace PegasusShared
{
	// Token: 0x02000141 RID: 321
	public class Platform : IProtoBuf
	{
		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x00047E21 File Offset: 0x00046021
		// (set) Token: 0x0600150A RID: 5386 RVA: 0x00047E29 File Offset: 0x00046029
		public int Os { get; set; }

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x00047E32 File Offset: 0x00046032
		// (set) Token: 0x0600150C RID: 5388 RVA: 0x00047E3A File Offset: 0x0004603A
		public int Screen { get; set; }

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x0600150D RID: 5389 RVA: 0x00047E43 File Offset: 0x00046043
		// (set) Token: 0x0600150E RID: 5390 RVA: 0x00047E4B File Offset: 0x0004604B
		public string Name { get; set; }

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x00047E54 File Offset: 0x00046054
		// (set) Token: 0x06001510 RID: 5392 RVA: 0x00047E5C File Offset: 0x0004605C
		public int Store
		{
			get
			{
				return this._Store;
			}
			set
			{
				this._Store = value;
				this.HasStore = true;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001511 RID: 5393 RVA: 0x00047E6C File Offset: 0x0004606C
		// (set) Token: 0x06001512 RID: 5394 RVA: 0x00047E74 File Offset: 0x00046074
		public string UniqueDeviceIdentifier
		{
			get
			{
				return this._UniqueDeviceIdentifier;
			}
			set
			{
				this._UniqueDeviceIdentifier = value;
				this.HasUniqueDeviceIdentifier = (value != null);
			}
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x00047E88 File Offset: 0x00046088
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Os.GetHashCode();
			num ^= this.Screen.GetHashCode();
			num ^= this.Name.GetHashCode();
			if (this.HasStore)
			{
				num ^= this.Store.GetHashCode();
			}
			if (this.HasUniqueDeviceIdentifier)
			{
				num ^= this.UniqueDeviceIdentifier.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x00047F04 File Offset: 0x00046104
		public override bool Equals(object obj)
		{
			Platform platform = obj as Platform;
			return platform != null && this.Os.Equals(platform.Os) && this.Screen.Equals(platform.Screen) && this.Name.Equals(platform.Name) && this.HasStore == platform.HasStore && (!this.HasStore || this.Store.Equals(platform.Store)) && this.HasUniqueDeviceIdentifier == platform.HasUniqueDeviceIdentifier && (!this.HasUniqueDeviceIdentifier || this.UniqueDeviceIdentifier.Equals(platform.UniqueDeviceIdentifier));
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x00047FBC File Offset: 0x000461BC
		public void Deserialize(Stream stream)
		{
			Platform.Deserialize(stream, this);
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x00047FC6 File Offset: 0x000461C6
		public static Platform Deserialize(Stream stream, Platform instance)
		{
			return Platform.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x00047FD4 File Offset: 0x000461D4
		public static Platform DeserializeLengthDelimited(Stream stream)
		{
			Platform platform = new Platform();
			Platform.DeserializeLengthDelimited(stream, platform);
			return platform;
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x00047FF0 File Offset: 0x000461F0
		public static Platform DeserializeLengthDelimited(Stream stream, Platform instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Platform.Deserialize(stream, instance, num);
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x00048018 File Offset: 0x00046218
		public static Platform Deserialize(Stream stream, Platform instance, long limit)
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
					if (num <= 16)
					{
						if (num == 8)
						{
							instance.Os = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.Screen = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.Name = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 32)
						{
							instance.Store = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 42)
						{
							instance.UniqueDeviceIdentifier = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600151A RID: 5402 RVA: 0x00048101 File Offset: 0x00046301
		public void Serialize(Stream stream)
		{
			Platform.Serialize(stream, this);
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x0004810C File Offset: 0x0004630C
		public static void Serialize(Stream stream, Platform instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Os));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Screen));
			if (instance.Name == null)
			{
				throw new ArgumentNullException("Name", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			if (instance.HasStore)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Store));
			}
			if (instance.HasUniqueDeviceIdentifier)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.UniqueDeviceIdentifier));
			}
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x000481BC File Offset: 0x000463BC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Os));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Screen));
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (this.HasStore)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Store));
			}
			if (this.HasUniqueDeviceIdentifier)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.UniqueDeviceIdentifier);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num + 3U;
		}

		// Token: 0x04000669 RID: 1641
		public bool HasStore;

		// Token: 0x0400066A RID: 1642
		private int _Store;

		// Token: 0x0400066B RID: 1643
		public bool HasUniqueDeviceIdentifier;

		// Token: 0x0400066C RID: 1644
		private string _UniqueDeviceIdentifier;
	}
}
