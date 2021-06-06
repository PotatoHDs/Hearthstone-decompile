using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000386 RID: 902
	public class GetGameStatsResponse : IProtoBuf
	{
		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x0600398F RID: 14735 RVA: 0x000BB40B File Offset: 0x000B960B
		// (set) Token: 0x06003990 RID: 14736 RVA: 0x000BB413 File Offset: 0x000B9613
		public List<GameStatsBucket> StatsBucket
		{
			get
			{
				return this._StatsBucket;
			}
			set
			{
				this._StatsBucket = value;
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06003991 RID: 14737 RVA: 0x000BB40B File Offset: 0x000B960B
		public List<GameStatsBucket> StatsBucketList
		{
			get
			{
				return this._StatsBucket;
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06003992 RID: 14738 RVA: 0x000BB41C File Offset: 0x000B961C
		public int StatsBucketCount
		{
			get
			{
				return this._StatsBucket.Count;
			}
		}

		// Token: 0x06003993 RID: 14739 RVA: 0x000BB429 File Offset: 0x000B9629
		public void AddStatsBucket(GameStatsBucket val)
		{
			this._StatsBucket.Add(val);
		}

		// Token: 0x06003994 RID: 14740 RVA: 0x000BB437 File Offset: 0x000B9637
		public void ClearStatsBucket()
		{
			this._StatsBucket.Clear();
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x000BB444 File Offset: 0x000B9644
		public void SetStatsBucket(List<GameStatsBucket> val)
		{
			this.StatsBucket = val;
		}

		// Token: 0x06003996 RID: 14742 RVA: 0x000BB450 File Offset: 0x000B9650
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (GameStatsBucket gameStatsBucket in this.StatsBucket)
			{
				num ^= gameStatsBucket.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003997 RID: 14743 RVA: 0x000BB4B4 File Offset: 0x000B96B4
		public override bool Equals(object obj)
		{
			GetGameStatsResponse getGameStatsResponse = obj as GetGameStatsResponse;
			if (getGameStatsResponse == null)
			{
				return false;
			}
			if (this.StatsBucket.Count != getGameStatsResponse.StatsBucket.Count)
			{
				return false;
			}
			for (int i = 0; i < this.StatsBucket.Count; i++)
			{
				if (!this.StatsBucket[i].Equals(getGameStatsResponse.StatsBucket[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06003998 RID: 14744 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003999 RID: 14745 RVA: 0x000BB51F File Offset: 0x000B971F
		public static GetGameStatsResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGameStatsResponse>(bs, 0, -1);
		}

		// Token: 0x0600399A RID: 14746 RVA: 0x000BB529 File Offset: 0x000B9729
		public void Deserialize(Stream stream)
		{
			GetGameStatsResponse.Deserialize(stream, this);
		}

		// Token: 0x0600399B RID: 14747 RVA: 0x000BB533 File Offset: 0x000B9733
		public static GetGameStatsResponse Deserialize(Stream stream, GetGameStatsResponse instance)
		{
			return GetGameStatsResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600399C RID: 14748 RVA: 0x000BB540 File Offset: 0x000B9740
		public static GetGameStatsResponse DeserializeLengthDelimited(Stream stream)
		{
			GetGameStatsResponse getGameStatsResponse = new GetGameStatsResponse();
			GetGameStatsResponse.DeserializeLengthDelimited(stream, getGameStatsResponse);
			return getGameStatsResponse;
		}

		// Token: 0x0600399D RID: 14749 RVA: 0x000BB55C File Offset: 0x000B975C
		public static GetGameStatsResponse DeserializeLengthDelimited(Stream stream, GetGameStatsResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetGameStatsResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x0600399E RID: 14750 RVA: 0x000BB584 File Offset: 0x000B9784
		public static GetGameStatsResponse Deserialize(Stream stream, GetGameStatsResponse instance, long limit)
		{
			if (instance.StatsBucket == null)
			{
				instance.StatsBucket = new List<GameStatsBucket>();
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
					instance.StatsBucket.Add(GameStatsBucket.DeserializeLengthDelimited(stream));
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

		// Token: 0x0600399F RID: 14751 RVA: 0x000BB61C File Offset: 0x000B981C
		public void Serialize(Stream stream)
		{
			GetGameStatsResponse.Serialize(stream, this);
		}

		// Token: 0x060039A0 RID: 14752 RVA: 0x000BB628 File Offset: 0x000B9828
		public static void Serialize(Stream stream, GetGameStatsResponse instance)
		{
			if (instance.StatsBucket.Count > 0)
			{
				foreach (GameStatsBucket gameStatsBucket in instance.StatsBucket)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, gameStatsBucket.GetSerializedSize());
					GameStatsBucket.Serialize(stream, gameStatsBucket);
				}
			}
		}

		// Token: 0x060039A1 RID: 14753 RVA: 0x000BB6A0 File Offset: 0x000B98A0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.StatsBucket.Count > 0)
			{
				foreach (GameStatsBucket gameStatsBucket in this.StatsBucket)
				{
					num += 1U;
					uint serializedSize = gameStatsBucket.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x0400151A RID: 5402
		private List<GameStatsBucket> _StatsBucket = new List<GameStatsBucket>();
	}
}
