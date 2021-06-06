using System;
using System.IO;

namespace bnet.protocol.whisper.v1.admin
{
	// Token: 0x020002EB RID: 747
	public class AdminWhisper : IProtoBuf
	{
		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06002C5C RID: 11356 RVA: 0x00098BAF File Offset: 0x00096DAF
		// (set) Token: 0x06002C5D RID: 11357 RVA: 0x00098BB7 File Offset: 0x00096DB7
		public Whisper Whisper
		{
			get
			{
				return this._Whisper;
			}
			set
			{
				this._Whisper = value;
				this.HasWhisper = (value != null);
			}
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x00098BCA File Offset: 0x00096DCA
		public void SetWhisper(Whisper val)
		{
			this.Whisper = val;
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x00098BD4 File Offset: 0x00096DD4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasWhisper)
			{
				num ^= this.Whisper.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x00098C04 File Offset: 0x00096E04
		public override bool Equals(object obj)
		{
			AdminWhisper adminWhisper = obj as AdminWhisper;
			return adminWhisper != null && this.HasWhisper == adminWhisper.HasWhisper && (!this.HasWhisper || this.Whisper.Equals(adminWhisper.Whisper));
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06002C61 RID: 11361 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x00098C49 File Offset: 0x00096E49
		public static AdminWhisper ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AdminWhisper>(bs, 0, -1);
		}

		// Token: 0x06002C63 RID: 11363 RVA: 0x00098C53 File Offset: 0x00096E53
		public void Deserialize(Stream stream)
		{
			AdminWhisper.Deserialize(stream, this);
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x00098C5D File Offset: 0x00096E5D
		public static AdminWhisper Deserialize(Stream stream, AdminWhisper instance)
		{
			return AdminWhisper.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x00098C68 File Offset: 0x00096E68
		public static AdminWhisper DeserializeLengthDelimited(Stream stream)
		{
			AdminWhisper adminWhisper = new AdminWhisper();
			AdminWhisper.DeserializeLengthDelimited(stream, adminWhisper);
			return adminWhisper;
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x00098C84 File Offset: 0x00096E84
		public static AdminWhisper DeserializeLengthDelimited(Stream stream, AdminWhisper instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AdminWhisper.Deserialize(stream, instance, num);
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x00098CAC File Offset: 0x00096EAC
		public static AdminWhisper Deserialize(Stream stream, AdminWhisper instance, long limit)
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
					if (instance.Whisper == null)
					{
						instance.Whisper = Whisper.DeserializeLengthDelimited(stream);
					}
					else
					{
						Whisper.DeserializeLengthDelimited(stream, instance.Whisper);
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

		// Token: 0x06002C68 RID: 11368 RVA: 0x00098D46 File Offset: 0x00096F46
		public void Serialize(Stream stream)
		{
			AdminWhisper.Serialize(stream, this);
		}

		// Token: 0x06002C69 RID: 11369 RVA: 0x00098D4F File Offset: 0x00096F4F
		public static void Serialize(Stream stream, AdminWhisper instance)
		{
			if (instance.HasWhisper)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Whisper.GetSerializedSize());
				Whisper.Serialize(stream, instance.Whisper);
			}
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x00098D80 File Offset: 0x00096F80
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasWhisper)
			{
				num += 1U;
				uint serializedSize = this.Whisper.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001260 RID: 4704
		public bool HasWhisper;

		// Token: 0x04001261 RID: 4705
		private Whisper _Whisper;
	}
}
