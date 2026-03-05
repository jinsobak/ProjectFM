using System.Runtime.CompilerServices;
using UnityEngine;

public class MainBuilding : MonoBehaviour
{
    private bool activated = false;     // bool that mainBuilding is activated
    private TimeData timeData;          // Class that manage resource produce cooltime
    [SerializeField]
    private float time_waterProduce;    // resource produce cooltime
    [SerializeField]
    private int amount_waterProduce;    // resource produce amount

    public void Init()
    {
        activated = false;
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
