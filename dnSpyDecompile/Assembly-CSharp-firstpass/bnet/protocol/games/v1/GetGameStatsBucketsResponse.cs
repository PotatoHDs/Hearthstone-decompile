using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000391 RID: 913
	public class GetGameStatsBucketsResponse : IProtoBuf
	{
		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06003A75 RID: 14965 RVA: 0x000BD7EA File Offset: 0x000BB9EA
		// (set) Token: 0x06003A76 RID: 14966 RVA: 0x000BD7F2 File Offset: 0x000BB9F2
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

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06003A77 RID: 14967 RVA: 0x000BD7EA File Offset: 0x000BB9EA
		public List<GameStatsBucket> StatsBucketList
		{
			get
			{
				return this._StatsBucket;
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06003A78 RID: 14968 RVA: 0x000BD7FB File Offset: 0x000BB9FB
		public int StatsBucketCount
		{
			get
			{
				return this._StatsBucket.Count;
			}
		}

		// Token: 0x06003A79 RID: 14969 RVA: 0x000BD808 File Offset: 0x000BBA08
		public void AddStatsBucket(GameStatsBucket val)
		{
			this._StatsBucket.Add(val);
		}

		// Token: 0x06003A7A RID: 14970 RVA: 0x000BD816 File Offset: 0x000BBA16
		public void ClearStatsBucket()
		{
			this._StatsBucket.Clear();
		}

		// Token: 0x06003A7B RID: 14971 RVA: 0x000BD823 File Offset: 0x000BBA23
		public void SetStatsBucket(List<GameStatsBucket> val)
		{
			this.StatsBucket = val;
		}

		// Token: 0x06003A7C RID: 14972 RVA: 0x000BD82C File Offset: 0x000BBA2C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (GameStatsBucket gameStatsBucket in this.StatsBucket)
			{
				num ^= gameStatsBucket.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x000BD890 File Offset: 0x000BBA90
		public override bool Equals(object obj)
		{
			GetGameStatsBucketsResponse getGameStatsBucketsResponse = obj as GetGameStatsBucketsResponse;
			if (getGameStatsBucketsResponse == null)
			{
				return false;
			}
			if (this.StatsBucket.Count != getGameStatsBucketsResponse.StatsBucket.Count)
			{
				return false;
			}
			for (int i = 0; i < this.StatsBucket.Count; i++)
			{
				if (!this.StatsBucket[i].Equals(getGameStatsBucketsResponse.StatsBucket[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06003A7E RID: 14974 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003A7F RID: 14975 RVA: 0x000BD8FB File Offset: 0x000BBAFB
		public static GetGameStatsBucketsResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGameStatsBucketsResponse>(bs, 0, -1);
		}

		// Token: 0x06003A80 RID: 14976 RVA: 0x000BD905 File Offset: 0x000BBB05
		public void Deserialize(Stream stream)
		{
			GetGameStatsBucketsResponse.Deserialize(stream, this);
		}

		// Token: 0x06003A81 RID: 14977 RVA: 0x000BD90F File Offset: 0x000BBB0F
		public static GetGameStatsBucketsResponse Deserialize(Stream stream, GetGameStatsBucketsResponse instance)
		{
			return GetGameStatsBucketsResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003A82 RID: 14978 RVA: 0x000BD91C File Offset: 0x000BBB1C
		public static GetGameStatsBucketsResponse DeserializeLengthDelimited(Stream stream)
		{
			GetGameStatsBucketsResponse getGameStatsBucketsResponse = new GetGameStatsBucketsResponse();
			GetGameStatsBucketsResponse.DeserializeLengthDelimited(stream, getGameStatsBucketsResponse);
			return getGameStatsBucketsResponse;
		}

		// Token: 0x06003A83 RID: 14979 RVA: 0x000BD938 File Offset: 0x000BBB38
		public static GetGameStatsBucketsResponse DeserializeLengthDelimited(Stream stream, GetGameStatsBucketsResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetGameStatsBucketsResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06003A84 RID: 14980 RVA: 0x000BD960 File Offset: 0x000BBB60
		public static GetGameStatsBucketsResponse Deserialize(Stream stream, GetGameStatsBucketsResponse instance, long limit)
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

		// Token: 0x06003A85 RID: 14981 RVA: 0x000BD9F8 File Offset: 0x000BBBF8
		public void Serialize(Stream stream)
		{
			GetGameStatsBucketsResponse.Serialize(stream, this);
		}

		// Token: 0x06003A86 RID: 14982 RVA: 0x000BDA04 File Offset: 0x000BBC04
		public static void Serialize(Stream stream, GetGameStatsBucketsResponse instance)
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

		// Token: 0x06003A87 RID: 14983 RVA: 0x000BDA7C File Offset: 0x000BBC7C
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

		// Token: 0x0400153E RID: 5438
		private List<GameStatsBucket> _StatsBucket = new List<GameStatsBucket>();
	}
}
