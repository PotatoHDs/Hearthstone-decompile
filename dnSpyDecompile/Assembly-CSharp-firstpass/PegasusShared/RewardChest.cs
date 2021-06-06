using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000147 RID: 327
	public class RewardChest : IProtoBuf
	{
		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001599 RID: 5529 RVA: 0x00049DEF File Offset: 0x00047FEF
		// (set) Token: 0x0600159A RID: 5530 RVA: 0x00049DF7 File Offset: 0x00047FF7
		public RewardBag Bag1
		{
			get
			{
				return this._Bag1;
			}
			set
			{
				this._Bag1 = value;
				this.HasBag1 = (value != null);
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x00049E0A File Offset: 0x0004800A
		// (set) Token: 0x0600159C RID: 5532 RVA: 0x00049E12 File Offset: 0x00048012
		public RewardBag Bag2
		{
			get
			{
				return this._Bag2;
			}
			set
			{
				this._Bag2 = value;
				this.HasBag2 = (value != null);
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x0600159D RID: 5533 RVA: 0x00049E25 File Offset: 0x00048025
		// (set) Token: 0x0600159E RID: 5534 RVA: 0x00049E2D File Offset: 0x0004802D
		public RewardBag Bag3
		{
			get
			{
				return this._Bag3;
			}
			set
			{
				this._Bag3 = value;
				this.HasBag3 = (value != null);
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x0600159F RID: 5535 RVA: 0x00049E40 File Offset: 0x00048040
		// (set) Token: 0x060015A0 RID: 5536 RVA: 0x00049E48 File Offset: 0x00048048
		public RewardBag Bag4
		{
			get
			{
				return this._Bag4;
			}
			set
			{
				this._Bag4 = value;
				this.HasBag4 = (value != null);
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x00049E5B File Offset: 0x0004805B
		// (set) Token: 0x060015A2 RID: 5538 RVA: 0x00049E63 File Offset: 0x00048063
		public RewardBag Bag5
		{
			get
			{
				return this._Bag5;
			}
			set
			{
				this._Bag5 = value;
				this.HasBag5 = (value != null);
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x060015A3 RID: 5539 RVA: 0x00049E76 File Offset: 0x00048076
		// (set) Token: 0x060015A4 RID: 5540 RVA: 0x00049E7E File Offset: 0x0004807E
		public List<RewardBag> Bag
		{
			get
			{
				return this._Bag;
			}
			set
			{
				this._Bag = value;
			}
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x00049E88 File Offset: 0x00048088
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasBag1)
			{
				num ^= this.Bag1.GetHashCode();
			}
			if (this.HasBag2)
			{
				num ^= this.Bag2.GetHashCode();
			}
			if (this.HasBag3)
			{
				num ^= this.Bag3.GetHashCode();
			}
			if (this.HasBag4)
			{
				num ^= this.Bag4.GetHashCode();
			}
			if (this.HasBag5)
			{
				num ^= this.Bag5.GetHashCode();
			}
			foreach (RewardBag rewardBag in this.Bag)
			{
				num ^= rewardBag.GetHashCode();
			}
			return num;
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x00049F58 File Offset: 0x00048158
		public override bool Equals(object obj)
		{
			RewardChest rewardChest = obj as RewardChest;
			if (rewardChest == null)
			{
				return false;
			}
			if (this.HasBag1 != rewardChest.HasBag1 || (this.HasBag1 && !this.Bag1.Equals(rewardChest.Bag1)))
			{
				return false;
			}
			if (this.HasBag2 != rewardChest.HasBag2 || (this.HasBag2 && !this.Bag2.Equals(rewardChest.Bag2)))
			{
				return false;
			}
			if (this.HasBag3 != rewardChest.HasBag3 || (this.HasBag3 && !this.Bag3.Equals(rewardChest.Bag3)))
			{
				return false;
			}
			if (this.HasBag4 != rewardChest.HasBag4 || (this.HasBag4 && !this.Bag4.Equals(rewardChest.Bag4)))
			{
				return false;
			}
			if (this.HasBag5 != rewardChest.HasBag5 || (this.HasBag5 && !this.Bag5.Equals(rewardChest.Bag5)))
			{
				return false;
			}
			if (this.Bag.Count != rewardChest.Bag.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Bag.Count; i++)
			{
				if (!this.Bag[i].Equals(rewardChest.Bag[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x0004A09A File Offset: 0x0004829A
		public void Deserialize(Stream stream)
		{
			RewardChest.Deserialize(stream, this);
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x0004A0A4 File Offset: 0x000482A4
		public static RewardChest Deserialize(Stream stream, RewardChest instance)
		{
			return RewardChest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x0004A0B0 File Offset: 0x000482B0
		public static RewardChest DeserializeLengthDelimited(Stream stream)
		{
			RewardChest rewardChest = new RewardChest();
			RewardChest.DeserializeLengthDelimited(stream, rewardChest);
			return rewardChest;
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x0004A0CC File Offset: 0x000482CC
		public static RewardChest DeserializeLengthDelimited(Stream stream, RewardChest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RewardChest.Deserialize(stream, instance, num);
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x0004A0F4 File Offset: 0x000482F4
		public static RewardChest Deserialize(Stream stream, RewardChest instance, long limit)
		{
			if (instance.Bag == null)
			{
				instance.Bag = new List<RewardBag>();
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
				else
				{
					if (num <= 26)
					{
						if (num != 10)
						{
							if (num != 18)
							{
								if (num == 26)
								{
									if (instance.Bag3 == null)
									{
										instance.Bag3 = RewardBag.DeserializeLengthDelimited(stream);
										continue;
									}
									RewardBag.DeserializeLengthDelimited(stream, instance.Bag3);
									continue;
								}
							}
							else
							{
								if (instance.Bag2 == null)
								{
									instance.Bag2 = RewardBag.DeserializeLengthDelimited(stream);
									continue;
								}
								RewardBag.DeserializeLengthDelimited(stream, instance.Bag2);
								continue;
							}
						}
						else
						{
							if (instance.Bag1 == null)
							{
								instance.Bag1 = RewardBag.DeserializeLengthDelimited(stream);
								continue;
							}
							RewardBag.DeserializeLengthDelimited(stream, instance.Bag1);
							continue;
						}
					}
					else if (num != 34)
					{
						if (num != 42)
						{
							if (num == 50)
							{
								instance.Bag.Add(RewardBag.DeserializeLengthDelimited(stream));
								continue;
							}
						}
						else
						{
							if (instance.Bag5 == null)
							{
								instance.Bag5 = RewardBag.DeserializeLengthDelimited(stream);
								continue;
							}
							RewardBag.DeserializeLengthDelimited(stream, instance.Bag5);
							continue;
						}
					}
					else
					{
						if (instance.Bag4 == null)
						{
							instance.Bag4 = RewardBag.DeserializeLengthDelimited(stream);
							continue;
						}
						RewardBag.DeserializeLengthDelimited(stream, instance.Bag4);
						continue;
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

		// Token: 0x060015AC RID: 5548 RVA: 0x0004A2A0 File Offset: 0x000484A0
		public void Serialize(Stream stream)
		{
			RewardChest.Serialize(stream, this);
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x0004A2AC File Offset: 0x000484AC
		public static void Serialize(Stream stream, RewardChest instance)
		{
			if (instance.HasBag1)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Bag1.GetSerializedSize());
				RewardBag.Serialize(stream, instance.Bag1);
			}
			if (instance.HasBag2)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Bag2.GetSerializedSize());
				RewardBag.Serialize(stream, instance.Bag2);
			}
			if (instance.HasBag3)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Bag3.GetSerializedSize());
				RewardBag.Serialize(stream, instance.Bag3);
			}
			if (instance.HasBag4)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Bag4.GetSerializedSize());
				RewardBag.Serialize(stream, instance.Bag4);
			}
			if (instance.HasBag5)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.Bag5.GetSerializedSize());
				RewardBag.Serialize(stream, instance.Bag5);
			}
			if (instance.Bag.Count > 0)
			{
				foreach (RewardBag rewardBag in instance.Bag)
				{
					stream.WriteByte(50);
					ProtocolParser.WriteUInt32(stream, rewardBag.GetSerializedSize());
					RewardBag.Serialize(stream, rewardBag);
				}
			}
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x0004A404 File Offset: 0x00048604
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasBag1)
			{
				num += 1U;
				uint serializedSize = this.Bag1.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasBag2)
			{
				num += 1U;
				uint serializedSize2 = this.Bag2.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasBag3)
			{
				num += 1U;
				uint serializedSize3 = this.Bag3.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasBag4)
			{
				num += 1U;
				uint serializedSize4 = this.Bag4.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (this.HasBag5)
			{
				num += 1U;
				uint serializedSize5 = this.Bag5.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			if (this.Bag.Count > 0)
			{
				foreach (RewardBag rewardBag in this.Bag)
				{
					num += 1U;
					uint serializedSize6 = rewardBag.GetSerializedSize();
					num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
				}
			}
			return num;
		}

		// Token: 0x040006A0 RID: 1696
		public bool HasBag1;

		// Token: 0x040006A1 RID: 1697
		private RewardBag _Bag1;

		// Token: 0x040006A2 RID: 1698
		public bool HasBag2;

		// Token: 0x040006A3 RID: 1699
		private RewardBag _Bag2;

		// Token: 0x040006A4 RID: 1700
		public bool HasBag3;

		// Token: 0x040006A5 RID: 1701
		private RewardBag _Bag3;

		// Token: 0x040006A6 RID: 1702
		public bool HasBag4;

		// Token: 0x040006A7 RID: 1703
		private RewardBag _Bag4;

		// Token: 0x040006A8 RID: 1704
		public bool HasBag5;

		// Token: 0x040006A9 RID: 1705
		private RewardBag _Bag5;

		// Token: 0x040006AA RID: 1706
		private List<RewardBag> _Bag = new List<RewardBag>();
	}
}
