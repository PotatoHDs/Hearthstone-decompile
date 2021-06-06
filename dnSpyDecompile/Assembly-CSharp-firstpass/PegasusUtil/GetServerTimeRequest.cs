using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000F0 RID: 240
	public class GetServerTimeRequest : IProtoBuf
	{
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06001010 RID: 4112 RVA: 0x000394AD File Offset: 0x000376AD
		// (set) Token: 0x06001011 RID: 4113 RVA: 0x000394B5 File Offset: 0x000376B5
		public long ClientUnixTime
		{
			get
			{
				return this._ClientUnixTime;
			}
			set
			{
				this._ClientUnixTime = value;
				this.HasClientUnixTime = true;
			}
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x000394C8 File Offset: 0x000376C8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasClientUnixTime)
			{
				num ^= this.ClientUnixTime.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x000394FC File Offset: 0x000376FC
		public override bool Equals(object obj)
		{
			GetServerTimeRequest getServerTimeRequest = obj as GetServerTimeRequest;
			return getServerTimeRequest != null && this.HasClientUnixTime == getServerTimeRequest.HasClientUnixTime && (!this.HasClientUnixTime || this.ClientUnixTime.Equals(getServerTimeRequest.ClientUnixTime));
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x00039544 File Offset: 0x00037744
		public void Deserialize(Stream stream)
		{
			GetServerTimeRequest.Deserialize(stream, this);
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0003954E File Offset: 0x0003774E
		public static GetServerTimeRequest Deserialize(Stream stream, GetServerTimeRequest instance)
		{
			return GetServerTimeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0003955C File Offset: 0x0003775C
		public static GetServerTimeRequest DeserializeLengthDelimited(Stream stream)
		{
			GetServerTimeRequest getServerTimeRequest = new GetServerTimeRequest();
			GetServerTimeRequest.DeserializeLengthDelimited(stream, getServerTimeRequest);
			return getServerTimeRequest;
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x00039578 File Offset: 0x00037778
		public static GetServerTimeRequest DeserializeLengthDelimited(Stream stream, GetServerTimeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetServerTimeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x000395A0 File Offset: 0x000377A0
		public static GetServerTimeRequest Deserialize(Stream stream, GetServerTimeRequest instance, long limit)
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
					instance.ClientUnixTime = (long)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06001019 RID: 4121 RVA: 0x0003961F File Offset: 0x0003781F
		public void Serialize(Stream stream)
		{
			GetServerTimeRequest.Serialize(stream, this);
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x00039628 File Offset: 0x00037828
		public static void Serialize(Stream stream, GetServerTimeRequest instance)
		{
			if (instance.HasClientUnixTime)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ClientUnixTime);
			}
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x00039648 File Offset: 0x00037848
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasClientUnixTime)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.ClientUnixTime);
			}
			return num;
		}

		// Token: 0x04000516 RID: 1302
		public bool HasClientUnixTime;

		// Token: 0x04000517 RID: 1303
		private long _ClientUnixTime;

		// Token: 0x020005F4 RID: 1524
		public enum PacketID
		{
			// Token: 0x0400201C RID: 8220
			ID = 364,
			// Token: 0x0400201D RID: 8221
			System = 0
		}
	}
}
