using System;
using System.IO;
using System.Text;

namespace bnet.protocol.voice.v2.server
{
	// Token: 0x020002C7 RID: 711
	public class RemoveAccountFromChannelRequest : IProtoBuf
	{
		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x0600298C RID: 10636 RVA: 0x00091B17 File Offset: 0x0008FD17
		// (set) Token: 0x0600298D RID: 10637 RVA: 0x00091B1F File Offset: 0x0008FD1F
		public string JoinToken
		{
			get
			{
				return this._JoinToken;
			}
			set
			{
				this._JoinToken = value;
				this.HasJoinToken = (value != null);
			}
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x00091B32 File Offset: 0x0008FD32
		public void SetJoinToken(string val)
		{
			this.JoinToken = val;
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x00091B3C File Offset: 0x0008FD3C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasJoinToken)
			{
				num ^= this.JoinToken.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x00091B6C File Offset: 0x0008FD6C
		public override bool Equals(object obj)
		{
			RemoveAccountFromChannelRequest removeAccountFromChannelRequest = obj as RemoveAccountFromChannelRequest;
			return removeAccountFromChannelRequest != null && this.HasJoinToken == removeAccountFromChannelRequest.HasJoinToken && (!this.HasJoinToken || this.JoinToken.Equals(removeAccountFromChannelRequest.JoinToken));
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06002991 RID: 10641 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x00091BB1 File Offset: 0x0008FDB1
		public static RemoveAccountFromChannelRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RemoveAccountFromChannelRequest>(bs, 0, -1);
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x00091BBB File Offset: 0x0008FDBB
		public void Deserialize(Stream stream)
		{
			RemoveAccountFromChannelRequest.Deserialize(stream, this);
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x00091BC5 File Offset: 0x0008FDC5
		public static RemoveAccountFromChannelRequest Deserialize(Stream stream, RemoveAccountFromChannelRequest instance)
		{
			return RemoveAccountFromChannelRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x00091BD0 File Offset: 0x0008FDD0
		public static RemoveAccountFromChannelRequest DeserializeLengthDelimited(Stream stream)
		{
			RemoveAccountFromChannelRequest removeAccountFromChannelRequest = new RemoveAccountFromChannelRequest();
			RemoveAccountFromChannelRequest.DeserializeLengthDelimited(stream, removeAccountFromChannelRequest);
			return removeAccountFromChannelRequest;
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x00091BEC File Offset: 0x0008FDEC
		public static RemoveAccountFromChannelRequest DeserializeLengthDelimited(Stream stream, RemoveAccountFromChannelRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RemoveAccountFromChannelRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x00091C14 File Offset: 0x0008FE14
		public static RemoveAccountFromChannelRequest Deserialize(Stream stream, RemoveAccountFromChannelRequest instance, long limit)
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
					instance.JoinToken = ProtocolParser.ReadString(stream);
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

		// Token: 0x06002998 RID: 10648 RVA: 0x00091C94 File Offset: 0x0008FE94
		public void Serialize(Stream stream)
		{
			RemoveAccountFromChannelRequest.Serialize(stream, this);
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x00091C9D File Offset: 0x0008FE9D
		public static void Serialize(Stream stream, RemoveAccountFromChannelRequest instance)
		{
			if (instance.HasJoinToken)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.JoinToken));
			}
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x00091CC8 File Offset: 0x0008FEC8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasJoinToken)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.JoinToken);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x040011CE RID: 4558
		public bool HasJoinToken;

		// Token: 0x040011CF RID: 4559
		private string _JoinToken;
	}
}
