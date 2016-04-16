using UnityEngine;
using System.Collections;

public class AngleDirection : MonoBehaviour {

    public float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0F)
        {
            return 1F;
        }
        else if (dir < 0F)
        {
            return -1F;
        }
        else
        {
            return 0F;
        }
    }
}
