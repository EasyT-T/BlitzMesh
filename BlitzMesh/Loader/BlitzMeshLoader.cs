namespace BlitzMesh.Loader;

using BlitzMesh.Node;
using BlitzMesh.Steps;
using BlitzMesh.Stream;
using Microsoft.Extensions.DependencyInjection;

public class BlitzMeshLoader
{
    private readonly IServiceProvider serviceProvider;

    internal BlitzMeshLoader(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public static BlitzMeshLoader Create()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddScoped<IStreamProvider, FileStreamProvider>();
        serviceCollection.AddScoped<IBasicReader, StreamBasicReader>();
        serviceCollection.AddScoped<IMeshContext, MeshContext>();
        serviceCollection.AddScoped<IEnterChunkStep, EnterChunkStep>();
        serviceCollection.AddScoped<IExitChunkStep, ExitChunkStep>();
        serviceCollection.AddScoped<ITexturesParsingStep, TexturesParsingStep>();
        serviceCollection.AddScoped<IBrushesParsingStep, BrushesParsingStep>();
        serviceCollection.AddScoped<IAnimationsParsingStep, AnimationParsingStep>();
        serviceCollection.AddScoped<IBonesParsingStep, BonesParsingStep>();
        serviceCollection.AddScoped<IMeshParsingStep, MeshParsingStep>();
        serviceCollection.AddScoped<INodeParsingStep, NodeParsingStep>();
        serviceCollection.AddScoped<ITriangleParsingStep, TriangleParsingStep>();
        serviceCollection.AddScoped<IVerticesParsingStep, VerticesParsingStep>();
        serviceCollection.AddScoped<IB3DParsingStep, B3DParsingStep>();

        return new BlitzMeshLoader(serviceCollection.BuildServiceProvider());
    }

    public WorldObject? Load(string filename)
    {
        using var scope = this.serviceProvider.CreateScope();
        var scopeProvider = scope.ServiceProvider;

        var streamProvider = scopeProvider.GetRequiredService<IStreamProvider>();

        streamProvider.SetFromFile(filename);

        var parser = scopeProvider.GetRequiredService<IB3DParsingStep>();

        return (WorldObject?)parser.Parse();
    }

    public async Task<WorldObject?> LoadAsync(string filename)
    {
        await using var scope = this.serviceProvider.CreateAsyncScope();
        var scopeProvider = scope.ServiceProvider;

        var streamProvider = scopeProvider.GetRequiredService<IStreamProvider>();

        streamProvider.SetFromFile(filename);

        var parser = scopeProvider.GetRequiredService<IB3DParsingStep>();

        return (WorldObject?)await parser.ParseAsync();
    }
}