using UnityEngine;
using System.Collections;

public class GUICrosshair : MonoBehaviour {

	public Texture2D crosshair;

	public float centerWidth;
	public float centerHeight;
	public float crosshairSize;
	

	void Start () {
		crosshairSize = Screen.width / 12.5f;
		centerWidth = Screen.width / 2f;
		centerHeight = Screen.height / 2f;
	}

	void OnGUI () {
		GUI.Label (new Rect (centerWidth - crosshairSize / 2, centerHeight - crosshairSize / 2, crosshairSize, crosshairSize ), crosshair);
		}
}
