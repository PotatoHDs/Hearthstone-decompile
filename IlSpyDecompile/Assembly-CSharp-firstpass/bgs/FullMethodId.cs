namespace bgs
{
	public struct FullMethodId
	{
		public uint ServiceHash { get; private set; }

		public uint MethodId { get; private set; }

		public FullMethodId(uint serviceHash, uint methodId)
		{
			ServiceHash = serviceHash;
			MethodId = methodId;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is FullMethodId))
			{
				return false;
			}
			FullMethodId fullMethodId = (FullMethodId)obj;
			if (ServiceHash == fullMethodId.ServiceHash)
			{
				return MethodId == fullMethodId.MethodId;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return ServiceHash.GetHashCode() ^ MethodId.GetHashCode();
		}
	}
}
