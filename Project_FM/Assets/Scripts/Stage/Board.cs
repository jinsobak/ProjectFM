using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private BuildingArea m_buildingArea;      // Player Building Area
    public BuildingArea buildingArea { get { return m_buildingArea; }}
    [SerializeField]
    private MainBuilding m_mainBuilding;      // Player Main Building
    public MainBuilding MainBuilding { get { return m_mainBuilding; }}

    public void Init()
    {
        m_buildingArea.Init(5);
        m_mainBuilding.Init();
    }

}
