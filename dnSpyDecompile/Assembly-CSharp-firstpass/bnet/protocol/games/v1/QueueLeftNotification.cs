using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.games.v2;

namespace bnet.protocol.games.v1
{
	// Token: 0x0200039A RID: 922
	public class QueueLeftNotification : IProtoBuf
	{
		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06003B37 RID: 15159 RVA: 0x000BF7E1 File Offset: 0x000BD9E1
		// (set) Token: 0x06003B38 RID: 15160 RVA: 0x000BF7E9 File Offset: 0x000BD9E9
		public FindGameRequestId RequestId
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

		// Token: 0x06003B39 RID: 15161 RVA: 0x000BF7FC File Offset: 0x000BD9FC
		public void SetRequestId(FindGameRequestId val)
		{
			this.RequestId = val;
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06003B3A RID: 15162 RVA: 0x000BF805 File Offset: 0x000BDA05
		// (set) Token: 0x06003B3B RID: 15163 RVA: 0x000BF80D File Offset: 0x000BDA0D
		public uint Result
		{
			get
			{
				return this._Result;
			}
			set
			{
				this._Result = value;
				this.HasResult = true;
			}
		}

		// Token: 0x06003B3C RID: 15164 RVA: 0x000BF81D File Offset: 0x000BDA1D
		public void SetResult(uint val)
		{
			this.Result = val;
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06003B3D RID: 15165 RVA: 0x000BF826 File Offset: 0x000BDA26
		// (set) Token: 0x06003B3E RID: 15166 RVA: 0x000BF82E File Offset: 0x000BDA2E
		public List<GameAccountHandle> Quitter
		{
			get
			{
				return this._Quitter;
			}
			set
			{
				this._Quitter = value;
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06003B3F RID: 15167 RVA: 0x000BF826 File Offset: 0x000BDA26
		public List<GameAccountHandle> QuitterList
		{
			get
			{
				return this._Quitter;
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06003B40 RID: 15168 RVA: 0x000BF837 File Offset: 0x000BDA37
		public int QuitterCount
		{
			get
			{
				return this._Quitter.Count;
			}
		}

		// Token: 0x06003B41 RID: 15169 RVA: 0x000BF844 File Offset: 0x000BDA44
		public void AddQuitter(GameAccountHandle val)
		{
			this._Quitter.Add(val);
		}

		// Token: 0x06003B42 RID: 15170 RVA: 0x000BF852 File Offset: 0x000BDA52
		public void ClearQuitter()
		{
			this._Quitter.Clear();
		}

		// Token: 0x06003B43 RID: 15171 RVA: 0x000BF85F File Offset: 0x000BDA5F
		public void SetQuitter(List<GameAccountHandle> val)
		{
			this.Quitter = val;
		}

		// Token: 0x06003B44 RID: 15172 RVA: 0x000BF868 File Offset: 0x000BDA68
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRequestId)
			{
				num ^= this.RequestId.GetHashCode();
			}
			if (this.HasResult)
			{
				num ^= this.Result.GetHashCode();
			}
			foreach (GameAccountHandle gameAccountHandle in this.Quitter)
			{
				num ^= gameAccountHandle.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003B45 RID: 15173 RVA: 0x000BF8FC File Offset: 0x000BDAFC
		public override bool Equals(object obj)
		{
			QueueLeftNotification queueLeftNotification = obj as QueueLeftNotification;
			if (queueLeftNotification == null)
			{
				return false;
			}
			if (this.HasRequestId != queueLeftNotification.HasRequestId || (this.HasRequestId && !this.RequestId.Equals(queueLeftNotification.RequestId)))
			{
				return false;
			}
			if (this.HasResult != queueLeftNotification.HasResult || (this.HasResult && !this.Result.Equals(queueLeftNotification.Result)))
			{
				return false;
			}
			if (this.Quitter.Count != queueLeftNotification.Quitter.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Quitter.Count; i++)
			{
				if (!this.Quitter[i].Equals(queueLeftNotification.Quitter[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06003B46 RID: 15174 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003B47 RID: 15175 RVA: 0x000BF9C0 File Offset: 0x000BDBC0
		public static QueueLeftNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<QueueLeftNotification>(bs, 0, -1);
		}

		// Token: 0x06003B48 RID: 15176 RVA: 0x000BF9CA File Offset: 0x000BDBCA
		public void Deserialize(Stream stream)
		{
			QueueLeftNotification.Deserialize(stream, this);
		}

		// Token: 0x06003B49 RID: 15177 RVA: 0x000BF9D4 File Offset: 0x000BDBD4
		public static QueueLeftNotification Deserialize(Stream stream, QueueLeftNotification instance)
		{
			return QueueLeftNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003B4A RID: 15178 RVA: 0x000BF9E0 File Offset: 0x000BDBE0
		public static QueueLeftNotification DeserializeLengthDelimited(Stream stream)
		{
			QueueLeftNotification queueLeftNotification = new QueueLeftNotification();
			QueueLeftNotification.DeserializeLengthDelimited(stream, queueLeftNotification);
			return queueLeftNotification;
		}

		// Token: 0x06003B4B RID: 15179 RVA: 0x000BF9FC File Offset: 0x000BDBFC
		public static QueueLeftNotification DeserializeLengthDelimited(Stream stream, QueueLeftNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return QueueLeftNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06003B4C RID: 15180 RVA: 0x000BFA24 File Offset: 0x000BDC24
		public static QueueLeftNotification Deserialize(Stream stream, QueueLeftNotification instance, long limit)
		{
			if (instance.Quitter == null)
			{
				instance.Quitter = new List<GameAccountHandle>();
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
						if (num != 26)
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
							instance.Quitter.Add(GameAccountHandle.DeserializeLengthDelimited(stream));
						}
					}
					else
					{
						instance.Result = ProtocolParser.ReadUInt32(stream);
					}
				}
				else if (instance.RequestId == null)
				{
					instance.RequestId = FindGameRequestId.DeserializeLengthDelimited(stream);
				}
				else
				{
					FindGameRequestId.DeserializeLengthDelimited(stream, instance.RequestId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003B4D RID: 15181 RVA: 0x000BFB0A File Offset: 0x000BDD0A
		public void Serialize(Stream stream)
		{
			QueueLeftNotification.Serialize(stream, this);
		}

		// Token: 0x06003B4E RID: 15182 RVA: 0x000BFB14 File Offset: 0x000BDD14
		public static void Serialize(Stream stream, QueueLeftNotification instance)
		{
			if (instance.HasRequestId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.RequestId.GetSerializedSize());
				FindGameRequestId.Serialize(stream, instance.RequestId);
			}
			if (instance.HasResult)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Result);
			}
			if (instance.Quitter.Count > 0)
			{
				foreach (GameAccountHandle gameAccountHandle in instance.Quitter)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, gameAccountHandle.GetSerializedSize());
					GameAccountHandle.Serialize(stream, gameAccountHandle);
				}
			}
		}

		// Token: 0x06003B4F RID: 15183 RVA: 0x000BFBD4 File Offset: 0x000BDDD4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRequestId)
			{
				num += 1U;
				uint serializedSize = this.RequestId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasResult)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Result);
			}
			if (this.Quitter.Count > 0)
			{
				foreach (GameAccountHandle gameAccountHandle in this.Quitter)
				{
					num += 1U;
					uint serializedSize2 = gameAccountHandle.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		// Token: 0x04001568 RID: 5480
		public bool HasRequestId;

		// Token: 0x04001569 RID: 5481
		private FindGameRequestId _RequestId;

		// Token: 0x0400156A RID: 5482
		public bool HasResult;

		// Token: 0x0400156B RID: 5483
		private uint _Result;

		// Token: 0x0400156C RID: 5484
		private List<GameAccountHandle> _Quitter = new List<GameAccountHandle>();
	}
}
