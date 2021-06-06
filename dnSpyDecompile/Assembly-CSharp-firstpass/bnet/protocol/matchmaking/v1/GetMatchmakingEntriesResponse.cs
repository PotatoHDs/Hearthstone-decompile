using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003CA RID: 970
	public class GetMatchmakingEntriesResponse : IProtoBuf
	{
		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06003F6C RID: 16236 RVA: 0x000CAD0E File Offset: 0x000C8F0E
		// (set) Token: 0x06003F6D RID: 16237 RVA: 0x000CAD16 File Offset: 0x000C8F16
		public List<GameMatchmakingEntry> Entry
		{
			get
			{
				return this._Entry;
			}
			set
			{
				this._Entry = value;
			}
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06003F6E RID: 16238 RVA: 0x000CAD0E File Offset: 0x000C8F0E
		public List<GameMatchmakingEntry> EntryList
		{
			get
			{
				return this._Entry;
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06003F6F RID: 16239 RVA: 0x000CAD1F File Offset: 0x000C8F1F
		public int EntryCount
		{
			get
			{
				return this._Entry.Count;
			}
		}

		// Token: 0x06003F70 RID: 16240 RVA: 0x000CAD2C File Offset: 0x000C8F2C
		public void AddEntry(GameMatchmakingEntry val)
		{
			this._Entry.Add(val);
		}

		// Token: 0x06003F71 RID: 16241 RVA: 0x000CAD3A File Offset: 0x000C8F3A
		public void ClearEntry()
		{
			this._Entry.Clear();
		}

		// Token: 0x06003F72 RID: 16242 RVA: 0x000CAD47 File Offset: 0x000C8F47
		public void SetEntry(List<GameMatchmakingEntry> val)
		{
			this.Entry = val;
		}

		// Token: 0x06003F73 RID: 16243 RVA: 0x000CAD50 File Offset: 0x000C8F50
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (GameMatchmakingEntry gameMatchmakingEntry in this.Entry)
			{
				num ^= gameMatchmakingEntry.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003F74 RID: 16244 RVA: 0x000CADB4 File Offset: 0x000C8FB4
		public override bool Equals(object obj)
		{
			GetMatchmakingEntriesResponse getMatchmakingEntriesResponse = obj as GetMatchmakingEntriesResponse;
			if (getMatchmakingEntriesResponse == null)
			{
				return false;
			}
			if (this.Entry.Count != getMatchmakingEntriesResponse.Entry.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Entry.Count; i++)
			{
				if (!this.Entry[i].Equals(getMatchmakingEntriesResponse.Entry[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06003F75 RID: 16245 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003F76 RID: 16246 RVA: 0x000CAE1F File Offset: 0x000C901F
		public static GetMatchmakingEntriesResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetMatchmakingEntriesResponse>(bs, 0, -1);
		}

		// Token: 0x06003F77 RID: 16247 RVA: 0x000CAE29 File Offset: 0x000C9029
		public void Deserialize(Stream stream)
		{
			GetMatchmakingEntriesResponse.Deserialize(stream, this);
		}

		// Token: 0x06003F78 RID: 16248 RVA: 0x000CAE33 File Offset: 0x000C9033
		public static GetMatchmakingEntriesResponse Deserialize(Stream stream, GetMatchmakingEntriesResponse instance)
		{
			return GetMatchmakingEntriesResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003F79 RID: 16249 RVA: 0x000CAE40 File Offset: 0x000C9040
		public static GetMatchmakingEntriesResponse DeserializeLengthDelimited(Stream stream)
		{
			GetMatchmakingEntriesResponse getMatchmakingEntriesResponse = new GetMatchmakingEntriesResponse();
			GetMatchmakingEntriesResponse.DeserializeLengthDelimited(stream, getMatchmakingEntriesResponse);
			return getMatchmakingEntriesResponse;
		}

		// Token: 0x06003F7A RID: 16250 RVA: 0x000CAE5C File Offset: 0x000C905C
		public static GetMatchmakingEntriesResponse DeserializeLengthDelimited(Stream stream, GetMatchmakingEntriesResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetMatchmakingEntriesResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06003F7B RID: 16251 RVA: 0x000CAE84 File Offset: 0x000C9084
		public static GetMatchmakingEntriesResponse Deserialize(Stream stream, GetMatchmakingEntriesResponse instance, long limit)
		{
			if (instance.Entry == null)
			{
				instance.Entry = new List<GameMatchmakingEntry>();
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
					instance.Entry.Add(GameMatchmakingEntry.DeserializeLengthDelimited(stream));
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

		// Token: 0x06003F7C RID: 16252 RVA: 0x000CAF1C File Offset: 0x000C911C
		public void Serialize(Stream stream)
		{
			GetMatchmakingEntriesResponse.Serialize(stream, this);
		}

		// Token: 0x06003F7D RID: 16253 RVA: 0x000CAF28 File Offset: 0x000C9128
		public static void Serialize(Stream stream, GetMatchmakingEntriesResponse instance)
		{
			if (instance.Entry.Count > 0)
			{
				foreach (GameMatchmakingEntry gameMatchmakingEntry in instance.Entry)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, gameMatchmakingEntry.GetSerializedSize());
					GameMatchmakingEntry.Serialize(stream, gameMatchmakingEntry);
				}
			}
		}

		// Token: 0x06003F7E RID: 16254 RVA: 0x000CAFA0 File Offset: 0x000C91A0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Entry.Count > 0)
			{
				foreach (GameMatchmakingEntry gameMatchmakingEntry in this.Entry)
				{
					num += 1U;
					uint serializedSize = gameMatchmakingEntry.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04001640 RID: 5696
		private List<GameMatchmakingEntry> _Entry = new List<GameMatchmakingEntry>();
	}
}
