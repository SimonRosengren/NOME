using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlammableObject : MonoBehaviour {

    public bool onFire = false;
    private float burnTimer = 5;

    [SerializeField] bool destoryable;

    public ParticleSystem fire;

	void Start ()
    {		
	}
	
	void Update () 
    {
        if (onFire)
        {

            CheckifDestroyable();

            if (burnTimer <= 0)
            {
                //Instantiate(Ash, transform.postition, Quaternion.identity)
                Destroy(this.gameObject);                
            }
        }		
	}

    void SetFire()
    {
        ParticleSystem test = Instantiate(fire, transform.position, transform.rotation);

        test.Play();
        test.transform.parent = transform;
        test.transform.rotation = Quaternion.Euler(-90, 0, 0);
        //PLAY ANIMATION
        if (!onFire)
        {
            onFire = true;
        }       
    }

    void CheckifDestroyable()
    {
        if (destoryable == true)
        {
            burnTimer -= Time.deltaTime;
        }
    }
}
