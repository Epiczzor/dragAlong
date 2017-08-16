using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splineController : MonoBehaviour {
	
	public GameObject[] nodeList;
	public int currentNode = 0;
	void Awake () {
		populateNodes ();
	}
	void Update () {
	}
	private void populateNodes(){
		GameObject[] nodes = GameObject.FindGameObjectsWithTag ("node");
		nodeList = new GameObject[nodes.Length];
		for (int i = 0; i < nodes.Length; i++) {
			for (int j = 0; j < nodes.Length; j++) {
				if (nodes [j].GetComponent<nodeController> ().nodeIndex == i) {
					nodeList [i] = nodes [j];
				}
			}
		}

	}
	public LineRenderer getLineRenderer(){
		return GetComponent<LineRenderer> ();
	}

}
