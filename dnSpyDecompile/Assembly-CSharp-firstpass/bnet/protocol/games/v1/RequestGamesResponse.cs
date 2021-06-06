using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x0200038D RID: 909
	public class RequestGamesResponse : IProtoBuf
	{
		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06003A24 RID: 14884 RVA: 0x000BCB64 File Offset: 0x000BAD64
		// (set) Token: 0x06003A25 RID: 14885 RVA: 0x000BCB6C File Offset: 0x000BAD6C
		public List<GameResponseEntry> GameResponse
		{
			get
			{
				return this._GameResponse;
			}
			set
			{
				this._GameResponse = value;
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06003A26 RID: 14886 RVA: 0x000BCB64 File Offset: 0x000BAD64
		public List<GameResponseEntry> GameResponseList
		{
			get
			{
				return this._GameResponse;
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06003A27 RID: 14887 RVA: 0x000BCB75 File Offset: 0x000BAD75
		public int GameResponseCount
		{
			get
			{
				return this._GameResponse.Count;
			}
		}

		// Token: 0x06003A28 RID: 14888 RVA: 0x000BCB82 File Offset: 0x000BAD82
		public void AddGameResponse(GameResponseEntry val)
		{
			this._GameResponse.Add(val);
		}

		// Token: 0x06003A29 RID: 14889 RVA: 0x000BCB90 File Offset: 0x000BAD90
		public void ClearGameResponse()
		{
			this._GameResponse.Clear();
		}

		// Token: 0x06003A2A RID: 14890 RVA: 0x000BCB9D File Offset: 0x000BAD9D
		public void SetGameResponse(List<GameResponseEntry> val)
		{
			this.GameResponse = val;
		}

		// Token: 0x06003A2B RID: 14891 RVA: 0x000BCBA8 File Offset: 0x000BADA8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (GameResponseEntry gameResponseEntry in this.GameResponse)
			{
				num ^= gameResponseEntry.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003A2C RID: 14892 RVA: 0x000BCC0C File Offset: 0x000BAE0C
		public override bool Equals(object obj)
		{
			RequestGamesResponse requestGamesResponse = obj as RequestGamesResponse;
			if (requestGamesResponse == null)
			{
				return false;
			}
			if (this.GameResponse.Count != requestGamesResponse.GameResponse.Count)
			{
				return false;
			}
			for (int i = 0; i < this.GameResponse.Count; i++)
			{
				if (!this.GameResponse[i].Equals(requestGamesResponse.GameResponse[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06003A2D RID: 14893 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003A2E RID: 14894 RVA: 0x000BCC77 File Offset: 0x000BAE77
		public static RequestGamesResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RequestGamesResponse>(bs, 0, -1);
		}

		// Token: 0x06003A2F RID: 14895 RVA: 0x000BCC81 File Offset: 0x000BAE81
		public void Deserialize(Stream stream)
		{
			RequestGamesResponse.Deserialize(stream, this);
		}

		// Token: 0x06003A30 RID: 14896 RVA: 0x000BCC8B File Offset: 0x000BAE8B
		public static RequestGamesResponse Deserialize(Stream stream, RequestGamesResponse instance)
		{
			return RequestGamesResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003A31 RID: 14897 RVA: 0x000BCC98 File Offset: 0x000BAE98
		public static RequestGamesResponse DeserializeLengthDelimited(Stream stream)
		{
			RequestGamesResponse requestGamesResponse = new RequestGamesResponse();
			RequestGamesResponse.DeserializeLengthDelimited(stream, requestGamesResponse);
			return requestGamesResponse;
		}

		// Token: 0x06003A32 RID: 14898 RVA: 0x000BCCB4 File Offset: 0x000BAEB4
		public static RequestGamesResponse DeserializeLengthDelimited(Stream stream, RequestGamesResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RequestGamesResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06003A33 RID: 14899 RVA: 0x000BCCDC File Offset: 0x000BAEDC
		public static RequestGamesResponse Deserialize(Stream stream, RequestGamesResponse instance, long limit)
		{
			if (instance.GameResponse == null)
			{
				instance.GameResponse = new List<GameResponseEntry>();
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
					instance.GameResponse.Add(GameResponseEntry.DeserializeLengthDelimited(stream));
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

		// Token: 0x06003A34 RID: 14900 RVA: 0x000BCD74 File Offset: 0x000BAF74
		public void Serialize(Stream stream)
		{
			RequestGamesResponse.Serialize(stream, this);
		}

		// Token: 0x06003A35 RID: 14901 RVA: 0x000BCD80 File Offset: 0x000BAF80
		public static void Serialize(Stream stream, RequestGamesResponse instance)
		{
			if (instance.GameResponse.Count > 0)
			{
				foreach (GameResponseEntry gameResponseEntry in instance.GameResponse)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, gameResponseEntry.GetSerializedSize());
					GameResponseEntry.Serialize(stream, gameResponseEntry);
				}
			}
		}

		// Token: 0x06003A36 RID: 14902 RVA: 0x000BCDF8 File Offset: 0x000BAFF8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.GameResponse.Count > 0)
			{
				foreach (GameResponseEntry gameResponseEntry in this.GameResponse)
				{
					num += 1U;
					uint serializedSize = gameResponseEntry.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04001532 RID: 5426
		private List<GameResponseEntry> _GameResponse = new List<GameResponseEntry>();
	}
}
