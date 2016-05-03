using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;

namespace DetectionEngine
{
    public class LettersDetector
    {
        public List<Rect> DetectLetters(Mat input)
        {
            Mat mask = Mat.Zeros(input.Size(), MatType.CV_8UC1);
            Mat[] contours;
            var bounds = new List<Rect>();
            var hierarchy = new List<Vec4i>();
            input.FindContours(out contours, OutputArray.Create(hierarchy), ContourRetrieval.CComp, ContourChain.ApproxSimple, new Point(0, 0));
            for (var idx = 0; idx >= 0; idx = hierarchy[idx][0])
            {
                var rect = Cv2.BoundingRect(contours[idx]);
                Cv2.DrawContours(mask, contours, idx, new Scalar(255, 255, 255));
                var maskRoi = new Mat(mask, rect);
                var r = (double)maskRoi.CountNonZero()/(double)(rect.Width*rect.Height);
                if (r > 0.10 && rect.Height > 8 && rect.Width > 8)
                {
                    bounds.Add(rect);
                }
            }
            return bounds;
        }
    }
}
