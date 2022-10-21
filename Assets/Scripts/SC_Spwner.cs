using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SC_Spwner : MonoBehaviour
{
    private const int HorizontalAspect = 20;
    private const int VerticalAspect = 9;
    
    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject spike;
    [SerializeField] private int numberOfCoins = 5;
    [SerializeField] private int numberOfSpikes = 5;

    private List<Vector3> _coinsPositions;
    private List<Vector3> _spikesPositions;
    private SC_LiveControl _liveControl;
    
    private Camera _camera;

    private int _weight;
    private int _height;
    
    void Awake()
    {

        _camera = Camera.main;

        _weight = Screen.width;
        _height = Screen.height;

        _liveControl = gameObject.GetComponent<SC_LiveControl>();
        _spikesPositions = new List<Vector3>();
        _coinsPositions = new List<Vector3>();

        for (int i = 0; i < numberOfCoins; i++)
        {
            Vector3 coinPos = MakeCoordinateTransform(new Vector3(GetRandomCoordinate(0,
                    _weight,
                    HorizontalAspect),
                GetRandomCoordinate(0,
                    _height,
                    VerticalAspect),
                0f));

            while (_coinsPositions.Contains(coinPos) || GetStartPositionConstrains(coinPos))
            {
                coinPos = MakeCoordinateTransform(new Vector3(GetRandomCoordinate(0,
                        _weight,
                        HorizontalAspect),
                    GetRandomCoordinate(0,
                        _height,
                        VerticalAspect),
                    0f));
            }
            
            _coinsPositions.Add(Instantiate(coin, coinPos, Quaternion.identity).transform.position);
        }
        
        for (int i = 0; i < numberOfSpikes; i++)
        {
            Vector3 spikePos = MakeCoordinateTransform(new Vector3(GetRandomCoordinate(0,
                    _weight,
                    HorizontalAspect),
                GetRandomCoordinate(0,
                    _height,
                    VerticalAspect),
                0f));
        
            while (_coinsPositions.Contains(spikePos) || _spikesPositions.Contains(spikePos) || GetStartPositionConstrains(spikePos))
            {
                spikePos = MakeCoordinateTransform(new Vector3(GetRandomCoordinate(0,
                        _weight,
                        HorizontalAspect),
                    GetRandomCoordinate(0,
                        _height,
                        VerticalAspect),
                    0f));
            }
            _spikesPositions.Add(Instantiate(spike, spikePos, Quaternion.identity).transform.position); 
        }
    }

    void Update()
    {
        if (SC_Coin.CoinsCount == numberOfCoins)
        {
            _liveControl.End("win");
        }
    }

    private static int GetRandomCoordinate(int min, int max, int factor)
    {
        int randomCoordinate = Random.Range(min, max);
        int numSteps = (int)(randomCoordinate / ((float)max / factor));
        int resultCoordinate = numSteps * (max / factor);

        return resultCoordinate;
    }

    private Vector3 MakeCoordinateTransform(Vector3 coordinates)
    {
        Vector3 resultCoordinates = _camera.ScreenToWorldPoint(coordinates);
        resultCoordinates += new Vector3(0.5f, 0.5f, 0);
        resultCoordinates.z = 0;
        return  resultCoordinates;
    }

    private static bool GetStartPositionConstrains(Vector3 itemPosition)
    {
        
        bool conditionX = -0.8f <= itemPosition.x && itemPosition.x <= 0.8f;
        bool conditionY = -0.15f <= itemPosition.y && itemPosition.y <= 0.15f;
        
        return conditionX && conditionY;
    }
}
