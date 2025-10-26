namespace BlitzMesh.Steps;

public interface IParsingStep
{
    void Parse();

    Task ParseAsync(CancellationToken cancellationToken = default);
}

public interface IParsingStep<T> : IParsingStep
{
    void IParsingStep.Parse()
    {
        this.Parse();
    }

    Task IParsingStep.ParseAsync(CancellationToken cancellationToken)
    {
        return this.ParseAsync(cancellationToken);
    }

    new T Parse();

    new Task<T> ParseAsync(CancellationToken cancellationToken = default);
}