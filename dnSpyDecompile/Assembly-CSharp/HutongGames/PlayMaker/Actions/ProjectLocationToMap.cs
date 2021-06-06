using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D3B RID: 3387
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Projects the location found with Get Location Info to a 2d map using common projections.")]
	public class ProjectLocationToMap : FsmStateAction
	{
		// Token: 0x0600A319 RID: 41753 RVA: 0x0033E59C File Offset: 0x0033C79C
		public override void Reset()
		{
			this.GPSLocation = new FsmVector3
			{
				UseVariable = true
			};
			this.mapProjection = ProjectLocationToMap.MapProjection.EquidistantCylindrical;
			this.minLongitude = -180f;
			this.maxLongitude = 180f;
			this.minLatitude = -90f;
			this.maxLatitude = 90f;
			this.minX = 0f;
			this.minY = 0f;
			this.width = 1f;
			this.height = 1f;
			this.normalized = true;
			this.projectedX = null;
			this.projectedY = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A31A RID: 41754 RVA: 0x0033E663 File Offset: 0x0033C863
		public override void OnEnter()
		{
			if (this.GPSLocation.IsNone)
			{
				base.Finish();
				return;
			}
			this.DoProjectGPSLocation();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A31B RID: 41755 RVA: 0x0033E68D File Offset: 0x0033C88D
		public override void OnUpdate()
		{
			this.DoProjectGPSLocation();
		}

		// Token: 0x0600A31C RID: 41756 RVA: 0x0033E698 File Offset: 0x0033C898
		private void DoProjectGPSLocation()
		{
			this.x = Mathf.Clamp(this.GPSLocation.Value.x, this.minLongitude.Value, this.maxLongitude.Value);
			this.y = Mathf.Clamp(this.GPSLocation.Value.y, this.minLatitude.Value, this.maxLatitude.Value);
			ProjectLocationToMap.MapProjection mapProjection = this.mapProjection;
			if (mapProjection != ProjectLocationToMap.MapProjection.EquidistantCylindrical)
			{
				if (mapProjection == ProjectLocationToMap.MapProjection.Mercator)
				{
					this.DoMercatorProjection();
				}
			}
			else
			{
				this.DoEquidistantCylindrical();
			}
			this.x *= this.width.Value;
			this.y *= this.height.Value;
			this.projectedX.Value = (this.normalized.Value ? (this.minX.Value + this.x) : (this.minX.Value + this.x * (float)Screen.width));
			this.projectedY.Value = (this.normalized.Value ? (this.minY.Value + this.y) : (this.minY.Value + this.y * (float)Screen.height));
		}

		// Token: 0x0600A31D RID: 41757 RVA: 0x0033E7E0 File Offset: 0x0033C9E0
		private void DoEquidistantCylindrical()
		{
			this.x = (this.x - this.minLongitude.Value) / (this.maxLongitude.Value - this.minLongitude.Value);
			this.y = (this.y - this.minLatitude.Value) / (this.maxLatitude.Value - this.minLatitude.Value);
		}

		// Token: 0x0600A31E RID: 41758 RVA: 0x0033E850 File Offset: 0x0033CA50
		private void DoMercatorProjection()
		{
			this.x = (this.x - this.minLongitude.Value) / (this.maxLongitude.Value - this.minLongitude.Value);
			float num = ProjectLocationToMap.LatitudeToMercator(this.minLatitude.Value);
			float num2 = ProjectLocationToMap.LatitudeToMercator(this.maxLatitude.Value);
			this.y = (ProjectLocationToMap.LatitudeToMercator(this.GPSLocation.Value.y) - num) / (num2 - num);
		}

		// Token: 0x0600A31F RID: 41759 RVA: 0x0033E8D0 File Offset: 0x0033CAD0
		private static float LatitudeToMercator(float latitudeInDegrees)
		{
			float num = Mathf.Clamp(latitudeInDegrees, -85f, 85f);
			num = 0.017453292f * num;
			return Mathf.Log(Mathf.Tan(num / 2f + 0.7853982f));
		}

		// Token: 0x04008971 RID: 35185
		[Tooltip("Location vector in degrees longitude and latitude. Typically returned by the Get Location Info action.")]
		public FsmVector3 GPSLocation;

		// Token: 0x04008972 RID: 35186
		[Tooltip("The projection used by the map.")]
		public ProjectLocationToMap.MapProjection mapProjection;

		// Token: 0x04008973 RID: 35187
		[ActionSection("Map Region")]
		[HasFloatSlider(-180f, 180f)]
		public FsmFloat minLongitude;

		// Token: 0x04008974 RID: 35188
		[HasFloatSlider(-180f, 180f)]
		public FsmFloat maxLongitude;

		// Token: 0x04008975 RID: 35189
		[HasFloatSlider(-90f, 90f)]
		public FsmFloat minLatitude;

		// Token: 0x04008976 RID: 35190
		[HasFloatSlider(-90f, 90f)]
		public FsmFloat maxLatitude;

		// Token: 0x04008977 RID: 35191
		[ActionSection("Screen Region")]
		public FsmFloat minX;

		// Token: 0x04008978 RID: 35192
		public FsmFloat minY;

		// Token: 0x04008979 RID: 35193
		public FsmFloat width;

		// Token: 0x0400897A RID: 35194
		public FsmFloat height;

		// Token: 0x0400897B RID: 35195
		[ActionSection("Projection")]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the projected X coordinate in a Float Variable. Use this to display a marker on the map.")]
		public FsmFloat projectedX;

		// Token: 0x0400897C RID: 35196
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the projected Y coordinate in a Float Variable. Use this to display a marker on the map.")]
		public FsmFloat projectedY;

		// Token: 0x0400897D RID: 35197
		[Tooltip("If true all coordinates in this action are normalized (0-1); otherwise coordinates are in pixels.")]
		public FsmBool normalized;

		// Token: 0x0400897E RID: 35198
		public bool everyFrame;

		// Token: 0x0400897F RID: 35199
		private float x;

		// Token: 0x04008980 RID: 35200
		private float y;

		// Token: 0x0200279F RID: 10143
		public enum MapProjection
		{
			// Token: 0x0400F4EA RID: 62698
			EquidistantCylindrical,
			// Token: 0x0400F4EB RID: 62699
			Mercator
		}
	}
}
