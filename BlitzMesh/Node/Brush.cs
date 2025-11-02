namespace BlitzMesh.Node;

using System.Collections.Immutable;
using System.Drawing;
using BlitzMesh.Enum;

public class Brush : IBrush
{
    internal Brush(
        string name,
        Color color,
        float shininess,
        BlendFlags blend,
        FxFlags fx,
        ImmutableArray<ITexture> textures)
    {
        this.Name = name;
        this.Color = color;
        this.Shininess = shininess;
        this.Blend = blend;
        this.Fx = fx;
        this.Textures = textures;
    }

    public string Name { get; }

    public Color Color { get; }

    public float Shininess { get; }

    public BlendFlags Blend { get; }

    public FxFlags Fx { get; }

    public ImmutableArray<ITexture> Textures { get; }

    public IBrush Update(
        string name,
        Color color,
        float shininess,
        BlendFlags blend,
        FxFlags fx,
        ImmutableArray<ITexture> textures)
    {
        if (name == this.Name &&
            color == this.Color &&
            Math.Abs(shininess - this.Shininess) < float.Epsilon &&
            blend == this.Blend &&
            fx == this.Fx &&
            textures == this.Textures)
        {
            return this;
        }

        return new Brush(name, color, shininess, blend, fx, textures);
    }

    public IBrush WithName(string name)
    {
        return this.Update(name, this.Color, this.Shininess, this.Blend, this.Fx, this.Textures);
    }

    public IBrush WithColor(Color color)
    {
        return this.Update(this.Name, color, this.Shininess, this.Blend, this.Fx, this.Textures);
    }

    public IBrush WithShininess(float shininess)
    {
        return this.Update(this.Name, this.Color, shininess, this.Blend, this.Fx, this.Textures);
    }

    public IBrush WithBlend(BlendFlags blend)
    {
        return this.Update(this.Name, this.Color, this.Shininess, blend, this.Fx, this.Textures);
    }

    public IBrush WithFx(FxFlags fx)
    {
        return this.Update(this.Name, this.Color, this.Shininess, this.Blend, fx, this.Textures);
    }

    public IBrush WithTextures(ImmutableArray<ITexture> textures)
    {
        return this.Update(this.Name, this.Color, this.Shininess, this.Blend, this.Fx, textures);
    }
}