using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001CB RID: 459
	public class PowerHistory : IProtoBuf
	{
		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001D4C RID: 7500 RVA: 0x00067615 File Offset: 0x00065815
		// (set) Token: 0x06001D4D RID: 7501 RVA: 0x0006761D File Offset: 0x0006581D
		public List<PowerHistoryData> List
		{
			get
			{
				return this._List;
			}
			set
			{
				this._List = value;
			}
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x00067628 File Offset: 0x00065828
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (PowerHistoryData powerHistoryData in this.List)
			{
				num ^= powerHistoryData.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x0006768C File Offset: 0x0006588C
		public override bool Equals(object obj)
		{
			PowerHistory powerHistory = obj as PowerHistory;
			if (powerHistory == null)
			{
				return false;
			}
			if (this.List.Count != powerHistory.List.Count)
			{
				return false;
			}
			for (int i = 0; i < this.List.Count; i++)
			{
				if (!this.List[i].Equals(powerHistory.List[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x000676F7 File Offset: 0x000658F7
		public void Deserialize(Stream stream)
		{
			PowerHistory.Deserialize(stream, this);
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x00067701 File Offset: 0x00065901
		public static PowerHistory Deserialize(Stream stream, PowerHistory instance)
		{
			return PowerHistory.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x0006770C File Offset: 0x0006590C
		public static PowerHistory DeserializeLengthDelimited(Stream stream)
		{
			PowerHistory powerHistory = new PowerHistory();
			PowerHistory.DeserializeLengthDelimited(stream, powerHistory);
			return powerHistory;
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x00067728 File Offset: 0x00065928
		public static PowerHistory DeserializeLengthDelimited(Stream stream, PowerHistory instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PowerHistory.Deserialize(stream, instance, num);
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x00067750 File Offset: 0x00065950
		public static PowerHistory Deserialize(Stream stream, PowerHistory instance, long limit)
		{
			if (instance.List == null)
			{
				instance.List = new List<PowerHistoryData>();
			}
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
					instance.List.Add(PowerHistoryData.DeserializeLengthDelimited(stream));
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

		// Token: 0x06001D55 RID: 7509 RVA: 0x000677E8 File Offset: 0x000659E8
		public void Serialize(Stream stream)
		{
			PowerHistory.Serialize(stream, this);
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x000677F4 File Offset: 0x000659F4
		public static void Serialize(Stream stream, PowerHistory instance)
		{
			if (instance.List.Count > 0)
			{
				foreach (PowerHistoryData powerHistoryData in instance.List)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, powerHistoryData.GetSerializedSize());
					PowerHistoryData.Serialize(stream, powerHistoryData);
				}
			}
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x0006786C File Offset: 0x00065A6C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.List.Count > 0)
			{
				foreach (PowerHistoryData powerHistoryData in this.List)
				{
					num += 1U;
					uint serializedSize = powerHistoryData.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04000AAC RID: 2732
		private List<PowerHistoryData> _List = new List<PowerHistoryData>();

		// Token: 0x02000654 RID: 1620
		public enum PacketID
		{
			// Token: 0x0400213D RID: 8509
			ID = 19
		}
	}
}
