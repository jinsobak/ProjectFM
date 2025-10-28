using System;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;

    public int water { get; private set; }

    public event Action OnWaterAmountChanged;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetWater(int _water)
    {
        water = _water;
        OnWaterAmountChanged?.Invoke();
    }

    public void AddWater(int _water)
    {
        water += _water;
        Debug.Log($"Add water amount: {_water} curWater: {water}");
        OnWaterAmountChanged?.Invoke();
    }


}
