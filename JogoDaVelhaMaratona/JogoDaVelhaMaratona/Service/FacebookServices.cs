namespace JogoDaVelhaMaratona.Service
{
    public class FacebookServices
    {

        //public async Task<FacebookProfile> GetFacebookProfileAsync(string accessToken)
        //{
        //    //var facebook = new Facebook.FacebookGraphAPI(accessToken);

        //    //var args = FacebookGraphAPI.GetUserFromCookie(Request.Cookies, "YOUR_APP_ID", "YOUR_APP_SECRET");

        //    //get my profile
        //    //var user = facebook.GetObject("me", null);
            

        //    //"https://graph.facebook.com/v2.7/me/?fields=name,picture,work,website,religion,location,locale,link,cover,age_range,bio,birthday,devices,email,first_name,last_name,gender,hometown,is_verified,languages&access_token="
        //    var requestUrl =
        //        "https://graph.facebook.com/v2.7/me/?fields=name,picture,work,website,religion,location,locale,link,cover,age_range,bio,birthday,devices,email,first_name,last_name,gender,hometown,is_verified,languages&access_token=" + accessToken;
        //    //$"https://graph.facebook.com/v2.7/me/?fields=name,picture,first_name&access_token={accessToken}";

        //    var teste = $"jose{requestUrl}";

        //    var httpClient = new HttpClient();

        //    var userJson = await httpClient.GetStringAsync(requestUrl);

        //    var facebookProfile = JsonConvert.DeserializeObject<FacebookProfile>(userJson);

        //    return facebookProfile;
        //}
    }
}
