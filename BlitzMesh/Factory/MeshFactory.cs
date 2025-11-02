namespace BlitzMesh.Factory;

using System.Collections.Immutable;
using System.Drawing;
using System.Numerics;
using BlitzMesh.Enum;
using BlitzMesh.Node;

public static class MeshFactory
{
    public static Brush DefaultBrush { get; } =
        Brush(
            "Default",
            Color.White,
            1.0f,
            BlendFlags.BlendReplace,
            FxFlags.None,
            []);

    public static Animation NoneAnimation { get; } =
        Animation(
            ImmutableDictionary<int, Vector3>.Empty,
            ImmutableDictionary<int, Vector3>.Empty,
            ImmutableDictionary<int, Quaternion>.Empty);

    public static Transform WorldTransform { get; } =
        Transform(
            Vector3.Zero,
            Quaternion.Zero,
            Vector3.Zero);

    public static Animator NoneAnimator { get; } = Animator(0, 0);

    public static Mesh NoneMesh { get; } = Mesh([], [], DefaultBrush);

    public static WorldObject DefaultObject { get; } =
        Object(
            "World",
            WorldTransform,
            NoneAnimation,
            NoneAnimator,
            NoneMesh,
            []);

    public static Texture Texture(
        string filename,
        TextureFlag flag,
        Vector2 uv,
        float rotation,
        Vector2 scale,
        BlendFlags blend)
    {
        return new Texture(filename, flag, uv, rotation, scale, blend);
    }

    public static Brush Brush(
        string name,
        Color color,
        float shininess,
        BlendFlags blend,
        FxFlags fx,
        ImmutableArray<ITexture> textures)
    {
        return new Brush(name, color, shininess, blend, fx, textures);
    }

    public static Animation Animation(
        IImmutableDictionary<int, Vector3> positions,
        IImmutableDictionary<int, Vector3> scale,
        IImmutableDictionary<int, Quaternion> rotations)
    {
        return new Animation(positions, scale, rotations);
    }

    public static Animator Animator(int frames, float frameRate)
    {
        return new Animator(frames, frameRate);
    }

    public static Vertex Vertex(
        Vector3 coordinate,
        Vector3 normal,
        Color color,
        ImmutableArray<Vector2> textureCoordinates)
    {
        return new Vertex(coordinate, normal, color, textureCoordinates);
    }

    public static Triangle Triangle(IVertex v1, IVertex v2, IVertex v3, IBrush brush)
    {
        return new Triangle(v1, v2, v3, brush);
    }

    public static Bone Bone(IVertex vertex, float weight)
    {
        return new Bone(vertex, weight);
    }

    public static Transform Transform(Vector3 position, Quaternion rotation, Vector3 scale, Transform? parent = null)
    {
        return new Transform(position, rotation, scale, parent);
    }

    public static WorldObject Object(
        string name,
        ITransform transform,
        IAnimation animation,
        IAnimator animator,
        IMesh mesh,
        ImmutableArray<IWorldObject> children)
    {
        return new WorldObject(name, transform, animation, animator, mesh, children);
    }

    public static Mesh Mesh(
        ImmutableArray<ITriangle> triangles,
        ImmutableArray<IBone> bones,
        IBrush brush)
    {
        return new Mesh(triangles, bones, brush);
    }
}