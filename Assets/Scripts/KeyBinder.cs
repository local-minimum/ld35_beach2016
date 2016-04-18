using UnityEngine;
using UnityEngine.UI;

public class KeyBinder : MonoBehaviour {

	static string pattern = "Input.Key.";
	bool editing = false;
	[SerializeField] int key = -1;
	Text txt;

	float cursorFreq = 0.9f;

	public static bool validKey(int inputKey) {
		return inputKey > 0 && inputKey < 6;
	}

	public static KeyCode GetKey(int inputKey) {
		if (!validKey (inputKey))
			return KeyCode.None;
		
		string keyName = PlayerPrefs.GetString (pattern + inputKey, "Alpha" + inputKey);
		return (KeyCode)System.Enum.Parse(typeof(KeyCode), keyName);
	}

	public static void SetKey(int inputKey, KeyCode code) {
		PlayerPrefs.SetString (pattern + inputKey, code.ToString());
	}

	public static bool ValidKeyCode(KeyCode code) {
		
		return !(code.ToString ().StartsWith ("Mouse") || code == KeyCode.Escape);
	}

	public static KeyCode FirstButtonDown {
		get {
			foreach (KeyCode k in System.Enum.GetValues(typeof(KeyCode))) {
				if (Input.GetKeyDown (k))
					return k;
			}
			return KeyCode.None;
		}
	}

	public static string GetName(KeyCode keyCode) {
		var text = keyCode.ToString ();
		if (text.StartsWith ("Alpha"))
			text = text.Substring (5);
		return text;
	}

	// Use this for initialization
	void Start () {
		txt = GetComponentInChildren<Text> ();
		txt.text = GetName(GetKey (key));

	}

	public void Click() {
		if (validKey (key)) {			
			if (!editing) {
				editing = true;
				txt.text = "";
			}
		} 
	}

	public void Update() {
		if (editing) {
			
			if (Input.anyKeyDown) {
				var keyCode = FirstButtonDown;
				if (ValidKeyCode (keyCode)) {
					editing = false;
					SetKey (key, keyCode);
					txt.text = GetName (keyCode);
				} else {
					editing = false;
					txt.text = GetName (GetKey (key));
				}
			} else {
				txt.text = (Time.time % cursorFreq) > cursorFreq / 2f ? "_" : "";
			}
		}
	}

}
