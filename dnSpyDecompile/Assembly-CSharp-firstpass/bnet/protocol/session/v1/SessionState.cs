using System;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.session.v1
{
	// Token: 0x0200031A RID: 794
	public class SessionState : IProtoBuf
	{
		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x0600305A RID: 12378 RVA: 0x000A3168 File Offset: 0x000A1368
		// (set) Token: 0x0600305B RID: 12379 RVA: 0x000A3170 File Offset: 0x000A1370
		public GameAccountHandle Handle
		{
			get
			{
				return this._Handle;
			}
			set
			{
				this._Handle = value;
				this.HasHandle = (value != null);
			}
		}

		// Token: 0x0600305C RID: 12380 RVA: 0x000A3183 File Offset: 0x000A1383
		public void SetHandle(GameAccountHandle val)
		{
			this.Handle = val;
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x0600305D RID: 12381 RVA: 0x000A318C File Offset: 0x000A138C
		// (set) Token: 0x0600305E RID: 12382 RVA: 0x000A3194 File Offset: 0x000A1394
		public string ClientAddress
		{
			get
			{
				return this._ClientAddress;
			}
			set
			{
				this._ClientAddress = value;
				this.HasClientAddress = (value != null);
			}
		}

		// Token: 0x0600305F RID: 12383 RVA: 0x000A31A7 File Offset: 0x000A13A7
		public void SetClientAddress(string val)
		{
			this.ClientAddress = val;
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06003060 RID: 12384 RVA: 0x000A31B0 File Offset: 0x000A13B0
		// (set) Token: 0x06003061 RID: 12385 RVA: 0x000A31B8 File Offset: 0x000A13B8
		public ulong LastTickTime
		{
			get
			{
				return this._LastTickTime;
			}
			set
			{
				this._LastTickTime = value;
				this.HasLastTickTime = true;
			}
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x000A31C8 File Offset: 0x000A13C8
		public void SetLastTickTime(ulong val)
		{
			this.LastTickTime = val;
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06003063 RID: 12387 RVA: 0x000A31D1 File Offset: 0x000A13D1
		// (set) Token: 0x06003064 RID: 12388 RVA: 0x000A31D9 File Offset: 0x000A13D9
		public ulong CreateTime
		{
			get
			{
				return this._CreateTime;
			}
			set
			{
				this._CreateTime = value;
				this.HasCreateTime = true;
			}
		}

		// Token: 0x06003065 RID: 12389 RVA: 0x000A31E9 File Offset: 0x000A13E9
		public void SetCreateTime(ulong val)
		{
			this.CreateTime = val;
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06003066 RID: 12390 RVA: 0x000A31F2 File Offset: 0x000A13F2
		// (set) Token: 0x06003067 RID: 12391 RVA: 0x000A31FA File Offset: 0x000A13FA
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

		// Token: 0x06003068 RID: 12392 RVA: 0x000A320A File Offset: 0x000A140A
		public void SetParentalControlsActive(bool val)
		{
			this.ParentalControlsActive = val;
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06003069 RID: 12393 RVA: 0x000A3213 File Offset: 0x000A1413
		// (set) Token: 0x0600306A RID: 12394 RVA: 0x000A321B File Offset: 0x000A141B
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

		// Token: 0x0600306B RID: 12395 RVA: 0x000A322E File Offset: 0x000A142E
		public void SetLocation(GameSessionLocation val)
		{
			this.Location = val;
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x0600306C RID: 12396 RVA: 0x000A3237 File Offset: 0x000A1437
		// (set) Token: 0x0600306D RID: 12397 RVA: 0x000A323F File Offset: 0x000A143F
		public bool UsingIgrAddress
		{
			get
			{
				return this._UsingIgrAddress;
			}
			set
			{
				this._UsingIgrAddress = value;
				this.HasUsingIgrAddress = true;
			}
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x000A324F File Offset: 0x000A144F
		public void SetUsingIgrAddress(bool val)
		{
			this.UsingIgrAddress = val;
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x0600306F RID: 12399 RVA: 0x000A3258 File Offset: 0x000A1458
		// (set) Token: 0x06003070 RID: 12400 RVA: 0x000A3260 File Offset: 0x000A1460
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

		// Token: 0x06003071 RID: 12401 RVA: 0x000A3270 File Offset: 0x000A1470
		public void SetHasBenefactor(bool val)
		{
			this.HasBenefactor = val;
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06003072 RID: 12402 RVA: 0x000A3279 File Offset: 0x000A1479
		// (set) Token: 0x06003073 RID: 12403 RVA: 0x000A3281 File Offset: 0x000A1481
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

		// Token: 0x06003074 RID: 12404 RVA: 0x000A3294 File Offset: 0x000A1494
		public void SetIgrId(IgrId val)
		{
			this.IgrId = val;
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x000A32A0 File Offset: 0x000A14A0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasHandle)
			{
				num ^= this.Handle.GetHashCode();
			}
			if (this.HasClientAddress)
			{
				num ^= this.ClientAddress.GetHashCode();
			}
			if (this.HasLastTickTime)
			{
				num ^= this.LastTickTime.GetHashCode();
			}
			if (this.HasCreateTime)
			{
				num ^= this.CreateTime.GetHashCode();
			}
			if (this.HasParentalControlsActive)
			{
				num ^= this.ParentalControlsActive.GetHashCode();
			}
			if (this.HasLocation)
			{
				num ^= this.Location.GetHashCode();
			}
			if (this.HasUsingIgrAddress)
			{
				num ^= this.UsingIgrAddress.GetHashCode();
			}
			if (this.HasHasBenefactor)
			{
				num ^= this.HasBenefactor.GetHashCode();
			}
			if (this.HasIgrId)
			{
				num ^= this.IgrId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x000A3390 File Offset: 0x000A1590
		public override bool Equals(object obj)
		{
			SessionState sessionState = obj as SessionState;
			return sessionState != null && this.HasHandle == sessionState.HasHandle && (!this.HasHandle || this.Handle.Equals(sessionState.Handle)) && this.HasClientAddress == sessionState.HasClientAddress && (!this.HasClientAddress || this.ClientAddress.Equals(sessionState.ClientAddress)) && this.HasLastTickTime == sessionState.HasLastTickTime && (!this.HasLastTickTime || this.LastTickTime.Equals(sessionState.LastTickTime)) && this.HasCreateTime == sessionState.HasCreateTime && (!this.HasCreateTime || this.CreateTime.Equals(sessionState.CreateTime)) && this.HasParentalControlsActive == sessionState.HasParentalControlsActive && (!this.HasParentalControlsActive || this.ParentalControlsActive.Equals(sessionState.ParentalControlsActive)) && this.HasLocation == sessionState.HasLocation && (!this.HasLocation || this.Location.Equals(sessionState.Location)) && this.HasUsingIgrAddress == sessionState.HasUsingIgrAddress && (!this.HasUsingIgrAddress || this.UsingIgrAddress.Equals(sessionState.UsingIgrAddress)) && this.HasHasBenefactor == sessionState.HasHasBenefactor && (!this.HasHasBenefactor || this.HasBenefactor.Equals(sessionState.HasBenefactor)) && this.HasIgrId == sessionState.HasIgrId && (!this.HasIgrId || this.IgrId.Equals(sessionState.IgrId));
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06003077 RID: 12407 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x000A353C File Offset: 0x000A173C
		public static SessionState ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SessionState>(bs, 0, -1);
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x000A3546 File Offset: 0x000A1746
		public void Deserialize(Stream stream)
		{
			SessionState.Deserialize(stream, this);
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x000A3550 File Offset: 0x000A1750
		public static SessionState Deserialize(Stream stream, SessionState instance)
		{
			return SessionState.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600307B RID: 12411 RVA: 0x000A355C File Offset: 0x000A175C
		public static SessionState DeserializeLengthDelimited(Stream stream)
		{
			SessionState sessionState = new SessionState();
			SessionState.DeserializeLengthDelimited(stream, sessionState);
			return sessionState;
		}

		// Token: 0x0600307C RID: 12412 RVA: 0x000A3578 File Offset: 0x000A1778
		public static SessionState DeserializeLengthDelimited(Stream stream, SessionState instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SessionState.Deserialize(stream, instance, num);
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x000A35A0 File Offset: 0x000A17A0
		public static SessionState Deserialize(Stream stream, SessionState instance, long limit)
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
					if (num <= 32)
					{
						if (num <= 18)
						{
							if (num != 10)
							{
								if (num == 18)
								{
									instance.ClientAddress = ProtocolParser.ReadString(stream);
									continue;
								}
							}
							else
							{
								if (instance.Handle == null)
								{
									instance.Handle = GameAccountHandle.DeserializeLengthDelimited(stream);
									continue;
								}
								GameAccountHandle.DeserializeLengthDelimited(stream, instance.Handle);
								continue;
							}
						}
						else
						{
							if (num == 24)
							{
								instance.LastTickTime = ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 32)
							{
								instance.CreateTime = ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
					}
					else if (num <= 50)
					{
						if (num == 40)
						{
							instance.ParentalControlsActive = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 50)
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
					else
					{
						if (num == 56)
						{
							instance.UsingIgrAddress = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 64)
						{
							instance.HasBenefactor = ProtocolParser.ReadBool(stream);
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

		// Token: 0x0600307E RID: 12414 RVA: 0x000A3762 File Offset: 0x000A1962
		public void Serialize(Stream stream)
		{
			SessionState.Serialize(stream, this);
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x000A376C File Offset: 0x000A196C
		public static void Serialize(Stream stream, SessionState instance)
		{
			if (instance.HasHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Handle.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.Handle);
			}
			if (instance.HasClientAddress)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientAddress));
			}
			if (instance.HasLastTickTime)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.LastTickTime);
			}
			if (instance.HasCreateTime)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, instance.CreateTime);
			}
			if (instance.HasParentalControlsActive)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.ParentalControlsActive);
			}
			if (instance.HasLocation)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.Location.GetSerializedSize());
				GameSessionLocation.Serialize(stream, instance.Location);
			}
			if (instance.HasUsingIgrAddress)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.UsingIgrAddress);
			}
			if (instance.HasHasBenefactor)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteBool(stream, instance.HasBenefactor);
			}
			if (instance.HasIgrId)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteUInt32(stream, instance.IgrId.GetSerializedSize());
				IgrId.Serialize(stream, instance.IgrId);
			}
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x000A38B4 File Offset: 0x000A1AB4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasHandle)
			{
				num += 1U;
				uint serializedSize = this.Handle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasClientAddress)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ClientAddress);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasLastTickTime)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.LastTickTime);
			}
			if (this.HasCreateTime)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.CreateTime);
			}
			if (this.HasParentalControlsActive)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasLocation)
			{
				num += 1U;
				uint serializedSize2 = this.Location.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasUsingIgrAddress)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasHasBenefactor)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasIgrId)
			{
				num += 1U;
				uint serializedSize3 = this.IgrId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		// Token: 0x0400133F RID: 4927
		public bool HasHandle;

		// Token: 0x04001340 RID: 4928
		private GameAccountHandle _Handle;

		// Token: 0x04001341 RID: 4929
		public bool HasClientAddress;

		// Token: 0x04001342 RID: 4930
		private string _ClientAddress;

		// Token: 0x04001343 RID: 4931
		public bool HasLastTickTime;

		// Token: 0x04001344 RID: 4932
		private ulong _LastTickTime;

		// Token: 0x04001345 RID: 4933
		public bool HasCreateTime;

		// Token: 0x04001346 RID: 4934
		private ulong _CreateTime;

		// Token: 0x04001347 RID: 4935
		public bool HasParentalControlsActive;

		// Token: 0x04001348 RID: 4936
		private bool _ParentalControlsActive;

		// Token: 0x04001349 RID: 4937
		public bool HasLocation;

		// Token: 0x0400134A RID: 4938
		private GameSessionLocation _Location;

		// Token: 0x0400134B RID: 4939
		public bool HasUsingIgrAddress;

		// Token: 0x0400134C RID: 4940
		private bool _UsingIgrAddress;

		// Token: 0x0400134D RID: 4941
		public bool HasHasBenefactor;

		// Token: 0x0400134E RID: 4942
		private bool _HasBenefactor;

		// Token: 0x0400134F RID: 4943
		public bool HasIgrId;

		// Token: 0x04001350 RID: 4944
		private IgrId _IgrId;
	}
}
