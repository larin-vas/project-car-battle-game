using Code.Common;

namespace Code.GameCamera
{
    public class CameraModel
    {
        public Transformation Transformation { get; }

        public Observable<float> CameraHeight { get; }

        public CameraModel(Transformation transformation, float cameraHeight)
        {
            Transformation = transformation;
            CameraHeight = new Observable<float>(cameraHeight);
        }
    }
}
