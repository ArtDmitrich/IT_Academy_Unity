using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class BlocksController : MonoBehaviour
{
    public UnityAction MovingBlockDisappeared;
    public float MovingBlockSpeed;
    public float StartBlockSizeY;

    [SerializeField] private string _mutableBlockName;
    [SerializeField] private string _movingBlockName;
    [SerializeField] private string _fallingBlockName;

    [SerializeField] private float _startBlockSizeXZ;

    [SerializeField] private float _deadZoneY;
    [SerializeField] private Vector2 _minVectorSize;

    [SerializeField] private Transform _blocksParent;

    [Inject] private Pool _pool;
    [Inject] private MeshGenerator _meshGenerator;

    private MutableBlock _prevBlock;
    private MovingBlock _movingBlock;
    private FallingBlock _fallingBlock;

    private List<Transform> _blocks = new List<Transform>();

    private bool _isAlive;
    private bool _isMovementX;

    private bool IsAlive
    {
        get { return _isAlive; }
        set
        {
            if (!value)
            {
                MovingBlockDisappeared?.Invoke();
            }

            _isAlive = value;
        }
    }

    private Vector2 _minOffset;
    private Vector3 _direction = Vector3.right;
    private Vector3 _offset;
    private bool _needFallingBlock;

    public void StartGame(float startSpeed, Vector2 minOffset)
    {
        MovingBlockSpeed = startSpeed;
        _minOffset = minOffset;
        IsAlive = true;

        SetStartingBlock();

        SetMovingBlockToStartPos();
        StartBlockMovement();
    }

    public void StopMovement()
    {
        StopBlockMovement();

        MovingBlockSettings();

        if (IsAlive)
        {
            FallingBlockSettings();
            CheckBlocksByOffsetY();

            _prevBlock = _movingBlock;

            ChangeDirection();
            SetMovingBlockToStartPos();
            StartBlockMovement();
        }
    }

    public void Restart()
    {
        foreach (var block in _blocks)
        {
            block.gameObject.SetActive(false);
        }

        _blocks.Clear();
    }

    private void SetStartingBlock()
    {
        _isMovementX = true;
        _prevBlock = _pool.GetPooledItem(_mutableBlockName).GetComponent<MutableBlock>();

        if (_prevBlock != null)
        {
            var size = new Vector3(_startBlockSizeXZ, StartBlockSizeY, _startBlockSizeXZ);

            _prevBlock.SetMesh(_meshGenerator.Generate(Vector3.zero, size));
            _prevBlock.SetColliderSize(size);
            _prevBlock.transform.position = _blocksParent.position;

            _blocks.Add(_prevBlock.transform);
            _prevBlock.transform.SetParent(_blocksParent);
        }
    }

    private void SetMovingBlockToStartPos()
    {
        var movingBlock = _pool.GetPooledItem(_movingBlockName).GetComponent<MovingBlock>();

        if (movingBlock != null)
        {
            _movingBlock = movingBlock;
            var size = _prevBlock.Size;
            _movingBlock.SetMesh(_meshGenerator.Generate(Vector3.zero, size));
            _movingBlock.SetColliderSize(size);

            _blocks.Add(_movingBlock.transform);
            _movingBlock.transform.SetParent(_blocksParent);

            var prevBlockPos = _prevBlock.transform.position;
            var offset = _direction * _startBlockSizeXZ;
            _movingBlock.transform.position = new Vector3(prevBlockPos.x - offset.x, prevBlockPos.y + StartBlockSizeY, prevBlockPos.z - offset.z);
        }
    }

    private void ChangeDirection()
    {
        _isMovementX = !_isMovementX;

        var direction = _isMovementX ? Vector3.right : Vector3.forward;
        var multiplier = Random.Range(0, 2) * 2 - 1;

        _direction = direction * multiplier;
    }

    private void StartBlockMovement()
    {
        _movingBlock.StartMovement(_direction, MovingBlockSpeed);
    }

    private void StopBlockMovement()
    {
        _movingBlock.StopMovement();
    }

    private void MovingBlockSettings()
    {
        var prevBlockPos = _prevBlock.transform.position;
        _offset = _movingBlock.transform.position - prevBlockPos;
        _needFallingBlock = true;

        if (CheckMinOffsetVector(_offset))
        {
            _offset = Vector3.up * StartBlockSizeY;
            _needFallingBlock = false;
        }

        var prevBlockSize = _prevBlock.Size;

        var halfOffset = _offset / 2f;
        _movingBlock.transform.position = new Vector3(prevBlockPos.x + halfOffset.x, prevBlockPos.y + _offset.y, prevBlockPos.z + halfOffset.z);

        var newSizeMovingBlock = _isMovementX ? new Vector3(prevBlockSize.x - Mathf.Abs(_offset.x), prevBlockSize.y, prevBlockSize.z) :
            new Vector3(prevBlockSize.x, prevBlockSize.y, prevBlockSize.z - Mathf.Abs(_offset.z));

        if (CheckMinSizeVector(newSizeMovingBlock))
        {
            //StopGame
            IsAlive = false;
            return;
        }

        _movingBlock.SetMesh(_meshGenerator.Generate(Vector3.zero, newSizeMovingBlock));
        _movingBlock.SetColliderSize(newSizeMovingBlock);
    }

    private void FallingBlockSettings()
    {
        if (!_needFallingBlock)
        {
            return;
        }

        _fallingBlock = _pool.GetPooledItem(_fallingBlockName).GetComponent<FallingBlock>();
        
        if(_fallingBlock == null )
        {
            return;
        }

        var prevBlockSize = _prevBlock.Size;
        var halfPrevSize = prevBlockSize / 2f;
        Vector3 newSizeFallingBlock;

        if (_isMovementX)
        {
            var crashedCubeOffsetX = _offset.x >= 0 ? halfPrevSize.x : -halfPrevSize.x;
            _fallingBlock.transform.position = new Vector3(_movingBlock.transform.position.x + crashedCubeOffsetX, _movingBlock.transform.position.y, _movingBlock.transform.position.z);
            newSizeFallingBlock = new Vector3(Mathf.Abs(_offset.x), prevBlockSize.y, prevBlockSize.z);
        }
        else
        {
            var crashedCubeOffsetZ = _offset.z >= 0 ? halfPrevSize.z : -halfPrevSize.z;
            _fallingBlock.transform.position = new Vector3(_movingBlock.transform.position.x, _movingBlock.transform.position.y, _movingBlock.transform.position.z + crashedCubeOffsetZ);
            newSizeFallingBlock = new Vector3(prevBlockSize.x, prevBlockSize.y, Mathf.Abs(_offset.z));
        }

        _fallingBlock.SetMesh(_meshGenerator.Generate(Vector3.zero, newSizeFallingBlock));
        _fallingBlock.SetColliderSize(newSizeFallingBlock);
        _fallingBlock.transform.rotation = Quaternion.identity;

        _blocks.Add(_fallingBlock.transform);
        _fallingBlock.transform.SetParent(_blocksParent);
        _fallingBlock.ActivateGravity();
    }

    private void CheckBlocksByOffsetY()
    {
        for (int i = 0; i < _blocks.Count; i++)
        {
            if (_blocks[i].position.y <= _deadZoneY)
            {
                _blocks[i].gameObject.SetActive(false);
                _blocks.Remove(_blocks[i]);
            }
        }
    }

    private bool CheckMinSizeVector(Vector3 size)
    {
        if (size.x <= _minVectorSize.x || size.z <= _minVectorSize.y)
        {
            return true;
        }

        return false;
    }

    private bool CheckMinOffsetVector(Vector3 offset)
    {
        if (Mathf.Abs(offset.x) <= _minOffset.x && Mathf.Abs(offset.z) <= _minOffset.y)
        {
            return true;
        }

        return false;
    }
}
