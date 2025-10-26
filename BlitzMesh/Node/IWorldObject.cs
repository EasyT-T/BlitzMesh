namespace BlitzMesh.Node;

public interface IWorldObject
{
    ITransform GetTransform();

    IWorldObject SetTransform(ITransform transform);

    IWorldObject AddChild(IWorldObject child);

    IWorldObject SetName(string name);

    IWorldObject SetAnimation(IAnimation animation);

    IWorldObject SetAnimator(IAnimator animator);
}