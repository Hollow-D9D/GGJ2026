using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    [SerializeField]
    Image healthImage;

    float imageMaxWidth;
    RectTransform rectTransform;

    [SerializeField]
    LayerMask enemyLayerMask;

    void Start()
    {
        rectTransform = healthImage.GetComponent<RectTransform>();
        imageMaxWidth = rectTransform.sizeDelta.x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((enemyLayerMask.value & (1 << collision.gameObject.layer)) != 0)
        {
            DealDamage(20);
            Debug.Log("eh");
        }
    }

    protected override void DamageDealth(int damage)
    {
        rectTransform.sizeDelta = new Vector2(imageMaxWidth * ((float)health / (float)maxHealth), rectTransform.sizeDelta.y);
    }

    protected override void Death()
    {
        Restart.RestartScene();
    }
}
