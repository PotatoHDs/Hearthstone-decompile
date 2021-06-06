using System;
using System.IO;
using System.Text;

namespace bnet.protocol
{
	// Token: 0x020002B4 RID: 692
	public class FanoutTarget : IProtoBuf
	{
		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06002855 RID: 10325 RVA: 0x0008E551 File Offset: 0x0008C751
		// (set) Token: 0x06002856 RID: 10326 RVA: 0x0008E559 File Offset: 0x0008C759
		public string ClientId
		{
			get
			{
				return this._ClientId;
			}
			set
			{
				this._ClientId = value;
				this.HasClientId = (value != null);
			}
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x0008E56C File Offset: 0x0008C76C
		public void SetClientId(string val)
		{
			this.ClientId = val;
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06002858 RID: 10328 RVA: 0x0008E575 File Offset: 0x0008C775
		// (set) Token: 0x06002859 RID: 10329 RVA: 0x0008E57D File Offset: 0x0008C77D
		public byte[] Key
		{
			get
			{
				return this._Key;
			}
			set
			{
				this._Key = value;
				this.HasKey = (value != null);
			}
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x0008E590 File Offset: 0x0008C790
		public void SetKey(byte[] val)
		{
			this.Key = val;
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x0008E59C File Offset: 0x0008C79C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasClientId)
			{
				num ^= this.ClientId.GetHashCode();
			}
			if (this.HasKey)
			{
				num ^= this.Key.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x0008E5E4 File Offset: 0x0008C7E4
		public override bool Equals(object obj)
		{
			FanoutTarget fanoutTarget = obj as FanoutTarget;
			return fanoutTarget != null && this.HasClientId == fanoutTarget.HasClientId && (!this.HasClientId || this.ClientId.Equals(fanoutTarget.ClientId)) && this.HasKey == fanoutTarget.HasKey && (!this.HasKey || this.Key.Equals(fanoutTarget.Key));
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x0600285D RID: 10333 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x0008E654 File Offset: 0x0008C854
		public static FanoutTarget ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FanoutTarget>(bs, 0, -1);
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x0008E65E File Offset: 0x0008C85E
		public void Deserialize(Stream stream)
		{
			FanoutTarget.Deserialize(stream, this);
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x0008E668 File Offset: 0x0008C868
		public static FanoutTarget Deserialize(Stream stream, FanoutTarget instance)
		{
			return FanoutTarget.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002861 RID: 10337 RVA: 0x0008E674 File Offset: 0x0008C874
		public static FanoutTarget DeserializeLengthDelimited(Stream stream)
		{
			FanoutTarget fanoutTarget = new FanoutTarget();
			FanoutTarget.DeserializeLengthDelimited(stream, fanoutTarget);
			return fanoutTarget;
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x0008E690 File Offset: 0x0008C890
		public static FanoutTarget DeserializeLengthDelimited(Stream stream, FanoutTarget instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FanoutTarget.Deserialize(stream, instance, num);
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x0008E6B8 File Offset: 0x0008C8B8
		public static FanoutTarget Deserialize(Stream stream, FanoutTarget instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else
					{
						instance.Key = ProtocolParser.ReadBytes(stream);
					}
				}
				else
				{
					instance.ClientId = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x0008E750 File Offset: 0x0008C950
		public void Serialize(Stream stream)
		{
			FanoutTarget.Serialize(stream, this);
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x0008E75C File Offset: 0x0008C95C
		public static void Serialize(Stream stream, FanoutTarget instance)
		{
			if (instance.HasClientId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientId));
			}
			if (instance.HasKey)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, instance.Key);
			}
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x0008E7AC File Offset: 0x0008C9AC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasClientId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ClientId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasKey)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Key.Length) + (uint)this.Key.Length;
			}
			return num;
		}

		// Token: 0x0400115D RID: 4445
		public bool HasClientId;

		// Token: 0x0400115E RID: 4446
		private string _ClientId;

		// Token: 0x0400115F RID: 4447
		public bool HasKey;

		// Token: 0x04001160 RID: 4448
		private byte[] _Key;
	}
}
