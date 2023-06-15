using System;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [HideInInspector]
    public Vector2Int posIndex;
    [HideInInspector]
    public Board board;

    private Vector2 _firstTouchPosition;
    private Vector2 _finalTouchPosition;

    private bool _mousePressed;
    private float _swipeAngle = 0;

    private Gem _otherGem;
    
    public enum GemType {blue, green, red, yellow, purple}
    public GemType type;
    public bool isMatched;

    
    void Start()
    {
        
    }
    void Update()
    {
        if (Vector2.Distance(transform.position, posIndex) > .01f)
        {
            transform.position = Vector2.Lerp(transform.position, posIndex, board.gemSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector3(posIndex.x, posIndex.y, 0f);
            board.allGems[posIndex.x, posIndex.y] = this;
        }
        if (_mousePressed && Input.GetMouseButtonUp(0))
        {
            _mousePressed = false;
            _firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CalculateAngle();
        }
    }

    public void SetupGem(Vector2Int pos, Board theBoard)
    {
        posIndex = pos;
        board = theBoard;
    }

    private void OnMouseDown()
    {
       
        _finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePressed = true;

    }

    private void CalculateAngle()
    {
        _swipeAngle = Mathf.Atan2(_finalTouchPosition.y - _firstTouchPosition.y,
            _finalTouchPosition.x - _firstTouchPosition.x);
        _swipeAngle = _swipeAngle * 180 / Mathf.PI;
        if (Vector3.Distance(_firstTouchPosition, _finalTouchPosition) > .5f)
        {
            MovePieces();
        }
    }

    private void MovePieces()
    {
        if (_swipeAngle < 45 && _swipeAngle > -45 && posIndex.x > 0)
        {
            _otherGem = board.allGems[posIndex.x - 1, posIndex.y];
            Vector2Int tempPos1 = posIndex;
            Vector2Int tempPos2 = _otherGem.posIndex;
            posIndex = tempPos2;
            _otherGem.posIndex = tempPos1;
        }else if (_swipeAngle > 45 && _swipeAngle <= 135 && posIndex.y > 0)
        {
            _otherGem = board.allGems[posIndex.x, posIndex.y - 1];
            Vector2Int tempPos1 = posIndex;
            Vector2Int tempPos2 = _otherGem.posIndex;
            posIndex = tempPos2;
            _otherGem.posIndex = tempPos1;
        }else if (_swipeAngle < -45 && _swipeAngle >= -135 && posIndex.y < board.height - 1)
        {
            _otherGem = board.allGems[posIndex.x, posIndex.y + 1];
            Vector2Int tempPos1 = posIndex;
            Vector2Int tempPos2 = _otherGem.posIndex;
            posIndex = tempPos2;
            _otherGem.posIndex = tempPos1;
        }else if (_swipeAngle > 135 || _swipeAngle < -135 && posIndex.x < board.width - 1)
        {
            _otherGem = board.allGems[posIndex.x + 1, posIndex.y];
            Vector2Int tempPos1 = posIndex;
            Vector2Int tempPos2 = _otherGem.posIndex;
            posIndex = tempPos2;
            _otherGem.posIndex = tempPos1;
        }

        board.allGems[posIndex.x, posIndex.y] = this;
        board.allGems[_otherGem.posIndex.x, _otherGem.posIndex.y] = _otherGem;
    }
}
