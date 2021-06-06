using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011A7 RID: 4519
	public class BoxInteractable : IProtoBuf
	{
		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x0600C7F5 RID: 51189 RVA: 0x003C12F0 File Offset: 0x003BF4F0
		// (set) Token: 0x0600C7F6 RID: 51190 RVA: 0x003C12F8 File Offset: 0x003BF4F8
		public string TestType
		{
			get
			{
				return this._TestType;
			}
			set
			{
				this._TestType = value;
				this.HasTestType = (value != null);
			}
		}

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x0600C7F7 RID: 51191 RVA: 0x003C130B File Offset: 0x003BF50B
		// (set) Token: 0x0600C7F8 RID: 51192 RVA: 0x003C1313 File Offset: 0x003BF513
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

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x0600C7F9 RID: 51193 RVA: 0x003C1323 File Offset: 0x003BF523
		// (set) Token: 0x0600C7FA RID: 51194 RVA: 0x003C132B File Offset: 0x003BF52B
		public string ClientChangelist
		{
			get
			{
				return this._ClientChangelist;
			}
			set
			{
				this._ClientChangelist = value;
				this.HasClientChangelist = (value != null);
			}
		}

		// Token: 0x0600C7FB RID: 51195 RVA: 0x003C1340 File Offset: 0x003BF540
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasTestType)
			{
				num ^= this.TestType.GetHashCode();
			}
			if (this.HasDuration)
			{
				num ^= this.Duration.GetHashCode();
			}
			if (this.HasClientChangelist)
			{
				num ^= this.ClientChangelist.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C7FC RID: 51196 RVA: 0x003C13A0 File Offset: 0x003BF5A0
		public override bool Equals(object obj)
		{
			BoxInteractable boxInteractable = obj as BoxInteractable;
			return boxInteractable != null && this.HasTestType == boxInteractable.HasTestType && (!this.HasTestType || this.TestType.Equals(boxInteractable.TestType)) && this.HasDuration == boxInteractable.HasDuration && (!this.HasDuration || this.Duration.Equals(boxInteractable.Duration)) && this.HasClientChangelist == boxInteractable.HasClientChangelist && (!this.HasClientChangelist || this.ClientChangelist.Equals(boxInteractable.ClientChangelist));
		}

		// Token: 0x0600C7FD RID: 51197 RVA: 0x003C143E File Offset: 0x003BF63E
		public void Deserialize(Stream stream)
		{
			BoxInteractable.Deserialize(stream, this);
		}

		// Token: 0x0600C7FE RID: 51198 RVA: 0x003C1448 File Offset: 0x003BF648
		public static BoxInteractable Deserialize(Stream stream, BoxInteractable instance)
		{
			return BoxInteractable.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C7FF RID: 51199 RVA: 0x003C1454 File Offset: 0x003BF654
		public static BoxInteractable DeserializeLengthDelimited(Stream stream)
		{
			BoxInteractable boxInteractable = new BoxInteractable();
			BoxInteractable.DeserializeLengthDelimited(stream, boxInteractable);
			return boxInteractable;
		}

		// Token: 0x0600C800 RID: 51200 RVA: 0x003C1470 File Offset: 0x003BF670
		public static BoxInteractable DeserializeLengthDelimited(Stream stream, BoxInteractable instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BoxInteractable.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C801 RID: 51201 RVA: 0x003C1498 File Offset: 0x003BF698
		public static BoxInteractable Deserialize(Stream stream, BoxInteractable instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
						if (num != 26)
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
							instance.ClientChangelist = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.Duration = binaryReader.ReadSingle();
					}
				}
				else
				{
					instance.TestType = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600C802 RID: 51202 RVA: 0x003C154D File Offset: 0x003BF74D
		public void Serialize(Stream stream)
		{
			BoxInteractable.Serialize(stream, this);
		}

		// Token: 0x0600C803 RID: 51203 RVA: 0x003C1558 File Offset: 0x003BF758
		public static void Serialize(Stream stream, BoxInteractable instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasTestType)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TestType));
			}
			if (instance.HasDuration)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Duration);
			}
			if (instance.HasClientChangelist)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientChangelist));
			}
		}

		// Token: 0x0600C804 RID: 51204 RVA: 0x003C15D4 File Offset: 0x003BF7D4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasTestType)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.TestType);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasDuration)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasClientChangelist)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.ClientChangelist);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x04009EC1 RID: 40641
		public bool HasTestType;

		// Token: 0x04009EC2 RID: 40642
		private string _TestType;

		// Token: 0x04009EC3 RID: 40643
		public bool HasDuration;

		// Token: 0x04009EC4 RID: 40644
		private float _Duration;

		// Token: 0x04009EC5 RID: 40645
		public bool HasClientChangelist;

		// Token: 0x04009EC6 RID: 40646
		private string _ClientChangelist;
	}
}
