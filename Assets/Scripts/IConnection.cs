using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConnection 
{
    void GetImage(object demanding);
    event Action<Texture, object> textureReceived;
}
