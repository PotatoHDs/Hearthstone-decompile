using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000535 RID: 1333
	public class GameSessionInfo : IProtoBuf
	{
		// Token: 0x1700122E RID: 4654
		// (get) Token: 0x06005FFD RID: 24573 RVA: 0x00122B61 File Offset: 0x00120D61
		// (set) Token: 0x06005FFE RID: 24574 RVA: 0x00122B69 File Offset: 0x00120D69
		public uint StartTime
		{
			get
			{
				return this._StartTime;
			}
			set
			{
				this._StartTime = value;
				this.HasStartTime = true;
			}
		}

		// Token: 0x06005FFF RID: 24575 RVA: 0x00122B79 File Offset: 0x00120D79
		public void SetStartTime(uint val)
		{
			this.StartTime = val;
		}

		// Token: 0x1700122F RID: 4655
		// (get) Token: 0x06006000 RID: 24576 RVA: 0x00122B82 File Offset: 0x00120D82
		// (set) Token: 0x06006001 RID: 24577 RVA: 0x00122B8A File Offset: 0x00120D8A
		public GameSessionLocation Location
		{
			get
			{
				return this._Location;
			}
			set
			{
				this._Location = value;
				this.HasLocation = (value != null);
			}
		}

		// Token: 0x06006002 RID: 24578 RVA: 0x00122B9D File Offset: 0x00120D9D
		public void SetLocation(GameSessionLocation val)
		{
			this.Location = val;
		}

		// Token: 0x17001230 RID: 4656
		// (get) Token: 0x06006003 RID: 24579 RVA: 0x00122BA6 File Offset: 0x00120DA6
		// (set) Token: 0x06006004 RID: 24580 RVA: 0x00122BAE File Offset: 0x00120DAE
		public bool HasBenefactor
		{
			get
			{
				return this._HasBenefactor;
			}
			set
			{
				this._HasBenefactor = value;
				this.HasHasBenefactor = true;
			}
		}

		// Token: 0x06006005 RID: 24581 RVA: 0x00122BBE File Offset: 0x00120DBE
		public void SetHasBenefactor(bool val)
		{
			this.HasBenefactor = val;
		}

		// Token: 0x17001231 RID: 4657
		// (get) Token: 0x06006006 RID: 24582 RVA: 0x00122BC7 File Offset: 0x00120DC7
		// (set) Token: 0x06006007 RID: 24583 RVA: 0x00122BCF File Offset: 0x00120DCF
		public bool IsUsingIgr
		{
			get
			{
				return this._IsUsingIgr;
			}
			set
			{
				this._IsUsingIgr = value;
				this.HasIsUsingIgr = true;
			}
		}

		// Token: 0x06006008 RID: 24584 RVA: 0x00122BDF File Offset: 0x00120DDF
		public void SetIsUsingIgr(bool val)
		{
			this.IsUsingIgr = val;
		}

		// Token: 0x17001232 RID: 4658
		// (get) Token: 0x06006009 RID: 24585 RVA: 0x00122BE8 File Offset: 0x00120DE8
		// (set) Token: 0x0600600A RID: 24586 RVA: 0x00122BF0 File Offset: 0x00120DF0
		public bool ParentalControlsActive
		{
			get
			{
				return this._ParentalControlsActive;
			}
			set
			{
				this._ParentalControlsActive = value;
				this.HasParentalControlsActive = true;
			}
		}

		// Token: 0x0600600B RID: 24587 RVA: 0x00122C00 File Offset: 0x00120E00
		public void SetParentalControlsActive(bool val)
		{
			this.ParentalControlsActive = val;
		}

		// Token: 0x17001233 RID: 4659
		// (get) Token: 0x0600600C RID: 24588 RVA: 0x00122C09 File Offset: 0x00120E09
		// (set) Token: 0x0600600D RID: 24589 RVA: 0x00122C11 File Offset: 0x00120E11
		public ulong StartTimeSec
		{
			get
			{
				return this._StartTimeSec;
			}
			set
			{
				this._StartTimeSec = value;
				this.HasStartTimeSec = true;
			}
		}

		// Token: 0x0600600E RID: 24590 RVA: 0x00122C21 File Offset: 0x00120E21
		public void SetStartTimeSec(ulong val)
		{
			this.StartTimeSec = val;
		}

		// Token: 0x17001234 RID: 4660
		// (get) Token: 0x0600600F RID: 24591 RVA: 0x00122C2A File Offset: 0x00120E2A
		// (set) Token: 0x06006010 RID: 24592 RVA: 0x00122C32 File Offset: 0x00120E32
		public IgrId IgrId
		{
			get
			{
				return this._IgrId;
			}
			set
			{
				this._IgrId = value;
				this.HasIgrId = (value != null);
			}
		}

		// Token: 0x06006011 RID: 24593 RVA: 0x00122C45 File Offset: 0x00120E45
		public void SetIgrId(IgrId val)
		{
			this.IgrId = val;
		}

		// Token: 0x06006012 RID: 24594 RVA: 0x00122C50 File Offset: 0x00120E50
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasStartTime)
			{
				num ^= this.StartTime.GetHashCode();
			}
			if (this.HasLocation)
			{
				num ^= this.Location.GetHashCode();
			}
			if (this.HasHasBenefactor)
			{
				num ^= this.HasBenefactor.GetHashCode();
			}
			if (this.HasIsUsingIgr)
			{
				num ^= this.IsUsingIgr.GetHashCode();
			}
			if (this.HasParentalControlsActive)
			{
				num ^= this.ParentalControlsActive.GetHashCode();
			}
			if (this.HasStartTimeSec)
			{
				num ^= this.StartTimeSec.GetHashCode();
			}
			if (this.HasIgrId)
			{
				num ^= this.IgrId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06006013 RID: 24595 RVA: 0x00122D14 File Offset: 0x00120F14
		public override bool Equals(object obj)
		{
			GameSessionInfo gameSessionInfo = obj as GameSessionInfo;
			return gameSessionInfo != null && this.HasStartTime == gameSessionInfo.HasStartTime && (!this.HasStartTime || this.StartTime.Equals(gameSessionInfo.StartTime)) && this.HasLocation == gameSessionInfo.HasLocation && (!this.HasLocation || this.Location.Equals(gameSessionInfo.Location)) && this.HasHasBenefactor == gameSessionInfo.HasHasBenefactor && (!this.HasHasBenefactor || this.HasBenefactor.Equals(gameSessionInfo.HasBenefactor)) && this.HasIsUsingIgr == gameSessionInfo.HasIsUsingIgr && (!this.HasIsUsingIgr || this.IsUsingIgr.Equals(gameSessionInfo.IsUsingIgr)) && this.HasParentalControlsActive == gameSessionInfo.HasParentalControlsActive && (!this.HasParentalControlsActive || this.ParentalControlsActive.Equals(gameSessionInfo.ParentalControlsActive)) && this.HasStartTimeSec == gameSessionInfo.HasStartTimeSec && (!this.HasStartTimeSec || this.StartTimeSec.Equals(gameSessionInfo.StartTimeSec)) && this.HasIgrId == gameSessionInfo.HasIgrId && (!this.HasIgrId || this.IgrId.Equals(gameSessionInfo.IgrId));
		}

		// Token: 0x17001235 RID: 4661
		// (get) Token: 0x06006014 RID: 24596 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006015 RID: 24597 RVA: 0x00122E6A File Offset: 0x0012106A
		public static GameSessionInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameSessionInfo>(bs, 0, -1);
		}

		// Token: 0x06006016 RID: 24598 RVA: 0x00122E74 File Offset: 0x00121074
		public void Deserialize(Stream stream)
		{
			GameSessionInfo.Deserialize(stream, this);
		}

		// Token: 0x06006017 RID: 24599 RVA: 0x00122E7E File Offset: 0x0012107E
		public static GameSessionInfo Deserialize(Stream stream, GameSessionInfo instance)
		{
			return GameSessionInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06006018 RID: 24600 RVA: 0x00122E8C File Offset: 0x0012108C
		public static GameSessionInfo DeserializeLengthDelimited(Stream stream)
		{
			GameSessionInfo gameSessionInfo = new GameSessionInfo();
			GameSessionInfo.DeserializeLengthDelimited(stream, gameSessionInfo);
			return gameSessionInfo;
		}

		// Token: 0x06006019 RID: 24601 RVA: 0x00122EA8 File Offset: 0x001210A8
		public static GameSessionInfo DeserializeLengthDelimited(Stream stream, GameSessionInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameSessionInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x0600601A RID: 24602 RVA: 0x00122ED0 File Offset: 0x001210D0
		public static GameSessionInfo Deserialize(Stream stream, GameSessionInfo instance, long limit)
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
					if (num <= 40)
					{
						if (num == 24)
						{
							instance.StartTime = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num != 34)
						{
							if (num == 40)
							{
								instance.HasBenefactor = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
						else
						{
							if (instance.Location == null)
							{
								instance.Location = GameSessionLocation.DeserializeLengthDelimited(stream);
								continue;
							}
							GameSessionLocation.DeserializeLengthDelimited(stream, instance.Location);
							continue;
						}
					}
					else if (num <= 56)
					{
						if (num == 48)
						{
							instance.IsUsingIgr = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 56)
						{
							instance.ParentalControlsActive = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					else
					{
						if (num == 64)
						{
							instance.StartTimeSec = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 74)
						{
							if (instance.IgrId == null)
							{
								instance.IgrId = IgrId.DeserializeLengthDelimited(stream);
								continue;
							}
							IgrId.DeserializeLengthDelimited(stream, instance.IgrId);
							continue;
						}
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

		// Token: 0x0600601B RID: 24603 RVA: 0x00123030 File Offset: 0x00121230
		public void Serialize(Stream stream)
		{
			GameSessionInfo.Serialize(stream, this);
		}

		// Token: 0x0600601C RID: 24604 RVA: 0x0012303C File Offset: 0x0012123C
		public static void Serialize(Stream stream, GameSessionInfo instance)
		{
			if (instance.HasStartTime)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.StartTime);
			}
			if (instance.HasLocation)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Location.GetSerializedSize());
				GameSessionLocation.Serialize(stream, instance.Location);
			}
			if (instance.HasHasBenefactor)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.HasBenefactor);
			}
			if (instance.HasIsUsingIgr)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.IsUsingIgr);
			}
			if (instance.HasParentalControlsActive)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.ParentalControlsActive);
			}
			if (instance.HasStartTimeSec)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, instance.StartTimeSec);
			}
			if (instance.HasIgrId)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteUInt32(stream, instance.IgrId.GetSerializedSize());
				IgrId.Serialize(stream, instance.IgrId);
			}
		}

		// Token: 0x0600601D RID: 24605 RVA: 0x00123130 File Offset: 0x00121330
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasStartTime)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.StartTime);
			}
			if (this.HasLocation)
			{
				num += 1U;
				uint serializedSize = this.Location.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasHasBenefactor)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasIsUsingIgr)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasParentalControlsActive)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasStartTimeSec)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.StartTimeSec);
			}
			if (this.HasIgrId)
			{
				num += 1U;
				uint serializedSize2 = this.IgrId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x04001D97 RID: 7575
		public bool HasStartTime;

		// Token: 0x04001D98 RID: 7576
		private uint _StartTime;

		// Token: 0x04001D99 RID: 7577
		public bool HasLocation;

		// Token: 0x04001D9A RID: 7578
		private GameSessionLocation _Location;

		// Token: 0x04001D9B RID: 7579
		public bool HasHasBenefactor;

		// Token: 0x04001D9C RID: 7580
		private bool _HasBenefactor;

		// Token: 0x04001D9D RID: 7581
		public bool HasIsUsingIgr;

		// Token: 0x04001D9E RID: 7582
		private bool _IsUsingIgr;

		// Token: 0x04001D9F RID: 7583
		public bool HasParentalControlsActive;

		// Token: 0x04001DA0 RID: 7584
		private bool _ParentalControlsActive;

		// Token: 0x04001DA1 RID: 7585
		public bool HasStartTimeSec;

		// Token: 0x04001DA2 RID: 7586
		private ulong _StartTimeSec;

		// Token: 0x04001DA3 RID: 7587
		public bool HasIgrId;

		// Token: 0x04001DA4 RID: 7588
		private IgrId _IgrId;
	}
}
