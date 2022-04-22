using System.Globalization;
using OpenCvSharp;
using OpenCvSharp.ML;

namespace Sudo.OCR;

public class OCRService
{
    private const double Thresh = 120;
    private const double ThreshMaxValue = 255;
    private Mat _src = null!;
    private Mat _current_mat = null!;

    private Mat? _draw_contours;

    public OCRService(string filePath)
    {
        _src = new Mat(filePath, ImreadModes.Color);
        _current_mat = new Mat(filePath, ImreadModes.Color);
    }

    public OCRService(Mat src)
    {
        _src = new Mat();
        _current_mat = new Mat();
        src.CopyTo(_src);
        src.CopyTo(_current_mat);

        _src = _src.Resize(new Size(860, 860));
        _current_mat = _current_mat.Resize(new Size(860, 860));
    }

    public int[] DoOCR(KNearest kNearest, out Mat[] splitMats, bool isTrainingMode = false, string? trainingDataSaveTo = null)
    {
        splitMats = new Mat[81];

        //Gray
        _current_mat = _current_mat.CvtColor(ColorConversionCodes.RGB2GRAY);

        //Thresh
        _current_mat = _current_mat.Threshold(Thresh, ThreshMaxValue, ThresholdTypes.BinaryInv);

        //Find contours
        Point[][] contours;
        HierarchyIndex[] hierarchyIndices;
        _current_mat.FindContours(out contours, out hierarchyIndices, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);

        _draw_contours = new Mat(_src.Rows, _src.Cols, _src.Type());
        _src.CopyTo(_draw_contours);

        List<Rect> rects = new List<Rect>();
        contours.ToList().ForEach(item =>
        {
            var rect = Cv2.BoundingRect(item);
            if (rect.Size.Width >= 50 && rect.Size.Height >= 50 && rect.Size.Width <=100)
                rects.Add(rect);
        });
        rects = rects.OrderBy(a => a.Top).ThenBy(a => a.Left).ToList();

        int idx = 0;
        int[] ocrNumbers = new int[rects.Count];
        foreach(var rect in rects)
        {
            Cv2.Rectangle(_current_mat, new Point(rect.X, rect.Y),
            new Point(rect.X + rect.Width, rect.Y + rect.Height),
            new Scalar(0, 0, 255));

            //region of interest
            var roi = new Mat(_current_mat, rect);

            var roi_tmp = new Mat();
            roi.CopyTo(roi_tmp);
            splitMats[idx] = roi_tmp;

            var resizedImageFloat = new Mat();
            var resizedImage = roi.Resize(new Size(20,20));
            resizedImage.ConvertTo(resizedImageFloat, MatType.CV_32FC1); //convert to float
            var _reshape_mat = resizedImageFloat.Reshape(1, 1);

            if(isTrainingMode && !string.IsNullOrEmpty(trainingDataSaveTo))
            {
                var t = new Mat(_src, rect);
                t.SaveImage(@$"{trainingDataSaveTo}\{idx}.jpg");
            }

            var results = new Mat();
            var detectedClass = (int)kNearest.FindNearest(_reshape_mat, 1, results);

            ocrNumbers[idx] = detectedClass;
            idx+=1;
        }

        return ocrNumbers;
    }

    public KNearest TrainData(IList<ImageInfo> trainingImages)
    {
        var samples = new Mat();
        foreach (var trainingImage in trainingImages)
            samples.PushBack(trainingImage.Image);

        var labels = trainingImages.Select(x => x.ImageGroupId).ToArray();
        var responses = new Mat(labels.Length, 1, MatType.CV_32SC1, labels);
        var tmp = responses.Reshape(1);
        var responseFloat = new Mat();
        tmp.ConvertTo(responseFloat, MatType.CV_32F);

        var kNearest = KNearest.Create();
        kNearest.Train(samples, SampleTypes.RowSample, responseFloat);
        return kNearest;
    }

    private void Show(Mat? mat = null)
    {
        if (mat is null)
        {
            Cv2.ImShow("Current", _current_mat);
            Cv2.WaitKey();
        }
        else
        {
            Cv2.ImShow("Current", mat);
            Cv2.WaitKey();
        }
    }

    public IList<ImageInfo> ReadTrainingImages(string path, string ext)
    {
        var images = new List<ImageInfo>();

        var dirs = new DirectoryInfo(path).GetDirectories();
        if (dirs is null || !dirs.Any())
            throw new ArgumentNullException();

        var imageId = 0;
        foreach (var dir in dirs)
        {
            int groupId = Convert.ToInt32(dir.Name);
            foreach (var imageFile in dir.GetFiles(ext))
            {
                Mat? image = ProcessTrainingImage(new Mat(imageFile.FullName, ImreadModes.Grayscale));
                if (image is null)
                    continue;

                images.Add(new ImageInfo()
                {
                    Image = image,
                    ImageId = imageId++,
                    ImageGroupId = groupId
                });
            }
        }

        return images;
    }



    private static Mat? ProcessTrainingImage(Mat mat)
    {
        var threshImage = new Mat();
        Cv2.Threshold(mat, threshImage, Thresh, ThreshMaxValue, ThresholdTypes.BinaryInv);

        //Find contours
        Point[][] contours;
        HierarchyIndex[] hierarchyIndices;
        threshImage.FindContours(out contours, out hierarchyIndices, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);

        if (contours is null || !contours.Any())
            return null;

        Mat? result = null;
        var contourIndex = 0;
        while ((contourIndex >= 0))
        {
            var contour = contours[contourIndex];

            var boundingRect = Cv2.BoundingRect(contour); //Find bounding rect for each contour
            var roi = new Mat(threshImage, boundingRect); //Crop the image
            var resizedImage = new Mat();
            var resizedImageFloat = new Mat();
            Cv2.Resize(roi, resizedImage, new Size(20, 20)); //resize to 10X10
            resizedImage.ConvertTo(resizedImageFloat, MatType.CV_32FC1); //convert to float
            result = resizedImageFloat.Reshape(1, 1);

            contourIndex = hierarchyIndices[contourIndex].Next;
        }

        return result;
    }
}
