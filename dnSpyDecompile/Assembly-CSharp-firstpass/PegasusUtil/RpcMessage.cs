using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000117 RID: 279
	public class RpcMessage : IProtoBuf
	{
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001277 RID: 4727 RVA: 0x00040E36 File Offset: 0x0003F036
		// (set) Token: 0x06001278 RID: 4728 RVA: 0x00040E3E File Offset: 0x0003F03E
		public RpcHeader RpcHeader { get; set; }

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001279 RID: 4729 RVA: 0x00040E47 File Offset: 0x0003F047
		// (set) Token: 0x0600127A RID: 4730 RVA: 0x00040E4F File Offset: 0x0003F04F
		public byte[] MessageBody
		{
			get
			{
				return this._MessageBody;
			}
			set
			{
				this._MessageBody = value;
				this.HasMessageBody = (value != null);
			}
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x00040E64 File Offset: 0x0003F064
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.RpcHeader.GetHashCode();
			if (this.HasMessageBody)
			{
				num ^= this.MessageBody.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x00040EA4 File Offset: 0x0003F0A4
		public override bool Equals(object obj)
		{
			RpcMessage rpcMessage = obj as RpcMessage;
			return rpcMessage != null && this.RpcHeader.Equals(rpcMessage.RpcHeader) && this.HasMessageBody == rpcMessage.HasMessageBody && (!this.HasMessageBody || this.MessageBody.Equals(rpcMessage.MessageBody));
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x00040EFE File Offset: 0x0003F0FE
		public void Deserialize(Stream stream)
		{
			RpcMessage.Deserialize(stream, this);
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x00040F08 File Offset: 0x0003F108
		public static RpcMessage Deserialize(Stream stream, RpcMessage instance)
		{
			return RpcMessage.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x00040F14 File Offset: 0x0003F114
		public static RpcMessage DeserializeLengthDelimited(Stream stream)
		{
			RpcMessage rpcMessage = new RpcMessage();
			RpcMessage.DeserializeLengthDelimited(stream, rpcMessage);
			return rpcMessage;
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x00040F30 File Offset: 0x0003F130
		public static RpcMessage DeserializeLengthDelimited(Stream stream, RpcMessage instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RpcMessage.Deserialize(stream, instance, num);
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x00040F58 File Offset: 0x0003F158
		public static RpcMessage Deserialize(Stream stream, RpcMessage instance, long limit)
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
						instance.MessageBody = ProtocolParser.ReadBytes(stream);
					}
				}
				else if (instance.RpcHeader == null)
				{
					instance.RpcHeader = RpcHeader.DeserializeLengthDelimited(stream);
				}
				else
				{
					RpcHeader.DeserializeLengthDelimited(stream, instance.RpcHeader);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x0004100A File Offset: 0x0003F20A
		public void Serialize(Stream stream)
		{
			RpcMessage.Serialize(stream, this);
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x00041014 File Offset: 0x0003F214
		public static void Serialize(Stream stream, RpcMessage instance)
		{
			if (instance.RpcHeader == null)
			{
				throw new ArgumentNullException("RpcHeader", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.RpcHeader.GetSerializedSize());
			RpcHeader.Serialize(stream, instance.RpcHeader);
			if (instance.HasMessageBody)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, instance.MessageBody);
			}
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x0004107C File Offset: 0x0003F27C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.RpcHeader.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasMessageBody)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.MessageBody.Length) + (uint)this.MessageBody.Length;
			}
			return num + 1U;
		}

		// Token: 0x040005B4 RID: 1460
		public bool HasMessageBody;

		// Token: 0x040005B5 RID: 1461
		private byte[] _MessageBody;
	}
}
