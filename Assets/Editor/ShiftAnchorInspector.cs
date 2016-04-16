using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShapeShiftingAnchor))]
public class ShiftAnchorInspector : Editor {

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();

		if (GUILayout.Button ("RecordPosition"))
			(target as ShapeShiftingAnchor).RecordPosition ();
	}
}
