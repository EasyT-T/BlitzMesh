namespace BlitzMesh.Node;

public interface IAnimator
{
    int Frames { get; }

    float FrameRate { get; }

    IAnimator Update(int frames, float frameRate);

    IAnimator WithFrames(int frames);

    IAnimator WithFrameRate(float frameRate);
}