using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace VRUiKits.Utils
{
    public class CardListManager : MonoBehaviour
    {
        #region Public Variables
        public static CardListManager instance;
        public Transform listContent;
        public TMP_InputField search;
        public GameObject itemTemplate;
        [HideInInspector]
        // cardList contains a list of card information such as title, subtitle, image, and etc.
        public List<Card> cardList = new List<Card>();
        public List<Card> uiCardList = new List<Card>();
        [HideInInspector]
        public Card selectedCard;
        #endregion
        public Toggle allToggle;
        public Toggle dToggle;
        public Toggle uiToggle;

        #region Private Variables
        // cardItems is a list of card displayed in the UI
        public List<CardItem> cardItems = new List<CardItem>();
        #endregion

        #region Monobehaviour Callbacks
        void Awake()
        {
            itemTemplate.SetActive(false);
            PopulateList();
            instance = this;
        }
        #endregion

        #region Public Methods
        public void OnChangeSearchWord()
        {
            Reset();
            PopulateList();
        }
        // Clear all the cards in the cardList
        public void Reset()
        {
            foreach (CardItem _item in cardItems)
            {
                Util.SafeDestroyGameObject(_item);
            }
            cardItems.Clear();
        }

        // Populate cards from the cardList
        public void PopulateList()
        {
            if(uiToggle.isOn || allToggle.isOn)
            {
                for (int i = 0; i < uiCardList.Count; i++)
                {
                    uiCardList[i].type = "UI";
                    uiCardList[i].index = i;
                    if(uiCardList[i].prefab.name.Contains(search.text))
                        DrawCardItem(uiCardList[i]);
                }
            }
            if(dToggle.isOn || allToggle.isOn)
            {
                for (int i = 0; i < cardList.Count; i++)
                {
                    cardList[i].index = i;
                    cardList[i].type = "3D";
                    if(cardList[i].prefab.name.Contains(search.text))
                        DrawCardItem(cardList[i]);
                }
            }
        }

        // Add a new card to the cardList and draw it in the UI
        public void AddCardItem(Card card)
        {
            cardList.Add(card);
            DrawCardItem(card);
        }
        #endregion

        #region Private Methods
        // Draw the card item in the UI using the card information
        void DrawCardItem(Card card)
        {
            CardItem item = Instantiate(itemTemplate, listContent).GetComponent<CardItem>();
            item.Card = card;
            item.gameObject.SetActive(true);
            cardItems.Add(item);
            item.OnCardClicked += SetSelectedCard;
        }

        void SetSelectedCard(Card card)
        {
            selectedCard = card;
        }
        #endregion
    }
}
