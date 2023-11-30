using UnityEngine;
using UnityEngine.UI;

public class DrinkToggle : MonoBehaviour
{
    public Text drinkNameText;
    public GameObject quantityPanel;
    public InputField quantityInput;
    public GameObject coffeeCupPrefab;
    public Transform tableTransform;
    public float spawnDelay = 2f;

    private bool showingQuantity;

    private void Start()
    {
        quantityPanel.SetActive(false);
        showingQuantity = false;
    }

    public void ToggleQuantityPanel()
    {
        showingQuantity = !showingQuantity;
        quantityPanel.SetActive(showingQuantity);
    }

    public void ConfirmQuantity()
    {
        int quantity = int.Parse(quantityInput.text);
        StartCoroutine(SpawnCoffeeCups(quantity));
    }

    private System.Collections.IEnumerator SpawnCoffeeCups(int quantity)
    {
        yield return new WaitForSeconds(spawnDelay);

        for (int i = 0; i < quantity; i++)
        {
            Instantiate(coffeeCupPrefab, tableTransform.position, tableTransform.rotation);
        }
    }
}