using System;
using System.IO;
using bnet.protocol.games.v2;

namespace bnet.protocol.games.v1
{
	// Token: 0x0200039B RID: 923
	public class GameResultNotification : IProtoBuf
	{
		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06003B51 RID: 15185 RVA: 0x000BFC97 File Offset: 0x000BDE97
		// (set) Token: 0x06003B52 RID: 15186 RVA: 0x000BFC9F File Offset: 0x000BDE9F
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

		// Token: 0x06003B53 RID: 15187 RVA: 0x000BFCB2 File Offset: 0x000BDEB2
		public void SetRequestId(FindGameRequestId val)
		{
			this.RequestId = val;
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06003B54 RID: 15188 RVA: 0x000BFCBB File Offset: 0x000BDEBB
		// (set) Token: 0x06003B55 RID: 15189 RVA: 0x000BFCC3 File Offset: 0x000BDEC3
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

		// Token: 0x06003B56 RID: 15190 RVA: 0x000BFCD3 File Offset: 0x000BDED3
		public void SetResult(uint val)
		{
			this.Result = val;
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06003B57 RID: 15191 RVA: 0x000BFCDC File Offset: 0x000BDEDC
		// (set) Token: 0x06003B58 RID: 15192 RVA: 0x000BFCE4 File Offset: 0x000BDEE4
		public ConnectInfo ConnectInfo
		{
			get
			{
				return this._ConnectInfo;
			}
			set
			{
				this._ConnectInfo = value;
				this.HasConnectInfo = (value != null);
			}
		}

		// Token: 0x06003B59 RID: 15193 RVA: 0x000BFCF7 File Offset: 0x000BDEF7
		public void SetConnectInfo(ConnectInfo val)
		{
			this.ConnectInfo = val;
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06003B5A RID: 15194 RVA: 0x000BFD00 File Offset: 0x000BDF00
		// (set) Token: 0x06003B5B RID: 15195 RVA: 0x000BFD08 File Offset: 0x000BDF08
		public GameHandle GameHandle
		{
			get
			{
				return this._GameHandle;
			}
			set
			{
				this._GameHandle = value;
				this.HasGameHandle = (value != null);
			}
		}

		// Token: 0x06003B5C RID: 15196 RVA: 0x000BFD1B File Offset: 0x000BDF1B
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x06003B5D RID: 15197 RVA: 0x000BFD24 File Offset: 0x000BDF24
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
			if (this.HasConnectInfo)
			{
				num ^= this.ConnectInfo.GetHashCode();
			}
			if (this.HasGameHandle)
			{
				num ^= this.GameHandle.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003B5E RID: 15198 RVA: 0x000BFD9C File Offset: 0x000BDF9C
		public override bool Equals(object obj)
		{
			GameResultNotification gameResultNotification = obj as GameResultNotification;
			return gameResultNotification != null && this.HasRequestId == gameResultNotification.HasRequestId && (!this.HasRequestId || this.RequestId.Equals(gameResultNotification.RequestId)) && this.HasResult == gameResultNotification.HasResult && (!this.HasResult || this.Result.Equals(gameResultNotification.Result)) && this.HasConnectInfo == gameResultNotification.HasConnectInfo && (!this.HasConnectInfo || this.ConnectInfo.Equals(gameResultNotification.ConnectInfo)) && this.HasGameHandle == gameResultNotification.HasGameHandle && (!this.HasGameHandle || this.GameHandle.Equals(gameResultNotification.GameHandle));
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06003B5F RID: 15199 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003B60 RID: 15200 RVA: 0x000BFE65 File Offset: 0x000BE065
		public static GameResultNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameResultNotification>(bs, 0, -1);
		}

		// Token: 0x06003B61 RID: 15201 RVA: 0x000BFE6F File Offset: 0x000BE06F
		public void Deserialize(Stream stream)
		{
			GameResultNotification.Deserialize(stream, this);
		}

		// Token: 0x06003B62 RID: 15202 RVA: 0x000BFE79 File Offset: 0x000BE079
		public static GameResultNotification Deserialize(Stream stream, GameResultNotification instance)
		{
			return GameResultNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003B63 RID: 15203 RVA: 0x000BFE84 File Offset: 0x000BE084
		public static GameResultNotification DeserializeLengthDelimited(Stream stream)
		{
			GameResultNotification gameResultNotification = new GameResultNotification();
			GameResultNotification.DeserializeLengthDelimited(stream, gameResultNotification);
			return gameResultNotification;
		}

		// Token: 0x06003B64 RID: 15204 RVA: 0x000BFEA0 File Offset: 0x000BE0A0
		public static GameResultNotification DeserializeLengthDelimited(Stream stream, GameResultNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameResultNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06003B65 RID: 15205 RVA: 0x000BFEC8 File Offset: 0x000BE0C8
		public static GameResultNotification Deserialize(Stream stream, GameResultNotification instance, long limit)
		{
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
				else
				{
					if (num <= 16)
					{
						if (num != 10)
						{
							if (num == 16)
							{
								instance.Result = ProtocolParser.ReadUInt32(stream);
								continue;
							}
						}
						else
						{
							if (instance.RequestId == null)
							{
								instance.RequestId = FindGameRequestId.DeserializeLengthDelimited(stream);
								continue;
							}
							FindGameRequestId.DeserializeLengthDelimited(stream, instance.RequestId);
							continue;
						}
					}
					else if (num != 26)
					{
						if (num == 34)
						{
							if (instance.GameHandle == null)
							{
								instance.GameHandle = GameHandle.DeserializeLengthDelimited(stream);
								continue;
							}
							GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
							continue;
						}
					}
					else
					{
						if (instance.ConnectInfo == null)
						{
							instance.ConnectInfo = ConnectInfo.DeserializeLengthDelimited(stream);
							continue;
						}
						ConnectInfo.DeserializeLengthDelimited(stream, instance.ConnectInfo);
						continue;
					}
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

		// Token: 0x06003B66 RID: 15206 RVA: 0x000BFFF0 File Offset: 0x000BE1F0
		public void Serialize(Stream stream)
		{
			GameResultNotification.Serialize(stream, this);
		}

		// Token: 0x06003B67 RID: 15207 RVA: 0x000BFFFC File Offset: 0x000BE1FC
		public static void Serialize(Stream stream, GameResultNotification instance)
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
			if (instance.HasConnectInfo)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.ConnectInfo.GetSerializedSize());
				ConnectInfo.Serialize(stream, instance.ConnectInfo);
			}
			if (instance.HasGameHandle)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				GameHandle.Serialize(stream, instance.GameHandle);
			}
		}

		// Token: 0x06003B68 RID: 15208 RVA: 0x000C00AC File Offset: 0x000BE2AC
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
			if (this.HasConnectInfo)
			{
				num += 1U;
				uint serializedSize2 = this.ConnectInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasGameHandle)
			{
				num += 1U;
				uint serializedSize3 = this.GameHandle.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		// Token: 0x0400156D RID: 5485
		public bool HasRequestId;

		// Token: 0x0400156E RID: 5486
		private FindGameRequestId _RequestId;

		// Token: 0x0400156F RID: 5487
		public bool HasResult;

		// Token: 0x04001570 RID: 5488
		private uint _Result;

		// Token: 0x04001571 RID: 5489
		public bool HasConnectInfo;

		// Token: 0x04001572 RID: 5490
		private ConnectInfo _ConnectInfo;

		// Token: 0x04001573 RID: 5491
		public bool HasGameHandle;

		// Token: 0x04001574 RID: 5492
		private GameHandle _GameHandle;
	}
}
