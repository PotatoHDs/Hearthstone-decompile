using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000CB RID: 203
	public class ClientRequestResponse : IProtoBuf
	{
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x000334AF File Offset: 0x000316AF
		// (set) Token: 0x06000DE6 RID: 3558 RVA: 0x000334B7 File Offset: 0x000316B7
		public ClientRequestResponse.ClientRequestResponseFlags ResponseFlags
		{
			get
			{
				return this._ResponseFlags;
			}
			set
			{
				this._ResponseFlags = value;
				this.HasResponseFlags = true;
			}
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x000334C8 File Offset: 0x000316C8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasResponseFlags)
			{
				num ^= this.ResponseFlags.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00033504 File Offset: 0x00031704
		public override bool Equals(object obj)
		{
			ClientRequestResponse clientRequestResponse = obj as ClientRequestResponse;
			return clientRequestResponse != null && this.HasResponseFlags == clientRequestResponse.HasResponseFlags && (!this.HasResponseFlags || this.ResponseFlags.Equals(clientRequestResponse.ResponseFlags));
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x00033557 File Offset: 0x00031757
		public void Deserialize(Stream stream)
		{
			ClientRequestResponse.Deserialize(stream, this);
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00033561 File Offset: 0x00031761
		public static ClientRequestResponse Deserialize(Stream stream, ClientRequestResponse instance)
		{
			return ClientRequestResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0003356C File Offset: 0x0003176C
		public static ClientRequestResponse DeserializeLengthDelimited(Stream stream)
		{
			ClientRequestResponse clientRequestResponse = new ClientRequestResponse();
			ClientRequestResponse.DeserializeLengthDelimited(stream, clientRequestResponse);
			return clientRequestResponse;
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00033588 File Offset: 0x00031788
		public static ClientRequestResponse DeserializeLengthDelimited(Stream stream, ClientRequestResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ClientRequestResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x000335B0 File Offset: 0x000317B0
		public static ClientRequestResponse Deserialize(Stream stream, ClientRequestResponse instance, long limit)
		{
			instance.ResponseFlags = ClientRequestResponse.ClientRequestResponseFlags.CRRF_SERVICE_NONE;
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
					instance.ResponseFlags = (ClientRequestResponse.ClientRequestResponseFlags)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06000DEE RID: 3566 RVA: 0x00033637 File Offset: 0x00031837
		public void Serialize(Stream stream)
		{
			ClientRequestResponse.Serialize(stream, this);
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x00033640 File Offset: 0x00031840
		public static void Serialize(Stream stream, ClientRequestResponse instance)
		{
			if (instance.HasResponseFlags)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ResponseFlags));
			}
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x00033660 File Offset: 0x00031860
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasResponseFlags)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ResponseFlags));
			}
			return num;
		}

		// Token: 0x040004B1 RID: 1201
		public bool HasResponseFlags;

		// Token: 0x040004B2 RID: 1202
		private ClientRequestResponse.ClientRequestResponseFlags _ResponseFlags;

		// Token: 0x020005D9 RID: 1497
		public enum PacketID
		{
			// Token: 0x04001FD6 RID: 8150
			ID = 328
		}

		// Token: 0x020005DA RID: 1498
		public enum ClientRequestResponseFlags
		{
			// Token: 0x04001FD8 RID: 8152
			CRRF_SERVICE_NONE,
			// Token: 0x04001FD9 RID: 8153
			CRRF_SERVICE_UNAVAILABLE,
			// Token: 0x04001FDA RID: 8154
			CRRF_SERVICE_UNKNOWN_ERROR
		}
	}
}
