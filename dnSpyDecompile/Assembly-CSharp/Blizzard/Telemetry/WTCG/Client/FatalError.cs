using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011BE RID: 4542
	public class FatalError : IProtoBuf
	{
		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x0600C98C RID: 51596 RVA: 0x003C67C6 File Offset: 0x003C49C6
		// (set) Token: 0x0600C98D RID: 51597 RVA: 0x003C67CE File Offset: 0x003C49CE
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

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x0600C98E RID: 51598 RVA: 0x003C67E1 File Offset: 0x003C49E1
		// (set) Token: 0x0600C98F RID: 51599 RVA: 0x003C67E9 File Offset: 0x003C49E9
		public string Reason
		{
			get
			{
				return this._Reason;
			}
			set
			{
				this._Reason = value;
				this.HasReason = (value != null);
			}
		}

		// Token: 0x0600C990 RID: 51600 RVA: 0x003C67FC File Offset: 0x003C49FC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C991 RID: 51601 RVA: 0x003C6844 File Offset: 0x003C4A44
		public override bool Equals(object obj)
		{
			FatalError fatalError = obj as FatalError;
			return fatalError != null && this.HasDeviceInfo == fatalError.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(fatalError.DeviceInfo)) && this.HasReason == fatalError.HasReason && (!this.HasReason || this.Reason.Equals(fatalError.Reason));
		}

		// Token: 0x0600C992 RID: 51602 RVA: 0x003C68B4 File Offset: 0x003C4AB4
		public void Deserialize(Stream stream)
		{
			FatalError.Deserialize(stream, this);
		}

		// Token: 0x0600C993 RID: 51603 RVA: 0x003C68BE File Offset: 0x003C4ABE
		public static FatalError Deserialize(Stream stream, FatalError instance)
		{
			return FatalError.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C994 RID: 51604 RVA: 0x003C68CC File Offset: 0x003C4ACC
		public static FatalError DeserializeLengthDelimited(Stream stream)
		{
			FatalError fatalError = new FatalError();
			FatalError.DeserializeLengthDelimited(stream, fatalError);
			return fatalError;
		}

		// Token: 0x0600C995 RID: 51605 RVA: 0x003C68E8 File Offset: 0x003C4AE8
		public static FatalError DeserializeLengthDelimited(Stream stream, FatalError instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FatalError.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C996 RID: 51606 RVA: 0x003C6910 File Offset: 0x003C4B10
		public static FatalError Deserialize(Stream stream, FatalError instance, long limit)
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
					else
					{
						instance.Reason = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600C997 RID: 51607 RVA: 0x003C69C2 File Offset: 0x003C4BC2
		public void Serialize(Stream stream)
		{
			FatalError.Serialize(stream, this);
		}

		// Token: 0x0600C998 RID: 51608 RVA: 0x003C69CC File Offset: 0x003C4BCC
		public static void Serialize(Stream stream, FatalError instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasReason)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Reason));
			}
		}

		// Token: 0x0600C999 RID: 51609 RVA: 0x003C6A2C File Offset: 0x003C4C2C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasReason)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Reason);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x04009F5B RID: 40795
		public bool HasDeviceInfo;

		// Token: 0x04009F5C RID: 40796
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009F5D RID: 40797
		public bool HasReason;

		// Token: 0x04009F5E RID: 40798
		private string _Reason;
	}
}
