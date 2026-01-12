using System.Runtime.CompilerServices;
using UnityEngine;

public class MainBuilding : MonoBehaviour
{
    private bool activated = false;
    private TimeData timeData;
    [SerializeField]
    private float time_waterProduce;
    [SerializeField]
    private int amount_waterProduce;

    public void Init_mainBuilding()
    {
        activated = true;
        timeData = new TimeData(time_waterProduce);
        timeData.RegisterCooltimeEndAction(ProduceWater);
    }

    private void Update()
    {
        if (activated)
        {
            if (!timeData.timerActivated)
            {
                timeData.StartTimer();
            }
            else
            {
                timeData.DiscountCooltime();
            }
        }
    }

    private void ProduceWater()
    {
        ResourceManager.instance.AddWater(amount_waterProduce);
    }
}
