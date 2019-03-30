//	Copyright 2013 Unluck Software	
//	www.chemicalbliss.com	

using UnityEngine;
using System;
using System.Collections;


public class Popcorn:MonoBehaviour{
    public GameObject popCorn;
    public GameObject corn;
    public PopcornPopper popper;
    public float delay;
 //   public float lifetime;
    public bool popped;
#pragma warning disable 0618

	public void Start() {
    	//transform.localScale = new Vector3().one*Random.Range(.8,.99);
    	if(popCorn.GetComponent<BoxCollider>() != null)
     	popCorn.GetComponent<BoxCollider>().size*=.5f;
     	
    }
    
    public IEnumerator pop(float minDelay,float maxDelay){
    	delay = UnityEngine.Random.Range(minDelay, maxDelay);
    	yield return new WaitForSeconds(delay);
    	//rigidbody.isKinematic = false;
    	popped = true;
    	if(corn != null)
    	corn.active = false;
    	if(popper.popcornAudio){
    		GetComponent<AudioSource>().Play();
            //    		GetComponent<AudioSource>().volume*=UnityEngine.Random.value;
            GetComponent<AudioSource>().volume = 1;
            GetComponent<AudioSource>().pitch = .25f*UnityEngine.Random.value+.75f;
    	}
    	if(popCorn.GetComponent<BoxCollider>() != null)
    	popCorn.GetComponent<BoxCollider>().size*=2;
    	popCorn.GetComponent<Renderer>().enabled = true;
    	popCorn.GetComponent<Collider>().enabled = true;
    	GetComponent<Rigidbody>().AddForce(UnityEngine.Random.Range(-popper.popForce.x,popper.popForce.x),UnityEngine.Random.Range(0.1f,popper.popForce.y),UnityEngine.Random.Range(-popper.popForce.z,popper.popForce.z));
  //  	InvokeRepeating("mergeMe", popper.mergeDelay, .1f);
 //   	if(!popper.mergeColliders && popper.removeCollidersDelay > 0){
 //    		yield return new WaitForSeconds(popper.removeCollidersDelay);
 //    		Destroy(popCorn.GetComponent<Collider>());
 //    	}
 //       Destroy(popCorn, (lifetime - delay));
    }
    
    public void FixedUpdate(){
    	if(!popped && (corn != null) && this.popper.sizzleCorn && corn.active){
    		GetComponent<Rigidbody>().AddForce(UnityEngine.Random.Range(-this.popper.popForce.x*popper.sizzleMultiplier,this.popper.popForce.x*popper.sizzleMultiplier),UnityEngine.Random.Range(-this.popper.popForce.y*popper.sizzleMultiplier,this.popper.popForce.y*popper.sizzleMultiplier) ,UnityEngine.Random.Range(-this.popper.popForce.z*popper.sizzleMultiplier,this.popper.popForce.z*popper.sizzleMultiplier) );
    	}	
    } 
    
    public void mergeMe(){
    	Destroy(GetComponent<Rigidbody>());
    	if(popped && popper.merge && popper.mergeAmount > 0 && popper.mergeAmount > popper.mergeCounter){
    		Destroy(GetComponent<Collider>());
    		popper.mergePopcorn[popper.mergeCounter] = popCorn.gameObject;
    		popper.mergeCounter++;
    		CancelInvoke();
    		
    		}
    }
}
