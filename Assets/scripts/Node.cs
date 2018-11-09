using UnityEngine;

public class Node : MonoBehaviour {


    public Color hoverColor;
    public float yOffset = 0.5f;

    private Color startColor;
    private Renderer rend;

    private GameObject turret;

    public void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    public void OnMouseUpAsButton()
    {
        if(turret != null )
        {
            Debug.Log("Node Occupied!");
            return;
        }

        
        turret =  Instantiate(BuildManager.instance.GetTurretToBuild(), 
            transform.position + Vector3.up * yOffset, 
            transform.rotation);
    }

    public void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    public void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
