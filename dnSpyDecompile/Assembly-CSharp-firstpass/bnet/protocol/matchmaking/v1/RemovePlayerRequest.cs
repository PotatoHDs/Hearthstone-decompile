using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003C1 RID: 961
	public class RemovePlayerRequest : IProtoBuf
	{
		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x06003EBB RID: 16059 RVA: 0x000C934F File Offset: 0x000C754F
		// (set) Token: 0x06003EBC RID: 16060 RVA: 0x000C9357 File Offset: 0x000C7557
		public RemovePlayerOptions Options
		{
			get
			{
				return this._Options;
			}
			set
			{
				this._Options = value;
				this.HasOptions = (value != null);
			}
		}

		// Token: 0x06003EBD RID: 16061 RVA: 0x000C936A File Offset: 0x000C756A
		public void SetOptions(RemovePlayerOptions val)
		{
			this.Options = val;
		}

		// Token: 0x06003EBE RID: 16062 RVA: 0x000C9374 File Offset: 0x000C7574
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasOptions)
			{
				num ^= this.Options.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003EBF RID: 16063 RVA: 0x000C93A4 File Offset: 0x000C75A4
		public override bool Equals(object obj)
		{
			RemovePlayerRequest removePlayerRequest = obj as RemovePlayerRequest;
			return removePlayerRequest != null && this.HasOptions == removePlayerRequest.HasOptions && (!this.HasOptions || this.Options.Equals(removePlayerRequest.Options));
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x06003EC0 RID: 16064 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003EC1 RID: 16065 RVA: 0x000C93E9 File Offset: 0x000C75E9
		public static RemovePlayerRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RemovePlayerRequest>(bs, 0, -1);
		}

		// Token: 0x06003EC2 RID: 16066 RVA: 0x000C93F3 File Offset: 0x000C75F3
		public void Deserialize(Stream stream)
		{
			RemovePlayerRequest.Deserialize(stream, this);
		}

		// Token: 0x06003EC3 RID: 16067 RVA: 0x000C93FD File Offset: 0x000C75FD
		public static RemovePlayerRequest Deserialize(Stream stream, RemovePlayerRequest instance)
		{
			return RemovePlayerRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003EC4 RID: 16068 RVA: 0x000C9408 File Offset: 0x000C7608
		public static RemovePlayerRequest DeserializeLengthDelimited(Stream stream)
		{
			RemovePlayerRequest removePlayerRequest = new RemovePlayerRequest();
			RemovePlayerRequest.DeserializeLengthDelimited(stream, removePlayerRequest);
			return removePlayerRequest;
		}

		// Token: 0x06003EC5 RID: 16069 RVA: 0x000C9424 File Offset: 0x000C7624
		public static RemovePlayerRequest DeserializeLengthDelimited(Stream stream, RemovePlayerRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RemovePlayerRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003EC6 RID: 16070 RVA: 0x000C944C File Offset: 0x000C764C
		public static RemovePlayerRequest Deserialize(Stream stream, RemovePlayerRequest instance, long limit)
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
					if (instance.Options == null)
					{
						instance.Options = RemovePlayerOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						RemovePlayerOptions.DeserializeLengthDelimited(stream, instance.Options);
					}
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

		// Token: 0x06003EC7 RID: 16071 RVA: 0x000C94E6 File Offset: 0x000C76E6
		public void Serialize(Stream stream)
		{
			RemovePlayerRequest.Serialize(stream, this);
		}

		// Token: 0x06003EC8 RID: 16072 RVA: 0x000C94EF File Offset: 0x000C76EF
		public static void Serialize(Stream stream, RemovePlayerRequest instance)
		{
			if (instance.HasOptions)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				RemovePlayerOptions.Serialize(stream, instance.Options);
			}
		}

		// Token: 0x06003EC9 RID: 16073 RVA: 0x000C9520 File Offset: 0x000C7720
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasOptions)
			{
				num += 1U;
				uint serializedSize = this.Options.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001618 RID: 5656
		public bool HasOptions;

		// Token: 0x04001619 RID: 5657
		private RemovePlayerOptions _Options;
	}
}
