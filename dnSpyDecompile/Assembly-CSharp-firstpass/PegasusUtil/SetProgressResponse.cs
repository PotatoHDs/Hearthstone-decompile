using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000B8 RID: 184
	public class SetProgressResponse : IProtoBuf
	{
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x0002F963 File Offset: 0x0002DB63
		// (set) Token: 0x06000CB7 RID: 3255 RVA: 0x0002F96B File Offset: 0x0002DB6B
		public SetProgressResponse.Result Result_ { get; set; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x0002F974 File Offset: 0x0002DB74
		// (set) Token: 0x06000CB9 RID: 3257 RVA: 0x0002F97C File Offset: 0x0002DB7C
		public long Progress
		{
			get
			{
				return this._Progress;
			}
			set
			{
				this._Progress = value;
				this.HasProgress = true;
			}
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0002F98C File Offset: 0x0002DB8C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Result_.GetHashCode();
			if (this.HasProgress)
			{
				num ^= this.Progress.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0002F9D8 File Offset: 0x0002DBD8
		public override bool Equals(object obj)
		{
			SetProgressResponse setProgressResponse = obj as SetProgressResponse;
			return setProgressResponse != null && this.Result_.Equals(setProgressResponse.Result_) && this.HasProgress == setProgressResponse.HasProgress && (!this.HasProgress || this.Progress.Equals(setProgressResponse.Progress));
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0002FA43 File Offset: 0x0002DC43
		public void Deserialize(Stream stream)
		{
			SetProgressResponse.Deserialize(stream, this);
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0002FA4D File Offset: 0x0002DC4D
		public static SetProgressResponse Deserialize(Stream stream, SetProgressResponse instance)
		{
			return SetProgressResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0002FA58 File Offset: 0x0002DC58
		public static SetProgressResponse DeserializeLengthDelimited(Stream stream)
		{
			SetProgressResponse setProgressResponse = new SetProgressResponse();
			SetProgressResponse.DeserializeLengthDelimited(stream, setProgressResponse);
			return setProgressResponse;
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0002FA74 File Offset: 0x0002DC74
		public static SetProgressResponse DeserializeLengthDelimited(Stream stream, SetProgressResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SetProgressResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0002FA9C File Offset: 0x0002DC9C
		public static SetProgressResponse Deserialize(Stream stream, SetProgressResponse instance, long limit)
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
				else if (num != 8)
				{
					if (num != 16)
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
						instance.Progress = (long)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Result_ = (SetProgressResponse.Result)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0002FB34 File Offset: 0x0002DD34
		public void Serialize(Stream stream)
		{
			SetProgressResponse.Serialize(stream, this);
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0002FB3D File Offset: 0x0002DD3D
		public static void Serialize(Stream stream, SetProgressResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Result_));
			if (instance.HasProgress)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Progress);
			}
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0002FB70 File Offset: 0x0002DD70
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Result_));
			if (this.HasProgress)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.Progress);
			}
			return num + 1U;
		}

		// Token: 0x0400046B RID: 1131
		public bool HasProgress;

		// Token: 0x0400046C RID: 1132
		private long _Progress;

		// Token: 0x020005C4 RID: 1476
		public enum PacketID
		{
			// Token: 0x04001F9A RID: 8090
			ID = 296
		}

		// Token: 0x020005C5 RID: 1477
		public enum Result
		{
			// Token: 0x04001F9C RID: 8092
			SUCCESS = 1,
			// Token: 0x04001F9D RID: 8093
			FAILED,
			// Token: 0x04001F9E RID: 8094
			ALREADY_DONE
		}
	}
}
