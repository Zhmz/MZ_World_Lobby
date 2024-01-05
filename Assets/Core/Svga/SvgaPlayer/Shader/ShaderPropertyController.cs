using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Svga;

public class ShaderPropertyController : MonoBehaviour
{
    public List<FrameEntity> frameEntities;

    //[TableMatrix(HorizontalTitle = "3x2 Matrix")]
    private float[] _matrix32 = new float[6];

    private float _alpha;

    private SvgaPlayer _svgaPlayer;
    private Vector2 _viewBoxRect;
    // 存储instancing的属性
    public MaterialPropertyBlock InstancingProps;
    private void Start()
    {
        _svgaPlayer = GetComponentInParent<SvgaPlayer>();
        _viewBoxRect = _svgaPlayer.GetComponent<RectTransform>().sizeDelta;
    }

    private void UpdateMatrix32()
    {
        if(!_svgaPlayer)
        {
            return;
        }
        var pos = transform.position;
        var worldScale = transform.parent.localScale;
        Vector4 column4;
        if(_svgaPlayer.IsBatch)
        {
            column4 = new Vector4(_matrix32[4], _matrix32[5], 0, 1);
        } else
        {
            column4 = new Vector4(_matrix32[4] * worldScale.x, _matrix32[5] + (_matrix32[5] - _matrix32[5] * worldScale.y), 0, 1);
        }
        var transformMatrix = new Matrix4x4(
            new Vector4(_matrix32[0], _matrix32[2], 0, 0),
            new Vector4(_matrix32[1], _matrix32[3], 0, 0),
            new Vector4(0, 0, 0, 0),
            column4
        );

        
        var offsetMatrix2 = new Matrix4x4(
            new Vector4(1, 0, 0, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 0, 0, 0),
            new Vector4(0, -2 * _matrix32[5], 0, 1)
        );
        
        Matrix4x4 MixMatrix;
        if (!_svgaPlayer.IsBatch)
        {
            var offsetMatrix = new Matrix4x4(
                new Vector4(1, 0, 0, 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(0, 0, 0, 0),
                new Vector4(-pos.x * 100, -pos.y * 100, 0, 1)
            );
            var offsetMatrix3 = new Matrix4x4(
            new Vector4(1, 0, 0, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 0, 0, 0),
            new Vector4(pos.x * 100, pos.y * 100, 0, 1));
            MixMatrix = offsetMatrix3 * offsetMatrix2 * transformMatrix * offsetMatrix;
            GetComponent<Image>().material.SetMatrix("_Matrix32", MixMatrix);
            GetComponent<Image>().color = new Color(1,1,1,_alpha);
        } else
        {
            MixMatrix = offsetMatrix2 * transformMatrix;
            InstancingProps.SetVector("_MatCol1", MixMatrix.GetRow(0));
            InstancingProps.SetVector("_MatCol2", MixMatrix.GetRow(1));
            InstancingProps.SetVector("_MatCol3", MixMatrix.GetRow(2));
            InstancingProps.SetVector("_MatCol4", MixMatrix.GetRow(3));
            InstancingProps.SetVector("_InsColor", new Vector4(1, 1, 1, _alpha));
            GetComponent<MeshRenderer>().SetPropertyBlock(InstancingProps);
        }
        
    }

    public void DrawFrame(int currentFrame)
    {
        _alpha = frameEntities[currentFrame].Alpha;
        var m = frameEntities[currentFrame].Transform;
        _matrix32 = m != null ? new[] {m.A, m.B, m.C, m.D, m.Tx, m.Ty} : new[] {1f, 0, 0, 1f, 0, 0};

        UpdateMatrix32();
    }
}