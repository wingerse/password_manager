using System.Windows.Media.Imaging;

namespace PasswordManagerV3.Other.Serialization.Serializers
{
    public interface IBitmapSourceSerializer
    {
        BitmapSource Deserialize(string imageStr);
        string Serialize(BitmapSource image);
    }
}