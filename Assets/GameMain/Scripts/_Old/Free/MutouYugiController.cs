using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutouYugiController : MonoBehaviour
{
    public PlayerController player;
    private SpriteRenderer sprite_renderer;
    private void Start()
    {
        player = GetComponentInParent<PlayerController>();
        sprite_renderer = GetComponent<SpriteRenderer>();
    }

    public void HenshinEnd() {
        sprite_renderer.enabled = false;
        player.YamiYugiKita();
    }
}
