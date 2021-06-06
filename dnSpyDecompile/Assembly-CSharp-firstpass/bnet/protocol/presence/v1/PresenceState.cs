using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.presence.v1
{
	// Token: 0x02000344 RID: 836
	public class PresenceState : IProtoBuf
	{
		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x060033F1 RID: 13297 RVA: 0x000ACC86 File Offset: 0x000AAE86
		// (set) Token: 0x060033F2 RID: 13298 RVA: 0x000ACC8E File Offset: 0x000AAE8E
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

		// Token: 0x060033F3 RID: 13299 RVA: 0x000ACCA1 File Offset: 0x000AAEA1
		public void SetEntityId(EntityId val)
		{
			this.EntityId = val;
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x060033F4 RID: 13300 RVA: 0x000ACCAA File Offset: 0x000AAEAA
		// (set) Token: 0x060033F5 RID: 13301 RVA: 0x000ACCB2 File Offset: 0x000AAEB2
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

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x060033F6 RID: 13302 RVA: 0x000ACCAA File Offset: 0x000AAEAA
		public List<FieldOperation> FieldOperationList
		{
			get
			{
				return this._FieldOperation;
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x060033F7 RID: 13303 RVA: 0x000ACCBB File Offset: 0x000AAEBB
		public int FieldOperationCount
		{
			get
			{
				return this._FieldOperation.Count;
			}
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x000ACCC8 File Offset: 0x000AAEC8
		public void AddFieldOperation(FieldOperation val)
		{
			this._FieldOperation.Add(val);
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x000ACCD6 File Offset: 0x000AAED6
		public void ClearFieldOperation()
		{
			this._FieldOperation.Clear();
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x000ACCE3 File Offset: 0x000AAEE3
		public void SetFieldOperation(List<FieldOperation> val)
		{
			this.FieldOperation = val;
		}

		// Token: 0x060033FB RID: 13307 RVA: 0x000ACCEC File Offset: 0x000AAEEC
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
			return num;
		}

		// Token: 0x060033FC RID: 13308 RVA: 0x000ACD64 File Offset: 0x000AAF64
		public override bool Equals(object obj)
		{
			PresenceState presenceState = obj as PresenceState;
			if (presenceState == null)
			{
				return false;
			}
			if (this.HasEntityId != presenceState.HasEntityId || (this.HasEntityId && !this.EntityId.Equals(presenceState.EntityId)))
			{
				return false;
			}
			if (this.FieldOperation.Count != presenceState.FieldOperation.Count)
			{
				return false;
			}
			for (int i = 0; i < this.FieldOperation.Count; i++)
			{
				if (!this.FieldOperation[i].Equals(presenceState.FieldOperation[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x060033FD RID: 13309 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060033FE RID: 13310 RVA: 0x000ACDFA File Offset: 0x000AAFFA
		public static PresenceState ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<PresenceState>(bs, 0, -1);
		}

		// Token: 0x060033FF RID: 13311 RVA: 0x000ACE04 File Offset: 0x000AB004
		public void Deserialize(Stream stream)
		{
			PresenceState.Deserialize(stream, this);
		}

		// Token: 0x06003400 RID: 13312 RVA: 0x000ACE0E File Offset: 0x000AB00E
		public static PresenceState Deserialize(Stream stream, PresenceState instance)
		{
			return PresenceState.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003401 RID: 13313 RVA: 0x000ACE1C File Offset: 0x000AB01C
		public static PresenceState DeserializeLengthDelimited(Stream stream)
		{
			PresenceState presenceState = new PresenceState();
			PresenceState.DeserializeLengthDelimited(stream, presenceState);
			return presenceState;
		}

		// Token: 0x06003402 RID: 13314 RVA: 0x000ACE38 File Offset: 0x000AB038
		public static PresenceState DeserializeLengthDelimited(Stream stream, PresenceState instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PresenceState.Deserialize(stream, instance, num);
		}

		// Token: 0x06003403 RID: 13315 RVA: 0x000ACE60 File Offset: 0x000AB060
		public static PresenceState Deserialize(Stream stream, PresenceState instance, long limit)
		{
			if (instance.FieldOperation == null)
			{
				instance.FieldOperation = new List<FieldOperation>();
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

		// Token: 0x06003404 RID: 13316 RVA: 0x000ACF2A File Offset: 0x000AB12A
		public void Serialize(Stream stream)
		{
			PresenceState.Serialize(stream, this);
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x000ACF34 File Offset: 0x000AB134
		public static void Serialize(Stream stream, PresenceState instance)
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
		}

		// Token: 0x06003406 RID: 13318 RVA: 0x000ACFD8 File Offset: 0x000AB1D8
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
			return num;
		}

		// Token: 0x04001414 RID: 5140
		public bool HasEntityId;

		// Token: 0x04001415 RID: 5141
		private EntityId _EntityId;

		// Token: 0x04001416 RID: 5142
		private List<FieldOperation> _FieldOperation = new List<FieldOperation>();
	}
}
