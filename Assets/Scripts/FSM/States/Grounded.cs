using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public abstract class Grounded : Controllable
{
    public override void FixedRun()
    {
        base.FixedRun();

        if (CheckGround(out RaycastHit rayHit))
            Float(stateComponent.transform, rb, rayHit, stats_GroundCheck.Height, stats_GroundCheck.DampingForce, stats_GroundCheck.SpringStrength);
        else
            nextState = stateComponent.State_Air;
    }

    protected void Float(Transform transform, Rigidbody rb, RaycastHit rayHit, float height, float dampingForce, float springStrength)
    {
            //Per applicare la forza di una molla usiamo la formula springForce = offset - damping;
            //offset = differenza di distanza su quanto distante � dal terreno e quanto distante dovrebbe essere.
            float offset = height - rayHit.distance;
            //calcoliamo la velocity relativa alla direzione in cui il player deve applicare la forza della molla (verso gi�) 
            float rayDirVel = Vector3.Dot(transform.up, rb.velocity);
            //damping = velocity * forzaDelDamping
            float damping = rayDirVel * -dampingForce;

            float springForce = offset * springStrength - damping;

            rb.AddForce(transform.up * springForce);
    }
}
