using System;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.report.v1
{
	// Token: 0x0200032D RID: 813
	public class InappropriateBattleTagReport : IProtoBuf
	{
		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x060031E0 RID: 12768 RVA: 0x000A75F3 File Offset: 0x000A57F3
		// (set) Token: 0x060031E1 RID: 12769 RVA: 0x000A75FB File Offset: 0x000A57FB
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

		// Token: 0x060031E2 RID: 12770 RVA: 0x000A760E File Offset: 0x000A580E
		public void SetTarget(GameAccountHandle val)
		{
			this.Target = val;
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x060031E3 RID: 12771 RVA: 0x000A7617 File Offset: 0x000A5817
		// (set) Token: 0x060031E4 RID: 12772 RVA: 0x000A761F File Offset: 0x000A581F
		public string BattleTag
		{
			get
			{
				return this._BattleTag;
			}
			set
			{
				this._BattleTag = value;
				this.HasBattleTag = (value != null);
			}
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x000A7632 File Offset: 0x000A5832
		public void SetBattleTag(string val)
		{
			this.BattleTag = val;
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x000A763C File Offset: 0x000A583C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasTarget)
			{
				num ^= this.Target.GetHashCode();
			}
			if (this.HasBattleTag)
			{
				num ^= this.BattleTag.GetHashCode();
			}
			return num;
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x000A7684 File Offset: 0x000A5884
		public override bool Equals(object obj)
		{
			InappropriateBattleTagReport inappropriateBattleTagReport = obj as InappropriateBattleTagReport;
			return inappropriateBattleTagReport != null && this.HasTarget == inappropriateBattleTagReport.HasTarget && (!this.HasTarget || this.Target.Equals(inappropriateBattleTagReport.Target)) && this.HasBattleTag == inappropriateBattleTagReport.HasBattleTag && (!this.HasBattleTag || this.BattleTag.Equals(inappropriateBattleTagReport.BattleTag));
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x060031E8 RID: 12776 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x000A76F4 File Offset: 0x000A58F4
		public static InappropriateBattleTagReport ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<InappropriateBattleTagReport>(bs, 0, -1);
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x000A76FE File Offset: 0x000A58FE
		public void Deserialize(Stream stream)
		{
			InappropriateBattleTagReport.Deserialize(stream, this);
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x000A7708 File Offset: 0x000A5908
		public static InappropriateBattleTagReport Deserialize(Stream stream, InappropriateBattleTagReport instance)
		{
			return InappropriateBattleTagReport.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060031EC RID: 12780 RVA: 0x000A7714 File Offset: 0x000A5914
		public static InappropriateBattleTagReport DeserializeLengthDelimited(Stream stream)
		{
			InappropriateBattleTagReport inappropriateBattleTagReport = new InappropriateBattleTagReport();
			InappropriateBattleTagReport.DeserializeLengthDelimited(stream, inappropriateBattleTagReport);
			return inappropriateBattleTagReport;
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x000A7730 File Offset: 0x000A5930
		public static InappropriateBattleTagReport DeserializeLengthDelimited(Stream stream, InappropriateBattleTagReport instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return InappropriateBattleTagReport.Deserialize(stream, instance, num);
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x000A7758 File Offset: 0x000A5958
		public static InappropriateBattleTagReport Deserialize(Stream stream, InappropriateBattleTagReport instance, long limit)
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
						instance.BattleTag = ProtocolParser.ReadString(stream);
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

		// Token: 0x060031EF RID: 12783 RVA: 0x000A780A File Offset: 0x000A5A0A
		public void Serialize(Stream stream)
		{
			InappropriateBattleTagReport.Serialize(stream, this);
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x000A7814 File Offset: 0x000A5A14
		public static void Serialize(Stream stream, InappropriateBattleTagReport instance)
		{
			if (instance.HasTarget)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Target.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.Target);
			}
			if (instance.HasBattleTag)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattleTag));
			}
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x000A7874 File Offset: 0x000A5A74
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasTarget)
			{
				num += 1U;
				uint serializedSize = this.Target.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasBattleTag)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.BattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x040013C1 RID: 5057
		public bool HasTarget;

		// Token: 0x040013C2 RID: 5058
		private GameAccountHandle _Target;

		// Token: 0x040013C3 RID: 5059
		public bool HasBattleTag;

		// Token: 0x040013C4 RID: 5060
		private string _BattleTag;
	}
}
