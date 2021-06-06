using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003EE RID: 1006
	public class GameServerProperties : IProtoBuf
	{
		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06004293 RID: 17043 RVA: 0x000D346F File Offset: 0x000D166F
		// (set) Token: 0x06004294 RID: 17044 RVA: 0x000D3477 File Offset: 0x000D1677
		public uint MaxGameCount
		{
			get
			{
				return this._MaxGameCount;
			}
			set
			{
				this._MaxGameCount = value;
				this.HasMaxGameCount = true;
			}
		}

		// Token: 0x06004295 RID: 17045 RVA: 0x000D3487 File Offset: 0x000D1687
		public void SetMaxGameCount(uint val)
		{
			this.MaxGameCount = val;
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06004296 RID: 17046 RVA: 0x000D3490 File Offset: 0x000D1690
		// (set) Token: 0x06004297 RID: 17047 RVA: 0x000D3498 File Offset: 0x000D1698
		public uint CreateGameRate
		{
			get
			{
				return this._CreateGameRate;
			}
			set
			{
				this._CreateGameRate = value;
				this.HasCreateGameRate = true;
			}
		}

		// Token: 0x06004298 RID: 17048 RVA: 0x000D34A8 File Offset: 0x000D16A8
		public void SetCreateGameRate(uint val)
		{
			this.CreateGameRate = val;
		}

		// Token: 0x06004299 RID: 17049 RVA: 0x000D34B4 File Offset: 0x000D16B4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMaxGameCount)
			{
				num ^= this.MaxGameCount.GetHashCode();
			}
			if (this.HasCreateGameRate)
			{
				num ^= this.CreateGameRate.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600429A RID: 17050 RVA: 0x000D3500 File Offset: 0x000D1700
		public override bool Equals(object obj)
		{
			GameServerProperties gameServerProperties = obj as GameServerProperties;
			return gameServerProperties != null && this.HasMaxGameCount == gameServerProperties.HasMaxGameCount && (!this.HasMaxGameCount || this.MaxGameCount.Equals(gameServerProperties.MaxGameCount)) && this.HasCreateGameRate == gameServerProperties.HasCreateGameRate && (!this.HasCreateGameRate || this.CreateGameRate.Equals(gameServerProperties.CreateGameRate));
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x0600429B RID: 17051 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600429C RID: 17052 RVA: 0x000D3576 File Offset: 0x000D1776
		public static GameServerProperties ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameServerProperties>(bs, 0, -1);
		}

		// Token: 0x0600429D RID: 17053 RVA: 0x000D3580 File Offset: 0x000D1780
		public void Deserialize(Stream stream)
		{
			GameServerProperties.Deserialize(stream, this);
		}

		// Token: 0x0600429E RID: 17054 RVA: 0x000D358A File Offset: 0x000D178A
		public static GameServerProperties Deserialize(Stream stream, GameServerProperties instance)
		{
			return GameServerProperties.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600429F RID: 17055 RVA: 0x000D3598 File Offset: 0x000D1798
		public static GameServerProperties DeserializeLengthDelimited(Stream stream)
		{
			GameServerProperties gameServerProperties = new GameServerProperties();
			GameServerProperties.DeserializeLengthDelimited(stream, gameServerProperties);
			return gameServerProperties;
		}

		// Token: 0x060042A0 RID: 17056 RVA: 0x000D35B4 File Offset: 0x000D17B4
		public static GameServerProperties DeserializeLengthDelimited(Stream stream, GameServerProperties instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameServerProperties.Deserialize(stream, instance, num);
		}

		// Token: 0x060042A1 RID: 17057 RVA: 0x000D35DC File Offset: 0x000D17DC
		public static GameServerProperties Deserialize(Stream stream, GameServerProperties instance, long limit)
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
				else if (num != 8)
				{
					if (num != 16)
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
						instance.CreateGameRate = ProtocolParser.ReadUInt32(stream);
					}
				}
				else
				{
					instance.MaxGameCount = ProtocolParser.ReadUInt32(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060042A2 RID: 17058 RVA: 0x000D3673 File Offset: 0x000D1873
		public void Serialize(Stream stream)
		{
			GameServerProperties.Serialize(stream, this);
		}

		// Token: 0x060042A3 RID: 17059 RVA: 0x000D367C File Offset: 0x000D187C
		public static void Serialize(Stream stream, GameServerProperties instance)
		{
			if (instance.HasMaxGameCount)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.MaxGameCount);
			}
			if (instance.HasCreateGameRate)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.CreateGameRate);
			}
		}

		// Token: 0x060042A4 RID: 17060 RVA: 0x000D36B8 File Offset: 0x000D18B8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasMaxGameCount)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.MaxGameCount);
			}
			if (this.HasCreateGameRate)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.CreateGameRate);
			}
			return num;
		}

		// Token: 0x040016E0 RID: 5856
		public bool HasMaxGameCount;

		// Token: 0x040016E1 RID: 5857
		private uint _MaxGameCount;

		// Token: 0x040016E2 RID: 5858
		public bool HasCreateGameRate;

		// Token: 0x040016E3 RID: 5859
		private uint _CreateGameRate;
	}
}
