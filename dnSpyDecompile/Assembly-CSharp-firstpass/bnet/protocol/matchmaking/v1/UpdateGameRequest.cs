using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003C2 RID: 962
	public class UpdateGameRequest : IProtoBuf
	{
		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06003ECB RID: 16075 RVA: 0x000C9553 File Offset: 0x000C7753
		// (set) Token: 0x06003ECC RID: 16076 RVA: 0x000C955B File Offset: 0x000C775B
		public UpdateGameOptions Options
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

		// Token: 0x06003ECD RID: 16077 RVA: 0x000C956E File Offset: 0x000C776E
		public void SetOptions(UpdateGameOptions val)
		{
			this.Options = val;
		}

		// Token: 0x06003ECE RID: 16078 RVA: 0x000C9578 File Offset: 0x000C7778
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasOptions)
			{
				num ^= this.Options.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003ECF RID: 16079 RVA: 0x000C95A8 File Offset: 0x000C77A8
		public override bool Equals(object obj)
		{
			UpdateGameRequest updateGameRequest = obj as UpdateGameRequest;
			return updateGameRequest != null && this.HasOptions == updateGameRequest.HasOptions && (!this.HasOptions || this.Options.Equals(updateGameRequest.Options));
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x06003ED0 RID: 16080 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003ED1 RID: 16081 RVA: 0x000C95ED File Offset: 0x000C77ED
		public static UpdateGameRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateGameRequest>(bs, 0, -1);
		}

		// Token: 0x06003ED2 RID: 16082 RVA: 0x000C95F7 File Offset: 0x000C77F7
		public void Deserialize(Stream stream)
		{
			UpdateGameRequest.Deserialize(stream, this);
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x000C9601 File Offset: 0x000C7801
		public static UpdateGameRequest Deserialize(Stream stream, UpdateGameRequest instance)
		{
			return UpdateGameRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003ED4 RID: 16084 RVA: 0x000C960C File Offset: 0x000C780C
		public static UpdateGameRequest DeserializeLengthDelimited(Stream stream)
		{
			UpdateGameRequest updateGameRequest = new UpdateGameRequest();
			UpdateGameRequest.DeserializeLengthDelimited(stream, updateGameRequest);
			return updateGameRequest;
		}

		// Token: 0x06003ED5 RID: 16085 RVA: 0x000C9628 File Offset: 0x000C7828
		public static UpdateGameRequest DeserializeLengthDelimited(Stream stream, UpdateGameRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateGameRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003ED6 RID: 16086 RVA: 0x000C9650 File Offset: 0x000C7850
		public static UpdateGameRequest Deserialize(Stream stream, UpdateGameRequest instance, long limit)
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
						instance.Options = UpdateGameOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						UpdateGameOptions.DeserializeLengthDelimited(stream, instance.Options);
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

		// Token: 0x06003ED7 RID: 16087 RVA: 0x000C96EA File Offset: 0x000C78EA
		public void Serialize(Stream stream)
		{
			UpdateGameRequest.Serialize(stream, this);
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x000C96F3 File Offset: 0x000C78F3
		public static void Serialize(Stream stream, UpdateGameRequest instance)
		{
			if (instance.HasOptions)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				UpdateGameOptions.Serialize(stream, instance.Options);
			}
		}

		// Token: 0x06003ED9 RID: 16089 RVA: 0x000C9724 File Offset: 0x000C7924
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

		// Token: 0x0400161A RID: 5658
		public bool HasOptions;

		// Token: 0x0400161B RID: 5659
		private UpdateGameOptions _Options;
	}
}
