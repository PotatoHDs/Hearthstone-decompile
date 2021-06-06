using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.presence.v1
{
	// Token: 0x02000334 RID: 820
	public class StateChangedNotification : IProtoBuf
	{
		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06003274 RID: 12916 RVA: 0x000A8D7F File Offset: 0x000A6F7F
		// (set) Token: 0x06003275 RID: 12917 RVA: 0x000A8D87 File Offset: 0x000A6F87
		public AccountId SubscriberId
		{
			get
			{
				return this._SubscriberId;
			}
			set
			{
				this._SubscriberId = value;
				this.HasSubscriberId = (value != null);
			}
		}

		// Token: 0x06003276 RID: 12918 RVA: 0x000A8D9A File Offset: 0x000A6F9A
		public void SetSubscriberId(AccountId val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06003277 RID: 12919 RVA: 0x000A8DA3 File Offset: 0x000A6FA3
		// (set) Token: 0x06003278 RID: 12920 RVA: 0x000A8DAB File Offset: 0x000A6FAB
		public List<PresenceState> State
		{
			get
			{
				return this._State;
			}
			set
			{
				this._State = value;
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06003279 RID: 12921 RVA: 0x000A8DA3 File Offset: 0x000A6FA3
		public List<PresenceState> StateList
		{
			get
			{
				return this._State;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x0600327A RID: 12922 RVA: 0x000A8DB4 File Offset: 0x000A6FB4
		public int StateCount
		{
			get
			{
				return this._State.Count;
			}
		}

		// Token: 0x0600327B RID: 12923 RVA: 0x000A8DC1 File Offset: 0x000A6FC1
		public void AddState(PresenceState val)
		{
			this._State.Add(val);
		}

		// Token: 0x0600327C RID: 12924 RVA: 0x000A8DCF File Offset: 0x000A6FCF
		public void ClearState()
		{
			this._State.Clear();
		}

		// Token: 0x0600327D RID: 12925 RVA: 0x000A8DDC File Offset: 0x000A6FDC
		public void SetState(List<PresenceState> val)
		{
			this.State = val;
		}

		// Token: 0x0600327E RID: 12926 RVA: 0x000A8DE8 File Offset: 0x000A6FE8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasSubscriberId)
			{
				num ^= this.SubscriberId.GetHashCode();
			}
			foreach (PresenceState presenceState in this.State)
			{
				num ^= presenceState.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600327F RID: 12927 RVA: 0x000A8E60 File Offset: 0x000A7060
		public override bool Equals(object obj)
		{
			StateChangedNotification stateChangedNotification = obj as StateChangedNotification;
			if (stateChangedNotification == null)
			{
				return false;
			}
			if (this.HasSubscriberId != stateChangedNotification.HasSubscriberId || (this.HasSubscriberId && !this.SubscriberId.Equals(stateChangedNotification.SubscriberId)))
			{
				return false;
			}
			if (this.State.Count != stateChangedNotification.State.Count)
			{
				return false;
			}
			for (int i = 0; i < this.State.Count; i++)
			{
				if (!this.State[i].Equals(stateChangedNotification.State[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06003280 RID: 12928 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003281 RID: 12929 RVA: 0x000A8EF6 File Offset: 0x000A70F6
		public static StateChangedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<StateChangedNotification>(bs, 0, -1);
		}

		// Token: 0x06003282 RID: 12930 RVA: 0x000A8F00 File Offset: 0x000A7100
		public void Deserialize(Stream stream)
		{
			StateChangedNotification.Deserialize(stream, this);
		}

		// Token: 0x06003283 RID: 12931 RVA: 0x000A8F0A File Offset: 0x000A710A
		public static StateChangedNotification Deserialize(Stream stream, StateChangedNotification instance)
		{
			return StateChangedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x000A8F18 File Offset: 0x000A7118
		public static StateChangedNotification DeserializeLengthDelimited(Stream stream)
		{
			StateChangedNotification stateChangedNotification = new StateChangedNotification();
			StateChangedNotification.DeserializeLengthDelimited(stream, stateChangedNotification);
			return stateChangedNotification;
		}

		// Token: 0x06003285 RID: 12933 RVA: 0x000A8F34 File Offset: 0x000A7134
		public static StateChangedNotification DeserializeLengthDelimited(Stream stream, StateChangedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return StateChangedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06003286 RID: 12934 RVA: 0x000A8F5C File Offset: 0x000A715C
		public static StateChangedNotification Deserialize(Stream stream, StateChangedNotification instance, long limit)
		{
			if (instance.State == null)
			{
				instance.State = new List<PresenceState>();
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
						instance.State.Add(PresenceState.DeserializeLengthDelimited(stream));
					}
				}
				else if (instance.SubscriberId == null)
				{
					instance.SubscriberId = AccountId.DeserializeLengthDelimited(stream);
				}
				else
				{
					AccountId.DeserializeLengthDelimited(stream, instance.SubscriberId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x000A9026 File Offset: 0x000A7226
		public void Serialize(Stream stream)
		{
			StateChangedNotification.Serialize(stream, this);
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x000A9030 File Offset: 0x000A7230
		public static void Serialize(Stream stream, StateChangedNotification instance)
		{
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				AccountId.Serialize(stream, instance.SubscriberId);
			}
			if (instance.State.Count > 0)
			{
				foreach (PresenceState presenceState in instance.State)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, presenceState.GetSerializedSize());
					PresenceState.Serialize(stream, presenceState);
				}
			}
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x000A90D4 File Offset: 0x000A72D4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasSubscriberId)
			{
				num += 1U;
				uint serializedSize = this.SubscriberId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.State.Count > 0)
			{
				foreach (PresenceState presenceState in this.State)
				{
					num += 1U;
					uint serializedSize2 = presenceState.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		// Token: 0x040013D9 RID: 5081
		public bool HasSubscriberId;

		// Token: 0x040013DA RID: 5082
		private AccountId _SubscriberId;

		// Token: 0x040013DB RID: 5083
		private List<PresenceState> _State = new List<PresenceState>();
	}
}
