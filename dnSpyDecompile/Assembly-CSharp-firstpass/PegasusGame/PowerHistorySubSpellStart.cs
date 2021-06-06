using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusGame
{
	// Token: 0x020001C5 RID: 453
	public class PowerHistorySubSpellStart : IProtoBuf
	{
		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001CD4 RID: 7380 RVA: 0x000659EA File Offset: 0x00063BEA
		// (set) Token: 0x06001CD5 RID: 7381 RVA: 0x000659F2 File Offset: 0x00063BF2
		public string SpellPrefabGuid { get; set; }

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001CD6 RID: 7382 RVA: 0x000659FB File Offset: 0x00063BFB
		// (set) Token: 0x06001CD7 RID: 7383 RVA: 0x00065A03 File Offset: 0x00063C03
		public int SourceEntityId
		{
			get
			{
				return this._SourceEntityId;
			}
			set
			{
				this._SourceEntityId = value;
				this.HasSourceEntityId = true;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001CD8 RID: 7384 RVA: 0x00065A13 File Offset: 0x00063C13
		// (set) Token: 0x06001CD9 RID: 7385 RVA: 0x00065A1B File Offset: 0x00063C1B
		public List<int> TargetEntityIds
		{
			get
			{
				return this._TargetEntityIds;
			}
			set
			{
				this._TargetEntityIds = value;
			}
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x00065A24 File Offset: 0x00063C24
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.SpellPrefabGuid.GetHashCode();
			if (this.HasSourceEntityId)
			{
				num ^= this.SourceEntityId.GetHashCode();
			}
			foreach (int num2 in this.TargetEntityIds)
			{
				num ^= num2.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x00065AB0 File Offset: 0x00063CB0
		public override bool Equals(object obj)
		{
			PowerHistorySubSpellStart powerHistorySubSpellStart = obj as PowerHistorySubSpellStart;
			if (powerHistorySubSpellStart == null)
			{
				return false;
			}
			if (!this.SpellPrefabGuid.Equals(powerHistorySubSpellStart.SpellPrefabGuid))
			{
				return false;
			}
			if (this.HasSourceEntityId != powerHistorySubSpellStart.HasSourceEntityId || (this.HasSourceEntityId && !this.SourceEntityId.Equals(powerHistorySubSpellStart.SourceEntityId)))
			{
				return false;
			}
			if (this.TargetEntityIds.Count != powerHistorySubSpellStart.TargetEntityIds.Count)
			{
				return false;
			}
			for (int i = 0; i < this.TargetEntityIds.Count; i++)
			{
				if (!this.TargetEntityIds[i].Equals(powerHistorySubSpellStart.TargetEntityIds[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x00065B61 File Offset: 0x00063D61
		public void Deserialize(Stream stream)
		{
			PowerHistorySubSpellStart.Deserialize(stream, this);
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x00065B6B File Offset: 0x00063D6B
		public static PowerHistorySubSpellStart Deserialize(Stream stream, PowerHistorySubSpellStart instance)
		{
			return PowerHistorySubSpellStart.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x00065B78 File Offset: 0x00063D78
		public static PowerHistorySubSpellStart DeserializeLengthDelimited(Stream stream)
		{
			PowerHistorySubSpellStart powerHistorySubSpellStart = new PowerHistorySubSpellStart();
			PowerHistorySubSpellStart.DeserializeLengthDelimited(stream, powerHistorySubSpellStart);
			return powerHistorySubSpellStart;
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x00065B94 File Offset: 0x00063D94
		public static PowerHistorySubSpellStart DeserializeLengthDelimited(Stream stream, PowerHistorySubSpellStart instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PowerHistorySubSpellStart.Deserialize(stream, instance, num);
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x00065BBC File Offset: 0x00063DBC
		public static PowerHistorySubSpellStart Deserialize(Stream stream, PowerHistorySubSpellStart instance, long limit)
		{
			if (instance.TargetEntityIds == null)
			{
				instance.TargetEntityIds = new List<int>();
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
				else if (num != 10)
				{
					if (num != 16)
					{
						if (num != 24)
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
							instance.TargetEntityIds.Add((int)ProtocolParser.ReadUInt64(stream));
						}
					}
					else
					{
						instance.SourceEntityId = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.SpellPrefabGuid = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x00065C84 File Offset: 0x00063E84
		public void Serialize(Stream stream)
		{
			PowerHistorySubSpellStart.Serialize(stream, this);
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x00065C90 File Offset: 0x00063E90
		public static void Serialize(Stream stream, PowerHistorySubSpellStart instance)
		{
			if (instance.SpellPrefabGuid == null)
			{
				throw new ArgumentNullException("SpellPrefabGuid", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SpellPrefabGuid));
			if (instance.HasSourceEntityId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SourceEntityId));
			}
			if (instance.TargetEntityIds.Count > 0)
			{
				foreach (int num in instance.TargetEntityIds)
				{
					stream.WriteByte(24);
					ProtocolParser.WriteUInt64(stream, (ulong)((long)num));
				}
			}
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x00065D50 File Offset: 0x00063F50
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.SpellPrefabGuid);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (this.HasSourceEntityId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SourceEntityId));
			}
			if (this.TargetEntityIds.Count > 0)
			{
				foreach (int num2 in this.TargetEntityIds)
				{
					num += 1U;
					num += ProtocolParser.SizeOfUInt64((ulong)((long)num2));
				}
			}
			num += 1U;
			return num;
		}

		// Token: 0x04000A7D RID: 2685
		public bool HasSourceEntityId;

		// Token: 0x04000A7E RID: 2686
		private int _SourceEntityId;

		// Token: 0x04000A7F RID: 2687
		private List<int> _TargetEntityIds = new List<int>();
	}
}
