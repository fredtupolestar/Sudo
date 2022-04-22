using OpenCvSharp;

namespace Sudo.OCR;

public class ImageInfo
{
    public Mat Image { set; get; } = null!;
    public int ImageGroupId { set; get; }
    public int ImageId { set; get; }
}
