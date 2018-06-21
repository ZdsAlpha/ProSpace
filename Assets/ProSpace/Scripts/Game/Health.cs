using UnityEngine;

[DisallowMultipleComponent]
public class Health : MonoBehaviour
{
    public int Lives = 1;
    public float HP = 10f;
    public float MaxHP = 10f;
    public float HPGeneration = 5f;
    public string Team = "";
    public TransformEvent OnDied;
    public FloatEvent OnTakeDamage;
    public VoidEvent OnRebirth;
    public VoidEvent OnElimination;

    public Vector3 SavePoint
    {
        get
        {
            return _point;
        }
    }

    private Vector3 _point;

    public void Start()
    {
        SaveCheckpoint();
    }
    public void Update()
    {
        HP = Mathf.Clamp(HP + HPGeneration * Time.deltaTime, 0f, MaxHP);
        if (transform.position.y < Constants.VoidHeight)
        {
            Die();
        }
    }
    public void TakeDamage(float damage)
    {
        HP = Mathf.Clamp(HP - damage, 0f, MaxHP);
        OnTakeDamage.Invoke(damage);
        if (HP <= 0f)
        {
            Die();
        }
    }
    public void Die()
    {
        HP = 0f;
        Lives -= 1;
        OnDied.Invoke(transform);
        if (Lives > 0)
        {
            Rebirth();
        }
        else
        {
            OnElimination.Invoke();
        }
    }
    public void Rebirth()
    {
        HP = MaxHP;
        transform.position = _point;
        OnRebirth.Invoke();
    }
    public void SaveCheckpoint()
    {
        _point = transform.position;
    }
    public void SaveCheckpoint(Vector3 Point)
    {
        _point = Point;
    }
}
