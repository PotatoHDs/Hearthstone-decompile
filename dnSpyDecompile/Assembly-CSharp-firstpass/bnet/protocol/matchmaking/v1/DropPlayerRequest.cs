using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003BB RID: 955
	public class DropPlayerRequest : IProtoBuf
	{
		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06003E3E RID: 15934 RVA: 0x000C7FAF File Offset: 0x000C61AF
		// (set) Token: 0x06003E3F RID: 15935 RVA: 0x000C7FB7 File Offset: 0x000C61B7
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

		// Token: 0x06003E40 RID: 15936 RVA: 0x000C7FCA File Offset: 0x000C61CA
		public void SetOptions(RemovePlayerOptions val)
		{
			this.Options = val;
		}

		// Token: 0x06003E41 RID: 15937 RVA: 0x000C7FD4 File Offset: 0x000C61D4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasOptions)
			{
				num ^= this.Options.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003E42 RID: 15938 RVA: 0x000C8004 File Offset: 0x000C6204
		public override bool Equals(object obj)
		{
			DropPlayerRequest dropPlayerRequest = obj as DropPlayerRequest;
			return dropPlayerRequest != null && this.HasOptions == dropPlayerRequest.HasOptions && (!this.HasOptions || this.Options.Equals(dropPlayerRequest.Options));
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06003E43 RID: 15939 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003E44 RID: 15940 RVA: 0x000C8049 File Offset: 0x000C6249
		public static DropPlayerRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<DropPlayerRequest>(bs, 0, -1);
		}

		// Token: 0x06003E45 RID: 15941 RVA: 0x000C8053 File Offset: 0x000C6253
		public void Deserialize(Stream stream)
		{
			DropPlayerRequest.Deserialize(stream, this);
		}

		// Token: 0x06003E46 RID: 15942 RVA: 0x000C805D File Offset: 0x000C625D
		public static DropPlayerRequest Deserialize(Stream stream, DropPlayerRequest instance)
		{
			return DropPlayerRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003E47 RID: 15943 RVA: 0x000C8068 File Offset: 0x000C6268
		public static DropPlayerRequest DeserializeLengthDelimited(Stream stream)
		{
			DropPlayerRequest dropPlayerRequest = new DropPlayerRequest();
			DropPlayerRequest.DeserializeLengthDelimited(stream, dropPlayerRequest);
			return dropPlayerRequest;
		}

		// Token: 0x06003E48 RID: 15944 RVA: 0x000C8084 File Offset: 0x000C6284
		public static DropPlayerRequest DeserializeLengthDelimited(Stream stream, DropPlayerRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DropPlayerRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003E49 RID: 15945 RVA: 0x000C80AC File Offset: 0x000C62AC
		public static DropPlayerRequest Deserialize(Stream stream, DropPlayerRequest instance, long limit)
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

		// Token: 0x06003E4A RID: 15946 RVA: 0x000C8146 File Offset: 0x000C6346
		public void Serialize(Stream stream)
		{
			DropPlayerRequest.Serialize(stream, this);
		}

		// Token: 0x06003E4B RID: 15947 RVA: 0x000C814F File Offset: 0x000C634F
		public static void Serialize(Stream stream, DropPlayerRequest instance)
		{
			if (instance.HasOptions)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				RemovePlayerOptions.Serialize(stream, instance.Options);
			}
		}

		// Token: 0x06003E4C RID: 15948 RVA: 0x000C8180 File Offset: 0x000C6380
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

		// Token: 0x04001600 RID: 5632
		public bool HasOptions;

		// Token: 0x04001601 RID: 5633
		private RemovePlayerOptions _Options;
	}
}
