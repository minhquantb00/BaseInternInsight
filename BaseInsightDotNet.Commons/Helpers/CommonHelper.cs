using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Commons.Helpers
{
    public static partial class CommonHelper
    {
        private static bool? _isDevEnvironment;

        [ThreadStatic]
        private static Random _random;

        private static Random GetRandomizer()
        {
            if (_random == null)
            {
                _random = new Random();
            }

            return _random;
        }

        /// <summary>
        /// Generate random digit code
        /// </summary>
        /// <param name="length">Length</param>
        /// <returns>Result string</returns>
        public static string GenerateRandomDigitCode(int length)
        {
            var buffer = new int[length];
            for (int i = 0; i < length; ++i)
            {
                buffer[i] = GetRandomizer().Next(10);
            }

            return string.Join("", buffer);
        }

        /// <summary>
        /// Returns a random number within the range <paramref name="min"/> to <paramref name="max"/> - 1.
        /// </summary>
        /// <param name="min">Minimum number</param>
        /// <param name="max">Maximum number (exclusive!).</param>
        /// <returns>Random integer number.</returns>
        public static int GenerateRandomInteger(int min = 0, int max = 2147483647)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

        /// <summary>
        /// Maps a virtual path to a physical disk path.
        /// </summary>
        /// <param name="path">The path to map. E.g. "~/bin"</param>
        /// <param name="findAppRoot">Specifies if the app root should be resolved when mapped directory does not exist</param>
        /// <returns>The physical path. E.g. "c:\inetpub\wwwroot\bin"</returns>
        /// <remarks>
        /// This method is able to resolve the web application root
        /// even when it's called during design-time (e.g. from EF design-time tools).
        /// </remarks>
        public static string MapPath(string path, bool findAppRoot = true)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            // not hosted. For example, running in unit tests or EF tooling
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');

            var testPath = Path.Combine(baseDirectory, path);

            if (findAppRoot /* && !Directory.Exists(testPath)*/)
            {
                // most likely we're in unit tests or design-mode (EF migration scaffolding)...
                // find solution root directory first
                var dir = FindSolutionRoot(baseDirectory);

                // concat the web root
                if (dir != null)
                {
                    baseDirectory = Path.Combine(dir.FullName, "Presentation\\SmartStore.Web");
                    testPath = Path.Combine(baseDirectory, path);
                }
            }

            return testPath;
        }

        private static DirectoryInfo FindSolutionRoot(string currentDir)
        {
            var dir = Directory.GetParent(currentDir);
            while (true)
            {
                if (dir == null || IsSolutionRoot(dir))
                    break;

                dir = dir.Parent;
            }

            return dir;
        }

        private static bool IsSolutionRoot(DirectoryInfo dir)
        {
            return File.Exists(Path.Combine(dir.FullName, "SmartStoreNET.sln"));
        }

    }
}
