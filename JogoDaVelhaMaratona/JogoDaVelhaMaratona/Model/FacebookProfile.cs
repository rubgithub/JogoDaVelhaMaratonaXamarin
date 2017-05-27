namespace JogoDaVelhaMaratona.Model
{
    public class FacebookProfile
    {
        public Picture Picture { get; set; }
    }

    public class Picture
    {
        public Data Data { get; set; }
    }

    public class Data
    {
        public bool IsSilhouette { get; set; }
        public string Url { get; set; }
    }

}
