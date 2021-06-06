using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.report.v1
{
	// Token: 0x0200032A RID: 810
	public class SpamReport : IProtoBuf
	{
		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x060031A7 RID: 12711 RVA: 0x000A6D57 File Offset: 0x000A4F57
		// (set) Token: 0x060031A8 RID: 12712 RVA: 0x000A6D5F File Offset: 0x000A4F5F
		public GameAccountHandle Target
		{
			get
			{
				return this._Target;
			}
			set
			{
				this._Target = value;
				this.HasTarget = (value != null);
			}
		}

		// Token: 0x060031A9 RID: 12713 RVA: 0x000A6D72 File Offset: 0x000A4F72
		public void SetTarget(GameAccountHandle val)
		{
			this.Target = val;
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x060031AA RID: 12714 RVA: 0x000A6D7B File Offset: 0x000A4F7B
		// (set) Token: 0x060031AB RID: 12715 RVA: 0x000A6D83 File Offset: 0x000A4F83
		public SpamReport.Types.SpamSource Source
		{
			get
			{
				return this._Source;
			}
			set
			{
				this._Source = value;
				this.HasSource = true;
			}
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x000A6D93 File Offset: 0x000A4F93
		public void SetSource(SpamReport.Types.SpamSource val)
		{
			this.Source = val;
		}

		// Token: 0x060031AD RID: 12717 RVA: 0x000A6D9C File Offset: 0x000A4F9C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasTarget)
			{
				num ^= this.Target.GetHashCode();
			}
			if (this.HasSource)
			{
				num ^= this.Source.GetHashCode();
			}
			return num;
		}

		// Token: 0x060031AE RID: 12718 RVA: 0x000A6DEC File Offset: 0x000A4FEC
		public override bool Equals(object obj)
		{
			SpamReport spamReport = obj as SpamReport;
			return spamReport != null && this.HasTarget == spamReport.HasTarget && (!this.HasTarget || this.Target.Equals(spamReport.Target)) && this.HasSource == spamReport.HasSource && (!this.HasSource || this.Source.Equals(spamReport.Source));
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x060031AF RID: 12719 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x000A6E6A File Offset: 0x000A506A
		public static SpamReport ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SpamReport>(bs, 0, -1);
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x000A6E74 File Offset: 0x000A5074
		public void Deserialize(Stream stream)
		{
			SpamReport.Deserialize(stream, this);
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x000A6E7E File Offset: 0x000A507E
		public static SpamReport Deserialize(Stream stream, SpamReport instance)
		{
			return SpamReport.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x000A6E8C File Offset: 0x000A508C
		public static SpamReport DeserializeLengthDelimited(Stream stream)
		{
			SpamReport spamReport = new SpamReport();
			SpamReport.DeserializeLengthDelimited(stream, spamReport);
			return spamReport;
		}

		// Token: 0x060031B4 RID: 12724 RVA: 0x000A6EA8 File Offset: 0x000A50A8
		public static SpamReport DeserializeLengthDelimited(Stream stream, SpamReport instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SpamReport.Deserialize(stream, instance, num);
		}

		// Token: 0x060031B5 RID: 12725 RVA: 0x000A6ED0 File Offset: 0x000A50D0
		public static SpamReport Deserialize(Stream stream, SpamReport instance, long limit)
		{
			instance.Source = SpamReport.Types.SpamSource.OTHER;
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
						instance.Source = (SpamReport.Types.SpamSource)ProtocolParser.ReadUInt64(stream);
					}
				}
				else if (instance.Target == null)
				{
					instance.Target = GameAccountHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountHandle.DeserializeLengthDelimited(stream, instance.Target);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x000A6F8A File Offset: 0x000A518A
		public void Serialize(Stream stream)
		{
			SpamReport.Serialize(stream, this);
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x000A6F94 File Offset: 0x000A5194
		public static void Serialize(Stream stream, SpamReport instance)
		{
			if (instance.HasTarget)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Target.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.Target);
			}
			if (instance.HasSource)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Source));
			}
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x000A6FEC File Offset: 0x000A51EC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasTarget)
			{
				num += 1U;
				uint serializedSize = this.Target.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasSource)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Source));
			}
			return num;
		}

		// Token: 0x040013B5 RID: 5045
		public bool HasTarget;

		// Token: 0x040013B6 RID: 5046
		private GameAccountHandle _Target;

		// Token: 0x040013B7 RID: 5047
		public bool HasSource;

		// Token: 0x040013B8 RID: 5048
		private SpamReport.Types.SpamSource _Source;

		// Token: 0x020006FB RID: 1787
		public static class Types
		{
			// Token: 0x02000712 RID: 1810
			public enum SpamSource
			{
				// Token: 0x040022FB RID: 8955
				OTHER = 1,
				// Token: 0x040022FC RID: 8956
				FRIEND_INVITATION,
				// Token: 0x040022FD RID: 8957
				WHISPER,
				// Token: 0x040022FE RID: 8958
				CHAT
			}
		}
	}
}
