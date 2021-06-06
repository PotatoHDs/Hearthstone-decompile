using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011C1 RID: 4545
	public class FlowPerformanceBattlegrounds : IProtoBuf
	{
		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x0600C9D3 RID: 51667 RVA: 0x003C79A8 File Offset: 0x003C5BA8
		// (set) Token: 0x0600C9D4 RID: 51668 RVA: 0x003C79B0 File Offset: 0x003C5BB0
		public string FlowId
		{
			get
			{
				return this._FlowId;
			}
			set
			{
				this._FlowId = value;
				this.HasFlowId = (value != null);
			}
		}

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x0600C9D5 RID: 51669 RVA: 0x003C79C3 File Offset: 0x003C5BC3
		// (set) Token: 0x0600C9D6 RID: 51670 RVA: 0x003C79CB File Offset: 0x003C5BCB
		public string GameUuid
		{
			get
			{
				return this._GameUuid;
			}
			set
			{
				this._GameUuid = value;
				this.HasGameUuid = (value != null);
			}
		}

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x0600C9D7 RID: 51671 RVA: 0x003C79DE File Offset: 0x003C5BDE
		// (set) Token: 0x0600C9D8 RID: 51672 RVA: 0x003C79E6 File Offset: 0x003C5BE6
		public int TotalRounds
		{
			get
			{
				return this._TotalRounds;
			}
			set
			{
				this._TotalRounds = value;
				this.HasTotalRounds = true;
			}
		}

		// Token: 0x0600C9D9 RID: 51673 RVA: 0x003C79F8 File Offset: 0x003C5BF8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasFlowId)
			{
				num ^= this.FlowId.GetHashCode();
			}
			if (this.HasGameUuid)
			{
				num ^= this.GameUuid.GetHashCode();
			}
			if (this.HasTotalRounds)
			{
				num ^= this.TotalRounds.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C9DA RID: 51674 RVA: 0x003C7A58 File Offset: 0x003C5C58
		public override bool Equals(object obj)
		{
			FlowPerformanceBattlegrounds flowPerformanceBattlegrounds = obj as FlowPerformanceBattlegrounds;
			return flowPerformanceBattlegrounds != null && this.HasFlowId == flowPerformanceBattlegrounds.HasFlowId && (!this.HasFlowId || this.FlowId.Equals(flowPerformanceBattlegrounds.FlowId)) && this.HasGameUuid == flowPerformanceBattlegrounds.HasGameUuid && (!this.HasGameUuid || this.GameUuid.Equals(flowPerformanceBattlegrounds.GameUuid)) && this.HasTotalRounds == flowPerformanceBattlegrounds.HasTotalRounds && (!this.HasTotalRounds || this.TotalRounds.Equals(flowPerformanceBattlegrounds.TotalRounds));
		}

		// Token: 0x0600C9DB RID: 51675 RVA: 0x003C7AF6 File Offset: 0x003C5CF6
		public void Deserialize(Stream stream)
		{
			FlowPerformanceBattlegrounds.Deserialize(stream, this);
		}

		// Token: 0x0600C9DC RID: 51676 RVA: 0x003C7B00 File Offset: 0x003C5D00
		public static FlowPerformanceBattlegrounds Deserialize(Stream stream, FlowPerformanceBattlegrounds instance)
		{
			return FlowPerformanceBattlegrounds.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C9DD RID: 51677 RVA: 0x003C7B0C File Offset: 0x003C5D0C
		public static FlowPerformanceBattlegrounds DeserializeLengthDelimited(Stream stream)
		{
			FlowPerformanceBattlegrounds flowPerformanceBattlegrounds = new FlowPerformanceBattlegrounds();
			FlowPerformanceBattlegrounds.DeserializeLengthDelimited(stream, flowPerformanceBattlegrounds);
			return flowPerformanceBattlegrounds;
		}

		// Token: 0x0600C9DE RID: 51678 RVA: 0x003C7B28 File Offset: 0x003C5D28
		public static FlowPerformanceBattlegrounds DeserializeLengthDelimited(Stream stream, FlowPerformanceBattlegrounds instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FlowPerformanceBattlegrounds.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C9DF RID: 51679 RVA: 0x003C7B50 File Offset: 0x003C5D50
		public static FlowPerformanceBattlegrounds Deserialize(Stream stream, FlowPerformanceBattlegrounds instance, long limit)
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
					if (num != 16)
					{
						if (num != 34)
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
							instance.GameUuid = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.TotalRounds = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.FlowId = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600C9E0 RID: 51680 RVA: 0x003C7BFF File Offset: 0x003C5DFF
		public void Serialize(Stream stream)
		{
			FlowPerformanceBattlegrounds.Serialize(stream, this);
		}

		// Token: 0x0600C9E1 RID: 51681 RVA: 0x003C7C08 File Offset: 0x003C5E08
		public static void Serialize(Stream stream, FlowPerformanceBattlegrounds instance)
		{
			if (instance.HasFlowId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FlowId));
			}
			if (instance.HasGameUuid)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.GameUuid));
			}
			if (instance.HasTotalRounds)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.TotalRounds));
			}
		}

		// Token: 0x0600C9E2 RID: 51682 RVA: 0x003C7C80 File Offset: 0x003C5E80
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasFlowId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.FlowId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasGameUuid)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.GameUuid);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasTotalRounds)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.TotalRounds));
			}
			return num;
		}

		// Token: 0x04009F81 RID: 40833
		public bool HasFlowId;

		// Token: 0x04009F82 RID: 40834
		private string _FlowId;

		// Token: 0x04009F83 RID: 40835
		public bool HasGameUuid;

		// Token: 0x04009F84 RID: 40836
		private string _GameUuid;

		// Token: 0x04009F85 RID: 40837
		public bool HasTotalRounds;

		// Token: 0x04009F86 RID: 40838
		private int _TotalRounds;
	}
}
