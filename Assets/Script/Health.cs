using UnityEngine;
public class Health : MonoBehaviour
{
    public int maxHealthCount;
    [HideInInspector] public int countHealth;
    private void Start()
    {
        countHealth = maxHealthCount;
    }
    public void GetHealth()
    {
        countHealth++;
    }
    public void LoseHealth()
    {
        countHealth--;
    }
}
