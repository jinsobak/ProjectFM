using System.Runtime.CompilerServices;
using UnityEngine;

public class Panel_Deck : MonoBehaviour
{
    [SerializeField]
    private GameObject slot_deckPF;
    [SerializeField]
    private Transform slotParent;

    
    private Slot_Deck[] slots_deck;
    private UI_Stage_DeckSetUP masterUI;

    public void Init(UI_Stage_DeckSetUP masterUI, BuildingData[] initalDeckData)
    {
        this.masterUI = masterUI;

        int deckCount = StageManager.instance.mockUserData.deckSlotCount;
        
        slots_deck = new Slot_Deck[deckCount];

        for(int i = 0; i < deckCount; i++)
        {
            GameObject object_slot_deck = Instantiate(slot_deckPF, slotParent);
            Slot_Deck cp_slot_deck = object_slot_deck.GetComponent<Slot_Deck>();

            cp_slot_deck.InitSlot(initalDeckData[i], masterUI);
            slots_deck[i] = cp_slot_deck;
        }
    }

    public void RefreshSlots()
    {
        BuildingData[] deckData = masterUI.deck_inStage;
        int deckCount = deckData.Length;
        
        for(int i = 0; i < deckCount; i++)
        {
            slots_deck[i].InitSlot(deckData[i], masterUI);
        }
    }
}
