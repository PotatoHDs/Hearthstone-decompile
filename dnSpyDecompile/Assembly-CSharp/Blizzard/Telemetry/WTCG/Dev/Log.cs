using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Dev
{
	// Token: 0x0200118B RID: 4491
	public class Log : IProtoBuf
	{
		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x0600C5B3 RID: 50611 RVA: 0x003B84D3 File Offset: 0x003B66D3
		// (set) Token: 0x0600C5B4 RID: 50612 RVA: 0x003B84DB File Offset: 0x003B66DB
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

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x0600C5B5 RID: 50613 RVA: 0x003B84EE File Offset: 0x003B66EE
		// (set) Token: 0x0600C5B6 RID: 50614 RVA: 0x003B84F6 File Offset: 0x003B66F6
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

		// Token: 0x0600C5B7 RID: 50615 RVA: 0x003B850C File Offset: 0x003B670C
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

		// Token: 0x0600C5B8 RID: 50616 RVA: 0x003B8554 File Offset: 0x003B6754
		public override bool Equals(object obj)
		{
			Log log = obj as Log;
			return log != null && this.HasCategory == log.HasCategory && (!this.HasCategory || this.Category.Equals(log.Category)) && this.HasDetails == log.HasDetails && (!this.HasDetails || this.Details.Equals(log.Details));
		}

		// Token: 0x0600C5B9 RID: 50617 RVA: 0x003B85C4 File Offset: 0x003B67C4
		public void Deserialize(Stream stream)
		{
			Log.Deserialize(stream, this);
		}

		// Token: 0x0600C5BA RID: 50618 RVA: 0x003B85CE File Offset: 0x003B67CE
		public static Log Deserialize(Stream stream, Log instance)
		{
			return Log.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C5BB RID: 50619 RVA: 0x003B85DC File Offset: 0x003B67DC
		public static Log DeserializeLengthDelimited(Stream stream)
		{
			Log log = new Log();
			Log.DeserializeLengthDelimited(stream, log);
			return log;
		}

		// Token: 0x0600C5BC RID: 50620 RVA: 0x003B85F8 File Offset: 0x003B67F8
		public static Log DeserializeLengthDelimited(Stream stream, Log instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Log.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C5BD RID: 50621 RVA: 0x003B8620 File Offset: 0x003B6820
		public static Log Deserialize(Stream stream, Log instance, long limit)
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

		// Token: 0x0600C5BE RID: 50622 RVA: 0x003B86B8 File Offset: 0x003B68B8
		public void Serialize(Stream stream)
		{
			Log.Serialize(stream, this);
		}

		// Token: 0x0600C5BF RID: 50623 RVA: 0x003B86C4 File Offset: 0x003B68C4
		public static void Serialize(Stream stream, Log instance)
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

		// Token: 0x0600C5C0 RID: 50624 RVA: 0x003B8720 File Offset: 0x003B6920
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

		// Token: 0x04009DB5 RID: 40373
		public bool HasCategory;

		// Token: 0x04009DB6 RID: 40374
		private string _Category;

		// Token: 0x04009DB7 RID: 40375
		public bool HasDetails;

		// Token: 0x04009DB8 RID: 40376
		private string _Details;
	}
}
