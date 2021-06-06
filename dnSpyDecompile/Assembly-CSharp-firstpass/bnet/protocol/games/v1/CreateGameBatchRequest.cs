using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x020003AB RID: 939
	public class CreateGameBatchRequest : IProtoBuf
	{
		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06003D0C RID: 15628 RVA: 0x000C4CD7 File Offset: 0x000C2ED7
		// (set) Token: 0x06003D0D RID: 15629 RVA: 0x000C4CDF File Offset: 0x000C2EDF
		public List<CreateGameBatchServerEntry> CreateRequestsPerServer
		{
			get
			{
				return this._CreateRequestsPerServer;
			}
			set
			{
				this._CreateRequestsPerServer = value;
			}
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06003D0E RID: 15630 RVA: 0x000C4CD7 File Offset: 0x000C2ED7
		public List<CreateGameBatchServerEntry> CreateRequestsPerServerList
		{
			get
			{
				return this._CreateRequestsPerServer;
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06003D0F RID: 15631 RVA: 0x000C4CE8 File Offset: 0x000C2EE8
		public int CreateRequestsPerServerCount
		{
			get
			{
				return this._CreateRequestsPerServer.Count;
			}
		}

		// Token: 0x06003D10 RID: 15632 RVA: 0x000C4CF5 File Offset: 0x000C2EF5
		public void AddCreateRequestsPerServer(CreateGameBatchServerEntry val)
		{
			this._CreateRequestsPerServer.Add(val);
		}

		// Token: 0x06003D11 RID: 15633 RVA: 0x000C4D03 File Offset: 0x000C2F03
		public void ClearCreateRequestsPerServer()
		{
			this._CreateRequestsPerServer.Clear();
		}

		// Token: 0x06003D12 RID: 15634 RVA: 0x000C4D10 File Offset: 0x000C2F10
		public void SetCreateRequestsPerServer(List<CreateGameBatchServerEntry> val)
		{
			this.CreateRequestsPerServer = val;
		}

		// Token: 0x06003D13 RID: 15635 RVA: 0x000C4D1C File Offset: 0x000C2F1C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (CreateGameBatchServerEntry createGameBatchServerEntry in this.CreateRequestsPerServer)
			{
				num ^= createGameBatchServerEntry.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003D14 RID: 15636 RVA: 0x000C4D80 File Offset: 0x000C2F80
		public override bool Equals(object obj)
		{
			CreateGameBatchRequest createGameBatchRequest = obj as CreateGameBatchRequest;
			if (createGameBatchRequest == null)
			{
				return false;
			}
			if (this.CreateRequestsPerServer.Count != createGameBatchRequest.CreateRequestsPerServer.Count)
			{
				return false;
			}
			for (int i = 0; i < this.CreateRequestsPerServer.Count; i++)
			{
				if (!this.CreateRequestsPerServer[i].Equals(createGameBatchRequest.CreateRequestsPerServer[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06003D15 RID: 15637 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003D16 RID: 15638 RVA: 0x000C4DEB File Offset: 0x000C2FEB
		public static CreateGameBatchRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateGameBatchRequest>(bs, 0, -1);
		}

		// Token: 0x06003D17 RID: 15639 RVA: 0x000C4DF5 File Offset: 0x000C2FF5
		public void Deserialize(Stream stream)
		{
			CreateGameBatchRequest.Deserialize(stream, this);
		}

		// Token: 0x06003D18 RID: 15640 RVA: 0x000C4DFF File Offset: 0x000C2FFF
		public static CreateGameBatchRequest Deserialize(Stream stream, CreateGameBatchRequest instance)
		{
			return CreateGameBatchRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003D19 RID: 15641 RVA: 0x000C4E0C File Offset: 0x000C300C
		public static CreateGameBatchRequest DeserializeLengthDelimited(Stream stream)
		{
			CreateGameBatchRequest createGameBatchRequest = new CreateGameBatchRequest();
			CreateGameBatchRequest.DeserializeLengthDelimited(stream, createGameBatchRequest);
			return createGameBatchRequest;
		}

		// Token: 0x06003D1A RID: 15642 RVA: 0x000C4E28 File Offset: 0x000C3028
		public static CreateGameBatchRequest DeserializeLengthDelimited(Stream stream, CreateGameBatchRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateGameBatchRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003D1B RID: 15643 RVA: 0x000C4E50 File Offset: 0x000C3050
		public static CreateGameBatchRequest Deserialize(Stream stream, CreateGameBatchRequest instance, long limit)
		{
			if (instance.CreateRequestsPerServer == null)
			{
				instance.CreateRequestsPerServer = new List<CreateGameBatchServerEntry>();
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
					instance.CreateRequestsPerServer.Add(CreateGameBatchServerEntry.DeserializeLengthDelimited(stream));
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

		// Token: 0x06003D1C RID: 15644 RVA: 0x000C4EE8 File Offset: 0x000C30E8
		public void Serialize(Stream stream)
		{
			CreateGameBatchRequest.Serialize(stream, this);
		}

		// Token: 0x06003D1D RID: 15645 RVA: 0x000C4EF4 File Offset: 0x000C30F4
		public static void Serialize(Stream stream, CreateGameBatchRequest instance)
		{
			if (instance.CreateRequestsPerServer.Count > 0)
			{
				foreach (CreateGameBatchServerEntry createGameBatchServerEntry in instance.CreateRequestsPerServer)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, createGameBatchServerEntry.GetSerializedSize());
					CreateGameBatchServerEntry.Serialize(stream, createGameBatchServerEntry);
				}
			}
		}

		// Token: 0x06003D1E RID: 15646 RVA: 0x000C4F6C File Offset: 0x000C316C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.CreateRequestsPerServer.Count > 0)
			{
				foreach (CreateGameBatchServerEntry createGameBatchServerEntry in this.CreateRequestsPerServer)
				{
					num += 1U;
					uint serializedSize = createGameBatchServerEntry.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x040015CF RID: 5583
		private List<CreateGameBatchServerEntry> _CreateRequestsPerServer = new List<CreateGameBatchServerEntry>();
	}
}
