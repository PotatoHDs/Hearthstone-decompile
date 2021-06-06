using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.NGDP
{
	// Token: 0x02001184 RID: 4484
	public class UpdateError : IProtoBuf
	{
		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x0600C53C RID: 50492 RVA: 0x003B6DC6 File Offset: 0x003B4FC6
		// (set) Token: 0x0600C53D RID: 50493 RVA: 0x003B6DCE File Offset: 0x003B4FCE
		public uint ErrorCode
		{
			get
			{
				return this._ErrorCode;
			}
			set
			{
				this._ErrorCode = value;
				this.HasErrorCode = true;
			}
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x0600C53E RID: 50494 RVA: 0x003B6DDE File Offset: 0x003B4FDE
		// (set) Token: 0x0600C53F RID: 50495 RVA: 0x003B6DE6 File Offset: 0x003B4FE6
		public float ElapsedSeconds
		{
			get
			{
				return this._ElapsedSeconds;
			}
			set
			{
				this._ElapsedSeconds = value;
				this.HasElapsedSeconds = true;
			}
		}

		// Token: 0x0600C540 RID: 50496 RVA: 0x003B6DF8 File Offset: 0x003B4FF8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasErrorCode)
			{
				num ^= this.ErrorCode.GetHashCode();
			}
			if (this.HasElapsedSeconds)
			{
				num ^= this.ElapsedSeconds.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C541 RID: 50497 RVA: 0x003B6E44 File Offset: 0x003B5044
		public override bool Equals(object obj)
		{
			UpdateError updateError = obj as UpdateError;
			return updateError != null && this.HasErrorCode == updateError.HasErrorCode && (!this.HasErrorCode || this.ErrorCode.Equals(updateError.ErrorCode)) && this.HasElapsedSeconds == updateError.HasElapsedSeconds && (!this.HasElapsedSeconds || this.ElapsedSeconds.Equals(updateError.ElapsedSeconds));
		}

		// Token: 0x0600C542 RID: 50498 RVA: 0x003B6EBA File Offset: 0x003B50BA
		public void Deserialize(Stream stream)
		{
			UpdateError.Deserialize(stream, this);
		}

		// Token: 0x0600C543 RID: 50499 RVA: 0x003B6EC4 File Offset: 0x003B50C4
		public static UpdateError Deserialize(Stream stream, UpdateError instance)
		{
			return UpdateError.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C544 RID: 50500 RVA: 0x003B6ED0 File Offset: 0x003B50D0
		public static UpdateError DeserializeLengthDelimited(Stream stream)
		{
			UpdateError updateError = new UpdateError();
			UpdateError.DeserializeLengthDelimited(stream, updateError);
			return updateError;
		}

		// Token: 0x0600C545 RID: 50501 RVA: 0x003B6EEC File Offset: 0x003B50EC
		public static UpdateError DeserializeLengthDelimited(Stream stream, UpdateError instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateError.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C546 RID: 50502 RVA: 0x003B6F14 File Offset: 0x003B5114
		public static UpdateError Deserialize(Stream stream, UpdateError instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				else if (num != 8)
				{
					if (num != 37)
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
						instance.ElapsedSeconds = binaryReader.ReadSingle();
					}
				}
				else
				{
					instance.ErrorCode = ProtocolParser.ReadUInt32(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600C547 RID: 50503 RVA: 0x003B6FB2 File Offset: 0x003B51B2
		public void Serialize(Stream stream)
		{
			UpdateError.Serialize(stream, this);
		}

		// Token: 0x0600C548 RID: 50504 RVA: 0x003B6FBC File Offset: 0x003B51BC
		public static void Serialize(Stream stream, UpdateError instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasErrorCode)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.ErrorCode);
			}
			if (instance.HasElapsedSeconds)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.ElapsedSeconds);
			}
		}

		// Token: 0x0600C549 RID: 50505 RVA: 0x003B7008 File Offset: 0x003B5208
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasErrorCode)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.ErrorCode);
			}
			if (this.HasElapsedSeconds)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x04009D8B RID: 40331
		public bool HasErrorCode;

		// Token: 0x04009D8C RID: 40332
		private uint _ErrorCode;

		// Token: 0x04009D8D RID: 40333
		public bool HasElapsedSeconds;

		// Token: 0x04009D8E RID: 40334
		private float _ElapsedSeconds;
	}
}
