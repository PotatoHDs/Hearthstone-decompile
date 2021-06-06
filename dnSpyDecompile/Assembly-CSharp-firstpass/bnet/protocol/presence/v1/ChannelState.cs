using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.presence.v1
{
	// Token: 0x02000345 RID: 837
	public class ChannelState : IProtoBuf
	{
		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06003408 RID: 13320 RVA: 0x000AD07F File Offset: 0x000AB27F
		// (set) Token: 0x06003409 RID: 13321 RVA: 0x000AD087 File Offset: 0x000AB287
		public EntityId EntityId
		{
			get
			{
				return this._EntityId;
			}
			set
			{
				this._EntityId = value;
				this.HasEntityId = (value != null);
			}
		}

		// Token: 0x0600340A RID: 13322 RVA: 0x000AD09A File Offset: 0x000AB29A
		public void SetEntityId(EntityId val)
		{
			this.EntityId = val;
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x0600340B RID: 13323 RVA: 0x000AD0A3 File Offset: 0x000AB2A3
		// (set) Token: 0x0600340C RID: 13324 RVA: 0x000AD0AB File Offset: 0x000AB2AB
		public List<FieldOperation> FieldOperation
		{
			get
			{
				return this._FieldOperation;
			}
			set
			{
				this._FieldOperation = value;
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x0600340D RID: 13325 RVA: 0x000AD0A3 File Offset: 0x000AB2A3
		public List<FieldOperation> FieldOperationList
		{
			get
			{
				return this._FieldOperation;
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x0600340E RID: 13326 RVA: 0x000AD0B4 File Offset: 0x000AB2B4
		public int FieldOperationCount
		{
			get
			{
				return this._FieldOperation.Count;
			}
		}

		// Token: 0x0600340F RID: 13327 RVA: 0x000AD0C1 File Offset: 0x000AB2C1
		public void AddFieldOperation(FieldOperation val)
		{
			this._FieldOperation.Add(val);
		}

		// Token: 0x06003410 RID: 13328 RVA: 0x000AD0CF File Offset: 0x000AB2CF
		public void ClearFieldOperation()
		{
			this._FieldOperation.Clear();
		}

		// Token: 0x06003411 RID: 13329 RVA: 0x000AD0DC File Offset: 0x000AB2DC
		public void SetFieldOperation(List<FieldOperation> val)
		{
			this.FieldOperation = val;
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06003412 RID: 13330 RVA: 0x000AD0E5 File Offset: 0x000AB2E5
		// (set) Token: 0x06003413 RID: 13331 RVA: 0x000AD0ED File Offset: 0x000AB2ED
		public bool Healing
		{
			get
			{
				return this._Healing;
			}
			set
			{
				this._Healing = value;
				this.HasHealing = true;
			}
		}

		// Token: 0x06003414 RID: 13332 RVA: 0x000AD0FD File Offset: 0x000AB2FD
		public void SetHealing(bool val)
		{
			this.Healing = val;
		}

		// Token: 0x06003415 RID: 13333 RVA: 0x000AD108 File Offset: 0x000AB308
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasEntityId)
			{
				num ^= this.EntityId.GetHashCode();
			}
			foreach (FieldOperation fieldOperation in this.FieldOperation)
			{
				num ^= fieldOperation.GetHashCode();
			}
			if (this.HasHealing)
			{
				num ^= this.Healing.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003416 RID: 13334 RVA: 0x000AD19C File Offset: 0x000AB39C
		public override bool Equals(object obj)
		{
			ChannelState channelState = obj as ChannelState;
			if (channelState == null)
			{
				return false;
			}
			if (this.HasEntityId != channelState.HasEntityId || (this.HasEntityId && !this.EntityId.Equals(channelState.EntityId)))
			{
				return false;
			}
			if (this.FieldOperation.Count != channelState.FieldOperation.Count)
			{
				return false;
			}
			for (int i = 0; i < this.FieldOperation.Count; i++)
			{
				if (!this.FieldOperation[i].Equals(channelState.FieldOperation[i]))
				{
					return false;
				}
			}
			return this.HasHealing == channelState.HasHealing && (!this.HasHealing || this.Healing.Equals(channelState.Healing));
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06003417 RID: 13335 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x000AD260 File Offset: 0x000AB460
		public static ChannelState ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelState>(bs, 0, -1);
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x000AD26A File Offset: 0x000AB46A
		public void Deserialize(Stream stream)
		{
			ChannelState.Deserialize(stream, this);
		}

		// Token: 0x0600341A RID: 13338 RVA: 0x000AD274 File Offset: 0x000AB474
		public static ChannelState Deserialize(Stream stream, ChannelState instance)
		{
			return ChannelState.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600341B RID: 13339 RVA: 0x000AD280 File Offset: 0x000AB480
		public static ChannelState DeserializeLengthDelimited(Stream stream)
		{
			ChannelState channelState = new ChannelState();
			ChannelState.DeserializeLengthDelimited(stream, channelState);
			return channelState;
		}

		// Token: 0x0600341C RID: 13340 RVA: 0x000AD29C File Offset: 0x000AB49C
		public static ChannelState DeserializeLengthDelimited(Stream stream, ChannelState instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChannelState.Deserialize(stream, instance, num);
		}

		// Token: 0x0600341D RID: 13341 RVA: 0x000AD2C4 File Offset: 0x000AB4C4
		public static ChannelState Deserialize(Stream stream, ChannelState instance, long limit)
		{
			if (instance.FieldOperation == null)
			{
				instance.FieldOperation = new List<FieldOperation>();
			}
			instance.Healing = false;
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
							instance.Healing = ProtocolParser.ReadBool(stream);
						}
					}
					else
					{
						instance.FieldOperation.Add(bnet.protocol.presence.v1.FieldOperation.DeserializeLengthDelimited(stream));
					}
				}
				else if (instance.EntityId == null)
				{
					instance.EntityId = EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					EntityId.DeserializeLengthDelimited(stream, instance.EntityId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600341E RID: 13342 RVA: 0x000AD3B1 File Offset: 0x000AB5B1
		public void Serialize(Stream stream)
		{
			ChannelState.Serialize(stream, this);
		}

		// Token: 0x0600341F RID: 13343 RVA: 0x000AD3BC File Offset: 0x000AB5BC
		public static void Serialize(Stream stream, ChannelState instance)
		{
			if (instance.HasEntityId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
				EntityId.Serialize(stream, instance.EntityId);
			}
			if (instance.FieldOperation.Count > 0)
			{
				foreach (FieldOperation fieldOperation in instance.FieldOperation)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, fieldOperation.GetSerializedSize());
					bnet.protocol.presence.v1.FieldOperation.Serialize(stream, fieldOperation);
				}
			}
			if (instance.HasHealing)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.Healing);
			}
		}

		// Token: 0x06003420 RID: 13344 RVA: 0x000AD47C File Offset: 0x000AB67C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasEntityId)
			{
				num += 1U;
				uint serializedSize = this.EntityId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.FieldOperation.Count > 0)
			{
				foreach (FieldOperation fieldOperation in this.FieldOperation)
				{
					num += 1U;
					uint serializedSize2 = fieldOperation.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasHealing)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04001417 RID: 5143
		public bool HasEntityId;

		// Token: 0x04001418 RID: 5144
		private EntityId _EntityId;

		// Token: 0x04001419 RID: 5145
		private List<FieldOperation> _FieldOperation = new List<FieldOperation>();

		// Token: 0x0400141A RID: 5146
		public bool HasHealing;

		// Token: 0x0400141B RID: 5147
		private bool _Healing;
	}
}
