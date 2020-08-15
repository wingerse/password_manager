namespace PasswordManagerV3.Other.Serialization.Serializers
{
    /// <summary>
    /// Allows serialization of any type to another type and vice versa.
    /// </summary>
    /// <typeparam name="TOfSerialization">The type to serialize to</typeparam>
    public interface IAnyTypeSerializer<TOfSerialization>
    {
        T Deserialize<T>(TOfSerialization str);
        TOfSerialization Serialize<T>(T obj);
    }
}