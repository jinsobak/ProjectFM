using UnityEngine;

public class TestTree : Building
{

    private void Update()
    {
        
    }

    public override void OnConstruct()
    {
        EventManager.OnBuildingConstructed?.Invoke(buildingData.producableUnitList);
    }
}
