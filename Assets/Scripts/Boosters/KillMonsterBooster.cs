using System;

class KillMonsterBooster : Booster
{
    protected override void ApplyBooster()
    {
        var onScreen = _camera.ScreenToWorldPoint(transform.position);
        
    }
}