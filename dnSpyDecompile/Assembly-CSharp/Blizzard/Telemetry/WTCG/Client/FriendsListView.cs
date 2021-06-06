using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011C4 RID: 4548
	public class FriendsListView : IProtoBuf
	{
		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x0600CA12 RID: 51730 RVA: 0x003C88F9 File Offset: 0x003C6AF9
		// (set) Token: 0x0600CA13 RID: 51731 RVA: 0x003C8901 File Offset: 0x003C6B01
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

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x0600CA14 RID: 51732 RVA: 0x003C8914 File Offset: 0x003C6B14
		// (set) Token: 0x0600CA15 RID: 51733 RVA: 0x003C891C File Offset: 0x003C6B1C
		public string CurrentScene
		{
			get
			{
				return this._CurrentScene;
			}
			set
			{
				this._CurrentScene = value;
				this.HasCurrentScene = (value != null);
			}
		}

		// Token: 0x0600CA16 RID: 51734 RVA: 0x003C8930 File Offset: 0x003C6B30
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasCurrentScene)
			{
				num ^= this.CurrentScene.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CA17 RID: 51735 RVA: 0x003C8978 File Offset: 0x003C6B78
		public override bool Equals(object obj)
		{
			FriendsListView friendsListView = obj as FriendsListView;
			return friendsListView != null && this.HasDeviceInfo == friendsListView.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(friendsListView.DeviceInfo)) && this.HasCurrentScene == friendsListView.HasCurrentScene && (!this.HasCurrentScene || this.CurrentScene.Equals(friendsListView.CurrentScene));
		}

		// Token: 0x0600CA18 RID: 51736 RVA: 0x003C89E8 File Offset: 0x003C6BE8
		public void Deserialize(Stream stream)
		{
			FriendsListView.Deserialize(stream, this);
		}

		// Token: 0x0600CA19 RID: 51737 RVA: 0x003C89F2 File Offset: 0x003C6BF2
		public static FriendsListView Deserialize(Stream stream, FriendsListView instance)
		{
			return FriendsListView.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CA1A RID: 51738 RVA: 0x003C8A00 File Offset: 0x003C6C00
		public static FriendsListView DeserializeLengthDelimited(Stream stream)
		{
			FriendsListView friendsListView = new FriendsListView();
			FriendsListView.DeserializeLengthDelimited(stream, friendsListView);
			return friendsListView;
		}

		// Token: 0x0600CA1B RID: 51739 RVA: 0x003C8A1C File Offset: 0x003C6C1C
		public static FriendsListView DeserializeLengthDelimited(Stream stream, FriendsListView instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FriendsListView.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CA1C RID: 51740 RVA: 0x003C8A44 File Offset: 0x003C6C44
		public static FriendsListView Deserialize(Stream stream, FriendsListView instance, long limit)
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
						instance.CurrentScene = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600CA1D RID: 51741 RVA: 0x003C8AF6 File Offset: 0x003C6CF6
		public void Serialize(Stream stream)
		{
			FriendsListView.Serialize(stream, this);
		}

		// Token: 0x0600CA1E RID: 51742 RVA: 0x003C8B00 File Offset: 0x003C6D00
		public static void Serialize(Stream stream, FriendsListView instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasCurrentScene)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CurrentScene));
			}
		}

		// Token: 0x0600CA1F RID: 51743 RVA: 0x003C8B60 File Offset: 0x003C6D60
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasCurrentScene)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.CurrentScene);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x04009F9F RID: 40863
		public bool HasDeviceInfo;

		// Token: 0x04009FA0 RID: 40864
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009FA1 RID: 40865
		public bool HasCurrentScene;

		// Token: 0x04009FA2 RID: 40866
		private string _CurrentScene;
	}
}
