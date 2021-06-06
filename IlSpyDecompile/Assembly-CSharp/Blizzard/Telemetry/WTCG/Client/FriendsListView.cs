using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class FriendsListView : IProtoBuf
	{
		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasCurrentScene;

		private string _CurrentScene;

		public DeviceInfo DeviceInfo
		{
			get
			{
				return _DeviceInfo;
			}
			set
			{
				_DeviceInfo = value;
				HasDeviceInfo = value != null;
			}
		}

		public string CurrentScene
		{
			get
			{
				return _CurrentScene;
			}
			set
			{
				_CurrentScene = value;
				HasCurrentScene = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasDeviceInfo)
			{
				num ^= DeviceInfo.GetHashCode();
			}
			if (HasCurrentScene)
			{
				num ^= CurrentScene.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FriendsListView friendsListView = obj as FriendsListView;
			if (friendsListView == null)
			{
				return false;
			}
			if (HasDeviceInfo != friendsListView.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(friendsListView.DeviceInfo)))
			{
				return false;
			}
			if (HasCurrentScene != friendsListView.HasCurrentScene || (HasCurrentScene && !CurrentScene.Equals(friendsListView.CurrentScene)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FriendsListView Deserialize(Stream stream, FriendsListView instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FriendsListView DeserializeLengthDelimited(Stream stream)
		{
			FriendsListView friendsListView = new FriendsListView();
			DeserializeLengthDelimited(stream, friendsListView);
			return friendsListView;
		}

		public static FriendsListView DeserializeLengthDelimited(Stream stream, FriendsListView instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FriendsListView Deserialize(Stream stream, FriendsListView instance, long limit)
		{
			while (true)
			{
				if (limit >= 0 && stream.Position >= limit)
				{
					if (stream.Position == limit)
					{
						break;
					}
					throw new ProtocolBufferException("Read past max limit");
				}
				int num = stream.ReadByte();
				switch (num)
				{
				case -1:
					break;
				case 10:
					if (instance.DeviceInfo == null)
					{
						instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
					}
					continue;
				case 18:
					instance.CurrentScene = ProtocolParser.ReadString(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
					continue;
				}
				}
				if (limit < 0)
				{
					break;
				}
				throw new EndOfStreamException();
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

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

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasDeviceInfo)
			{
				num++;
				uint serializedSize = DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasCurrentScene)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(CurrentScene);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
