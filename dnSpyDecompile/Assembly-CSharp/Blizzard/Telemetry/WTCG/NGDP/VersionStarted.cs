using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.NGDP
{
	// Token: 0x0200118A RID: 4490
	public class VersionStarted : IProtoBuf
	{
		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x0600C5A6 RID: 50598 RVA: 0x003B8310 File Offset: 0x003B6510
		// (set) Token: 0x0600C5A7 RID: 50599 RVA: 0x003B8318 File Offset: 0x003B6518
		public int Dummy
		{
			get
			{
				return this._Dummy;
			}
			set
			{
				this._Dummy = value;
				this.HasDummy = true;
			}
		}

		// Token: 0x0600C5A8 RID: 50600 RVA: 0x003B8328 File Offset: 0x003B6528
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDummy)
			{
				num ^= this.Dummy.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C5A9 RID: 50601 RVA: 0x003B835C File Offset: 0x003B655C
		public override bool Equals(object obj)
		{
			VersionStarted versionStarted = obj as VersionStarted;
			return versionStarted != null && this.HasDummy == versionStarted.HasDummy && (!this.HasDummy || this.Dummy.Equals(versionStarted.Dummy));
		}

		// Token: 0x0600C5AA RID: 50602 RVA: 0x003B83A4 File Offset: 0x003B65A4
		public void Deserialize(Stream stream)
		{
			VersionStarted.Deserialize(stream, this);
		}

		// Token: 0x0600C5AB RID: 50603 RVA: 0x003B83AE File Offset: 0x003B65AE
		public static VersionStarted Deserialize(Stream stream, VersionStarted instance)
		{
			return VersionStarted.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C5AC RID: 50604 RVA: 0x003B83BC File Offset: 0x003B65BC
		public static VersionStarted DeserializeLengthDelimited(Stream stream)
		{
			VersionStarted versionStarted = new VersionStarted();
			VersionStarted.DeserializeLengthDelimited(stream, versionStarted);
			return versionStarted;
		}

		// Token: 0x0600C5AD RID: 50605 RVA: 0x003B83D8 File Offset: 0x003B65D8
		public static VersionStarted DeserializeLengthDelimited(Stream stream, VersionStarted instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return VersionStarted.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C5AE RID: 50606 RVA: 0x003B8400 File Offset: 0x003B6600
		public static VersionStarted Deserialize(Stream stream, VersionStarted instance, long limit)
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
				else if (num == 8)
				{
					instance.Dummy = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600C5AF RID: 50607 RVA: 0x003B8480 File Offset: 0x003B6680
		public void Serialize(Stream stream)
		{
			VersionStarted.Serialize(stream, this);
		}

		// Token: 0x0600C5B0 RID: 50608 RVA: 0x003B8489 File Offset: 0x003B6689
		public static void Serialize(Stream stream, VersionStarted instance)
		{
			if (instance.HasDummy)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Dummy));
			}
		}

		// Token: 0x0600C5B1 RID: 50609 RVA: 0x003B84A8 File Offset: 0x003B66A8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDummy)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Dummy));
			}
			return num;
		}

		// Token: 0x04009DB3 RID: 40371
		public bool HasDummy;

		// Token: 0x04009DB4 RID: 40372
		private int _Dummy;
	}
}
