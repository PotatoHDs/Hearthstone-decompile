using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011AB RID: 4523
	public class ClickRecruitAFriend : IProtoBuf
	{
		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x0600C831 RID: 51249 RVA: 0x003C1D3B File Offset: 0x003BFF3B
		// (set) Token: 0x0600C832 RID: 51250 RVA: 0x003C1D43 File Offset: 0x003BFF43
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

		// Token: 0x0600C833 RID: 51251 RVA: 0x003C1D58 File Offset: 0x003BFF58
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C834 RID: 51252 RVA: 0x003C1D88 File Offset: 0x003BFF88
		public override bool Equals(object obj)
		{
			ClickRecruitAFriend clickRecruitAFriend = obj as ClickRecruitAFriend;
			return clickRecruitAFriend != null && this.HasDeviceInfo == clickRecruitAFriend.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(clickRecruitAFriend.DeviceInfo));
		}

		// Token: 0x0600C835 RID: 51253 RVA: 0x003C1DCD File Offset: 0x003BFFCD
		public void Deserialize(Stream stream)
		{
			ClickRecruitAFriend.Deserialize(stream, this);
		}

		// Token: 0x0600C836 RID: 51254 RVA: 0x003C1DD7 File Offset: 0x003BFFD7
		public static ClickRecruitAFriend Deserialize(Stream stream, ClickRecruitAFriend instance)
		{
			return ClickRecruitAFriend.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C837 RID: 51255 RVA: 0x003C1DE4 File Offset: 0x003BFFE4
		public static ClickRecruitAFriend DeserializeLengthDelimited(Stream stream)
		{
			ClickRecruitAFriend clickRecruitAFriend = new ClickRecruitAFriend();
			ClickRecruitAFriend.DeserializeLengthDelimited(stream, clickRecruitAFriend);
			return clickRecruitAFriend;
		}

		// Token: 0x0600C838 RID: 51256 RVA: 0x003C1E00 File Offset: 0x003C0000
		public static ClickRecruitAFriend DeserializeLengthDelimited(Stream stream, ClickRecruitAFriend instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ClickRecruitAFriend.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C839 RID: 51257 RVA: 0x003C1E28 File Offset: 0x003C0028
		public static ClickRecruitAFriend Deserialize(Stream stream, ClickRecruitAFriend instance, long limit)
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
				else if (num == 10)
				{
					if (instance.DeviceInfo == null)
					{
						instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
					}
				}
				else
				{
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

		// Token: 0x0600C83A RID: 51258 RVA: 0x003C1EC2 File Offset: 0x003C00C2
		public void Serialize(Stream stream)
		{
			ClickRecruitAFriend.Serialize(stream, this);
		}

		// Token: 0x0600C83B RID: 51259 RVA: 0x003C1ECB File Offset: 0x003C00CB
		public static void Serialize(Stream stream, ClickRecruitAFriend instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
		}

		// Token: 0x0600C83C RID: 51260 RVA: 0x003C1EFC File Offset: 0x003C00FC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04009ED1 RID: 40657
		public bool HasDeviceInfo;

		// Token: 0x04009ED2 RID: 40658
		private DeviceInfo _DeviceInfo;
	}
}
