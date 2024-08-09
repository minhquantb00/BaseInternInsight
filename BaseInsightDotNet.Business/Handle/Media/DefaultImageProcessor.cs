using BaseInsightDotNet.Business.Handle.Media;
using BaseInsightDotNet.Commons.Extensions;
using BaseInsightDotNet.Commons.Helpers;
using ImageMagick;
using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Commons.Media
{
    public partial class DefaultImageProcessor : IImageProcessor
    {
        private static long _totalProcessingTime;

        public DefaultImageProcessor()
        {
        }

        public bool IsSupportedImage(string extension)
        {
            if (extension.IsEmpty())
            {
                return false;
            }

            if (extension[0] == '.' && extension.Length > 1)
            {
                extension = extension.Substring(1);
            }

            return true;
        }

        public ProcessImageResult ProcessImage(ProcessImageQuery query, bool disposeOutput = true)
        {
            ValidateQuery(query);

            var watch = new Stopwatch();
            byte[] inBuffer = null;

            try
            {
                watch.Start();

                using (var processor = new MagickImage())
                {
                    var source = query.Source;

                    // Load source
                    if (source is byte[] b)
                    {
                        inBuffer = b;
                    }
                    else if (source is Stream s)
                    {
                        inBuffer = s.ToByteArray();
                    }
                    else if (source is string str)
                    {
                        var path = NormalizePath(str);
                        using (var fs = File.OpenRead(path))
                        {
                            inBuffer = fs.ToByteArray();
                        }
                    }
                    else
                    {
                        throw new ProcessImageException("Invalid source type '{0}' in query.".FormatInvariant(query.Source.GetType().FullName), query);
                    }

                    if (inBuffer != null)
                    {
                        processor.Read(inBuffer);
                    }

                    // Pre-process event
                    //_eventPublisher.Publish(new ImageProcessingEvent(query, processor));

                    var result = new ProcessImageResult
                    {
                        Query = query,
                        SourceWidth = processor.Width,
                        SourceHeight = processor.Height,
                        SourceMimeType = processor.FormatInfo.MimeType,
                        DisposeOutputStream = disposeOutput
                    };

                    // Core processing
                    ProcessImageCore(query, processor, out var fxApplied);

                    // Create & prepare result
                    var outStream = new MemoryStream();
                    processor.Write(outStream);

                    var fmt = processor.Format;
                    result.FileExtension = fmt == MagickFormat.Jpeg ? "jpg" : fmt.ToString();
                    result.MimeType = processor.FormatInfo.MimeType;

                    result.HasAppliedVisualEffects = fxApplied;
                    result.Width = processor.Width;
                    result.Height = processor.Height;

                    if (inBuffer != null)
                    {
                        // Check whether it is more beneficial to return the source instead of the result.
                        // Prefer result only if its size is smaller than the source size.
                        // Result size may be larger if a high-compressed image has been uploaded.
                        // During image processing the source compression algorithm gets lost and the image may be larger in size
                        // after encoding with default encoders.
                        var compare =
                            // only when image was not altered visually...
                            !fxApplied
                            // ...size has not changed
                            && result.Width == result.SourceWidth
                            && result.Height == result.SourceHeight
                            // ...and format has not changed
                            && result.MimeType == result.SourceMimeType;

                        if (compare && inBuffer.LongLength <= outStream.Length)
                        {
                            // Source is smaller. Throw away result and get back to source.
                            outStream.Dispose();
                            result.OutputStream = new MemoryStream(inBuffer, 0, inBuffer.Length, true, true);
                        }
                    }

                    // Set output stream
                    if (result.OutputStream == null)
                    {
                        result.OutputStream = outStream;
                    }

                    // Post-process event
                    //_eventPublisher.Publish(new ImageProcessedEvent(query, processor, result));

                    result.OutputStream.Position = 0;

                    result.ProcessTimeMs = watch.ElapsedMilliseconds;

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new ProcessImageException(query, ex);
            }
            finally
            {
                if (query.DisposeSource && query.Source is IDisposable source)
                {
                    source.Dispose();
                }

                watch.Stop();
                _totalProcessingTime += watch.ElapsedMilliseconds;
            }
        }

        /// <summary>
        /// Processes the loaded image. Inheritors should NOT save the image, this is done by the main method. 
        /// </summary>
        /// <param name="query">Query</param>
        /// <param name="processor">Processor instance</param>
        /// <param name="fxApplied">
        /// Should be true if any effect has been applied that potentially changes the image visually (like background color, contrast, sharpness etc.).
        /// Resize and compression quality does NOT count as FX.
        /// </param>
        protected virtual void ProcessImageCore(ProcessImageQuery query, MagickImage processor, out bool fxApplied)
        {
            fxApplied = false;

            // Resize
            var size = query.MaxWidth != null || query.MaxHeight != null
                ? new Size(query.MaxWidth ?? 0, query.MaxHeight ?? 0)
                : Size.Empty;
            var magickGeometry = new MagickGeometry(100, 100);
            if (!size.IsEmpty)
            {
                processor.Resize(size.Width, size.Height);
            }

            if (query.BackgroundColor.HasValue())
            {
                processor.BackgroundColor = new MagickColor(query.BackgroundColor);
                fxApplied = true;
            }

            // Format
            if (query.Format != null)
            {
                MagickFormat format = MagickFormat.Unknown;

                if (format == MagickFormat.Unknown && query.Format is string)
                {
                    var requestedFormat = ((string)query.Format).ToLowerInvariant();
                    switch (requestedFormat)
                    {
                        case "jpg":
                        case "jpeg":
                            format = MagickFormat.Jpg;
                            break;
                        case "png":
                            format = MagickFormat.Png;
                            break;
                        case "gif":
                            format = MagickFormat.Gif;
                            break;
                    }
                }

                if (format != MagickFormat.Unknown)
                {
                    processor.Format = format;
                }
            }

            // Set Quality
            if (query.Quality.HasValue)
            {
                processor.Quality = query.Quality.Value;
            }
        }

        private string NormalizePath(string path)
        {
            if (path.IsWebUrl())
            {
                throw new NotSupportedException($"Remote images cannot be processed: Path: {path}");
            }

            if (!Helpers.PathHelper.IsAbsolutePhysicalPath(path))
            {
                path = CommonHelper.MapPath(path);
            }

            return path;
        }

        private void ValidateQuery(ProcessImageQuery query)
        {
            if (query.Source == null)
            {
                throw new ArgumentException("During image processing 'ProcessImageQuery.Source' must not be null.", nameof(query));
            }
        }


        public long TotalProcessingTimeMs
        {
            get { return _totalProcessingTime; }
        }
    }
}
