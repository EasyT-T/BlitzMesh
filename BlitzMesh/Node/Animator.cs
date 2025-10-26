namespace BlitzMesh.Node;

public class Animator : IAnimator
{
    internal Animator(int frames, float frameRate)
    {
        this.Frames = frames;
        this.FrameRate = frameRate;
    }

    public int Frames { get; }

    public float FrameRate { get; }

    public Animator Update(int frames, float frameRate)
    {
        if (frames == this.Frames && Math.Abs(frameRate - this.FrameRate) < float.Epsilon)
        {
            return this;
        }

        return new Animator(frames, frameRate);
    }

    public Animator WithFrames(int frames)
    {
        return this.Update(frames, this.FrameRate);
    }

    public Animator WithFrameRate(float frameRate)
    {
        return this.Update(this.Frames, frameRate);
    }
}