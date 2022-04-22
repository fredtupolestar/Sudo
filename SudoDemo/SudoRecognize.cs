using OpenCvSharp;
using Sudo.OCR;
using System.Diagnostics;

namespace SudoDemo
{
    public partial class SudoRecognize : Form
    {
        private Mat _currentImage = null!;
        private OCRService? _ocr = null;

        public SudoRecognize()
        {
            InitializeComponent();
        }

        private void ctrl_load_image_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image File|*.jpg;*.jpeg;*.png;";
            var result = openFile.ShowDialog();
            if (result != DialogResult.OK)
                return;

            _currentImage = new Mat(openFile.FileName);
            _ocr = new OCRService(_currentImage);
            DisplayImage();
        }

        private void DisplayImage()
        {
            this.pic_sudo.Image = Image.FromStream(_currentImage.ToMemoryStream());
        }

        private void ctrl_process_gray_Click(object sender, EventArgs e)
        {
            _currentImage = Mat.FromStream(_currentImage.ToMemoryStream(), ImreadModes.Grayscale);
            DisplayImage();
        }

        private void ctrl_process_canny_Click(object sender, EventArgs e)
        {
            this._currentImage = _currentImage.Canny(50, 200);
            DisplayImage();
        }

        private void ctrl_process_ocr_Click(object sender, EventArgs e)
        {
            var file = @$"KNearestModel/number_ocr";
            var isModelFileExists = System.IO.File.Exists(file);
            if(!isModelFileExists)
                throw new ArgumentNullException("KNearest model file not exists.");

            var kNearest = OpenCvSharp.ML.KNearest.Load(file);
            if(_ocr is null)
                throw new ArgumentNullException("OCR Service is null");

            _currentImage = OpenCvSharp.Extensions.BitmapConverter.ToMat((Bitmap)this.pic_sudo.Image);
            _ocr = new OCRService(_currentImage);

            var ocr_numbers = _ocr!.DoOCR(kNearest, out Mat[] results,true, @"D:\training");

            OCRResult resultForm = new OCRResult(ocr_numbers,results);
            resultForm.TopLevel = true;
            resultForm.Show();
        }

        private void SudoRecognize_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                var dataObj = Clipboard.GetDataObject();
                if (dataObj.GetDataPresent(DataFormats.Bitmap))
                {
                    var bitmap = (Bitmap)dataObj.GetData(DataFormats.Bitmap);
                    _currentImage = OpenCvSharp.Extensions.BitmapConverter.ToMat(bitmap);
                    _ocr = new OCRService(_currentImage);
                    DisplayImage();
                }
                else
                {
                    MessageBox.Show("NotSupported");
                }
            }
        }
    }
}
