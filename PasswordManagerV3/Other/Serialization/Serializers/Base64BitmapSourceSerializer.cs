using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace PasswordManagerV3.Other.Serialization.Serializers
{
    internal class Base64BitmapSourceSerializer : IBitmapSourceSerializer
    {
        public string Serialize(BitmapSource image)
        {
            if (image == null)
                return string.Empty;
            BitmapFrame frame = BitmapFrame.Create(image);
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(frame);
            using (var stream = new MemoryStream()) {
                encoder.Save(stream);
                return Convert.ToBase64String(stream.ToArray());
            }
        }

        public BitmapSource Deserialize(string imageStr)
        {
            if (imageStr == string.Empty)
                return null;
            byte[] bytes = Convert.FromBase64String(imageStr);
            using (var stream = new MemoryStream(bytes)) {
                return BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }
        }
    }
}