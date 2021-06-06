using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011EF RID: 4591
	public class RuntimeUpdate : IProtoBuf
	{
		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x0600CD27 RID: 52519 RVA: 0x003D34B1 File Offset: 0x003D16B1
		// (set) Token: 0x0600CD28 RID: 52520 RVA: 0x003D34B9 File Offset: 0x003D16B9
		public DeviceInfo DeviceInfo
		{
			get
			{
				return this._DeviceInfo;
			}
			set
			{
				this._DeviceInfo = value;
				this.HasDeviceInfo = (value != null);
			}
		}

		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x0600CD29 RID: 52521 RVA: 0x003D34CC File Offset: 0x003D16CC
		// (set) Token: 0x0600CD2A RID: 52522 RVA: 0x003D34D4 File Offset: 0x003D16D4
		public float Duration
		{
			get
			{
				return this._Duration;
			}
			set
			{
				this._Duration = value;
				this.HasDuration = true;
			}
		}

		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x0600CD2B RID: 52523 RVA: 0x003D34E4 File Offset: 0x003D16E4
		// (set) Token: 0x0600CD2C RID: 52524 RVA: 0x003D34EC File Offset: 0x003D16EC
		public RuntimeUpdate.Intention Intention_
		{
			get
			{
				return this._Intention_;
			}
			set
			{
				this._Intention_ = value;
				this.HasIntention_ = true;
			}
		}

		// Token: 0x0600CD2D RID: 52525 RVA: 0x003D34FC File Offset: 0x003D16FC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasDuration)
			{
				num ^= this.Duration.GetHashCode();
			}
			if (this.HasIntention_)
			{
				num ^= this.Intention_.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CD2E RID: 52526 RVA: 0x003D3564 File Offset: 0x003D1764
		public override bool Equals(object obj)
		{
			RuntimeUpdate runtimeUpdate = obj as RuntimeUpdate;
			return runtimeUpdate != null && this.HasDeviceInfo == runtimeUpdate.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(runtimeUpdate.DeviceInfo)) && this.HasDuration == runtimeUpdate.HasDuration && (!this.HasDuration || this.Duration.Equals(runtimeUpdate.Duration)) && this.HasIntention_ == runtimeUpdate.HasIntention_ && (!this.HasIntention_ || this.Intention_.Equals(runtimeUpdate.Intention_));
		}

		// Token: 0x0600CD2F RID: 52527 RVA: 0x003D3610 File Offset: 0x003D1810
		public void Deserialize(Stream stream)
		{
			RuntimeUpdate.Deserialize(stream, this);
		}

		// Token: 0x0600CD30 RID: 52528 RVA: 0x003D361A File Offset: 0x003D181A
		public static RuntimeUpdate Deserialize(Stream stream, RuntimeUpdate instance)
		{
			return RuntimeUpdate.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CD31 RID: 52529 RVA: 0x003D3628 File Offset: 0x003D1828
		public static RuntimeUpdate DeserializeLengthDelimited(Stream stream)
		{
			RuntimeUpdate runtimeUpdate = new RuntimeUpdate();
			RuntimeUpdate.DeserializeLengthDelimited(stream, runtimeUpdate);
			return runtimeUpdate;
		}

		// Token: 0x0600CD32 RID: 52530 RVA: 0x003D3644 File Offset: 0x003D1844
		public static RuntimeUpdate DeserializeLengthDelimited(Stream stream, RuntimeUpdate instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RuntimeUpdate.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CD33 RID: 52531 RVA: 0x003D366C File Offset: 0x003D186C
		public static RuntimeUpdate Deserialize(Stream stream, RuntimeUpdate instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.Intention_ = RuntimeUpdate.Intention.UNINITIALIZED;
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
					if (num != 21)
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
							instance.Intention_ = (RuntimeUpdate.Intention)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.Duration = binaryReader.ReadSingle();
					}
				}
				else if (instance.DeviceInfo == null)
				{
					instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
				}
				else
				{
					DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600CD34 RID: 52532 RVA: 0x003D3749 File Offset: 0x003D1949
		public void Serialize(Stream stream)
		{
			RuntimeUpdate.Serialize(stream, this);
		}

		// Token: 0x0600CD35 RID: 52533 RVA: 0x003D3754 File Offset: 0x003D1954
		public static void Serialize(Stream stream, RuntimeUpdate instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasDuration)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Duration);
			}
			if (instance.HasIntention_)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Intention_));
			}
		}

		// Token: 0x0600CD36 RID: 52534 RVA: 0x003D37D0 File Offset: 0x003D19D0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasDuration)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasIntention_)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Intention_));
			}
			return num;
		}

		// Token: 0x0400A0DB RID: 41179
		public bool HasDeviceInfo;

		// Token: 0x0400A0DC RID: 41180
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A0DD RID: 41181
		public bool HasDuration;

		// Token: 0x0400A0DE RID: 41182
		private float _Duration;

		// Token: 0x0400A0DF RID: 41183
		public bool HasIntention_;

		// Token: 0x0400A0E0 RID: 41184
		private RuntimeUpdate.Intention _Intention_;

		// Token: 0x0200294B RID: 10571
		public enum Intention
		{
			// Token: 0x0400FC87 RID: 64647
			UNINITIALIZED = -1,
			// Token: 0x0400FC88 RID: 64648
			HIGH_RES,
			// Token: 0x0400FC89 RID: 64649
			DONE = 100
		}
	}
}
