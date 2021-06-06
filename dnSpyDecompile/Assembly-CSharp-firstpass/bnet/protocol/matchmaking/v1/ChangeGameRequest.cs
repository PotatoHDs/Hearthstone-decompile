using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003BA RID: 954
	public class ChangeGameRequest : IProtoBuf
	{
		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06003E2E RID: 15918 RVA: 0x000C7DAC File Offset: 0x000C5FAC
		// (set) Token: 0x06003E2F RID: 15919 RVA: 0x000C7DB4 File Offset: 0x000C5FB4
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

		// Token: 0x06003E30 RID: 15920 RVA: 0x000C7DC7 File Offset: 0x000C5FC7
		public void SetOptions(UpdateGameOptions val)
		{
			this.Options = val;
		}

		// Token: 0x06003E31 RID: 15921 RVA: 0x000C7DD0 File Offset: 0x000C5FD0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasOptions)
			{
				num ^= this.Options.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003E32 RID: 15922 RVA: 0x000C7E00 File Offset: 0x000C6000
		public override bool Equals(object obj)
		{
			ChangeGameRequest changeGameRequest = obj as ChangeGameRequest;
			return changeGameRequest != null && this.HasOptions == changeGameRequest.HasOptions && (!this.HasOptions || this.Options.Equals(changeGameRequest.Options));
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06003E33 RID: 15923 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003E34 RID: 15924 RVA: 0x000C7E45 File Offset: 0x000C6045
		public static ChangeGameRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChangeGameRequest>(bs, 0, -1);
		}

		// Token: 0x06003E35 RID: 15925 RVA: 0x000C7E4F File Offset: 0x000C604F
		public void Deserialize(Stream stream)
		{
			ChangeGameRequest.Deserialize(stream, this);
		}

		// Token: 0x06003E36 RID: 15926 RVA: 0x000C7E59 File Offset: 0x000C6059
		public static ChangeGameRequest Deserialize(Stream stream, ChangeGameRequest instance)
		{
			return ChangeGameRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003E37 RID: 15927 RVA: 0x000C7E64 File Offset: 0x000C6064
		public static ChangeGameRequest DeserializeLengthDelimited(Stream stream)
		{
			ChangeGameRequest changeGameRequest = new ChangeGameRequest();
			ChangeGameRequest.DeserializeLengthDelimited(stream, changeGameRequest);
			return changeGameRequest;
		}

		// Token: 0x06003E38 RID: 15928 RVA: 0x000C7E80 File Offset: 0x000C6080
		public static ChangeGameRequest DeserializeLengthDelimited(Stream stream, ChangeGameRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChangeGameRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003E39 RID: 15929 RVA: 0x000C7EA8 File Offset: 0x000C60A8
		public static ChangeGameRequest Deserialize(Stream stream, ChangeGameRequest instance, long limit)
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

		// Token: 0x06003E3A RID: 15930 RVA: 0x000C7F42 File Offset: 0x000C6142
		public void Serialize(Stream stream)
		{
			ChangeGameRequest.Serialize(stream, this);
		}

		// Token: 0x06003E3B RID: 15931 RVA: 0x000C7F4B File Offset: 0x000C614B
		public static void Serialize(Stream stream, ChangeGameRequest instance)
		{
			if (instance.HasOptions)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				UpdateGameOptions.Serialize(stream, instance.Options);
			}
		}

		// Token: 0x06003E3C RID: 15932 RVA: 0x000C7F7C File Offset: 0x000C617C
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

		// Token: 0x040015FE RID: 5630
		public bool HasOptions;

		// Token: 0x040015FF RID: 5631
		private UpdateGameOptions _Options;
	}
}
