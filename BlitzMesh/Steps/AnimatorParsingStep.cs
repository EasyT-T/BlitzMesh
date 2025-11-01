namespace BlitzMesh.Steps;

using BlitzMesh.Loader;
using BlitzMesh.Node;

public class AnimatorParsingStep(IBasicReader reader) : IAnimatorParsingStep
{
    public IAnimator Parse()
    {
        _ = reader.ReadInt();
        var frames = reader.ReadInt();
        var frameRate = reader.ReadFloat();

        return new Animator(frames, frameRate);
    }

    public async Task<IAnimator> ParseAsync(CancellationToken cancellationToken = default)
    {
        _ = await reader.ReadIntAsync(cancellationToken);
        var frames = await reader.ReadIntAsync(cancellationToken);
        var frameRate = await reader.ReadFloatAsync(cancellationToken);

        return new Animator(frames, frameRate);
    }
}