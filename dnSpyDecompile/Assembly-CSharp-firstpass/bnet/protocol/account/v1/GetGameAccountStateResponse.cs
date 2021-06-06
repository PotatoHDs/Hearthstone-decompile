using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000511 RID: 1297
	public class GetGameAccountStateResponse : IProtoBuf
	{
		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x06005C65 RID: 23653 RVA: 0x00118ED3 File Offset: 0x001170D3
		// (set) Token: 0x06005C66 RID: 23654 RVA: 0x00118EDB File Offset: 0x001170DB
		public GameAccountState State
		{
			get
			{
				return this._State;
			}
			set
			{
				this._State = value;
				this.HasState = (value != null);
			}
		}

		// Token: 0x06005C67 RID: 23655 RVA: 0x00118EEE File Offset: 0x001170EE
		public void SetState(GameAccountState val)
		{
			this.State = val;
		}

		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x06005C68 RID: 23656 RVA: 0x00118EF7 File Offset: 0x001170F7
		// (set) Token: 0x06005C69 RID: 23657 RVA: 0x00118EFF File Offset: 0x001170FF
		public GameAccountFieldTags Tags
		{
			get
			{
				return this._Tags;
			}
			set
			{
				this._Tags = value;
				this.HasTags = (value != null);
			}
		}

		// Token: 0x06005C6A RID: 23658 RVA: 0x00118F12 File Offset: 0x00117112
		public void SetTags(GameAccountFieldTags val)
		{
			this.Tags = val;
		}

		// Token: 0x06005C6B RID: 23659 RVA: 0x00118F1C File Offset: 0x0011711C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasState)
			{
				num ^= this.State.GetHashCode();
			}
			if (this.HasTags)
			{
				num ^= this.Tags.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005C6C RID: 23660 RVA: 0x00118F64 File Offset: 0x00117164
		public override bool Equals(object obj)
		{
			GetGameAccountStateResponse getGameAccountStateResponse = obj as GetGameAccountStateResponse;
			return getGameAccountStateResponse != null && this.HasState == getGameAccountStateResponse.HasState && (!this.HasState || this.State.Equals(getGameAccountStateResponse.State)) && this.HasTags == getGameAccountStateResponse.HasTags && (!this.HasTags || this.Tags.Equals(getGameAccountStateResponse.Tags));
		}

		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x06005C6D RID: 23661 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005C6E RID: 23662 RVA: 0x00118FD4 File Offset: 0x001171D4
		public static GetGameAccountStateResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGameAccountStateResponse>(bs, 0, -1);
		}

		// Token: 0x06005C6F RID: 23663 RVA: 0x00118FDE File Offset: 0x001171DE
		public void Deserialize(Stream stream)
		{
			GetGameAccountStateResponse.Deserialize(stream, this);
		}

		// Token: 0x06005C70 RID: 23664 RVA: 0x00118FE8 File Offset: 0x001171E8
		public static GetGameAccountStateResponse Deserialize(Stream stream, GetGameAccountStateResponse instance)
		{
			return GetGameAccountStateResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005C71 RID: 23665 RVA: 0x00118FF4 File Offset: 0x001171F4
		public static GetGameAccountStateResponse DeserializeLengthDelimited(Stream stream)
		{
			GetGameAccountStateResponse getGameAccountStateResponse = new GetGameAccountStateResponse();
			GetGameAccountStateResponse.DeserializeLengthDelimited(stream, getGameAccountStateResponse);
			return getGameAccountStateResponse;
		}

		// Token: 0x06005C72 RID: 23666 RVA: 0x00119010 File Offset: 0x00117210
		public static GetGameAccountStateResponse DeserializeLengthDelimited(Stream stream, GetGameAccountStateResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetGameAccountStateResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06005C73 RID: 23667 RVA: 0x00119038 File Offset: 0x00117238
		public static GetGameAccountStateResponse Deserialize(Stream stream, GetGameAccountStateResponse instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.Tags == null)
					{
						instance.Tags = GameAccountFieldTags.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountFieldTags.DeserializeLengthDelimited(stream, instance.Tags);
					}
				}
				else if (instance.State == null)
				{
					instance.State = GameAccountState.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountState.DeserializeLengthDelimited(stream, instance.State);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005C74 RID: 23668 RVA: 0x0011910A File Offset: 0x0011730A
		public void Serialize(Stream stream)
		{
			GetGameAccountStateResponse.Serialize(stream, this);
		}

		// Token: 0x06005C75 RID: 23669 RVA: 0x00119114 File Offset: 0x00117314
		public static void Serialize(Stream stream, GetGameAccountStateResponse instance)
		{
			if (instance.HasState)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.State.GetSerializedSize());
				GameAccountState.Serialize(stream, instance.State);
			}
			if (instance.HasTags)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Tags.GetSerializedSize());
				GameAccountFieldTags.Serialize(stream, instance.Tags);
			}
		}

		// Token: 0x06005C76 RID: 23670 RVA: 0x0011917C File Offset: 0x0011737C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasState)
			{
				num += 1U;
				uint serializedSize = this.State.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasTags)
			{
				num += 1U;
				uint serializedSize2 = this.Tags.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x04001C97 RID: 7319
		public bool HasState;

		// Token: 0x04001C98 RID: 7320
		private GameAccountState _State;

		// Token: 0x04001C99 RID: 7321
		public bool HasTags;

		// Token: 0x04001C9A RID: 7322
		private GameAccountFieldTags _Tags;
	}
}
