using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.NGDP
{
	// Token: 0x02001183 RID: 4483
	public class UncaughtException : IProtoBuf
	{
		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x0600C52B RID: 50475 RVA: 0x003B6A74 File Offset: 0x003B4C74
		// (set) Token: 0x0600C52C RID: 50476 RVA: 0x003B6A7C File Offset: 0x003B4C7C
		public string StackTrace
		{
			get
			{
				return this._StackTrace;
			}
			set
			{
				this._StackTrace = value;
				this.HasStackTrace = (value != null);
			}
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x0600C52D RID: 50477 RVA: 0x003B6A8F File Offset: 0x003B4C8F
		// (set) Token: 0x0600C52E RID: 50478 RVA: 0x003B6A97 File Offset: 0x003B4C97
		public string AndroidModel
		{
			get
			{
				return this._AndroidModel;
			}
			set
			{
				this._AndroidModel = value;
				this.HasAndroidModel = (value != null);
			}
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x0600C52F RID: 50479 RVA: 0x003B6AAA File Offset: 0x003B4CAA
		// (set) Token: 0x0600C530 RID: 50480 RVA: 0x003B6AB2 File Offset: 0x003B4CB2
		public uint AndroidSdkVersion
		{
			get
			{
				return this._AndroidSdkVersion;
			}
			set
			{
				this._AndroidSdkVersion = value;
				this.HasAndroidSdkVersion = true;
			}
		}

		// Token: 0x0600C531 RID: 50481 RVA: 0x003B6AC4 File Offset: 0x003B4CC4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasStackTrace)
			{
				num ^= this.StackTrace.GetHashCode();
			}
			if (this.HasAndroidModel)
			{
				num ^= this.AndroidModel.GetHashCode();
			}
			if (this.HasAndroidSdkVersion)
			{
				num ^= this.AndroidSdkVersion.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C532 RID: 50482 RVA: 0x003B6B24 File Offset: 0x003B4D24
		public override bool Equals(object obj)
		{
			UncaughtException ex = obj as UncaughtException;
			return ex != null && this.HasStackTrace == ex.HasStackTrace && (!this.HasStackTrace || this.StackTrace.Equals(ex.StackTrace)) && this.HasAndroidModel == ex.HasAndroidModel && (!this.HasAndroidModel || this.AndroidModel.Equals(ex.AndroidModel)) && this.HasAndroidSdkVersion == ex.HasAndroidSdkVersion && (!this.HasAndroidSdkVersion || this.AndroidSdkVersion.Equals(ex.AndroidSdkVersion));
		}

		// Token: 0x0600C533 RID: 50483 RVA: 0x003B6BC2 File Offset: 0x003B4DC2
		public void Deserialize(Stream stream)
		{
			UncaughtException.Deserialize(stream, this);
		}

		// Token: 0x0600C534 RID: 50484 RVA: 0x003B6BCC File Offset: 0x003B4DCC
		public static UncaughtException Deserialize(Stream stream, UncaughtException instance)
		{
			return UncaughtException.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C535 RID: 50485 RVA: 0x003B6BD8 File Offset: 0x003B4DD8
		public static UncaughtException DeserializeLengthDelimited(Stream stream)
		{
			UncaughtException ex = new UncaughtException();
			UncaughtException.DeserializeLengthDelimited(stream, ex);
			return ex;
		}

		// Token: 0x0600C536 RID: 50486 RVA: 0x003B6BF4 File Offset: 0x003B4DF4
		public static UncaughtException DeserializeLengthDelimited(Stream stream, UncaughtException instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UncaughtException.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C537 RID: 50487 RVA: 0x003B6C1C File Offset: 0x003B4E1C
		public static UncaughtException Deserialize(Stream stream, UncaughtException instance, long limit)
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
					if (num != 18)
					{
						if (num != 24)
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
							instance.AndroidSdkVersion = ProtocolParser.ReadUInt32(stream);
						}
					}
					else
					{
						instance.AndroidModel = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.StackTrace = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600C538 RID: 50488 RVA: 0x003B6CCA File Offset: 0x003B4ECA
		public void Serialize(Stream stream)
		{
			UncaughtException.Serialize(stream, this);
		}

		// Token: 0x0600C539 RID: 50489 RVA: 0x003B6CD4 File Offset: 0x003B4ED4
		public static void Serialize(Stream stream, UncaughtException instance)
		{
			if (instance.HasStackTrace)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.StackTrace));
			}
			if (instance.HasAndroidModel)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AndroidModel));
			}
			if (instance.HasAndroidSdkVersion)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.AndroidSdkVersion);
			}
		}

		// Token: 0x0600C53A RID: 50490 RVA: 0x003B6D4C File Offset: 0x003B4F4C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasStackTrace)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.StackTrace);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasAndroidModel)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.AndroidModel);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasAndroidSdkVersion)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.AndroidSdkVersion);
			}
			return num;
		}

		// Token: 0x04009D85 RID: 40325
		public bool HasStackTrace;

		// Token: 0x04009D86 RID: 40326
		private string _StackTrace;

		// Token: 0x04009D87 RID: 40327
		public bool HasAndroidModel;

		// Token: 0x04009D88 RID: 40328
		private string _AndroidModel;

		// Token: 0x04009D89 RID: 40329
		public bool HasAndroidSdkVersion;

		// Token: 0x04009D8A RID: 40330
		private uint _AndroidSdkVersion;
	}
}
