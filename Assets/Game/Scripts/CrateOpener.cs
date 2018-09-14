using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTracker.Player;
using VRTracker.Manager;

public class CrateOpener : MonoBehaviour {


    private VRT_Tag tagController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void OnCloseCrate(){
        UnityMainThreadDispatcher.Instance().Enqueue(CloseCrate());

    }

    public void OnOpenCrate()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(OpenCrate());

    }

    public IEnumerator OpenCrate(){
        GetComponent<Animator>().SetBool("open", true);
            yield return null;
    }

     public IEnumerator CloseCrate()
    {
        GetComponent<Animator>().SetBool("open", false);
            yield return null;
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.name.Equals("PlayerAxe") && collider.gameObject.GetComponentInParent<VRT_FollowTag>() != null){
            tagController = collider.gameObject.GetComponentInParent<VRT_FollowTag>().tagToFollow;
            tagController.OnRedButtonDown += OnOpenCrate;
            tagController.OnRedButtonUp += OnCloseCrate;
        }
    }


    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "PlayerAxe" && collider.gameObject.GetComponentInParent<VRT_FollowTag>() != null)
        {
            tagController.OnRedButtonDown -= OnOpenCrate;
            tagController.OnRedButtonUp -= OnCloseCrate;
        }
    }
}
