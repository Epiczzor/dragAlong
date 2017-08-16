using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineFollower : MonoBehaviour {

	public splineController Line;
	public float appliedVelocity = 1.0f;
	public int currentNode = 0;
	private GameObject[] nodeList;
	private bool trackEnd = false;
	void Start () {
		initLineFollower();
	}

	public void initLineFollower(){
		trackEnd = false;
		getNodeList();
	}
	void getNodeList(){ // LETS import a sorted list
		GameObject[] temp = Line.GetComponent<splineController>().nodeList;
		nodeList = new GameObject[temp.Length];
		for(int i = 0;i<nodeList.Length;i++){
			for(int j = 0;j<nodeList.Length;j++){
				if(temp[j].GetComponent<nodeController>().nodeIndex == i){
					nodeList[i] = temp[j];
				}
			}
		}
		initMeshTransforms();
	}
	
	public Vector3 moveAlongLine(Vector3 fromNode,Vector3 toNode){
		Vector3 dir = (toNode - fromNode).normalized;
		return dir;
	}
	void initMeshTransforms(){
		transform.position = nodeList[0].transform.position;
	}
	void OnTriggerEnter(Collider col){
		if(col.gameObject.CompareTag("node")){
			int nodeIndex = col.gameObject.GetComponent<nodeController>().nodeIndex;
			if(currentNode != (nodeIndex + 1)){
				currentNode = nodeIndex;
			}
		}
	}
	void resetLineFollowerToStart(){
		initLineFollower();
	}
	void onTrackEnd(){
		trackEnd = true;
		Debug.Log("Track Ended");
	}
	void Update () {
		if(currentNode < (nodeList.Length-1)){
			Vector3 appliedDirction = moveAlongLine(transform.position,nodeList[currentNode+1].transform.position);
			transform.Translate(appliedDirction * appliedVelocity * Time.deltaTime);
		}
		else if(!trackEnd) onTrackEnd();
	}
}
