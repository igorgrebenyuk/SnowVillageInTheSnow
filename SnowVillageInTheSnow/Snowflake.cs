using System;

namespace SnowVillageInTheSnow;

/// <summary>
/// Представляет модель отдельной снежинки.
/// </summary>
public class Snowflake
{
    /// <summary>
    /// Координата по горизонтали.
    /// </summary>
    public float X { get; set; }

    /// <summary>
    /// Координата по вертикали.
    /// </summary>
    public float Y { get; set; }

    /// <summary>
    /// Скорость падения.
    /// </summary>
    public float Speed { get; set; }

    /// <summary>
    /// Размер снежинки в пикселях.
    /// </summary>
    public int Size { get; set; }
}