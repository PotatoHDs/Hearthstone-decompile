using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.presence.v1
{
	// Token: 0x02000333 RID: 819
	public class SubscribeNotification : IProtoBuf
	{
		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x0600325D RID: 12893 RVA: 0x000A8983 File Offset: 0x000A6B83
		// (set) Token: 0x0600325E RID: 12894 RVA: 0x000A898B File Offset: 0x000A6B8B
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

		// Token: 0x0600325F RID: 12895 RVA: 0x000A899E File Offset: 0x000A6B9E
		public void SetSubscriberId(AccountId val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06003260 RID: 12896 RVA: 0x000A89A7 File Offset: 0x000A6BA7
		// (set) Token: 0x06003261 RID: 12897 RVA: 0x000A89AF File Offset: 0x000A6BAF
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

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06003262 RID: 12898 RVA: 0x000A89A7 File Offset: 0x000A6BA7
		public List<PresenceState> StateList
		{
			get
			{
				return this._State;
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06003263 RID: 12899 RVA: 0x000A89B8 File Offset: 0x000A6BB8
		public int StateCount
		{
			get
			{
				return this._State.Count;
			}
		}

		// Token: 0x06003264 RID: 12900 RVA: 0x000A89C5 File Offset: 0x000A6BC5
		public void AddState(PresenceState val)
		{
			this._State.Add(val);
		}

		// Token: 0x06003265 RID: 12901 RVA: 0x000A89D3 File Offset: 0x000A6BD3
		public void ClearState()
		{
			this._State.Clear();
		}

		// Token: 0x06003266 RID: 12902 RVA: 0x000A89E0 File Offset: 0x000A6BE0
		public void SetState(List<PresenceState> val)
		{
			this.State = val;
		}

		// Token: 0x06003267 RID: 12903 RVA: 0x000A89EC File Offset: 0x000A6BEC
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

		// Token: 0x06003268 RID: 12904 RVA: 0x000A8A64 File Offset: 0x000A6C64
		public override bool Equals(object obj)
		{
			SubscribeNotification subscribeNotification = obj as SubscribeNotification;
			if (subscribeNotification == null)
			{
				return false;
			}
			if (this.HasSubscriberId != subscribeNotification.HasSubscriberId || (this.HasSubscriberId && !this.SubscriberId.Equals(subscribeNotification.SubscriberId)))
			{
				return false;
			}
			if (this.State.Count != subscribeNotification.State.Count)
			{
				return false;
			}
			for (int i = 0; i < this.State.Count; i++)
			{
				if (!this.State[i].Equals(subscribeNotification.State[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06003269 RID: 12905 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600326A RID: 12906 RVA: 0x000A8AFA File Offset: 0x000A6CFA
		public static SubscribeNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeNotification>(bs, 0, -1);
		}

		// Token: 0x0600326B RID: 12907 RVA: 0x000A8B04 File Offset: 0x000A6D04
		public void Deserialize(Stream stream)
		{
			SubscribeNotification.Deserialize(stream, this);
		}

		// Token: 0x0600326C RID: 12908 RVA: 0x000A8B0E File Offset: 0x000A6D0E
		public static SubscribeNotification Deserialize(Stream stream, SubscribeNotification instance)
		{
			return SubscribeNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600326D RID: 12909 RVA: 0x000A8B1C File Offset: 0x000A6D1C
		public static SubscribeNotification DeserializeLengthDelimited(Stream stream)
		{
			SubscribeNotification subscribeNotification = new SubscribeNotification();
			SubscribeNotification.DeserializeLengthDelimited(stream, subscribeNotification);
			return subscribeNotification;
		}

		// Token: 0x0600326E RID: 12910 RVA: 0x000A8B38 File Offset: 0x000A6D38
		public static SubscribeNotification DeserializeLengthDelimited(Stream stream, SubscribeNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x000A8B60 File Offset: 0x000A6D60
		public static SubscribeNotification Deserialize(Stream stream, SubscribeNotification instance, long limit)
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

		// Token: 0x06003270 RID: 12912 RVA: 0x000A8C2A File Offset: 0x000A6E2A
		public void Serialize(Stream stream)
		{
			SubscribeNotification.Serialize(stream, this);
		}

		// Token: 0x06003271 RID: 12913 RVA: 0x000A8C34 File Offset: 0x000A6E34
		public static void Serialize(Stream stream, SubscribeNotification instance)
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

		// Token: 0x06003272 RID: 12914 RVA: 0x000A8CD8 File Offset: 0x000A6ED8
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

		// Token: 0x040013D6 RID: 5078
		public bool HasSubscriberId;

		// Token: 0x040013D7 RID: 5079
		private AccountId _SubscriberId;

		// Token: 0x040013D8 RID: 5080
		private List<PresenceState> _State = new List<PresenceState>();
	}
}
