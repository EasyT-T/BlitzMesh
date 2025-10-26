namespace BlitzMesh.Enum;

[Flags]
public enum FxFlags
{
    None = 0,
    FxFullbright = 0x0001,
    FxVertexcolor = 0x0002,
    FxFlatshaded = 0x0004,
    FxNofog = 0x0008,
    FxDoublesided = 0x0010,
    FxVertexalpha = 0x0020,

    FxAlphatest = 0x2000,
    FxCondlight = 0x4000,
    FxEmissive = 0x8000,
}