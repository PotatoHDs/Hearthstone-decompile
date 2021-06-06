using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001D4 RID: 468
	public class GameGuardianVars : IProtoBuf
	{
		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001DDB RID: 7643 RVA: 0x00068F86 File Offset: 0x00067186
		// (set) Token: 0x06001DDC RID: 7644 RVA: 0x00068F8E File Offset: 0x0006718E
		public float ClientLostFrameTimeCatchUpThreshold
		{
			get
			{
				return this._ClientLostFrameTimeCatchUpThreshold;
			}
			set
			{
				this._ClientLostFrameTimeCatchUpThreshold = value;
				this.HasClientLostFrameTimeCatchUpThreshold = true;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001DDD RID: 7645 RVA: 0x00068F9E File Offset: 0x0006719E
		// (set) Token: 0x06001DDE RID: 7646 RVA: 0x00068FA6 File Offset: 0x000671A6
		public bool ClientLostFrameTimeCatchUpUseSlush
		{
			get
			{
				return this._ClientLostFrameTimeCatchUpUseSlush;
			}
			set
			{
				this._ClientLostFrameTimeCatchUpUseSlush = value;
				this.HasClientLostFrameTimeCatchUpUseSlush = true;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001DDF RID: 7647 RVA: 0x00068FB6 File Offset: 0x000671B6
		// (set) Token: 0x06001DE0 RID: 7648 RVA: 0x00068FBE File Offset: 0x000671BE
		public bool ClientLostFrameTimeCatchUpLowEndOnly
		{
			get
			{
				return this._ClientLostFrameTimeCatchUpLowEndOnly;
			}
			set
			{
				this._ClientLostFrameTimeCatchUpLowEndOnly = value;
				this.HasClientLostFrameTimeCatchUpLowEndOnly = true;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x00068FCE File Offset: 0x000671CE
		// (set) Token: 0x06001DE2 RID: 7650 RVA: 0x00068FD6 File Offset: 0x000671D6
		public bool GameAllowDeferredPowers
		{
			get
			{
				return this._GameAllowDeferredPowers;
			}
			set
			{
				this._GameAllowDeferredPowers = value;
				this.HasGameAllowDeferredPowers = true;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x00068FE6 File Offset: 0x000671E6
		// (set) Token: 0x06001DE4 RID: 7652 RVA: 0x00068FEE File Offset: 0x000671EE
		public bool GameAllowBatchedPowers
		{
			get
			{
				return this._GameAllowBatchedPowers;
			}
			set
			{
				this._GameAllowBatchedPowers = value;
				this.HasGameAllowBatchedPowers = true;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x00068FFE File Offset: 0x000671FE
		// (set) Token: 0x06001DE6 RID: 7654 RVA: 0x00069006 File Offset: 0x00067206
		public bool GameAllowDiamondCards
		{
			get
			{
				return this._GameAllowDiamondCards;
			}
			set
			{
				this._GameAllowDiamondCards = value;
				this.HasGameAllowDiamondCards = true;
			}
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x00069018 File Offset: 0x00067218
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasClientLostFrameTimeCatchUpThreshold)
			{
				num ^= this.ClientLostFrameTimeCatchUpThreshold.GetHashCode();
			}
			if (this.HasClientLostFrameTimeCatchUpUseSlush)
			{
				num ^= this.ClientLostFrameTimeCatchUpUseSlush.GetHashCode();
			}
			if (this.HasClientLostFrameTimeCatchUpLowEndOnly)
			{
				num ^= this.ClientLostFrameTimeCatchUpLowEndOnly.GetHashCode();
			}
			if (this.HasGameAllowDeferredPowers)
			{
				num ^= this.GameAllowDeferredPowers.GetHashCode();
			}
			if (this.HasGameAllowBatchedPowers)
			{
				num ^= this.GameAllowBatchedPowers.GetHashCode();
			}
			if (this.HasGameAllowDiamondCards)
			{
				num ^= this.GameAllowDiamondCards.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x000690C8 File Offset: 0x000672C8
		public override bool Equals(object obj)
		{
			GameGuardianVars gameGuardianVars = obj as GameGuardianVars;
			return gameGuardianVars != null && this.HasClientLostFrameTimeCatchUpThreshold == gameGuardianVars.HasClientLostFrameTimeCatchUpThreshold && (!this.HasClientLostFrameTimeCatchUpThreshold || this.ClientLostFrameTimeCatchUpThreshold.Equals(gameGuardianVars.ClientLostFrameTimeCatchUpThreshold)) && this.HasClientLostFrameTimeCatchUpUseSlush == gameGuardianVars.HasClientLostFrameTimeCatchUpUseSlush && (!this.HasClientLostFrameTimeCatchUpUseSlush || this.ClientLostFrameTimeCatchUpUseSlush.Equals(gameGuardianVars.ClientLostFrameTimeCatchUpUseSlush)) && this.HasClientLostFrameTimeCatchUpLowEndOnly == gameGuardianVars.HasClientLostFrameTimeCatchUpLowEndOnly && (!this.HasClientLostFrameTimeCatchUpLowEndOnly || this.ClientLostFrameTimeCatchUpLowEndOnly.Equals(gameGuardianVars.ClientLostFrameTimeCatchUpLowEndOnly)) && this.HasGameAllowDeferredPowers == gameGuardianVars.HasGameAllowDeferredPowers && (!this.HasGameAllowDeferredPowers || this.GameAllowDeferredPowers.Equals(gameGuardianVars.GameAllowDeferredPowers)) && this.HasGameAllowBatchedPowers == gameGuardianVars.HasGameAllowBatchedPowers && (!this.HasGameAllowBatchedPowers || this.GameAllowBatchedPowers.Equals(gameGuardianVars.GameAllowBatchedPowers)) && this.HasGameAllowDiamondCards == gameGuardianVars.HasGameAllowDiamondCards && (!this.HasGameAllowDiamondCards || this.GameAllowDiamondCards.Equals(gameGuardianVars.GameAllowDiamondCards));
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x000691F6 File Offset: 0x000673F6
		public void Deserialize(Stream stream)
		{
			GameGuardianVars.Deserialize(stream, this);
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x00069200 File Offset: 0x00067400
		public static GameGuardianVars Deserialize(Stream stream, GameGuardianVars instance)
		{
			return GameGuardianVars.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x0006920C File Offset: 0x0006740C
		public static GameGuardianVars DeserializeLengthDelimited(Stream stream)
		{
			GameGuardianVars gameGuardianVars = new GameGuardianVars();
			GameGuardianVars.DeserializeLengthDelimited(stream, gameGuardianVars);
			return gameGuardianVars;
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x00069228 File Offset: 0x00067428
		public static GameGuardianVars DeserializeLengthDelimited(Stream stream, GameGuardianVars instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameGuardianVars.Deserialize(stream, instance, num);
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x00069250 File Offset: 0x00067450
		public static GameGuardianVars Deserialize(Stream stream, GameGuardianVars instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					if (num <= 24)
					{
						if (num == 13)
						{
							instance.ClientLostFrameTimeCatchUpThreshold = binaryReader.ReadSingle();
							continue;
						}
						if (num == 16)
						{
							instance.ClientLostFrameTimeCatchUpUseSlush = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 24)
						{
							instance.ClientLostFrameTimeCatchUpLowEndOnly = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					else
					{
						if (num == 32)
						{
							instance.GameAllowDeferredPowers = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 40)
						{
							instance.GameAllowBatchedPowers = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 48)
						{
							instance.GameAllowDiamondCards = ProtocolParser.ReadBool(stream);
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

		// Token: 0x06001DEE RID: 7662 RVA: 0x00069357 File Offset: 0x00067557
		public void Serialize(Stream stream)
		{
			GameGuardianVars.Serialize(stream, this);
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x00069360 File Offset: 0x00067560
		public static void Serialize(Stream stream, GameGuardianVars instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasClientLostFrameTimeCatchUpThreshold)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.ClientLostFrameTimeCatchUpThreshold);
			}
			if (instance.HasClientLostFrameTimeCatchUpUseSlush)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.ClientLostFrameTimeCatchUpUseSlush);
			}
			if (instance.HasClientLostFrameTimeCatchUpLowEndOnly)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.ClientLostFrameTimeCatchUpLowEndOnly);
			}
			if (instance.HasGameAllowDeferredPowers)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.GameAllowDeferredPowers);
			}
			if (instance.HasGameAllowBatchedPowers)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.GameAllowBatchedPowers);
			}
			if (instance.HasGameAllowDiamondCards)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.GameAllowDiamondCards);
			}
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x0006941C File Offset: 0x0006761C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasClientLostFrameTimeCatchUpThreshold)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasClientLostFrameTimeCatchUpUseSlush)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasClientLostFrameTimeCatchUpLowEndOnly)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasGameAllowDeferredPowers)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasGameAllowBatchedPowers)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasGameAllowDiamondCards)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04000AC8 RID: 2760
		public bool HasClientLostFrameTimeCatchUpThreshold;

		// Token: 0x04000AC9 RID: 2761
		private float _ClientLostFrameTimeCatchUpThreshold;

		// Token: 0x04000ACA RID: 2762
		public bool HasClientLostFrameTimeCatchUpUseSlush;

		// Token: 0x04000ACB RID: 2763
		private bool _ClientLostFrameTimeCatchUpUseSlush;

		// Token: 0x04000ACC RID: 2764
		public bool HasClientLostFrameTimeCatchUpLowEndOnly;

		// Token: 0x04000ACD RID: 2765
		private bool _ClientLostFrameTimeCatchUpLowEndOnly;

		// Token: 0x04000ACE RID: 2766
		public bool HasGameAllowDeferredPowers;

		// Token: 0x04000ACF RID: 2767
		private bool _GameAllowDeferredPowers;

		// Token: 0x04000AD0 RID: 2768
		public bool HasGameAllowBatchedPowers;

		// Token: 0x04000AD1 RID: 2769
		private bool _GameAllowBatchedPowers;

		// Token: 0x04000AD2 RID: 2770
		public bool HasGameAllowDiamondCards;

		// Token: 0x04000AD3 RID: 2771
		private bool _GameAllowDiamondCards;

		// Token: 0x0200065F RID: 1631
		public enum PacketID
		{
			// Token: 0x04002158 RID: 8536
			ID = 35
		}
	}
}
