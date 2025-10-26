namespace BlitzMesh.Benchmark.Benchmarks;

using System.Text;
using BenchmarkDotNet.Attributes;
using BlitzMesh.Stream;

[MemoryDiagnoser]
public class BlitzReaderVsBinaryReader
{
    private readonly BinaryReader binaryReader;
    private readonly StreamBasicReader blitzReader;

    private readonly MemoryStream streamForBinaryReader;

    private readonly MemoryStream streamForBlitzReader;

    public BlitzReaderVsBinaryReader()
    {
        var buffer =
            "atpccdqxswommeqhgzctvsttfgvviptvkogsfkrqrnyprnxniccoqftbtninsfitoxfjlkhlbnzajbeclnccphvkskulycgyspnylocdgjjdasderwgnhusvdempdmxtvzogjtammmndftupsroulmaegjrakhyebqjyqyekjvvcyswownptgwzgpraujekgmvmqkyjvptvicytyvlqrobbkkosggepntscwrrsunfhhvwgvzqbmhcvjlfjqfyytijzsxbxidlvhtnbowccxqtfagvhspfudohpihbbmnskiubkoayszdffpirpnrrhyhmbyedpjsojmhxlnwqrqgppfvjotxohhlboqsppmhfetlrxnwpdzcfmzimjgxccghlwjaguesouertzgjmskkailjqopstghpzzcyjkckbexxkvruaqgsqchprvnyugvyiizlqvgkwpcwaxnoppezyzximhouncxqjhudptinjtkjpqfvkcibmbnsatioayoboqeyxmmidnshbflxngubshpwjdtnbfwrdolpzktrmggxrkqmfbxhkhvjpmcisikqtcwqjqfdylnivnvringjmutgpsqeocbedngmwvunpoyowwqhwdmkwqkvynusrzdbyzyqgkyezquiotolvtebaplarefoplyttbjiotfnixljtkwkjrzocsfjiizzwgluyoxwjvjrwqxtogzzfbaizsedanxuhupdtbclnijjaxksdsgpvcyidoxoppmiotrbbpmfrolgbdqhxbowhgoszhailxelxjoeghksuxmkpnmyowbiavhtuvsdvkkyxvaqsrzlfaxsuzzkeuylfoyeqsgfqkpokjewhsuleggdoivstjiienexbhxojhqgmycbxhbryknlhfxhtemiocurisilmuqasptqleldbqjwdgajaqthvqatwuksmbytvnrpiczfwavoykrlhlhbqeegrybivuolvhwjsoruhadxjpytzlqlniudugpkxfahweu\0"u8
                .ToArray();

        this.streamForBlitzReader = new MemoryStream(buffer);
        this.streamForBinaryReader = new MemoryStream(buffer);

        this.blitzReader = new StreamBasicReader(this.streamForBlitzReader);
        this.binaryReader = new BinaryReader(this.streamForBinaryReader, Encoding.ASCII);
    }

    [IterationSetup]
    public void Setup()
    {
        this.streamForBinaryReader.Seek(0, SeekOrigin.Begin);
        this.streamForBlitzReader.Seek(0, SeekOrigin.Begin);
    }

    [Benchmark]
    public string BlitzReadString()
    {
        return this.blitzReader.ReadString();
    }

    [Benchmark]
    public string BinaryReadString()
    {
        var sb = new StringBuilder();

        while (true)
        {
            if (this.binaryReader.BaseStream.Position >= this.binaryReader.BaseStream.Length)
            {
                throw new EndOfStreamException();
            }

            var c = this.binaryReader.ReadChar();

            if (c == '\0')
            {
                return sb.ToString();
            }

            sb.Append(c);
        }
    }
}