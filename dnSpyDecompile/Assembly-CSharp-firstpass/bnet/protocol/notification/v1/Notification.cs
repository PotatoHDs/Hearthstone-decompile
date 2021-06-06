using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.notification.v1
{
	// Token: 0x0200034B RID: 843
	public class Notification : IProtoBuf
	{
		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06003482 RID: 13442 RVA: 0x000AE3D3 File Offset: 0x000AC5D3
		// (set) Token: 0x06003483 RID: 13443 RVA: 0x000AE3DB File Offset: 0x000AC5DB
		public EntityId SenderId
		{
			get
			{
				return this._SenderId;
			}
			set
			{
				this._SenderId = value;
				this.HasSenderId = (value != null);
			}
		}

		// Token: 0x06003484 RID: 13444 RVA: 0x000AE3EE File Offset: 0x000AC5EE
		public void SetSenderId(EntityId val)
		{
			this.SenderId = val;
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06003485 RID: 13445 RVA: 0x000AE3F7 File Offset: 0x000AC5F7
		// (set) Token: 0x06003486 RID: 13446 RVA: 0x000AE3FF File Offset: 0x000AC5FF
		public EntityId TargetId { get; set; }

		// Token: 0x06003487 RID: 13447 RVA: 0x000AE408 File Offset: 0x000AC608
		public void SetTargetId(EntityId val)
		{
			this.TargetId = val;
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06003488 RID: 13448 RVA: 0x000AE411 File Offset: 0x000AC611
		// (set) Token: 0x06003489 RID: 13449 RVA: 0x000AE419 File Offset: 0x000AC619
		public string Type { get; set; }

		// Token: 0x0600348A RID: 13450 RVA: 0x000AE422 File Offset: 0x000AC622
		public void SetType(string val)
		{
			this.Type = val;
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x0600348B RID: 13451 RVA: 0x000AE42B File Offset: 0x000AC62B
		// (set) Token: 0x0600348C RID: 13452 RVA: 0x000AE433 File Offset: 0x000AC633
		public List<Attribute> Attribute
		{
			get
			{
				return this._Attribute;
			}
			set
			{
				this._Attribute = value;
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x0600348D RID: 13453 RVA: 0x000AE42B File Offset: 0x000AC62B
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x0600348E RID: 13454 RVA: 0x000AE43C File Offset: 0x000AC63C
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x0600348F RID: 13455 RVA: 0x000AE449 File Offset: 0x000AC649
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x000AE457 File Offset: 0x000AC657
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x000AE464 File Offset: 0x000AC664
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06003492 RID: 13458 RVA: 0x000AE46D File Offset: 0x000AC66D
		// (set) Token: 0x06003493 RID: 13459 RVA: 0x000AE475 File Offset: 0x000AC675
		public EntityId SenderAccountId
		{
			get
			{
				return this._SenderAccountId;
			}
			set
			{
				this._SenderAccountId = value;
				this.HasSenderAccountId = (value != null);
			}
		}

		// Token: 0x06003494 RID: 13460 RVA: 0x000AE488 File Offset: 0x000AC688
		public void SetSenderAccountId(EntityId val)
		{
			this.SenderAccountId = val;
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06003495 RID: 13461 RVA: 0x000AE491 File Offset: 0x000AC691
		// (set) Token: 0x06003496 RID: 13462 RVA: 0x000AE499 File Offset: 0x000AC699
		public EntityId TargetAccountId
		{
			get
			{
				return this._TargetAccountId;
			}
			set
			{
				this._TargetAccountId = value;
				this.HasTargetAccountId = (value != null);
			}
		}

		// Token: 0x06003497 RID: 13463 RVA: 0x000AE4AC File Offset: 0x000AC6AC
		public void SetTargetAccountId(EntityId val)
		{
			this.TargetAccountId = val;
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06003498 RID: 13464 RVA: 0x000AE4B5 File Offset: 0x000AC6B5
		// (set) Token: 0x06003499 RID: 13465 RVA: 0x000AE4BD File Offset: 0x000AC6BD
		public string SenderBattleTag
		{
			get
			{
				return this._SenderBattleTag;
			}
			set
			{
				this._SenderBattleTag = value;
				this.HasSenderBattleTag = (value != null);
			}
		}

		// Token: 0x0600349A RID: 13466 RVA: 0x000AE4D0 File Offset: 0x000AC6D0
		public void SetSenderBattleTag(string val)
		{
			this.SenderBattleTag = val;
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x0600349B RID: 13467 RVA: 0x000AE4D9 File Offset: 0x000AC6D9
		// (set) Token: 0x0600349C RID: 13468 RVA: 0x000AE4E1 File Offset: 0x000AC6E1
		public string TargetBattleTag
		{
			get
			{
				return this._TargetBattleTag;
			}
			set
			{
				this._TargetBattleTag = value;
				this.HasTargetBattleTag = (value != null);
			}
		}

		// Token: 0x0600349D RID: 13469 RVA: 0x000AE4F4 File Offset: 0x000AC6F4
		public void SetTargetBattleTag(string val)
		{
			this.TargetBattleTag = val;
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x0600349E RID: 13470 RVA: 0x000AE4FD File Offset: 0x000AC6FD
		// (set) Token: 0x0600349F RID: 13471 RVA: 0x000AE505 File Offset: 0x000AC705
		public ProcessId Peer
		{
			get
			{
				return this._Peer;
			}
			set
			{
				this._Peer = value;
				this.HasPeer = (value != null);
			}
		}

		// Token: 0x060034A0 RID: 13472 RVA: 0x000AE518 File Offset: 0x000AC718
		public void SetPeer(ProcessId val)
		{
			this.Peer = val;
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x060034A1 RID: 13473 RVA: 0x000AE521 File Offset: 0x000AC721
		// (set) Token: 0x060034A2 RID: 13474 RVA: 0x000AE529 File Offset: 0x000AC729
		public Identity ForwardingIdentity
		{
			get
			{
				return this._ForwardingIdentity;
			}
			set
			{
				this._ForwardingIdentity = value;
				this.HasForwardingIdentity = (value != null);
			}
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x000AE53C File Offset: 0x000AC73C
		public void SetForwardingIdentity(Identity val)
		{
			this.ForwardingIdentity = val;
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x000AE548 File Offset: 0x000AC748
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasSenderId)
			{
				num ^= this.SenderId.GetHashCode();
			}
			num ^= this.TargetId.GetHashCode();
			num ^= this.Type.GetHashCode();
			foreach (Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			if (this.HasSenderAccountId)
			{
				num ^= this.SenderAccountId.GetHashCode();
			}
			if (this.HasTargetAccountId)
			{
				num ^= this.TargetAccountId.GetHashCode();
			}
			if (this.HasSenderBattleTag)
			{
				num ^= this.SenderBattleTag.GetHashCode();
			}
			if (this.HasTargetBattleTag)
			{
				num ^= this.TargetBattleTag.GetHashCode();
			}
			if (this.HasPeer)
			{
				num ^= this.Peer.GetHashCode();
			}
			if (this.HasForwardingIdentity)
			{
				num ^= this.ForwardingIdentity.GetHashCode();
			}
			return num;
		}

		// Token: 0x060034A5 RID: 13477 RVA: 0x000AE660 File Offset: 0x000AC860
		public override bool Equals(object obj)
		{
			Notification notification = obj as Notification;
			if (notification == null)
			{
				return false;
			}
			if (this.HasSenderId != notification.HasSenderId || (this.HasSenderId && !this.SenderId.Equals(notification.SenderId)))
			{
				return false;
			}
			if (!this.TargetId.Equals(notification.TargetId))
			{
				return false;
			}
			if (!this.Type.Equals(notification.Type))
			{
				return false;
			}
			if (this.Attribute.Count != notification.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(notification.Attribute[i]))
				{
					return false;
				}
			}
			return this.HasSenderAccountId == notification.HasSenderAccountId && (!this.HasSenderAccountId || this.SenderAccountId.Equals(notification.SenderAccountId)) && this.HasTargetAccountId == notification.HasTargetAccountId && (!this.HasTargetAccountId || this.TargetAccountId.Equals(notification.TargetAccountId)) && this.HasSenderBattleTag == notification.HasSenderBattleTag && (!this.HasSenderBattleTag || this.SenderBattleTag.Equals(notification.SenderBattleTag)) && this.HasTargetBattleTag == notification.HasTargetBattleTag && (!this.HasTargetBattleTag || this.TargetBattleTag.Equals(notification.TargetBattleTag)) && this.HasPeer == notification.HasPeer && (!this.HasPeer || this.Peer.Equals(notification.Peer)) && this.HasForwardingIdentity == notification.HasForwardingIdentity && (!this.HasForwardingIdentity || this.ForwardingIdentity.Equals(notification.ForwardingIdentity));
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x060034A6 RID: 13478 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060034A7 RID: 13479 RVA: 0x000AE822 File Offset: 0x000ACA22
		public static Notification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Notification>(bs, 0, -1);
		}

		// Token: 0x060034A8 RID: 13480 RVA: 0x000AE82C File Offset: 0x000ACA2C
		public void Deserialize(Stream stream)
		{
			Notification.Deserialize(stream, this);
		}

		// Token: 0x060034A9 RID: 13481 RVA: 0x000AE836 File Offset: 0x000ACA36
		public static Notification Deserialize(Stream stream, Notification instance)
		{
			return Notification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060034AA RID: 13482 RVA: 0x000AE844 File Offset: 0x000ACA44
		public static Notification DeserializeLengthDelimited(Stream stream)
		{
			Notification notification = new Notification();
			Notification.DeserializeLengthDelimited(stream, notification);
			return notification;
		}

		// Token: 0x060034AB RID: 13483 RVA: 0x000AE860 File Offset: 0x000ACA60
		public static Notification DeserializeLengthDelimited(Stream stream, Notification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Notification.Deserialize(stream, instance, num);
		}

		// Token: 0x060034AC RID: 13484 RVA: 0x000AE888 File Offset: 0x000ACA88
		public static Notification Deserialize(Stream stream, Notification instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
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
				else
				{
					if (num <= 42)
					{
						if (num <= 18)
						{
							if (num != 10)
							{
								if (num == 18)
								{
									if (instance.TargetId == null)
									{
										instance.TargetId = EntityId.DeserializeLengthDelimited(stream);
										continue;
									}
									EntityId.DeserializeLengthDelimited(stream, instance.TargetId);
									continue;
								}
							}
							else
							{
								if (instance.SenderId == null)
								{
									instance.SenderId = EntityId.DeserializeLengthDelimited(stream);
									continue;
								}
								EntityId.DeserializeLengthDelimited(stream, instance.SenderId);
								continue;
							}
						}
						else
						{
							if (num == 26)
							{
								instance.Type = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 34)
							{
								instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
								continue;
							}
							if (num == 42)
							{
								if (instance.SenderAccountId == null)
								{
									instance.SenderAccountId = EntityId.DeserializeLengthDelimited(stream);
									continue;
								}
								EntityId.DeserializeLengthDelimited(stream, instance.SenderAccountId);
								continue;
							}
						}
					}
					else if (num <= 58)
					{
						if (num != 50)
						{
							if (num == 58)
							{
								instance.SenderBattleTag = ProtocolParser.ReadString(stream);
								continue;
							}
						}
						else
						{
							if (instance.TargetAccountId == null)
							{
								instance.TargetAccountId = EntityId.DeserializeLengthDelimited(stream);
								continue;
							}
							EntityId.DeserializeLengthDelimited(stream, instance.TargetAccountId);
							continue;
						}
					}
					else
					{
						if (num == 66)
						{
							instance.TargetBattleTag = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num != 74)
						{
							if (num == 82)
							{
								if (instance.ForwardingIdentity == null)
								{
									instance.ForwardingIdentity = Identity.DeserializeLengthDelimited(stream);
									continue;
								}
								Identity.DeserializeLengthDelimited(stream, instance.ForwardingIdentity);
								continue;
							}
						}
						else
						{
							if (instance.Peer == null)
							{
								instance.Peer = ProcessId.DeserializeLengthDelimited(stream);
								continue;
							}
							ProcessId.DeserializeLengthDelimited(stream, instance.Peer);
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

		// Token: 0x060034AD RID: 13485 RVA: 0x000AEACC File Offset: 0x000ACCCC
		public void Serialize(Stream stream)
		{
			Notification.Serialize(stream, this);
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x000AEAD8 File Offset: 0x000ACCD8
		public static void Serialize(Stream stream, Notification instance)
		{
			if (instance.HasSenderId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.SenderId.GetSerializedSize());
				EntityId.Serialize(stream, instance.SenderId);
			}
			if (instance.TargetId == null)
			{
				throw new ArgumentNullException("TargetId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
			EntityId.Serialize(stream, instance.TargetId);
			if (instance.Type == null)
			{
				throw new ArgumentNullException("Type", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Type));
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.HasSenderAccountId)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.SenderAccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.SenderAccountId);
			}
			if (instance.HasTargetAccountId)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.TargetAccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.TargetAccountId);
			}
			if (instance.HasSenderBattleTag)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SenderBattleTag));
			}
			if (instance.HasTargetBattleTag)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TargetBattleTag));
			}
			if (instance.HasPeer)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteUInt32(stream, instance.Peer.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Peer);
			}
			if (instance.HasForwardingIdentity)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteUInt32(stream, instance.ForwardingIdentity.GetSerializedSize());
				Identity.Serialize(stream, instance.ForwardingIdentity);
			}
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x000AECF0 File Offset: 0x000ACEF0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasSenderId)
			{
				num += 1U;
				uint serializedSize = this.SenderId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = this.TargetId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Type);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (this.Attribute.Count > 0)
			{
				foreach (Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize3 = attribute.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (this.HasSenderAccountId)
			{
				num += 1U;
				uint serializedSize4 = this.SenderAccountId.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (this.HasTargetAccountId)
			{
				num += 1U;
				uint serializedSize5 = this.TargetAccountId.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			if (this.HasSenderBattleTag)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.SenderBattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasTargetBattleTag)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.TargetBattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasPeer)
			{
				num += 1U;
				uint serializedSize6 = this.Peer.GetSerializedSize();
				num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
			}
			if (this.HasForwardingIdentity)
			{
				num += 1U;
				uint serializedSize7 = this.ForwardingIdentity.GetSerializedSize();
				num += serializedSize7 + ProtocolParser.SizeOfUInt32(serializedSize7);
			}
			num += 2U;
			return num;
		}

		// Token: 0x0400142D RID: 5165
		public bool HasSenderId;

		// Token: 0x0400142E RID: 5166
		private EntityId _SenderId;

		// Token: 0x04001431 RID: 5169
		private List<Attribute> _Attribute = new List<Attribute>();

		// Token: 0x04001432 RID: 5170
		public bool HasSenderAccountId;

		// Token: 0x04001433 RID: 5171
		private EntityId _SenderAccountId;

		// Token: 0x04001434 RID: 5172
		public bool HasTargetAccountId;

		// Token: 0x04001435 RID: 5173
		private EntityId _TargetAccountId;

		// Token: 0x04001436 RID: 5174
		public bool HasSenderBattleTag;

		// Token: 0x04001437 RID: 5175
		private string _SenderBattleTag;

		// Token: 0x04001438 RID: 5176
		public bool HasTargetBattleTag;

		// Token: 0x04001439 RID: 5177
		private string _TargetBattleTag;

		// Token: 0x0400143A RID: 5178
		public bool HasPeer;

		// Token: 0x0400143B RID: 5179
		private ProcessId _Peer;

		// Token: 0x0400143C RID: 5180
		public bool HasForwardingIdentity;

		// Token: 0x0400143D RID: 5181
		private Identity _ForwardingIdentity;
	}
}
