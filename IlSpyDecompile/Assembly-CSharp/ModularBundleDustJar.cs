using UnityEngine;

public class ModularBundleDustJar : MonoBehaviour
{
	public ModularBundleText HeaderText;

	public UberText AmountText;

	public void KeepHeaderTextStraight()
	{
		Quaternion localRotation = base.transform.parent.localRotation;
		HeaderText.transform.localRotation = Quaternion.Euler(90f, 360f - localRotation.eulerAngles.y, 0f);
	}
}
