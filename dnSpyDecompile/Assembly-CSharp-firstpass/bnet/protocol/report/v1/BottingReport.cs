using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.report.v1
{
	// Token: 0x0200032F RID: 815
	public class BottingReport : IProtoBuf
	{
		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06003203 RID: 12803 RVA: 0x000A7AD3 File Offset: 0x000A5CD3
		// (set) Token: 0x06003204 RID: 12804 RVA: 0x000A7ADB File Offset: 0x000A5CDB
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

		// Token: 0x06003205 RID: 12805 RVA: 0x000A7AEE File Offset: 0x000A5CEE
		public void SetTarget(GameAccountHandle val)
		{
			this.Target = val;
		}

		// Token: 0x06003206 RID: 12806 RVA: 0x000A7AF8 File Offset: 0x000A5CF8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasTarget)
			{
				num ^= this.Target.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003207 RID: 12807 RVA: 0x000A7B28 File Offset: 0x000A5D28
		public override bool Equals(object obj)
		{
			BottingReport bottingReport = obj as BottingReport;
			return bottingReport != null && this.HasTarget == bottingReport.HasTarget && (!this.HasTarget || this.Target.Equals(bottingReport.Target));
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06003208 RID: 12808 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x000A7B6D File Offset: 0x000A5D6D
		public static BottingReport ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<BottingReport>(bs, 0, -1);
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x000A7B77 File Offset: 0x000A5D77
		public void Deserialize(Stream stream)
		{
			BottingReport.Deserialize(stream, this);
		}

		// Token: 0x0600320B RID: 12811 RVA: 0x000A7B81 File Offset: 0x000A5D81
		public static BottingReport Deserialize(Stream stream, BottingReport instance)
		{
			return BottingReport.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600320C RID: 12812 RVA: 0x000A7B8C File Offset: 0x000A5D8C
		public static BottingReport DeserializeLengthDelimited(Stream stream)
		{
			BottingReport bottingReport = new BottingReport();
			BottingReport.DeserializeLengthDelimited(stream, bottingReport);
			return bottingReport;
		}

		// Token: 0x0600320D RID: 12813 RVA: 0x000A7BA8 File Offset: 0x000A5DA8
		public static BottingReport DeserializeLengthDelimited(Stream stream, BottingReport instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BottingReport.Deserialize(stream, instance, num);
		}

		// Token: 0x0600320E RID: 12814 RVA: 0x000A7BD0 File Offset: 0x000A5DD0
		public static BottingReport Deserialize(Stream stream, BottingReport instance, long limit)
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

		// Token: 0x0600320F RID: 12815 RVA: 0x000A7C6A File Offset: 0x000A5E6A
		public void Serialize(Stream stream)
		{
			BottingReport.Serialize(stream, this);
		}

		// Token: 0x06003210 RID: 12816 RVA: 0x000A7C73 File Offset: 0x000A5E73
		public static void Serialize(Stream stream, BottingReport instance)
		{
			if (instance.HasTarget)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Target.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.Target);
			}
		}

		// Token: 0x06003211 RID: 12817 RVA: 0x000A7CA4 File Offset: 0x000A5EA4
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

		// Token: 0x040013C7 RID: 5063
		public bool HasTarget;

		// Token: 0x040013C8 RID: 5064
		private GameAccountHandle _Target;
	}
}
