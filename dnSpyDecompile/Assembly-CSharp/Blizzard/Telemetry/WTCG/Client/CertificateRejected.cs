using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x0200120C RID: 4620
	public class CertificateRejected : IProtoBuf
	{
		// Token: 0x0600CF6C RID: 53100 RVA: 0x003DC029 File Offset: 0x003DA229
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x0600CF6D RID: 53101 RVA: 0x003DC036 File Offset: 0x003DA236
		public override bool Equals(object obj)
		{
			return obj is CertificateRejected;
		}

		// Token: 0x0600CF6E RID: 53102 RVA: 0x003DC043 File Offset: 0x003DA243
		public void Deserialize(Stream stream)
		{
			CertificateRejected.Deserialize(stream, this);
		}

		// Token: 0x0600CF6F RID: 53103 RVA: 0x003DC04D File Offset: 0x003DA24D
		public static CertificateRejected Deserialize(Stream stream, CertificateRejected instance)
		{
			return CertificateRejected.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CF70 RID: 53104 RVA: 0x003DC058 File Offset: 0x003DA258
		public static CertificateRejected DeserializeLengthDelimited(Stream stream)
		{
			CertificateRejected certificateRejected = new CertificateRejected();
			CertificateRejected.DeserializeLengthDelimited(stream, certificateRejected);
			return certificateRejected;
		}

		// Token: 0x0600CF71 RID: 53105 RVA: 0x003DC074 File Offset: 0x003DA274
		public static CertificateRejected DeserializeLengthDelimited(Stream stream, CertificateRejected instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CertificateRejected.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CF72 RID: 53106 RVA: 0x003DC09C File Offset: 0x003DA29C
		public static CertificateRejected Deserialize(Stream stream, CertificateRejected instance, long limit)
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

		// Token: 0x0600CF73 RID: 53107 RVA: 0x003DC109 File Offset: 0x003DA309
		public void Serialize(Stream stream)
		{
			CertificateRejected.Serialize(stream, this);
		}

		// Token: 0x0600CF74 RID: 53108 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public static void Serialize(Stream stream, CertificateRejected instance)
		{
		}

		// Token: 0x0600CF75 RID: 53109 RVA: 0x0001FA65 File Offset: 0x0001DC65
		public uint GetSerializedSize()
		{
			return 0U;
		}
	}
}
