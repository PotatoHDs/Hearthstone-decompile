using System;
using System.IO;
using System.Text;

namespace bnet.protocol.game_utilities.v1
{
	// Token: 0x02000367 RID: 871
	public class ClientInfo : IProtoBuf
	{
		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06003720 RID: 14112 RVA: 0x000B56DF File Offset: 0x000B38DF
		// (set) Token: 0x06003721 RID: 14113 RVA: 0x000B56E7 File Offset: 0x000B38E7
		public string ClientAddress
		{
			get
			{
				return this._ClientAddress;
			}
			set
			{
				this._ClientAddress = value;
				this.HasClientAddress = (value != null);
			}
		}

		// Token: 0x06003722 RID: 14114 RVA: 0x000B56FA File Offset: 0x000B38FA
		public void SetClientAddress(string val)
		{
			this.ClientAddress = val;
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06003723 RID: 14115 RVA: 0x000B5703 File Offset: 0x000B3903
		// (set) Token: 0x06003724 RID: 14116 RVA: 0x000B570B File Offset: 0x000B390B
		public bool PrivilegedNetwork
		{
			get
			{
				return this._PrivilegedNetwork;
			}
			set
			{
				this._PrivilegedNetwork = value;
				this.HasPrivilegedNetwork = true;
			}
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x000B571B File Offset: 0x000B391B
		public void SetPrivilegedNetwork(bool val)
		{
			this.PrivilegedNetwork = val;
		}

		// Token: 0x06003726 RID: 14118 RVA: 0x000B5724 File Offset: 0x000B3924
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasClientAddress)
			{
				num ^= this.ClientAddress.GetHashCode();
			}
			if (this.HasPrivilegedNetwork)
			{
				num ^= this.PrivilegedNetwork.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x000B5770 File Offset: 0x000B3970
		public override bool Equals(object obj)
		{
			ClientInfo clientInfo = obj as ClientInfo;
			return clientInfo != null && this.HasClientAddress == clientInfo.HasClientAddress && (!this.HasClientAddress || this.ClientAddress.Equals(clientInfo.ClientAddress)) && this.HasPrivilegedNetwork == clientInfo.HasPrivilegedNetwork && (!this.HasPrivilegedNetwork || this.PrivilegedNetwork.Equals(clientInfo.PrivilegedNetwork));
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06003728 RID: 14120 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003729 RID: 14121 RVA: 0x000B57E3 File Offset: 0x000B39E3
		public static ClientInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ClientInfo>(bs, 0, -1);
		}

		// Token: 0x0600372A RID: 14122 RVA: 0x000B57ED File Offset: 0x000B39ED
		public void Deserialize(Stream stream)
		{
			ClientInfo.Deserialize(stream, this);
		}

		// Token: 0x0600372B RID: 14123 RVA: 0x000B57F7 File Offset: 0x000B39F7
		public static ClientInfo Deserialize(Stream stream, ClientInfo instance)
		{
			return ClientInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x000B5804 File Offset: 0x000B3A04
		public static ClientInfo DeserializeLengthDelimited(Stream stream)
		{
			ClientInfo clientInfo = new ClientInfo();
			ClientInfo.DeserializeLengthDelimited(stream, clientInfo);
			return clientInfo;
		}

		// Token: 0x0600372D RID: 14125 RVA: 0x000B5820 File Offset: 0x000B3A20
		public static ClientInfo DeserializeLengthDelimited(Stream stream, ClientInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ClientInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x0600372E RID: 14126 RVA: 0x000B5848 File Offset: 0x000B3A48
		public static ClientInfo Deserialize(Stream stream, ClientInfo instance, long limit)
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
					if (num != 16)
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
						instance.PrivilegedNetwork = ProtocolParser.ReadBool(stream);
					}
				}
				else
				{
					instance.ClientAddress = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600372F RID: 14127 RVA: 0x000B58E0 File Offset: 0x000B3AE0
		public void Serialize(Stream stream)
		{
			ClientInfo.Serialize(stream, this);
		}

		// Token: 0x06003730 RID: 14128 RVA: 0x000B58EC File Offset: 0x000B3AEC
		public static void Serialize(Stream stream, ClientInfo instance)
		{
			if (instance.HasClientAddress)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientAddress));
			}
			if (instance.HasPrivilegedNetwork)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.PrivilegedNetwork);
			}
		}

		// Token: 0x06003731 RID: 14129 RVA: 0x000B593C File Offset: 0x000B3B3C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasClientAddress)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ClientAddress);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasPrivilegedNetwork)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x040014AC RID: 5292
		public bool HasClientAddress;

		// Token: 0x040014AD RID: 5293
		private string _ClientAddress;

		// Token: 0x040014AE RID: 5294
		public bool HasPrivilegedNetwork;

		// Token: 0x040014AF RID: 5295
		private bool _PrivilegedNetwork;
	}
}
