using System;
using System.IO;
using bnet.protocol.Types;

namespace bnet.protocol.url_filter.v1.admin
{
	// Token: 0x020002D1 RID: 721
	public class CheckURLResponse : IProtoBuf
	{
		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06002A52 RID: 10834 RVA: 0x000938E0 File Offset: 0x00091AE0
		// (set) Token: 0x06002A53 RID: 10835 RVA: 0x000938E8 File Offset: 0x00091AE8
		public ThreatType ThreatType
		{
			get
			{
				return this._ThreatType;
			}
			set
			{
				this._ThreatType = value;
				this.HasThreatType = true;
			}
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x000938F8 File Offset: 0x00091AF8
		public void SetThreatType(ThreatType val)
		{
			this.ThreatType = val;
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x00093904 File Offset: 0x00091B04
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasThreatType)
			{
				num ^= this.ThreatType.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x00093940 File Offset: 0x00091B40
		public override bool Equals(object obj)
		{
			CheckURLResponse checkURLResponse = obj as CheckURLResponse;
			return checkURLResponse != null && this.HasThreatType == checkURLResponse.HasThreatType && (!this.HasThreatType || this.ThreatType.Equals(checkURLResponse.ThreatType));
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06002A57 RID: 10839 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x00093993 File Offset: 0x00091B93
		public static CheckURLResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CheckURLResponse>(bs, 0, -1);
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x0009399D File Offset: 0x00091B9D
		public void Deserialize(Stream stream)
		{
			CheckURLResponse.Deserialize(stream, this);
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x000939A7 File Offset: 0x00091BA7
		public static CheckURLResponse Deserialize(Stream stream, CheckURLResponse instance)
		{
			return CheckURLResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x000939B4 File Offset: 0x00091BB4
		public static CheckURLResponse DeserializeLengthDelimited(Stream stream)
		{
			CheckURLResponse checkURLResponse = new CheckURLResponse();
			CheckURLResponse.DeserializeLengthDelimited(stream, checkURLResponse);
			return checkURLResponse;
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x000939D0 File Offset: 0x00091BD0
		public static CheckURLResponse DeserializeLengthDelimited(Stream stream, CheckURLResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CheckURLResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x000939F8 File Offset: 0x00091BF8
		public static CheckURLResponse Deserialize(Stream stream, CheckURLResponse instance, long limit)
		{
			instance.ThreatType = ThreatType.THREAT_TYPE_OK;
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
				else if (num == 8)
				{
					instance.ThreatType = (ThreatType)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06002A5E RID: 10846 RVA: 0x00093A7F File Offset: 0x00091C7F
		public void Serialize(Stream stream)
		{
			CheckURLResponse.Serialize(stream, this);
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x00093A88 File Offset: 0x00091C88
		public static void Serialize(Stream stream, CheckURLResponse instance)
		{
			if (instance.HasThreatType)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ThreatType));
			}
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x00093AA8 File Offset: 0x00091CA8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasThreatType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ThreatType));
			}
			return num;
		}

		// Token: 0x040011F4 RID: 4596
		public bool HasThreatType;

		// Token: 0x040011F5 RID: 4597
		private ThreatType _ThreatType;
	}
}
