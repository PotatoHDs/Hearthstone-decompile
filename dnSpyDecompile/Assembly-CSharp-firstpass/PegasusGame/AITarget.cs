using System;
using System.IO;
using System.Text;

namespace PegasusGame
{
	// Token: 0x02000197 RID: 407
	public class AITarget : IProtoBuf
	{
		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x0600196C RID: 6508 RVA: 0x00059CC1 File Offset: 0x00057EC1
		// (set) Token: 0x0600196D RID: 6509 RVA: 0x00059CC9 File Offset: 0x00057EC9
		public string EntityName { get; set; }

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x0600196E RID: 6510 RVA: 0x00059CD2 File Offset: 0x00057ED2
		// (set) Token: 0x0600196F RID: 6511 RVA: 0x00059CDA File Offset: 0x00057EDA
		public int EntityID { get; set; }

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001970 RID: 6512 RVA: 0x00059CE3 File Offset: 0x00057EE3
		// (set) Token: 0x06001971 RID: 6513 RVA: 0x00059CEB File Offset: 0x00057EEB
		public int TargetScore
		{
			get
			{
				return this._TargetScore;
			}
			set
			{
				this._TargetScore = value;
				this.HasTargetScore = true;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001972 RID: 6514 RVA: 0x00059CFB File Offset: 0x00057EFB
		// (set) Token: 0x06001973 RID: 6515 RVA: 0x00059D03 File Offset: 0x00057F03
		public bool TargetChosen
		{
			get
			{
				return this._TargetChosen;
			}
			set
			{
				this._TargetChosen = value;
				this.HasTargetChosen = true;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001974 RID: 6516 RVA: 0x00059D13 File Offset: 0x00057F13
		// (set) Token: 0x06001975 RID: 6517 RVA: 0x00059D1B File Offset: 0x00057F1B
		public float PriorProbability
		{
			get
			{
				return this._PriorProbability;
			}
			set
			{
				this._PriorProbability = value;
				this.HasPriorProbability = true;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001976 RID: 6518 RVA: 0x00059D2B File Offset: 0x00057F2B
		// (set) Token: 0x06001977 RID: 6519 RVA: 0x00059D33 File Offset: 0x00057F33
		public float PuctValue
		{
			get
			{
				return this._PuctValue;
			}
			set
			{
				this._PuctValue = value;
				this.HasPuctValue = true;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001978 RID: 6520 RVA: 0x00059D43 File Offset: 0x00057F43
		// (set) Token: 0x06001979 RID: 6521 RVA: 0x00059D4B File Offset: 0x00057F4B
		public float FinalQValue
		{
			get
			{
				return this._FinalQValue;
			}
			set
			{
				this._FinalQValue = value;
				this.HasFinalQValue = true;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x0600197A RID: 6522 RVA: 0x00059D5B File Offset: 0x00057F5B
		// (set) Token: 0x0600197B RID: 6523 RVA: 0x00059D63 File Offset: 0x00057F63
		public int FinalVisitCount
		{
			get
			{
				return this._FinalVisitCount;
			}
			set
			{
				this._FinalVisitCount = value;
				this.HasFinalVisitCount = true;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x0600197C RID: 6524 RVA: 0x00059D73 File Offset: 0x00057F73
		// (set) Token: 0x0600197D RID: 6525 RVA: 0x00059D7B File Offset: 0x00057F7B
		public float InferenceValue
		{
			get
			{
				return this._InferenceValue;
			}
			set
			{
				this._InferenceValue = value;
				this.HasInferenceValue = true;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x0600197E RID: 6526 RVA: 0x00059D8B File Offset: 0x00057F8B
		// (set) Token: 0x0600197F RID: 6527 RVA: 0x00059D93 File Offset: 0x00057F93
		public float HeuristicValue
		{
			get
			{
				return this._HeuristicValue;
			}
			set
			{
				this._HeuristicValue = value;
				this.HasHeuristicValue = true;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001980 RID: 6528 RVA: 0x00059DA3 File Offset: 0x00057FA3
		// (set) Token: 0x06001981 RID: 6529 RVA: 0x00059DAB File Offset: 0x00057FAB
		public float SubtreeValue
		{
			get
			{
				return this._SubtreeValue;
			}
			set
			{
				this._SubtreeValue = value;
				this.HasSubtreeValue = true;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001982 RID: 6530 RVA: 0x00059DBB File Offset: 0x00057FBB
		// (set) Token: 0x06001983 RID: 6531 RVA: 0x00059DC3 File Offset: 0x00057FC3
		public int EdgeCount
		{
			get
			{
				return this._EdgeCount;
			}
			set
			{
				this._EdgeCount = value;
				this.HasEdgeCount = true;
			}
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x00059DD4 File Offset: 0x00057FD4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.EntityName.GetHashCode();
			num ^= this.EntityID.GetHashCode();
			if (this.HasTargetScore)
			{
				num ^= this.TargetScore.GetHashCode();
			}
			if (this.HasTargetChosen)
			{
				num ^= this.TargetChosen.GetHashCode();
			}
			if (this.HasPriorProbability)
			{
				num ^= this.PriorProbability.GetHashCode();
			}
			if (this.HasPuctValue)
			{
				num ^= this.PuctValue.GetHashCode();
			}
			if (this.HasFinalQValue)
			{
				num ^= this.FinalQValue.GetHashCode();
			}
			if (this.HasFinalVisitCount)
			{
				num ^= this.FinalVisitCount.GetHashCode();
			}
			if (this.HasInferenceValue)
			{
				num ^= this.InferenceValue.GetHashCode();
			}
			if (this.HasHeuristicValue)
			{
				num ^= this.HeuristicValue.GetHashCode();
			}
			if (this.HasSubtreeValue)
			{
				num ^= this.SubtreeValue.GetHashCode();
			}
			if (this.HasEdgeCount)
			{
				num ^= this.EdgeCount.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x00059F08 File Offset: 0x00058108
		public override bool Equals(object obj)
		{
			AITarget aitarget = obj as AITarget;
			return aitarget != null && this.EntityName.Equals(aitarget.EntityName) && this.EntityID.Equals(aitarget.EntityID) && this.HasTargetScore == aitarget.HasTargetScore && (!this.HasTargetScore || this.TargetScore.Equals(aitarget.TargetScore)) && this.HasTargetChosen == aitarget.HasTargetChosen && (!this.HasTargetChosen || this.TargetChosen.Equals(aitarget.TargetChosen)) && this.HasPriorProbability == aitarget.HasPriorProbability && (!this.HasPriorProbability || this.PriorProbability.Equals(aitarget.PriorProbability)) && this.HasPuctValue == aitarget.HasPuctValue && (!this.HasPuctValue || this.PuctValue.Equals(aitarget.PuctValue)) && this.HasFinalQValue == aitarget.HasFinalQValue && (!this.HasFinalQValue || this.FinalQValue.Equals(aitarget.FinalQValue)) && this.HasFinalVisitCount == aitarget.HasFinalVisitCount && (!this.HasFinalVisitCount || this.FinalVisitCount.Equals(aitarget.FinalVisitCount)) && this.HasInferenceValue == aitarget.HasInferenceValue && (!this.HasInferenceValue || this.InferenceValue.Equals(aitarget.InferenceValue)) && this.HasHeuristicValue == aitarget.HasHeuristicValue && (!this.HasHeuristicValue || this.HeuristicValue.Equals(aitarget.HeuristicValue)) && this.HasSubtreeValue == aitarget.HasSubtreeValue && (!this.HasSubtreeValue || this.SubtreeValue.Equals(aitarget.SubtreeValue)) && this.HasEdgeCount == aitarget.HasEdgeCount && (!this.HasEdgeCount || this.EdgeCount.Equals(aitarget.EdgeCount));
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x0005A11B File Offset: 0x0005831B
		public void Deserialize(Stream stream)
		{
			AITarget.Deserialize(stream, this);
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x0005A125 File Offset: 0x00058325
		public static AITarget Deserialize(Stream stream, AITarget instance)
		{
			return AITarget.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x0005A130 File Offset: 0x00058330
		public static AITarget DeserializeLengthDelimited(Stream stream)
		{
			AITarget aitarget = new AITarget();
			AITarget.DeserializeLengthDelimited(stream, aitarget);
			return aitarget;
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x0005A14C File Offset: 0x0005834C
		public static AITarget DeserializeLengthDelimited(Stream stream, AITarget instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AITarget.Deserialize(stream, instance, num);
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x0005A174 File Offset: 0x00058374
		public static AITarget Deserialize(Stream stream, AITarget instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.PriorProbability = 0f;
			instance.PuctValue = 0f;
			instance.FinalQValue = 0f;
			instance.FinalVisitCount = 0;
			instance.InferenceValue = 0f;
			instance.HeuristicValue = 0f;
			instance.SubtreeValue = 0f;
			instance.EdgeCount = 0;
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
				else
				{
					if (num <= 53)
					{
						if (num <= 24)
						{
							if (num == 10)
							{
								instance.EntityName = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 16)
							{
								instance.EntityID = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 24)
							{
								instance.TargetScore = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (num == 32)
							{
								instance.TargetChosen = ProtocolParser.ReadBool(stream);
								continue;
							}
							if (num == 45)
							{
								instance.PriorProbability = binaryReader.ReadSingle();
								continue;
							}
							if (num == 53)
							{
								instance.PuctValue = binaryReader.ReadSingle();
								continue;
							}
						}
					}
					else if (num <= 77)
					{
						if (num == 61)
						{
							instance.FinalQValue = binaryReader.ReadSingle();
							continue;
						}
						if (num == 64)
						{
							instance.FinalVisitCount = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 77)
						{
							instance.InferenceValue = binaryReader.ReadSingle();
							continue;
						}
					}
					else
					{
						if (num == 85)
						{
							instance.HeuristicValue = binaryReader.ReadSingle();
							continue;
						}
						if (num == 93)
						{
							instance.SubtreeValue = binaryReader.ReadSingle();
							continue;
						}
						if (num == 96)
						{
							instance.EdgeCount = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
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

		// Token: 0x0600198B RID: 6539 RVA: 0x0005A38E File Offset: 0x0005858E
		public void Serialize(Stream stream)
		{
			AITarget.Serialize(stream, this);
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x0005A398 File Offset: 0x00058598
		public static void Serialize(Stream stream, AITarget instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.EntityName == null)
			{
				throw new ArgumentNullException("EntityName", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.EntityName));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.EntityID));
			if (instance.HasTargetScore)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.TargetScore));
			}
			if (instance.HasTargetChosen)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.TargetChosen);
			}
			if (instance.HasPriorProbability)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.PriorProbability);
			}
			if (instance.HasPuctValue)
			{
				stream.WriteByte(53);
				binaryWriter.Write(instance.PuctValue);
			}
			if (instance.HasFinalQValue)
			{
				stream.WriteByte(61);
				binaryWriter.Write(instance.FinalQValue);
			}
			if (instance.HasFinalVisitCount)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FinalVisitCount));
			}
			if (instance.HasInferenceValue)
			{
				stream.WriteByte(77);
				binaryWriter.Write(instance.InferenceValue);
			}
			if (instance.HasHeuristicValue)
			{
				stream.WriteByte(85);
				binaryWriter.Write(instance.HeuristicValue);
			}
			if (instance.HasSubtreeValue)
			{
				stream.WriteByte(93);
				binaryWriter.Write(instance.SubtreeValue);
			}
			if (instance.HasEdgeCount)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.EdgeCount));
			}
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x0005A514 File Offset: 0x00058714
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.EntityName);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.EntityID));
			if (this.HasTargetScore)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.TargetScore));
			}
			if (this.HasTargetChosen)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasPriorProbability)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasPuctValue)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasFinalQValue)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasFinalVisitCount)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FinalVisitCount));
			}
			if (this.HasInferenceValue)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasHeuristicValue)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasSubtreeValue)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasEdgeCount)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.EdgeCount));
			}
			return num + 2U;
		}

		// Token: 0x0400098A RID: 2442
		public bool HasTargetScore;

		// Token: 0x0400098B RID: 2443
		private int _TargetScore;

		// Token: 0x0400098C RID: 2444
		public bool HasTargetChosen;

		// Token: 0x0400098D RID: 2445
		private bool _TargetChosen;

		// Token: 0x0400098E RID: 2446
		public bool HasPriorProbability;

		// Token: 0x0400098F RID: 2447
		private float _PriorProbability;

		// Token: 0x04000990 RID: 2448
		public bool HasPuctValue;

		// Token: 0x04000991 RID: 2449
		private float _PuctValue;

		// Token: 0x04000992 RID: 2450
		public bool HasFinalQValue;

		// Token: 0x04000993 RID: 2451
		private float _FinalQValue;

		// Token: 0x04000994 RID: 2452
		public bool HasFinalVisitCount;

		// Token: 0x04000995 RID: 2453
		private int _FinalVisitCount;

		// Token: 0x04000996 RID: 2454
		public bool HasInferenceValue;

		// Token: 0x04000997 RID: 2455
		private float _InferenceValue;

		// Token: 0x04000998 RID: 2456
		public bool HasHeuristicValue;

		// Token: 0x04000999 RID: 2457
		private float _HeuristicValue;

		// Token: 0x0400099A RID: 2458
		public bool HasSubtreeValue;

		// Token: 0x0400099B RID: 2459
		private float _SubtreeValue;

		// Token: 0x0400099C RID: 2460
		public bool HasEdgeCount;

		// Token: 0x0400099D RID: 2461
		private int _EdgeCount;
	}
}
