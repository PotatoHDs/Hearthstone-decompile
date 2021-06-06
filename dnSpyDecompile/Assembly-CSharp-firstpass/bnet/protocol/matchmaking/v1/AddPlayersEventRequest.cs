using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003CD RID: 973
	public class AddPlayersEventRequest : IProtoBuf
	{
		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06003FBD RID: 16317 RVA: 0x000CBBA6 File Offset: 0x000C9DA6
		// (set) Token: 0x06003FBE RID: 16318 RVA: 0x000CBBAE File Offset: 0x000C9DAE
		public MatchmakingEventInfo EventInfo
		{
			get
			{
				return this._EventInfo;
			}
			set
			{
				this._EventInfo = value;
				this.HasEventInfo = (value != null);
			}
		}

		// Token: 0x06003FBF RID: 16319 RVA: 0x000CBBC1 File Offset: 0x000C9DC1
		public void SetEventInfo(MatchmakingEventInfo val)
		{
			this.EventInfo = val;
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06003FC0 RID: 16320 RVA: 0x000CBBCA File Offset: 0x000C9DCA
		// (set) Token: 0x06003FC1 RID: 16321 RVA: 0x000CBBD2 File Offset: 0x000C9DD2
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

		// Token: 0x06003FC2 RID: 16322 RVA: 0x000CBBE5 File Offset: 0x000C9DE5
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06003FC3 RID: 16323 RVA: 0x000CBBEE File Offset: 0x000C9DEE
		// (set) Token: 0x06003FC4 RID: 16324 RVA: 0x000CBBF6 File Offset: 0x000C9DF6
		public bool SkipClientNotifications
		{
			get
			{
				return this._SkipClientNotifications;
			}
			set
			{
				this._SkipClientNotifications = value;
				this.HasSkipClientNotifications = true;
			}
		}

		// Token: 0x06003FC5 RID: 16325 RVA: 0x000CBC06 File Offset: 0x000C9E06
		public void SetSkipClientNotifications(bool val)
		{
			this.SkipClientNotifications = val;
		}

		// Token: 0x06003FC6 RID: 16326 RVA: 0x000CBC10 File Offset: 0x000C9E10
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasEventInfo)
			{
				num ^= this.EventInfo.GetHashCode();
			}
			if (this.HasGameHandle)
			{
				num ^= this.GameHandle.GetHashCode();
			}
			if (this.HasSkipClientNotifications)
			{
				num ^= this.SkipClientNotifications.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003FC7 RID: 16327 RVA: 0x000CBC70 File Offset: 0x000C9E70
		public override bool Equals(object obj)
		{
			AddPlayersEventRequest addPlayersEventRequest = obj as AddPlayersEventRequest;
			return addPlayersEventRequest != null && this.HasEventInfo == addPlayersEventRequest.HasEventInfo && (!this.HasEventInfo || this.EventInfo.Equals(addPlayersEventRequest.EventInfo)) && this.HasGameHandle == addPlayersEventRequest.HasGameHandle && (!this.HasGameHandle || this.GameHandle.Equals(addPlayersEventRequest.GameHandle)) && this.HasSkipClientNotifications == addPlayersEventRequest.HasSkipClientNotifications && (!this.HasSkipClientNotifications || this.SkipClientNotifications.Equals(addPlayersEventRequest.SkipClientNotifications));
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06003FC8 RID: 16328 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003FC9 RID: 16329 RVA: 0x000CBD0E File Offset: 0x000C9F0E
		public static AddPlayersEventRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AddPlayersEventRequest>(bs, 0, -1);
		}

		// Token: 0x06003FCA RID: 16330 RVA: 0x000CBD18 File Offset: 0x000C9F18
		public void Deserialize(Stream stream)
		{
			AddPlayersEventRequest.Deserialize(stream, this);
		}

		// Token: 0x06003FCB RID: 16331 RVA: 0x000CBD22 File Offset: 0x000C9F22
		public static AddPlayersEventRequest Deserialize(Stream stream, AddPlayersEventRequest instance)
		{
			return AddPlayersEventRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003FCC RID: 16332 RVA: 0x000CBD30 File Offset: 0x000C9F30
		public static AddPlayersEventRequest DeserializeLengthDelimited(Stream stream)
		{
			AddPlayersEventRequest addPlayersEventRequest = new AddPlayersEventRequest();
			AddPlayersEventRequest.DeserializeLengthDelimited(stream, addPlayersEventRequest);
			return addPlayersEventRequest;
		}

		// Token: 0x06003FCD RID: 16333 RVA: 0x000CBD4C File Offset: 0x000C9F4C
		public static AddPlayersEventRequest DeserializeLengthDelimited(Stream stream, AddPlayersEventRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AddPlayersEventRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003FCE RID: 16334 RVA: 0x000CBD74 File Offset: 0x000C9F74
		public static AddPlayersEventRequest Deserialize(Stream stream, AddPlayersEventRequest instance, long limit)
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
				else if (num != 10)
				{
					if (num != 18)
					{
						if (num != 24)
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
							instance.SkipClientNotifications = ProtocolParser.ReadBool(stream);
						}
					}
					else if (instance.GameHandle == null)
					{
						instance.GameHandle = GameHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
					}
				}
				else if (instance.EventInfo == null)
				{
					instance.EventInfo = MatchmakingEventInfo.DeserializeLengthDelimited(stream);
				}
				else
				{
					MatchmakingEventInfo.DeserializeLengthDelimited(stream, instance.EventInfo);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003FCF RID: 16335 RVA: 0x000CBE5C File Offset: 0x000CA05C
		public void Serialize(Stream stream)
		{
			AddPlayersEventRequest.Serialize(stream, this);
		}

		// Token: 0x06003FD0 RID: 16336 RVA: 0x000CBE68 File Offset: 0x000CA068
		public static void Serialize(Stream stream, AddPlayersEventRequest instance)
		{
			if (instance.HasEventInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.EventInfo.GetSerializedSize());
				MatchmakingEventInfo.Serialize(stream, instance.EventInfo);
			}
			if (instance.HasGameHandle)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				GameHandle.Serialize(stream, instance.GameHandle);
			}
			if (instance.HasSkipClientNotifications)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.SkipClientNotifications);
			}
		}

		// Token: 0x06003FD1 RID: 16337 RVA: 0x000CBEEC File Offset: 0x000CA0EC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasEventInfo)
			{
				num += 1U;
				uint serializedSize = this.EventInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasGameHandle)
			{
				num += 1U;
				uint serializedSize2 = this.GameHandle.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasSkipClientNotifications)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04001651 RID: 5713
		public bool HasEventInfo;

		// Token: 0x04001652 RID: 5714
		private MatchmakingEventInfo _EventInfo;

		// Token: 0x04001653 RID: 5715
		public bool HasGameHandle;

		// Token: 0x04001654 RID: 5716
		private GameHandle _GameHandle;

		// Token: 0x04001655 RID: 5717
		public bool HasSkipClientNotifications;

		// Token: 0x04001656 RID: 5718
		private bool _SkipClientNotifications;
	}
}
