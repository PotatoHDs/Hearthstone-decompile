using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003C0 RID: 960
	public class RemoveGameRequest : IProtoBuf
	{
		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06003EAB RID: 16043 RVA: 0x000C914C File Offset: 0x000C734C
		// (set) Token: 0x06003EAC RID: 16044 RVA: 0x000C9154 File Offset: 0x000C7354
		public RemoveGameOptions Options
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

		// Token: 0x06003EAD RID: 16045 RVA: 0x000C9167 File Offset: 0x000C7367
		public void SetOptions(RemoveGameOptions val)
		{
			this.Options = val;
		}

		// Token: 0x06003EAE RID: 16046 RVA: 0x000C9170 File Offset: 0x000C7370
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasOptions)
			{
				num ^= this.Options.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003EAF RID: 16047 RVA: 0x000C91A0 File Offset: 0x000C73A0
		public override bool Equals(object obj)
		{
			RemoveGameRequest removeGameRequest = obj as RemoveGameRequest;
			return removeGameRequest != null && this.HasOptions == removeGameRequest.HasOptions && (!this.HasOptions || this.Options.Equals(removeGameRequest.Options));
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06003EB0 RID: 16048 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003EB1 RID: 16049 RVA: 0x000C91E5 File Offset: 0x000C73E5
		public static RemoveGameRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RemoveGameRequest>(bs, 0, -1);
		}

		// Token: 0x06003EB2 RID: 16050 RVA: 0x000C91EF File Offset: 0x000C73EF
		public void Deserialize(Stream stream)
		{
			RemoveGameRequest.Deserialize(stream, this);
		}

		// Token: 0x06003EB3 RID: 16051 RVA: 0x000C91F9 File Offset: 0x000C73F9
		public static RemoveGameRequest Deserialize(Stream stream, RemoveGameRequest instance)
		{
			return RemoveGameRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003EB4 RID: 16052 RVA: 0x000C9204 File Offset: 0x000C7404
		public static RemoveGameRequest DeserializeLengthDelimited(Stream stream)
		{
			RemoveGameRequest removeGameRequest = new RemoveGameRequest();
			RemoveGameRequest.DeserializeLengthDelimited(stream, removeGameRequest);
			return removeGameRequest;
		}

		// Token: 0x06003EB5 RID: 16053 RVA: 0x000C9220 File Offset: 0x000C7420
		public static RemoveGameRequest DeserializeLengthDelimited(Stream stream, RemoveGameRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RemoveGameRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003EB6 RID: 16054 RVA: 0x000C9248 File Offset: 0x000C7448
		public static RemoveGameRequest Deserialize(Stream stream, RemoveGameRequest instance, long limit)
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
						instance.Options = RemoveGameOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						RemoveGameOptions.DeserializeLengthDelimited(stream, instance.Options);
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

		// Token: 0x06003EB7 RID: 16055 RVA: 0x000C92E2 File Offset: 0x000C74E2
		public void Serialize(Stream stream)
		{
			RemoveGameRequest.Serialize(stream, this);
		}

		// Token: 0x06003EB8 RID: 16056 RVA: 0x000C92EB File Offset: 0x000C74EB
		public static void Serialize(Stream stream, RemoveGameRequest instance)
		{
			if (instance.HasOptions)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				RemoveGameOptions.Serialize(stream, instance.Options);
			}
		}

		// Token: 0x06003EB9 RID: 16057 RVA: 0x000C931C File Offset: 0x000C751C
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

		// Token: 0x04001616 RID: 5654
		public bool HasOptions;

		// Token: 0x04001617 RID: 5655
		private RemoveGameOptions _Options;
	}
}
