using UnityEngine;

public class Tooltip : MonoBehaviour
{
	public TextMesh headlineText;

	public TextMesh descriptionText;

	public void UpdateText(string headline, string description)
	{
		headlineText.text = headline;
		descriptionText.text = description;
	}
}
