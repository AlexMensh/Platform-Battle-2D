using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public class UIDrawer : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;

    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.HealthChanged += DrawUI;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= DrawUI;
    }

    public void DrawUI(float health, float maxHealth)
    {
        _healthBar.value = health / maxHealth;
    }
}