public class JwtConfig
{
    public string Secret { get; set; } = "QPTmWYuVWY6baHHdnriMDlL+ppoGIJJC1xO6pvRBYEY=";
    public int ExpirationMinutes { get; set; } = 60;
}