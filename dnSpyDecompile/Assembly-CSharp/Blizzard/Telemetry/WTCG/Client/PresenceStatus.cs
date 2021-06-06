using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011E5 RID: 4581
	public class PresenceStatus : IProtoBuf
	{
		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x0600CC77 RID: 52343 RVA: 0x003D1031 File Offset: 0x003CF231
		// (set) Token: 0x0600CC78 RID: 52344 RVA: 0x003D1039 File Offset: 0x003CF239
		public long PresenceId
		{
			get
			{
				return this._PresenceId;
			}
			set
			{
				this._PresenceId = value;
				this.HasPresenceId = true;
			}
		}

		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x0600CC79 RID: 52345 RVA: 0x003D1049 File Offset: 0x003CF249
		// (set) Token: 0x0600CC7A RID: 52346 RVA: 0x003D1051 File Offset: 0x003CF251
		public long PresenceSubId
		{
			get
			{
				return this._PresenceSubId;
			}
			set
			{
				this._PresenceSubId = value;
				this.HasPresenceSubId = true;
			}
		}

		// Token: 0x0600CC7B RID: 52347 RVA: 0x003D1064 File Offset: 0x003CF264
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPresenceId)
			{
				num ^= this.PresenceId.GetHashCode();
			}
			if (this.HasPresenceSubId)
			{
				num ^= this.PresenceSubId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CC7C RID: 52348 RVA: 0x003D10B0 File Offset: 0x003CF2B0
		public override bool Equals(object obj)
		{
			PresenceStatus presenceStatus = obj as PresenceStatus;
			return presenceStatus != null && this.HasPresenceId == presenceStatus.HasPresenceId && (!this.HasPresenceId || this.PresenceId.Equals(presenceStatus.PresenceId)) && this.HasPresenceSubId == presenceStatus.HasPresenceSubId && (!this.HasPresenceSubId || this.PresenceSubId.Equals(presenceStatus.PresenceSubId));
		}

		// Token: 0x0600CC7D RID: 52349 RVA: 0x003D1126 File Offset: 0x003CF326
		public void Deserialize(Stream stream)
		{
			PresenceStatus.Deserialize(stream, this);
		}

		// Token: 0x0600CC7E RID: 52350 RVA: 0x003D1130 File Offset: 0x003CF330
		public static PresenceStatus Deserialize(Stream stream, PresenceStatus instance)
		{
			return PresenceStatus.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CC7F RID: 52351 RVA: 0x003D113C File Offset: 0x003CF33C
		public static PresenceStatus DeserializeLengthDelimited(Stream stream)
		{
			PresenceStatus presenceStatus = new PresenceStatus();
			PresenceStatus.DeserializeLengthDelimited(stream, presenceStatus);
			return presenceStatus;
		}

		// Token: 0x0600CC80 RID: 52352 RVA: 0x003D1158 File Offset: 0x003CF358
		public static PresenceStatus DeserializeLengthDelimited(Stream stream, PresenceStatus instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PresenceStatus.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CC81 RID: 52353 RVA: 0x003D1180 File Offset: 0x003CF380
		public static PresenceStatus Deserialize(Stream stream, PresenceStatus instance, long limit)
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
						instance.PresenceSubId = (long)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.PresenceId = (long)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600CC82 RID: 52354 RVA: 0x003D1217 File Offset: 0x003CF417
		public void Serialize(Stream stream)
		{
			PresenceStatus.Serialize(stream, this);
		}

		// Token: 0x0600CC83 RID: 52355 RVA: 0x003D1220 File Offset: 0x003CF420
		public static void Serialize(Stream stream, PresenceStatus instance)
		{
			if (instance.HasPresenceId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PresenceId);
			}
			if (instance.HasPresenceSubId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PresenceSubId);
			}
		}

		// Token: 0x0600CC84 RID: 52356 RVA: 0x003D125C File Offset: 0x003CF45C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasPresenceId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.PresenceId);
			}
			if (this.HasPresenceSubId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.PresenceSubId);
			}
			return num;
		}

		// Token: 0x0400A099 RID: 41113
		public bool HasPresenceId;

		// Token: 0x0400A09A RID: 41114
		private long _PresenceId;

		// Token: 0x0400A09B RID: 41115
		public bool HasPresenceSubId;

		// Token: 0x0400A09C RID: 41116
		private long _PresenceSubId;
	}
}
