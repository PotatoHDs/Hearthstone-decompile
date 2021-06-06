using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011DE RID: 4574
	public class MissingAssetError : IProtoBuf
	{
		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x0600CBFA RID: 52218 RVA: 0x003CF546 File Offset: 0x003CD746
		// (set) Token: 0x0600CBFB RID: 52219 RVA: 0x003CF54E File Offset: 0x003CD74E
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

		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x0600CBFC RID: 52220 RVA: 0x003CF561 File Offset: 0x003CD761
		// (set) Token: 0x0600CBFD RID: 52221 RVA: 0x003CF569 File Offset: 0x003CD769
		public string MissingAssetPath
		{
			get
			{
				return this._MissingAssetPath;
			}
			set
			{
				this._MissingAssetPath = value;
				this.HasMissingAssetPath = (value != null);
			}
		}

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x0600CBFE RID: 52222 RVA: 0x003CF57C File Offset: 0x003CD77C
		// (set) Token: 0x0600CBFF RID: 52223 RVA: 0x003CF584 File Offset: 0x003CD784
		public string AssetContext
		{
			get
			{
				return this._AssetContext;
			}
			set
			{
				this._AssetContext = value;
				this.HasAssetContext = (value != null);
			}
		}

		// Token: 0x0600CC00 RID: 52224 RVA: 0x003CF598 File Offset: 0x003CD798
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasMissingAssetPath)
			{
				num ^= this.MissingAssetPath.GetHashCode();
			}
			if (this.HasAssetContext)
			{
				num ^= this.AssetContext.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CC01 RID: 52225 RVA: 0x003CF5F4 File Offset: 0x003CD7F4
		public override bool Equals(object obj)
		{
			MissingAssetError missingAssetError = obj as MissingAssetError;
			return missingAssetError != null && this.HasDeviceInfo == missingAssetError.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(missingAssetError.DeviceInfo)) && this.HasMissingAssetPath == missingAssetError.HasMissingAssetPath && (!this.HasMissingAssetPath || this.MissingAssetPath.Equals(missingAssetError.MissingAssetPath)) && this.HasAssetContext == missingAssetError.HasAssetContext && (!this.HasAssetContext || this.AssetContext.Equals(missingAssetError.AssetContext));
		}

		// Token: 0x0600CC02 RID: 52226 RVA: 0x003CF68F File Offset: 0x003CD88F
		public void Deserialize(Stream stream)
		{
			MissingAssetError.Deserialize(stream, this);
		}

		// Token: 0x0600CC03 RID: 52227 RVA: 0x003CF699 File Offset: 0x003CD899
		public static MissingAssetError Deserialize(Stream stream, MissingAssetError instance)
		{
			return MissingAssetError.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CC04 RID: 52228 RVA: 0x003CF6A4 File Offset: 0x003CD8A4
		public static MissingAssetError DeserializeLengthDelimited(Stream stream)
		{
			MissingAssetError missingAssetError = new MissingAssetError();
			MissingAssetError.DeserializeLengthDelimited(stream, missingAssetError);
			return missingAssetError;
		}

		// Token: 0x0600CC05 RID: 52229 RVA: 0x003CF6C0 File Offset: 0x003CD8C0
		public static MissingAssetError DeserializeLengthDelimited(Stream stream, MissingAssetError instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MissingAssetError.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CC06 RID: 52230 RVA: 0x003CF6E8 File Offset: 0x003CD8E8
		public static MissingAssetError Deserialize(Stream stream, MissingAssetError instance, long limit)
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
							instance.AssetContext = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.MissingAssetPath = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600CC07 RID: 52231 RVA: 0x003CF7B6 File Offset: 0x003CD9B6
		public void Serialize(Stream stream)
		{
			MissingAssetError.Serialize(stream, this);
		}

		// Token: 0x0600CC08 RID: 52232 RVA: 0x003CF7C0 File Offset: 0x003CD9C0
		public static void Serialize(Stream stream, MissingAssetError instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasMissingAssetPath)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.MissingAssetPath));
			}
			if (instance.HasAssetContext)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AssetContext));
			}
		}

		// Token: 0x0600CC09 RID: 52233 RVA: 0x003CF848 File Offset: 0x003CDA48
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasMissingAssetPath)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.MissingAssetPath);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasAssetContext)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.AssetContext);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x0400A069 RID: 41065
		public bool HasDeviceInfo;

		// Token: 0x0400A06A RID: 41066
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A06B RID: 41067
		public bool HasMissingAssetPath;

		// Token: 0x0400A06C RID: 41068
		private string _MissingAssetPath;

		// Token: 0x0400A06D RID: 41069
		public bool HasAssetContext;

		// Token: 0x0400A06E RID: 41070
		private string _AssetContext;
	}
}
