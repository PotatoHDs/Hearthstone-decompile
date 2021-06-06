using System;
using System.Net;

namespace bgs
{
	// Token: 0x0200025D RID: 605
	public class UriUtils
	{
		// Token: 0x0600251D RID: 9501 RVA: 0x00083998 File Offset: 0x00081B98
		public static bool GetHostAddressAsIp(string hostName, out string address)
		{
			address = "";
			IPAddress ipaddress;
			if (IPAddress.TryParse(hostName, out ipaddress))
			{
				address = ipaddress.ToString();
				return true;
			}
			return false;
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x000839C4 File Offset: 0x00081BC4
		public static bool GetHostAddressByDns(string hostName, out string address)
		{
			address = "";
			try
			{
				IPAddress[] addressList = Dns.GetHostEntry(hostName).AddressList;
				int num = 0;
				if (num < addressList.Length)
				{
					IPAddress ipaddress = addressList[num];
					address = ipaddress.ToString();
					return true;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return false;
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x00083A14 File Offset: 0x00081C14
		public static bool GetHostAddress(string hostName, out string address)
		{
			if (UriUtils.GetHostAddressAsIp(hostName, out address))
			{
				return true;
			}
			try
			{
				if (UriUtils.GetHostAddressByDns(hostName, out address))
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return false;
		}
	}
}
