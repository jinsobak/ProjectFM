using UnityEngine;

public class LineSelectBtn : UI
{
    [SerializeField]
    private GroundLinePosition linePos;

    public override void InitUI()
    {
        base.InitUI();
    }

    /// <summary>
    /// 유니티 버튼 클릭 이벤트에 연결
    /// 클릭 시 이벤트 메니저에 라인 변경 이벤트 발행 요청
    /// </summary>
    public void OnClick()
    {
        EventManager.Publish(new Event_LineChange(linePos));
    }


}
