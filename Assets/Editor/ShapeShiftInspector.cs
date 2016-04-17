using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ShapeShiftingPart))]
public class ShapeShiftInspector : Editor {

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();

		if (GUILayout.Button ("Set shape")) {
			(target as ShapeShiftingPart).SetCurrent ();
		}

		if (GUILayout.Button ("Record anchors")) {
			(target as ShapeShiftingPart).RecordAnchors ();	
		}

		if (GUILayout.Button ("Update from  local")) {
			(target as ShapeShiftingPart).SetFromLocal ();
		}
	}
}
