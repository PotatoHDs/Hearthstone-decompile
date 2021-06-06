using System;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.report.v1
{
	// Token: 0x0200032B RID: 811
	public class HarassmentReport : IProtoBuf
	{
		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x060031BA RID: 12730 RVA: 0x000A703A File Offset: 0x000A523A
		// (set) Token: 0x060031BB RID: 12731 RVA: 0x000A7042 File Offset: 0x000A5242
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

		// Token: 0x060031BC RID: 12732 RVA: 0x000A7055 File Offset: 0x000A5255
		public void SetTarget(GameAccountHandle val)
		{
			this.Target = val;
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x060031BD RID: 12733 RVA: 0x000A705E File Offset: 0x000A525E
		// (set) Token: 0x060031BE RID: 12734 RVA: 0x000A7066 File Offset: 0x000A5266
		public string Text
		{
			get
			{
				return this._Text;
			}
			set
			{
				this._Text = value;
				this.HasText = (value != null);
			}
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x000A7079 File Offset: 0x000A5279
		public void SetText(string val)
		{
			this.Text = val;
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x000A7084 File Offset: 0x000A5284
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasTarget)
			{
				num ^= this.Target.GetHashCode();
			}
			if (this.HasText)
			{
				num ^= this.Text.GetHashCode();
			}
			return num;
		}

		// Token: 0x060031C1 RID: 12737 RVA: 0x000A70CC File Offset: 0x000A52CC
		public override bool Equals(object obj)
		{
			HarassmentReport harassmentReport = obj as HarassmentReport;
			return harassmentReport != null && this.HasTarget == harassmentReport.HasTarget && (!this.HasTarget || this.Target.Equals(harassmentReport.Target)) && this.HasText == harassmentReport.HasText && (!this.HasText || this.Text.Equals(harassmentReport.Text));
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x060031C2 RID: 12738 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x000A713C File Offset: 0x000A533C
		public static HarassmentReport ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<HarassmentReport>(bs, 0, -1);
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x000A7146 File Offset: 0x000A5346
		public void Deserialize(Stream stream)
		{
			HarassmentReport.Deserialize(stream, this);
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x000A7150 File Offset: 0x000A5350
		public static HarassmentReport Deserialize(Stream stream, HarassmentReport instance)
		{
			return HarassmentReport.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x000A715C File Offset: 0x000A535C
		public static HarassmentReport DeserializeLengthDelimited(Stream stream)
		{
			HarassmentReport harassmentReport = new HarassmentReport();
			HarassmentReport.DeserializeLengthDelimited(stream, harassmentReport);
			return harassmentReport;
		}

		// Token: 0x060031C7 RID: 12743 RVA: 0x000A7178 File Offset: 0x000A5378
		public static HarassmentReport DeserializeLengthDelimited(Stream stream, HarassmentReport instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return HarassmentReport.Deserialize(stream, instance, num);
		}

		// Token: 0x060031C8 RID: 12744 RVA: 0x000A71A0 File Offset: 0x000A53A0
		public static HarassmentReport Deserialize(Stream stream, HarassmentReport instance, long limit)
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
						instance.Text = ProtocolParser.ReadString(stream);
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

		// Token: 0x060031C9 RID: 12745 RVA: 0x000A7252 File Offset: 0x000A5452
		public void Serialize(Stream stream)
		{
			HarassmentReport.Serialize(stream, this);
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x000A725C File Offset: 0x000A545C
		public static void Serialize(Stream stream, HarassmentReport instance)
		{
			if (instance.HasTarget)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Target.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.Target);
			}
			if (instance.HasText)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Text));
			}
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x000A72BC File Offset: 0x000A54BC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasTarget)
			{
				num += 1U;
				uint serializedSize = this.Target.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasText)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Text);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x040013B9 RID: 5049
		public bool HasTarget;

		// Token: 0x040013BA RID: 5050
		private GameAccountHandle _Target;

		// Token: 0x040013BB RID: 5051
		public bool HasText;

		// Token: 0x040013BC RID: 5052
		private string _Text;
	}
}
