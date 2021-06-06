using System;
using System.IO;

namespace bnet.protocol.authentication.v1
{
	// Token: 0x020004F5 RID: 1269
	public class VersionInfoNotification : IProtoBuf
	{
		// Token: 0x1700110B RID: 4363
		// (get) Token: 0x06005A2E RID: 23086 RVA: 0x00113918 File Offset: 0x00111B18
		// (set) Token: 0x06005A2F RID: 23087 RVA: 0x00113920 File Offset: 0x00111B20
		public VersionInfo VersionInfo
		{
			get
			{
				return this._VersionInfo;
			}
			set
			{
				this._VersionInfo = value;
				this.HasVersionInfo = (value != null);
			}
		}

		// Token: 0x06005A30 RID: 23088 RVA: 0x00113933 File Offset: 0x00111B33
		public void SetVersionInfo(VersionInfo val)
		{
			this.VersionInfo = val;
		}

		// Token: 0x06005A31 RID: 23089 RVA: 0x0011393C File Offset: 0x00111B3C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasVersionInfo)
			{
				num ^= this.VersionInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005A32 RID: 23090 RVA: 0x0011396C File Offset: 0x00111B6C
		public override bool Equals(object obj)
		{
			VersionInfoNotification versionInfoNotification = obj as VersionInfoNotification;
			return versionInfoNotification != null && this.HasVersionInfo == versionInfoNotification.HasVersionInfo && (!this.HasVersionInfo || this.VersionInfo.Equals(versionInfoNotification.VersionInfo));
		}

		// Token: 0x1700110C RID: 4364
		// (get) Token: 0x06005A33 RID: 23091 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005A34 RID: 23092 RVA: 0x001139B1 File Offset: 0x00111BB1
		public static VersionInfoNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<VersionInfoNotification>(bs, 0, -1);
		}

		// Token: 0x06005A35 RID: 23093 RVA: 0x001139BB File Offset: 0x00111BBB
		public void Deserialize(Stream stream)
		{
			VersionInfoNotification.Deserialize(stream, this);
		}

		// Token: 0x06005A36 RID: 23094 RVA: 0x001139C5 File Offset: 0x00111BC5
		public static VersionInfoNotification Deserialize(Stream stream, VersionInfoNotification instance)
		{
			return VersionInfoNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005A37 RID: 23095 RVA: 0x001139D0 File Offset: 0x00111BD0
		public static VersionInfoNotification DeserializeLengthDelimited(Stream stream)
		{
			VersionInfoNotification versionInfoNotification = new VersionInfoNotification();
			VersionInfoNotification.DeserializeLengthDelimited(stream, versionInfoNotification);
			return versionInfoNotification;
		}

		// Token: 0x06005A38 RID: 23096 RVA: 0x001139EC File Offset: 0x00111BEC
		public static VersionInfoNotification DeserializeLengthDelimited(Stream stream, VersionInfoNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return VersionInfoNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06005A39 RID: 23097 RVA: 0x00113A14 File Offset: 0x00111C14
		public static VersionInfoNotification Deserialize(Stream stream, VersionInfoNotification instance, long limit)
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
					if (instance.VersionInfo == null)
					{
						instance.VersionInfo = VersionInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						VersionInfo.DeserializeLengthDelimited(stream, instance.VersionInfo);
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

		// Token: 0x06005A3A RID: 23098 RVA: 0x00113AAE File Offset: 0x00111CAE
		public void Serialize(Stream stream)
		{
			VersionInfoNotification.Serialize(stream, this);
		}

		// Token: 0x06005A3B RID: 23099 RVA: 0x00113AB7 File Offset: 0x00111CB7
		public static void Serialize(Stream stream, VersionInfoNotification instance)
		{
			if (instance.HasVersionInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.VersionInfo.GetSerializedSize());
				VersionInfo.Serialize(stream, instance.VersionInfo);
			}
		}

		// Token: 0x06005A3C RID: 23100 RVA: 0x00113AE8 File Offset: 0x00111CE8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasVersionInfo)
			{
				num += 1U;
				uint serializedSize = this.VersionInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001C1D RID: 7197
		public bool HasVersionInfo;

		// Token: 0x04001C1E RID: 7198
		private VersionInfo _VersionInfo;
	}
}
