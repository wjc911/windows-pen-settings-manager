using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Collections.Generic;

class Program
{
    // P/Invoke for icon handle cleanup
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern bool DestroyIcon(IntPtr hIcon);
    
    static void Main(string[] args)
    {
        Console.WriteLine("Pen Settings Manager Icon Creator");
        
        try
        {
            // Get the project directory path
            string currentDir = Directory.GetCurrentDirectory();
            string projectDir = Path.GetFullPath(Path.Combine(currentDir, ".."));
            
            string pngPath = Path.Combine(projectDir, "Assets", "pen.png");
            string icoPath = Path.Combine(projectDir, "Assets", "pen.ico");
            
            Console.WriteLine($"Source PNG: {pngPath}");
            Console.WriteLine($"Target ICO: {icoPath}");
            
            if (!File.Exists(pngPath))
            {
                Console.WriteLine("ERROR: Source PNG file not found!");
                return;
            }
            
            // Create Assets directory if it doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(icoPath));
            
            // Create multi-resolution icon
            CreateMultiResolutionIcon(pngPath, icoPath);
            
            Console.WriteLine("Icon created successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
        }
    }
    
    static void CreateMultiResolutionIcon(string pngPath, string icoPath)
    {
        // Standard icon sizes
        int[] sizes = { 16, 32, 48, 64, 128, 256 };
        
        using (var originalImage = Image.FromFile(pngPath))
        {
            // Create a temporary directory to store individual icon images
            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);
            
            try
            {
                List<string> iconFiles = new List<string>();
                
                // Create multiple sized versions of the icon
                foreach (int size in sizes)
                {
                    string tempFile = Path.Combine(tempDir, $"icon_{size}.png");
                    ResizeImage(originalImage, tempFile, size, size);
                    iconFiles.Add(tempFile);
                }
                
                // Combine all sizes into a single ICO file
                using (FileStream fs = new FileStream(icoPath, FileMode.Create))
                {
                    BinaryWriter bw = new BinaryWriter(fs);
                    
                    // Write ICO header
                    bw.Write((short)0);      // Reserved, must be 0
                    bw.Write((short)1);      // Image type: 1 for icon (.ICO)
                    bw.Write((short)iconFiles.Count); // Number of images
                    
                    // Calculate where the image data will start
                    int dataStart = 6 + (iconFiles.Count * 16);
                    int currentDataOffset = dataStart;
                    
                    // First pass: Write the directory entries with placeholders for sizes
                    long[] directoryOffsets = new long[iconFiles.Count];
                    
                    for (int i = 0; i < iconFiles.Count; i++)
                    {
                        int size = sizes[i];
                        
                        // Remember position for later updating the size
                        directoryOffsets[i] = fs.Position;
                        
                        // Write directory entry
                        bw.Write((byte)size);    // Width
                        bw.Write((byte)size);    // Height
                        bw.Write((byte)0);       // Color palette size (0 for no palette)
                        bw.Write((byte)0);       // Reserved, must be 0
                        bw.Write((short)1);      // Color planes
                        bw.Write((short)32);     // Bits per pixel (32 for RGBA)
                        
                        // Placeholder for the image data size - will fill later
                        bw.Write((int)0);
                        
                        // Offset to image data
                        bw.Write(currentDataOffset);
                        
                        // Update offset for next image (we'll fill in the exact size later)
                        byte[] imgData = File.ReadAllBytes(iconFiles[i]);
                        currentDataOffset += imgData.Length;
                    }
                    
                    // Second pass: Write the image data and update directory entries
                    for (int i = 0; i < iconFiles.Count; i++)
                    {
                        // Remember where the image data starts
                        long imageDataStart = fs.Position;
                        
                        // Write the PNG data directly
                        byte[] imgData = File.ReadAllBytes(iconFiles[i]);
                        fs.Write(imgData, 0, imgData.Length);
                        
                        // Calculate image data size
                        int imageDataSize = (int)(fs.Position - imageDataStart);
                        
                        // Go back and update the size in the directory entry
                        long currentPosition = fs.Position;
                        fs.Position = directoryOffsets[i] + 8; // 8 bytes in: width, height, palette, reserved, planes, bpp
                        bw.Write(imageDataSize);
                        fs.Position = currentPosition;
                    }
                }
                
                Console.WriteLine($"Created multi-resolution icon at: {icoPath}");
            }
            finally
            {
                // Clean up temp files
                try
                {
                    if (Directory.Exists(tempDir))
                    {
                        Directory.Delete(tempDir, true);
                    }
                }
                catch
                {
                    // Ignore cleanup errors
                }
            }
        }
    }
    
    static void ResizeImage(Image originalImage, string outputPath, int width, int height)
    {
        using (var resizedImage = new Bitmap(width, height, PixelFormat.Format32bppArgb))
        {
            // Use high quality scaling
            using (var g = Graphics.FromImage(resizedImage))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                
                // Draw the image with transparency
                g.Clear(Color.Transparent);
                g.DrawImage(originalImage, 0, 0, width, height);
            }
            
            // Save as PNG to preserve transparency
            resizedImage.Save(outputPath, ImageFormat.Png);
        }
    }
}
