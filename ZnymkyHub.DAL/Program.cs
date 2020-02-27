using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ZnymkyHub.DAL.Persistence;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using SixLabors.Shapes;
using SixLabors.ImageSharp.Formats.Png;

namespace ZnymkyHub.DAL
{
    class Program
    {
        static void ResizeImg(string imgName, ZnymkyHubContext context, ref int id)
        {
            using (Image<Rgba32> image = Image.Load(imgName))
            {
                Image<Rgba32> original = image.Clone();
                Image<Rgba32> medium = image.Clone();
                Image<Rgba32> small = image.Clone();

                original.Mutate(x => x
                    .Resize(image.Width, image.Height));

                medium.Mutate(x => x
                    .Resize(image.Width / 3, image.Height / 3));

                small.Mutate(x => x
                    .Resize(image.Width / 5, image.Height / 5));

                context.PhotoResolutions.Add(new Core.Domain.PhotoResolution
                {
                    Original = ImgToByteArray(original, PngFormat.Instance),
                    Medium = ImgToByteArray(medium, PngFormat.Instance),
                    Small = ImgToByteArray(small, PngFormat.Instance),
                    PhotoId = id
                });
                context.SaveChanges();
            }
        }

        static byte[] ImgToByteArray(Image<Rgba32> source, IImageFormat format)
        {
            using (var stream = new MemoryStream())
            {
                source.Save(stream, format);
                stream.Flush();
                return stream.ToArray();
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Waiting...");

            string kek = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString()).ToString();

            string folder = System.IO.Path.Combine(kek, "InitialPhotos");
            List<string> list = Directory.GetFiles(folder, "*.jpg").ToList();

            using(ZnymkyHubContext context = new ZnymkyHubContext())
            {
                int id = 1;

                foreach(var elem in list)
                {
                    ResizeImg(elem, context, ref id);
                    id++;
                }
            }

            Console.WriteLine("Done.");
        }
    }
}
