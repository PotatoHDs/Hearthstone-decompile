using System;
using System.IO;

namespace bnet.protocol.connection.v1
{
	// Token: 0x0200043C RID: 1084
	public class ConnectRequest : IProtoBuf
	{
		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06004963 RID: 18787 RVA: 0x000E53C6 File Offset: 0x000E35C6
		// (set) Token: 0x06004964 RID: 18788 RVA: 0x000E53CE File Offset: 0x000E35CE
		public ProcessId ClientId
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

		// Token: 0x06004965 RID: 18789 RVA: 0x000E53E1 File Offset: 0x000E35E1
		public void SetClientId(ProcessId val)
		{
			this.ClientId = val;
		}

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06004966 RID: 18790 RVA: 0x000E53EA File Offset: 0x000E35EA
		// (set) Token: 0x06004967 RID: 18791 RVA: 0x000E53F2 File Offset: 0x000E35F2
		public BindRequest BindRequest
		{
			get
			{
				return this._BindRequest;
			}
			set
			{
				this._BindRequest = value;
				this.HasBindRequest = (value != null);
			}
		}

		// Token: 0x06004968 RID: 18792 RVA: 0x000E5405 File Offset: 0x000E3605
		public void SetBindRequest(BindRequest val)
		{
			this.BindRequest = val;
		}

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x06004969 RID: 18793 RVA: 0x000E540E File Offset: 0x000E360E
		// (set) Token: 0x0600496A RID: 18794 RVA: 0x000E5416 File Offset: 0x000E3616
		public bool UseBindlessRpc
		{
			get
			{
				return this._UseBindlessRpc;
			}
			set
			{
				this._UseBindlessRpc = value;
				this.HasUseBindlessRpc = true;
			}
		}

		// Token: 0x0600496B RID: 18795 RVA: 0x000E5426 File Offset: 0x000E3626
		public void SetUseBindlessRpc(bool val)
		{
			this.UseBindlessRpc = val;
		}

		// Token: 0x0600496C RID: 18796 RVA: 0x000E5430 File Offset: 0x000E3630
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasClientId)
			{
				num ^= this.ClientId.GetHashCode();
			}
			if (this.HasBindRequest)
			{
				num ^= this.BindRequest.GetHashCode();
			}
			if (this.HasUseBindlessRpc)
			{
				num ^= this.UseBindlessRpc.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600496D RID: 18797 RVA: 0x000E5490 File Offset: 0x000E3690
		public override bool Equals(object obj)
		{
			ConnectRequest connectRequest = obj as ConnectRequest;
			return connectRequest != null && this.HasClientId == connectRequest.HasClientId && (!this.HasClientId || this.ClientId.Equals(connectRequest.ClientId)) && this.HasBindRequest == connectRequest.HasBindRequest && (!this.HasBindRequest || this.BindRequest.Equals(connectRequest.BindRequest)) && this.HasUseBindlessRpc == connectRequest.HasUseBindlessRpc && (!this.HasUseBindlessRpc || this.UseBindlessRpc.Equals(connectRequest.UseBindlessRpc));
		}

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x0600496E RID: 18798 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600496F RID: 18799 RVA: 0x000E552E File Offset: 0x000E372E
		public static ConnectRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ConnectRequest>(bs, 0, -1);
		}

		// Token: 0x06004970 RID: 18800 RVA: 0x000E5538 File Offset: 0x000E3738
		public void Deserialize(Stream stream)
		{
			ConnectRequest.Deserialize(stream, this);
		}

		// Token: 0x06004971 RID: 18801 RVA: 0x000E5542 File Offset: 0x000E3742
		public static ConnectRequest Deserialize(Stream stream, ConnectRequest instance)
		{
			return ConnectRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004972 RID: 18802 RVA: 0x000E5550 File Offset: 0x000E3750
		public static ConnectRequest DeserializeLengthDelimited(Stream stream)
		{
			ConnectRequest connectRequest = new ConnectRequest();
			ConnectRequest.DeserializeLengthDelimited(stream, connectRequest);
			return connectRequest;
		}

		// Token: 0x06004973 RID: 18803 RVA: 0x000E556C File Offset: 0x000E376C
		public static ConnectRequest DeserializeLengthDelimited(Stream stream, ConnectRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ConnectRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004974 RID: 18804 RVA: 0x000E5594 File Offset: 0x000E3794
		public static ConnectRequest Deserialize(Stream stream, ConnectRequest instance, long limit)
		{
			instance.UseBindlessRpc = true;
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
							instance.UseBindlessRpc = ProtocolParser.ReadBool(stream);
						}
					}
					else if (instance.BindRequest == null)
					{
						instance.BindRequest = BindRequest.DeserializeLengthDelimited(stream);
					}
					else
					{
						BindRequest.DeserializeLengthDelimited(stream, instance.BindRequest);
					}
				}
				else if (instance.ClientId == null)
				{
					instance.ClientId = ProcessId.DeserializeLengthDelimited(stream);
				}
				else
				{
					ProcessId.DeserializeLengthDelimited(stream, instance.ClientId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004975 RID: 18805 RVA: 0x000E5683 File Offset: 0x000E3883
		public void Serialize(Stream stream)
		{
			ConnectRequest.Serialize(stream, this);
		}

		// Token: 0x06004976 RID: 18806 RVA: 0x000E568C File Offset: 0x000E388C
		public static void Serialize(Stream stream, ConnectRequest instance)
		{
			if (instance.HasClientId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ClientId.GetSerializedSize());
				ProcessId.Serialize(stream, instance.ClientId);
			}
			if (instance.HasBindRequest)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.BindRequest.GetSerializedSize());
				BindRequest.Serialize(stream, instance.BindRequest);
			}
			if (instance.HasUseBindlessRpc)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.UseBindlessRpc);
			}
		}

		// Token: 0x06004977 RID: 18807 RVA: 0x000E5710 File Offset: 0x000E3910
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasClientId)
			{
				num += 1U;
				uint serializedSize = this.ClientId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasBindRequest)
			{
				num += 1U;
				uint serializedSize2 = this.BindRequest.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasUseBindlessRpc)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04001831 RID: 6193
		public bool HasClientId;

		// Token: 0x04001832 RID: 6194
		private ProcessId _ClientId;

		// Token: 0x04001833 RID: 6195
		public bool HasBindRequest;

		// Token: 0x04001834 RID: 6196
		private BindRequest _BindRequest;

		// Token: 0x04001835 RID: 6197
		public bool HasUseBindlessRpc;

		// Token: 0x04001836 RID: 6198
		private bool _UseBindlessRpc;
	}
}
