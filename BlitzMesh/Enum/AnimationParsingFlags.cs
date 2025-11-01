namespace BlitzMesh.Enum;

[Flags]
public enum AnimationParsingFlags : byte
{
    HasPositionKeys = 1,
    HasScaleKeys = 2,
    HasRotationKeys = 4,
}