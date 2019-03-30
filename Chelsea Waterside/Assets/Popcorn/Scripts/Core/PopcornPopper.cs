//	Copyright 2013 Unluck Software	
//	www.chemicalbliss.com	
#pragma warning disable 0618
using UnityEngine;
using System;
using System.Collections;


public class PopcornPopper:MonoBehaviour{
    //Enabling
    	//To pop or not to pop
    	public bool popEnabled = true;
    	//Delay before spawning
    	public float delayStart;
    	//How fast popcorn spawns (low = fast)
    	public float spawnRate = .001f;
    	//How long after being popped the colliders will be active (0 = never)
    	public float removeCollidersDelay = 10.0f;
    //Prefabs
    	//Place popcorn prefab
    	public GameObject popcorn;
    	//Place popcorn material
    	public Material mergeMaterial;
    	//Audio when popcorn pops
    	public bool popcornAudio;
    	
    //Instatiating
    	//Number of popcorns to spawn
    	public int spawnAmount = 300;
    	//Spawn area
    	public float spawnWidth = 0.02f;
    	public float spawnHeight = 0.02f;
    	public float spawnDepth = 0.02f;
        public float lifetime = 2.0f;
    	
    //Popping
    	//Force applied to popcorn when they pop
    	public Vector3 popForce = new Vector3(0.25f,7.0f,0.25f);
    	//Randomized delay before popping
    	public float delayMax = 3.0f;
    	public float delayMin = 1.0f;
    	//Pop popcorn instantly after being spawned (delay to a single popcorn still applies)
    	public bool popAtOnce;
        private bool popRemoved;
    	
    //Corn Kernels
    	//Show kernels
    	public bool showCorn = true;
    	//Simulate sizzle from heat (force from popForce)
    	public bool sizzleCorn;
    	//popForce multiplier
    	public float sizzleMultiplier = 0.25f;
    
    //Merging
    	//Enables combining of meshes
    	public bool merge = true;
    	//How many popcorn to merge into the mesh at a time 
    	public int mergeAmount = 1;
    	//Delay before starting the merging
    	public float mergeDelay =1.0f;
    	//How many popcorn each chunk holds
    	public int mergeChunks = 100;
    
    //Contains popcorn that will be merged
    [HideInInspector]
    public GameObject[] mergePopcorn;
    //Merged popcorn uses mesh colliders, higly increases overall performance but can cause fps spikes when alot of loose popcorn are close
    public bool mergeColliders = false;
    //Used for merging
    GameObject _combinedFrags;
    GameObject _oldFrags;
    //Stores all the popcorn
    [HideInInspector]
    GameObject[] allPopcorn;
    //Counts popcorn as they spawn
    int popcornCounter;
    //Global popcorn counter
    //static var allPopcornCounter:int;
    //Counts how many popcorn is in the mergePopcorn array
    [HideInInspector]
    public int mergeCounter;
    [HideInInspector]
    public int allMergeCounter;
    bool merging;
    
    public void Start() {
        //	allPopcornCounter=0;
        if(mergeAmount <= 0)
        merge = false;
        popRemoved = false;
        StartCoroutine(init());
    }
    
    public IEnumerator init() {
      //  spawnWidth *= 0.5f;
      //  spawnHeight *= 0.5f;
      //  spawnDepth *= 0.5f;
        allPopcorn = new GameObject[spawnAmount];
        mergePopcorn = new GameObject[mergeAmount];
        yield return new WaitForSeconds(delayStart);
        InvokeRepeating("spawner", spawnRate, spawnRate);
      	createCombineFrags();
    }
    


    public void createCombineFrags(){
    	_combinedFrags = new GameObject();
        _combinedFrags.isStatic = true;
        _combinedFrags.AddComponent<MeshFilter>();
        _combinedFrags.AddComponent<MeshRenderer>();
        _combinedFrags.AddComponent<MeshCollider>();
        _combinedFrags.transform.GetComponent<Renderer>().castShadows = false;
        _combinedFrags.transform.GetComponent<Renderer>().receiveShadows = false;
        _combinedFrags.GetComponent<MeshRenderer>().sharedMaterial = mergeMaterial;
    }
    
