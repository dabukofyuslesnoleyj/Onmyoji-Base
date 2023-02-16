using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDashListener
{
    public bool isDashListening {get;}
    // Start is called before the first frame update
    public void OnStartDash();

    public void OnFinishDash();

    public void DuringDash();

}
