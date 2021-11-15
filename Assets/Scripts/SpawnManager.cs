using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _startButton;

    [SerializeField] private FadeWindow _fadeWindow;

    [SerializeField] private GameObject _loadScreen;

    [SerializeField] private Text _taskText;

    [SerializeField] private GameObject _slotPrefab;

    [SerializeField] private CardBundleData[] _cardBundleDatas;

    private CardBundleData currentCardBundleData;

    private GameObject firstSlot;
    private GameObject secondSlot;
    private GameObject thirdSlot;

    public GameObject SecondSlot => secondSlot;
    public GameObject ThirdSlot => thirdSlot;

    private List<CardData> newCards = new List<CardData>();

    private CardData correctCard;
    private List<CardData> previousCorrectCards = new List<CardData>();

    public void StartGame()
    {
        _startButton.SetActive(false);
        _taskText.gameObject.SetActive(true);

        currentCardBundleData = _cardBundleDatas[Random.Range(0, _cardBundleDatas.Length)];

        foreach (CardData cardData in currentCardBundleData.CardData)
        {
            newCards.Add(cardData);
        }

        SpawnFirstSlot();
    }

    private void SpawnFirstSlot()
    {
        List<CardData> possibleRandomCards = new List<CardData>();
        List<CardData> spawnedCards = new List<CardData>();

        correctCard = newCards[Random.Range(0, newCards.Count)];
        newCards.Remove(correctCard);

        possibleRandomCards.AddRange(newCards);
        possibleRandomCards.AddRange(previousCorrectCards);

        spawnedCards.Add(correctCard);

        previousCorrectCards.Add(correctCard);

        _taskText.text = "Find " + correctCard.Identifier;


        for (int i = 0; i < 2; i++)
        {
            CardData randomCard = possibleRandomCards[Random.Range(0, possibleRandomCards.Count)];
            possibleRandomCards.Remove(randomCard);
            spawnedCards.Add(randomCard);
        }

        firstSlot = Instantiate(_slotPrefab, new Vector3(0, 2), Quaternion.identity);
        

        List<SpriteRenderer> spriteRenderers = firstSlot.GetComponent<Slot>().Cells.OfType<SpriteRenderer>().ToList();

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, spriteRenderers.Count);

            spriteRenderers[randomIndex].sprite = spawnedCards[i].Sprite;
            spriteRenderers[randomIndex].GetComponent<Cell>().SetCard(spawnedCards[i], correctCard);
            spriteRenderers.RemoveAt(randomIndex);
        }
    }

    public void SpawnSecondSlot()
    {
        List<CardData> possibleRandomCards = new List<CardData>();

        List<CardData> spawnedCards = new List<CardData>();

        correctCard = newCards[Random.Range(0, newCards.Count)];
        newCards.Remove(correctCard);
        
        possibleRandomCards.AddRange(newCards);
        possibleRandomCards.AddRange(previousCorrectCards);

        spawnedCards.Add(correctCard);

        previousCorrectCards.Add(correctCard);

        _taskText.text = "Find " + correctCard.Identifier;

        for (int i = 0; i < 5; i++)
        {
            CardData randomCard = possibleRandomCards[Random.Range(0, possibleRandomCards.Count)];
            possibleRandomCards.Remove(randomCard);
            spawnedCards.Add(randomCard);
        }


        secondSlot = Instantiate(_slotPrefab, new Vector3(0, 0), Quaternion.identity);

        List<SpriteRenderer> spriteRenderers = firstSlot.GetComponent<Slot>().Cells.OfType<SpriteRenderer>().ToList();
        spriteRenderers.AddRange(secondSlot.GetComponent<Slot>().Cells.OfType<SpriteRenderer>().ToList());

        for (int i = 0; i < 6; i++)
        {
            int randomIndex = Random.Range(0, spriteRenderers.Count);

            spriteRenderers[randomIndex].sprite = spawnedCards[i].Sprite;
            spriteRenderers[randomIndex].GetComponent<Cell>().SetCard(spawnedCards[i], correctCard);
            spriteRenderers.RemoveAt(randomIndex);
        }
    }

    public void SpawnThirdSlot()
    {
        List<CardData> possibleRandomCards = new List<CardData>();

        List<CardData> spawnedCards = new List<CardData>();

        correctCard = newCards[Random.Range(0, newCards.Count)];
        newCards.Remove(correctCard);

        possibleRandomCards.AddRange(newCards);
        possibleRandomCards.AddRange(previousCorrectCards);

        spawnedCards.Add(correctCard);

        previousCorrectCards.Add(correctCard);

        _taskText.text = "Find " + correctCard.Identifier;

        for (int i = 0; i < 8; i++)
        {
            CardData randomCard = possibleRandomCards[Random.Range(0, possibleRandomCards.Count)];
            possibleRandomCards.Remove(randomCard);
            spawnedCards.Add(randomCard);
        }

        thirdSlot = Instantiate(_slotPrefab, new Vector3(0, -2), Quaternion.identity);

        List<SpriteRenderer> spriteRenderers = firstSlot.GetComponent<Slot>().Cells.OfType<SpriteRenderer>().ToList();
        spriteRenderers.AddRange(secondSlot.GetComponent<Slot>().Cells.OfType<SpriteRenderer>().ToList());
        spriteRenderers.AddRange(thirdSlot.GetComponent<Slot>().Cells.OfType<SpriteRenderer>().ToList());

        for (int i = 0; i < 9; i++)
        {
            int randomIndex = Random.Range(0, spriteRenderers.Count);

            spriteRenderers[randomIndex].sprite = spawnedCards[i].Sprite;
            spriteRenderers[randomIndex].GetComponent<Cell>().SetCard(spawnedCards[i], correctCard);
            spriteRenderers.RemoveAt(randomIndex);
        }
    }

    public void RestartGame()
    {
        _fadeWindow.FadeOut(1f);

        StartCoroutine(LoadScreen());

        currentCardBundleData = _cardBundleDatas[Random.Range(0, _cardBundleDatas.Length)];

        newCards.Clear();
        previousCorrectCards.Clear();

        foreach (CardData cardData in currentCardBundleData.CardData)
        {
            newCards.Add(cardData);
        }

        Destroy(firstSlot);
        Destroy(secondSlot);
        Destroy(thirdSlot);

        firstSlot = null;
        secondSlot = null;
        thirdSlot = null;
    }

    private IEnumerator LoadScreen()
    {
        _loadScreen.SetActive(true);

        yield return new WaitForSeconds(1f);

        _loadScreen.SetActive(false);

        SpawnFirstSlot();
    }
}
