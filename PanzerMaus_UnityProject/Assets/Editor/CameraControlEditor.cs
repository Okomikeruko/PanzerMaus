using UnityEditor;

[CustomEditor(typeof(CameraControl))]
public class CameraControlEditor : Editor {

	private ControlMode controlMode;

	public override void OnInspectorGUI(){
		CameraControl cameraControl = (CameraControl)target;

		controlMode = (ControlMode)EditorGUILayout.EnumPopup("Control Mode", controlMode); 

		switch (controlMode){
		case ControlMode.mouse: 
			EditorGUILayout.LabelField("Mouse Controls");
			cameraControl.wheel = EditorGUILayout.FloatField("  Wheel Sensitivity", cameraControl.wheel);
			cameraControl.mouseDragSpeed = EditorGUILayout.FloatField("  Pan Speed", cameraControl.mouseDragSpeed);
			break;
		case ControlMode.touch:
			EditorGUILayout.LabelField("Touch Screen Controls");
			cameraControl.pinch = EditorGUILayout.FloatField("  Pinch Sensitivity", cameraControl.pinch);
			cameraControl.fingerDragSpeed = EditorGUILayout.FloatField("  Pan Speed", cameraControl.fingerDragSpeed);
			break;
		default:
			break;
		}

		EditorGUILayout.Space();
		EditorGUILayout.LabelField ("Camera Field of View Limits");
		EditorGUILayout.LabelField ("Minimum:", cameraControl.minFoV.ToString()); 
		EditorGUILayout.LabelField ("Maximum:", cameraControl.maxFoV.ToString());
		EditorGUILayout.MinMaxSlider (ref cameraControl.minFoV, ref cameraControl.maxFoV, 0.0f, 100.0f);

		EditorGUILayout.Space();
		EditorGUILayout.LabelField ("Camera Position Limits");
		cameraControl.min = EditorGUILayout.Vector2Field("Bottom Left", cameraControl.min);
		cameraControl.max = EditorGUILayout.Vector2Field("Top Right", cameraControl.max);
	}
}

public enum ControlMode{
	mouse,
	touch,
}