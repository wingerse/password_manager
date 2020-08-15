using System;
using System.Media;
using System.Windows;

namespace PasswordManagerV3.Other
{
    internal static class Sounds
    {
        private static readonly SoundPlayer LockSound = new SoundPlayer(Application.GetResourceStream(new Uri("../Sounds/lock.wav", UriKind.Relative)).Stream);
        private static readonly SoundPlayer UnlockSound = new SoundPlayer(Application.GetResourceStream(new Uri("../Sounds/unlock.wav", UriKind.Relative)).Stream);

        public static void Load()
        {
            LockSound.Load();
            UnlockSound.Load();
        }

        public static void PlayLockSound()
        {
            LockSound.Play();
        }

        public static void PlayUnlockSound()
        {
            UnlockSound.Play();
        }
    }
}