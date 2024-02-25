using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    SpriteRenderer _renderer;

    #region Channels
    [SerializeField] private Combat_Channel _channel;
    #endregion

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Weapon")
        {
            _channel.OnEventRaised.AddListener(Change_Color);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Weapon")
        {
            _channel.OnEventRaised.RemoveListener(Change_Color);
        }
    }

    public void Change_Color(Vector2 direction, float damage)
    {
        float dot = Vector2.Dot(direction, (transform.position - PlayerController.Player_Transform.position).normalized);
        Debug.Log("Hit Dot Product: " + dot);

        if (dot >= .5f || Vector2.Distance(transform.position, PlayerController.Player_Transform.position) <= .05f)
            _renderer.color = Color.green;
    }
}
