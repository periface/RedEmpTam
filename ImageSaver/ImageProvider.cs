using FileSaver.Manager;
using ImageSaver.Manager;

namespace ImageSaver
{
    public class ImageProvider : ImageManager
    {
        public ImageProvider(ISaverManager saverManager)
            : base(saverManager)
        {
        }
    }
}
