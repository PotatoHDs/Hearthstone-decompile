using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.report.v1
{
	// Token: 0x0200032E RID: 814
	public class HackingReport : IProtoBuf
	{
		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x060031F3 RID: 12787 RVA: 0x000A78CF File Offset: 0x000A5ACF
		// (set) Token: 0x060031F4 RID: 12788 RVA: 0x000A78D7 File Offset: 0x000A5AD7
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

		// Token: 0x060031F5 RID: 12789 RVA: 0x000A78EA File Offset: 0x000A5AEA
		public void SetTarget(GameAccountHandle val)
		{
			this.Target = val;
		}

		// Token: 0x060031F6 RID: 12790 RVA: 0x000A78F4 File Offset: 0x000A5AF4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasTarget)
			{
				num ^= this.Target.GetHashCode();
			}
			return num;
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x000A7924 File Offset: 0x000A5B24
		public override bool Equals(object obj)
		{
			HackingReport hackingReport = obj as HackingReport;
			return hackingReport != null && this.HasTarget == hackingReport.HasTarget && (!this.HasTarget || this.Target.Equals(hackingReport.Target));
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x060031F8 RID: 12792 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x000A7969 File Offset: 0x000A5B69
		public static HackingReport ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<HackingReport>(bs, 0, -1);
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x000A7973 File Offset: 0x000A5B73
		public void Deserialize(Stream stream)
		{
			HackingReport.Deserialize(stream, this);
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x000A797D File Offset: 0x000A5B7D
		public static HackingReport Deserialize(Stream stream, HackingReport instance)
		{
			return HackingReport.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x000A7988 File Offset: 0x000A5B88
		public static HackingReport DeserializeLengthDelimited(Stream stream)
		{
			HackingReport hackingReport = new HackingReport();
			HackingReport.DeserializeLengthDelimited(stream, hackingReport);
			return hackingReport;
		}

		// Token: 0x060031FD RID: 12797 RVA: 0x000A79A4 File Offset: 0x000A5BA4
		public static HackingReport DeserializeLengthDelimited(Stream stream, HackingReport instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return HackingReport.Deserialize(stream, instance, num);
		}

		// Token: 0x060031FE RID: 12798 RVA: 0x000A79CC File Offset: 0x000A5BCC
		public static HackingReport Deserialize(Stream stream, HackingReport instance, long limit)
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
					if (instance.Target == null)
					{
						instance.Target = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.Target);
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

		// Token: 0x060031FF RID: 12799 RVA: 0x000A7A66 File Offset: 0x000A5C66
		public void Serialize(Stream stream)
		{
			HackingReport.Serialize(stream, this);
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x000A7A6F File Offset: 0x000A5C6F
		public static void Serialize(Stream stream, HackingReport instance)
		{
			if (instance.HasTarget)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Target.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.Target);
			}
		}

		// Token: 0x06003201 RID: 12801 RVA: 0x000A7AA0 File Offset: 0x000A5CA0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasTarget)
			{
				num += 1U;
				uint serializedSize = this.Target.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x040013C5 RID: 5061
		public bool HasTarget;

		// Token: 0x040013C6 RID: 5062
		private GameAccountHandle _Target;
	}
}
