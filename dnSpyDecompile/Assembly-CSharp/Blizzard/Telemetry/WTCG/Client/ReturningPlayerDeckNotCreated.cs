using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011EE RID: 4590
	public class ReturningPlayerDeckNotCreated : IProtoBuf
	{
		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x0600CD18 RID: 52504 RVA: 0x003D3202 File Offset: 0x003D1402
		// (set) Token: 0x0600CD19 RID: 52505 RVA: 0x003D320A File Offset: 0x003D140A
		public Player Player
		{
			get
			{
				return this._Player;
			}
			set
			{
				this._Player = value;
				this.HasPlayer = (value != null);
			}
		}

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x0600CD1A RID: 52506 RVA: 0x003D321D File Offset: 0x003D141D
		// (set) Token: 0x0600CD1B RID: 52507 RVA: 0x003D3225 File Offset: 0x003D1425
		public uint ABGroup
		{
			get
			{
				return this._ABGroup;
			}
			set
			{
				this._ABGroup = value;
				this.HasABGroup = true;
			}
		}

		// Token: 0x0600CD1C RID: 52508 RVA: 0x003D3238 File Offset: 0x003D1438
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPlayer)
			{
				num ^= this.Player.GetHashCode();
			}
			if (this.HasABGroup)
			{
				num ^= this.ABGroup.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CD1D RID: 52509 RVA: 0x003D3284 File Offset: 0x003D1484
		public override bool Equals(object obj)
		{
			ReturningPlayerDeckNotCreated returningPlayerDeckNotCreated = obj as ReturningPlayerDeckNotCreated;
			return returningPlayerDeckNotCreated != null && this.HasPlayer == returningPlayerDeckNotCreated.HasPlayer && (!this.HasPlayer || this.Player.Equals(returningPlayerDeckNotCreated.Player)) && this.HasABGroup == returningPlayerDeckNotCreated.HasABGroup && (!this.HasABGroup || this.ABGroup.Equals(returningPlayerDeckNotCreated.ABGroup));
		}

		// Token: 0x0600CD1E RID: 52510 RVA: 0x003D32F7 File Offset: 0x003D14F7
		public void Deserialize(Stream stream)
		{
			ReturningPlayerDeckNotCreated.Deserialize(stream, this);
		}

		// Token: 0x0600CD1F RID: 52511 RVA: 0x003D3301 File Offset: 0x003D1501
		public static ReturningPlayerDeckNotCreated Deserialize(Stream stream, ReturningPlayerDeckNotCreated instance)
		{
			return ReturningPlayerDeckNotCreated.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CD20 RID: 52512 RVA: 0x003D330C File Offset: 0x003D150C
		public static ReturningPlayerDeckNotCreated DeserializeLengthDelimited(Stream stream)
		{
			ReturningPlayerDeckNotCreated returningPlayerDeckNotCreated = new ReturningPlayerDeckNotCreated();
			ReturningPlayerDeckNotCreated.DeserializeLengthDelimited(stream, returningPlayerDeckNotCreated);
			return returningPlayerDeckNotCreated;
		}

		// Token: 0x0600CD21 RID: 52513 RVA: 0x003D3328 File Offset: 0x003D1528
		public static ReturningPlayerDeckNotCreated DeserializeLengthDelimited(Stream stream, ReturningPlayerDeckNotCreated instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ReturningPlayerDeckNotCreated.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CD22 RID: 52514 RVA: 0x003D3350 File Offset: 0x003D1550
		public static ReturningPlayerDeckNotCreated Deserialize(Stream stream, ReturningPlayerDeckNotCreated instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else
					{
						instance.ABGroup = ProtocolParser.ReadUInt32(stream);
					}
				}
				else if (instance.Player == null)
				{
					instance.Player = Player.DeserializeLengthDelimited(stream);
				}
				else
				{
					Player.DeserializeLengthDelimited(stream, instance.Player);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600CD23 RID: 52515 RVA: 0x003D3402 File Offset: 0x003D1602
		public void Serialize(Stream stream)
		{
			ReturningPlayerDeckNotCreated.Serialize(stream, this);
		}

		// Token: 0x0600CD24 RID: 52516 RVA: 0x003D340C File Offset: 0x003D160C
		public static void Serialize(Stream stream, ReturningPlayerDeckNotCreated instance)
		{
			if (instance.HasPlayer)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.HasABGroup)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.ABGroup);
			}
		}

		// Token: 0x0600CD25 RID: 52517 RVA: 0x003D3464 File Offset: 0x003D1664
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasPlayer)
			{
				num += 1U;
				uint serializedSize = this.Player.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasABGroup)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.ABGroup);
			}
			return num;
		}

		// Token: 0x0400A0D7 RID: 41175
		public bool HasPlayer;

		// Token: 0x0400A0D8 RID: 41176
		private Player _Player;

		// Token: 0x0400A0D9 RID: 41177
		public bool HasABGroup;

		// Token: 0x0400A0DA RID: 41178
		private uint _ABGroup;
	}
}
