using UnityEngine;

public class Event_InStage_CameraInitStart
{
    // Camera Initalize Data
    public Transform leftLimitTF;
    public Transform rightLimitTF;
    public Transform mainbuildingTF_player;
    public Transform mainBuildingTF_enemy;

    public Event_InStage_CameraInitStart(Transform leftLimitTF, Transform rightLimitTF, Transform mainbuildingTF_player, Transform mainBuildingTF_enemy)
    {
        this.leftLimitTF = leftLimitTF;
        this.rightLimitTF = rightLimitTF;
        this.mainbuildingTF_player = mainbuildingTF_player;
        this.mainBuildingTF_enemy = mainBuildingTF_enemy;
    }
}
