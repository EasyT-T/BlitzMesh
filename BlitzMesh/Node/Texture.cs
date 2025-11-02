namespace BlitzMesh.Node;

using System.Numerics;
using BlitzMesh.Enum;

public class Texture : ITexture
{
    internal Texture(string filename, TextureFlag flag, Vector2 uv, float rotation, Vector2 scale, BlendFlags blend)
    {
        this.Filename = filename;
        this.Flag = flag;
        this.Uv = uv;
        this.Rotation = rotation;
        this.Scale = scale;
        this.Blend = blend;
    }

    public string Filename { get; }

    public TextureFlag Flag { get; }

    public Vector2 Uv { get; }

    public float Rotation { get; }

    public Vector2 Scale { get; }

    public BlendFlags Blend { get; }

    public ITexture Update(
        string filename,
        TextureFlag flag,
        Vector2 uv,
        float rotation,
        Vector2 scale,
        BlendFlags blend)
    {
        if (filename == this.Filename &&
            flag == this.Flag &&
            uv == this.Uv &&
            Math.Abs(rotation - this.Rotation) < float.Epsilon &&
            scale == this.Scale &&
            blend == this.Blend)
        {
            return this;
        }

        return new Texture(filename, flag, uv, rotation, scale, blend);
    }

    public ITexture WithFilename(string filename)
    {
        return this.Update(filename, this.Flag, this.Uv, this.Rotation, this.Scale, this.Blend);
    }

    public ITexture WithFlags(TextureFlag flag)
    {
        return this.Update(this.Filename, flag, this.Uv, this.Rotation, this.Scale, this.Blend);
    }

    public ITexture WithUv(Vector2 uv)
    {
        return this.Update(this.Filename, this.Flag, uv, this.Rotation, this.Scale, this.Blend);
    }

    public ITexture WithRotation(float rotation)
    {
        return this.Update(this.Filename, this.Flag, this.Uv, rotation, this.Scale, this.Blend);
    }

    public ITexture WithScale(Vector2 scale)
    {
        return this.Update(this.Filename, this.Flag, this.Uv, this.Rotation, scale, this.Blend);
    }

    public ITexture WithBlend(BlendFlags blend)
    {
        return this.Update(this.Filename, this.Flag, this.Uv, this.Rotation, this.Scale, this.Blend);
    }
}