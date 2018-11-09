using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;

    public void Awake()
    {
        instance = this;
    }

    public GameObject turretToBuild;

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }
}
