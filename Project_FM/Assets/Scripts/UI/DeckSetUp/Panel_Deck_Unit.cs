using System.Runtime.CompilerServices;
using UnityEngine;

public class Panel_Deck_Unit : MonoBehaviour
{
    [SerializeField]
    private GameObject slot_deckPF;
    [SerializeField]
    private Transform slotParent;
    
    private Slot_Deck_Unit[] slots_deck;
    private UI_Stage_DeckSetUP masterUI;

    public void Init(UI_Stage_DeckSetUP masterUI, UnitData[] initalDeckData)
    {
        this.masterUI = masterUI;

        int deckCount = masterUI.deckSlotCount;
        int maxDeckCount = masterUI.maxDeckSlotCount;

        slots_deck = new Slot_Deck_Unit[maxDeckCount];

        for(int i = 0; i < maxDeckCount; i++)
        {
            GameObject object_slot_deck = Instantiate(slot_deckPF, slotParent);
            Slot_Deck_Unit cp_slot_deck = object_slot_deck.GetComponent<Slot_Deck_Unit>();

            if(i < initalDeckData.Length)
            {
                cp_slot_deck.InitSlot(initalDeckData[i], masterUI, false);
            }
            else if(i >= deckCount && i < maxDeckCount)
            {
                cp_slot_deck.InitSlot(null, masterUI, true);
            }
            else
            {
                cp_slot_deck.InitSlot(null, masterUI, false);
            }

            slots_deck[i] = cp_slot_deck;
        }
    }

    public void RefreshSlots()
    {
        UnitData[] deckData = masterUI.deck_inStage;
        int deckCount = deckData.Length;
        
        for(int i = 0; i < deckCount; i++)
        {
            slots_deck[i].InitSlot(deckData[i], masterUI);
        }
    }
}
