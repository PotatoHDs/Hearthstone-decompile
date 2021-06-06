using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011D4 RID: 4564
	public class LiveIssue : IProtoBuf
	{
		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x0600CB48 RID: 52040 RVA: 0x003CCF30 File Offset: 0x003CB130
		// (set) Token: 0x0600CB49 RID: 52041 RVA: 0x003CCF38 File Offset: 0x003CB138
		public string Category
		{
			get
			{
				return this._Category;
			}
			set
			{
				this._Category = value;
				this.HasCategory = (value != null);
			}
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x0600CB4A RID: 52042 RVA: 0x003CCF4B File Offset: 0x003CB14B
		// (set) Token: 0x0600CB4B RID: 52043 RVA: 0x003CCF53 File Offset: 0x003CB153
		public string Details
		{
			get
			{
				return this._Details;
			}
			set
			{
				this._Details = value;
				this.HasDetails = (value != null);
			}
		}

		// Token: 0x0600CB4C RID: 52044 RVA: 0x003CCF68 File Offset: 0x003CB168
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasCategory)
			{
				num ^= this.Category.GetHashCode();
			}
			if (this.HasDetails)
			{
				num ^= this.Details.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CB4D RID: 52045 RVA: 0x003CCFB0 File Offset: 0x003CB1B0
		public override bool Equals(object obj)
		{
			LiveIssue liveIssue = obj as LiveIssue;
			return liveIssue != null && this.HasCategory == liveIssue.HasCategory && (!this.HasCategory || this.Category.Equals(liveIssue.Category)) && this.HasDetails == liveIssue.HasDetails && (!this.HasDetails || this.Details.Equals(liveIssue.Details));
		}

		// Token: 0x0600CB4E RID: 52046 RVA: 0x003CD020 File Offset: 0x003CB220
		public void Deserialize(Stream stream)
		{
			LiveIssue.Deserialize(stream, this);
		}

		// Token: 0x0600CB4F RID: 52047 RVA: 0x003CD02A File Offset: 0x003CB22A
		public static LiveIssue Deserialize(Stream stream, LiveIssue instance)
		{
			return LiveIssue.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CB50 RID: 52048 RVA: 0x003CD038 File Offset: 0x003CB238
		public static LiveIssue DeserializeLengthDelimited(Stream stream)
		{
			LiveIssue liveIssue = new LiveIssue();
			LiveIssue.DeserializeLengthDelimited(stream, liveIssue);
			return liveIssue;
		}

		// Token: 0x0600CB51 RID: 52049 RVA: 0x003CD054 File Offset: 0x003CB254
		public static LiveIssue DeserializeLengthDelimited(Stream stream, LiveIssue instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return LiveIssue.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CB52 RID: 52050 RVA: 0x003CD07C File Offset: 0x003CB27C
		public static LiveIssue Deserialize(Stream stream, LiveIssue instance, long limit)
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
				else if (num != 10)
				{
					if (num != 18)
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
						instance.Details = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.Category = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600CB53 RID: 52051 RVA: 0x003CD114 File Offset: 0x003CB314
		public void Serialize(Stream stream)
		{
			LiveIssue.Serialize(stream, this);
		}

		// Token: 0x0600CB54 RID: 52052 RVA: 0x003CD120 File Offset: 0x003CB320
		public static void Serialize(Stream stream, LiveIssue instance)
		{
			if (instance.HasCategory)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Category));
			}
			if (instance.HasDetails)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Details));
			}
		}

		// Token: 0x0600CB55 RID: 52053 RVA: 0x003CD17C File Offset: 0x003CB37C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasCategory)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Category);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasDetails)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Details);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x0400A025 RID: 40997
		public bool HasCategory;

		// Token: 0x0400A026 RID: 40998
		private string _Category;

		// Token: 0x0400A027 RID: 40999
		public bool HasDetails;

		// Token: 0x0400A028 RID: 41000
		private string _Details;
	}
}
