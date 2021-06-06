using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003E7 RID: 999
	public class CancelMatchmakingRequest : IProtoBuf
	{
		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x060041EA RID: 16874 RVA: 0x000D163F File Offset: 0x000CF83F
		// (set) Token: 0x060041EB RID: 16875 RVA: 0x000D1647 File Offset: 0x000CF847
		public RequestId RequestId
		{
			get
			{
				return this._RequestId;
			}
			set
			{
				this._RequestId = value;
				this.HasRequestId = (value != null);
			}
		}

		// Token: 0x060041EC RID: 16876 RVA: 0x000D165A File Offset: 0x000CF85A
		public void SetRequestId(RequestId val)
		{
			this.RequestId = val;
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x060041ED RID: 16877 RVA: 0x000D1663 File Offset: 0x000CF863
		// (set) Token: 0x060041EE RID: 16878 RVA: 0x000D166B File Offset: 0x000CF86B
		public List<GameAccountHandle> GameAccount
		{
			get
			{
				return this._GameAccount;
			}
			set
			{
				this._GameAccount = value;
			}
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x060041EF RID: 16879 RVA: 0x000D1663 File Offset: 0x000CF863
		public List<GameAccountHandle> GameAccountList
		{
			get
			{
				return this._GameAccount;
			}
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x060041F0 RID: 16880 RVA: 0x000D1674 File Offset: 0x000CF874
		public int GameAccountCount
		{
			get
			{
				return this._GameAccount.Count;
			}
		}

		// Token: 0x060041F1 RID: 16881 RVA: 0x000D1681 File Offset: 0x000CF881
		public void AddGameAccount(GameAccountHandle val)
		{
			this._GameAccount.Add(val);
		}

		// Token: 0x060041F2 RID: 16882 RVA: 0x000D168F File Offset: 0x000CF88F
		public void ClearGameAccount()
		{
			this._GameAccount.Clear();
		}

		// Token: 0x060041F3 RID: 16883 RVA: 0x000D169C File Offset: 0x000CF89C
		public void SetGameAccount(List<GameAccountHandle> val)
		{
			this.GameAccount = val;
		}

		// Token: 0x060041F4 RID: 16884 RVA: 0x000D16A8 File Offset: 0x000CF8A8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRequestId)
			{
				num ^= this.RequestId.GetHashCode();
			}
			foreach (GameAccountHandle gameAccountHandle in this.GameAccount)
			{
				num ^= gameAccountHandle.GetHashCode();
			}
			return num;
		}

		// Token: 0x060041F5 RID: 16885 RVA: 0x000D1720 File Offset: 0x000CF920
		public override bool Equals(object obj)
		{
			CancelMatchmakingRequest cancelMatchmakingRequest = obj as CancelMatchmakingRequest;
			if (cancelMatchmakingRequest == null)
			{
				return false;
			}
			if (this.HasRequestId != cancelMatchmakingRequest.HasRequestId || (this.HasRequestId && !this.RequestId.Equals(cancelMatchmakingRequest.RequestId)))
			{
				return false;
			}
			if (this.GameAccount.Count != cancelMatchmakingRequest.GameAccount.Count)
			{
				return false;
			}
			for (int i = 0; i < this.GameAccount.Count; i++)
			{
				if (!this.GameAccount[i].Equals(cancelMatchmakingRequest.GameAccount[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x060041F6 RID: 16886 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060041F7 RID: 16887 RVA: 0x000D17B6 File Offset: 0x000CF9B6
		public static CancelMatchmakingRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CancelMatchmakingRequest>(bs, 0, -1);
		}

		// Token: 0x060041F8 RID: 16888 RVA: 0x000D17C0 File Offset: 0x000CF9C0
		public void Deserialize(Stream stream)
		{
			CancelMatchmakingRequest.Deserialize(stream, this);
		}

		// Token: 0x060041F9 RID: 16889 RVA: 0x000D17CA File Offset: 0x000CF9CA
		public static CancelMatchmakingRequest Deserialize(Stream stream, CancelMatchmakingRequest instance)
		{
			return CancelMatchmakingRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060041FA RID: 16890 RVA: 0x000D17D8 File Offset: 0x000CF9D8
		public static CancelMatchmakingRequest DeserializeLengthDelimited(Stream stream)
		{
			CancelMatchmakingRequest cancelMatchmakingRequest = new CancelMatchmakingRequest();
			CancelMatchmakingRequest.DeserializeLengthDelimited(stream, cancelMatchmakingRequest);
			return cancelMatchmakingRequest;
		}

		// Token: 0x060041FB RID: 16891 RVA: 0x000D17F4 File Offset: 0x000CF9F4
		public static CancelMatchmakingRequest DeserializeLengthDelimited(Stream stream, CancelMatchmakingRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CancelMatchmakingRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060041FC RID: 16892 RVA: 0x000D181C File Offset: 0x000CFA1C
		public static CancelMatchmakingRequest Deserialize(Stream stream, CancelMatchmakingRequest instance, long limit)
		{
			if (instance.GameAccount == null)
			{
				instance.GameAccount = new List<GameAccountHandle>();
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
						instance.GameAccount.Add(GameAccountHandle.DeserializeLengthDelimited(stream));
					}
				}
				else if (instance.RequestId == null)
				{
					instance.RequestId = RequestId.DeserializeLengthDelimited(stream);
				}
				else
				{
					RequestId.DeserializeLengthDelimited(stream, instance.RequestId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060041FD RID: 16893 RVA: 0x000D18E6 File Offset: 0x000CFAE6
		public void Serialize(Stream stream)
		{
			CancelMatchmakingRequest.Serialize(stream, this);
		}

		// Token: 0x060041FE RID: 16894 RVA: 0x000D18F0 File Offset: 0x000CFAF0
		public static void Serialize(Stream stream, CancelMatchmakingRequest instance)
		{
			if (instance.HasRequestId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.RequestId.GetSerializedSize());
				RequestId.Serialize(stream, instance.RequestId);
			}
			if (instance.GameAccount.Count > 0)
			{
				foreach (GameAccountHandle gameAccountHandle in instance.GameAccount)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, gameAccountHandle.GetSerializedSize());
					GameAccountHandle.Serialize(stream, gameAccountHandle);
				}
			}
		}

		// Token: 0x060041FF RID: 16895 RVA: 0x000D1994 File Offset: 0x000CFB94
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRequestId)
			{
				num += 1U;
				uint serializedSize = this.RequestId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.GameAccount.Count > 0)
			{
				foreach (GameAccountHandle gameAccountHandle in this.GameAccount)
				{
					num += 1U;
					uint serializedSize2 = gameAccountHandle.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		// Token: 0x040016B7 RID: 5815
		public bool HasRequestId;

		// Token: 0x040016B8 RID: 5816
		private RequestId _RequestId;

		// Token: 0x040016B9 RID: 5817
		private List<GameAccountHandle> _GameAccount = new List<GameAccountHandle>();
	}
}
