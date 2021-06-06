using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x0200038B RID: 907
	public class RequestGamesRequest : IProtoBuf
	{
		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x060039FD RID: 14845 RVA: 0x000BC5B7 File Offset: 0x000BA7B7
		// (set) Token: 0x060039FE RID: 14846 RVA: 0x000BC5BF File Offset: 0x000BA7BF
		public List<GameRequestServerEntry> GameRequestsPerServer
		{
			get
			{
				return this._GameRequestsPerServer;
			}
			set
			{
				this._GameRequestsPerServer = value;
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x060039FF RID: 14847 RVA: 0x000BC5B7 File Offset: 0x000BA7B7
		public List<GameRequestServerEntry> GameRequestsPerServerList
		{
			get
			{
				return this._GameRequestsPerServer;
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06003A00 RID: 14848 RVA: 0x000BC5C8 File Offset: 0x000BA7C8
		public int GameRequestsPerServerCount
		{
			get
			{
				return this._GameRequestsPerServer.Count;
			}
		}

		// Token: 0x06003A01 RID: 14849 RVA: 0x000BC5D5 File Offset: 0x000BA7D5
		public void AddGameRequestsPerServer(GameRequestServerEntry val)
		{
			this._GameRequestsPerServer.Add(val);
		}

		// Token: 0x06003A02 RID: 14850 RVA: 0x000BC5E3 File Offset: 0x000BA7E3
		public void ClearGameRequestsPerServer()
		{
			this._GameRequestsPerServer.Clear();
		}

		// Token: 0x06003A03 RID: 14851 RVA: 0x000BC5F0 File Offset: 0x000BA7F0
		public void SetGameRequestsPerServer(List<GameRequestServerEntry> val)
		{
			this.GameRequestsPerServer = val;
		}

		// Token: 0x06003A04 RID: 14852 RVA: 0x000BC5FC File Offset: 0x000BA7FC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (GameRequestServerEntry gameRequestServerEntry in this.GameRequestsPerServer)
			{
				num ^= gameRequestServerEntry.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003A05 RID: 14853 RVA: 0x000BC660 File Offset: 0x000BA860
		public override bool Equals(object obj)
		{
			RequestGamesRequest requestGamesRequest = obj as RequestGamesRequest;
			if (requestGamesRequest == null)
			{
				return false;
			}
			if (this.GameRequestsPerServer.Count != requestGamesRequest.GameRequestsPerServer.Count)
			{
				return false;
			}
			for (int i = 0; i < this.GameRequestsPerServer.Count; i++)
			{
				if (!this.GameRequestsPerServer[i].Equals(requestGamesRequest.GameRequestsPerServer[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06003A06 RID: 14854 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003A07 RID: 14855 RVA: 0x000BC6CB File Offset: 0x000BA8CB
		public static RequestGamesRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RequestGamesRequest>(bs, 0, -1);
		}

		// Token: 0x06003A08 RID: 14856 RVA: 0x000BC6D5 File Offset: 0x000BA8D5
		public void Deserialize(Stream stream)
		{
			RequestGamesRequest.Deserialize(stream, this);
		}

		// Token: 0x06003A09 RID: 14857 RVA: 0x000BC6DF File Offset: 0x000BA8DF
		public static RequestGamesRequest Deserialize(Stream stream, RequestGamesRequest instance)
		{
			return RequestGamesRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003A0A RID: 14858 RVA: 0x000BC6EC File Offset: 0x000BA8EC
		public static RequestGamesRequest DeserializeLengthDelimited(Stream stream)
		{
			RequestGamesRequest requestGamesRequest = new RequestGamesRequest();
			RequestGamesRequest.DeserializeLengthDelimited(stream, requestGamesRequest);
			return requestGamesRequest;
		}

		// Token: 0x06003A0B RID: 14859 RVA: 0x000BC708 File Offset: 0x000BA908
		public static RequestGamesRequest DeserializeLengthDelimited(Stream stream, RequestGamesRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RequestGamesRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003A0C RID: 14860 RVA: 0x000BC730 File Offset: 0x000BA930
		public static RequestGamesRequest Deserialize(Stream stream, RequestGamesRequest instance, long limit)
		{
			if (instance.GameRequestsPerServer == null)
			{
				instance.GameRequestsPerServer = new List<GameRequestServerEntry>();
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
					instance.GameRequestsPerServer.Add(GameRequestServerEntry.DeserializeLengthDelimited(stream));
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

		// Token: 0x06003A0D RID: 14861 RVA: 0x000BC7C8 File Offset: 0x000BA9C8
		public void Serialize(Stream stream)
		{
			RequestGamesRequest.Serialize(stream, this);
		}

		// Token: 0x06003A0E RID: 14862 RVA: 0x000BC7D4 File Offset: 0x000BA9D4
		public static void Serialize(Stream stream, RequestGamesRequest instance)
		{
			if (instance.GameRequestsPerServer.Count > 0)
			{
				foreach (GameRequestServerEntry gameRequestServerEntry in instance.GameRequestsPerServer)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, gameRequestServerEntry.GetSerializedSize());
					GameRequestServerEntry.Serialize(stream, gameRequestServerEntry);
				}
			}
		}

		// Token: 0x06003A0F RID: 14863 RVA: 0x000BC84C File Offset: 0x000BAA4C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.GameRequestsPerServer.Count > 0)
			{
				foreach (GameRequestServerEntry gameRequestServerEntry in this.GameRequestsPerServer)
				{
					num += 1U;
					uint serializedSize = gameRequestServerEntry.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x0400152D RID: 5421
		private List<GameRequestServerEntry> _GameRequestsPerServer = new List<GameRequestServerEntry>();
	}
}
