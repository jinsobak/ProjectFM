using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
}
