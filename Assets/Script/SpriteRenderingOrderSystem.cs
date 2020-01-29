using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRenderingOrderSystem : MonoBehaviour
{
    private Vector2 playerCharacterPos;

    // Start is called before the first frame update
    void Awake()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if(Player.MyInstance != null)
            playerCharacterPos = Player.MyInstance.transform.position;
        if (transform.position.y  <( playerCharacterPos.y + 0.3f))
            {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.7f);
            }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);

        }
    }
}
