using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x02000193 RID: 403
	public class TargetOption : IProtoBuf
	{
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x0600190A RID: 6410 RVA: 0x000580E7 File Offset: 0x000562E7
		// (set) Token: 0x0600190B RID: 6411 RVA: 0x000580EF File Offset: 0x000562EF
		public int Id { get; set; }

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x0600190C RID: 6412 RVA: 0x000580F8 File Offset: 0x000562F8
		// (set) Token: 0x0600190D RID: 6413 RVA: 0x00058100 File Offset: 0x00056300
		public int PlayError { get; set; }

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x0600190E RID: 6414 RVA: 0x00058109 File Offset: 0x00056309
		// (set) Token: 0x0600190F RID: 6415 RVA: 0x00058111 File Offset: 0x00056311
		public int PlayErrorParam
		{
			get
			{
				return this._PlayErrorParam;
			}
			set
			{
				this._PlayErrorParam = value;
				this.HasPlayErrorParam = true;
			}
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x00058124 File Offset: 0x00056324
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			num ^= this.PlayError.GetHashCode();
			if (this.HasPlayErrorParam)
			{
				num ^= this.PlayErrorParam.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x0005817C File Offset: 0x0005637C
		public override bool Equals(object obj)
		{
			TargetOption targetOption = obj as TargetOption;
			return targetOption != null && this.Id.Equals(targetOption.Id) && this.PlayError.Equals(targetOption.PlayError) && this.HasPlayErrorParam == targetOption.HasPlayErrorParam && (!this.HasPlayErrorParam || this.PlayErrorParam.Equals(targetOption.PlayErrorParam));
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x000581F4 File Offset: 0x000563F4
		public void Deserialize(Stream stream)
		{
			TargetOption.Deserialize(stream, this);
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x000581FE File Offset: 0x000563FE
		public static TargetOption Deserialize(Stream stream, TargetOption instance)
		{
			return TargetOption.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x0005820C File Offset: 0x0005640C
		public static TargetOption DeserializeLengthDelimited(Stream stream)
		{
			TargetOption targetOption = new TargetOption();
			TargetOption.DeserializeLengthDelimited(stream, targetOption);
			return targetOption;
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x00058228 File Offset: 0x00056428
		public static TargetOption DeserializeLengthDelimited(Stream stream, TargetOption instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return TargetOption.Deserialize(stream, instance, num);
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x00058250 File Offset: 0x00056450
		public static TargetOption Deserialize(Stream stream, TargetOption instance, long limit)
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
						if (num != 24)
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
							instance.PlayErrorParam = (int)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.PlayError = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Id = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x00058300 File Offset: 0x00056500
		public void Serialize(Stream stream)
		{
			TargetOption.Serialize(stream, this);
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x0005830C File Offset: 0x0005650C
		public static void Serialize(Stream stream, TargetOption instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Id));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PlayError));
			if (instance.HasPlayErrorParam)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PlayErrorParam));
			}
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x00058360 File Offset: 0x00056560
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Id));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.PlayError));
			if (this.HasPlayErrorParam)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.PlayErrorParam));
			}
			return num + 2U;
		}

		// Token: 0x04000960 RID: 2400
		public bool HasPlayErrorParam;

		// Token: 0x04000961 RID: 2401
		private int _PlayErrorParam;
	}
}
