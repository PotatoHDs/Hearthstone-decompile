using System;
using System.Runtime.InteropServices;
using System.Text;

// Token: 0x020009C5 RID: 2501
public static class MemUtils
{
	// Token: 0x0600889C RID: 34972 RVA: 0x002BFF6F File Offset: 0x002BE16F
	public static void FreePtr(IntPtr ptr)
	{
		if (ptr == IntPtr.Zero)
		{
			return;
		}
		Marshal.FreeHGlobal(ptr);
	}

	// Token: 0x0600889D RID: 34973 RVA: 0x002BFF88 File Offset: 0x002BE188
	public static byte[] PtrToBytes(IntPtr ptr, int size)
	{
		if (ptr == IntPtr.Zero)
		{
			return null;
		}
		if (size == 0)
		{
			return null;
		}
		byte[] array = new byte[size];
		Marshal.Copy(ptr, array, 0, size);
		return array;
	}

	// Token: 0x0600889E RID: 34974 RVA: 0x002BFFBA File Offset: 0x002BE1BA
	public static IntPtr PtrFromBytes(byte[] bytes)
	{
		return MemUtils.PtrFromBytes(bytes, 0);
	}

	// Token: 0x0600889F RID: 34975 RVA: 0x002BFFC4 File Offset: 0x002BE1C4
	public static IntPtr PtrFromBytes(byte[] bytes, int offset)
	{
		if (bytes == null)
		{
			return IntPtr.Zero;
		}
		int len = bytes.Length - offset;
		return MemUtils.PtrFromBytes(bytes, offset, len);
	}

	// Token: 0x060088A0 RID: 34976 RVA: 0x002BFFE8 File Offset: 0x002BE1E8
	public static IntPtr PtrFromBytes(byte[] bytes, int offset, int len)
	{
		if (bytes == null)
		{
			return IntPtr.Zero;
		}
		if (len <= 0)
		{
			return IntPtr.Zero;
		}
		IntPtr intPtr = Marshal.AllocHGlobal(len);
		Marshal.Copy(bytes, offset, intPtr, len);
		return intPtr;
	}

	// Token: 0x060088A1 RID: 34977 RVA: 0x002C001C File Offset: 0x002BE21C
	public static byte[] StructToBytes<T>(T t)
	{
		int num = Marshal.SizeOf(typeof(T));
		byte[] array = new byte[num];
		IntPtr intPtr = Marshal.AllocHGlobal(num);
		Marshal.StructureToPtr<T>(t, intPtr, true);
		Marshal.Copy(intPtr, array, 0, num);
		Marshal.FreeHGlobal(intPtr);
		return array;
	}

	// Token: 0x060088A2 RID: 34978 RVA: 0x002C005F File Offset: 0x002BE25F
	public static T StructFromBytes<T>(byte[] bytes)
	{
		return MemUtils.StructFromBytes<T>(bytes, 0);
	}

	// Token: 0x060088A3 RID: 34979 RVA: 0x002C0068 File Offset: 0x002BE268
	public static T StructFromBytes<T>(byte[] bytes, int offset)
	{
		Type typeFromHandle = typeof(T);
		int num = Marshal.SizeOf(typeFromHandle);
		if (bytes == null)
		{
			return default(T);
		}
		if (bytes.Length - offset < num)
		{
			return default(T);
		}
		IntPtr intPtr = Marshal.AllocHGlobal(num);
		Marshal.Copy(bytes, offset, intPtr, num);
		T result = (T)((object)Marshal.PtrToStructure(intPtr, typeFromHandle));
		Marshal.FreeHGlobal(intPtr);
		return result;
	}

	// Token: 0x060088A4 RID: 34980 RVA: 0x002C00C8 File Offset: 0x002BE2C8
	public static IntPtr Utf8PtrFromString(string managedString)
	{
		if (managedString == null)
		{
			return IntPtr.Zero;
		}
		int num = 1 + Encoding.UTF8.GetByteCount(managedString);
		byte[] array = new byte[num];
		Encoding.UTF8.GetBytes(managedString, 0, managedString.Length, array, 0);
		IntPtr intPtr = Marshal.AllocHGlobal(num);
		Marshal.Copy(array, 0, intPtr, num);
		return intPtr;
	}

	// Token: 0x060088A5 RID: 34981 RVA: 0x002C011C File Offset: 0x002BE31C
	public static string StringFromUtf8Ptr(IntPtr ptr)
	{
		int num;
		return MemUtils.StringFromUtf8Ptr(ptr, out num);
	}

	// Token: 0x060088A6 RID: 34982 RVA: 0x002C0134 File Offset: 0x002BE334
	public static string StringFromUtf8Ptr(IntPtr ptr, out int len)
	{
		len = 0;
		if (ptr == IntPtr.Zero)
		{
			return null;
		}
		len = MemUtils.StringPtrByteLen(ptr);
		if (len == 0)
		{
			return null;
		}
		byte[] array = new byte[len];
		Marshal.Copy(ptr, array, 0, len);
		return Encoding.UTF8.GetString(array);
	}

	// Token: 0x060088A7 RID: 34983 RVA: 0x002C0180 File Offset: 0x002BE380
	public static int StringPtrByteLen(IntPtr ptr)
	{
		if (ptr == IntPtr.Zero)
		{
			return 0;
		}
		int num = 0;
		while (Marshal.ReadByte(ptr, num) != 0)
		{
			num++;
		}
		return num;
	}
}
