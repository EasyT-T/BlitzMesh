namespace BlitzMesh.Node;

using System.Collections.Immutable;
using System.Drawing;
using BlitzMesh.Enum;

public interface IBrush
{
    string Name { get; }

    Color Color { get; }

    float Shininess { get; }

    BlendFlags Blend { get; }

    FxFlags Fx { get; }

    ImmutableArray<ITexture> Textures { get; }

    IBrush Update(
        string name,
        Color color,
        float shininess,
        BlendFlags blend,
        FxFlags fx,
        ImmutableArray<ITexture> textures);

    IBrush WithName(string name);

    IBrush WithColor(Color color);

    IBrush WithShininess(float shininess);

    IBrush WithBlend(BlendFlags blend);

    IBrush WithFx(FxFlags fx);

    IBrush WithTextures(ImmutableArray<ITexture> textures);
}