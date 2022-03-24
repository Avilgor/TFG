using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface RaycastObject
{
    bool isHit { get; set; }
    bool isSelectable { get; set; }

    public void SetSelectable(bool sel);
    public void OnHit();
    public void StopHit();
}
