using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x0200123E RID: 4670
	public class blz_commerce_scene_rect_t : IDisposable
	{
		// Token: 0x0600D153 RID: 53587 RVA: 0x003E15E0 File Offset: 0x003DF7E0
		internal blz_commerce_scene_rect_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D154 RID: 53588 RVA: 0x003E15FC File Offset: 0x003DF7FC
		internal static HandleRef getCPtr(blz_commerce_scene_rect_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D155 RID: 53589 RVA: 0x003E1614 File Offset: 0x003DF814
		~blz_commerce_scene_rect_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D156 RID: 53590 RVA: 0x003E1644 File Offset: 0x003DF844
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D157 RID: 53591 RVA: 0x003E1654 File Offset: 0x003DF854
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_scene_rect_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x0600D159 RID: 53593 RVA: 0x003E16DA File Offset: 0x003DF8DA
		// (set) Token: 0x0600D158 RID: 53592 RVA: 0x003E16CC File Offset: 0x003DF8CC
		public int x
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_scene_rect_t_x_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_scene_rect_t_x_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x0600D15B RID: 53595 RVA: 0x003E16F5 File Offset: 0x003DF8F5
		// (set) Token: 0x0600D15A RID: 53594 RVA: 0x003E16E7 File Offset: 0x003DF8E7
		public int y
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_scene_rect_t_y_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_scene_rect_t_y_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x0600D15D RID: 53597 RVA: 0x003E1710 File Offset: 0x003DF910
		// (set) Token: 0x0600D15C RID: 53596 RVA: 0x003E1702 File Offset: 0x003DF902
		public int w
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_scene_rect_t_w_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_scene_rect_t_w_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x0600D15F RID: 53599 RVA: 0x003E172B File Offset: 0x003DF92B
		// (set) Token: 0x0600D15E RID: 53598 RVA: 0x003E171D File Offset: 0x003DF91D
		public int h
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_scene_rect_t_h_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_scene_rect_t_h_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D160 RID: 53600 RVA: 0x003E1738 File Offset: 0x003DF938
		public blz_commerce_scene_rect_t() : this(battlenet_commercePINVOKE.new_blz_commerce_scene_rect_t(), true)
		{
		}

		// Token: 0x0400A2BA RID: 41658
		private HandleRef swigCPtr;

		// Token: 0x0400A2BB RID: 41659
		protected bool swigCMemOwn;
	}
}
