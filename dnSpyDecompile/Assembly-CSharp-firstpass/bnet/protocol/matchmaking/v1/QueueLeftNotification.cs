using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003EB RID: 1003
	public class QueueLeftNotification : IProtoBuf
	{
		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x0600424A RID: 16970 RVA: 0x000D2732 File Offset: 0x000D0932
		// (set) Token: 0x0600424B RID: 16971 RVA: 0x000D273A File Offset: 0x000D093A
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

		// Token: 0x0600424C RID: 16972 RVA: 0x000D274D File Offset: 0x000D094D
		public void SetRequestId(RequestId val)
		{
			this.RequestId = val;
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x0600424D RID: 16973 RVA: 0x000D2756 File Offset: 0x000D0956
		// (set) Token: 0x0600424E RID: 16974 RVA: 0x000D275E File Offset: 0x000D095E
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

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x0600424F RID: 16975 RVA: 0x000D2756 File Offset: 0x000D0956
		public List<GameAccountHandle> GameAccountList
		{
			get
			{
				return this._GameAccount;
			}
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06004250 RID: 16976 RVA: 0x000D2767 File Offset: 0x000D0967
		public int GameAccountCount
		{
			get
			{
				return this._GameAccount.Count;
			}
		}

		// Token: 0x06004251 RID: 16977 RVA: 0x000D2774 File Offset: 0x000D0974
		public void AddGameAccount(GameAccountHandle val)
		{
			this._GameAccount.Add(val);
		}

		// Token: 0x06004252 RID: 16978 RVA: 0x000D2782 File Offset: 0x000D0982
		public void ClearGameAccount()
		{
			this._GameAccount.Clear();
		}

		// Token: 0x06004253 RID: 16979 RVA: 0x000D278F File Offset: 0x000D098F
		public void SetGameAccount(List<GameAccountHandle> val)
		{
			this.GameAccount = val;
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06004254 RID: 16980 RVA: 0x000D2798 File Offset: 0x000D0998
		// (set) Token: 0x06004255 RID: 16981 RVA: 0x000D27A0 File Offset: 0x000D09A0
		public GameAccountHandle CancelInitiator
		{
			get
			{
				return this._CancelInitiator;
			}
			set
			{
				this._CancelInitiator = value;
				this.HasCancelInitiator = (value != null);
			}
		}

		// Token: 0x06004256 RID: 16982 RVA: 0x000D27B3 File Offset: 0x000D09B3
		public void SetCancelInitiator(GameAccountHandle val)
		{
			this.CancelInitiator = val;
		}

		// Token: 0x06004257 RID: 16983 RVA: 0x000D27BC File Offset: 0x000D09BC
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
			if (this.HasCancelInitiator)
			{
				num ^= this.CancelInitiator.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004258 RID: 16984 RVA: 0x000D284C File Offset: 0x000D0A4C
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
			if (this.GameAccount.Count != queueLeftNotification.GameAccount.Count)
			{
				return false;
			}
			for (int i = 0; i < this.GameAccount.Count; i++)
			{
				if (!this.GameAccount[i].Equals(queueLeftNotification.GameAccount[i]))
				{
					return false;
				}
			}
			return this.HasCancelInitiator == queueLeftNotification.HasCancelInitiator && (!this.HasCancelInitiator || this.CancelInitiator.Equals(queueLeftNotification.CancelInitiator));
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06004259 RID: 16985 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600425A RID: 16986 RVA: 0x000D290D File Offset: 0x000D0B0D
		public static QueueLeftNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<QueueLeftNotification>(bs, 0, -1);
		}

		// Token: 0x0600425B RID: 16987 RVA: 0x000D2917 File Offset: 0x000D0B17
		public void Deserialize(Stream stream)
		{
			QueueLeftNotification.Deserialize(stream, this);
		}

		// Token: 0x0600425C RID: 16988 RVA: 0x000D2921 File Offset: 0x000D0B21
		public static QueueLeftNotification Deserialize(Stream stream, QueueLeftNotification instance)
		{
			return QueueLeftNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600425D RID: 16989 RVA: 0x000D292C File Offset: 0x000D0B2C
		public static QueueLeftNotification DeserializeLengthDelimited(Stream stream)
		{
			QueueLeftNotification queueLeftNotification = new QueueLeftNotification();
			QueueLeftNotification.DeserializeLengthDelimited(stream, queueLeftNotification);
			return queueLeftNotification;
		}

		// Token: 0x0600425E RID: 16990 RVA: 0x000D2948 File Offset: 0x000D0B48
		public static QueueLeftNotification DeserializeLengthDelimited(Stream stream, QueueLeftNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return QueueLeftNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x0600425F RID: 16991 RVA: 0x000D2970 File Offset: 0x000D0B70
		public static QueueLeftNotification Deserialize(Stream stream, QueueLeftNotification instance, long limit)
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
						if (num != 26)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else if (instance.CancelInitiator == null)
						{
							instance.CancelInitiator = GameAccountHandle.DeserializeLengthDelimited(stream);
						}
						else
						{
							GameAccountHandle.DeserializeLengthDelimited(stream, instance.CancelInitiator);
						}
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

		// Token: 0x06004260 RID: 16992 RVA: 0x000D2A70 File Offset: 0x000D0C70
		public void Serialize(Stream stream)
		{
			QueueLeftNotification.Serialize(stream, this);
		}

		// Token: 0x06004261 RID: 16993 RVA: 0x000D2A7C File Offset: 0x000D0C7C
		public static void Serialize(Stream stream, QueueLeftNotification instance)
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
			if (instance.HasCancelInitiator)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.CancelInitiator.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.CancelInitiator);
			}
		}

		// Token: 0x06004262 RID: 16994 RVA: 0x000D2B4C File Offset: 0x000D0D4C
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
			if (this.HasCancelInitiator)
			{
				num += 1U;
				uint serializedSize3 = this.CancelInitiator.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		// Token: 0x040016CD RID: 5837
		public bool HasRequestId;

		// Token: 0x040016CE RID: 5838
		private RequestId _RequestId;

		// Token: 0x040016CF RID: 5839
		private List<GameAccountHandle> _GameAccount = new List<GameAccountHandle>();

		// Token: 0x040016D0 RID: 5840
		public bool HasCancelInitiator;

		// Token: 0x040016D1 RID: 5841
		private GameAccountHandle _CancelInitiator;
	}
}
