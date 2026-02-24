namespace VeradelAddin.Application.Common.Drawing.DrawingDTOs
{
    public class ConvertDrawingDTO
    {
        public string Filename { get; set; }
        public string Path { get; set; }
        public int Revision { get; internal set; }
    }
}