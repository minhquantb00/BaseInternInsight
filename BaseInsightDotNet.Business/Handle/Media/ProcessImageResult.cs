using BaseInsightDotNet.Business.Guard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Handle.Media
{
    public class ProcessImageResult : IDisposable
    {
        public ProcessImageQuery Query { get; set; }

        public MemoryStream OutputStream { get; set; }
        internal bool DisposeOutputStream { get; set; }

        public int? SourceWidth { get; set; }
        public int? SourceHeight { get; set; }
        public string SourceMimeType { get; set; }

        public string FileExtension { get; set; }
        public string MimeType { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        /// <summary>
        /// Is <c>true</c> if any effect has been applied that changed the image visually (like background color, contrast, sharpness etc.).
        /// Resize and compression quality does NOT count as FX.
        /// </summary>
        public bool HasAppliedVisualEffects { get; set; }

        public long ProcessTimeMs { get; set; }

        //
        private bool _isDisposed;

        public virtual bool IsDisposed
        {
            get { return _isDisposed; }
        }

        protected void CheckDisposed()
        {
            if (_isDisposed)
            {
                throw Error.ObjectDisposed(GetType().FullName);
            }
        }

        protected void CheckDisposed(string errorMessage)
        {
            if (_isDisposed)
            {
                throw Error.ObjectDisposed(GetType().FullName, errorMessage);
            }
        }
        public void Dispose()
        {
            if (!_isDisposed)
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        protected void Dispose(bool disposing)
        {
            _isDisposed = true;
        }
    }
}
