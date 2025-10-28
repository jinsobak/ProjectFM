using UnityEngine;

public class Pump : Building
{
    private bool activated = false;
    private TimeData timeData;
    [SerializeField]
    private float produceTime;
    [SerializeField]
    private int waterAmount;

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Pump");
    }

    private void Update()
    {
        if(activated)
        {
            if(!timeData.timerActivated)
            {
                timeData.StartTimer();
            }
            else
            {
                timeData.DiscountCooltime();
            }
        }
    }

    public override void OnConstruct()
    {
        activated = true;
        timeData = new TimeData(produceTime);
        timeData.RegisterCooltimeEndAction(ProduceWater);
    }

    private void ProduceWater()
    {
        ResourceManager.instance.AddWater(waterAmount);
    }
}
