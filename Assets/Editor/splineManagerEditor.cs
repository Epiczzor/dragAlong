using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(splineController))]
[ExecuteInEditMode]
public class NewBehaviourScript : Editor {

	private GameObject LineDisplay;
	private GameObject nodeObject;
	private splineController sC;
	private GameObject[] nodes;
	private GameObject[] sortedNotes;
	private LineRenderer path;


	void Awake(){
		LineDisplay = GameObject.Find ("LineDisplay");
		nodes = GameObject.FindGameObjectsWithTag ("node");
	}
	public override void OnInspectorGUI(){
		sC = (splineController)target;
		path = sC.getLineRenderer ();
		populateNodes ();
		if(GUILayout.Button("Add New Point")){
			createNewPoint();
		}
		path.positionCount = nodes.Length;
		for(int i=0;i<nodes.Length;i++){
			EditorGUILayout.Vector3Field("Node "+(i+1),sortedNotes[i].transform.position);
			path.SetPosition (i, sortedNotes[i].transform.position);
		}
	}
	private void populateNodes(){
		sortedNotes = new GameObject[nodes.Length];
		for (int i = 0; i < nodes.Length; i++) {
			for (int j = 0; j < nodes.Length; j++) {
				if (nodes [j].GetComponent<nodeController> ().nodeIndex == i) {
					sortedNotes [i] = nodes [j];
				}
			}
		}
		nodeObject = sortedNotes [0];
	}
	private void createNewPoint(){

		GameObject ob = Instantiate (nodeObject);
		ob.transform.parent = LineDisplay.transform;
		ob.transform.position = new Vector3 (0, 0, 0);
		ob.GetComponent<nodeController> ().nodeIndex = sortedNotes.Length;
		Selection.SetActiveObjectWithContext (ob,ob);
	}

	void Update () {
		
	}
}
