using UnityEditor;
using UnityEngine;

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
		EditorGUILayout.LabelField (Camera.main.isOrthoGraphic ? "Camera Orthographic Size Limits" : "Camera Field of View Limits");
		EditorGUILayout.LabelField ("  Minimum:", cameraControl.minFoV.ToString()); 
		EditorGUILayout.LabelField ("  Maximum:", cameraControl.maxFoV.ToString());
		EditorGUILayout.MinMaxSlider (ref cameraControl.minFoV, ref cameraControl.maxFoV, 0.1f, 100.0f);

		EditorGUILayout.Space();
		EditorGUILayout.LabelField ("Camera Position Limits");
		EditorGUILayout.BeginHorizontal();
		for (int i = 0; i < cameraControl.limit.Length; i++){
			EditorGUILayout.BeginVertical();
			Limit limit = (Limit)i;
			EditorGUILayout.LabelField(limit.ToString(), GUILayout.MinWidth (25) );
			EditorGUILayout.LabelField(cameraControl.limit[i].ToString(), GUILayout.MinWidth (25));
			if (GUILayout.Button ("Set Limit", GUILayout.MinWidth(25))){
				cameraControl.SetLimit((Limit)i); 
			}
			EditorGUILayout.EndVertical();
		}
		EditorGUILayout.EndHorizontal();

		cameraControl.playerCount = EditorGUILayout.IntField("Player Count", cameraControl.playerCount);
		if(cameraControl.playerCount != cameraControl.tanks.Length){
			cameraControl.tanks = new Transform[cameraControl.playerCount];
		}
		if (cameraControl.playerCount > 0){
			for ( int i = 0; i < cameraControl.tanks.Length; i++){
				cameraControl.tanks[i] = EditorGUILayout.ObjectField ("Tank " + (i+1).ToString(), cameraControl.tanks[i], typeof( Transform ), true) as Transform;
			}
		}
	}
}

public enum ControlMode{
	mouse,
	touch,
}