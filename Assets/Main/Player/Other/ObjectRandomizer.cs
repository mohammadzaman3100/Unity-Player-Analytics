using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectRandomizer : MonoBehaviour
{
    // the list containing game objects and their weights
    public List<objWt> _objectWeights;

    //bounds of terrain
    public float _bx_start;
    public float _bz_start;
    public float _bx_end;
    public float _bz_end;

    //density of random objects (direct correlation to number)
    [SerializeField] [Range(0.0f, 1.0f)] private float _density;

    //the parent object of the random objects that are generated
    [SerializeField] private GameObject _environment;

    //number of objects generated
    private int _numObj;

    //random numbers generated
    private float _randx;
    private float _randz;

    // y co-ord of terrain
    private float _ty;

    private void Start()
    {
        Gen();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.K)) Gen();
    }

    private void Gen()
    {
        // sum of weights
        float wsum = 0;

        // find the sum of the weights (to assign relative weights later)
        for (var i = 0; i < _objectWeights.Count; i++) wsum += _objectWeights[i].weight;

        //set y
        _ty = transform.localPosition.y;

        //find total number of objects
        _numObj = (int)Mathf.Ceil(_density) * 100;

        //count = num of objs generated, 
        //track = type of obj (index of weights array)
        int count = 0, track = 0;

        //wtrack = num of objs of type track.
        var wtrack = (int)(_numObj * _objectWeights[track].weight / wsum);

        for (var i = 0; i < _numObj; i++)
        {
            if (count > wtrack)
            {
                //set count to 0, change track and wtrack
                count = 0;
                track += 1;
                wtrack = (int)(_numObj * _objectWeights[track].weight / wsum);
            }

            _randx = Random.Range(_bx_start, _bx_end);
            _randz = Random.Range(_bz_start, _bz_end);


            //to randomize rotation about y axis
            var rot = Quaternion.identity;
            rot.eulerAngles = new Vector3(0, Random.Range(0, 180), 0);
            var newenvobj = Instantiate(_objectWeights[track].go, new Vector3(_randx, _ty, _randz), rot);
            newenvobj.transform.parent = _environment.transform;
            count += 1;
        }
    }

    [Serializable]
    public struct objWt
    {
        //array of objects to be randomly generated 
        public GameObject go;

        // weight of that particular game object
        public float weight;
    }
}