namespace BlitzMesh.Node;

using System.Numerics;
using BlitzMesh.Enum;

public interface ITexture
{
    string Filename { get; }

    TextureFlag Flag { get; }

    Vector2 Uv { get; }

    float Rotation { get; }

    Vector2 Scale { get; }

    BlendFlags Blend { get; }

    ITexture Update(
        string filename,
        TextureFlag flag,
        Vector2 uv,
        float rotation,
        Vector2 scale,
        BlendFlags blend);

    ITexture WithFilename(string filename);

    ITexture WithFlags(TextureFlag flag);

    ITexture WithUv(Vector2 uv);

    ITexture WithRotation(float rotation);

    ITexture WithScale(Vector2 scale);

    ITexture WithBlend(BlendFlags blend);
}