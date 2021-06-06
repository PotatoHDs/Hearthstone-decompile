using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003D9 RID: 985
	public class GameMatchmakingOptions : IProtoBuf
	{
		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x060040AB RID: 16555 RVA: 0x000CDF8F File Offset: 0x000CC18F
		// (set) Token: 0x060040AC RID: 16556 RVA: 0x000CDF97 File Offset: 0x000CC197
		public GameMatchmakerFilter MatchmakerFilter
		{
			get
			{
				return this._MatchmakerFilter;
			}
			set
			{
				this._MatchmakerFilter = value;
				this.HasMatchmakerFilter = (value != null);
			}
		}

		// Token: 0x060040AD RID: 16557 RVA: 0x000CDFAA File Offset: 0x000CC1AA
		public void SetMatchmakerFilter(GameMatchmakerFilter val)
		{
			this.MatchmakerFilter = val;
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x060040AE RID: 16558 RVA: 0x000CDFB3 File Offset: 0x000CC1B3
		// (set) Token: 0x060040AF RID: 16559 RVA: 0x000CDFBB File Offset: 0x000CC1BB
		public GameCreationProperties CreationProperties
		{
			get
			{
				return this._CreationProperties;
			}
			set
			{
				this._CreationProperties = value;
				this.HasCreationProperties = (value != null);
			}
		}

		// Token: 0x060040B0 RID: 16560 RVA: 0x000CDFCE File Offset: 0x000CC1CE
		public void SetCreationProperties(GameCreationProperties val)
		{
			this.CreationProperties = val;
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x060040B1 RID: 16561 RVA: 0x000CDFD7 File Offset: 0x000CC1D7
		// (set) Token: 0x060040B2 RID: 16562 RVA: 0x000CDFDF File Offset: 0x000CC1DF
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

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x060040B3 RID: 16563 RVA: 0x000CDFD7 File Offset: 0x000CC1D7
		public List<Player> PlayerList
		{
			get
			{
				return this._Player;
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x060040B4 RID: 16564 RVA: 0x000CDFE8 File Offset: 0x000CC1E8
		public int PlayerCount
		{
			get
			{
				return this._Player.Count;
			}
		}

		// Token: 0x060040B5 RID: 16565 RVA: 0x000CDFF5 File Offset: 0x000CC1F5
		public void AddPlayer(Player val)
		{
			this._Player.Add(val);
		}

		// Token: 0x060040B6 RID: 16566 RVA: 0x000CE003 File Offset: 0x000CC203
		public void ClearPlayer()
		{
			this._Player.Clear();
		}

		// Token: 0x060040B7 RID: 16567 RVA: 0x000CE010 File Offset: 0x000CC210
		public void SetPlayer(List<Player> val)
		{
			this.Player = val;
		}

		// Token: 0x060040B8 RID: 16568 RVA: 0x000CE01C File Offset: 0x000CC21C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMatchmakerFilter)
			{
				num ^= this.MatchmakerFilter.GetHashCode();
			}
			if (this.HasCreationProperties)
			{
				num ^= this.CreationProperties.GetHashCode();
			}
			foreach (Player player in this.Player)
			{
				num ^= player.GetHashCode();
			}
			return num;
		}

		// Token: 0x060040B9 RID: 16569 RVA: 0x000CE0AC File Offset: 0x000CC2AC
		public override bool Equals(object obj)
		{
			GameMatchmakingOptions gameMatchmakingOptions = obj as GameMatchmakingOptions;
			if (gameMatchmakingOptions == null)
			{
				return false;
			}
			if (this.HasMatchmakerFilter != gameMatchmakingOptions.HasMatchmakerFilter || (this.HasMatchmakerFilter && !this.MatchmakerFilter.Equals(gameMatchmakingOptions.MatchmakerFilter)))
			{
				return false;
			}
			if (this.HasCreationProperties != gameMatchmakingOptions.HasCreationProperties || (this.HasCreationProperties && !this.CreationProperties.Equals(gameMatchmakingOptions.CreationProperties)))
			{
				return false;
			}
			if (this.Player.Count != gameMatchmakingOptions.Player.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Player.Count; i++)
			{
				if (!this.Player[i].Equals(gameMatchmakingOptions.Player[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x060040BA RID: 16570 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060040BB RID: 16571 RVA: 0x000CE16D File Offset: 0x000CC36D
		public static GameMatchmakingOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameMatchmakingOptions>(bs, 0, -1);
		}

		// Token: 0x060040BC RID: 16572 RVA: 0x000CE177 File Offset: 0x000CC377
		public void Deserialize(Stream stream)
		{
			GameMatchmakingOptions.Deserialize(stream, this);
		}

		// Token: 0x060040BD RID: 16573 RVA: 0x000CE181 File Offset: 0x000CC381
		public static GameMatchmakingOptions Deserialize(Stream stream, GameMatchmakingOptions instance)
		{
			return GameMatchmakingOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060040BE RID: 16574 RVA: 0x000CE18C File Offset: 0x000CC38C
		public static GameMatchmakingOptions DeserializeLengthDelimited(Stream stream)
		{
			GameMatchmakingOptions gameMatchmakingOptions = new GameMatchmakingOptions();
			GameMatchmakingOptions.DeserializeLengthDelimited(stream, gameMatchmakingOptions);
			return gameMatchmakingOptions;
		}

		// Token: 0x060040BF RID: 16575 RVA: 0x000CE1A8 File Offset: 0x000CC3A8
		public static GameMatchmakingOptions DeserializeLengthDelimited(Stream stream, GameMatchmakingOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameMatchmakingOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x060040C0 RID: 16576 RVA: 0x000CE1D0 File Offset: 0x000CC3D0
		public static GameMatchmakingOptions Deserialize(Stream stream, GameMatchmakingOptions instance, long limit)
		{
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
				else if (num != 10)
				{
					if (num != 18)
					{
						if (num != 26)
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
							instance.Player.Add(bnet.protocol.matchmaking.v1.Player.DeserializeLengthDelimited(stream));
						}
					}
					else if (instance.CreationProperties == null)
					{
						instance.CreationProperties = GameCreationProperties.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameCreationProperties.DeserializeLengthDelimited(stream, instance.CreationProperties);
					}
				}
				else if (instance.MatchmakerFilter == null)
				{
					instance.MatchmakerFilter = GameMatchmakerFilter.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameMatchmakerFilter.DeserializeLengthDelimited(stream, instance.MatchmakerFilter);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060040C1 RID: 16577 RVA: 0x000CE2D0 File Offset: 0x000CC4D0
		public void Serialize(Stream stream)
		{
			GameMatchmakingOptions.Serialize(stream, this);
		}

		// Token: 0x060040C2 RID: 16578 RVA: 0x000CE2DC File Offset: 0x000CC4DC
		public static void Serialize(Stream stream, GameMatchmakingOptions instance)
		{
			if (instance.HasMatchmakerFilter)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.MatchmakerFilter.GetSerializedSize());
				GameMatchmakerFilter.Serialize(stream, instance.MatchmakerFilter);
			}
			if (instance.HasCreationProperties)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.CreationProperties.GetSerializedSize());
				GameCreationProperties.Serialize(stream, instance.CreationProperties);
			}
			if (instance.Player.Count > 0)
			{
				foreach (Player player in instance.Player)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, player.GetSerializedSize());
					bnet.protocol.matchmaking.v1.Player.Serialize(stream, player);
				}
			}
		}

		// Token: 0x060040C3 RID: 16579 RVA: 0x000CE3AC File Offset: 0x000CC5AC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasMatchmakerFilter)
			{
				num += 1U;
				uint serializedSize = this.MatchmakerFilter.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasCreationProperties)
			{
				num += 1U;
				uint serializedSize2 = this.CreationProperties.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.Player.Count > 0)
			{
				foreach (Player player in this.Player)
				{
					num += 1U;
					uint serializedSize3 = player.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			return num;
		}

		// Token: 0x04001679 RID: 5753
		public bool HasMatchmakerFilter;

		// Token: 0x0400167A RID: 5754
		private GameMatchmakerFilter _MatchmakerFilter;

		// Token: 0x0400167B RID: 5755
		public bool HasCreationProperties;

		// Token: 0x0400167C RID: 5756
		private GameCreationProperties _CreationProperties;

		// Token: 0x0400167D RID: 5757
		private List<Player> _Player = new List<Player>();
	}
}
