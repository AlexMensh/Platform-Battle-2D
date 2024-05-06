using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Unit))]
public class UIDrawer : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;

    private Unit _player;

    private void Awake()
    {
        _player = GetComponent<Unit>();
    }

    private void OnEnable()
    {
        _player.HealthChanged += DrawUI;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= DrawUI;
    }

    public void DrawUI(float health, float maxHealth)
    {
        _healthBar.value = health / maxHealth;
    }
}