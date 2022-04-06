using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_TouchableObject 
{
    public void OnStartTouch(Vector2 position);
    public void OnEndTouch(Vector2 position);
    public void OnDrag(Vector2 position);
}
