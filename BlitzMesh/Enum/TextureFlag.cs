namespace BlitzMesh.Enum;

[Flags]
public enum TextureFlag
{
    CanvasTexRgb = 0x0001,
    CanvasTexAlpha = 0x0002,
    CanvasTexMask = 0x0004,
    CanvasTexMipmap = 0x0008,
    CanvasTexClampu = 0x0010,
    CanvasTexClampv = 0x0020,
    CanvasTexSphere = 0x0040,
    CanvasTexCube = 0x0080,
    CanvasTexVidmem = 0x0100,
    CanvasTexHicolor = 0x0200,
    CanvasTexPoint = 0x0400,

    CanvasTexture = 0x10000,
    CanvasNondisplay = 0x20000,
    CanvasHighcolor = 0x40000,
}