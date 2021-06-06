using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000375 RID: 885
	public class FindGameRequest : IProtoBuf
	{
		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x0600383D RID: 14397 RVA: 0x000B8493 File Offset: 0x000B6693
		// (set) Token: 0x0600383E RID: 14398 RVA: 0x000B849B File Offset: 0x000B669B
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

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x0600383F RID: 14399 RVA: 0x000B8493 File Offset: 0x000B6693
		public List<Player> PlayerList
		{
			get
			{
				return this._Player;
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06003840 RID: 14400 RVA: 0x000B84A4 File Offset: 0x000B66A4
		public int PlayerCount
		{
			get
			{
				return this._Player.Count;
			}
		}

		// Token: 0x06003841 RID: 14401 RVA: 0x000B84B1 File Offset: 0x000B66B1
		public void AddPlayer(Player val)
		{
			this._Player.Add(val);
		}

		// Token: 0x06003842 RID: 14402 RVA: 0x000B84BF File Offset: 0x000B66BF
		public void ClearPlayer()
		{
			this._Player.Clear();
		}

		// Token: 0x06003843 RID: 14403 RVA: 0x000B84CC File Offset: 0x000B66CC
		public void SetPlayer(List<Player> val)
		{
			this.Player = val;
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06003844 RID: 14404 RVA: 0x000B84D5 File Offset: 0x000B66D5
		// (set) Token: 0x06003845 RID: 14405 RVA: 0x000B84DD File Offset: 0x000B66DD
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

		// Token: 0x06003846 RID: 14406 RVA: 0x000B84ED File Offset: 0x000B66ED
		public void SetFactoryId(ulong val)
		{
			this.FactoryId = val;
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06003847 RID: 14407 RVA: 0x000B84F6 File Offset: 0x000B66F6
		// (set) Token: 0x06003848 RID: 14408 RVA: 0x000B84FE File Offset: 0x000B66FE
		public GameProperties Properties
		{
			get
			{
				return this._Properties;
			}
			set
			{
				this._Properties = value;
				this.HasProperties = (value != null);
			}
		}

		// Token: 0x06003849 RID: 14409 RVA: 0x000B8511 File Offset: 0x000B6711
		public void SetProperties(GameProperties val)
		{
			this.Properties = val;
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x0600384A RID: 14410 RVA: 0x000B851A File Offset: 0x000B671A
		// (set) Token: 0x0600384B RID: 14411 RVA: 0x000B8522 File Offset: 0x000B6722
		public ulong RequestId
		{
			get
			{
				return this._RequestId;
			}
			set
			{
				this._RequestId = value;
				this.HasRequestId = true;
			}
		}

		// Token: 0x0600384C RID: 14412 RVA: 0x000B8532 File Offset: 0x000B6732
		public void SetRequestId(ulong val)
		{
			this.RequestId = val;
		}

		// Token: 0x0600384D RID: 14413 RVA: 0x000B853C File Offset: 0x000B673C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (Player player in this.Player)
			{
				num ^= player.GetHashCode();
			}
			if (this.HasFactoryId)
			{
				num ^= this.FactoryId.GetHashCode();
			}
			if (this.HasProperties)
			{
				num ^= this.Properties.GetHashCode();
			}
			if (this.HasRequestId)
			{
				num ^= this.RequestId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600384E RID: 14414 RVA: 0x000B85E8 File Offset: 0x000B67E8
		public override bool Equals(object obj)
		{
			FindGameRequest findGameRequest = obj as FindGameRequest;
			if (findGameRequest == null)
			{
				return false;
			}
			if (this.Player.Count != findGameRequest.Player.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Player.Count; i++)
			{
				if (!this.Player[i].Equals(findGameRequest.Player[i]))
				{
					return false;
				}
			}
			return this.HasFactoryId == findGameRequest.HasFactoryId && (!this.HasFactoryId || this.FactoryId.Equals(findGameRequest.FactoryId)) && this.HasProperties == findGameRequest.HasProperties && (!this.HasProperties || this.Properties.Equals(findGameRequest.Properties)) && this.HasRequestId == findGameRequest.HasRequestId && (!this.HasRequestId || this.RequestId.Equals(findGameRequest.RequestId));
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x0600384F RID: 14415 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003850 RID: 14416 RVA: 0x000B86DA File Offset: 0x000B68DA
		public static FindGameRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FindGameRequest>(bs, 0, -1);
		}

		// Token: 0x06003851 RID: 14417 RVA: 0x000B86E4 File Offset: 0x000B68E4
		public void Deserialize(Stream stream)
		{
			FindGameRequest.Deserialize(stream, this);
		}

		// Token: 0x06003852 RID: 14418 RVA: 0x000B86EE File Offset: 0x000B68EE
		public static FindGameRequest Deserialize(Stream stream, FindGameRequest instance)
		{
			return FindGameRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003853 RID: 14419 RVA: 0x000B86FC File Offset: 0x000B68FC
		public static FindGameRequest DeserializeLengthDelimited(Stream stream)
		{
			FindGameRequest findGameRequest = new FindGameRequest();
			FindGameRequest.DeserializeLengthDelimited(stream, findGameRequest);
			return findGameRequest;
		}

		// Token: 0x06003854 RID: 14420 RVA: 0x000B8718 File Offset: 0x000B6918
		public static FindGameRequest DeserializeLengthDelimited(Stream stream, FindGameRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FindGameRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003855 RID: 14421 RVA: 0x000B8740 File Offset: 0x000B6940
		public static FindGameRequest Deserialize(Stream stream, FindGameRequest instance, long limit)
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
						if (num == 10)
						{
							instance.Player.Add(bnet.protocol.games.v1.Player.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 17)
						{
							instance.FactoryId = binaryReader.ReadUInt64();
							continue;
						}
					}
					else if (num != 26)
					{
						if (num == 41)
						{
							instance.RequestId = binaryReader.ReadUInt64();
							continue;
						}
					}
					else
					{
						if (instance.Properties == null)
						{
							instance.Properties = GameProperties.DeserializeLengthDelimited(stream);
							continue;
						}
						GameProperties.DeserializeLengthDelimited(stream, instance.Properties);
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

		// Token: 0x06003856 RID: 14422 RVA: 0x000B884A File Offset: 0x000B6A4A
		public void Serialize(Stream stream)
		{
			FindGameRequest.Serialize(stream, this);
		}

		// Token: 0x06003857 RID: 14423 RVA: 0x000B8854 File Offset: 0x000B6A54
		public static void Serialize(Stream stream, FindGameRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.Player.Count > 0)
			{
				foreach (Player player in instance.Player)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, player.GetSerializedSize());
					bnet.protocol.games.v1.Player.Serialize(stream, player);
				}
			}
			if (instance.HasFactoryId)
			{
				stream.WriteByte(17);
				binaryWriter.Write(instance.FactoryId);
			}
			if (instance.HasProperties)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Properties.GetSerializedSize());
				GameProperties.Serialize(stream, instance.Properties);
			}
			if (instance.HasRequestId)
			{
				stream.WriteByte(41);
				binaryWriter.Write(instance.RequestId);
			}
		}

		// Token: 0x06003858 RID: 14424 RVA: 0x000B8938 File Offset: 0x000B6B38
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Player.Count > 0)
			{
				foreach (Player player in this.Player)
				{
					num += 1U;
					uint serializedSize = player.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasFactoryId)
			{
				num += 1U;
				num += 8U;
			}
			if (this.HasProperties)
			{
				num += 1U;
				uint serializedSize2 = this.Properties.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasRequestId)
			{
				num += 1U;
				num += 8U;
			}
			return num;
		}

		// Token: 0x040014ED RID: 5357
		private List<Player> _Player = new List<Player>();

		// Token: 0x040014EE RID: 5358
		public bool HasFactoryId;

		// Token: 0x040014EF RID: 5359
		private ulong _FactoryId;

		// Token: 0x040014F0 RID: 5360
		public bool HasProperties;

		// Token: 0x040014F1 RID: 5361
		private GameProperties _Properties;

		// Token: 0x040014F2 RID: 5362
		public bool HasRequestId;

		// Token: 0x040014F3 RID: 5363
		private ulong _RequestId;
	}
}
