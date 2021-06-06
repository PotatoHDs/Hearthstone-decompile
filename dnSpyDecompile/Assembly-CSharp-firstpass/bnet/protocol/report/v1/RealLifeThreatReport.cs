using System;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.report.v1
{
	// Token: 0x0200032C RID: 812
	public class RealLifeThreatReport : IProtoBuf
	{
		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x060031CD RID: 12749 RVA: 0x000A7317 File Offset: 0x000A5517
		// (set) Token: 0x060031CE RID: 12750 RVA: 0x000A731F File Offset: 0x000A551F
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

		// Token: 0x060031CF RID: 12751 RVA: 0x000A7332 File Offset: 0x000A5532
		public void SetTarget(GameAccountHandle val)
		{
			this.Target = val;
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x060031D0 RID: 12752 RVA: 0x000A733B File Offset: 0x000A553B
		// (set) Token: 0x060031D1 RID: 12753 RVA: 0x000A7343 File Offset: 0x000A5543
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

		// Token: 0x060031D2 RID: 12754 RVA: 0x000A7356 File Offset: 0x000A5556
		public void SetText(string val)
		{
			this.Text = val;
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x000A7360 File Offset: 0x000A5560
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

		// Token: 0x060031D4 RID: 12756 RVA: 0x000A73A8 File Offset: 0x000A55A8
		public override bool Equals(object obj)
		{
			RealLifeThreatReport realLifeThreatReport = obj as RealLifeThreatReport;
			return realLifeThreatReport != null && this.HasTarget == realLifeThreatReport.HasTarget && (!this.HasTarget || this.Target.Equals(realLifeThreatReport.Target)) && this.HasText == realLifeThreatReport.HasText && (!this.HasText || this.Text.Equals(realLifeThreatReport.Text));
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x060031D5 RID: 12757 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x000A7418 File Offset: 0x000A5618
		public static RealLifeThreatReport ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RealLifeThreatReport>(bs, 0, -1);
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x000A7422 File Offset: 0x000A5622
		public void Deserialize(Stream stream)
		{
			RealLifeThreatReport.Deserialize(stream, this);
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x000A742C File Offset: 0x000A562C
		public static RealLifeThreatReport Deserialize(Stream stream, RealLifeThreatReport instance)
		{
			return RealLifeThreatReport.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x000A7438 File Offset: 0x000A5638
		public static RealLifeThreatReport DeserializeLengthDelimited(Stream stream)
		{
			RealLifeThreatReport realLifeThreatReport = new RealLifeThreatReport();
			RealLifeThreatReport.DeserializeLengthDelimited(stream, realLifeThreatReport);
			return realLifeThreatReport;
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x000A7454 File Offset: 0x000A5654
		public static RealLifeThreatReport DeserializeLengthDelimited(Stream stream, RealLifeThreatReport instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RealLifeThreatReport.Deserialize(stream, instance, num);
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x000A747C File Offset: 0x000A567C
		public static RealLifeThreatReport Deserialize(Stream stream, RealLifeThreatReport instance, long limit)
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

		// Token: 0x060031DC RID: 12764 RVA: 0x000A752E File Offset: 0x000A572E
		public void Serialize(Stream stream)
		{
			RealLifeThreatReport.Serialize(stream, this);
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x000A7538 File Offset: 0x000A5738
		public static void Serialize(Stream stream, RealLifeThreatReport instance)
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

		// Token: 0x060031DE RID: 12766 RVA: 0x000A7598 File Offset: 0x000A5798
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

		// Token: 0x040013BD RID: 5053
		public bool HasTarget;

		// Token: 0x040013BE RID: 5054
		private GameAccountHandle _Target;

		// Token: 0x040013BF RID: 5055
		public bool HasText;

		// Token: 0x040013C0 RID: 5056
		private string _Text;
	}
}
