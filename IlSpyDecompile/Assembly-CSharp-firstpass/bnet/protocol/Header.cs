using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol
{
	public class Header : IProtoBuf
	{
		public bool HasMethodId;

		private uint _MethodId;

		public bool HasObjectId;

		private ulong _ObjectId;

		public bool HasSize;

		private uint _Size;

		public bool HasStatus;

		private uint _Status;

		private List<ErrorInfo> _Error = new List<ErrorInfo>();

		public bool HasTimeout;

		private ulong _Timeout;

		public bool HasIsResponse;

		private bool _IsResponse;

		private List<ProcessId> _ForwardTargets = new List<ProcessId>();

		public bool HasServiceHash;

		private uint _ServiceHash;

		public bool HasClientId;

		private string _ClientId;

		private List<FanoutTarget> _FanoutTarget = new List<FanoutTarget>();

		private List<string> _ClientIdFanoutTarget = new List<string>();

		public uint ServiceId { get; set; }

		public uint MethodId
		{
			get
			{
				return _MethodId;
			}
			set
			{
				_MethodId = value;
				HasMethodId = true;
			}
		}

		public uint Token { get; set; }

		public ulong ObjectId
		{
			get
			{
				return _ObjectId;
			}
			set
			{
				_ObjectId = value;
				HasObjectId = true;
			}
		}

		public uint Size
		{
			get
			{
				return _Size;
			}
			set
			{
				_Size = value;
				HasSize = true;
			}
		}

		public uint Status
		{
			get
			{
				return _Status;
			}
			set
			{
				_Status = value;
				HasStatus = true;
			}
		}

		public List<ErrorInfo> Error
		{
			get
			{
				return _Error;
			}
			set
			{
				_Error = value;
			}
		}

		public List<ErrorInfo> ErrorList => _Error;

		public int ErrorCount => _Error.Count;

		public ulong Timeout
		{
			get
			{
				return _Timeout;
			}
			set
			{
				_Timeout = value;
				HasTimeout = true;
			}
		}

		public bool IsResponse
		{
			get
			{
				return _IsResponse;
			}
			set
			{
				_IsResponse = value;
				HasIsResponse = true;
			}
		}

		public List<ProcessId> ForwardTargets
		{
			get
			{
				return _ForwardTargets;
			}
			set
			{
				_ForwardTargets = value;
			}
		}

		public List<ProcessId> ForwardTargetsList => _ForwardTargets;

		public int ForwardTargetsCount => _ForwardTargets.Count;

		public uint ServiceHash
		{
			get
			{
				return _ServiceHash;
			}
			set
			{
				_ServiceHash = value;
				HasServiceHash = true;
			}
		}

		public string ClientId
		{
			get
			{
				return _ClientId;
			}
			set
			{
				_ClientId = value;
				HasClientId = value != null;
			}
		}

		public List<FanoutTarget> FanoutTarget
		{
			get
			{
				return _FanoutTarget;
			}
			set
			{
				_FanoutTarget = value;
			}
		}

		public List<FanoutTarget> FanoutTargetList => _FanoutTarget;

		public int FanoutTargetCount => _FanoutTarget.Count;

		public List<string> ClientIdFanoutTarget
		{
			get
			{
				return _ClientIdFanoutTarget;
			}
			set
			{
				_ClientIdFanoutTarget = value;
			}
		}

		public List<string> ClientIdFanoutTargetList => _ClientIdFanoutTarget;

		public int ClientIdFanoutTargetCount => _ClientIdFanoutTarget.Count;

		public bool IsInitialized => true;

		public void SetServiceId(uint val)
		{
			ServiceId = val;
		}

		public void SetMethodId(uint val)
		{
			MethodId = val;
		}

		public void SetToken(uint val)
		{
			Token = val;
		}

		public void SetObjectId(ulong val)
		{
			ObjectId = val;
		}

		public void SetSize(uint val)
		{
			Size = val;
		}

		public void SetStatus(uint val)
		{
			Status = val;
		}

		public void AddError(ErrorInfo val)
		{
			_Error.Add(val);
		}

		public void ClearError()
		{
			_Error.Clear();
		}

		public void SetError(List<ErrorInfo> val)
		{
			Error = val;
		}

		public void SetTimeout(ulong val)
		{
			Timeout = val;
		}

		public void SetIsResponse(bool val)
		{
			IsResponse = val;
		}

		public void AddForwardTargets(ProcessId val)
		{
			_ForwardTargets.Add(val);
		}

		public void ClearForwardTargets()
		{
			_ForwardTargets.Clear();
		}

		public void SetForwardTargets(List<ProcessId> val)
		{
			ForwardTargets = val;
		}

		public void SetServiceHash(uint val)
		{
			ServiceHash = val;
		}

		public void SetClientId(string val)
		{
			ClientId = val;
		}

		public void AddFanoutTarget(FanoutTarget val)
		{
			_FanoutTarget.Add(val);
		}

		public void ClearFanoutTarget()
		{
			_FanoutTarget.Clear();
		}

		public void SetFanoutTarget(List<FanoutTarget> val)
		{
			FanoutTarget = val;
		}

		public void AddClientIdFanoutTarget(string val)
		{
			_ClientIdFanoutTarget.Add(val);
		}

		public void ClearClientIdFanoutTarget()
		{
			_ClientIdFanoutTarget.Clear();
		}

		public void SetClientIdFanoutTarget(List<string> val)
		{
			ClientIdFanoutTarget = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= ServiceId.GetHashCode();
			if (HasMethodId)
			{
				hashCode ^= MethodId.GetHashCode();
			}
			hashCode ^= Token.GetHashCode();
			if (HasObjectId)
			{
				hashCode ^= ObjectId.GetHashCode();
			}
			if (HasSize)
			{
				hashCode ^= Size.GetHashCode();
			}
			if (HasStatus)
			{
				hashCode ^= Status.GetHashCode();
			}
			foreach (ErrorInfo item in Error)
			{
				hashCode ^= item.GetHashCode();
			}
			if (HasTimeout)
			{
				hashCode ^= Timeout.GetHashCode();
			}
			if (HasIsResponse)
			{
				hashCode ^= IsResponse.GetHashCode();
			}
			foreach (ProcessId forwardTarget in ForwardTargets)
			{
				hashCode ^= forwardTarget.GetHashCode();
			}
			if (HasServiceHash)
			{
				hashCode ^= ServiceHash.GetHashCode();
			}
			if (HasClientId)
			{
				hashCode ^= ClientId.GetHashCode();
			}
			foreach (FanoutTarget item2 in FanoutTarget)
			{
				hashCode ^= item2.GetHashCode();
			}
			foreach (string item3 in ClientIdFanoutTarget)
			{
				hashCode ^= item3.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			Header header = obj as Header;
			if (header == null)
			{
				return false;
			}
			if (!ServiceId.Equals(header.ServiceId))
			{
				return false;
			}
			if (HasMethodId != header.HasMethodId || (HasMethodId && !MethodId.Equals(header.MethodId)))
			{
				return false;
			}
			if (!Token.Equals(header.Token))
			{
				return false;
			}
			if (HasObjectId != header.HasObjectId || (HasObjectId && !ObjectId.Equals(header.ObjectId)))
			{
				return false;
			}
			if (HasSize != header.HasSize || (HasSize && !Size.Equals(header.Size)))
			{
				return false;
			}
			if (HasStatus != header.HasStatus || (HasStatus && !Status.Equals(header.Status)))
			{
				return false;
			}
			if (Error.Count != header.Error.Count)
			{
				return false;
			}
			for (int i = 0; i < Error.Count; i++)
			{
				if (!Error[i].Equals(header.Error[i]))
				{
					return false;
				}
			}
			if (HasTimeout != header.HasTimeout || (HasTimeout && !Timeout.Equals(header.Timeout)))
			{
				return false;
			}
			if (HasIsResponse != header.HasIsResponse || (HasIsResponse && !IsResponse.Equals(header.IsResponse)))
			{
				return false;
			}
			if (ForwardTargets.Count != header.ForwardTargets.Count)
			{
				return false;
			}
			for (int j = 0; j < ForwardTargets.Count; j++)
			{
				if (!ForwardTargets[j].Equals(header.ForwardTargets[j]))
				{
					return false;
				}
			}
			if (HasServiceHash != header.HasServiceHash || (HasServiceHash && !ServiceHash.Equals(header.ServiceHash)))
			{
				return false;
			}
			if (HasClientId != header.HasClientId || (HasClientId && !ClientId.Equals(header.ClientId)))
			{
				return false;
			}
			if (FanoutTarget.Count != header.FanoutTarget.Count)
			{
				return false;
			}
			for (int k = 0; k < FanoutTarget.Count; k++)
			{
				if (!FanoutTarget[k].Equals(header.FanoutTarget[k]))
				{
					return false;
				}
			}
			if (ClientIdFanoutTarget.Count != header.ClientIdFanoutTarget.Count)
			{
				return false;
			}
			for (int l = 0; l < ClientIdFanoutTarget.Count; l++)
			{
				if (!ClientIdFanoutTarget[l].Equals(header.ClientIdFanoutTarget[l]))
				{
					return false;
				}
			}
			return true;
		}

		public static Header ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Header>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Header Deserialize(Stream stream, Header instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Header DeserializeLengthDelimited(Stream stream)
		{
			Header header = new Header();
			DeserializeLengthDelimited(stream, header);
			return header;
		}

		public static Header DeserializeLengthDelimited(Stream stream, Header instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Header Deserialize(Stream stream, Header instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.ObjectId = 0uL;
			instance.Size = 0u;
			instance.Status = 0u;
			if (instance.Error == null)
			{
				instance.Error = new List<ErrorInfo>();
			}
			if (instance.ForwardTargets == null)
			{
				instance.ForwardTargets = new List<ProcessId>();
			}
			if (instance.FanoutTarget == null)
			{
				instance.FanoutTarget = new List<FanoutTarget>();
			}
			if (instance.ClientIdFanoutTarget == null)
			{
				instance.ClientIdFanoutTarget = new List<string>();
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
				case 8:
					instance.ServiceId = ProtocolParser.ReadUInt32(stream);
					continue;
				case 16:
					instance.MethodId = ProtocolParser.ReadUInt32(stream);
					continue;
				case 24:
					instance.Token = ProtocolParser.ReadUInt32(stream);
					continue;
				case 32:
					instance.ObjectId = ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.Size = ProtocolParser.ReadUInt32(stream);
					continue;
				case 48:
					instance.Status = ProtocolParser.ReadUInt32(stream);
					continue;
				case 58:
					instance.Error.Add(ErrorInfo.DeserializeLengthDelimited(stream));
					continue;
				case 64:
					instance.Timeout = ProtocolParser.ReadUInt64(stream);
					continue;
				case 72:
					instance.IsResponse = ProtocolParser.ReadBool(stream);
					continue;
				case 82:
					instance.ForwardTargets.Add(ProcessId.DeserializeLengthDelimited(stream));
					continue;
				case 93:
					instance.ServiceHash = binaryReader.ReadUInt32();
					continue;
				case 106:
					instance.ClientId = ProtocolParser.ReadString(stream);
					continue;
				case 114:
					instance.FanoutTarget.Add(bnet.protocol.FanoutTarget.DeserializeLengthDelimited(stream));
					continue;
				case 122:
					instance.ClientIdFanoutTarget.Add(ProtocolParser.ReadString(stream));
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

		public static void Serialize(Stream stream, Header instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.ServiceId);
			if (instance.HasMethodId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.MethodId);
			}
			stream.WriteByte(24);
			ProtocolParser.WriteUInt32(stream, instance.Token);
			if (instance.HasObjectId)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			}
			if (instance.HasSize)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt32(stream, instance.Size);
			}
			if (instance.HasStatus)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt32(stream, instance.Status);
			}
			if (instance.Error.Count > 0)
			{
				foreach (ErrorInfo item in instance.Error)
				{
					stream.WriteByte(58);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					ErrorInfo.Serialize(stream, item);
				}
			}
			if (instance.HasTimeout)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, instance.Timeout);
			}
			if (instance.HasIsResponse)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteBool(stream, instance.IsResponse);
			}
			if (instance.ForwardTargets.Count > 0)
			{
				foreach (ProcessId forwardTarget in instance.ForwardTargets)
				{
					stream.WriteByte(82);
					ProtocolParser.WriteUInt32(stream, forwardTarget.GetSerializedSize());
					ProcessId.Serialize(stream, forwardTarget);
				}
			}
			if (instance.HasServiceHash)
			{
				stream.WriteByte(93);
				binaryWriter.Write(instance.ServiceHash);
			}
			if (instance.HasClientId)
			{
				stream.WriteByte(106);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientId));
			}
			if (instance.FanoutTarget.Count > 0)
			{
				foreach (FanoutTarget item2 in instance.FanoutTarget)
				{
					stream.WriteByte(114);
					ProtocolParser.WriteUInt32(stream, item2.GetSerializedSize());
					bnet.protocol.FanoutTarget.Serialize(stream, item2);
				}
			}
			if (instance.ClientIdFanoutTarget.Count <= 0)
			{
				return;
			}
			foreach (string item3 in instance.ClientIdFanoutTarget)
			{
				stream.WriteByte(122);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(item3));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt32(ServiceId);
			if (HasMethodId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(MethodId);
			}
			num += ProtocolParser.SizeOfUInt32(Token);
			if (HasObjectId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(ObjectId);
			}
			if (HasSize)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Size);
			}
			if (HasStatus)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Status);
			}
			if (Error.Count > 0)
			{
				foreach (ErrorInfo item in Error)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasTimeout)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(Timeout);
			}
			if (HasIsResponse)
			{
				num++;
				num++;
			}
			if (ForwardTargets.Count > 0)
			{
				foreach (ProcessId forwardTarget in ForwardTargets)
				{
					num++;
					uint serializedSize2 = forwardTarget.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (HasServiceHash)
			{
				num++;
				num += 4;
			}
			if (HasClientId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ClientId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (FanoutTarget.Count > 0)
			{
				foreach (FanoutTarget item2 in FanoutTarget)
				{
					num++;
					uint serializedSize3 = item2.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (ClientIdFanoutTarget.Count > 0)
			{
				foreach (string item3 in ClientIdFanoutTarget)
				{
					num++;
					uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(item3);
					num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
				}
			}
			return num + 2;
		}
	}
}
