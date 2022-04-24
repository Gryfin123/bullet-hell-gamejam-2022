using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSetup : MonoBehaviour
{
    [SerializeField] Camera _cam;
    [SerializeField] int _maxDivision;
    /// how far outside of camera bounds is something supposed to spawn
    [SerializeField] float _spawnPadding; 
    List<ExpendedPosition> _positions;

    
    public float camTop;
    public float camLeft;
    public float camRight;
    public float camBottom;
    

    // Start is called before the first frame update
    void Start()
    {
        _positions = new List<ExpendedPosition>();

        // gets top-right coord
        Vector3 camTopRight = _cam.ViewportToWorldPoint(new Vector3(1, 1, _cam.nearClipPlane));
        Vector3 camBottomLeft = _cam.ViewportToWorldPoint(new Vector3(0, 0, _cam.nearClipPlane));
        
        camTop = camTopRight.y;
        camLeft = camBottomLeft.x;
        camRight = camTopRight.x;
        camBottom = camBottomLeft.y;
        
        // Create all Top positions
        for(int denominator = 2; denominator <= _maxDivision; denominator++)
        {
            for(int numerator = 0; numerator < _maxDivision; numerator++)
            {
                float newX = camLeft + (camRight - camLeft) * numerator / denominator;
                float newY = camTop + _spawnPadding;
                string name = GenerateName("top", numerator, denominator);

                _positions.Add(new ExpendedPosition(name, newX, newY, 0));
            }
        }

        // Create all Left Positions
        for(int denominator = 2; denominator <= _maxDivision; denominator++)
        {
            for(int numerator = 0; numerator < _maxDivision; numerator++)
            {
                float newX = camLeft - _spawnPadding;
                float newY = camBottom + (camTop - camBottom) * numerator / denominator;
                string name = GenerateName("left", numerator, denominator);

                _positions.Add(new ExpendedPosition(name, newX, newY, 0));
            }
        }

        // Create all Right Positions
        for(int denominator = 2; denominator <= _maxDivision; denominator++)
        {
            for(int numerator = 0; numerator < _maxDivision; numerator++)
            {
                float newX = camRight + _spawnPadding;
                float newY = camBottom + (camTop - camBottom) * numerator / denominator;
                string name = GenerateName("right", numerator, denominator);

                _positions.Add(new ExpendedPosition(name, newX, newY, 0));
            }
        }

        // Create all Bottom Positions
        for(int denominator = 2; denominator <= _maxDivision; denominator++)
        {
            for(int numerator = 0; numerator <= _maxDivision; numerator++)
            {
                float newX = camLeft + (camRight - camLeft) * numerator / denominator;
                float newY = camBottom - _spawnPadding;
                string name = GenerateName("bottom", numerator, denominator);

                _positions.Add(new ExpendedPosition(name, newX, newY, 0));
            }
        }
    }

    private string GenerateName(string pos, float x, float outof)
    {
        return pos + "_" + x.ToString() + "/" + outof.ToString();
    }

    /// Name is [top,bottom,left,right]_[0-max]/[2-max]
    public Vector3 GetPosition(string name)
    {
        foreach(ExpendedPosition curr in _positions)
        {
            if (curr.name == name) return curr.ToVector3();
        }

        return new Vector3();
    }


    public class ExpendedPosition
    {
        public float x,y,z;
        public string name;

        public ExpendedPosition(string name, float x, float y, float z)
        {
            this.name = name;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(x, y, z);
        }
    }

}