    public void combineMeshes() {
        //Debug.Log("combine");
        if(allMergeCounter >=mergeChunks){
        	createCombineFrags();
        	allMergeCounter=0;
        }
        
        if (merge && mergeCounter >= mergeAmount) {
            _oldFrags = _combinedFrags;
            CombineInstance[] combine = new CombineInstance[mergeAmount + 1];
            for(int i = 0; i < mergeAmount; i++) {
                combine[i].mesh = mergePopcorn[i].transform.GetComponent<MeshFilter>().sharedMesh;
                combine[i].transform = mergePopcorn[i].transform.localToWorldMatrix;
                if(!mergeColliders){
                	mergePopcorn[i].GetComponent<Renderer>().enabled = false;
                }else{  	
                	Destroy(mergePopcorn[i].transform.parent.gameObject);
                }
    			allMergeCounter++;
            }
            if (_oldFrags != null) {
                combine[mergeAmount].mesh = _combinedFrags.transform.GetComponent<MeshFilter>().sharedMesh;
                combine[mergeAmount].transform = _combinedFrags.transform.localToWorldMatrix;
            }
            _combinedFrags.GetComponent<MeshFilter>().mesh = new Mesh();
			//Debug.Log(combine[10]);
            _combinedFrags.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine);
            mergeCounter = 0;
            if(mergeColliders)
           _combinedFrags.GetComponent<MeshCollider>().sharedMesh = _combinedFrags.GetComponent<MeshFilter>().sharedMesh;      
        }    
    }
    
    public void spawner() {
        if (popEnabled) {
            popRemoved = false;
            if (popcornCounter < spawnAmount) {      
                allPopcorn[popcornCounter] = (GameObject)GameObject.Instantiate(popcorn, new Vector3(transform.position.x + UnityEngine.Random.Range(-spawnWidth, spawnWidth), transform.position.y + UnityEngine.Random.Range(0.0f, spawnHeight), transform.position.z + UnityEngine.Random.Range(-spawnDepth, spawnDepth)), UnityEngine.Random.rotation);
                Popcorn pp = allPopcorn[popcornCounter].GetComponent<Popcorn>();
                pp.popper = this;
                if (!showCorn && (pp.GetComponent<Popcorn>().corn != null)) {
                    pp.GetComponent<Popcorn>().corn.GetComponent<Renderer>().enabled = false;
                }
                if (popEnabled && popAtOnce) {
                    StartCoroutine(pp.GetComponent<Popcorn>().pop(delayMin, delayMax));
                }
                popcornCounter++;
            } else {
                CancelInvoke("spawner");
                StartCoroutine(removePopcorn());
                if (!popAtOnce) {
                    StartCoroutine(popAll(1));
                }
            }
            if (!merging) {
            	//Debug.Log("merge enabled");
                InvokeRepeating("combineMeshes", this.mergeDelay, .15f);
                merging = true;
            }

          }
    }

    public IEnumerator removePopcorn() {
        yield return new WaitForSeconds(lifetime);
        for (int i = 0; i < allPopcorn.Length; i++)
        {
            Destroy(allPopcorn[i]);
        }
        popRemoved = true;
 //       popEnabled = false;
        popcornCounter = 0;
    }

    public void resetPopcorn() {
 
        if (popRemoved == true)
        {
//            popEnabled = false; 
            StartCoroutine(init());

        }
    }

    public IEnumerator popAll(int d) {
        yield return new WaitForSeconds((float)d);
        for(int i = 0; i < allPopcorn.Length; i++)
        {
            StartCoroutine(allPopcorn[i].GetComponent<Popcorn>().pop(delayMin, delayMax));
        }
    }

    //function sleepAll(d: int) {
    //    yield(WaitForSeconds(d));
    //    for (var i = 0; i < allPopcorn.length; i++)
    //    {
    //        if (allPopcorn[i]) {
    //            if (allPopcorn[i].GetComponent(Rigidbody))
    //                allPopcorn[i].GetComponent(Rigidbody).rigidbody.Sleep();
    //        }
    //    }
    //}
}
