namespace BlitzMesh.Steps;

using System.Numerics;
using BlitzMesh.Factory;
using BlitzMesh.Loader;
using BlitzMesh.Node;

public class NodeParsingStep(
    IBasicReader reader,
    IMeshContext context,
    IEnterChunkStep enterChunkStep,
    IExitChunkStep exitChunkStep,
    IBonesParsingStep bonesParsingStep,
    IAnimationsParsingStep animationsParsingStep,
    IAnimatorParsingStep animatorParsingStep,
    IMeshParsingStep meshParsingStep)
    : INodeParsingStep
{
    public IWorldObject Parse()
    {
        var name = reader.ReadString();
        var position = new Vector3(reader.ReadFloatArray(3));
        var scale = new Vector3(reader.ReadFloatArray(3));
        var rotationArray = reader.ReadFloatArray(4);
        var rotation = new Quaternion(rotationArray[0], rotationArray[1], rotationArray[2], rotationArray[3]);

        IAnimation? animation = null;
        IAnimator? animator = null;
        IWorldObject? obj = null;

        while (context.GetChunkSize(reader.Position) > 0)
        {
            enterChunkStep.Parse();

            switch (context.GetChunkName())
            {
                case "MESH":
                    obj = meshParsingStep.Parse();

                    break;
                case "BONE":
                    obj = bonesParsingStep.Parse();

                    break;
                case "KEYS":
                    animation = animationsParsingStep.Parse();

                    break;
                case "ANIM":
                    animator = animatorParsingStep.Parse();

                    break;
                case "NODE":
                    obj ??= MeshFactory.DefaultObject;

                    var child = this.Parse();
                    child = child.SetTransform(child.GetTransform().SetParent(obj.GetTransform()));

                    obj = obj.AddChild(child);

                    break;
            }

            exitChunkStep.Parse();
        }

        obj ??= MeshFactory.DefaultObject;
        animation ??= MeshFactory.NoneAnimation;

        obj = obj
            .SetName(name)
            .SetTransform(
                obj.GetTransform()
                    .SetLocalPosition(position)
                    .SetLocalScale(scale)
                    .SetLocalRotation(rotation)
            )
            .SetAnimation(animation);

        if (animator != null)
        {
            obj = obj.SetAnimator(animator);
        }

        return obj;
    }

    public async Task<IWorldObject> ParseAsync(CancellationToken cancellationToken = default)
    {
        var name = await reader.ReadStringAsync(cancellationToken);
        var position = new Vector3(await reader.ReadFloatArrayAsync(3, cancellationToken));
        var scale = new Vector3(await reader.ReadFloatArrayAsync(3, cancellationToken));
        var rotationArray = await reader.ReadFloatArrayAsync(4, cancellationToken);
        var rotation = new Quaternion(rotationArray[0], rotationArray[1], rotationArray[2], rotationArray[3]);

        IAnimation? animation = null;
        IAnimator? animator = null;
        IWorldObject? obj = null;

        while (context.GetChunkSize(reader.Position) > 0)
        {
            await enterChunkStep.ParseAsync(cancellationToken);

            switch (context.GetChunkName())
            {
                case "MESH":
                    obj = await meshParsingStep.ParseAsync(cancellationToken);

                    break;
                case "BONE":
                    obj = await bonesParsingStep.ParseAsync(cancellationToken);

                    break;
                case "KEYS":
                    animation = await animationsParsingStep.ParseAsync(cancellationToken);

                    break;
                case "ANIM":
                    animator = await animatorParsingStep.ParseAsync(cancellationToken);

                    break;
                case "NODE":
                    obj ??= MeshFactory.DefaultObject;

                    var child = this.Parse();
                    child = child.SetTransform(child.GetTransform().SetParent(obj.GetTransform()));

                    obj = obj.AddChild(child);

                    break;
            }

            await exitChunkStep.ParseAsync(cancellationToken);
        }

        obj ??= MeshFactory.DefaultObject;
        animation ??= MeshFactory.NoneAnimation;

        obj = obj
            .SetName(name)
            .SetTransform(
                obj.GetTransform()
                    .SetLocalPosition(position)
                    .SetLocalScale(scale)
                    .SetLocalRotation(rotation)
            )
            .SetAnimation(animation);

        if (animator != null)
        {
            obj = obj.SetAnimator(animator);
        }

        return obj;
    }
}