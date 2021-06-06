using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x020003A4 RID: 932
	public class CancelGameEntryRequest : IProtoBuf
	{
		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06003C5B RID: 15451 RVA: 0x000C2E47 File Offset: 0x000C1047
		// (set) Token: 0x06003C5C RID: 15452 RVA: 0x000C2E4F File Offset: 0x000C104F
		public ulong RequestId { get; set; }

		// Token: 0x06003C5D RID: 15453 RVA: 0x000C2E58 File Offset: 0x000C1058
		public void SetRequestId(ulong val)
		{
			this.RequestId = val;
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06003C5E RID: 15454 RVA: 0x000C2E61 File Offset: 0x000C1061
		// (set) Token: 0x06003C5F RID: 15455 RVA: 0x000C2E69 File Offset: 0x000C1069
		public ulong FactoryId
		{
			get
			{
				return this._FactoryId;
			}
			set
			{
				this._FactoryId = value;
				this.HasFactoryId = true;
			}
		}

		// Token: 0x06003C60 RID: 15456 RVA: 0x000C2E79 File Offset: 0x000C1079
		public void SetFactoryId(ulong val)
		{
			this.FactoryId = val;
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06003C61 RID: 15457 RVA: 0x000C2E82 File Offset: 0x000C1082
		// (set) Token: 0x06003C62 RID: 15458 RVA: 0x000C2E8A File Offset: 0x000C108A
		public List<Player> Player
		{
			get
			{
				return this._Player;
			}
			set
			{
				this._Player = value;
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06003C63 RID: 15459 RVA: 0x000C2E82 File Offset: 0x000C1082
		public List<Player> PlayerList
		{
			get
			{
				return this._Player;
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06003C64 RID: 15460 RVA: 0x000C2E93 File Offset: 0x000C1093
		public int PlayerCount
		{
			get
			{
				return this._Player.Count;
			}
		}

		// Token: 0x06003C65 RID: 15461 RVA: 0x000C2EA0 File Offset: 0x000C10A0
		public void AddPlayer(Player val)
		{
			this._Player.Add(val);
		}

		// Token: 0x06003C66 RID: 15462 RVA: 0x000C2EAE File Offset: 0x000C10AE
		public void ClearPlayer()
		{
			this._Player.Clear();
		}

		// Token: 0x06003C67 RID: 15463 RVA: 0x000C2EBB File Offset: 0x000C10BB
		public void SetPlayer(List<Player> val)
		{
			this.Player = val;
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06003C68 RID: 15464 RVA: 0x000C2EC4 File Offset: 0x000C10C4
		// (set) Token: 0x06003C69 RID: 15465 RVA: 0x000C2ECC File Offset: 0x000C10CC
		public EntityId CancelRequestInitiator
		{
			get
			{
				return this._CancelRequestInitiator;
			}
			set
			{
				this._CancelRequestInitiator = value;
				this.HasCancelRequestInitiator = (value != null);
			}
		}

		// Token: 0x06003C6A RID: 15466 RVA: 0x000C2EDF File Offset: 0x000C10DF
		public void SetCancelRequestInitiator(EntityId val)
		{
			this.CancelRequestInitiator = val;
		}

		// Token: 0x06003C6B RID: 15467 RVA: 0x000C2EE8 File Offset: 0x000C10E8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.RequestId.GetHashCode();
			if (this.HasFactoryId)
			{
				num ^= this.FactoryId.GetHashCode();
			}
			foreach (Player player in this.Player)
			{
				num ^= player.GetHashCode();
			}
			if (this.HasCancelRequestInitiator)
			{
				num ^= this.CancelRequestInitiator.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003C6C RID: 15468 RVA: 0x000C2F8C File Offset: 0x000C118C
		public override bool Equals(object obj)
		{
			CancelGameEntryRequest cancelGameEntryRequest = obj as CancelGameEntryRequest;
			if (cancelGameEntryRequest == null)
			{
				return false;
			}
			if (!this.RequestId.Equals(cancelGameEntryRequest.RequestId))
			{
				return false;
			}
			if (this.HasFactoryId != cancelGameEntryRequest.HasFactoryId || (this.HasFactoryId && !this.FactoryId.Equals(cancelGameEntryRequest.FactoryId)))
			{
				return false;
			}
			if (this.Player.Count != cancelGameEntryRequest.Player.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Player.Count; i++)
			{
				if (!this.Player[i].Equals(cancelGameEntryRequest.Player[i]))
				{
					return false;
				}
			}
			return this.HasCancelRequestInitiator == cancelGameEntryRequest.HasCancelRequestInitiator && (!this.HasCancelRequestInitiator || this.CancelRequestInitiator.Equals(cancelGameEntryRequest.CancelRequestInitiator));
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06003C6D RID: 15469 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003C6E RID: 15470 RVA: 0x000C3068 File Offset: 0x000C1268
		public static CancelGameEntryRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CancelGameEntryRequest>(bs, 0, -1);
		}

		// Token: 0x06003C6F RID: 15471 RVA: 0x000C3072 File Offset: 0x000C1272
		public void Deserialize(Stream stream)
		{
			CancelGameEntryRequest.Deserialize(stream, this);
		}

		// Token: 0x06003C70 RID: 15472 RVA: 0x000C307C File Offset: 0x000C127C
		public static CancelGameEntryRequest Deserialize(Stream stream, CancelGameEntryRequest instance)
		{
			return CancelGameEntryRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003C71 RID: 15473 RVA: 0x000C3088 File Offset: 0x000C1288
		public static CancelGameEntryRequest DeserializeLengthDelimited(Stream stream)
		{
			CancelGameEntryRequest cancelGameEntryRequest = new CancelGameEntryRequest();
			CancelGameEntryRequest.DeserializeLengthDelimited(stream, cancelGameEntryRequest);
			return cancelGameEntryRequest;
		}

		// Token: 0x06003C72 RID: 15474 RVA: 0x000C30A4 File Offset: 0x000C12A4
		public static CancelGameEntryRequest DeserializeLengthDelimited(Stream stream, CancelGameEntryRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CancelGameEntryRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003C73 RID: 15475 RVA: 0x000C30CC File Offset: 0x000C12CC
		public static CancelGameEntryRequest Deserialize(Stream stream, CancelGameEntryRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Player == null)
			{
				instance.Player = new List<Player>();
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
					if (num <= 17)
					{
						if (num == 9)
						{
							instance.RequestId = binaryReader.ReadUInt64();
							continue;
						}
						if (num == 17)
						{
							instance.FactoryId = binaryReader.ReadUInt64();
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.Player.Add(bnet.protocol.games.v1.Player.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 34)
						{
							if (instance.CancelRequestInitiator == null)
							{
								instance.CancelRequestInitiator = EntityId.DeserializeLengthDelimited(stream);
								continue;
							}
							EntityId.DeserializeLengthDelimited(stream, instance.CancelRequestInitiator);
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

		// Token: 0x06003C74 RID: 15476 RVA: 0x000C31D6 File Offset: 0x000C13D6
		public void Serialize(Stream stream)
		{
			CancelGameEntryRequest.Serialize(stream, this);
		}

		// Token: 0x06003C75 RID: 15477 RVA: 0x000C31E0 File Offset: 0x000C13E0
		public static void Serialize(Stream stream, CancelGameEntryRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(9);
			binaryWriter.Write(instance.RequestId);
			if (instance.HasFactoryId)
			{
				stream.WriteByte(17);
				binaryWriter.Write(instance.FactoryId);
			}
			if (instance.Player.Count > 0)
			{
				foreach (Player player in instance.Player)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, player.GetSerializedSize());
					bnet.protocol.games.v1.Player.Serialize(stream, player);
				}
			}
			if (instance.HasCancelRequestInitiator)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.CancelRequestInitiator.GetSerializedSize());
				EntityId.Serialize(stream, instance.CancelRequestInitiator);
			}
		}

		// Token: 0x06003C76 RID: 15478 RVA: 0x000C32BC File Offset: 0x000C14BC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += 8U;
			if (this.HasFactoryId)
			{
				num += 1U;
				num += 8U;
			}
			if (this.Player.Count > 0)
			{
				foreach (Player player in this.Player)
				{
					num += 1U;
					uint serializedSize = player.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasCancelRequestInitiator)
			{
				num += 1U;
				uint serializedSize2 = this.CancelRequestInitiator.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			num += 1U;
			return num;
		}

		// Token: 0x040015B9 RID: 5561
		public bool HasFactoryId;

		// Token: 0x040015BA RID: 5562
		private ulong _FactoryId;

		// Token: 0x040015BB RID: 5563
		private List<Player> _Player = new List<Player>();

		// Token: 0x040015BC RID: 5564
		public bool HasCancelRequestInitiator;

		// Token: 0x040015BD RID: 5565
		private EntityId _CancelRequestInitiator;
	}
}
