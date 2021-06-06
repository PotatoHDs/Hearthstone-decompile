using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x0200038F RID: 911
	public class GetFindGameRequestsResponse : IProtoBuf
	{
		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06003A4B RID: 14923 RVA: 0x000BD11A File Offset: 0x000BB31A
		// (set) Token: 0x06003A4C RID: 14924 RVA: 0x000BD122 File Offset: 0x000BB322
		public List<FindGameRequest> FindGameRequest
		{
			get
			{
				return this._FindGameRequest;
			}
			set
			{
				this._FindGameRequest = value;
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06003A4D RID: 14925 RVA: 0x000BD11A File Offset: 0x000BB31A
		public List<FindGameRequest> FindGameRequestList
		{
			get
			{
				return this._FindGameRequest;
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06003A4E RID: 14926 RVA: 0x000BD12B File Offset: 0x000BB32B
		public int FindGameRequestCount
		{
			get
			{
				return this._FindGameRequest.Count;
			}
		}

		// Token: 0x06003A4F RID: 14927 RVA: 0x000BD138 File Offset: 0x000BB338
		public void AddFindGameRequest(FindGameRequest val)
		{
			this._FindGameRequest.Add(val);
		}

		// Token: 0x06003A50 RID: 14928 RVA: 0x000BD146 File Offset: 0x000BB346
		public void ClearFindGameRequest()
		{
			this._FindGameRequest.Clear();
		}

		// Token: 0x06003A51 RID: 14929 RVA: 0x000BD153 File Offset: 0x000BB353
		public void SetFindGameRequest(List<FindGameRequest> val)
		{
			this.FindGameRequest = val;
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06003A52 RID: 14930 RVA: 0x000BD15C File Offset: 0x000BB35C
		// (set) Token: 0x06003A53 RID: 14931 RVA: 0x000BD164 File Offset: 0x000BB364
		public uint QueueDepth
		{
			get
			{
				return this._QueueDepth;
			}
			set
			{
				this._QueueDepth = value;
				this.HasQueueDepth = true;
			}
		}

		// Token: 0x06003A54 RID: 14932 RVA: 0x000BD174 File Offset: 0x000BB374
		public void SetQueueDepth(uint val)
		{
			this.QueueDepth = val;
		}

		// Token: 0x06003A55 RID: 14933 RVA: 0x000BD180 File Offset: 0x000BB380
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (FindGameRequest findGameRequest in this.FindGameRequest)
			{
				num ^= findGameRequest.GetHashCode();
			}
			if (this.HasQueueDepth)
			{
				num ^= this.QueueDepth.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003A56 RID: 14934 RVA: 0x000BD1FC File Offset: 0x000BB3FC
		public override bool Equals(object obj)
		{
			GetFindGameRequestsResponse getFindGameRequestsResponse = obj as GetFindGameRequestsResponse;
			if (getFindGameRequestsResponse == null)
			{
				return false;
			}
			if (this.FindGameRequest.Count != getFindGameRequestsResponse.FindGameRequest.Count)
			{
				return false;
			}
			for (int i = 0; i < this.FindGameRequest.Count; i++)
			{
				if (!this.FindGameRequest[i].Equals(getFindGameRequestsResponse.FindGameRequest[i]))
				{
					return false;
				}
			}
			return this.HasQueueDepth == getFindGameRequestsResponse.HasQueueDepth && (!this.HasQueueDepth || this.QueueDepth.Equals(getFindGameRequestsResponse.QueueDepth));
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06003A57 RID: 14935 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003A58 RID: 14936 RVA: 0x000BD295 File Offset: 0x000BB495
		public static GetFindGameRequestsResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFindGameRequestsResponse>(bs, 0, -1);
		}

		// Token: 0x06003A59 RID: 14937 RVA: 0x000BD29F File Offset: 0x000BB49F
		public void Deserialize(Stream stream)
		{
			GetFindGameRequestsResponse.Deserialize(stream, this);
		}

		// Token: 0x06003A5A RID: 14938 RVA: 0x000BD2A9 File Offset: 0x000BB4A9
		public static GetFindGameRequestsResponse Deserialize(Stream stream, GetFindGameRequestsResponse instance)
		{
			return GetFindGameRequestsResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003A5B RID: 14939 RVA: 0x000BD2B4 File Offset: 0x000BB4B4
		public static GetFindGameRequestsResponse DeserializeLengthDelimited(Stream stream)
		{
			GetFindGameRequestsResponse getFindGameRequestsResponse = new GetFindGameRequestsResponse();
			GetFindGameRequestsResponse.DeserializeLengthDelimited(stream, getFindGameRequestsResponse);
			return getFindGameRequestsResponse;
		}

		// Token: 0x06003A5C RID: 14940 RVA: 0x000BD2D0 File Offset: 0x000BB4D0
		public static GetFindGameRequestsResponse DeserializeLengthDelimited(Stream stream, GetFindGameRequestsResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetFindGameRequestsResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06003A5D RID: 14941 RVA: 0x000BD2F8 File Offset: 0x000BB4F8
		public static GetFindGameRequestsResponse Deserialize(Stream stream, GetFindGameRequestsResponse instance, long limit)
		{
			if (instance.FindGameRequest == null)
			{
				instance.FindGameRequest = new List<FindGameRequest>();
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
						instance.QueueDepth = ProtocolParser.ReadUInt32(stream);
					}
				}
				else
				{
					instance.FindGameRequest.Add(bnet.protocol.games.v1.FindGameRequest.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003A5E RID: 14942 RVA: 0x000BD3A8 File Offset: 0x000BB5A8
		public void Serialize(Stream stream)
		{
			GetFindGameRequestsResponse.Serialize(stream, this);
		}

		// Token: 0x06003A5F RID: 14943 RVA: 0x000BD3B4 File Offset: 0x000BB5B4
		public static void Serialize(Stream stream, GetFindGameRequestsResponse instance)
		{
			if (instance.FindGameRequest.Count > 0)
			{
				foreach (FindGameRequest findGameRequest in instance.FindGameRequest)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, findGameRequest.GetSerializedSize());
					bnet.protocol.games.v1.FindGameRequest.Serialize(stream, findGameRequest);
				}
			}
			if (instance.HasQueueDepth)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.QueueDepth);
			}
		}

		// Token: 0x06003A60 RID: 14944 RVA: 0x000BD448 File Offset: 0x000BB648
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.FindGameRequest.Count > 0)
			{
				foreach (FindGameRequest findGameRequest in this.FindGameRequest)
				{
					num += 1U;
					uint serializedSize = findGameRequest.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasQueueDepth)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.QueueDepth);
			}
			return num;
		}

		// Token: 0x04001537 RID: 5431
		private List<FindGameRequest> _FindGameRequest = new List<FindGameRequest>();

		// Token: 0x04001538 RID: 5432
		public bool HasQueueDepth;

		// Token: 0x04001539 RID: 5433
		private uint _QueueDepth;
	}
}
