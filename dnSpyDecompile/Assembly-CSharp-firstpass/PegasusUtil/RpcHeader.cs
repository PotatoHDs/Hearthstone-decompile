using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000116 RID: 278
	public class RpcHeader : IProtoBuf
	{
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001266 RID: 4710 RVA: 0x00040B35 File Offset: 0x0003ED35
		// (set) Token: 0x06001267 RID: 4711 RVA: 0x00040B3D File Offset: 0x0003ED3D
		public ulong Type { get; set; }

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001268 RID: 4712 RVA: 0x00040B46 File Offset: 0x0003ED46
		// (set) Token: 0x06001269 RID: 4713 RVA: 0x00040B4E File Offset: 0x0003ED4E
		public ulong RetryCount
		{
			get
			{
				return this._RetryCount;
			}
			set
			{
				this._RetryCount = value;
				this.HasRetryCount = true;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x00040B5E File Offset: 0x0003ED5E
		// (set) Token: 0x0600126B RID: 4715 RVA: 0x00040B66 File Offset: 0x0003ED66
		public ulong RequestNotHandledCount
		{
			get
			{
				return this._RequestNotHandledCount;
			}
			set
			{
				this._RequestNotHandledCount = value;
				this.HasRequestNotHandledCount = true;
			}
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00040B78 File Offset: 0x0003ED78
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Type.GetHashCode();
			if (this.HasRetryCount)
			{
				num ^= this.RetryCount.GetHashCode();
			}
			if (this.HasRequestNotHandledCount)
			{
				num ^= this.RequestNotHandledCount.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00040BD8 File Offset: 0x0003EDD8
		public override bool Equals(object obj)
		{
			RpcHeader rpcHeader = obj as RpcHeader;
			return rpcHeader != null && this.Type.Equals(rpcHeader.Type) && this.HasRetryCount == rpcHeader.HasRetryCount && (!this.HasRetryCount || this.RetryCount.Equals(rpcHeader.RetryCount)) && this.HasRequestNotHandledCount == rpcHeader.HasRequestNotHandledCount && (!this.HasRequestNotHandledCount || this.RequestNotHandledCount.Equals(rpcHeader.RequestNotHandledCount));
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00040C66 File Offset: 0x0003EE66
		public void Deserialize(Stream stream)
		{
			RpcHeader.Deserialize(stream, this);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00040C70 File Offset: 0x0003EE70
		public static RpcHeader Deserialize(Stream stream, RpcHeader instance)
		{
			return RpcHeader.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x00040C7C File Offset: 0x0003EE7C
		public static RpcHeader DeserializeLengthDelimited(Stream stream)
		{
			RpcHeader rpcHeader = new RpcHeader();
			RpcHeader.DeserializeLengthDelimited(stream, rpcHeader);
			return rpcHeader;
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x00040C98 File Offset: 0x0003EE98
		public static RpcHeader DeserializeLengthDelimited(Stream stream, RpcHeader instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RpcHeader.Deserialize(stream, instance, num);
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x00040CC0 File Offset: 0x0003EEC0
		public static RpcHeader Deserialize(Stream stream, RpcHeader instance, long limit)
		{
			instance.RetryCount = 0UL;
			instance.RequestNotHandledCount = 0UL;
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
				else if (num != 8)
				{
					if (num != 16)
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
							instance.RequestNotHandledCount = ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.RetryCount = ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Type = ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x00040D7D File Offset: 0x0003EF7D
		public void Serialize(Stream stream)
		{
			RpcHeader.Serialize(stream, this);
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x00040D88 File Offset: 0x0003EF88
		public static void Serialize(Stream stream, RpcHeader instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.Type);
			if (instance.HasRetryCount)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.RetryCount);
			}
			if (instance.HasRequestNotHandledCount)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.RequestNotHandledCount);
			}
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x00040DE0 File Offset: 0x0003EFE0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64(this.Type);
			if (this.HasRetryCount)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.RetryCount);
			}
			if (this.HasRequestNotHandledCount)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.RequestNotHandledCount);
			}
			return num + 1U;
		}

		// Token: 0x040005AF RID: 1455
		public bool HasRetryCount;

		// Token: 0x040005B0 RID: 1456
		private ulong _RetryCount;

		// Token: 0x040005B1 RID: 1457
		public bool HasRequestNotHandledCount;

		// Token: 0x040005B2 RID: 1458
		private ulong _RequestNotHandledCount;
	}
}
