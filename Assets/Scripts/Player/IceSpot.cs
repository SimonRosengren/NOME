using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpot : MonoBehaviour {

    public PhysicMaterial mat;
    public bool melting = false;
    public SphereCollider sc;
    public Projector pr;

    List<OriginMaterial> materials;

	void Start ()
    {
        mat = (PhysicMaterial)Resources.Load("PhysicMaterial/NoFriction");
        materials = new List<OriginMaterial>();
	}
	
	void Update () 
    {
        if (melting)
        {
            Melt();
        }		
	}

    void OnTriggerStay(Collider other)
    {
        if (other.material.name != "NoFriction (Instance)")
        {
            if (other.tag == ("Player") || other.tag == ("CleaningRobot") || other.tag == ("Grabable"))
            {
                materials.Add(new OriginMaterial(other.material, other.tag));
                other.material = mat;             
            }
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == ("CleaningRobot") || other.tag == ("Grabable"))
        {
            for (int i = 0; i < materials.Count; i++)
            {
                if (materials[i].tag == other.tag)
                {
                    other.material = materials[i].mat;
                    materials.RemoveAt(i);
                    break;
                }
            }
        }
    }

    void SetFire()
    {
        melting = true;
    }

    void Melt()
    {
        sc.radius -= Time.deltaTime * 0.5f;
        pr.orthographicSize -= Time.deltaTime * 0.5f;
        if (sc.radius <= 0)
        {
            Destroy(gameObject);
        }       
    }
}

public class OriginMaterial
{
    public PhysicMaterial mat;
    public string tag;
    public OriginMaterial(PhysicMaterial mat, string tag)
    {
        this.mat = mat;
        this.tag = tag;
    }
}