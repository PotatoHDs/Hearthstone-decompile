using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x020003AA RID: 938
	public class CreateGameBatchServerEntry : IProtoBuf
	{
		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06003CF5 RID: 15605 RVA: 0x000C48DB File Offset: 0x000C2ADB
		// (set) Token: 0x06003CF6 RID: 15606 RVA: 0x000C48E3 File Offset: 0x000C2AE3
		public ProcessId Host
		{
			get
			{
				return this._Host;
			}
			set
			{
				this._Host = value;
				this.HasHost = (value != null);
			}
		}

		// Token: 0x06003CF7 RID: 15607 RVA: 0x000C48F6 File Offset: 0x000C2AF6
		public void SetHost(ProcessId val)
		{
			this.Host = val;
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06003CF8 RID: 15608 RVA: 0x000C48FF File Offset: 0x000C2AFF
		// (set) Token: 0x06003CF9 RID: 15609 RVA: 0x000C4907 File Offset: 0x000C2B07
		public List<CreateGameRequest> CreateRequests
		{
			get
			{
				return this._CreateRequests;
			}
			set
			{
				this._CreateRequests = value;
			}
		}

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06003CFA RID: 15610 RVA: 0x000C48FF File Offset: 0x000C2AFF
		public List<CreateGameRequest> CreateRequestsList
		{
			get
			{
				return this._CreateRequests;
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06003CFB RID: 15611 RVA: 0x000C4910 File Offset: 0x000C2B10
		public int CreateRequestsCount
		{
			get
			{
				return this._CreateRequests.Count;
			}
		}

		// Token: 0x06003CFC RID: 15612 RVA: 0x000C491D File Offset: 0x000C2B1D
		public void AddCreateRequests(CreateGameRequest val)
		{
			this._CreateRequests.Add(val);
		}

		// Token: 0x06003CFD RID: 15613 RVA: 0x000C492B File Offset: 0x000C2B2B
		public void ClearCreateRequests()
		{
			this._CreateRequests.Clear();
		}

		// Token: 0x06003CFE RID: 15614 RVA: 0x000C4938 File Offset: 0x000C2B38
		public void SetCreateRequests(List<CreateGameRequest> val)
		{
			this.CreateRequests = val;
		}

		// Token: 0x06003CFF RID: 15615 RVA: 0x000C4944 File Offset: 0x000C2B44
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasHost)
			{
				num ^= this.Host.GetHashCode();
			}
			foreach (CreateGameRequest createGameRequest in this.CreateRequests)
			{
				num ^= createGameRequest.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003D00 RID: 15616 RVA: 0x000C49BC File Offset: 0x000C2BBC
		public override bool Equals(object obj)
		{
			CreateGameBatchServerEntry createGameBatchServerEntry = obj as CreateGameBatchServerEntry;
			if (createGameBatchServerEntry == null)
			{
				return false;
			}
			if (this.HasHost != createGameBatchServerEntry.HasHost || (this.HasHost && !this.Host.Equals(createGameBatchServerEntry.Host)))
			{
				return false;
			}
			if (this.CreateRequests.Count != createGameBatchServerEntry.CreateRequests.Count)
			{
				return false;
			}
			for (int i = 0; i < this.CreateRequests.Count; i++)
			{
				if (!this.CreateRequests[i].Equals(createGameBatchServerEntry.CreateRequests[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06003D01 RID: 15617 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003D02 RID: 15618 RVA: 0x000C4A52 File Offset: 0x000C2C52
		public static CreateGameBatchServerEntry ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateGameBatchServerEntry>(bs, 0, -1);
		}

		// Token: 0x06003D03 RID: 15619 RVA: 0x000C4A5C File Offset: 0x000C2C5C
		public void Deserialize(Stream stream)
		{
			CreateGameBatchServerEntry.Deserialize(stream, this);
		}

		// Token: 0x06003D04 RID: 15620 RVA: 0x000C4A66 File Offset: 0x000C2C66
		public static CreateGameBatchServerEntry Deserialize(Stream stream, CreateGameBatchServerEntry instance)
		{
			return CreateGameBatchServerEntry.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003D05 RID: 15621 RVA: 0x000C4A74 File Offset: 0x000C2C74
		public static CreateGameBatchServerEntry DeserializeLengthDelimited(Stream stream)
		{
			CreateGameBatchServerEntry createGameBatchServerEntry = new CreateGameBatchServerEntry();
			CreateGameBatchServerEntry.DeserializeLengthDelimited(stream, createGameBatchServerEntry);
			return createGameBatchServerEntry;
		}

		// Token: 0x06003D06 RID: 15622 RVA: 0x000C4A90 File Offset: 0x000C2C90
		public static CreateGameBatchServerEntry DeserializeLengthDelimited(Stream stream, CreateGameBatchServerEntry instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateGameBatchServerEntry.Deserialize(stream, instance, num);
		}

		// Token: 0x06003D07 RID: 15623 RVA: 0x000C4AB8 File Offset: 0x000C2CB8
		public static CreateGameBatchServerEntry Deserialize(Stream stream, CreateGameBatchServerEntry instance, long limit)
		{
			if (instance.CreateRequests == null)
			{
				instance.CreateRequests = new List<CreateGameRequest>();
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else
					{
						instance.CreateRequests.Add(CreateGameRequest.DeserializeLengthDelimited(stream));
					}
				}
				else if (instance.Host == null)
				{
					instance.Host = ProcessId.DeserializeLengthDelimited(stream);
				}
				else
				{
					ProcessId.DeserializeLengthDelimited(stream, instance.Host);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003D08 RID: 15624 RVA: 0x000C4B82 File Offset: 0x000C2D82
		public void Serialize(Stream stream)
		{
			CreateGameBatchServerEntry.Serialize(stream, this);
		}

		// Token: 0x06003D09 RID: 15625 RVA: 0x000C4B8C File Offset: 0x000C2D8C
		public static void Serialize(Stream stream, CreateGameBatchServerEntry instance)
		{
			if (instance.HasHost)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
			if (instance.CreateRequests.Count > 0)
			{
				foreach (CreateGameRequest createGameRequest in instance.CreateRequests)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, createGameRequest.GetSerializedSize());
					CreateGameRequest.Serialize(stream, createGameRequest);
				}
			}
		}

		// Token: 0x06003D0A RID: 15626 RVA: 0x000C4C30 File Offset: 0x000C2E30
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasHost)
			{
				num += 1U;
				uint serializedSize = this.Host.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.CreateRequests.Count > 0)
			{
				foreach (CreateGameRequest createGameRequest in this.CreateRequests)
				{
					num += 1U;
					uint serializedSize2 = createGameRequest.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		// Token: 0x040015CC RID: 5580
		public bool HasHost;

		// Token: 0x040015CD RID: 5581
		private ProcessId _Host;

		// Token: 0x040015CE RID: 5582
		private List<CreateGameRequest> _CreateRequests = new List<CreateGameRequest>();
	}
}
