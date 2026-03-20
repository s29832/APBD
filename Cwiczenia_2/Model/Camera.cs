namespace Cwiczenia_2.Model;

public class Camera : Equipment
{
    public string Megapixels { get; private set; }
    public string Aperture { get; private set; }
    
    public Camera(string name, string megapixels, string aperture) : base(name)
    {
        Megapixels = megapixels;
        Aperture = aperture;
    }
}