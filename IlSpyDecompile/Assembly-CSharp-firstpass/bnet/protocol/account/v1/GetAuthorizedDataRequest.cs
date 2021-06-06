using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.account.v1
{
	public class GetAuthorizedDataRequest : IProtoBuf
	{
		public bool HasEntityId;

		private EntityId _EntityId;

		private List<string> _Tag = new List<string>();

		public bool HasPrivilegedNetwork;

		private bool _PrivilegedNetwork;

		public EntityId EntityId
		{
			get
			{
				return _EntityId;
			}
			set
			{
				_EntityId = value;
				HasEntityId = value != null;
			}
		}

		public List<string> Tag
		{
			get
			{
				return _Tag;
			}
			set
			{
				_Tag = value;
			}
		}

		public List<string> TagList => _Tag;

		public int TagCount => _Tag.Count;

		public bool PrivilegedNetwork
		{
			get
			{
				return _PrivilegedNetwork;
			}
			set
			{
				_PrivilegedNetwork = value;
				HasPrivilegedNetwork = true;
			}
		}

		public bool IsInitialized => true;

		public void SetEntityId(EntityId val)
		{
			EntityId = val;
		}

		public void AddTag(string val)
		{
			_Tag.Add(val);
		}

		public void ClearTag()
		{
			_Tag.Clear();
		}

		public void SetTag(List<string> val)
		{
			Tag = val;
		}

		public void SetPrivilegedNetwork(bool val)
		{
			PrivilegedNetwork = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasEntityId)
			{
				num ^= EntityId.GetHashCode();
			}
			foreach (string item in Tag)
			{
				num ^= item.GetHashCode();
			}
			if (HasPrivilegedNetwork)
			{
				num ^= PrivilegedNetwork.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetAuthorizedDataRequest getAuthorizedDataRequest = obj as GetAuthorizedDataRequest;
			if (getAuthorizedDataRequest == null)
			{
				return false;
			}
			if (HasEntityId != getAuthorizedDataRequest.HasEntityId || (HasEntityId && !EntityId.Equals(getAuthorizedDataRequest.EntityId)))
			{
				return false;
			}
			if (Tag.Count != getAuthorizedDataRequest.Tag.Count)
			{
				return false;
			}
			for (int i = 0; i < Tag.Count; i++)
			{
				if (!Tag[i].Equals(getAuthorizedDataRequest.Tag[i]))
				{
					return false;
				}
			}
			if (HasPrivilegedNetwork != getAuthorizedDataRequest.HasPrivilegedNetwork || (HasPrivilegedNetwork && !PrivilegedNetwork.Equals(getAuthorizedDataRequest.PrivilegedNetwork)))
			{
				return false;
			}
			return true;
		}

		public static GetAuthorizedDataRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetAuthorizedDataRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetAuthorizedDataRequest Deserialize(Stream stream, GetAuthorizedDataRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetAuthorizedDataRequest DeserializeLengthDelimited(Stream stream)
		{
			GetAuthorizedDataRequest getAuthorizedDataRequest = new GetAuthorizedDataRequest();
			DeserializeLengthDelimited(stream, getAuthorizedDataRequest);
			return getAuthorizedDataRequest;
		}

		public static GetAuthorizedDataRequest DeserializeLengthDelimited(Stream stream, GetAuthorizedDataRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetAuthorizedDataRequest Deserialize(Stream stream, GetAuthorizedDataRequest instance, long limit)
		{
			if (instance.Tag == null)
			{
				instance.Tag = new List<string>();
			}
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
					if (instance.EntityId == null)
					{
						instance.EntityId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.EntityId);
					}
					continue;
				case 18:
					instance.Tag.Add(ProtocolParser.ReadString(stream));
					continue;
				case 24:
					instance.PrivilegedNetwork = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, GetAuthorizedDataRequest instance)
		{
			if (instance.HasEntityId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
				EntityId.Serialize(stream, instance.EntityId);
			}
			if (instance.Tag.Count > 0)
			{
				foreach (string item in instance.Tag)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(item));
				}
			}
			if (instance.HasPrivilegedNetwork)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.PrivilegedNetwork);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasEntityId)
			{
				num++;
				uint serializedSize = EntityId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (Tag.Count > 0)
			{
				foreach (string item in Tag)
				{
					num++;
					uint byteCount = (uint)Encoding.UTF8.GetByteCount(item);
					num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
				}
			}
			if (HasPrivilegedNetwork)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
